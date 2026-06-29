using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using RegistroVentas.Api.Contracts;
using RegistroVentas.Api.Data;
using RegistroVentas.Api.Security;

namespace RegistroVentas.Api.Business;

public sealed class RegistroOperacionService(RegistroDbContext db, Aes256CryptoService crypto, ILogger<RegistroOperacionService> logger) : IRegistroOperacionService 
{
    
    private static readonly Regex ReferenciaRegex = new("^[0-9]{8}$", RegexOptions.Compiled);

    public async Task<ServiceResult<RegistroOperacionResponse>> RegistrarVentaAsync(RegistrarVentaRequest request, CancellationToken cancellationToken)
    {
        var errors = ValidarVenta(request);

        if (errors.Count > 0)
        {
            logger.LogWarning("Validación fallida al registrar venta. Errores={Errores}", string.Join(" | ", errors) );

            return ServiceResult<RegistroOperacionResponse>.BadRequest(
                "Validación fallida.",
                errors
            );
        }

        string secretoDescifrado;

        try
        {
            secretoDescifrado = crypto.Decrypt(request.Secreto!);
        }
        catch (FormatException)
        {
            logger.LogWarning("El secreto recibido no tiene formato Base64 válido.");

            return ServiceResult<RegistroOperacionResponse>.BadRequest(
                "Validación fallida.",
                new[] { "El secreto debe venir cifrado en Base64." }
            );
        }
        catch (CryptographicException)
        {
            logger.LogWarning("El secreto recibido no pudo ser descifrado con AES-256.");

            return ServiceResult<RegistroOperacionResponse>.BadRequest(
                "Validación fallida.",
                new[] { "El secreto no pudo ser descifrado con AES-256." }
            );
        }

        if (string.IsNullOrWhiteSpace(secretoDescifrado))
        {
            logger.LogWarning("El secreto descifrado resultó vacío.");

            return ServiceResult<RegistroOperacionResponse>.BadRequest(
                "Validación fallida.",
                new[] { "El secreto descifrado no puede estar vacío." }
            );
        }

        var referencia = await GenerarReferenciaUnicaAsync(cancellationToken);

        var entity = new RegistroOperacion
        {
            Operacion = "venta",
            Importe = decimal.Parse(request.Importe!.Trim(), System.Globalization.CultureInfo.InvariantCulture),
            Cliente = request.Cliente!.Trim(),
            Referencia = referencia,
            Estatus = "Aprobado",
            Secreto = secretoDescifrado
        };

        db.RegistrosOperaciones.Add(entity);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Venta registrada. Pk={Pk}, Referencia={Referencia}, Cliente={Cliente}, Importe={Importe}",
            entity.Pk,
            entity.Referencia,
            entity.Cliente,
            entity.Importe
        );

        return ServiceResult<RegistroOperacionResponse>.Created(ToResponse(entity));
    }

    public async Task<RegistroOperacionResponse?> ObtenerPorPkAsync(int pk, CancellationToken cancellationToken)
    {
        var result = await db.RegistrosOperaciones
            .AsNoTracking()
            .Where(x => x.Pk == pk)
            .Select(x => ToResponse(x))
            .SingleOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            logger.LogInformation("Consulta por PK sin resultado. Pk={Pk}", pk );
        }

        return result;
    }

    public async Task<ServiceResult<PagedResult<RegistroOperacionResponse>>> ObtenerPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        if (page < 1)
        {
            logger.LogWarning("Consulta paginada con page inválido. Page={Page}, PageSize={PageSize}", page, pageSize );

            return ServiceResult<PagedResult<RegistroOperacionResponse>>.BadRequest(
                "Parámetro page inválido.",
                new[] { "page debe ser mayor o igual a 1." }
            );
        }

        if (pageSize < 1 || pageSize > 100)
        {
            logger.LogWarning("Consulta paginada con pageSize inválido. Page={Page}, PageSize={PageSize}", page, pageSize );

            return ServiceResult<PagedResult<RegistroOperacionResponse>>.BadRequest(
                "Parámetro pageSize inválido.",
                new[] { "pageSize debe estar entre 1 y 100." }
            );
        }

        var query = db.RegistrosOperaciones.AsNoTracking();

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Pk)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => ToResponse(x))
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "Consulta paginada ejecutada. Page={Page}, PageSize={PageSize}, Total={Total}, Items={Items}",
            page,
            pageSize,
            total,
            items.Count
        );

        var result = new PagedResult<RegistroOperacionResponse>(
            page,
            pageSize,
            total,
            items
        );

        return ServiceResult<PagedResult<RegistroOperacionResponse>>.Ok(result);
    }

    public async Task<ServiceResult<RegistroOperacionResponse>> CancelarPorReferenciaAsync(string referencia, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(referencia) || !ReferenciaRegex.IsMatch(referencia))
        {
            logger.LogWarning("Cancelación con referencia inválida. Referencia={Referencia}", referencia );

            return ServiceResult<RegistroOperacionResponse>.BadRequest(
                "Referencia inválida.",
                new[] { "La referencia debe ser numérica y de longitud 8." }
            );
        }

        var entity = await db.RegistrosOperaciones.SingleOrDefaultAsync(x => x.Referencia == referencia, cancellationToken);

        if (entity is null)
        {
            logger.LogInformation(
                "Cancelación solicitada para referencia inexistente. Referencia={Referencia}",
                referencia
            );

            return ServiceResult<RegistroOperacionResponse>.NotFound(
                "Referencia inexistente."
            );
        }

        if (string.Equals(entity.Estatus, "Cancelado", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogInformation(
                "Cancelación rechazada porque el registro ya estaba cancelado. Pk={Pk}, Referencia={Referencia}",
                entity.Pk,
                entity.Referencia
            );

            return ServiceResult<RegistroOperacionResponse>.Conflict(
                "El registro ya estaba cancelado."
            );
        }

        entity.Estatus = "Cancelado";

        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Registro cancelado. Pk={Pk}, Referencia={Referencia}",
            entity.Pk,
            entity.Referencia
        );

        return ServiceResult<RegistroOperacionResponse>.Ok(ToResponse(entity));
    }

    private static List<string> ValidarVenta(RegistrarVentaRequest request)
    {
        var errors = new List<string>();

        if (!System.Text.RegularExpressions.Regex.IsMatch(request.OperacionEntrada ?? string.Empty, @"\S+"))
        {
            errors.Add("operacion es requerida.");
        }
        //else if (!System.Text.RegularExpressions.Regex.IsMatch(request.OperacionEntrada!, @"^venta$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        //{
        //    errors.Add("operacion solo puede ser venta.");
        //}
        if (string.IsNullOrWhiteSpace(request.Importe))
        {
            errors.Add("importe es requerido.");
        }
        else if (!System.Text.RegularExpressions.Regex.IsMatch(request.Importe.Trim(), @"^(?!0+(?:\.0{1,2})?$)(?:0|[1-9]\d{0,8})(?:\.\d{1,2})?$"))
        {
            errors.Add("importe debe ser un número mayor a 0 con máximo 2 decimales.");
        }

        if (string.IsNullOrWhiteSpace(request.Cliente))
        {
            errors.Add("cliente es requerido.");
        }
        else if (!System.Text.RegularExpressions.Regex.IsMatch(request.Cliente.Trim(), @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ][A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s'.-]{1,79}$"))
        {
            errors.Add("cliente tiene un formato inválido.");
        }


        if (string.IsNullOrWhiteSpace(request.Secreto))
        {
            errors.Add("secreto es requerido.");
        }
        else if (!System.Text.RegularExpressions.Regex.IsMatch(request.Secreto.Trim(), @"^[A-Za-z0-9+/=_\-]{8,512}$"))
        {
            errors.Add("secreto tiene un formato inválido.");
        }

        return errors;
    }

    private async Task<string> GenerarReferenciaUnicaAsync(CancellationToken cancellationToken)
    {
        for (var i = 0; i < 10; i++)
        {
            var referencia = RandomNumberGenerator.GetInt32(0, 100_000_000).ToString("D8");

            var exists = await db.RegistrosOperaciones.AnyAsync(x => x.Referencia == referencia, cancellationToken);

            if (!exists)
            {
                return referencia;
            }
        }

        logger.LogError("No fue posible generar una referencia única después de 10 intentos.");

        throw new InvalidOperationException("No fue posible generar una referencia única.");
    }

    private static RegistroOperacionResponse ToResponse(RegistroOperacion entity)
    {
        return new RegistroOperacionResponse(
            entity.Pk,
            entity.Operacion,
            entity.Importe,
            entity.Cliente,
            entity.Referencia,
            entity.Estatus
        );
    }
}
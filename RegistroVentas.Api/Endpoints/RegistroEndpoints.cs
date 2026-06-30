using RegistroVentas.Api.Business;
using RegistroVentas.Api.Contracts;

namespace RegistroVentas.Api.Endpoints;

public static class RegistroEndpoints
{
    public static WebApplication MapRegistroEndpoints(this WebApplication app)
    {
        var registros = app.MapGroup("/registros") .RequireAuthorization("RolValido");


        registros.MapPost("", async (RegistrarVentaRequest request, IRegistroOperacionService service, CancellationToken cancellationToken) =>
        {
            var result = await service.RegistrarVentaAsync(request, cancellationToken);

            return result.Status switch
            {
                ServiceResultStatus.Created => Results.Created($"/registros/{result.Value!.Pk}", result.Value),
                ServiceResultStatus.BadRequest => Results.BadRequest(new
                {
                    error = result.Error,
                    details = result.Details
                }),

                _ => Results.Problem("Error inesperado.")
            };
        })
        .RequireAuthorization("Operador")
        .WithTags("Registros")
        .WithName("RegistrarVenta");


        registros.MapGet("/{pk:int}", async (int pk, IRegistroOperacionService service, CancellationToken cancellationToken) =>
        {
            var result = await service.ObtenerPorPkAsync(pk, cancellationToken);

            return result is null ? Results.NotFound(new { error = "Registro no encontrado." }) : Results.Ok(result);
        })
        .WithTags("Registros")
        .WithName("ObtenerRegistroPorPk");


        registros.MapGet("", async (int page, int pageSize, IRegistroOperacionService service, CancellationToken cancellationToken) =>
        {
            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;

            var result = await service.ObtenerPaginadoAsync(page, pageSize, cancellationToken);

            return result.Status switch
            {
                ServiceResultStatus.Ok => Results.Ok(result.Value),

                ServiceResultStatus.BadRequest => Results.BadRequest(new
                {
                    error = result.Error,
                    details = result.Details
                }),

                _ => Results.Problem("Error inesperado.")
            };
        })
        .WithTags("Registros")
        .WithName("ObtenerRegistrosPaginados");


        registros.MapPatch("/{referencia}/cancelacion", async (string referencia, IRegistroOperacionService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CancelarPorReferenciaAsync(referencia, cancellationToken);

            return result.Status switch
            {
                ServiceResultStatus.Ok => Results.Ok(result.Value),

                ServiceResultStatus.BadRequest => Results.BadRequest(new
                {
                    error = result.Error,
                    details = result.Details
                }),

                ServiceResultStatus.NotFound => Results.NotFound(new
                {
                    error = result.Error
                }),

                ServiceResultStatus.Conflict => Results.Conflict(new
                {
                    error = result.Error
                }),

                _ => Results.Problem("Error inesperado.")
            };
        })
        .RequireAuthorization("Supervisor")
        .WithTags("Registros")
        .WithName("CancelarRegistroPorReferencia");

        return app;
    }
}
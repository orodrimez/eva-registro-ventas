using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RegistroVentas.Api.Business;
using RegistroVentas.Api.Contracts;
using RegistroVentas.Api.Data;
using RegistroVentas.Api.Security;
using RegistroVentas.Api.Tests.TestSupport;

namespace RegistroVentas.Api.Tests.Business;

public sealed class RegistroOperacionServiceTests
{
    [Fact]
    public async Task RegistrarVentaAsync_WithValidRequest_ReturnsCreatedAndStoresApprovedSale()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);
        var crypto = CreateCryptoService();

        var request = new RegistrarVentaRequest
        {
            Operacion = "venta",
            Importe = "100.00",
            Cliente = "Angel",
            Secreto = crypto.EncryptForDev("secreto-prueba")
        };

        // Act
        var result = await service.RegistrarVentaAsync(request, CancellationToken.None );

        // Assert
        Assert.Equal(ServiceResultStatus.Created, result.Status);
        Assert.NotNull(result.Value);

        Assert.True(result.Value!.Pk > 0);
        Assert.Equal("venta", result.Value.Operacion);
        Assert.Equal(100.00m, result.Value.Importe);
        Assert.Equal("Angel", result.Value.Cliente);
        Assert.Equal("Aprobado", result.Value.Estatus);

        Assert.Matches("^[0-9]{8}$", result.Value.Referencia);
    }


    [Fact]
    public async Task ObtenerPorPkAsync_WithExistingPk_ReturnsRegistro()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        var registro = await InsertVentaAsync(
            database.DbContext,
            cliente: "Angel",
            referencia: "12345678"
        );

        // Act
        var result = await service.ObtenerPorPkAsync(registro.Pk, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(registro.Pk, result!.Pk);
        Assert.Equal("Angel", result.Cliente);
        Assert.Equal("12345678", result.Referencia);
        Assert.Equal("Aprobado", result.Estatus);
    }

    [Fact]
    public async Task ObtenerPorPkAsync_WithNonExistingPk_ReturnsNull()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        // Act
        var result = await service.ObtenerPorPkAsync(
            999,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ObtenerPaginadoAsync_PageOnePageSizeTen_ReturnsMaxTenItemsAndTotal()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        for (var i = 1; i <= 25; i++)
        {
            await InsertVentaAsync(database.DbContext, cliente: $"Cliente {i}", referencia: i.ToString("D8"));
        }

        // Act
        var result = await service.ObtenerPaginadoAsync(page: 1, pageSize: 10, CancellationToken.None);

        // Assert
        Assert.Equal(ServiceResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);

        Assert.Equal(1, result.Value!.Page);
        Assert.Equal(10, result.Value.PageSize);
        Assert.Equal(25, result.Value.Total);
        Assert.Equal(10, result.Value.Items.Count);
        Assert.Equal(1, result.Value.Items.First().Pk);
    }


    [Fact]
    public async Task CancelarPorReferenciaAsync_WithExistingReference_CancelsRegistro()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        await InsertVentaAsync(
            database.DbContext,
            cliente: "Angel",
            referencia: "12345678"
        );

        // Act
        var result = await service.CancelarPorReferenciaAsync("12345678", CancellationToken.None);

        // Assert
        Assert.Equal(ServiceResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);

        Assert.Equal("Cancelado", result.Value!.Estatus);
        Assert.Equal("12345678", result.Value.Referencia);
    }

    [Fact]
    public async Task CancelarPorReferenciaAsync_WithAlreadyCancelledRegistro_ReturnsConflict()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        await InsertVentaAsync(
            database.DbContext,
            cliente: "Angel",
            referencia: "12345678",
            estatus: "Cancelado"
        );

        // Act
        var result = await service.CancelarPorReferenciaAsync("12345678", CancellationToken.None );

        // Assert
        Assert.Equal(ServiceResultStatus.Conflict, result.Status);
        Assert.Equal("El registro ya estaba cancelado.", result.Error);
    }

    [Fact]
    public async Task CancelarPorReferenciaAsync_WithNonExistingReference_ReturnsNotFound()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        // Act
        var result = await service.CancelarPorReferenciaAsync("99999999", CancellationToken.None );

        // Assert
        Assert.Equal(ServiceResultStatus.NotFound, result.Status);
        Assert.Equal("Referencia inexistente.", result.Error);
    }

    [Fact]
    public async Task CancelarPorReferenciaAsync_WithInvalidReference_ReturnsBadRequest()
    {
        // Arrange
        using var database = new SqliteTestDatabase();
        var service = CreateService(database.DbContext);

        // Act
        var result = await service.CancelarPorReferenciaAsync("ABC123", CancellationToken.None);

        // Assert
        Assert.Equal(ServiceResultStatus.BadRequest, result.Status);
        Assert.Equal("Referencia inválida.", result.Error);
    }

    private static RegistroOperacionService CreateService(RegistroDbContext dbContext)
    {
        return new RegistroOperacionService(dbContext, CreateCryptoService(), NullLogger<RegistroOperacionService>.Instance);
    }

    private static Aes256CryptoService CreateCryptoService()
    {
        var options = Options.Create(new CryptoOptions
        {
            AesKeyBase64 = "MDEyMzQ1Njc4OWFiY2RlZjAxMjM0NTY3ODlhYmNkZWY="
        });

        return new Aes256CryptoService(options);
    }

    private static async Task<RegistroOperacion> InsertVentaAsync(RegistroDbContext dbContext, 
        string cliente, string referencia, string estatus = "Aprobado")
    {
        var entity = new RegistroOperacion
        {
            Operacion = "venta",
            Importe = 100.00m,
            Cliente = cliente,
            Referencia = referencia,
            Estatus = estatus,
            Secreto = "secreto-descifrado"
        };

        dbContext.RegistrosOperaciones.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity;
    }
}
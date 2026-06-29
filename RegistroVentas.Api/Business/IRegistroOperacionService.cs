using RegistroVentas.Api.Contracts;

namespace RegistroVentas.Api.Business;

public interface IRegistroOperacionService
{
    Task<ServiceResult<PagedResult<RegistroOperacionResponse>>> ObtenerPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<ServiceResult<RegistroOperacionResponse>> RegistrarVentaAsync(RegistrarVentaRequest request, CancellationToken cancellationToken);
    Task<ServiceResult<RegistroOperacionResponse>> CancelarPorReferenciaAsync(string referencia, CancellationToken cancellationToken);
    Task<RegistroOperacionResponse?> ObtenerPorPkAsync(int pk, CancellationToken cancellationToken);    

}
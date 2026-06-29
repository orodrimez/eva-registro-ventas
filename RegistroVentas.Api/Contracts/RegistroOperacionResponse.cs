namespace RegistroVentas.Api.Contracts;

public sealed record RegistroOperacionResponse(
    int Pk,
    string Operacion,
    decimal Importe,
    string Cliente,
    string Referencia,
    string Estatus
);
namespace RegistroVentas.Api.Data;

public sealed class RegistroOperacion
{
    public int Pk { get; set; }

    public string Operacion { get; set; } = string.Empty;

    public decimal Importe { get; set; }

    public string Cliente { get; set; } = string.Empty;

    public string Referencia { get; set; } = string.Empty;

    public string Estatus { get; set; } = string.Empty;

    public string Secreto { get; set; } = string.Empty;
}
using System.Text.Json.Serialization;

namespace RegistroVentas.Api.Contracts;

public sealed class RegistrarVentaRequest
{
    [JsonPropertyName("operacion")]
    public string? Operacion { get; init; }

    [JsonPropertyName("operación")]
    public string? OperacionConAcento { get; init; }

    [JsonIgnore]
    public string? OperacionEntrada => Operacion ?? OperacionConAcento;

    [JsonPropertyName("importe")]
    public string? Importe { get; init; }

    [JsonPropertyName("cliente")]
    public string? Cliente { get; init; }

    [JsonPropertyName("secreto")]
    public string? Secreto { get; init; }
}
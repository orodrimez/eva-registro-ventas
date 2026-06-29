namespace RegistroVentas.Api.Contracts;

public sealed record TokenRequest(string Usuario, string Rol);

public sealed record TokenResponse(string AccessToken, string TokenType, int ExpiresInSeconds);

public sealed record EncryptRequest(string Valor);

public sealed record EncryptResponse(string Secreto);
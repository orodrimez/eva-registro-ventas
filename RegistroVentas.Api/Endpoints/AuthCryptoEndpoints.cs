using RegistroVentas.Api.Contracts;
using RegistroVentas.Api.Security;

namespace RegistroVentas.Api.Endpoints;

public static class AuthCryptoEndpoints
{
    public static WebApplication MapAuthCryptoEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/token", (TokenRequest request, JwtTokenService jwt, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("AuthEndpoints");

            if (string.IsNullOrWhiteSpace(request.Usuario))
            {
                logger.LogWarning("Solicitud de token rechazada. Usuario vacío.");

                return Results.BadRequest(new
                {
                    error = "usuario es requerido."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Rol))
            {
                logger.LogWarning("Solicitud de token rechazada. Usuario={Usuario}, Rol vacío.", request.Usuario);

                return Results.BadRequest(new
                {
                    error = "rol es requerido."
                });
            }

            const int expiresInSeconds = 7200;

            var token = jwt.CreateToken(request.Usuario, request.Rol, expiresInSeconds);

            logger.LogInformation(
                "Token generado. Usuario={Usuario}, Rol={Rol}, ExpiresInSeconds={ExpiresInSeconds}",
                request.Usuario,
                request.Rol,
                expiresInSeconds
            );

            return Results.Ok(new TokenResponse(token, "Bearer", expiresInSeconds));
        });

        app.MapPost("/crypto/encrypt", (EncryptRequest request, Aes256CryptoService crypto, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CryptoEndpoints");

            if (string.IsNullOrWhiteSpace(request.Valor))
            {
                logger.LogWarning("Solicitud de cifrado rechazada. Valor vacío.");

                return Results.BadRequest(new
                {
                    error = "valor es requerido."
                });
            }

            var secreto = crypto.EncryptForDev(request.Valor);

            logger.LogInformation("Valor cifrado correctamente con AES-256.");

            return Results.Ok(new EncryptResponse(secreto));
        });

        return app;
    }
}
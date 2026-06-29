using System.Text.Json;

namespace RegistroVentas.Api.Infrastructure;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            logger.LogWarning("Request cancelado por el cliente. TraceId={TraceId}, Path={Path}", context.TraceIdentifier, context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error no controlado. TraceId={TraceId}, Method={Method}, Path={Path}", context.TraceIdentifier, context.Request.Method, context.Request.Path );

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problem = new
            {
                type = "https://httpstatuses.com/500",
                title = "Error interno del servidor.",
                status = 500,
                traceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);
        }
    }
}
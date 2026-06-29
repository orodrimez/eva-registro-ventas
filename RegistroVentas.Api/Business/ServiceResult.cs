namespace RegistroVentas.Api.Business;

public enum ServiceResultStatus
{
    Created,
    Ok,
    BadRequest,
    NotFound,
    Conflict
}

public sealed record ServiceResult<T>(ServiceResultStatus Status, T? Value = default, string? Error = null, object? Details = null )
{
    public static ServiceResult<T> Created(T value)
        => new(ServiceResultStatus.Created, value);

    public static ServiceResult<T> Ok(T value)
        => new(ServiceResultStatus.Ok, value);

    public static ServiceResult<T> BadRequest(string error, object? details = null)
        => new(ServiceResultStatus.BadRequest, default, error, details);

    public static ServiceResult<T> NotFound(string error)
        => new(ServiceResultStatus.NotFound, default, error);

    public static ServiceResult<T> Conflict(string error)
        => new(ServiceResultStatus.Conflict, default, error);
}
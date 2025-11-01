namespace Skolar.Api.Extensions;

public record ApiResponse<T>(
    T? Data,
    string Message,
    string Status,
    int StatusCode,
    string Type,
    object? Errors = null)
{
    public DateTime Timestamp => DateTime.UtcNow;

    public static ApiResponse<T> Success(
        T data,
        string type,
        int statusCode,
        string message = "Request successful")
        => new(data, message, "SUCCESS", statusCode, type, null);

    public static ApiResponse<T> Failure(
        string type,
        int statusCode,
        string message,
        object? errors = null)
        => new(default, message, "FAILED", statusCode, type, errors);
}

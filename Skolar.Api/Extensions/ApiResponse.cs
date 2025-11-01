namespace Skolar.Api.Extensions;

public record ApiResponse<T>(T Data, string Message = "Request successful")
{
    public string Status => "SUCCESS";
    public DateTime Timestamp => DateTime.UtcNow;
}

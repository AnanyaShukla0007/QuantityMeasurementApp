namespace Shared.Common.Responses;

public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
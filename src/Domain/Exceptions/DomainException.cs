namespace Domain.Exceptions;

public class DomainException : Exception
{
    public int HttpStatusCode { get; init; }
    public object? Error { get; init; }
    public string ErrorCode { get; init; } = default!;

    public DomainException(string message, string errorCode, int statusCode, object? error = null) : base(message)
    {
        HttpStatusCode = statusCode;
        Error = error;
        ErrorCode = errorCode;
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }


}
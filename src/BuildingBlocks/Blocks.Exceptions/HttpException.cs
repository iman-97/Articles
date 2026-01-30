using System.Net;

namespace Blocks.Exceptions;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public HttpException(HttpStatusCode statusCode, string message)
        : base(string.IsNullOrEmpty(message) ? statusCode.ToString() : message)
    {
        this.StatusCode = statusCode;
    }

    public HttpException(HttpStatusCode statusCode, string message, Exception ex)
        : base(message, ex)
    {
        this.StatusCode = statusCode;
    }
}

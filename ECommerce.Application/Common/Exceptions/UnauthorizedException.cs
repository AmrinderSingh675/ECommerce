namespace ECommerce.Application.Common.Exceptions;

// Custom exception class for handling unauthorized access errors in the middleware.
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}
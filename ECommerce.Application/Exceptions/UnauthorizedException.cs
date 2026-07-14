namespace ECommerce.Application.Exceptions;

// Custom exception class for handling unauthorized access errors in the middleware.
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}
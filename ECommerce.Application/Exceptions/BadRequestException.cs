namespace ECommerce.Application.Exceptions;


// Custom exception class for handling bad request errors in the middleware.
public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}
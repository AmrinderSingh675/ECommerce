namespace ECommerce.Application.Exceptions;

// Custom exception class for handling not found errors in the middleware.
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}
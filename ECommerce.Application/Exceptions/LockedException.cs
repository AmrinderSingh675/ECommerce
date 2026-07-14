namespace ECommerce.Application.Exceptions;

// Custom exception class for handling locaked access errors in the middleware.
public class LockedException : Exception
{
    public LockedException(string message)
        : base(message)
    {
    }
}
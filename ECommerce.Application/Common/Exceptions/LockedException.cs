namespace ECommerce.Application.Common.Exceptions;

public class LockedException : Exception
{
    public LockedException(string message)
        : base(message)
    {
    }
}
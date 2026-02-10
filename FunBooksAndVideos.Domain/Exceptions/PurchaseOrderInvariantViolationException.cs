namespace FunBooksAndVideos.Domain.Exceptions;

public class PurchaseOrderInvariantViolationException: Exception
{
    public PurchaseOrderInvariantViolationException(string message) : base(message)
    {
    }
}

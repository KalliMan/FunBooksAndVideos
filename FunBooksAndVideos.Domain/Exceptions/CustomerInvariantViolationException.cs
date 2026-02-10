namespace FunBooksAndVideos.Domain.Exceptions;

public class CustomerInvariantViolationException : Exception
{
    public CustomerInvariantViolationException(string message) : base(message) { }
}

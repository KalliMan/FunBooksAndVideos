namespace FunBooksAndVideos.Application.Exceptions;

public class PersistenceException: Exception
{
    public PersistenceException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

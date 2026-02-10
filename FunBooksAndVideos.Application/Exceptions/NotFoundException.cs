namespace FunBooksAndVideos.Application.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
    
    public NotFoundException(string name, int id)
        : base($"{name} ({id}) was not found!")
    {
    }
}

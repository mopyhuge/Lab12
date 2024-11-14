
public class GameFullException : Exception
{
    public GameFullException()
    {
    }

    public GameFullException(string? message) : base(message)
    {
    }

    public GameFullException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
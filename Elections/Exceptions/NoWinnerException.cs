namespace Elections.Exceptions;

public class NoWinnerException : Exception
{
    public NoWinnerException() : base("No Winner determined")
    {

    }

    public NoWinnerException(string message) : base(message)
    {

    }
}

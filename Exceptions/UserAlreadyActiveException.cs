namespace airtext_api.Exceptions;

public class UserAlreadyActiveException : Exception
{
    public UserAlreadyActiveException(string message) : base(message) { }
}

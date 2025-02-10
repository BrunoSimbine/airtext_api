namespace airtext_api.Exceptions;

public class UsernameExistsException : Exception
{
    public UsernameExistsException(string message) : base(message) { }
}

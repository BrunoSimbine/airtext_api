namespace airtext_api.Exceptions;

public class UserOrPassInvalidException : Exception
{
    public UserOrPassInvalidException(string message) : base(message) { }
}

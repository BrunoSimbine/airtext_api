namespace airtext_api.Exceptions;

public class EmailExistsException : Exception
{
    public EmailExistsException(string message) : base(message) { }
}

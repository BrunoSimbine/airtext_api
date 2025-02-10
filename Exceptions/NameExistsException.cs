namespace airtext_api.Exceptions;

public class NameExistsException : Exception
{
    public NameExistsException(string message) : base(message) { }
}

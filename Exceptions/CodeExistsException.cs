namespace airtext_api.Exceptions;

public class CodeExistsException : Exception
{
    public CodeExistsException(string message) : base(message) { }
}

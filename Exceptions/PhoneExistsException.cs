namespace airtext_api.Exceptions;

public class PhoneExistsException : Exception
{
    public PhoneExistsException(string message) : base(message) { }
}

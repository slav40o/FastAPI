namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class EmailConfigurationException : ValidationException
{
    public EmailConfigurationException() { }

    public EmailConfigurationException(string error) : base(error) { }
}
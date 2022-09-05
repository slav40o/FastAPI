namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class InvalidEmailException : ValidationException
{
    public InvalidEmailException() { }

    public InvalidEmailException(string error) : base(error) { }
}
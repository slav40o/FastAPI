namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class EmailSendingException : ValidationException
{
    public EmailSendingException()
    { }

    public EmailSendingException(string error) : base(error)
    { }
}
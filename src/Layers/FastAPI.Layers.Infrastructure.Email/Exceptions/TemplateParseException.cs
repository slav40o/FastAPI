namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class TemplateParseException : ValidationException
{
    public TemplateParseException()
    { }

    public TemplateParseException(string error) : base(error)
    { }
}
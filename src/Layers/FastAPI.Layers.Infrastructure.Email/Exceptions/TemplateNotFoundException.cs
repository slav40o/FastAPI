namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class TemplateNotFoundException : ValidationException
{
    public TemplateNotFoundException()
    { }

    public TemplateNotFoundException(string error) : base(error)
    { }
}
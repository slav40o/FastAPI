namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class TemplateRenderException : ValidationException
{
    public TemplateRenderException()
    { }

    public TemplateRenderException(string error) : base(error)
    { }
}
namespace FastAPI.Layers.Domain.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

public class DomainException : ValidationException
{
    public DomainException()
    {
    }

    public DomainException(string error)
        : base(error)
    {
    }
}

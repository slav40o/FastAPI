namespace FastAPI.Layers.Domain.Common.Phone;

using FastAPI.Layers.Domain.Exceptions;

public class InvalidPhoneNumberException : DomainException
{
    public InvalidPhoneNumberException()
    {
    }

    public InvalidPhoneNumberException(string error) 
        : base(error)
    {
    }
}
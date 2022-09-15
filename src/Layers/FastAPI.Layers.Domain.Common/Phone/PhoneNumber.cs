namespace FastAPI.Layers.Domain.Common.Phone;

using FastAPI.Layers.Domain.Entities.ValueObjects;
using FastAPI.Libraries.Validation;

public sealed record PhoneNumber : ValueObject
{
    public PhoneNumber(string number)
    {
        ValidateModel(number);
        this.Number = number;
    }

    public string Number { get; }

    public static implicit operator string(PhoneNumber number) => number.Number;

    public static implicit operator PhoneNumber(string number) => new(number);

    private static void ValidateModel(string phoneNumber)
    {
        Ensure.IsValidPhoneNumber<InvalidPhoneNumberException>(phoneNumber);
    }
}
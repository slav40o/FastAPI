namespace FastAPI.Libraries.Validation;

using FastAPI.Libraries.Validation.Resources;

public sealed class ValidationMessagesProvider
{
    internal static string ForNullValue(string paramName)
        => string.Format(ValidationMessages.NullValueErrorMessageFormat, paramName);

    internal static string ForEmptyString(string paramName)
        => string.Format(ValidationMessages.EmptyStringErrorMessageFormat, paramName);

    internal static string ForWrongFormat(string paramName)
        => string.Format(ValidationMessages.WrongFormatErrorMessageFormat, paramName);

    internal static string ForMaxLength(string paramName, int maxLength)
        => string.Format(ValidationMessages.MaxLengthErrorMessageFormat, paramName, maxLength);

    internal static string ForMinLength(string paramName, int minLength)
        => string.Format(ValidationMessages.MinLengthErrorMessageFormat, paramName, minLength);

    internal static string ForLessThan<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => string.Format(ValidationMessages.LessThanErrorMessageFormat, paramName, second);

    internal static string ForLessThanOrEqualTo<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => string.Format(ValidationMessages.LessThanOrEqualToErrorMessageFormat, paramName, second);

    internal static string ForLessThanZero(string paramName)
        => string.Format(ValidationMessages.LessThanZeroErrorMessageFormat, paramName);

    internal static string ForLessThanOrEqualToZero(string paramName)
        => string.Format(ValidationMessages.ForLessThanOrEqualToZeroErrorMessageFormat, paramName);

    internal static string ForGreaterThan<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => string.Format(ValidationMessages.GreaterThanErrorMessageFormat, paramName, second);

    internal static string ForGreaterThanOrEqualTo<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => string.Format(ValidationMessages.ForGreaterThanOrEqualToErrorMessageFormat, paramName, second);

    internal static string ForGreaterThanZero(string paramName)
        => string.Format(ValidationMessages.ForGreaterThanZeroErrorMessageFormat, paramName);

    internal static string ForGreaterThanOrEqualToZero(string paramName)
        => string.Format(ValidationMessages.ForHasOnlyDigitsErrorMessageFormat, paramName);

    internal static string ForHasOnlyDigits(string paramName)
        => string.Format(ValidationMessages.ForHasOnlyDigitsErrorMessageFormat, paramName);

    internal static string ForIsValidUrl(string paramName)
        => string.Format(ValidationMessages.ForIsValidUrlErrorMessageFormat, paramName);
}

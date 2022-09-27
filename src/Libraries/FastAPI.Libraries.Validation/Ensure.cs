namespace FastAPI.Libraries.Validation;

using Exceptions;

using System.Numerics;
using System.Text.RegularExpressions;

public static class Ensure
{
    public static void NotNull<TValue, TException>(TValue value, string paramName, string? message = null)
        where TValue : class
        where TException : ValidationException, new()
            => That<TValue, TException>(
                value,
                val => val is not null,
                message ?? ValidationMessagesProvider.ForNullValue(paramName));

    public static void NotEmpty<T, TException>(T value, string paramName, string? message = null)
        where T : IEnumerable<T>
        where TException : ValidationException, new()
    {
        NotNull<IEnumerable<T>, TException>(value, paramName, message);
        That<T, TException>(
            value,
            val => val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));
    }

    #region String
    public static void NotEmpty<TException>(string value, string paramName, string? message = null)
        where TException : ValidationException, new()
    {
        NotNull<string, TException>(value, paramName, message);
        That<string, TException>(
            value,
            val => val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));
    }

    public static void HasMinLength<TException>(string value, int minLength, string paramName)
        where TException : ValidationException, new()
    {
        NotNull<string, TException>(value, paramName);
        That<string, TException>(value, value => value.Length >= minLength, ValidationMessagesProvider.ForMinLength(paramName, minLength));
    }

    public static void HasMaxLength<TException>(string value, int maxLength, string paramName)
        where TException : ValidationException, new()
    {
        NotNull<string, TException>(value, paramName);
        That<string, TException>(value, value => value.Length <= maxLength, ValidationMessagesProvider.ForMaxLength(paramName, maxLength));
    }

    public static void HasValidFormat<TException>(string value, Regex format, string paramName)
        where TException : ValidationException, new()
    {
        NotEmpty<TException>(value, paramName);
        if (!format.IsMatch(value))
        {
            ThrowException<TException>(ValidationMessagesProvider.ForWrongFormat(paramName));
        }
    }

    public static void IsValidEmail<TException>(string email)
        where TException : ValidationException, new()
    {
        HasValidFormat<TException>(email, ValidationConstants.Email.Format, nameof(email));
    }

    public static void IsValidPhoneNumber<TException>(string phone)
        where TException : ValidationException, new()
    {
        HasMinLength<TException>(phone, ValidationConstants.Phone.MinLength, nameof(phone));
        HasMaxLength<TException>(phone, ValidationConstants.Phone.MaxLength, nameof(phone));
        HasValidFormat<TException>(phone, ValidationConstants.Phone.Format, nameof(phone));
    }

    public static void HasOnlyDigits<TException>(string value, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForHasOnlyDigits(paramName);
        That<string, TException>(value, v => v.All(c => c >= '0' && c <= '9'), message);
    }

    #endregion

    #region Less Than
    public static void LessThan<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForLessThan(paramName, second);
        bool assertion(TValue val) => val.CompareTo(second) < 0;

        That<TValue, TException>(first, assertion, message);
    }

    public static void LessThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualTo(paramName, second);
        bool assertion(TValue val) => val.CompareTo(second) <= 0;

        That<TValue, TException>(first, assertion, message);
    }

    public static void LessThanZero<T, TException>(T value, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
        where T : INumber<T>
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForLessThanZero(paramName);
        LessThan<T, TException>(value, T.Zero, paramName, message);
    }

    public static void LessThanOrEqualToZero<T, TException>(T value, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
        where T : INumber<T>
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualToZero(paramName);
        LessThanOrEqualTo<T, TException>(value, T.Zero, message);
    }
    #endregion

    #region Greater Than
    public static void GreaterThan<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForGreaterThan(paramName, second);
        bool assertion(TValue val) => val.CompareTo(second) > 0;

        That<TValue, TException>(first, assertion, message);
    }

    public static void GreaterThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForGreaterThanOrEqualTo(paramName, second);
        bool assertion(TValue val) => val.CompareTo(second) >= 0;

        That<TValue, TException>(first, assertion, message);
    }

    public static void GreaterThanZero<T, TException>(T value, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
        where T : INumber<T>
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForGreaterThanZero(paramName);
        GreaterThan<T, TException>(value, T.Zero, paramName, message);
    }

    public static void GreaterThanOrEqualToZero<T, TException>(T value, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
        where T : INumber<T>
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForGreaterThanOrEqualToZero(paramName);
        GreaterThanOrEqualTo<T, TException>(value, T.Zero, paramName, message);
    }

    #endregion

    public static void IsValidUrl<TException>(string url, string paramName, string? errorMessage = null)
        where TException : ValidationException, new()
    {
        string message = errorMessage ?? ValidationMessagesProvider.ForIsValidUrl(paramName);
        That<string, TException>(url, v => url.Length <= 2048 && Uri.IsWellFormedUriString(url, UriKind.Absolute), message);
    }

    public static void That<TValue, TException>(TValue value, Func<TValue, bool> assertion, string errorMessage)
        where TException : ValidationException, new()
    {
        if (!assertion.Invoke(value))
        {
            ThrowException<TException>(errorMessage);
        }
    }

    private static void ThrowException<TException>(string message)
        where TException : ValidationException, new()
    {
        var ex = new TException
        {
            ErrorMessage = message,
        };

        throw ex;
    }
}

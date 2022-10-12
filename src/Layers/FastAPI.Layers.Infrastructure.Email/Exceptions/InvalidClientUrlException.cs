namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

/// <summary>
/// Invalid client URL exception.
/// </summary>
public sealed class InvalidClientUrlException : ValidationException
{
    private const string DefaultMessage = "Invalid client URL value.";

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidClientUrlException"/> class.
    /// </summary>
    public InvalidClientUrlException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidClientUrlException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public InvalidClientUrlException(string message)
        : base(message)
    {
    }
}

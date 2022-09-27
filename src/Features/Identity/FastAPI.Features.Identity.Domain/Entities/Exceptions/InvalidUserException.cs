namespace FastAPI.Features.Identity.Domain.Entities.Exceptions;

using FastAPI.Layers.Domain.Exceptions;

/// <summary>
/// Represents an invalid user error.
/// </summary>
public sealed class InvalidUserException : DomainException
{
    private const string DefaultMessage = "Invalid user.";

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
    /// </summary>
    public InvalidUserException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public InvalidUserException(string message)
        : base(message)
    {
    }
}

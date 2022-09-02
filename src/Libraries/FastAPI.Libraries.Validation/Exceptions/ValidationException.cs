namespace FastAPI.Libraries.Validation.Exceptions;

using FastAPI.Libraries.Validation.Resources;

public class ValidationException : Exception
{
    private static readonly string DefaultMessage = ValidationMessages.DefaultValidationExceptionMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
    /// </summary>
    public ValidationException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
    /// </summary>
    /// <param name="message">Specifies a concrete message.</param>
    public ValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Gets or sets the Exception's Message.
    /// </summary>
    public virtual string ErrorMessage { get; set; } = DefaultMessage;

    /// <inheritdoc cref="Exception.Message"/>
    public override string Message => this.ErrorMessage;
}

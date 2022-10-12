namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

/// <summary>
/// Invalid template configuration exception.
/// </summary>
public sealed class InvalidEmailConfigurationException : ValidationException
{
    private const string DefaultMessage = "Invalid email configuration provided!";

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    public InvalidEmailConfigurationException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public InvalidEmailConfigurationException(string message)
        : base(message)
    {
    }
}

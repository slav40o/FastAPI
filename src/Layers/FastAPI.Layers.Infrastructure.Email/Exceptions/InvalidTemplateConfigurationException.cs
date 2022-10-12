namespace FastAPI.Layers.Infrastructure.Email.Exceptions;

using FastAPI.Libraries.Validation.Exceptions;

/// <summary>
/// Invalid template configuration exception.
/// </summary>
public sealed class InvalidTemplateConfigurationException : ValidationException
{
    private const string DefaultMessage = "Invalid template configuration provided!";

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    public InvalidTemplateConfigurationException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public InvalidTemplateConfigurationException(string message)
        : base(message)
    {
    }
}

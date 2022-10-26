namespace FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// General email layout view model interface.
/// </summary>
public interface IEmailLayoutModel
{
    /// <summary>
    /// Gets or sets layout internal body content.
    /// This content is the dynamic part rendered for each different template view.
    /// </summary>
    string Content { get; }

    /// <summary>
    /// Set content.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    void SetContent(string content);

    /// <summary>
    /// Copy's the current instance into a new one with the given new content.
    /// </summary>
    /// <param name="content">New layout content(rendered body)</param>
    /// <returns>Copied instance.</returns>
    IEmailLayoutModel CopyWithNewContent(string content);
}

namespace FastAPI.Layers.Infrastructure.Email.Settings;

/// <summary>
/// Holds settings information used from <see cref="SendGridEmailSender.cs"/>.
/// </summary>
public sealed class EmailSettings
{
    /// <summary>
    /// Gets or sets secret external API key.
    /// </summary>
    public string ApiKey { get; init; } = default!;

    /// <summary>
    /// Gets or sets email address that is the sender.
    /// </summary>
    public string SenderAddress { get; init; } = default!;

    /// <summary>
    /// Gets or sets friendly name that will appear instead of the sender email address in mail clients.
    /// </summary>
    public string SenderName { get; init; } = default!;

    /// <summary>
    /// Gets or sets Client URL.
    /// </summary>
    public string ClientURL { get; init; } = default!;
}

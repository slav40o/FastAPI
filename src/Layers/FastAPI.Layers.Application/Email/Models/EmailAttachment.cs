namespace FastAPI.Layers.Application.Email.Models;

/// <summary>
/// Main email attachment implementation.
/// </summary>
public sealed class EmailAttachment : IEmailAttachment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    public EmailAttachment(string name, byte[] content)
    {
        Name = name;
        Content = content;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MIME type.</param>
    public EmailAttachment(string name, byte[] content, string? type)
        : this(name, content)
    {
        Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MIME type.</param>
    /// <param name="disposition">Attachment disposition type. Either "attachment" or "inline" ("attachment" by default).</param>
    /// <param name="contentId">Id used in the email body for displaying the attachment(cid).</param>
    public EmailAttachment(string name, byte[] content, string? type, string? disposition, string? contentId)
        : this(name, content, type)
    {
        Disposition = disposition;
        ContentId = contentId;
    }

    /// <inheritdoc/>
    public string Name { get; private set; }

    /// <inheritdoc/>
    public string? Type { get; private set; }

    /// <inheritdoc/>
    public string? Disposition { get; private set; }

    /// <inheritdoc/>
    public string? ContentId { get; private set; }

    /// <inheritdoc/>
    public byte[] Content { get; private set; }

    /// <summary>
    /// Create in-line attachment.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MYME type.</param>
    /// <param name="contentId">Attachment cid.</param>
    /// <returns>New email attachment.</returns>
    public static EmailAttachment CreateInlineAttachment(string name, byte[] content, string? type, string contentId)
        => new(name, content, type, EmailDispositionTypes.Inline, contentId);

    /// <inheritdoc/>
    public Stream GetContentStream()
    {
        return new MemoryStream(Content);
    }
}

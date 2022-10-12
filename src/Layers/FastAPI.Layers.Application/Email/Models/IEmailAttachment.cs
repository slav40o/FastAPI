namespace FastAPI.Layers.Application.Email.Models;

/// <summary>
/// General email attachment interface.
/// </summary>
public interface IEmailAttachment
{
    /// <summary>
    /// Gets attachment name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets attachment type. For example application/pdf, image/jpeg, etc.
    /// </summary>
    string? Type { get; }

    /// <summary>
    /// Gets the disposition type. Either "attachment" or "inline".
    /// </summary>
    string? Disposition { get; }

    /// <summary>
    /// Gets content id for cid resources shown in the email body.
    /// </summary>
    string? ContentId { get; }

    /// <summary>
    /// Gets attachment content as byte array.
    /// </summary>
    byte[] Content { get; }

    /// <summary>
    /// Get content stream.
    /// </summary>
    /// <returns>Byte content as stream.</returns>
    Stream GetContentStream();
}

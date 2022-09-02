namespace FastAPI.Layers.Domain.Entities.Abstractions;

/// <summary>
/// This context class provides extra data for the creator/modifier that makes changes to the domain.
/// </summary>
public interface IModifierDetails
{
    /// <summary>
    /// Gets creator Id.
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets creator email.
    /// </summary>
    string? UserEmail { get;  }

    /// <summary>
    /// Gets modification date.
    /// </summary>
    DateTime ModificationDate { get; init; }
}

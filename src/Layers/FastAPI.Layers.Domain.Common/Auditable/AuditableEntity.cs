namespace FastAPI.Layers.Domain.Entities;

using Abstractions;

using ValueObjects;

/// <summary>
/// Base entity that holds basic auditing details.
/// </summary>
/// <typeparam name="TKey">Type of base class id.</typeparam>
public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
{
    /// <summary>
    /// Initializes initial base audit-able entity data.
    /// </summary>
    /// <param name="creatorDetails">Creator user details.</param>
    protected AuditableEntity(ModificationDetails creatorDetails)
    {
        this.SetCreated(creatorDetails);
    }

    /// <summary>
    /// Initializes existing base audit-able entity data.
    /// </summary>
    /// <param name="creatorDetails">Creator user details.</param>
    /// <param name="lastModifierDetails">Last modifier details.</param>
    protected AuditableEntity(
        ModificationDetails creatorDetails,
        ModificationDetails lastModifierDetails)
    {
        this.SetCreated(creatorDetails);
        this.SetModified(lastModifierDetails);
    }

    /// <summary>
    /// Gets main user details on creation of the entity.
    /// </summary>
    public ModificationDetails CreationDetails { get; private set; } = default!;

    /// <summary>
    /// Gets main user details on the last modification of the entity.
    /// </summary>
    public ModificationDetails? LastModificationDetails { get; private set; }

    /// <summary>
    /// Sets created details.
    /// </summary>
    /// <param name="userId">Creator user Id.</param>
    /// <param name="userEmail">Creator user Email.</param>
    /// <returns>Current instance.</returns>
    protected IAuditableEntity SetCreated(string userId, string userEmail)
        => this.SetCreated(new ModificationDetails(userId, userEmail));

    /// <summary>
    /// Sets created details.
    /// </summary>
    /// <param name="creatorDetails">Creator user details.</param>
    /// <returns>Current instance.</returns>
    protected IAuditableEntity SetCreated(ModificationDetails creatorDetails)
    {
        this.CreationDetails = creatorDetails;
        return this;
    }

    /// <summary>
    /// Sets last modification details.
    /// </summary>
    /// <param name="userId">Modifier user Id.</param>
    /// <param name="userEmail">Modifier user Email.</param>
    /// <returns>Current instance.</returns>
    protected IAuditableEntity SetModified(string userId, string userEmail)
        => this.SetModified(new ModificationDetails(userId, userEmail));

    /// <summary>
    /// Sets modified details.
    /// </summary>
    /// <param name="modifierDetails">Modifier details.</param>
    /// <returns>Current instance.</returns>
    protected IAuditableEntity SetModified(ModificationDetails modifierDetails)
    {
        this.LastModificationDetails = modifierDetails;
        return this;
    }
}

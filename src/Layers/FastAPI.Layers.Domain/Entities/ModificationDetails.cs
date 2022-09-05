namespace FastAPI.Layers.Domain.Entities.ValueObjects;

using Abstractions;

using FastAPI.Libraries.Validation;
using FastAPI.Libraries.Validation.Exceptions;

/// <summary>
/// Value for modifier user details.
/// </summary>
public sealed record ModificationDetails : ValueObject, IModifierDetails
{
    /// <summary>
    /// Initializes new modifier details instance.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <param name="userEmail">User Email.</param>
    public ModificationDetails(string userId, string userEmail)
        : this(userId, userEmail, FastAPIDateTime.UtcNow)
    {
    }

    /// <summary>
    /// Initializes new modifier details instance.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <param name="userEmail">User Email.</param>
    /// <param name="modificationDate">Date of modification.</param>
    public ModificationDetails(string userId, string userEmail, DateTimeOffset modificationDate)
    {
        Validate(userId, userEmail);

        this.UserId = userId;
        this.UserEmail = userEmail;
        this.ModificationDate = modificationDate;
    }

    /// <summary>
    /// Gets modifier user Id.
    /// </summary>
    public string UserId { get; init; }

    /// <summary>
    /// Gets modifier email.
    /// </summary>
    public string UserEmail { get; init; }

    /// <summary>
    /// Gets modification date.
    /// </summary>
    public DateTimeOffset ModificationDate { get; init; }

    private static void Validate(string userId, string userEmail)
    {
        Ensure.NotEmpty<ValidationException>(userId, nameof(userId));
        Ensure.NotEmpty<ValidationException>(userEmail, nameof(userEmail));
    }
}

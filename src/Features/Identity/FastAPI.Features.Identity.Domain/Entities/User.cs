namespace FastAPI.Features.Identity.Domain.Entities;

using System.Collections.Generic;
using Exceptions;

using FastAPI.Features.Identity.Domain.Resources;
using FastAPI.Layers.Domain.Common.Phone;
using FastAPI.Layers.Domain.Common.User;
using FastAPI.Layers.Domain.Entities.Abstractions;
using FastAPI.Layers.Domain.Events.Abstractions;
using FastAPI.Libraries.System.Extensions;
using FastAPI.Libraries.Validation;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// User domain entity.
/// </summary>
public sealed class User : IdentityUser, IUser, IAggregateRoot, IEntity<string>
{
    private ICollection<IDomainEvent> events;

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="email">Email value.</param>
    /// <param name="firstName">First name value.</param>
    /// <param name="lastName">Last name value.</param>
    internal User(string email, string firstName, string lastName)
        : base(email)
    {
        Ensure.NotNull<string, InvalidUserException>(email, nameof(email));
        this.SetFirstName(firstName);
        this.SetLastName(lastName);
        this.SetEmail(email);

        this.Gender = default!;
        this.events = new List<IDomainEvent>();
    }

    /// <summary>
    /// Gets or sets user email.
    /// </summary>
    public new string Email
    {
        get => base.Email!;
        set => base.Email = value;
    }

    /// <summary>
    /// Gets user first name.
    /// </summary>
    public string FirstName { get; private set; } = default!;

    /// <summary>
    /// Gets user last name.
    /// </summary>
    public string LastName { get; private set; } = default!;

    /// <summary>
    /// Gets user gender.
    /// </summary>
    public Gender? Gender { get; private set; }

    /// <summary>
    /// Gets user date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; private set; }

    /// <summary>
    /// Gets user age.
    /// </summary>
    public int Age
    {
        get
        {
            if (this.DateOfBirth is not null)
            {
                return this.DateOfBirth.Age();
            }

            return default;
        }
    }

    /// <summary>
    /// Gets user refresh token.
    /// </summary>
    public IdentityToken? RefreshToken { get; private set; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> Events => this.events.ToArray();

    /// <inheritdoc/>
    public void AddEvent(IDomainEvent domainEvent)
        => this.events.Add(domainEvent);

    /// <inheritdoc/>
    public void ClearEvents()
        => this.events = new List<IDomainEvent>();

    /// <summary>
    /// Set user email.
    /// </summary>
    /// <param name="email">Email value.</param>
    /// <returns>Updated instance.</returns>
    public User SetEmail(string email)
    {
        ValidateEmail(email);
        this.Email = email;
        return this;
    }

    /// <summary>
    /// Set user first name.
    /// </summary>
    /// <param name="firstName">First name value.</param>
    /// <returns>Updated instance.</returns>
    public User SetFirstName(string firstName)
    {
        ValidateName(firstName);
        this.FirstName = firstName;
        return this;
    }

    /// <summary>
    /// Set user last name.
    /// </summary>
    /// <param name="lastName">Last name value.</param>
    /// <returns>Updated instance.</returns>
    public User SetLastName(string lastName)
    {
        ValidateName(lastName);
        this.LastName = lastName;
        return this;
    }

    /// <summary>
    /// Set user phone number.
    /// </summary>
    /// <param name="phoneNumber">Phone number value.</param>
    /// <returns>Updated instance.</returns>
    public User SetPhoneNumber(string? phoneNumber)
    {
        if (phoneNumber is null)
        {
            this.PhoneNumber = null;
            return this;
        }

        this.PhoneNumber = new PhoneNumber(phoneNumber);
        return this;
    }

    /// <summary>
    /// Set user gender.
    /// </summary>
    /// <param name="gender">Gender value.</param>
    /// <returns>Updated instance.</returns>
    public User SetGender(Gender? gender)
    {
        this.Gender = gender;
        return this;
    }

    /// <summary>
    /// Set user date of birth.
    /// </summary>
    /// <param name="date">Date of birth value.</param>
    /// <returns>Updated instance.</returns>
    public User SetDateOfBirth(DateOnly? date)
        => this.SetDateOfBirth(date?.ToDateTime(TimeOnly.MinValue));

    /// <summary>
    /// Set user date of birth.
    /// </summary>
    /// <param name="date">Date of birth value.</param>
    /// <returns>Updated instance.</returns>
    public User SetDateOfBirth(DateTime? date)
    {
        if (date is not null)
        {
            Ensure.That<DateTime?, InvalidUserException>(
                date,
                value => value < DateTime.Now,
                ValidationMessages.BirthdayInFutureErrorMessage);

            Ensure.LessThan<DateTime, InvalidUserException>(
                date!.Value,
                DateTime.Now,
                nameof(date));
        }

        this.DateOfBirth = date;
        return this;
    }

    /// <summary>
    /// Set new refresh token.
    /// </summary>
    /// <param name="token">New Refresh token instance.</param>
    /// <returns>Updated instance.</returns>
    public User SetNewRefreshToken(IdentityToken token)
    {
        ValidateToken(token);

        this.RefreshToken = token;
        return this;
    }

    /// <summary>
    /// Revoke current refresh token.
    /// </summary>
    /// <returns>Updated instance.</returns>
    public User RevokeRefreshToken()
    {
        this.RefreshToken = null;
        return this;
    }

    private static void ValidateName(string name)
    {
        Ensure.NotEmpty<InvalidUserException>(name, nameof(name));
        Ensure.HasMaxLength<InvalidUserException>(name, ValidationConstants.Human.MaxNameLength, nameof(name));
    }

    private static void ValidateEmail(string email)
    {
        Ensure.IsValidEmail<InvalidUserException>(email);
    }

    private static void ValidateToken(IdentityToken token)
    {
        Ensure.GreaterThan<DateTime, InvalidUserException>(
            token.ExpirationTime.LocalDateTime,
            DateTime.Now,
            nameof(token.ExpirationTime));
    }
}

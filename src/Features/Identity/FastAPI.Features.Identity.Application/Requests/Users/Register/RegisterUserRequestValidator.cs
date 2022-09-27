namespace FastAPI.Features.Identity.Application.Requests.Users.Register;

using FluentValidation;

using Microsoft.Extensions.Options;

using Resources;

using static FastAPI.Libraries.Validation.ValidationConstants;

public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
	public RegisterUserRequestValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        this.RuleFor(u => u.Email)
            .Matches(Email.Format)
            .MaximumLength(Email.MaxLength)
            .EmailAddress()
            .NotEmpty();

        this.RuleFor(u => u.Password)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();

        this.RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password)
            .WithMessage(UserValidationMessages.ConfirmPasswordNotMatching)
            .NotEmpty();

        this.RuleFor(u => u.FirstName)
            .MaximumLength(Human.MaxNameLength)
            .NotEmpty();

        this.RuleFor(u => u.LastName)
            .MaximumLength(Human.MaxNameLength)
            .NotEmpty();
    }
}

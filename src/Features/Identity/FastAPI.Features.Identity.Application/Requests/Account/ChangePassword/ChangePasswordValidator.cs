namespace FastAPI.Features.Identity.Application.Requests.Account.ChangePassword;

using FluentValidation;

using Microsoft.Extensions.Options;

public sealed class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
	public ChangePasswordRequestValidator(IOptions<IdentitySettings> options)
	{
        var settings = options.Value;

        this.RuleFor(m => m.UserId)
            .NotEmpty();

        this.RuleFor(m => m.OldPassword)
            .NotEmpty();

        this.RuleFor(m => m.NewPassword)
            .NotEmpty()
            .MinimumLength(settings.MinPasswordLength)
            .NotEqual(c => c.OldPassword);

        this.RuleFor(m => m.ConfirmNewPassword)
            .NotEmpty()
            .Equal(c => c.NewPassword);
    }
}

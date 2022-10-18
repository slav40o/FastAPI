namespace FastAPI.Features.Identity.Application.Requests.Account.ConfirmEmail;

using FluentValidation;

public sealed class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        this.RuleFor(m => m.UserId)
            .NotEmpty();

        this.RuleFor(m => m.Token)
            .NotEmpty();
    }
}

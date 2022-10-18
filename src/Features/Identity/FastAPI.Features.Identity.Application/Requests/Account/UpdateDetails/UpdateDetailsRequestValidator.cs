namespace FastAPI.Features.Identity.Application.Requests.Account.UpdateDetails;

using FastAPI.Libraries.Validation;

using FluentValidation;

public sealed class UpdateDetailsRequestValidator : AbstractValidator<UpdateDetailsRequest>
{
	public UpdateDetailsRequestValidator()
	{
        this.RuleFor(m => m.Id)
            .NotEmpty();

        this.RuleFor(m => m.FirstName)
            .NotEmpty()
            .MaximumLength(ValidationConstants.Human.MaxNameLength);

        this.RuleFor(m => m.LastName)
            .NotEmpty()
            .MaximumLength(ValidationConstants.Human.MaxNameLength);

        this.RuleFor(m => m.PhoneNumber)
            .Matches(ValidationConstants.Phone.Format)
            .MinimumLength(ValidationConstants.Phone.MinLength)
            .MaximumLength(ValidationConstants.Phone.MaxLength);
    }
}

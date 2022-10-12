namespace FastAPI.Features.Identity.Application.Services;

using FastAPI.Features.Identity.Application.Requests.Users.ConfirmEmail;
using FastAPI.Features.Identity.Application.Resources;
using FastAPI.Features.Identity.Application.Resources.Emails.UserRegistered;
using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Application.Email.Models;
using FastAPI.Layers.Application.Services;

/// <inheritdoc />
internal sealed class IdentityEmailService : IIdentityEmailService
{
    private readonly IEmailService emailService;
    private readonly IUrlProvider urlProvider;

    public IdentityEmailService(IEmailService emailService, IUrlProvider urlProvider)
    {
        this.emailService = emailService;
        this.urlProvider = urlProvider;
    }

    /// <inheritdoc />
    public Task SendUserEmailConfirmationEmail(User user, string token, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task SendUserPasswordResetEmail(User user, string token, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task SendUserRegisteredEmail(User user, string token, CancellationToken cancellationToken = default)
    {
        var confirmUrl = this.urlProvider.GetRequestUrl<ConfirmEmailRequest>();
        var model = new UserRegisteredEmailModel
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            ConfirmationToken = token,
            ConfirmationUrl = confirmUrl,
        };

        var emailModel = new EmailModel<UserRegisteredEmailModel>(user.Email, EmailMessages.AccountCreated, model);
        await this.emailService.SendAsync(emailModel, cancellationToken);
    }
}

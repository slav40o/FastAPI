namespace FastAPI.Features.Identity.Infrastructure.Services;

using FastAPI.Features.Identity.Application.Services;
using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Application.Email;

/// <inheritdoc />
internal sealed class DummyEmailService : IIdentityEmailService
{
    public DummyEmailService()
    {
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
    public Task SendUserRegisteredEmail(User user, string token, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}

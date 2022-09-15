namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email;

public sealed class MockEmailSender : IEmailSender
{
    public Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.FromResult(true);
    }
}
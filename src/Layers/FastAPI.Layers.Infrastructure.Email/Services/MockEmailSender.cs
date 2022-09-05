namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email;

public class MockEmailSender : IEmailSender
{
    public Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.FromResult(true);
    }
}
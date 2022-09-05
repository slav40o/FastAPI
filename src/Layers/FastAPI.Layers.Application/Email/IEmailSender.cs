namespace FastAPI.Layers.Application.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}
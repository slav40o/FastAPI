namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Infrastructure.Email.Exceptions;
using FastAPI.Layers.Infrastructure.Email.Settings;
using FastAPI.Libraries.Validation;

using SendGrid;
using SendGrid.Helpers.Mail;

using Microsoft.Extensions.Options;

using static FastAPI.Layers.Infrastructure.Email.EmailConstants;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridEmailSettings settings;
    private readonly EmailAddress senderAddress;

    public SendGridEmailSender(IOptions<SendGridEmailSettings> opts)
    {
        settings = opts.Value;
        ValidateSettings(settings);

        senderAddress = new EmailAddress(settings.SenderAddress, settings.SenderName);
    }
    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        ValidateEmail(email, subject);

        var client = new SendGridClient(settings.SenderApiKey);
        var to = new EmailAddress(email);

        var msg = MailHelper.CreateSingleEmail(senderAddress, to, subject, htmlMessage.StripHtmlTags(), htmlMessage);
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);
        Ensure.NotNull<Response, EmailSendingException>(response, nameof(response), message: ErrorMessages.NoReponse);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Body.ReadAsStringAsync();
            throw new EmailSendingException(responseBody);
        }

        return response.IsSuccessStatusCode;
    }

    private static void ValidateEmail(string email, string subject)
    {
        Ensure.IsValidEmail<InvalidEmailException>(email);
        Ensure.NotEmpty<InvalidEmailException>(subject, nameof(subject), ErrorMessages.SubjectRequired);
    }

    private static void ValidateSettings(SendGridEmailSettings settings)
    {
        Ensure.NotEmpty<InvalidEmailException>(
            settings.SenderApiKey, nameof(settings.SenderApiKey), ErrorMessages.ApiKeyMissing);
        Ensure.NotEmpty<InvalidEmailException>(
            settings.SenderAddress, nameof(settings.SenderAddress), ErrorMessages.SenderEmailMissing);
        Ensure.NotEmpty<InvalidEmailException>(
            settings.SenderName, nameof(settings.SenderName), ErrorMessages.SenderNameMissing);
    }
}
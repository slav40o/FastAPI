namespace FastAPI.Layers.Infrastructure.Email.Services;

using System.Threading.Tasks;

using FastAPI.Layers.Application.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Abstractions;
using FastAPI.Layers.Infrastructure.Email.Settings;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;


/// <summary>
/// SendGrid email sender.
/// </summary>
internal sealed class SendGridEmailSender : IEmailSender
{
    private readonly EmailSettings emailSettings;
    private readonly EmailAddress fromAddress;
    private readonly ISendGridClient client;
    private readonly ILogger<SendGridEmailSender> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendGridEmailSender"/> class.
    /// </summary>
    /// <param name="client">SendGrid API client.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="emailSettingsOptions">Email settings.</param>
    public SendGridEmailSender(
        ISendGridClient client,
        ILogger<SendGridEmailSender> logger,
        IOptions<EmailSettings> emailSettingsOptions)
    {
        this.client = client;
        this.logger = logger;
        emailSettings = emailSettingsOptions.Value;
        fromAddress = new EmailAddress(emailSettings.SenderAddress, emailSettings.SenderName);
    }

    /// <inheritdoc/>
    public Task<bool> SendEmailAsync(
        string to,
        string subject,
        string htmlMessage,
        CancellationToken cancellationToken = default)
            => SendEmailAsync(to, subject, htmlMessage, null, cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> SendEmailAsync(
        string to,
        string subject,
        string htmlMessage,
        IEnumerable<IEmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default)
    {
        var toAddress = new EmailAddress(to);
        var msg = MailHelper.CreateSingleEmail(
            fromAddress,
            toAddress,
            subject,
            htmlMessage.StripHtmlTags(),
            htmlMessage);

        await AddAttachmentsAsync(msg, attachments, cancellationToken);

        var result = await client.SendEmailAsync(msg, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            string resultBody = await result.Body.ReadAsStringAsync(cancellationToken);
            logger.LogError("Error sending email to {to}", to);
            logger.LogError("Response body from SendGrid is {body}", resultBody);
        }

        return result.IsSuccessStatusCode;
    }

    /// <inheritdoc/>
    public Task<bool> SendEmailAsync(
        IEnumerable<string> tos,
        string subject,
        string htmlMessage,
        IEnumerable<string>? ccs,
        IEnumerable<string>? bccs,
        CancellationToken cancellationToken = default)
            => SendEmailAsync(
                tos,
                subject,
                htmlMessage,
                ccs,
                bccs,
                null,
                cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> SendEmailAsync(
        IEnumerable<string> tos,
        string subject,
        string htmlMessage,
        IEnumerable<string>? ccs,
        IEnumerable<string>? bccs,
        IEnumerable<IEmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default)
    {
        var msg = new SendGridMessage();

        msg.AddTos(tos.Select(to => new EmailAddress(to)).ToList());
        msg.Subject = subject;
        msg.SetFrom(fromAddress);
        msg.HtmlContent = htmlMessage;
        msg.PlainTextContent = htmlMessage.StripHtmlTags();

        if (ccs!.IsNotEmpty())
        {
            msg.AddCcs(ccs?.Select(cc => new EmailAddress(cc)).ToList());
        }

        if (bccs!.IsNotEmpty())
        {
            msg.AddBccs(bccs?.Select(bcc => new EmailAddress(bcc)).ToList());
        }

        await AddAttachmentsAsync(msg, attachments, cancellationToken);
        var result = await client.SendEmailAsync(msg, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            string resultBody = await result.Body.ReadAsStringAsync(cancellationToken);
            logger.LogError("Error sending emails to {tos}", string.Join(",", tos));
            logger.LogError("Response body from SendGrid is {body}", resultBody);
        }

        return result.IsSuccessStatusCode;
    }

    private static async Task AddAttachmentsAsync(SendGridMessage msg, IEnumerable<IEmailAttachment>? attachments, CancellationToken cancellationToken)
    {
        if (attachments is null)
        {
            return;
        }

        if (attachments is not null && attachments.IsNotEmpty())
        {
            foreach (var attachment in attachments)
            {
                await msg.AddAttachmentAsync(
                    attachment.Name,
                    attachment.GetContentStream(),
                    cancellationToken: cancellationToken);
            }
        }
    }
}

namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Application.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// Central email service responsible for email generation and sending.
/// </summary>
public sealed class EmailService : IEmailService
{
    private readonly ITemplateRenderer templateRenderer;
    private readonly IEmailSender emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="templateRenderer">Templates renderer.</param>
    /// <param name="emailSender">Email sender service.</param>
    public EmailService(ITemplateRenderer templateRenderer, IEmailSender emailSender)
    {
        this.templateRenderer = templateRenderer;
        this.emailSender = emailSender;
    }

    /// <inheritdoc/>
    public Task<bool> SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        => emailSender.SendEmailAsync(to, subject, body, cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> SendAsync<T>(EmailModel<T> model, CancellationToken cancellationToken = default)
    {
        string emailBody = await templateRenderer.RenderAsync<T>(model.Data);
        return await emailSender.SendEmailAsync(
            model.To,
            model.Subject,
            emailBody,
            model.Cc,
            model.Bcc,
            model.Attachments,
            cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> SendAsync<T>(string templateName, EmailModel<T> model, CancellationToken cancellationToken = default)
    {
        string emailBody = await templateRenderer.RenderAsync<T>(templateName, model.Data);
        return await emailSender.SendEmailAsync(
            model.To,
            model.Subject,
            emailBody,
            model.Cc,
            model.Bcc,
            model.Attachments,
            cancellationToken);
    }
}

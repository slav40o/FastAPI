namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Application.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Settings;
using Microsoft.Extensions.Options;

using System.Threading.Tasks;

public sealed class EmailService : IEmailService
{
    private readonly EmailTemplateSettings settings;
    private readonly IEmailSender emailSender;
    private readonly IEmailTemplateRenderer templateRenderer;

    public EmailService(
        IOptions<EmailTemplateSettings> opts,
        IEmailSender emailSender,
        IEmailTemplateRenderer templateRenderer)
    {
        settings = opts.Value;
        this.emailSender = emailSender;
        this.templateRenderer = templateRenderer;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        await emailSender.SendEmailAsync(to, subject, body);
    }

    public Task SendAsync<TData>(SendEmailModel<TData> emailModel)
        where TData : class
    {
        string modelName = typeof(TData).Name;
        string viewName = modelName.Replace(settings.ModelNameSuffix, string.Empty);

        return SendAsync(viewName, emailModel);
    }

    public async Task SendAsync<TData>(string viewName, SendEmailModel<TData> emailModel)
        where TData : class
    {
        string body = await templateRenderer.RenderAsync(viewName, emailModel.Model);
        await emailSender.SendEmailAsync(emailModel.To, emailModel.Subject, body);
    }
}
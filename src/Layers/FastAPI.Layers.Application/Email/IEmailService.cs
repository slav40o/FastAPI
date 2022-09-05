namespace FastAPI.Layers.Application.Email;

using FastAPI.Layers.Application.Email.Models;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);

    Task SendAsync<TData>(SendEmailModel<TData> emailModel)
        where TData : class;

    Task SendAsync<TData>(string templateName, SendEmailModel<TData> emailModel)
        where TData : class;
}
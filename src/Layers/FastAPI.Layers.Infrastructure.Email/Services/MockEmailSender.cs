namespace FastAPI.Layers.Infrastructure.Email.Services;

using FastAPI.Layers.Application.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Abstractions;

using System.Collections.Generic;
using System.Threading;

public sealed class MockEmailSender : IEmailSender
{
    public Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SendEmailAsync(string to, string subject, string htmlMessage, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SendEmailAsync(string to, string subject, string htmlMessage, IEnumerable<IEmailAttachment>? attachments = null, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SendEmailAsync(IEnumerable<string> tos, string subject, string htmlMessage, IEnumerable<string>? ccs, IEnumerable<string>? bccs, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SendEmailAsync(IEnumerable<string> tos, string subject, string htmlMessage, IEnumerable<string>? ccs, IEnumerable<string>? bccs, IEnumerable<IEmailAttachment>? attachments = null, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}
using FastAPI.Layers.Application.Email.Models;

namespace FastAPI.Layers.Infrastructure.Email.Abstractions;


/// <summary>
/// Send generated email with external service provider.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="htmlMessage">Email body in HTML format.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        string to,
        string subject,
        string htmlMessage,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="htmlMessage">Email body in HTML format.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        string to,
        string subject,
        string htmlMessage,
        IEnumerable<IEmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="tos">Email main recipients.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="htmlMessage">Email body in HTML format.</param>
    /// <param name="ccs">Email CC recipients.</param>
    /// <param name="bccs">Email BCC recipients.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        IEnumerable<string> tos,
        string subject,
        string htmlMessage,
        IEnumerable<string>? ccs,
        IEnumerable<string>? bccs,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="tos">Email main recipients.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="htmlMessage">Email body in HTML format.</param>
    /// <param name="ccs">Email CC recipients.</param>
    /// <param name="bccs">Email BCC recipients.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        IEnumerable<string> tos,
        string subject,
        string htmlMessage,
        IEnumerable<string>? ccs,
        IEnumerable<string>? bccs,
        IEnumerable<IEmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default);
}

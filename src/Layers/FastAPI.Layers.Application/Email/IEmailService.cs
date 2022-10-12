namespace FastAPI.Layers.Application.Email;

using Models;

/// <summary>
/// Central email service responsible for generating full
/// email data and sending it to its recipients.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Send simple mail to given recipient.
    /// </summary>
    /// <param name="to">Receiver email.</param>
    /// <param name="subject">Email subject/title.</param>
    /// <param name="body">Email body in plain text or HTMLformat.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completed task indicating whether the mail sending was successful.</returns>
    Task<bool> SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email using the given <see cref="T"/> data.
    /// </summary>
    /// <typeparam name="T">Type of the data used for the email body generation.</typeparam>
    /// <param name="model">Full email data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completed task indicating whether the mail sending was successful.</returns>
    Task<bool> SendAsync<T>(EmailModel<T> model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email using the given <see cref="T"/> data.
    /// </summary>
    /// <typeparam name="T">Type of the data used for the email body generation.</typeparam>
    /// <param name="templateName">Name of the template if it is different form the <see cref="T"/> class name.</param>
    /// <param name="model">Full email data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completed task indicating whether the mail sending was successful.</returns>
    Task<bool> SendAsync<T>(string templateName, EmailModel<T> model, CancellationToken cancellationToken = default);
}
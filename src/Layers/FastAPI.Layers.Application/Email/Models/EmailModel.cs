namespace FastAPI.Layers.Application.Email.Models;

/// <summary>
/// Email model containing necessary data for sending
/// emails with <see cref="IEmailService"/>.
/// </summary>
/// <typeparam name="TData">Type of the data that will be used as information in the email body.</typeparam>
public sealed class EmailModel<TData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TData}"/> class.
    /// </summary>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data used for generating the email body.</param>
    public EmailModel(string to, string subject, TData data)
    {
        To = new List<string> { to };
        Subject = subject;
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TData}"/> class.
    /// </summary>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    public EmailModel(string to, string subject, TData data, params IEmailAttachment[] attachments)
        : this(to, subject, data)
    {
        Attachments = attachments;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TData}"/> class.
    /// </summary>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data used for generating the email body.</param>
    public EmailModel(
        IEnumerable<string> to,
        IEnumerable<string> cc,
        IEnumerable<string> bcc,
        string subject,
        TData data)
    {
        To = to;
        Cc = cc;
        Bcc = bcc;
        Subject = subject;
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TData}"/> class.
    /// </summary>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    public EmailModel(
        IEnumerable<string> to,
        IEnumerable<string> cc,
        IEnumerable<string> bcc,
        string subject,
        TData data,
        params IEmailAttachment[] attachments)
            : this(to, cc, bcc, subject, data)
    {
        Attachments = attachments;
    }

    /// <summary>
    /// Gets main receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string> To { get; init; }

    /// <summary>
    /// Gets CC receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string>? Cc { get; init; }

    /// <summary>
    /// Gets BCC receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string>? Bcc { get; init; }

    /// <summary>
    /// Gets email subject.
    /// </summary>
    public string Subject { get; init; }

    /// <summary>
    /// Gets data used for generating the body of the email.
    /// </summary>
    public TData Data { get; init; }

    /// <summary>
    /// Gets email attachments.
    /// </summary>
    public IEnumerable<IEmailAttachment>? Attachments { get; init; }
}

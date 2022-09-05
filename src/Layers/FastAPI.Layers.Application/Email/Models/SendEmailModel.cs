namespace FastAPI.Layers.Application.Email.Models;

public class SendEmailModel<TData> where TData : class
{
    public SendEmailModel(string to, string subject, TData model)
    {
        this.To = to;
        this.Subject = subject;
        this.Model = model;
    }

    public string To { get; private set; }

    public string Subject { get; private set; }

    public TData Model { get; private set; }
}
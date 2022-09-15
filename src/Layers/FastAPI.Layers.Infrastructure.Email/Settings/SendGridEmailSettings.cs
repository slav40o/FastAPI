namespace FastAPI.Layers.Infrastructure.Email.Settings;

public sealed class SendGridEmailSettings
{
    public string SenderApiKey { get; private set; } = default!;

    public string SenderAddress { get; private set; } = default!;

    public string SenderName { get; private set; } = default!;

    public SendGridEmailSettings SetApiKey(string key)
    {
        SenderApiKey = key;
        return this;
    }

    public SendGridEmailSettings SetSenderAddress(string email)
    {
        SenderAddress = email;
        return this;
    }

    public SendGridEmailSettings SetSenderName(string name)
    {
        SenderName = name;
        return this;
    }
}
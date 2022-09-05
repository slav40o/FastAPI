namespace FastAPI.Layers.Infrastructure.Email.Models;

using FastAPI.Libraries.Validation;
using FastAPI.Libraries.Validation.Exceptions;

public class LayoutModel
{
    public string Content { get; private set; } = default!;

    public string ClientUrl { get; private set; } = default!;

    public string TermsUrl { get; private set; } = default!;

    public string PrivacyUrl { get; private set; } = default!;

    public LayoutModel SetContent(string content)
    {
        this.Content = content;
        return this;
    }

    public LayoutModel SetClientUrl(string clientUrl)
    {
        Ensure.IsValidUrl<ValidationException>(clientUrl, nameof(clientUrl));
        this.ClientUrl = clientUrl;
        this.PrivacyUrl = $"{clientUrl}/privacy-policy";
        this.TermsUrl = $"{clientUrl}/terms-of-use";
        return this;
    }
}
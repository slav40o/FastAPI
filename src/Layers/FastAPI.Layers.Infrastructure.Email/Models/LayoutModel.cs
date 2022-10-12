namespace FastAPI.Layers.Infrastructure.Email.Models;

using FastAPI.Layers.Infrastructure.Email.Abstractions;
using FastAPI.Layers.Infrastructure.Email.Exceptions;
using FastAPI.Libraries.Validation;

/// <summary>
/// Layout model for the Layout email view.
/// </summary>
public sealed class LayoutModel : IEmailLayoutModel
{
    /// <inheritdoc/>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets client URL.
    /// </summary>
    public string ClientUrl { get; private set; } = default!;

    /// <summary>
    /// Gets terms of use URL.
    /// </summary>
    public string TermsUrl { get; private set; } = default!;

    /// <summary>
    /// Gets privacy policy URL.
    /// </summary>
    public string PrivacyUrl { get; private set; } = default!;

    /// <summary>
    /// Set client URL that will be filled in the layout page.
    /// </summary>
    /// <param name="clientUrl">Client URL.</param>
    /// <returns>Current instance.</returns>
    public LayoutModel SetClientUrl(string clientUrl)
    {
        Ensure.IsValidUrl<InvalidClientUrlException>(clientUrl, nameof(clientUrl));
        this.ClientUrl = clientUrl;
        this.PrivacyUrl = $"{clientUrl}/privacy";
        this.TermsUrl = $"{clientUrl}/terms";
        return this;
    }
}
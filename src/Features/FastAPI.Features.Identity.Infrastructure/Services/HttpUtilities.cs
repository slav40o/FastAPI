namespace FastAPI.Features.Identity.Infrastructure.Services;

using FastAPI.Features.Identity.Application.Services;

using System.Web;

/// <inheritdoc />
public class HttpUtilities : IHttpUtilities
{
    /// <inheritdoc />
    public string? UrlDecode(string? url)
        => HttpUtility.UrlDecode(url);

    /// <inheritdoc />
    public string? UrlEncode(string? url)
        => HttpUtility.UrlEncode(url);
}

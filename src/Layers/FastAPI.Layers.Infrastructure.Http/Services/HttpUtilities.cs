namespace FastAPI.Layers.Infrastructure.Http.Services;

using FastAPI.Layers.Application;
using System.Web;

/// <inheritdoc />
public sealed class HttpUtilities : IHttpUtilities
{
    /// <inheritdoc />
    public string? UrlDecode(string? url)
        => HttpUtility.UrlDecode(url);

    /// <inheritdoc />
    public string? UrlEncode(string? url)
        => HttpUtility.UrlEncode(url);
}

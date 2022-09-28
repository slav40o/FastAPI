namespace FastAPI.Layers.Application.Settings;

public sealed class AppSettings
{
    public string ApiVersion { get; init; } = default!;

    public string ApiName { get; init; } = default!;

    public string ApiUrl { get; init; } = default!;

    public string ClientUrl { get; init; } = default!;

    public string AdminRoleName { get; init; } = default!;

    public string[] Roles { get; init; } = default!;
}

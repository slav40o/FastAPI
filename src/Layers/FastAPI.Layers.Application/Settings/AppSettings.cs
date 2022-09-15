namespace FastAPI.Layers.Application.Settings;

public sealed class AppSettings
{
    public string ApiUrl { get; private set; } = default!;

    public string ClientUrl { get; private set; } = default!;

    public string AdminRoleName { get; private set; } = default!;

    public string[] RequiredRoles { get; private set; } = default!;
}

namespace FastAPI.Layers.Application.Settings;

public class AppSettings
{
    public string ApiUrl { get; private set; } = default!;

    public string AdminRoleName { get; private set; } = default!;

    public string[] RequiredRoles { get; private set; } = default!;
}

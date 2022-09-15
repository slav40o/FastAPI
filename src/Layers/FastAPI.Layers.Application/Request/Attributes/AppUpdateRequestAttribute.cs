namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppUpdateRequestAttribute : AppRequestAttribute
{
    public AppUpdateRequestAttribute()
        : base(AppRequestTypes.Update)
    {
    }
}

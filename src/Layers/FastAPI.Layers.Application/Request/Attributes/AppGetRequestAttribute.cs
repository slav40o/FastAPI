namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppGetRequestAttribute : AppRequestAttribute
{
    public AppGetRequestAttribute()
        : base(AppRequestTypes.Get)
    {
    }
}
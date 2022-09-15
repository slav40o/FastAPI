namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppQueryRequestAttribute : AppRequestAttribute
{
    public AppQueryRequestAttribute()
        : base(AppRequestTypes.Get)
    {
    }
}
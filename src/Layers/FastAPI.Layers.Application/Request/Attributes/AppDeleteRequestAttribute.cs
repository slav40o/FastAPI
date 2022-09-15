namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppDeleteRequestAttribute : AppRequestAttribute
{
    public AppDeleteRequestAttribute()
        : base(AppRequestTypes.Delete)
    {
    }
}

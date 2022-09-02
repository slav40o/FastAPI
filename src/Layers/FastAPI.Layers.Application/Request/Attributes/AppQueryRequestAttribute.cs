namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AppQueryRequestAttribute : AppRequestAttribute
{
    public AppQueryRequestAttribute()
        : base(AppRequestTypes.Get)
    {
    }
}
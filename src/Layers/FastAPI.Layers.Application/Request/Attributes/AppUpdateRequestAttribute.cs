namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AppUpdateRequestAttribute : AppRequestAttribute
{
    public AppUpdateRequestAttribute()
        : base(AppRequestTypes.Update)
    {
    }
}

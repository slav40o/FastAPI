namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AppDeleteRequestAttribute : AppRequestAttribute
{
    public AppDeleteRequestAttribute()
        : base(AppRequestTypes.Delete)
    {
    }
}

namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AppCreateRequestAttribute : AppRequestAttribute
{
	public AppCreateRequestAttribute()
		: base(AppRequestTypes.Create)
	{
	}
}

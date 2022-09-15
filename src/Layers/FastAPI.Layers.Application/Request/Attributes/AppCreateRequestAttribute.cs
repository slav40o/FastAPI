namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppCreateRequestAttribute : AppRequestAttribute
{
	public AppCreateRequestAttribute()
		: base(AppRequestTypes.Create)
	{
	}
}

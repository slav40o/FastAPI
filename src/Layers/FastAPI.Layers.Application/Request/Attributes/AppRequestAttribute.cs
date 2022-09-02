namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class AppRequestAttribute : Attribute
{
	public AppRequestAttribute(AppRequestTypes type)
	{
		this.Type = type;
	}

	public AppRequestTypes Type { get; init; }
}

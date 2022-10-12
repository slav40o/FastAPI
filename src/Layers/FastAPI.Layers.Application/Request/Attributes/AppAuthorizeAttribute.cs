namespace FastAPI.Layers.Application.Request.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class AppAuthorizeAttribute : Attribute
{
	public AppAuthorizeAttribute()
	{
	}

	public AppAuthorizeAttribute(params string[] policies)
	{
		this.PolicyNames = policies;
	}

	public string[]? PolicyNames { get; init; }
}

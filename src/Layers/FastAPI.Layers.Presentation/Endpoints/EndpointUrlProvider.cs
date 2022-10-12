namespace FastAPI.Layers.Infrastructure.Http.Services;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Services;
using FastAPI.Layers.Application.Settings;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

public sealed class EndpointUrlProvider : IUrlProvider
{
	private readonly LinkGenerator linkGenerator;
	private readonly AppSettings settings;

	public EndpointUrlProvider(
		LinkGenerator linker,
        IOptions<AppSettings> opts)
	{
		this.linkGenerator = linker;
		this.settings = opts.Value;
	}

	public string ApiUrl => this.settings.ApiUrl;

	public string ClientUrl => this.settings.ClientUrl;

    public string GetRequestUrl<TRequest>() where TRequest : AppRequest
		=> $"{this.ApiUrl}{this.linkGenerator.GetPathByName(typeof(TRequest).Name, values: new { })}";

    public string GetRequestUrl<TRequest>(object args) where TRequest : AppRequest
        => $"{this.ApiUrl}{this.linkGenerator.GetPathByName(typeof(TRequest).Name, values: args)}";

    public string GetRequestUrl<TRequest, TResponseModel>() where TRequest : AppRequest<TResponseModel>
		=> $"{this.ApiUrl}{this.linkGenerator.GetPathByName(typeof(TRequest).Name, values: new { })}";

    public string GetRequestUrl<TRequest, TResponseModel>(object args) where TRequest : AppRequest<TResponseModel>
        => $"{this.ApiUrl}{this.linkGenerator.GetPathByName(typeof(TRequest).Name, values: args)}";
}

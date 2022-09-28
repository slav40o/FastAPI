namespace FastAPI.Features.Identity.Presentation;

using FastAPI.Layers.Presentation;
using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class IdentityPresentationConfigurations
{
    public static IServiceCollection AddIdentityPresentationLayer(this IServiceCollection services)
        => services.AddPresentationLayer(typeof(IdentityPresentationConfigurations).Assembly);

    public static WebApplication UseApiEndpoints(
        this WebApplication app,
        IServiceProvider serviceProvider)
    {
        var endpoints = serviceProvider.GetServices(typeof(IEndpointRegister));
        foreach (var endpoint in endpoints)
        {
            (endpoint as IEndpointRegister)?.AddEndpoints(app);
        }

        return app;
    }
}

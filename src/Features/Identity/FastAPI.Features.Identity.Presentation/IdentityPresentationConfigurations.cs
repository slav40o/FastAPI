namespace FastAPI.Features.Identity.Presentation;

using FastAPI.Layers.Presentation;

using Microsoft.Extensions.DependencyInjection;

public static class IdentityPresentationConfigurations
{
    public static IServiceCollection AddIdentityPresentationLayer(this IServiceCollection services)
        => services.AddPresentationLayer(typeof(IdentityPresentationConfigurations).Assembly);
}

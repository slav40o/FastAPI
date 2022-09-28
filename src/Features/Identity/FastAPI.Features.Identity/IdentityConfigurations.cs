namespace FastAPI.Features.Identity;

using FastAPI.Features.Identity.Application;
using FastAPI.Features.Identity.Domain;
using FastAPI.Features.Identity.Infrastructure;
using FastAPI.Features.Identity.Presentation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class IdentityConfigurations
{
    public static IServiceCollection AddIdentityFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddIdentityDomainLayer()
            .AddIdentityApplicationLayer()
            .AddIdentityInfrastructureLayer(configuration)
            .AddIdentityPresentationLayer();

        return services;
    }
}

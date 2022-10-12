namespace FastAPI.Features.Identity;

using FastAPI.Features.Identity.Application;
using FastAPI.Features.Identity.Domain;
using FastAPI.Features.Identity.Infrastructure;
using FastAPI.Features.Identity.Presentation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class IdentityConfigurations
{
    public static IServiceCollection AddIdentityFeature(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)))
                .AddLayers(configuration);

    public static Assembly[] GetEmailAssemblies()
        => new Assembly[1] { typeof(IdentitySettings).Assembly };

    public static IServiceCollection AddIdentityFeature(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IdentitySettings> identitySettingsAction)
            => services
                .Configure(identitySettingsAction)
                .AddLayers(configuration);

    private static IServiceCollection AddLayers(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .AddIdentityDomainLayer()
                .AddIdentityApplicationLayer()
                .AddIdentityInfrastructureLayer(configuration)
                .AddIdentityPresentationLayer();
}

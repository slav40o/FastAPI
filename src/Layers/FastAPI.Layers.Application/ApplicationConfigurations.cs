namespace FastAPI.Layers.Application;

using FastAPI.Layers.Application.Behaviors;
using FastAPI.Layers.Application.Settings;
using FastAPI.Libraries.Mapping;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ApplicationConfigurations
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        Assembly contextAssembly)
    {
        bool hasValidationBehaviour = services.Any(x => x.ImplementationType == typeof(RequestValidationBehavior<,>));
        if (!hasValidationBehaviour)
        {
            services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                // .AddApplicationSettings();
        }

        services
            .AddMappingProfiles(contextAssembly)
            .AddValidatorsFromAssembly(contextAssembly)
            .AddMediatR(config => config.AsScoped(), contextAssembly);

        return services;
    }

    private static IServiceCollection AddApplicationSettings(this IServiceCollection services)
        => services.ConfigureOptions<AppSettings>();
}

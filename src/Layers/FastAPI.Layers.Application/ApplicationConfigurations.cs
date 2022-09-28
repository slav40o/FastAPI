namespace FastAPI.Layers.Application;

using FastAPI.Layers.Application.Behaviors;
using FastAPI.Layers.Application.Settings;
using FastAPI.Libraries.Mapping;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ApplicationConfigurations
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        Assembly applicationAssembly)
    {
        bool hasValidationBehaviour = services.Any(x => x.ImplementationType == typeof(RequestValidationBehavior<,>));
        if (!hasValidationBehaviour)
        {
            services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        services
            .AddMappingProfiles(applicationAssembly)
            .AddValidatorsFromAssembly(applicationAssembly)
            .AddMediatR(config => config.AsScoped(), applicationAssembly);

        return services;
    }
}

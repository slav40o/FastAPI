namespace FastAPI.Layers.Application;

using FastAPI.Layers.Application.Behaviors;
using FastAPI.Libraries.Mapping;

using FluentValidation;

using Mediator;

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
            .AddMediator();

        return services;
    }
}

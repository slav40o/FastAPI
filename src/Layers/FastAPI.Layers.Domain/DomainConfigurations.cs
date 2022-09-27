namespace FastAPI.Layers.Domain;

using FastAPI.Layers.Domain.Builders;
using FastAPI.Layers.Domain.Events;
using FastAPI.Layers.Domain.Events.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;

public static class DomainConfigurations
{
    /// <summary>
    /// Register domain layer services.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="contextAssembly">Context assembly.</param>
    /// <returns>Application services with registered domain layer.</returns>
    public static IServiceCollection AddDomainLayer(
        this IServiceCollection services,
        Assembly contextAssembly)
    {
        services.Scan(scan => scan
            .FromAssemblies(contextAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IBuilder<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.TryAddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}

namespace FastAPI.Layers.Domain.Common;

using FastAPI.Layers.Domain.Builders;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class DomainCommonConfiguration
{
    /// <summary>
    /// Register domain layer services.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="contextAssembly">Context assembly.</param>
    /// <returns>Application services with registered domain layer.</returns>
    public static IServiceCollection AddCommonDomainLayer(
        this IServiceCollection services,
        Assembly contextAssembly)
    {
        services.Scan(scan => scan
            .FromAssemblies(contextAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IAuditableEntityBuilder<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}

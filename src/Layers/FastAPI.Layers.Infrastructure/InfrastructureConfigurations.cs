namespace FastAPI.Layers.Infrastructure;

using FastAPI.Layers.Application.Services;
using FastAPI.Layers.Infrastructure.Exceptions;
using FastAPI.Layers.Infrastructure.Services;
using FastAPI.Layers.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;

/// <summary>
/// Infrastructure layer configurations class.
/// </summary>
public static class InfrastructureConfigurations
{
    private const string DefaultConnectionName = "DefaultConnection";

    /// <summary>
    /// Add base infrastructure layer configurations with setup persistence layer.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="configuration">Application configurations.</param>
    /// <param name="contextAssembly">Context assembly.</param>
    /// <param name="connectionStringName">connection string name.</param>
    /// <typeparam name="TDbContext">Type of the DB context.</typeparam>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddInfrastructureLayer<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly contextAssembly,
        string connectionStringName = DefaultConnectionName)
            where TDbContext : DbContext
    {
        string? connectionStirng = configuration.GetConnectionString(connectionStringName);
        if (connectionStirng is null)
        {
            throw new ConfigurationException("Database connection string is not set!");
        }

        services
            .AddPersistenceLayer<TDbContext>(configuration, contextAssembly, connectionStringName)
            .TryAddScoped<ICurrentUser, CurrentUser>();

        return services;
    }

    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.TryAddScoped<ICurrentUser, CurrentUser>();
        return services;
    }
}

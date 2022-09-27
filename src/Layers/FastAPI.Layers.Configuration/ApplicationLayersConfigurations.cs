namespace FastAPI.Layers.Configuration;

using FastAPI.Layers.Application;
using FastAPI.Layers.Domain;
using FastAPI.Layers.Infrastructure.Email;
using FastAPI.Layers.Infrastructure.Http;
using FastAPI.Layers.Infrastructure.Messaging;
using FastAPI.Layers.Persistence.SQL;
using FastAPI.Layers.Presentation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ApplicationLayersConfigurations
{
    private const string DefaultConnectionName = "DefaultConnection";

    public static IServiceCollection AddDefaultApplicationLayers<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly serviceAssembly)
        where TDbContext : DbContext
            => services
                .AddDomainLayer(serviceAssembly)
                .AddApplicationLayer(serviceAssembly)
                .AddInfrastructureLayer<TDbContext>(configuration, serviceAssembly, serviceAssembly)
                .AddPresentationLayer(serviceAssembly);

    public static IServiceCollection AddDefaultApplicationLayers<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly domainAssembly,
        Assembly applicationAssembly,
        Assembly infrastructureAssembly,
        Assembly presentationAssembly)
        where TDbContext : DbContext
            => services
                .AddDomainLayer(domainAssembly)
                .AddApplicationLayer(applicationAssembly)
                .AddInfrastructureLayer<TDbContext>(configuration, infrastructureAssembly, applicationAssembly)
                .AddPresentationLayer(presentationAssembly);

    /// <summary>
    /// Add base infrastructure layer configurations with setup persistence layer.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="configuration">Application configurations.</param>
    /// <param name="contextAssembly">Context assembly.</param>
    /// <param name="connectionStringName">connection string name.</param>
    /// <typeparam name="TDbContext">Type of the DB context.</typeparam>
    /// <returns>Configured application services.</returns>
    private static IServiceCollection AddInfrastructureLayer<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly contextAssembly,
        Assembly messageConsumersAssembly,
        string connectionStringName = DefaultConnectionName)
            where TDbContext : DbContext
    {
        string? connectionStirng = configuration.GetConnectionString(connectionStringName);
        if (connectionStirng is null)
        {
            throw new ConfigurationException("Database connection string is not set!");
        }

        services
            .AddSqlServerPersistence<TDbContext>(contextAssembly, connectionStringName)
            .AddRabbitMQMessaging(messageConsumersAssembly)
            .AddSendGridEmail(configuration, settings =>
            {
                settings
                    .SetApiKey("")
                    .SetSenderName("")
                    .SetSenderAddress("");
            })
            .AddHttpInfrastructureLayer();

        return services;
    }
}

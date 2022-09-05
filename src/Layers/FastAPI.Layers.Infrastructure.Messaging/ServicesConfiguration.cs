namespace FastAPI.Layers.Infrastructure.Messaging;

using FastAPI.Layers.Application.Messaging;
using FastAPI.Layers.Infrastructure.Messaging.InMemoryMessaging;
using FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;

public static class ServicesConfiguration
{
    public static IServiceCollection AddRabbitMQMessaging(
        this IServiceCollection services,
        Action<RabbitMQConnectionSettings>? settings,
        params Assembly[] consumerAssemblies)
    {
        services.Configure(settings ?? DefaultRabbitMQSettings);
        services.TryAddSingleton<RabbitMQConnection>();
        services.TryAddSingleton<IMessageSubscriber, RabbitMQSubscriber>();
        services.TryAddScoped<IMessagePublisher, RabbitMQPublisher>();
        services.AddConsumers(consumerAssemblies);

        // TODO: Add Hang-fire background service for message polling
        return services;
    }

    public static IServiceCollection AddRabbitMQMessaging(
        this IServiceCollection services,
        params Assembly[] consumerAssemblies)
            => services.AddRabbitMQMessaging(null, consumerAssemblies);

    public static IServiceCollection AddInMemoryMessaging(
        this IServiceCollection services,
        params Assembly[] consumerAssemblies)
    {
        services.TryAddSingleton<InMemoryMessageBroker>();
        services.TryAddSingleton<IMessageSubscriber>(s => s.GetRequiredService<InMemoryMessageBroker>());
        services.TryAddSingleton<IMessagePublisher>(s => s.GetRequiredService<InMemoryMessageBroker>());
        services.AddConsumers(consumerAssemblies);

        // TODO: Add Hang-fire background service for message polling
        return services;
    }

    private static void AddConsumers(this IServiceCollection services, Assembly[] consumerAssemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(consumerAssemblies)
            .AddClasses(c => c.AssignableTo(typeof(IMessageConsumer<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static Action<RabbitMQConnectionSettings> DefaultRabbitMQSettings
        => (s) => { };
}

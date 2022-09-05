namespace FastAPI.Layers.Infrastructure.Messaging;

using FastAPI.Layers.Application.Messaging;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationConfiguration
{
    public static IApplicationBuilder UseMessaging(this IApplicationBuilder appBuilder)
    {
        using var scope = appBuilder.ApplicationServices.CreateScope();
        var subscriber = scope.ServiceProvider.GetService<IMessageSubscriber>();
        if (subscriber is null)
        {
            throw new ApplicationException("Add messaging is not called in your services registration!");
        }

        var consumers = scope.ServiceProvider.GetServices<IMessageConsumer>();
        if (consumers is not null)
        {
            foreach (var consumer in consumers)
            {
                if (consumer is null)
                {
                    continue;
                }

                var type = consumer.GetType();
                subscriber.Subscribe((dynamic)Convert.ChangeType(consumer, type));
            }
        }
        
        return appBuilder;
    }
}

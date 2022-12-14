namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

internal sealed class SubscriberMetaData
{
    public string QueueName { get; init; } = default!;

    public string ExchangeName { get; init; } = default!;

    public string RouteKey { get; init; } = default!;
}

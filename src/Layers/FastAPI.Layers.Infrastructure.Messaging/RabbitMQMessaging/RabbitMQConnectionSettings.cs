namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

public sealed class RabbitMQConnectionSettings
{
    public string ClientProvidedName { get; set; } = "DefaultClientName";

    public string HostName { get; set; } = "rabbitmq";

    public string UserName { get; set; } = "rabbitmq";

    public string Password { get; set; } = "rabbitmq";

    public int Port { get; set; } = 5672;

    /// <summary>
    /// Each publishing to consumer has payload time of 200ms.
    /// If the execution time of the consumer is around 10ms we can
    /// set the buffer count to 20 to keep the consumer busy instead of
    /// waiting another 200ms for the next call from the queue.
    /// https://blog.rabbitmq.com/posts/2012/05/some-queuing-theory-throughput-latency-and-bandwidth
    /// </summary>
    public ushort PrefetchCount { get; set; } = 20;

    public TimeSpan HeartbeatInterval { get; set; } = TimeSpan.FromSeconds(60);

    public bool UseMessagePolling { get; set; } = false;

    public string? MessageDbConnection { get; set; }
}

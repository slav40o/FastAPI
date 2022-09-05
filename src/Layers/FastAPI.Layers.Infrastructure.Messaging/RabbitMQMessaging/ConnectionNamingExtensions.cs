namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging
{
    internal static class ConnectionNamingExtensions
    {
        public static string ToQueueName(this string name)
           => $"{name.Replace(".", string.Empty)}Queue";

        public static string ToExchangeName(this string name)
            => $"{name.Replace(".", string.Empty)}Exchange";

        public static string ToRoutingKeyName(this string name)
            => $"{name}Key";
    }
}

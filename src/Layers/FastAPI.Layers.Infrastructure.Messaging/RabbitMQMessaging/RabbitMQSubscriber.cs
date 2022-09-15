namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

using FastAPI.Layers.Application.Messaging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.IO;
using System.Text.Json;

internal sealed class RabbitMQSubscriber : IMessageSubscriber
{
    private readonly IModel channel;

    public RabbitMQSubscriber(RabbitMQConnection connection)
    {
        this.channel = connection.ConsumeChannel;
    }

    public void Subscribe<TMessage>(IMessageConsumer<TMessage> messageConsumer)
        where TMessage : Message
    {
        string messageName = typeof(TMessage).Name;
        string consumerName = messageConsumer.GetType().FullName!;

        var subscriptionData = BindConsumer(messageName, consumerName, this.channel);

        var consumer = new AsyncEventingBasicConsumer(this.channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            if (body is null)
            {
                throw new ArgumentException("Submitted message is empty!");
            }

            using var stream = new MemoryStream(body);
            if (await JsonSerializer.DeserializeAsync(stream, typeof(TMessage)) is not TMessage message)
            {
                throw new ArgumentException("Provided message type is incorrect!");
            }

            await messageConsumer.Consume(message);
            // TODO: Save message to a DB
            this.channel.BasicAck(ea.DeliveryTag, false);
        };

        this.channel.BasicConsume(
            queue: subscriptionData.QueueName,
            autoAck: false,
            consumerTag: consumerName,
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer);
    }

    /// <summary>
    /// Creates 'exchange => binding => queue' flow.
    /// Each message has it's own exchange.
    /// Each consumer creates new queue and binds it to the message exchange.
    /// </summary>
    private static SubscriberMetaData BindConsumer(string messageName, string consumerName, IModel channel)
    {
        string queueName = consumerName.ToQueueName();
        string exchangeName = messageName.ToExchangeName();
        string routeKey = $"{exchangeName}{queueName}Key";

        channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: QuoteArguments);

        channel.QueueBind(queueName, exchangeName, routeKey);
        return new SubscriberMetaData
        {
            ExchangeName = exchangeName,
            QueueName = queueName,
            RouteKey = routeKey
        };
    }

    private static readonly Dictionary<string, object> QuoteArguments = new ()
    {
        { "x-queue-type", "quorum" }
    };
}

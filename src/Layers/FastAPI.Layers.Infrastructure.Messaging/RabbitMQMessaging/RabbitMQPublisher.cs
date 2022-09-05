namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

using FastAPI.Layers.Application.Messaging;

using RabbitMQ.Client;

using System;
using System.Text.Json;

public class RabbitMQPublisher : IMessagePublisher
{
    private readonly IModel channel;

    public RabbitMQPublisher(RabbitMQConnection connection)
    {
        this.channel = connection.PublishChannel;
    }

    public void Publish<TMessage>(TMessage message)
        where TMessage : Message
    {
        string messageName = typeof(TMessage).Name;
        lock (this.channel)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            this.channel.BasicPublish(
                exchange: messageName.ToExchangeName(),
                routingKey: string.Empty,
                mandatory: true,
                basicProperties: null,
                body: body);
        }
    }

    public void Publish(object message, Type messageType)
    {
        string messageName = messageType.Name;
        lock (this.channel)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            this.channel.BasicPublish(
                exchange: messageName.ToExchangeName(),
                routingKey: string.Empty,
                mandatory: true,
                basicProperties: null,
                body: body);
        }
    }
}

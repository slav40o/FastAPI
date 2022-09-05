namespace FastAPI.Layers.Infrastructure.Messaging.InMemoryMessaging;

using FastAPI.Layers.Application.Messaging;

using System;
using System.Collections.Concurrent;

public class InMemoryMessageBroker : IMessageSubscriber, IMessagePublisher
{
    private readonly IDictionary<string, IList<object>> consumers;

    public InMemoryMessageBroker()
    {
        consumers = new ConcurrentDictionary<string, IList<object>>();
    }
    
    public void Publish<TMessage>(TMessage message)
        where TMessage : Message
    {
        var queueName = typeof(TMessage).Name;
        AddQueue(queueName);

        foreach (var consumers in consumers[queueName])
        {
            var messageConsumer = consumers as IMessageConsumer<TMessage>;
            messageConsumer?.Consume(message);
        }
    }

    public void Publish(object message, Type messageType)
    {
        throw new NotSupportedException("Currently not supported!");
    }

    public void Subscribe<TMessage>(IMessageConsumer<TMessage> messageConsumer)
        where TMessage : Message
    {
        var queueName = typeof(TMessage).Name;
        AddQueue(queueName);

        consumers[queueName].Add(messageConsumer);
    }

    private void AddQueue(string name)
    {
        if (consumers.ContainsKey(name))
        {
            return;
        }

        consumers.Add(name, new List<object>());
    }
}

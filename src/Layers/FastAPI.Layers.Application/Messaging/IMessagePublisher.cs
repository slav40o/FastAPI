namespace FastAPI.Layers.Application.Messaging;

public interface IMessagePublisher
{
    void Publish<TMessage>(TMessage message)
        where TMessage : Message;

    void Publish(object message, Type messageType);
}

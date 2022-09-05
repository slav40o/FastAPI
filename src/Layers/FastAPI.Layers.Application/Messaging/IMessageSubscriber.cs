namespace FastAPI.Layers.Application.Messaging;

public interface IMessageSubscriber
{
    void Subscribe<TMessage>(IMessageConsumer<TMessage> messageConsumer)
        where TMessage : Message;
}

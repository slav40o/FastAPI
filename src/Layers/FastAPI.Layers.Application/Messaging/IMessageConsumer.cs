namespace FastAPI.Layers.Application.Messaging;

public interface IMessageConsumer
{
}

public interface IMessageConsumer<in TMessage> : IMessageConsumer
    where TMessage : Message
{
    Task Consume(TMessage message);
}

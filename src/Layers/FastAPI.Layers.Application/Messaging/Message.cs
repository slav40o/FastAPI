namespace FastAPI.Layers.Application.Messaging;

public abstract class Message
{
    public DateTimeOffset Created { get; set; }
}

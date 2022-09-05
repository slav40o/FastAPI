namespace FastAPI.Layers.Infrastructure.Messaging.RabbitMQMessaging;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

public class RabbitMQConnection : IDisposable
{
    private readonly RabbitMQConnectionSettings settings;

    public RabbitMQConnection(IOptions<RabbitMQConnectionSettings> connectionSettings)
    {
        this.settings = connectionSettings.Value;
        this.PublishConnection = this.SetupConnection();
        this.PublishChannel = this.SetupChannel(this.PublishConnection);

        this.ConsumeConnection = this.SetupConnection();
        this.ConsumeChannel = this.SetupChannel(this.ConsumeConnection);
    }

    internal IConnection PublishConnection { get; }

    internal IModel PublishChannel { get; private set; }

    internal IConnection ConsumeConnection { get; }

    internal IModel ConsumeChannel { get; private set; }

    public void Dispose()
    {
        PublishChannel.Close();
        PublishChannel.Dispose();

        PublishConnection.Close();
        PublishConnection.Dispose();

        ConsumeChannel.Close();
        ConsumeChannel.Dispose();

        ConsumeConnection.Close();
        ConsumeConnection.Dispose();

        GC.SuppressFinalize(this);
    }

    private static ConnectionFactory GetConnectionFactory(RabbitMQConnectionSettings settings)
    {
        return new ConnectionFactory()
        {
            ClientProvidedName = settings.ClientProvidedName,
            HostName = settings.HostName,
            UserName = settings.UserName,
            Password = settings.Password,
            Port = settings.Port,
            RequestedHeartbeat = settings.HeartbeatInterval,
            DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
        };
    }

    private IConnection SetupConnection()
    {
        try
        {
            return GetConnectionFactory(this.settings).CreateConnection();
        }
        catch (BrokerUnreachableException ex)
        {
            // TODO: apply retry policy
            throw ex;
        }
    }

    private IModel SetupChannel(IConnection connection)
    {
        var channel = connection.CreateModel();
        channel.BasicQos(0, this.settings.PrefetchCount, false);

        return channel;
    }
}

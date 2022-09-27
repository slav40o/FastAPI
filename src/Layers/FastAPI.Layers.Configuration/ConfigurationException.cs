namespace FastAPI.Layers.Configuration;

public class ConfigurationException : Exception
{
	public ConfigurationException()
	{
	}

    public ConfigurationException(string? message) : base(message)
    {
    }
}

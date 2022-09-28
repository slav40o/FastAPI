namespace FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public interface IEndpointRegister
{
    abstract static string BaseURL { get; }

    void AddEndpoints(WebApplication app);
}

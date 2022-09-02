namespace FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public interface IEndpointRegister
{
    void AddEndpoints(WebApplication app);
}

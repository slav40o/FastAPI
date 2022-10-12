namespace FastAPI.Layers.Infrastructure.Http;

using Application;
using Application.Services;

using Infrastructure.Http.Json;
using Infrastructure.Http.Services;
using Infrastructure.Services;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class HttpInfrastructureConfigurations
{
    public static IServiceCollection AddHttpInfrastructureLayer(this IServiceCollection services)
    {
        services.TryAddScoped<ICurrentUser, CurrentUser>();
        services.TryAddScoped<IHttpUtilities, HttpUtilities>();

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new EnumerationJsonConverter());
        });

        return services;
    }
}

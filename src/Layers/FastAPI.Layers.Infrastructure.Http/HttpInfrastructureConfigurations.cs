﻿namespace FastAPI.Layers.Infrastructure.Http;

using FastAPI.Layers.Application;
using FastAPI.Layers.Application.Services;
using FastAPI.Layers.Infrastructure.Http.Services;
using FastAPI.Layers.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class HttpInfrastructureConfigurations
{
    public static IServiceCollection AddHttpInfrastructureLayer(this IServiceCollection services)
    {
        services.TryAddScoped<ICurrentUser, CurrentUser>();
        services.TryAddScoped<IHttpUtilities, HttpUtilities>();
        return services;
    }
}

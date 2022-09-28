﻿namespace FastAPI.Layers.Presentation;

using FastAPI.Layers.Application.Services;
using FastAPI.Layers.Infrastructure.Http.Services;
using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;

public static class PresentationConfiguration
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, Assembly presentationAssembly)
    {
        services.TryAddSingleton<IUrlProvider, EndpointUrlProvider>();
        services.AddEndpointsRegisters(presentationAssembly);
        return services;
    }

    private static IServiceCollection AddEndpointsRegisters(
        this IServiceCollection services, Assembly contextAssembly)
            => services.Scan(scan => scan
                .FromAssemblies(contextAssembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IEndpointRegister)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
}

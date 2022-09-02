namespace FastAPI.Layers.Configuration;

using FastAPI.Layers.Application;
using FastAPI.Layers.Domain;
using FastAPI.Layers.Infrastructure;
using FastAPI.Layers.Presentation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class LayersConfiguration
{
    public static IServiceCollection AddApplicationFeature<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly serviceAssembly)
        where TDbContext : DbContext
            => services
                .AddDomainLayer(serviceAssembly)
                .AddApplicationLayer(serviceAssembly)
                .AddInfrastructureLayer<TDbContext>(configuration, serviceAssembly)
                .AddPresentationLayer(serviceAssembly);

    public static IServiceCollection AddApplicationFeature<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly domainAssembly,
        Assembly applicationAssembly,
        Assembly infrastructureAssembly,
        Assembly presentationAssembly)
        where TDbContext : DbContext
            => services
                .AddDomainLayer(domainAssembly)
                .AddApplicationLayer(applicationAssembly)
                .AddInfrastructureLayer<TDbContext>(configuration, infrastructureAssembly)
                .AddPresentationLayer(presentationAssembly);
}

namespace FastAPI.Layers.Presentation;

using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class PresentationConfiguration
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, Assembly contextAssembly)
        => AddEndpointsRegisters(services, contextAssembly);

    private static IServiceCollection AddEndpointsRegisters(
        this IServiceCollection services, Assembly contextAssembly)
            => services.Scan(scan => scan
                .FromAssemblies(contextAssembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IEndpointRegister)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
}

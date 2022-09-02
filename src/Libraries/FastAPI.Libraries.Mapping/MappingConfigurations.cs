namespace FastAPI.Libraries.Mapping;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class MappingConfigurations
{
    public static IServiceCollection AddMappingProfiles(
        this IServiceCollection services, 
        Assembly contextAssembly)
        => services
            .AddAutoMapper(
                (_, config) => config.AddProfile(new MappingProfile(contextAssembly)),
                Array.Empty<Assembly>());
}

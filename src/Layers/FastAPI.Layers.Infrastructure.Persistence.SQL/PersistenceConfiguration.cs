namespace FastAPI.Layers.Persistence.SQL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddSqlServerPersistence<TContextType>(
        this IServiceCollection services,
        Assembly infrastructureAssembly,
        string connectionString)
            where TContextType : DbContext
    {
        services
            .AddDbContext<TContextType>(options =>
            {
                options
                    .UseSqlServer(connectionString, sb => sb.MigrationsAssembly(typeof(TContextType).Assembly.FullName));
            })
            .AddRepositories(infrastructureAssembly)
            .AddDbInitializers(infrastructureAssembly);

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly contextAssembly)
    {
        services
            .Scan(scan => scan
            .FromAssemblies(contextAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IDbRepository<,,>)))
            .AsMatchingInterface()
            .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddDbInitializers(
        this IServiceCollection services,
        Assembly contextAssembly)
            => services
                .Scan(scan => scan
                .FromAssemblies(contextAssembly)
                .AddClasses(classes => classes.AssignableTo<IDbInitializer>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
}

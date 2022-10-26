namespace FastAPI.Features.Identity.Infrastructure;

using FastAPI.Features.Identity.Application;
using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Features.Identity.Infrastructure.Persistence;
using FastAPI.Features.Identity.Infrastructure.Persistence.Repositories;
using FastAPI.Layers.Infrastructure.Persistence.Postgre;
using FastAPI.Layers.Infrastructure.Persistence;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class IdentityInfrastructureConfigurations
{
    public static IServiceCollection AddIdentityInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration,
        string dbConnectionStringName)
    {
        string? connectionStirng = configuration.GetConnectionString(dbConnectionStringName);
        if (connectionStirng is null)
        {
            throw new ApplicationException("DB connection string is not specified.");
        }

        services
            .AddPostgrePersistence<IdentityUserDbContext>(
                typeof(IdentityInfrastructureConfigurations).Assembly,
                connectionStirng)
            .AddIdentity<IdentityUserDbContext>(configuration)
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddIdentity<TContext>(
        this IServiceCollection services,
        IConfiguration configuration)
            where TContext : IdentityUserDbContext
    {
        var settings = configuration.GetSection(nameof(IdentitySettings));

        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts =
                    settings.GetValue<int>(nameof(IdentitySettings.MaxLoginAttempths));
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(settings.GetValue<int>(nameof(IdentitySettings.LockoutTimeSpanInMinutes)));
                options.Lockout.AllowedForNewUsers =
                    settings.GetValue<bool>(nameof(IdentitySettings.LockoutUserAccounts));

                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength =
                    settings.GetValue<int>(nameof(IdentitySettings.MinPasswordLength));
                options.Password.RequireDigit =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireDigit));
                options.Password.RequireLowercase =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireLowercase));
                options.Password.RequireNonAlphanumeric =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireNonAlphanumeric));
                options.Password.RequireUppercase =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireUppercase));
            })
            .AddEntityFrameworkStores<TContext>()
            .AddDefaultTokenProviders();

        services.AddDataProtection()
            .PersistKeysToDbContext<TContext>();

        return services;
    }
}

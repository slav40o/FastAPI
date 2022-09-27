namespace FastAPI.Features.Identity.Domain;

using FastAPI.Features.Identity.Domain.Services.Auth;
using FastAPI.Features.Identity.Domain.Services.Security;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Domain;
using FastAPI.Layers.Domain.Common;

using Microsoft.Extensions.DependencyInjection;

public static class IdentityDomainConfigurations
{
    public static IServiceCollection AddIdentityDomainLayer(this IServiceCollection services)
        => services
            .AddDomainLayer(typeof(IdentityDomainConfigurations).Assembly)
            .AddCommonDomainEntities(typeof(IdentityDomainConfigurations).Assembly)
            .AddTransient<ILoginValidationService, LoginValidationService>()
            .AddTransient<ILoginProviderService, UserLoginProviderService>()
            .AddTransient<IAuthTokenProvider, JwtAuthTokenProvider>()
            .AddTransient<IRandomTokenProvider, RandomTokenProvider>()
            .AddTransient<IUserManager, UserManager>();
}

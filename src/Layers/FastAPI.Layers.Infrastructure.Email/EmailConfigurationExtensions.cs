namespace StoryBooks.Libraries.Email;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Infrastructure.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Render;
using FastAPI.Layers.Infrastructure.Email.Services;
using FastAPI.Layers.Infrastructure.Email.Settings;
using Fluid;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class EmailConfigurationExtensions
{
    private const string ClientUrlKey = "AppSettings:ClientUrl";

    public static IServiceCollection AddSendGridEmail(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<SendGridEmailSettings> settings)
    {
        services
            .Configure(settings)
            .AddMemoryCache()
            .AddSendGrid()
            .AddTemplateSettings()
            .AddFluidTemplates()
            .AddEmailLayout(configuration)
            .AddTransient<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddMockEmail(this IServiceCollection services)
    {
        services
            .AddTemplateSettings()
            .AddTransient<IEmailTemplateRenderer, MockTemplateRenderer>()
            .AddTransient<IEmailSender, MockEmailSender>()
            .AddTransient<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddTemplateSettings(this IServiceCollection services)
        => services.Configure((EmailTemplateSettings s) => { });
    
    private static IServiceCollection AddEmailLayout(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<LayoutModel>(cfg =>
            {
                cfg.SetClientUrl(configuration.GetValue<string>(ClientUrlKey));
            });

    private static IServiceCollection AddSendGrid(this IServiceCollection services)
        => services.AddTransient<IEmailSender, SendGridEmailSender>();

    private static IServiceCollection AddFluidTemplates(this IServiceCollection services)
    {
        return services.AddSingleton<FluidParser>()
                       .AddTransient<IEmailTemplateRenderer, FluidTemplateRenderer>();
    }
}
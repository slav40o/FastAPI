namespace FastAPI.Layers.Infrastructure.Email;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Infrastructure.Email.Abstractions;
using FastAPI.Layers.Infrastructure.Email.Exceptions;
using FastAPI.Layers.Infrastructure.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Services;
using FastAPI.Layers.Infrastructure.Email.Settings;
using FastAPI.Layers.Infrastructure.Email.Templates;
using FastAPI.Libraries.Validation;

using Fluid;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SendGrid.Extensions.DependencyInjection;

using System.Reflection;

public static class EmailConfigurationExtensions
{
    private static Action<EmailTemplateSettings> DefaultEmailTemplateSettings
    => (s) => { };

    /// <summary>
    /// Configure email services.
    /// </summary>
    /// <param name="services">Services instance.</param>
    /// <param name="emailSettingsConfig">Email settings configuration.</param>
    /// <param name="emailAssemblies">Assemblies containing email template files.</param>
    /// <returns>Configured services collection instance.</returns>
    public static IServiceCollection AddEmail(
        this IServiceCollection services,
        Action<EmailSettings> emailSettingsConfig,
        params Assembly[] emailAssemblies)
           => AddEmail(services, emailSettingsConfig, DefaultEmailTemplateSettings, emailAssemblies);

    /// <summary>
    /// Configure email services.
    /// </summary>
    /// <param name="services">Services instance.</param>
    /// <param name="emailSettingsConfig">Email settings configuration.</param>
    /// <param name="templateSettingsConfig">Template settings configuration override.</param>
    /// <param name="emailAssemblies">Assemblies containing email template files.</param>
    /// <returns>Configured services collection instance.</returns>
    public static IServiceCollection AddEmail(
        this IServiceCollection services,
        Action<EmailSettings> emailSettingsConfig,
        Action<EmailTemplateSettings> templateSettingsConfig,
        params Assembly[] emailAssemblies)
    {
        var emailSettings = new EmailSettings();
        emailSettingsConfig.Invoke(emailSettings);
        ValidateEmailSettings(emailSettings);

        var templateSettings = new EmailTemplateSettings();
        templateSettingsConfig.Invoke(templateSettings);

        services
            .Configure(emailSettingsConfig)
            .Configure(templateSettingsConfig)
            .AddEmailLayout(emailSettings.ClientURL);

        services
            .AddMemoryCache()
            .AddSendGrid(emailSettings.ApiKey)
            .AddFluidTemplates()
            .TryAddSingleton<ITemplateProvider>(
                    new EmbeddedTemplateProvider(templateSettings.LayoutViewFileName, emailAssemblies));

        services.TryAddTransient<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddMockEmail(this IServiceCollection services)
    {
        services.AddTemplateSettings();
        services.TryAddTransient<ITemplateRenderer, FluidTemplateRenderer>();
        services.TryAddTransient<IEmailSender, MockEmailSender>();
        services.TryAddTransient<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddTemplateSettings(this IServiceCollection services)
        => services.Configure((EmailTemplateSettings s) => { });
    
    private static IServiceCollection AddEmailLayout(this IServiceCollection services, string clientUrl)
        => services.Configure<LayoutModel>(cfg =>
            {
                cfg.SetClientUrl(clientUrl);
            });

    private static IServiceCollection AddSendGrid(this IServiceCollection services, string apiKey)
    {
        services.TryAddTransient<IEmailSender, SendGridEmailSender>();
        services.AddSendGrid(options =>
        {
            options.ApiKey = apiKey;
        });
        return services;
    }

    private static IServiceCollection AddFluidTemplates(this IServiceCollection services)
    {
        services.TryAddSingleton<FluidParser>();
        services.TryAddTransient<ITemplateRenderer, FluidTemplateRenderer>();

        return services;
    }

    private static void ValidateEmailSettings(EmailSettings settings)
    {
        Ensure.NotEmpty<InvalidEmailConfigurationException>(
            settings.ApiKey, nameof(settings.ApiKey));

        Ensure.IsValidEmail<InvalidEmailConfigurationException>(settings.SenderAddress);

        Ensure.NotEmpty<InvalidEmailConfigurationException>(
            settings.SenderName, nameof(settings.SenderName));

        Ensure.IsValidUrl<InvalidEmailConfigurationException>(
            settings.ClientURL, nameof(settings.ClientURL));
    }
}
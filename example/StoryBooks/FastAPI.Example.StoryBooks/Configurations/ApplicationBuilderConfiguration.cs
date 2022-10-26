namespace FastAPI.Example.StoryBooks.Configurations;

using FastAPI.Features.Identity;
using FastAPI.Layers.Application.Settings;
using FastAPI.Layers.Infrastructure.Email;
using FastAPI.Layers.Infrastructure.Email.Settings;
using FastAPI.Layers.Infrastructure.Http;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Reflection;
using System.Text;

public static class ApplicationBuilderConfiguration
{
    private const string SecretKey = "Authentication:Secret";
    private const string ApiNameKey = "AppSettings:ApiName";
    private const string ApiVersionKey = "AppSettings:ApiVersion";
    private const string SendGridApiKey = "EmailSettings:ApiKey";

    public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
    {
        // Add shared infrastructure services here
        builder.ConfigureCommonApplicationServices(IdentityConfigurations.GetEmailAssemblies());

        // Add application features here
        builder.Services
            .AddIdentityFeature(builder.Configuration);

        return builder;
    }

    private static void ConfigureCommonApplicationServices(
        this WebApplicationBuilder builder,
        params Assembly[] emailAssemblies)
    {
        if (builder.Environment.IsDevelopment())
        {
            string? apiName = builder.Configuration.GetValue<string>(ApiNameKey);
            string? apiVersion = builder.Configuration.GetValue<string>(ApiVersionKey);

            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
        }

        builder.Services
            .AddCors()
            .AddAuthentication(builder.Configuration)
            .AddAuthorization(c =>
            {
                c.DefaultPolicy = new AuthorizationPolicyBuilder("Bearer")
                    .RequireAuthenticatedUser()
                    .Build();
            });

        var emailSettings = builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>()!;

        // Add shared infrastructure services here
        builder.Services
            .Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)))
            .AddEmail(cfg =>
            {
                cfg.ApiKey = emailSettings.ApiKey;
                cfg.ClientURL = emailSettings.ClientURL;
                cfg.SenderName = emailSettings.SenderName;
                cfg.SenderAddress = emailSettings.SenderAddress;
            }, emailAssemblies)
            // .AddRabbitMqMessaging()
            // .AddAzureDocumentStorage()
            // .AddDistributedRedisCache()
            .AddHttpInfrastructureLayer();
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? secret = configuration.GetValue<string>(SecretKey);
        if (secret is null)
        {
            throw new ApplicationException("Authentication secret is not provided.");
        }

        byte[] key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //.AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = configuration.GetValue<string>("Authentication:Facebook:AppId");
            //    facebookOptions.AppSecret = configuration.GetValue<string>("Authentication:Facebook:AppSecret");
            //})
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}

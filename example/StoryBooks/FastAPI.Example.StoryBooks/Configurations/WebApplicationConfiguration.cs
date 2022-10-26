namespace FastAPI.Example.StoryBooks.Configurations;

using FastAPI.Layers.Infrastructure.Persistence;
using FastAPI.Layers.Presentation;

public static class WebApplicationConfiguration
{
    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.UseCommonApplicationServices();

        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        app.UseApiEndpoints(serviceProvider)
           .UseDbInitializers(serviceProvider);

        return app;
    }

    private static void UseCommonApplicationServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error")
               .UseHsts();
        }

        app.UseHttpsRedirection()
           .UseCors(options => options
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod())
           .UseAuthentication()
           .UseAuthorization()
           .UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger()
               .UseSwaggerUI();
        }
    }

    private static IApplicationBuilder UseDbInitializers(
        this IApplicationBuilder app,
        IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in services)
        {
            AsyncHelper.RunSync(() => initializer.Initialize());
        }

        return app;
    }
}

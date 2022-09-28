namespace FastAPI.Example.StoryBooks.Configurations;

using FastAPI.Features.Identity.Presentation;
using FastAPI.Layers.Persistence;

using Microsoft.Extensions.Options;

public static class WebApplicationConfiguration
{
    public static WebApplication ConfigureWebApplication(this WebApplication app)
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

        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        app.UseApiEndpoints(serviceProvider)
           .UseDbInitializers(serviceProvider);


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger()
               .UseSwaggerUI();
        }

        return app;
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

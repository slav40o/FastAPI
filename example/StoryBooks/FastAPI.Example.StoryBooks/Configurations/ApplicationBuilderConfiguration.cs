namespace FastAPI.Example.StoryBooks.Configurations;

using FastAPI.Features.Identity;

public static class ApplicationBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
    {
        // Add shared infrastructure services here
        builder.ConfigureCommonApplicationServices(IdentityConfigurations.GetEmailAssemblies());

        // Add application features here
        builder.Services
            .AddIdentityFeature(builder.Configuration);

        return builder;
    }
}

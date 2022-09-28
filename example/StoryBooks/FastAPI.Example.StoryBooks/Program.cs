using FastAPI.Example.StoryBooks.Configurations;

WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationServices()
    .Build()
    .ConfigureWebApplication()
    .Run();

namespace FastAPI.Layers.Infrastructure.Email.Render;

using FastAPI.Layers.Application.Email;

internal class MockTemplateRenderer : IEmailTemplateRenderer
{
    public string Extension => ".txt";

    public Task<string> RenderAsync<TModel>(string viewName, TModel model)
        => Task.FromResult($"{viewName} {model?.ToString()}");
}

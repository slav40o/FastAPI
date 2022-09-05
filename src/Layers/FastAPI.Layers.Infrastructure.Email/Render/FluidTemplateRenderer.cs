namespace FastAPI.Layers.Infrastructure.Email.Render;

using FastAPI.Layers.Application.Email;
using FastAPI.Layers.Infrastructure.Email.Exceptions;
using FastAPI.Layers.Infrastructure.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Settings;
using Fluid;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

public class FluidTemplateRenderer : IEmailTemplateRenderer
{
    private readonly EmailTemplateSettings settings;
    private readonly IFileProvider fileProvider;
    private readonly IMemoryCache cache;
    private readonly FluidParser parser;
    private readonly LayoutModel layoutModel;

    public FluidTemplateRenderer(
        IOptions<EmailTemplateSettings> opts,
        IOptions<LayoutModel> layoutOpts,
        IMemoryCache cache,
        FluidParser parser)
    {
        this.settings = opts.Value;
        this.layoutModel = layoutOpts.Value;

        this.fileProvider = settings.FileProvider;
        this.cache = cache;
        this.parser = parser;
    }

    public string Extension => settings.ViewExtension;

    public async Task<string> RenderAsync<TModel>(string viewName, TModel model)
    {
        string resourceName = $"{viewName}{Extension}";

        IFluidTemplate bodyTemplate = GetCachedTemplate(resourceName);
        string bodyContent = await RenderTemplate(model, bodyTemplate);

        IFluidTemplate layoutTemplate = GetLayoutTemplate();
        this.layoutModel.SetContent(bodyContent);
        string emailContent = await RenderTemplate(layoutModel, layoutTemplate);

        return emailContent ?? string.Empty;
    }

    private IFluidTemplate GetLayoutTemplate()
    {
        string resourceName = $"{this.settings.EmailLayoutViewName}{Extension}";
        return GetCachedTemplate(resourceName);
    }

    private IFluidTemplate GetCachedTemplate(string temlateResourceName)
    {
        if (this.cache.Get(temlateResourceName) is not IFluidTemplate cachedTemplate)
        {
            var templateFile = this.fileProvider.GetFileInfo(temlateResourceName);
            if (!templateFile.Exists)
            {
                throw new TemplateNotFoundException($"File {temlateResourceName} is not found!");
            }

            using var s = templateFile.CreateReadStream();
            using var writer = new StreamReader(s);

            var template = writer.ReadToEnd();
            if (!this.parser.TryParse(template, out cachedTemplate, out var error))
            {
                throw new TemplateParseException(error);
            }

            this.cache.Set(temlateResourceName, cachedTemplate);
        }

        return cachedTemplate;
    }

    private static async Task<string> RenderTemplate<TModel>(TModel model, IFluidTemplate cachedTemplate)
    {
        var context = new TemplateContext(model);
        var content = await cachedTemplate.RenderAsync(context);
        if (content is null)
        {
            throw new TemplateRenderException("Content is null!");
        }

        return content;
    }
}
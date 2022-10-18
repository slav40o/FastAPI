namespace FastAPI.Layers.Infrastructure.Email.Templates;

using System.Threading.Tasks;

using FastAPI.Layers.Infrastructure.Email.Abstractions;
using FastAPI.Layers.Infrastructure.Email.Exceptions;
using FastAPI.Layers.Infrastructure.Email.Models;
using FastAPI.Layers.Infrastructure.Email.Settings;
using FastAPI.Libraries.Validation;

using Fluid;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

/// <summary>
/// Template render using Fluid library. This internally relies on the liquid template language.
/// </summary>
internal sealed class FluidTemplateRenderer : ITemplateRenderer
{
    private readonly FluidParser fluidParser;
    private readonly LayoutModel layoutModel;
    private readonly EmailTemplateSettings templateSettings;
    private readonly IMemoryCache memoryCache;
    private readonly ITemplateProvider templateProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="FluidTemplateRenderer"/> class.
    /// </summary>
    /// <param name="fluidParser">Fluid parser instance.</param>
    /// <param name="memoryCache">Memory cache.</param>
    /// <param name="templateProvider">Template content provider.</param>
    /// <param name="layoutModelOptions">Shared layout model settings.</param>
    /// <param name="templateSettingsOptions">Template settings.</param>
    public FluidTemplateRenderer(
        FluidParser fluidParser,
        IMemoryCache memoryCache,
        ITemplateProvider templateProvider,
        IOptions<LayoutModel> layoutModelOptions,
        IOptions<EmailTemplateSettings> templateSettingsOptions)
    {
        ValidateSettings(templateSettingsOptions.Value);

        this.fluidParser = fluidParser;
        this.memoryCache = memoryCache;
        this.templateProvider = templateProvider;
        layoutModel = layoutModelOptions.Value;
        templateSettings = templateSettingsOptions.Value;
    }

    /// <inheritdoc/>
    public Task<string> RenderAsync<TData>(TData data)
    {
        string modelName = typeof(TData).FullName!;
        string templateName = modelName.Replace(
            templateSettings.DataModelNameSuffix, templateSettings.ViewNameSuffix);

        return RenderAsync(templateName, data);
    }

    /// <inheritdoc/>
    public async Task<string> RenderAsync<TData>(string templateName, TData data)
    {
        string resourceName = $"{templateName}{templateSettings.TemplatetExtension}";

        IFluidTemplate bodyTemplate = await GetCachedTemplate(resourceName);
        string bodyContent = await RenderTemplate(data, bodyTemplate);

        string? layoutResourceName = await templateProvider.GetLayoutNameForTemplateAsync(resourceName);
        if (layoutResourceName is null)
        {
            throw new TemplateNotFoundException("Layout template is not found!");
        }

        IFluidTemplate layoutTemplate = await GetCachedTemplate(layoutResourceName);
        layoutModel.Content = bodyContent;
        string emailContent = await RenderTemplate(layoutModel, layoutTemplate);

        return emailContent;
    }

    private static async Task<string> RenderTemplate<TModel>(TModel model, IFluidTemplate template)
    {
        var context = new TemplateContext(model);
        var content = await template.RenderAsync(context);
        if (content is null)
        {
            throw new TemplateRenderException();
        }

        return content;
    }

    private static void ValidateSettings(EmailTemplateSettings settings)
    {
        Ensure.NotEmpty<InvalidTemplateConfigurationException>(
            settings.LayoutViewName, nameof(settings.LayoutViewName));

        Ensure.NotEmpty<InvalidTemplateConfigurationException>(
            settings.TemplatetExtension, nameof(settings.TemplatetExtension));

        Ensure.NotEmpty<InvalidTemplateConfigurationException>(
            settings.DataModelNameSuffix, nameof(settings.DataModelNameSuffix));

        Ensure.NotEmpty<InvalidTemplateConfigurationException>(
            settings.ViewNameSuffix, nameof(settings.ViewNameSuffix));
    }

    private async Task<IFluidTemplate> GetCachedTemplate(string templateResourceName)
    {
        if (memoryCache.Get($"EmailTemplate-{templateResourceName}") is not IFluidTemplate cachedTemplate)
        {
            var template = await templateProvider.GetTemplateAsync(templateResourceName);
            if (template is null || template.Content is null)
            {
                throw new TemplateNotFoundException($"Template '{templateResourceName}' is not found!");
            }

            if (!fluidParser.TryParse(template.Content, out cachedTemplate, out var error))
            {
                throw new TemplateParseException(error);
            }

            memoryCache.Set($"EmailTemplate-{templateResourceName}", cachedTemplate);
        }

        return cachedTemplate;
    }
}

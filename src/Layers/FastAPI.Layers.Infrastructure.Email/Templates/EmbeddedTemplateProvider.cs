namespace FastAPI.Layers.Infrastructure.Email.Templates;

using FastAPI.Layers.Infrastructure.Email.Abstractions;
using FastAPI.Layers.Infrastructure.Email.Models;

using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

/// <inheritdoc/>
internal sealed class EmbeddedTemplateProvider : ITemplateProvider
{
    private const string ResourceFileExtension = ".resources";
    private readonly ConcurrentDictionary<string, Assembly> templateResources;
    private readonly List<string> layoutResourceNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddedTemplateProvider"/> class.
    /// </summary>
    /// <param name="layoutFileName">Layout file name.</param>
    /// <param name="emailAssemblies">Assemblies containing email templates.</param>
    public EmbeddedTemplateProvider(string layoutFileName, params Assembly[] emailAssemblies)
    {
        templateResources = new ConcurrentDictionary<string, Assembly>();
        layoutResourceNames = new List<string>();

        foreach (var a in emailAssemblies)
        {
            foreach (var r in a.GetManifestResourceNames())
            {
                if (r.EndsWith(ResourceFileExtension))
                {
                    continue;
                }

                if (r.EndsWith(layoutFileName))
                {
                    layoutResourceNames.Add(r);
                }

                templateResources.TryAdd(r, a);
            }
        }
    }

    /// <inheritdoc/>
    public Task<ITemplateInfo?> GetTemplateAsync(string embeddedTemplateName)
    {
        if (templateResources.IsNullOrEmpty())
        {
            return Task.FromResult<ITemplateInfo?>(null);
        }

        KeyValuePair<string, Assembly>? resourcePair = templateResources
            .Where(a => a.Key.EndsWith(embeddedTemplateName))
            .FirstOrDefault();

        if (resourcePair is null)
        {
            return Task.FromResult<ITemplateInfo?>(null);
        }

        return Task.FromResult<ITemplateInfo?>(
            new EmbeddedTemplateInfo(embeddedTemplateName, resourcePair.Value.Key, resourcePair.Value.Value));
    }

    /// <inheritdoc/>
    public Task<string?> GetLayoutNameForTemplateAsync(string embeddedTemplateName)
    {
        var resource = layoutResourceNames.FirstOrDefault();
        return Task.FromResult(resource);
    }
}

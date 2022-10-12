namespace FastAPI.Layers.Infrastructure.Email.Models;

using Abstractions;

using System.Reflection;

/// <summary>
/// Holding information about the read template from an embedded resource.
/// </summary>
internal sealed record EmbeddedTemplateInfo : ITemplateInfo
{
    private readonly string resourceName;
    private readonly Assembly resourceAssembly;

    private string? content;
    private bool resourceRead;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbededTemplateInfo"/> class.
    /// </summary>
    /// <param name="name">Template name embedded.</param>
    /// <param name="resourceName">Manifest resource name.</param>
    /// <param name="resourceAssembly">Manifest resource assembly.</param>
    public EmbeddedTemplateInfo(string name, string resourceName, Assembly resourceAssembly)
    {
        this.Name = name;
        this.resourceName = resourceName;
        this.resourceAssembly = resourceAssembly;
    }

    /// <inheritdoc/>
    public string Name { get; init; }

    /// <inheritdoc/>
    public string? Content
    {
        get
        {
            if (this.resourceRead)
            {
                return this.content;
            }

            this.content = this.ReadEmbededResource();
            this.resourceRead = true;
            return this.content;
        }
    }

    private string? ReadEmbededResource()
    {
        if (this.resourceName is null ||
            this.resourceAssembly is null)
        {
            return null;
        }

        using var resourceStream = this.resourceAssembly.GetManifestResourceStream(this.resourceName);
        if (resourceStream is null)
        {
            return null;
        }

        using var streamReader = new StreamReader(resourceStream);
        return streamReader.ReadToEnd();
    }
}

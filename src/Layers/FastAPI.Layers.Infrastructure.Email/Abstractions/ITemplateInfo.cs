namespace FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// Holds information about the read template.
/// </summary>
internal interface ITemplateInfo
{
    /// <summary>
    /// Gets template name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets template content.
    /// </summary>
    public string? Content { get; }
}

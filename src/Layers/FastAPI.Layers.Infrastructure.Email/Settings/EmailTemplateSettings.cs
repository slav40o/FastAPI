namespace FastAPI.Layers.Infrastructure.Email.Settings;

/// <summary>
/// Holds settings information used from <see cref="ITemplateRenderer.cs"/>.
/// </summary>
public sealed class EmailTemplateSettings
{
    /// <summary>
    /// Gets template files extension including the dot.
    /// </summary>
    public string TemplatetExtension { get; internal set; } = ".html";

    /// <summary>
    /// Gets layout template name without the file extension.
    /// </summary>
    public string LayoutViewName { get; internal set; } = "_EmailLayout";

    /// <summary>
    /// Gets layout file name with the extension.
    /// </summary>
    public string LayoutViewFileName => $"{LayoutViewName}{TemplatetExtension}";

    /// <summary>
    /// Gets the common template file name suffix.
    /// </summary>
    public string ViewNameSuffix { get; internal set; } = "Template";

    /// <summary>
    /// Gets the common data class file name suffix.
    /// </summary>
    public string DataModelNameSuffix { get; internal set; } = "EmailModel";
}

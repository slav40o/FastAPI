namespace FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// Template provider interface.
/// </summary>
internal interface ITemplateProvider
{
    /// <summary>
    /// Finds and reads template content for the given template name.
    /// </summary>
    /// <param name="embeddedTemplateName">Unique template name.</param>
    /// <returns>Task with information for the read template.</returns>
    Task<ITemplateInfo?> GetTemplateAsync(string embeddedTemplateName);

    /// <summary>
    /// Get layout view for given embedded template.
    /// If no view is found the firs found will be returned as default.
    /// </summary>
    /// <param name="embeddedTemplateName">Full name of the embedded resource.</param>
    /// <returns>Task with information for the read template.</returns>
    Task<string?> GetLayoutNameForTemplateAsync(string embeddedTemplateName);
}

namespace FastAPI.Layers.Infrastructure.Email.Settings;

using FastAPI.Layers.Infrastructure.Email.IO;

using Microsoft.Extensions.FileProviders;

public class EmailTemplateSettings
{
    /// <summary>
    /// Liquid views extensions
    /// </summary>
    public string ViewExtension { get; private set; }
        = ".html";

    /// <summary>
    /// Naming convention for template model class name
    /// </summary>
    public string ModelNameSuffix { get; private set; }
        = "Model";

    /// <summary>
    /// Email layout view name
    /// </summary>
    public string EmailLayoutViewName { get; private set; }
        = "_Layout";

    /// <summary>
    /// Define file provider that is used form the template renderer
    /// </summary>
    public IFileProvider FileProvider { get; private set; }
        = new CombinedResourcesFileProvider(AppDomain.CurrentDomain.BaseDirectory);

    public void SetFileProvider(IFileProvider fileProvider) => FileProvider = fileProvider;
}

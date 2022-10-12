namespace FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// Email view template renderer.
/// </summary>
public interface ITemplateRenderer
{
    /// <summary>
    /// Renders template using the given <see cref="TData"/> data.
    /// </summary>
    /// <typeparam name="TData">Type of the data used for the template rendering.</typeparam>
    /// <param name="data">Data used for the template rendering.</param>
    /// <returns>Rendered email view.</returns>
    Task<string> RenderAsync<TData>(TData data);

    /// <summary>
    /// Renders template using the given <see cref="TData"/> data.
    /// </summary>
    /// <typeparam name="TData">Type of the data used for the template rendering.</typeparam>
    /// <param name="templateName">Template file name without the extension.</param>
    /// <param name="data">Data used for the template rendering.</param>
    /// <returns>Rendered email view.</returns>
    Task<string> RenderAsync<TData>(string templateName, TData data);
}

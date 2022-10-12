namespace FastAPI.Layers.Infrastructure.Email.Abstractions;

/// <summary>
/// General email layout view model interface. For now is available only internally.
/// In future we can have generic template renderer that accepts email layout type and data.
/// For now we have one shared(set-up in the infrastructure) email layout model and at least one layout template views.
/// </summary>
internal interface IEmailLayoutModel
{
    /// <summary>
    /// Gets or sets layout internal body content.
    /// This content is the dynamic part rendered for each different template view.
    /// </summary>
    string Content { get; set; }
}

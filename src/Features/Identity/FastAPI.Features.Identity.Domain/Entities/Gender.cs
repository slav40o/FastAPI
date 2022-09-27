namespace FastAPI.Features.Identity.Domain.Entities;

using System.Diagnostics.CodeAnalysis;

using FastAPI.Layers.Domain.Entities;

/// <summary>
/// Enumeration holding gender types.
/// </summary>
public sealed class Gender : Enumeration
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Gender(int value)
    : base(value, FromValue<Gender>(value).Name)
    {
    }

    private Gender(int value, string name)
        : base(value, name)
    {
    }

    /// <summary>
    /// Gets Value for Male gender.
    /// </summary>
    public static Gender Male => new(1, nameof(Male));

    /// <summary>
    /// Gets Value for Female gender.
    /// </summary>
    public static Gender Female => new(2, nameof(Female));

    /// <summary>
    /// Gets Value for Other gender.
    /// </summary>
    public static Gender Other => new(3, nameof(Other));
}

namespace FastAPI.Libraries.Mapping.Contracts;

using AutoMapper;

/// <summary>
/// Enforce implementing custom mapping.
/// </summary>
/// <typeparam name="T">Map from type.</typeparam>
public interface ICustomMapFrom<T>
{
    /// <summary>
    /// Add custom mappings.
    /// </summary>
    /// <param name="mapperProfile">Auto-mapper profile.</param>
    void Mapping(Profile mapperProfile);
}

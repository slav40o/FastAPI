namespace FastAPI.Libraries.Mapping.Contracts;

using AutoMapper;

/// <summary>
/// Map from given source type.
/// </summary>
/// <typeparam name="T">Map from type.</typeparam>
public interface IMapFrom<T>
{
    /// <summary>
    /// Add custom mappings.
    /// </summary>
    /// <param name="mapperProfile">Auto-mapper profile.</param>
    void Mapping(Profile mapperProfile)
        => mapperProfile.CreateMap(typeof(T), GetType());
}

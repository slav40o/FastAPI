namespace FastAPI.Layers.Infrastructure.Persistence.Configurations;

using FastAPI.Layers.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Enumeration configuration methods.
/// </summary>
public static class EnumerationConfiguration
{
    /// <summary>
    /// Configure enumeration for EF.
    /// </summary>
    /// <typeparam name="TOwner">Owner type.</typeparam>
    /// <typeparam name="TDependant">Enumeration type.</typeparam>
    /// <param name="cfg">Navigation builder instance.</param>
    public static void ConfigureEnum<TOwner, TDependant>(this OwnedNavigationBuilder<TOwner, TDependant> cfg)
        where TOwner : class
        where TDependant : Enumeration
    {
        cfg.WithOwner();
        cfg.Property(i => i.Value);
    }
}

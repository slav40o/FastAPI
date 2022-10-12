namespace FastAPI.Features.Identity.Infrastructure.Persistence.Configurations;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Infrastructure.Persistence.SQL.Configurations;
using FastAPI.Libraries.Validation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// User entity db configuration.
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configure user entity.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.ConcurrencyStamp)
            .HasMaxLength(512);

        builder
            .Property(x => x.SecurityStamp)
            .HasMaxLength(512);

        builder
            .Property(u => u.PhoneNumber)
            .HasMaxLength(ValidationConstants.Phone.MaxLength);

        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(ValidationConstants.Human.MaxNameLength);

        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(ValidationConstants.Human.MaxNameLength);

        builder.OwnsOne(u => u.Gender, g => { g.ConfigureEnum(); });

        builder.OwnsOne(u => u.RefreshToken, t =>
        {
            t.WithOwner();
            t.Property(e => e.Value)
                .HasMaxLength(512);

            t.Property(e => e.ExpirationTime);
        });
    }
}

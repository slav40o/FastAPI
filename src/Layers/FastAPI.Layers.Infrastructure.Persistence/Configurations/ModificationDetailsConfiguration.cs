namespace FastAPI.Layers.Infrastructure.Persistence.Configurations;

using FastAPI.Layers.Domain.Entities.ValueObjects;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ModificationDetailsConfiguration
{
    public static void ConfigureMoney<TOwner>(this OwnedNavigationBuilder<TOwner, ModificationDetails> cfg)
        where TOwner : class
    {
        cfg.WithOwner();

        cfg.Property(i => i.UserId)
           .IsRequired();

        cfg.Property(i => i.UserEmail)
           .IsRequired();

        cfg.Property(i => i.ModificationDate)
           .IsRequired();
    }

}

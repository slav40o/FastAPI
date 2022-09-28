namespace FastAPI.Features.Identity.Infrastructure.Persistence;

using FastAPI.Layers.Application.Settings;
using FastAPI.Layers.Persistence;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


using System.Threading.Tasks;

public sealed class IdentityDbInitializer : IDbInitializer
{
    private readonly IdentityUserDbContext dbContext;
    private readonly AppSettings settings;
    private readonly RoleManager<IdentityRole> rolesManager;

    public IdentityDbInitializer
        (IdentityUserDbContext dbContext,
        IOptions<AppSettings> settings,
        RoleManager<IdentityRole> rolesManager)
    {
        this.dbContext = dbContext;
        this.settings = settings.Value;
        this.rolesManager = rolesManager;
    }

    public async Task Initialize()
    {
        await this.dbContext.Database.MigrateAsync();
        await this.SeedData();
    }

    public async Task SeedData()
    {
        if (!dbContext.Roles.Any())
        {
            await SeedRoles();
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task SeedRoles()
    {
        foreach (var role in this.settings.Roles)
        {
            await rolesManager.CreateAsync(new IdentityRole(role)
            {
                NormalizedName = role.ToUpperInvariant()
            });
        }
    }
}
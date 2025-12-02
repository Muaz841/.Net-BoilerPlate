using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Seeding;

public static class DataSeeder
{


    public static async Task SeedEverythingAsync(BoilerPlateDbContext context, IPasswordHasher hasher)
    {
        await SeedRolesAsync(context);
        await SeedPermissionsAsync(context);
        await SeedRolePermissionsAsync(context);
        await SeedAdminUserAsync(context, hasher);
    }


    private static async Task SeedRolesAsync(BoilerPlateDbContext context)
    {
        if (await context.UserRoles.AnyAsync()) return;

        var Roles = new List<Role>
            {
                new Role { Name = "Administrator", Code = "ADMIN" },
                new Role { Name = "User", Code = "USER" },
            };


        await context.Roles.AddRangeAsync(Roles);
        await context.SaveChangesAsync();
    }
    private static async Task SeedPermissionsAsync(BoilerPlateDbContext context)
    {
        if (await context.Permissions.AnyAsync()) return;

        var permissions = new List<Permission>
    {
        new() { Name = "Pages.Users",           DisplayName = "User Management",       IsGrantedByDefault = true },
        new() { Name = "Pages.Users.Create",    DisplayName = "Create User",           IsGrantedByDefault = true },
        new() { Name = "Pages.Users.Edit",      DisplayName = "Edit User",             IsGrantedByDefault = true },
        new() { Name = "Pages.Users.Delete",    DisplayName = "Delete User",           IsGrantedByDefault = true },

        new() { Name = "Pages.Roles",           DisplayName = "Role Management",       IsGrantedByDefault = true },
        new() { Name = "Pages.Roles.Create",    DisplayName = "Create Role",           IsGrantedByDefault = true },
        new() { Name = "Pages.Roles.Edit",      DisplayName = "Edit Role",             IsGrantedByDefault = true },
        new() { Name = "Pages.Roles.Delete",    DisplayName = "Delete Role",           IsGrantedByDefault = true },

        new() { Name = "Pages.Tenants",         DisplayName = "Tenant Management",     IsGrantedByDefault = true },
        new() { Name = "Pages.Administration", DisplayName = "Administration",        IsGrantedByDefault = true },
        new() { Name = "Pages.Dashboard",       DisplayName = "View Dashboard",        IsGrantedByDefault = true }
    };

        await context.Permissions.AddRangeAsync(permissions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRolePermissionsAsync(BoilerPlateDbContext context)
    {
        if (await context.RolePermissions.AnyAsync()) return;

        var adminRole = await context.Roles.FirstAsync(r => r.Name == "Administrator");
        var allPermissions = await context.Permissions.ToListAsync();

        var rolePermissions = allPermissions.Select(p => new RolePermission
        {
            RoleId = adminRole.Id,
            PermissionId = p.Id,
            IsGranted = true
        });

        await context.RolePermissions.AddRangeAsync(rolePermissions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(BoilerPlateDbContext context, IPasswordHasher hasher)
    {
        const string email = "admin@boilerplate.com";
        if (await context.UsersEntity.AnyAsync()) return;

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = "System Administrator",
            PasswordHash = hasher.HashPassword("Admin@123"),
            CreatedAt = DateTime.UtcNow
        };

        await context.UsersEntity.AddAsync(admin);

        await context.UserRoles.AddAsync(new UserRole
        {
            UserId = admin.Id,
            RoleId = 1
        });

        await context.SaveChangesAsync();
    }
}

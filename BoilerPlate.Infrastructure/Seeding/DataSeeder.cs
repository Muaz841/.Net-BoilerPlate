using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Seeding;

public static class DataSeeder
{


    public static async Task SeedEverythingAsync(BoilerPlateDbContext context, IPasswordHasher hasher)
    {
        await SeedRolesAsync(context);
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

    private static async Task SeedAdminUserAsync(BoilerPlateDbContext context, IPasswordHasher hasher)
    {
        const string email = "admin@boilerplate.com";
        if (await context.UsersEntity.AnyAsync(u => u.Email == email)) return;

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

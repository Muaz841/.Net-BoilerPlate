using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using BoilerPlate.Infrastructure.Seeding;
using System.Collections.Generic;


namespace BoilerPlate.Infrastructure.Repository;
public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly BoilerPlateDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DatabaseSeeder(BoilerPlateDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await DataSeeder.SeedEverythingAsync(_context, _passwordHasher);       
    }
}

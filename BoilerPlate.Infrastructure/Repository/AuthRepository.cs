using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using Microsoft.EntityFrameworkCore;
using System;
namespace BoilerPlate.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BoilerPlateDbContext _context;

        public AuthRepository(BoilerPlateDbContext context) => _context = context;

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
            => _context.UsersEntity.FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}

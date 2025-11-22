using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoilerPlateDbContext _context;
        private IRepository<User>? _users;
        public UnitOfWork(BoilerPlateDbContext context)
        {
            _context = context;
        }


        public IRepository<User> Users =>
        _users ??= new GenericRepository<User>(_context);


        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

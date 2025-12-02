using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Repository;

public class GenericRepository<T> : IRepository<T> where T : class
{

    private readonly BoilerPlateDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(BoilerPlateDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync([id], ct);
    }

    public async Task GetByEmailAsync(string email, CancellationToken ct = default)
    {
        var blog = _context.UsersEntity
                   .Where(b => b.Email == email)
                   .FirstOrDefault();
         _dbSet.FindAsync(blog);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbSet.ToListAsync(ct);
    }
    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Remove(entity);
    }

    
}


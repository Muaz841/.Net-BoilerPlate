using BoilerPlate.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}

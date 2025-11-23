using BoilerPlate.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces.Auth
{
    public  interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}


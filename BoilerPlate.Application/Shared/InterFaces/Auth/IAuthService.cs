using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.Dtos.Auth;
using BoilerPlate.Application.Shared.DTOS.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces.Auth
{
    public  interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default);

         Task<User> GetByEmailAsyncI(string email, CancellationToken ct);
    }
}

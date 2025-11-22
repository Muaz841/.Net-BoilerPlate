using BoilerPlate.Application.Shared.DTOS.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces.UserInterface
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<UserDto>> GetAllUsersAsync(CancellationToken ct = default);
        Task<UserDto> CreateUserAsync(string email, string name, string password, CancellationToken ct = default);
    }
}

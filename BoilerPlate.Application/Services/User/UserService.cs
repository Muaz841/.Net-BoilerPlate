using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.DTOS.User;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Services.UserServices
{
    public  class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _PasswordHasher;
        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _PasswordHasher = passwordHasher;
        }

        public async Task<UserDto> CreateUserAsync(string email, string name, string password, CancellationToken ct = default)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Name = name,
                PasswordHash = _PasswordHasher.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(user);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync(CancellationToken ct = default)
        {
            var users = await _unitOfWork.Users.GetAllAsync(ct);
            return users.Select(MapToDto).ToList();
        }

        public async  Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id, ct);
            return user is null ? null : MapToDto(user);
        }

        private static UserDto MapToDto(User user) => new(
        user.Id, user.Email, user.Name, user.CreatedAt);
        
    }
}

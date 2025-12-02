using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.DTOS.User;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;


namespace BoilerPlate.Application.Services.UserServices
{
    public  class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserRole> _userRole;
        private readonly IRepository<UserPermission> _userPermission;
        private readonly IRepository<RolePermission> _rolePermissions;
        private readonly IPasswordHasher _PasswordHasher;
        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IRepository<UserRole> userRole, IRepository<UserPermission> userPermission, IRepository<RolePermission> rolePermissions)
        {
            _unitOfWork = unitOfWork;
            _PasswordHasher = passwordHasher;
            _userRole = userRole;
            _userPermission = userPermission;
            _rolePermissions = rolePermissions;
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
            var users = await _unitOfWork.Users.GetAllAsync(null,ct);
            return users.Select(MapToDto).ToList();
        }

        public async  Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id, ct);
            return user is null ? null : MapToDto(user);
        }

        public async Task<IReadOnlyCollection<UserRole>> GetUserRolesAsync(Guid userId)
                      => await _userRole.GetAllAsync(ur => ur.UserId == userId);

        public async Task<IReadOnlyCollection<UserPermission>> GetUserPermissionsAsync(Guid userId)
                       => await _userPermission.GetAllAsync(up => up.UserId == userId);

        public async Task<IReadOnlyCollection<RolePermission>> GetRolePermissionsAsync(int roleId)
                        => await _rolePermissions.GetAllAsync(rp => rp.RoleId == roleId);        

        public async Task UpdateUserAsync(UserDto userinput)
        {
           var user  =  MapToEntity(userinput);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        private static UserDto MapToDto(User user) => new(user.Id, user.Email, user.Name, user.CreatedAt, user.PermissionVersion);

         private static User MapToEntity(UserDto user) =>
         new User
         {
             Id = user.Id,
             Email = user.Email,
             Name = user.Name,
             UpdatedAt = DateTime.UtcNow,
             PermissionVersion = user.PermissionVersion
         };
    }
}

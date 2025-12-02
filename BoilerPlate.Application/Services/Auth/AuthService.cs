using BoilerPlate.Application.Configuration;
using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.Dtos.Auth;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BoilerPlate.Application.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepo;
        private readonly IPasswordHasher _PasswordHasher;
        private readonly JwtSettings _jwt;

        public AuthService(IAuthRepository authRepo, IOptions<JwtSettings> jwtOptions, IPasswordHasher passwordHasher)
        {
            _authRepo = authRepo;
            _jwt = jwtOptions.Value;
            _PasswordHasher = passwordHasher;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var user = await _authRepo.GetByEmailAsync(request.Email, ct);
            if (user == null || !_PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);
            return new LoginResponse(token, DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes));
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("perm_ver", user.PermissionVersion.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? user.Email),
            };
            
            foreach (var userRole in user.UserRoles ?? Enumerable.Empty<UserRole>())
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));                
            }
              
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

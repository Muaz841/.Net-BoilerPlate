using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.Dtos.Auth;
using BoilerPlate.Application.Shared.DTOS.User;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Api.Controllers.AuthController
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);
            return result is null ? Unauthorized() : Ok(result);
        }

        [HttpGet("{email}/GetByEmail")]
        public async Task<ActionResult<User>> GetByEmail(string email, CancellationToken ct)
        {
            var users = await _authService.GetByEmailAsyncI(email, ct);
            return users is null ? NotFound() : Ok(users);
        }
    }
}

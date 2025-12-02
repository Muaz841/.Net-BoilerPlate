using BoilerPlate.Application.Shared.Dtos.Auth;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Api.Controllers.AuthController
{

    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);
            return result is null ? Unauthorized() : Ok(result);
        }
    }
}

using BoilerPlate.Api.Controllers.Users.DTOS;
using BoilerPlate.Application.Shared.Dtos.Auth;
using BoilerPlate.Application.Shared.DTOS.User;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;

namespace BoilerPlate.Api.Controllers.AuthController
{

    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);
            return result is null ? Unauthorized() : Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserRequest request, CancellationToken ct)
        {
            var createdUser = await _userService.CreateUserAsync(
                request.Email,
                request.Name,
                request.Password,
                ct);

            return CreatedAtAction(nameof(Create), new { id = createdUser.Id }, createdUser);
        }



    }
}

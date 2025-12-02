using BoilerPlate.Api.Controllers.Users.DTOS;
using BoilerPlate.Application.Shared.DTOS.User;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Api.Controllers.Users
{

    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken ct)
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        }


        [HttpGet("{id:guid}/GetById")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken ct)
        {
            var user = await _userService.GetUserByIdAsync(id, ct);
            return user is null ? NotFound() : Ok(user);
        }


        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> Create(CreateUserRequest request, CancellationToken ct)
        {                       

            var createdUser = await _userService.CreateUserAsync(
                request.Email,
                request.Name,
                request.Password, 
                ct);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

       
    }
}

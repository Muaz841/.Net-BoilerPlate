using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Api.Controllers.PermissionsController
{

    [Authorize]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;

        public PermissionsController(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }


        [HttpGet("me")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMyPermissions()
        {
            var user = await  _userService.GetCurrentUserAsync();
            var permissions = await _permissionService.GetEffectivePermissionsAsync(user.Id);
            return Ok(permissions);
        }
    }
}

using BoilerPlate.Application.Shared.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Api.Controllers.PermissionsController
{

    [Authorize]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;            
        }


        [HttpGet("me")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMyPermissions()
        {
            var permissions = await _permissionService.GetEffectivePermissionsAsync(_currentUser.Id);
            return Ok(permissions);
        }
    }
}

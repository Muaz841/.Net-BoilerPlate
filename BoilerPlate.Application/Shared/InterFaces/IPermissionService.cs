using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Shared.InterFaces
{
    public interface IPermissionService
    {
        Task<IReadOnlyList<string>> GetEffectivePermissionsAsync(Guid userId);

        Task InvalidateCacheAsync(Guid userId);
    }
}

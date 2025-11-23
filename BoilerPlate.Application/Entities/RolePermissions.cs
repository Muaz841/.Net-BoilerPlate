using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public bool IsGranted { get; set; } = true;

        public Role Role { get; set; } = null!;
        public Permission Permission { get; set; } = null!;

      

    }
}

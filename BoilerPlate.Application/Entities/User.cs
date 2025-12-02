using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<UserPermission> UserPermissions { get; set; } = [];
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public long PermissionVersion { get; set; } = 1;
    }
}

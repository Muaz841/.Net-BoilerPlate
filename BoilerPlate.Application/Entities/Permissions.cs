namespace BoilerPlate.Application.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;  // e.g., "Pages.Users"
    public string DisplayName { get; set; } = string.Empty;  // Localized, e.g., "User Management"
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }  // For hierarchy: Pages → Pages.Users
    public bool IsGrantedByDefault { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Permission? Parent { get; set; }
    public ICollection<Permission> Children { get; set; } = new List<Permission>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
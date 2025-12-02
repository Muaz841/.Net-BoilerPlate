using BoilerPlate.Application.Entities;
using Microsoft.EntityFrameworkCore;
namespace BoilerPlate.Infrastructure.Database.BoilerPlateDbContext
{
    public class BoilerPlateDbContext : DbContext
    {
        public BoilerPlateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> UsersEntity { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserPermission> UserPermissions => Set<UserPermission>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.Code).IsUnique();

                entity.HasMany(r => r.UserRoles)          
                    .WithOne(ur => ur.Role)                
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
            });

         
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.UserRoles)          
            .WithOne(ur => ur.User)              
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            });
         
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.Name).IsUnique();
                entity.HasOne(p => p.Parent)
                      .WithMany(p => p.Children)
                      .HasForeignKey(p => p.ParentId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)  
                      .HasForeignKey(rp => rp.RoleId);
                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionId);
            });

            // UserPermission (similar)
            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(up => new { up.UserId, up.PermissionId });
                entity.HasOne(up => up.User)
                      .WithMany(u => u.UserPermissions)  
                      .HasForeignKey(up => up.UserId);
                entity.HasOne(up => up.Permission)
                      .WithMany(p => p.UserPermissions)
                      .HasForeignKey(up => up.PermissionId);
            });

            base.OnModelCreating(modelBuilder);
        }
    };
    
    
}

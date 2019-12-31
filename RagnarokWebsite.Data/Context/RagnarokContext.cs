using RagnarokWebsite.Data.Models;
using System.Data.Entity;

namespace RagnarokWebsite.Data.Context
{
    public class RagnarokContext : DbContext
    {
        public RagnarokContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }

        public DbSet<CampMaster> CampMasters { get; set; }
        public DbSet<Camp> Camps { get; set; }
    }
}

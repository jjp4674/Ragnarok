using System.Collections.Generic;

namespace RagnarokWebsite.Data.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}

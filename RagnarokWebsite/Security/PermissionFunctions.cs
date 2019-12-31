using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RagnarokWebsite.Security
{
    public static class PermissionFunctions
    {
        public static bool CheckPermission(HttpRequestBase request, string permissionName)
        {
            var ctx = request.GetOwinContext();
            var authManager = ctx.Authentication;

            if (authManager.User.Claims.Any(c => c.Value.Contains(permissionName)))
            {
                return true;
            }

            return false;
        }
    }
}
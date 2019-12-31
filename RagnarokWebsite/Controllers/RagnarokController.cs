﻿using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Controllers
{
    public class RagnarokController : Controller
    {
        RagnarokContext db = new RagnarokContext();

        public bool ValidateCredentials(string permission)
        {
            if (!PermissionFunctions.CheckPermission(Request, permission))
            {
                return false;
            }

            return true;
        }

        public long GetCurrentUserId()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            long? userId = authManager.User.Claims.Where(c => c.Type == ClaimTypes.UserData).Select(c => Convert.ToInt64(c.Value)).SingleOrDefault();
            if (userId == null)
            {
                userId = 0;
            }

            return (long)userId;
        }
    }
}
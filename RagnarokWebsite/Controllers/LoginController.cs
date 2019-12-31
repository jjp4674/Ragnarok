using log4net;
using Microsoft.AspNet.Identity;
using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
using RagnarokWebsite.Models;
using RagnarokWebsite.Security;
using RagnarokWebsite.Security.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Controllers
{
    [AllowAnonymous]
    public class LoginController : RagnarokController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (RagnarokContext dbContext = new RagnarokContext())
            {
                // Authenticate User
                var user = dbContext.Users.FirstOrDefault(u => u.Username == model.Username && !u.Deleted);

                if (user == null)
                {
                    ModelState.AddModelError("", LoginErrorConstants.INVALID_USERNAME_OR_PASSWORD);
                    return View();
                }

                if (user.Locked)
                {
                    ModelState.AddModelError("", LoginErrorConstants.ACCOUNT_LOCKED);
                    return View();
                }

                if (!PasswordFunctions.ValidatePassword(model.Password, user.PasswordHash))
                {
                    ModelState.AddModelError("", LoginErrorConstants.INVALID_USERNAME_OR_PASSWORD);
                    IncrementFailedLogins(user, dbContext);

                    return View();
                }

                // The user has successfully logged in
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.UserData, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                DefaultAuthenticationTypes.ApplicationCookie);

                // Add permissions to identity
                foreach (UserRole ur in user.UserRoles)
                {
                    foreach (RolePermission rp in ur.Role.RolePermissions)
                    {
                        // If the claim doesn't already exist, add it to the identity
                        if (!identity.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == rp.Permission.Name && c.ValueType == rp.Permission.PermissionGroup.Name))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, rp.Permission.Name, rp.Permission.PermissionGroup.Name));
                        }
                    }
                }

                try
                {
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);
                }
                catch (Exception ex)
                {
                    log.Error("An error occurred when logging in", ex);
                }

                user.FailedLogins = 0;
                dbContext.SaveChanges();

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
        }

        private void IncrementFailedLogins(User user, RagnarokContext dbContext)
        {
            user.FailedLogins++;
            if (user.FailedLogins > 5)
            {
                user.Locked = true;
                user.LockedDate = DateTime.Now;
            }
            dbContext.SaveChanges();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }
    }
}
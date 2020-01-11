using RagnarokWebsite.Controllers;
using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
using RagnarokWebsite.Models;
using RagnarokWebsite.Security;
using RagnarokWebsite.Security.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Areas.Administration.Controllers
{
    [Authorize]
    public class UsersController : RagnarokController
    {
        private RagnarokContext db = new RagnarokContext();

        // GET: Administration/Users
        public ActionResult Index()
        {
            if (!ValidateCredentials(PermissionConstants.VIEW_USER_ADMINISTRATION))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "" });
            }

            return View(db.Users.Where(u => !u.Deleted).OrderBy(u => u.Username).ToList());
        }

        // GET: Administration/Users/Create
        public ActionResult Create()
        {
            if (!ValidateCredentials(PermissionConstants.CREATE_NEW_USERS))
            {
                return RedirectToAction("Index");
            }

            return View(new UserViewModel(ValidateCredentials(PermissionConstants.LIMIT_VIEWABLE_CALLS)));
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                var userId = GetCurrentUserId();

                //var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                User newUser = new User
                {
                    UserId = 0,
                    Username = formCollection["Username"],
                    PasswordHash = PasswordFunctions.HashPassword(formCollection["Password"]), 
                    Email = formCollection["Email"],
                    //EmailToken = token,
                    EmailConfirmed = true,
                    FirstName = formCollection["FirstName"],
                    LastName = formCollection["LastName"],
                    Locked = false,
                    FailedLogins = 0,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                string roles = "";
                // Get each selected role from the page, and add a UserRole record if it doesn't exist
                foreach (string key in formCollection.AllKeys.Where(k => k.StartsWith("Roles[") && k.EndsWith("].RoleId")))
                {
                    string id = key.Replace("Roles[", "").Replace("].RoleId", "");

                    // If the IsSelected flag contains the word "true", it is selected
                    if (formCollection["Roles[" + id + "].IsSelected"].Contains("true"))
                    {
                        string roleId = formCollection[key];
                        long roleLong = Convert.ToInt64(roleId);

                        if (!string.IsNullOrEmpty(roles))
                        {
                            roles += ", ";
                        }
                        Role dbRole = db.Roles.FirstOrDefault(r => r.RoleId == roleLong);
                        roles += dbRole.Name;

                        UserRole ur = new UserRole
                        {
                            UserRoleId = 0,
                            UserId = newUser.UserId,
                            RoleId = roleLong,
                            CreatedBy = userId,
                            CreatedDate = DateTime.Now
                        };

                        db.UserRoles.Add(ur);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Edit(Int64 id)
        {
            if (!ValidateCredentials(PermissionConstants.EDIT_EXISTING_USERS))
            {
                return RedirectToAction("Index");
            }

            var user = db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return View("Error");
            }

            return View(new UserViewModel(user, ValidateCredentials(PermissionConstants.LIMIT_VIEWABLE_CALLS)));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            try
            {
                var userId = GetCurrentUserId();

                long existingUserId = Convert.ToInt64(formCollection["UserId"]);
                User user = db.Users.FirstOrDefault(u => u.UserId == existingUserId);
                if (user == null)
                {
                    return View("Error");
                }

                if (!string.IsNullOrEmpty(formCollection["Password"]) && !PasswordFunctions.ValidatePassword(formCollection["Password"], user.PasswordHash))
                {
                    user.PasswordHash = PasswordFunctions.HashPassword(formCollection["Password"]);
                }

                //bool newEmail = false;
                //var token = "";
                //if (user.Email != formCollection["Email"])
                //{
                //    user.EmailConfirmed = false;
                //    token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                //    user.EmailToken = token;
                //    newEmail = true;
                //}
                user.Email = formCollection["Email"];

                user.FirstName = formCollection["FirstName"];
                user.LastName = formCollection["LastName"];

                // If locked and being unlocked, reset the failed logins count
                string LockedValue = formCollection["Locked"];
                if (!LockedValue.Contains("true") && user.Locked != (LockedValue.Contains("true") ? true : false))
                {
                    user.FailedLogins = 0;
                }
                // If unlocked and being locked, set the locked date
                if (LockedValue.Contains("true") && user.Locked != (LockedValue.Contains("true") ? true : false))
                {
                    user.LockedDate = DateTime.Now;
                }
                user.Locked = (LockedValue.Contains("true") ? true : false);

                user.UpdatedBy = userId;
                user.UpdatedDate = DateTime.Now;
                db.SaveChanges();

                List<long> selectedRoleIds = new List<long>();

                // Get each selected role from the page, and add a UserRole record if it doesn't exist
                foreach (string key in formCollection.AllKeys.Where(k => k.StartsWith("Roles[") && k.EndsWith("].RoleId")))
                {
                    string id = key.Replace("Roles[", "").Replace("].RoleId", "");

                    // If the IsSelected flag contains the word "true", it is selected
                    if (formCollection["Roles[" + id + "].IsSelected"].Contains("true"))
                    {
                        string roleId = formCollection[key];
                        long roleLong = Convert.ToInt64(roleId);
                        selectedRoleIds.Add(roleLong);

                        var urDB = db.UserRoles.FirstOrDefault(urs => urs.UserId == user.UserId && urs.RoleId == roleLong);
                        if (urDB == null)
                        {
                            UserRole ur = new UserRole
                            {
                                UserRoleId = 0,
                                UserId = user.UserId,
                                RoleId = roleLong,
                                CreatedBy = userId,
                                CreatedDate = DateTime.Now
                            };

                            db.UserRoles.Add(ur);
                        }
                    }
                }

                // Find any records in the UserRoles table that don't exist in the checked list and remove them
                foreach (UserRole userRole in db.UserRoles.Where(ur => ur.UserId == user.UserId).ToList())
                {
                    // If the role isn't found in the selected list
                    if (!selectedRoleIds.Contains(userRole.RoleId))
                    {
                        db.UserRoles.Remove(userRole);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Int64 userId)
        {
            if (!ValidateCredentials(PermissionConstants.DELETE_USERS))
            {
                return RedirectToAction("Index");
            }

            var user = db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                // Remove user roles
                foreach (var userRole in db.UserRoles.Where(ur => ur.UserId == userId).ToList())
                {
                    db.UserRoles.Remove(userRole);
                }
                db.SaveChanges();

                user.Deleted = true;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
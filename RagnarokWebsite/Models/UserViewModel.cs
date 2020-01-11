using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class UserViewModel
    {
        private RagnarokContext db = new RagnarokContext();

        [Key]
        [HiddenInput(DisplayValue = false)]
        public Int64 UserId { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public bool Locked { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Locked Date")]
        public DateTime? LockedDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "# of Failed Logins")]
        public int FailedLogins { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public UserViewModel(bool limit)
        {
            List<Role> dbRoles = db.Roles.ToList();

            Roles = new List<RoleViewModel>();
            foreach (var r in dbRoles)
            {
                var rvm = new RoleViewModel(false)
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description,
                    IsSelected = false
                };

                Roles.Add(rvm);
            }
        }

        public UserViewModel(User user, bool limit)
        {
            Roles = new List<RoleViewModel>();

            UserId = user.UserId;
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Locked = user.Locked;
            LockedDate = user.LockedDate;
            FailedLogins = user.FailedLogins;

            List<Role> dbRoles = db.Roles.ToList();

            foreach (var r in dbRoles)
            {
                var selected = false;
                if (user.UserRoles.Any(ur => ur.Role.RoleId == r.RoleId))
                {
                    selected = true;
                }

                var rvm = new RoleViewModel(false)
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description,
                    IsSelected = selected
                };

                Roles.Add(rvm);
            }
        }
    }
}
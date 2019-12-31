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
    public class RegistrationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string RegistrationType { get; set; }

        [DataType(DataType.Text)]
        public string BusinessName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Address1 { get; set; }

        [DataType(DataType.Text)]
        public string Address2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool VolunteerTroll { get; set; }
        public bool VolunteerWatch { get; set; }
        public bool VolunteerMedic { get; set; }
        public bool VolunteerDay { get; set; }
        public bool VolunteerNight { get; set; }
        public bool VolunteerCheck { get; set; }
        public bool VolunteerRagU { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CharacterName { get; set; }

        [DataType(DataType.Text)]
        public string ChapterName { get; set; }

        [DataType(DataType.Text)]
        public string UnitName { get; set; }

        public int CampId { get; set; }
        public List<SelectListItem> CampOptions { get; set; }

        public RegistrationViewModel()
        {
            RagnarokContext db = new RagnarokContext();

            List<Camp> camps = db.Camps.Where(x => x.Year == 2020).OrderBy(x => x.Name).ToList();
            CampOptions = new List<SelectListItem>();
            CampOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = "-- Select a Camp --"
            });

            foreach (var c in camps)
            {
                CampOptions.Add(new SelectListItem
                {
                    Value = c.CampId.ToString(), 
                    Text = c.Name
                });
            }
        }
    }
}
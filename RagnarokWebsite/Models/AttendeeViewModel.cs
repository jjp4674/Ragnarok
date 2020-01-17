using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class AttendeeViewModel
    {
        private RagnarokContext db = new RagnarokContext();

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ParticipantId { get; set; }

        public string CampName { get; set; }

        public List<SelectListItem> CampOptions { get; set; }
        [Required]
        public string CampId { get; set; }

        [DataType(DataType.Text)]
        public int TagNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        public bool IsMinor { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CharacterName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ChapterName { get; set; }

        [DataType(DataType.Text)]
        public string UnitName { get; set; }

        public DateTime EventYear { get; set; }
        public DateTime DateSigned { get; set; }

        public List<SelectListItem> StatusOptions { get; set; }
        public string Status { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public bool IsMerchant { get; set; }
        public bool VolunteerTroll { get; set; }
        public bool VolunteerSafety { get; set; }
        public bool VolunteerMedic { get; set; }
        public bool VolunteerDay { get; set; }
        public bool VolunteerNight { get; set; }
        public bool VolunteerWeapon { get; set; }
        public bool VolunteerRagU { get; set; }

        [DataType(DataType.Text)]
        public int MinorParentTagNumber { get; set; }

        public bool ReplacementTag { get; set; }











        public AttendeeViewModel()
        {

        }

        public AttendeeViewModel(Participant participant)
        {
            ParticipantId = participant.ParticipantId;
            FirstName = participant.FirstName;
            LastName = participant.LastName;
            DOB = participant.DOB;
            IsMinor = participant.IsMinor == "Y" ? true : false;
            CampName = participant.Camp.Name;
            CharacterName = participant.CharacterName;
            Status = participant.Status;
            IsMerchant = participant.IsMerchant == "Y" ? true : false;
        }
    }
}
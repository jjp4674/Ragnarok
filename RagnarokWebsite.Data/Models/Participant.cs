using System;
using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        public int CampId { get; set; }
        public int? TagNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string CharacterName { get; set; }
        public string ChapterName { get; set; }
        public DateTime EventYear { get; set; }
        public DateTime DateSigned { get; set; }
        public string Status { get; set; }
        public string IsMinor { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string IsMerchant { get; set; }
        public string VolunteerTroll { get; set; }
        public string VolunteerSafety { get; set; }
        public string VolunteerMedic { get; set; }
        public string VolunteerDay { get; set; }
        public string VolunteerNight { get; set; }
        public string VolunteerWeapon { get; set; }
        public string VolunteerRagU { get; set; }
        public int? MinorParentTagNumber { get; set; }
        public string ReplacementTag { get; set; }

        public virtual Camp Camp { get; set; }
    }
}

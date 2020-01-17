using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
using RagnarokWebsite.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class CampViewModel
    {
        private RagnarokContext db = new RagnarokContext();
        
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CampId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Location { get; set; }
        
        public List<SelectListItem> Options { get; set; }
        public string Year { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CampMasterId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CMFirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CMLastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CMCharacterName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string CMEmail { get; set; }

        public int NumRegistrations { get; set; }

        public CampViewModel()
        {
            PopulateOptions();

            Year = "2020";
            NumRegistrations = 0;
        }

        public CampViewModel(Camp camp)
        {
            PopulateOptions();

            CampId = camp.CampId;
            Name = camp.Name;
            Location = camp.Location;
            Year = camp.Year.ToString();

            CampMasterId = camp.CampMaster.CampMasterId;
            CMFirstName = camp.CampMaster.FirstName;
            CMLastName = camp.CampMaster.LastName;
            CMCharacterName = camp.CampMaster.CharacterName;
            CMEmail = camp.CampMaster.Email;

            NumRegistrations = camp.Participants.Count;
        }

        private void PopulateOptions()
        {
            Options = DateExtensions.GetDateOptions();
        }
    }
}
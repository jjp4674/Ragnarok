using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RagnarokWebsite.Data.Models
{
    public class CampMaster
    {
        [Key]
        public int CampMasterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CharacterName { get; set; }
        public string Email { get; set; }
    }
}

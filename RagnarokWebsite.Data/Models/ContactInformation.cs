using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class ContactInformation
    {
        [Key]
        public int ContactInformationId { get; set; }

        public int ParticipantId { get; set; }

        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

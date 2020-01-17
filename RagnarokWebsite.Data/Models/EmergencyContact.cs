using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class EmergencyContact
    {
        [Key]
        public int EmergencyContactId { get; set; }

        public int ParticipantId { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        public int ParticipantId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

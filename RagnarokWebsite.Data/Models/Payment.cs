using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int ParticipantId { get; set; }

        public string AuthCode { get; set; }
        public string TransId { get; set; }
        public double Amount { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

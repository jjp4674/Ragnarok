using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RagnarokWebsite.Data.Models
{
    public class HealthIssue
    {
        [Key]
        public int HealthIssueId { get; set; }

        public int ParticipantId { get; set; }

        [Column("HealthIssue")]
        public string Issue { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

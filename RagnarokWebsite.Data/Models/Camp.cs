using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RagnarokWebsite.Data.Models
{
    public class Camp
    {
        [Key]
        public int CampId { get; set; }
        public int CampMasterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public short Year { get; set; }

        public virtual CampMaster CampMaster { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}

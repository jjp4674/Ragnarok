using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}

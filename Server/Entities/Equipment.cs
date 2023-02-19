using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class Equipment {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public float Budget { get; set; }
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; } = default!;
        public int InvestigatorId { get; set; }
        [ForeignKey("InvestigatorId")]
        public Investigator Investigator { get; set; } = default!;
    }
}
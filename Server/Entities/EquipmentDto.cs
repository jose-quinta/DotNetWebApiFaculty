using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class EquipmentDto {
        public string Description { get; set; } = string.Empty;
        public float Budget { get; set; }
        public int FacultyId { get; set; }
        public int InvestigatorId { get; set; }
    }
}
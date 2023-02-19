using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class Faculty {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Equipment> Equipments { get; set; } = new List<Equipment>();
        public List<Investigator> Researchers { get; set; } = new List<Investigator>();
    }
}
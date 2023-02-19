using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class EquipmentsName
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Names { get; set; } = new List<string>();
    }
}
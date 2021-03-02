using System;
using System.Collections.Generic;

#nullable disable

namespace Context.Models
{
    public partial class Termin
    {
        public Termin()
        {
            Kundentermins = new HashSet<Kundentermin>();
        }

        public int TerminId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Ende { get; set; }
        public string Bemerkung { get; set; }

        public virtual ICollection<Kundentermin> Kundentermins { get; set; }
    }
}

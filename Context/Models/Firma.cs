using System;
using System.Collections.Generic;

#nullable disable

namespace Context.Models
{
    public partial class Firma
    {
        public Firma()
        {
            Ansprechpartners = new HashSet<Ansprechpartner>();
        }

        public int FirmenId { get; set; }
        public string Name { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }

        public virtual ICollection<Ansprechpartner> Ansprechpartners { get; set; }
    }
}

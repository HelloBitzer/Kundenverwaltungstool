using System;
using System.Collections.Generic;

#nullable disable

namespace Context.Models
{
    public partial class Ansprechpartner
    {
        public Ansprechpartner()
        {
            Kundentermins = new HashSet<Kundentermin>();
        }

        public int AnsprechpartnerId { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int FirmenId { get; set; }
        public string Titel { get; set; }

        public virtual Firma Firmen { get; set; }
        public virtual ICollection<Kundentermin> Kundentermins { get; set; }
    }
}

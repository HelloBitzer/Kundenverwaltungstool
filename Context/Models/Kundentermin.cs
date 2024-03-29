﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Context.Models
{
    public partial class Kundentermin
    {
        public int AnsprechpartnerId { get; set; }
        public int TerminId { get; set; }

        public virtual Ansprechpartner Ansprechpartner { get; set; }
        public virtual Termin Termin { get; set; }
    }
}

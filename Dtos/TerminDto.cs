using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dtos
{
    public class TerminDto
    {
        public int TerminId { get; set; }
        [JsonRequired]
        public DateTime Start { get; set; }
        [JsonRequired]
        public DateTime? Ende { get; set; }
        [JsonRequired]
        public string Bemerkung { get; set; }
    }
}

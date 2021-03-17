using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dtos
{
    public class KundenterminDto
    {
        public int KundenterminId { get; set; }
        [JsonRequired]
        public int AnsprechpartnerId { get; set; }
        [JsonRequired]
        public int TerminId { get; set; }
        [JsonRequired]
        public string Name { get; set; }
    }
}

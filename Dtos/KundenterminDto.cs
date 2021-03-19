using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dtos
{
    public class KundenterminDto
    {
        [JsonRequired]
        public int AnsprechpartnerId { get; set; }
        [JsonRequired]
        public int TerminId { get; set; }
    }
}

using Context.Models;
using MyNamespace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos
{
    public class FirmaDto
    {
        public int FirmenId { get; set; }
        [JsonRequired]
        public string Name { get; set; }
        [JsonRequired]
        public string Strasse { get; set; }
        [JsonRequired]
        public string Hausnummer { get; set; }
        [JsonRequired]
        public string Plz { get; set; }
        [JsonRequired]
        public string Ort { get; set; }
    }
}

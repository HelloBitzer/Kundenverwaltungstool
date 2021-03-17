using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dtos
{
    public class AnsprechpartnerDto
    {
        public int AnsprechpartnerId { get; set; }
        [JsonRequired]
        public string Nachname { get; set; }
        [JsonRequired]
        public string Vorname { get; set; }
        [JsonRequired]
        public string Telefon { get; set; }
        [JsonRequired]
        public string Email { get; set; }
        [JsonRequired]
        public int FirmenId { get; set; }
        [JsonRequired]
        public string Titel { get; set; }
    }
}

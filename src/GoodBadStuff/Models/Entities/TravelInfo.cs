using System;
using System.Collections.Generic;

namespace GoodBadStuff.Models.Entities
{
    public partial class TravelInfo
    {
        public int Id { get; set; }
        public string Transport { get; set; }
        public double? Co2 { get; set; }
        public DateTime? Date { get; set; }
        public int? TreeCount { get; set; }
        public double? Distance { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string UserId { get; set; }
    }
}

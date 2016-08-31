using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class Travels
    {
        public string Transport { get; set; }
        public double? Co2 { get; set; }
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? TreeCount { get; set; }
        public double? Distance { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string UserId { get; set; }
        public List<int> Vehicles { get; set; }

    }
}

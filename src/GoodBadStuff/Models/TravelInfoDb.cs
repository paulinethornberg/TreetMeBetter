using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models
{
    public class TravelInfoDb
    {
        public int Id { get; set; }
        public string Transport { get; set; }
        public float Co2 { get; set; }
        public DateTime Date { get; set; }
        public int TreeCount { get; set; }
        public float Distance { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int UserId { get; set; }
    }
}

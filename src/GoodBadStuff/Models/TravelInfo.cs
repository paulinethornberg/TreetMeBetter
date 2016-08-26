using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models
{
    public class TravelInfo
    {
        public string FromLat { get; set; }
        public string FromLng { get; set; }
        public string ToLat { get; set; }
        public string ToLng { get; set; }

        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        //&public string UrlInCaseOfFire { get; set; }
    }
}

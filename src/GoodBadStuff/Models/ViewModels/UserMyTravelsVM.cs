using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class UserMyTravelsVM
    {
        public UserMyTravelsVM(string transport, float co2, string date, float distance, string fromAddress, string toAddress )
        {
            Transport = transport;
            Co2 = co2;
            Date = date;
            Distance = distance;
            FromAddress = fromAddress;
            ToAddress = toAddress;



        }
        public string Transport { get; set; }
        public float Co2 { get; set; }
        public string Date { get; set; }
        public float Distance { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Id { get; set; }

    }
}

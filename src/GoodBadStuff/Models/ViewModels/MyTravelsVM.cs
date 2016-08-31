using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class MyTravelsVM
    {
        public List<Travels> TravelInfo { get; set; }
        public int TravelsByBus { get; set; }
        public int TravelsByCar { get; set; }
        public int TravelsByWalking { get; set; }
        public int TravelsByTrain { get; set; }
        public int TravelsByBicycling { get; set; }
        public int TravelsByMotorcycle { get; set; }
    }
}

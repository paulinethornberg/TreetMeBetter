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
        public Travels Co2Max { get; set; }
        public Travels Co2Min { get; set; }
        public Double? Co2Average { get; set; }
        public Travels Co2Mean { get; set; }
        //public Travels City1 { get; set; }
        //public Travels City2 { get; set; }
        //public Travels City3 { get; set; }
        //public Travels City4{ get; set; }
        //public Travels City5 { get; set; }
    }
}

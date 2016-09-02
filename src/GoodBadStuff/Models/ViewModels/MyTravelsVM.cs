using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class CityStatistics
    {
        public string CityName { get; set; }
        public int TimesVisited { get; set; }
    }

    public class MyTravelsVM
    {
        public List<Travels> TravelInfo { get; set; }
        public int TravelsByBus { get; set; }
        public int TravelsByCar { get; set; }
        public int TravelsByWalking { get; set; }
        public int TravelsByTrain { get; set; }
        public int TravelsByBicycling { get; set; }
        public int TravelsByMotorcycle { get; set; }
        public Travels Co2CarMax { get; set; }
        public Travels Co2CarMin { get; set; }
        public Double? Co2CarAverage { get; set; }
        public Travels Co2Mean { get; set; }
        public double? TotalCo2 { get; set; }
        public List<CityStatistics> CityStatistics { get; set; } = new List<ViewModels.CityStatistics>();
    }
}

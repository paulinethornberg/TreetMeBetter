using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using GoodBadStuff.Models;
using GoodBadStuff.Models.ViewModels;
using Newtonsoft.Json.Linq;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodBadStuff.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetCarbonData(TravelInfo travelInfo)
        {
            string webapiurl = $"http://api.commutegreener.com/api/co2/emissions?startLat={travelInfo.FromLat}&startLng={travelInfo.FromLng}&endLat={travelInfo.ToLat}&endLng={travelInfo.ToLng}&format=json";
            var client = new HttpClient();
            var json = await client.GetStringAsync(webapiurl);
            // SAVE TO DATABASE() - GET INFO FROM json strängen här && get more 
            SQL sql = new SQL();
            sql.GetValuesFromAPIs(travelInfo, json);

            return Content(json, "application/json");
        }

    }
}

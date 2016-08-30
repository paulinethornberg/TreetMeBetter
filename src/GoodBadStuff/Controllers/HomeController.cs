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
using GoodBadStuff.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodBadStuff.Controllers
{
    public class HomeController : Controller
    {
        DataManager dataManager;

        public HomeController(TrvlrContext context, UserManager<IdentityUser> userManager)
        {
            dataManager= new DataManager(context, userManager);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetCarbonData(TravelInfoVM travelInfo)
        {

            string webapiurl = $"http://api.commutegreener.com/api/co2/emissions?startLat={travelInfo.FromLat}&startLng={travelInfo.FromLng}&endLat={travelInfo.ToLat}&endLng={travelInfo.ToLng}&format=json";
            var client = new HttpClient();
            var json = await client.GetStringAsync(webapiurl);
            // SAVE TO DATABASE() - GET INFO FROM json strängen här && get more 

            
            var id = dataManager.GetValuesFromAPIs(travelInfo, json);
            
            var ret = new { apiJson = json, travelInfoId = id };

            return Json(ret);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCarbonData(GetTravelId travelId)
        {
            string userName = User.Identity.Name;
            await dataManager.SaveTravelToUser(travelId.Id, userName);

            return View("home/index");
        }

    }

    public class GetTravelId
    {
        public int Id { get; set; }
    }
}

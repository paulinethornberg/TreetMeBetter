using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodBadStuff.Controllers
{
    public class HomeController : Controller
    {

        HttpClient _client;

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        
        public async Task<ActionResult> GetCarbonData()
        {
            string webapiurl = "http://api.commutegreener.com/api/co2/emissions?startLat=57.7097704&startLng=11.9661608&endLat=57.6969943&endLng=11.9865&format=json";
            _client = new HttpClient();
            _client.BaseAddress = new Uri(webapiurl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var ResponseMessage = await _client.GetAsync(webapiurl);

            if (ResponseMessage.IsSuccessStatusCode)
            {

                var data = ResponseMessage.Content.ReadAsStringAsync().Result;
                //var datasource = JsonConvert.DeserializeObject<List<DataSourceInfo>>(data);
                return Json(data);

            }

            return View("Error");
   
        }

    }
}

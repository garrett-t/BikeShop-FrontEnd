using BikeShop_FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BikeShop_FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<BicycleModel> bikeTypes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("modeltype");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BicycleModel>>();
                    readTask.Wait();

                    bikeTypes = readTask.Result;
                }
                else
                {
                    bikeTypes = Enumerable.Empty<BicycleModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(bikeTypes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
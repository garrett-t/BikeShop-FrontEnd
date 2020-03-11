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
            IEnumerable<BicycleTypeModel> bikeTypes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("modeltype");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BicycleTypeModel>>();
                    readTask.Wait();

                    bikeTypes = readTask.Result;
                }
                else
                {
                    bikeTypes = Enumerable.Empty<BicycleTypeModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(bikeTypes);
        }

        [HttpPost]
        public ActionResult Paint(string bikeModel)
        {
            ViewBag.Bike = bikeModel;
            IEnumerable<PaintModel> paintTypes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("paint");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PaintModel>>();
                    readTask.Wait();

                    paintTypes = readTask.Result;
                }
                else
                {
                    paintTypes = Enumerable.Empty<PaintModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(paintTypes);
        }

        [HttpPost]
        public ActionResult Construction(string bikeModel, int paintType)
        {
            ViewBag.Bike = bikeModel;
            ViewBag.Paint = paintType;

            return View();
        }

        public ActionResult NewBicyclePurchase(string bikeModel, int paintType, string constructionType)
        {
            IEnumerable<BicycleViewModel> bikes = null;
            BicycleViewModel bike = new BicycleViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("bicycle");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BicycleViewModel>>();
                    readTask.Wait();

                    bikes = readTask.Result;
                }
                else
                {
                    bikes = Enumerable.Empty<BicycleViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }
            bikes.OrderByDescending(s => s.SERIALNUMBER);
            ViewBag.PK = bikes.Last().SERIALNUMBER + 1;
            ViewBag.Bike = bikeModel;
            ViewBag.Paint = paintType;
            ViewBag.Construction = constructionType;
            bike.SERIALNUMBER = ViewBag.PK;
            bike.CONSTRUCTION = ViewBag.Bike;
            bike.PAINTID = ViewBag.Paint;
            return View(bike);
        }

        [HttpPost]
        public ActionResult PostBicyclePurchase(BicycleViewModel bike)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/bicycle");

                var postTask = client.PostAsJsonAsync<BicycleViewModel>("bicycle", bike);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("About");
                }
            }
            ModelState.AddModelError(string.Empty, "Server error.");
            return View(bike);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is WonderWheels' SEII Bikeshop application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
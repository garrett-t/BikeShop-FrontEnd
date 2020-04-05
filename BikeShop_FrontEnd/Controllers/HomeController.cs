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
        //Loads modeltypes from database
        
        public ActionResult Index()
        {
            
            //bicycle = new BicycleViewModel();
            IEnumerable<BicycleTypeModel> bikeTypes = null;
            using (var client = new HttpClient())
            {
                //GET request to API for all modeltypes
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("modeltype");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BicycleTypeModel>>();
                    readTask.Wait();

                    //Read all modeltypes into IList
                    bikeTypes = readTask.Result;
                }
                else
                {
                    bikeTypes = Enumerable.Empty<BicycleTypeModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            //Pass IEnumerable of all bike models to view
            //ViewBag.Bike = new BicycleViewModel();
            //ViewBag.bike = bike;
            //Session["count"] = 1;
            return View(bikeTypes);
        }

        /*[HttpPost]
        public ActionResult Index(string bikeModel)
        {
            //bike.MODELTYPE = bikeModel;
            
            return RedirectToAction("Paint", bikeModel);
        }*/

        [HttpPost]
        public ActionResult Paint(string bikeModel)
        {
            IEnumerable<PaintModel> paintTypes = null;
            using (var client = new HttpClient())
            {
                //GET request to API for all paint types
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("paint");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PaintModel>>();
                    readTask.Wait();

                    //Read all paint types into IList
                    paintTypes = readTask.Result;
                }
                else
                {
                    paintTypes = Enumerable.Empty<PaintModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            ViewBag.bikeModel = bikeModel;
            return View(paintTypes);
        }

        [HttpPost]
        public ActionResult Construction(string bikeModel, int paintType)
        {
            ViewBag.bikeModel = bikeModel;
            ViewBag.paintType = paintType;
            return View();
        }

        //Select type of paint
        /*[HttpPost]
        public ActionResult Paint(string bikeModel, int paintType)
        {
            //Pass selected bike model into new view
            /*ViewBag.Bike = bikeModel;
            IEnumerable<PaintModel> paintTypes = null;
            using (var client = new HttpClient())
            {
                //GET request to API for all paint types
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                var responseTask = client.GetAsync("paint");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PaintModel>>();
                    readTask.Wait();

                    //Read all paint types into IList
                    paintTypes = readTask.Result;
                }
                else
                {
                    paintTypes = Enumerable.Empty<PaintModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            ViewBag.Paint = paintType;
            ViewBag.bikeModel = bikeModel;
            return View("Construction");
        }*/

        //Select construction type of bike
        /*[HttpPost]
        public ActionResult Construction(string bikeModel, int paintType, string construction)
        {
            /*ViewBag.Bike = bikeModel;
            ViewBag.Paint = paintType;
            Session["change"] = bikeModel;
            ViewBag.bikeModel = bikeModel;
            ViewBag
            return View("NewBicycle");
        }*/

        //Check that all previous info is correct
        //public ActionResult NewBicyclePurchase(string bikeModel, int paintType, string constructionType)
        public ActionResult NewBicycle(string bikeModel, int paintType, string constructionType)
        {
            /*IEnumerable<BicycleViewModel> bikes = null;
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
            //Order list of bicycles by serial number
            bikes.OrderByDescending(s => s.SERIALNUMBER);
            //Create new serial number for new record
            bike.SERIALNUMBER = bikes.Last().SERIALNUMBER + 1;
            bike.MODELTYPE = bikeModel;
            bike.CONSTRUCTION = constructionType;
            bike.PAINTID = paintType;
            return View(bike);*/
            BicycleViewModel bike = new BicycleViewModel();
            bike.MODELTYPE = bikeModel;
            bike.PAINTID = paintType;
            bike.CONSTRUCTION = constructionType;
            return View(bike);
        }

        //POST new bike
        [HttpPost]
        public ActionResult PostBicyclePurchase(BicycleViewModel bike)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/bicycle");
                //Send new bike as JSON POST request
                var postTask = client.PostAsJsonAsync<BicycleViewModel>("bicycle", bike);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //Return user to front page to create new bike order
                    return RedirectToAction("Index");
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
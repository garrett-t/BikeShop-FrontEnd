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
            //Pass all bicycle types to view
            return View(bikeTypes);
        }

        //Select the type of paint for the bicycle
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

        //Check that all previous info is correct
        public ActionResult NewBicyclePurchase(string bikeModel, int paintType, string constructionType)
        {
            BicycleViewModel bike = new BicycleViewModel();
            bike.MODELTYPE = bikeModel;
            bike.PAINTID = paintType;
            bike.CONSTRUCTION = constructionType;
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
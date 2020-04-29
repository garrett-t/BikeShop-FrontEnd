using BikeShop_FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BikeShop_FrontEnd.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateBikeType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBikeType(BicycleTypeModel modelType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/modeltype");

                var postTask = client.PostAsJsonAsync<BicycleTypeModel>("modelType", modelType);
                postTask.Wait();

                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator");
            return View(modelType);
        }
    }
}
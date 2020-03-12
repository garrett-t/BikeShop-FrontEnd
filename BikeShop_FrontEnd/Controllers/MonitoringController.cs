using BikeShop_FrontEnd.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BikeShop_FrontEnd.Controllers
{
    public class MonitoringController : Controller
    {
        // GET: Monitoring
        public ActionResult Index()
        {
            IEnumerable<Metrics> metrics = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dahkm.azurewebsites.net/api/");
                var responseTask = client.GetAsync("metricscontroller");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Metrics>>();
                    readTask.Wait();

                    metrics = readTask.Result;
                }
                else
                {
                    metrics = Enumerable.Empty<Metrics>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(metrics);
        }
    }
}
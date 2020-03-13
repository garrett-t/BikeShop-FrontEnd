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
        // GET: Monitoring team's API information
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Metrics metrics)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dahkm.azurewebsites.net/api/metricscontroller");

                var postTask = client.PostAsJsonAsync<Metrics>("metricscontroller", metrics);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server error.");
            return View(metrics);
        }

        public ActionResult Customers()
        {
            IEnumerable<Customer> customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dahkm.azurewebsites.net/api/");
                var responseTask = client.GetAsync("customercontroller");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customer>>();
                    readTask.Wait();

                    customers = readTask.Result;
                }
                else
                {
                    customers = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(customers);
        }
    }
}
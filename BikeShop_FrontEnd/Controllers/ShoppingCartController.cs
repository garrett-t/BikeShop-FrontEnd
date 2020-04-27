using BikeShop_FrontEnd.Models;
using BikeShop_FrontEnd.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BikeShop_FrontEnd.Controllers
{
    public class ShoppingCartController : Controller
    {
        // Once a bicycle order is completed, it is added to the shopping cart
        public ActionResult Add(BicycleViewModel bike)
        {
            if(Session["cart"]==null)
            {
                List<BicycleViewModel> bicycleList = new List<BicycleViewModel>();

                bicycleList.Add(bike);
                Session["cart"] = bicycleList;
                ViewBag.cart = bicycleList.Count();

                //Sets the number displayed on the shopping cart icon to be 1
                Session["count"] = 1;
            }
            else
            {
                List<BicycleViewModel> bicycleList = (List<BicycleViewModel>)Session["cart"];
                bicycleList.Add(bike);
                Session["cart"] = bicycleList;
                ViewBag.cart = bicycleList.Count();
                //Increases shopping cart icon number
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewShoppingCart()
        {
            //Displays contents of shopping cart to the user
            return View((List<BicycleViewModel>)Session["cart"]);
        }

        [Authorize]
        public ActionResult PurchaseItems()
        {
            List<BicycleViewModel> bicycleList = (List<BicycleViewModel>)Session["cart"];

            using (var client3 = new HttpClient())
            {
                //Send number of bikes purchased for metrics
                Transactions t = new Transactions();
                t.Quantity = bicycleList.Count;
                client3.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/transactions");
                var tPost = client3.PostAsJsonAsync<Transactions>("transactions", t);
                tPost.Wait();
            }

            foreach (var bike in bicycleList)
            {
                //Post each bike to API
                using (var client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/");
                    var responseTask = client1.GetAsync("bicycle");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<BicycleViewModel>();
                        readTask.Wait();

                        bike.SERIALNUMBER = readTask.Result.SERIALNUMBER + 1;
                    }
                }

                using (var client2 = new HttpClient())
                {
                    //Send type of bike for Metrics
                    BikeSalesType bst = new BikeSalesType();
                    bst.modelType = bike.MODELTYPE;
                    client2.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/bikesalestype");
                    var bstPost = client2.PostAsJsonAsync<BikeSalesType>("bikesalestype", bst);
                    bstPost.Wait();
                }


                using (var client4 = new HttpClient())
                {
                    client4.BaseAddress = new Uri("http://bikeshop-frontend.azurewebsites.net/api/bicycle");
                    var postTask = client4.PostAsJsonAsync<BicycleViewModel>("bicycle", bike);
                    postTask.Wait();


                    var res = postTask.Result;
                    if (!res.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "There was an error submitting the purchase request");
                        return View();
                    }
                }
            }
            
            Session["cart"] = new List<BicycleViewModel>();
            Session["count"] = 0;
            return RedirectToAction("Index", "Home");
        }
    }
}
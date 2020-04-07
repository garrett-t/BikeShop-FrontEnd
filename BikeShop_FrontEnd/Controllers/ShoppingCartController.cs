using BikeShop_FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
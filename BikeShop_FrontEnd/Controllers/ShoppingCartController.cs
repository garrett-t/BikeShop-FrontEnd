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
        // GET: ShoppingCart
        public ActionResult Add(BicycleViewModel bike)
        {
            if(Session["cart"]==null)
            {
                List<BicycleViewModel> bicycleList = new List<BicycleViewModel>();

                bicycleList.Add(bike);
                Session["cart"] = bike;
                ViewBag.cart = bicycleList.Count();

                Session["count"] = 1;
            }
            else
            {
                List<BicycleViewModel> bicycleList = (List<BicycleViewModel>)Session["cart"];
                bicycleList.Add(bike);
                Session["cart"] = bicycleList;
                ViewBag.cart = bicycleList.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 10;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewShoppingCart()
        {
            return View((List<BicycleViewModel>)Session["cart"]);
        }
    }
}
using BikeShop_FrontEnd.Models;
using BikeShop_FrontEnd.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BikeShop_FrontEnd.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (SE2Entities context = new SE2Entities())
            {
                //check whether attempted login is valid
                bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower()
                && user.UserPassword == model.Password);
                LoginAttempts la = new LoginAttempts();

                if (IsValidUser)
                {
                    la.UserName = model.UserName;
                    la.Successful = true;

                    //log this login attempt as successful
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://dahkm.azurewebsites.net/api/loginattempts");
                        var postTask = client.PostAsJsonAsync<LoginAttempts>("loginattempts", la);
                        postTask.Wait();
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                la.UserName = model.UserName;
                la.Successful = false;
                //log this login attempt as failed
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://dahkm.azurewebsites.net/api/loginattempts");
                    var postTask = client.PostAsJsonAsync<LoginAttempts>("loginattempts", la);
                    postTask.Wait();
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            //create new user account
            using (SE2Entities context = new SE2Entities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
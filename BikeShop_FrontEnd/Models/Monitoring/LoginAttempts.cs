using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeShop_FrontEnd.Models.Monitoring
{
    public class LoginAttempts
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool Successful { get;set; }
    }
}
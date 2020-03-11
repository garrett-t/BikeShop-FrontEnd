using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeShop_FrontEnd.Models
{
    public class BicycleTypeModel
    {
        [Key]
        public string MODELTYPE1 { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> COMPONENTID { get; set; }
    }
}
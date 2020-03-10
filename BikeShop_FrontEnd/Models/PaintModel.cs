using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeShop_FrontEnd.Models
{
    public class PaintModel
    {
        [Key]
        public int PAINTID { get; set; }
        public string COLORNAME { get; set; }
        public string COLORSTYLE { get; set; }
        public string COLORLIST { get; set; }
        public Nullable<System.DateTime> DATEINTRODUCED { get; set; }
        public Nullable<System.DateTime> DATEDISCONTINUED { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeShop_FrontEnd.Models
{
    public class BicycleViewModel
    {
        [Key]
        public int SERIALNUMBER { get; set; }
        public Nullable<int> CUSTOMERID { get; set; }
        public string MODELTYPE { get; set; }
        public Nullable<int> PAINTID { get; set; }
        public Nullable<int> FRAMESIZE { get; set; }
        public Nullable<System.DateTime> ORDERDATE { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> SHIPDATE { get; set; }
        public Nullable<int> SHIPEMPLOYEE { get; set; }
        public Nullable<decimal> FRAMEASSEMBLER { get; set; }
        public Nullable<decimal> PAINTER { get; set; }
        public string CONSTRUCTION { get; set; }
        public Nullable<decimal> WATERBOTTLEBRAZEONS { get; set; }
        public string CUSTOMNAME { get; set; }
        public string LETTERSTYLEID { get; set; }
        public Nullable<int> STOREID { get; set; }
        public Nullable<int> EMPLOYEEID { get; set; }
        public Nullable<int> TOPTUBE { get; set; }
        public Nullable<int> CHAINSTAY { get; set; }
        public Nullable<int> HEADTUBEANGLE { get; set; }
        public Nullable<int> SEATTUBEANGLE { get; set; }
        public Nullable<decimal> LISTPRICE { get; set; }
        public Nullable<decimal> SALEPRICE { get; set; }
        public Nullable<decimal> SALESTAX { get; set; }
        public string SALESTATE { get; set; }
        public Nullable<decimal> SHIPPRICE { get; set; }
        public Nullable<decimal> FRAMEPRICE { get; set; }
        public Nullable<decimal> COMPONENTLIST { get; set; }
    }
}
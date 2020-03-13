using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BikeShop_FrontEnd.Models.Monitoring
{
    //Current implementation of Monitoring Metrics model
    public class Metrics
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClicksAM { get; set; }
        public int ClicksAfternoon { get; set; }
        public int ClicksPM { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal ConversionRate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LWMS.ViewModels
{
    public class fooddiscountactives
    {

        public long foodid { get; set; }
        public string pic { get; set; }

        public string name { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> merchant_id { get; set; }
        public Nullable<decimal> discount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LWMS.ViewModels
{
    public class FoodcategoriesMerchant
    {
        public long id { get; set; }
        public string name { get; set; }
        public Nullable<int> sequence { get; set; }
       public string remark { get; set; }
        public string merchant_name { get; set; }
      // public Nullable<int> merchant_id { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
    }
}
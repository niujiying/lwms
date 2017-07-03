using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LWMS.ViewModels
{
    public class merchantext
    {
        public long id { get; set; }
       
        public string name { get; set; }
     
        public string pic { get; set; }
        public string notice { get; set; }   
        public Nullable<bool> join_full_reduce { get; set; }
        public Nullable<bool> join_discount { get; set; }
        public Nullable<bool> join_buy_gift { get; set; }
        public    bool join_full_reduce1 { get ; set; }
        public  bool  join_discount1 { get; set; }
        public  bool  join_buy_gift1 { get; set; }

    }
}
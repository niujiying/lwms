using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LWMS.ViewModels
{
    public class MerchantShopCost
    {
        //merchant shops  merchant_costs  investments(合伙人)
        // 美食城  商铺 保证金 交租金方式  租金/比例 物业费 合伙人 入驻时间 商户名称 店长
        public long id { get; set; }
        public string gourmetplaceName { get; set; }
        public string code { get; set; }//商铺编号

        // 保证金
        public Nullable<decimal> bond { get; set; }
        //交租金方式 
        public Nullable<int> rent_type { get; set; }
        public string rent_type1 { get; set; }
        //租金/比例 ???
        public Nullable<decimal> rent_money { get; set; }
        //    物业费  
        public Nullable<decimal> management_cost { get; set; }
        public string usernamae { get; set; }// 合伙人
        public Nullable<System.DateTime> enter_at { get; set; }
        public string name { get; set; }//商户名称
        public string shopowner { get; set; }//店长

    }
}
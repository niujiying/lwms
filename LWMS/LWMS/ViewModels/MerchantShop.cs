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

        public string gourmetplaceName { get; set; }
        public string code { get; set; }//商铺编号

        // 保证金
        public decimal bond { get; set; }
        //交租金方式 
        public int rent_type { get; set; }
        //租金/比例 ???
        //    物业费  
        public decimal management_cost { get; set; }
        public string usernamae { get; set; }// 合伙人
        public Nullable<System.DateTime> enter_at { get; set; }
        public string name { get; set; }
        public Nullable<int> shopowner { get; set; }

    }
}
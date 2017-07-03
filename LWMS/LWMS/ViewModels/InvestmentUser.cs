using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LWMS.ViewModels
{
    public class InvestmentUser
    {
        public Nullable<int> merchant_id { get; set; }
        
        public Nullable<decimal> invest_money { get; set; }//投资金额
        //public Nullable<int> bonus_type { get; set; }//分红方式
        public Nullable<decimal> bonus_ratio { get; set; }//分红比例
        //public Nullable<decimal> bonus_money { get; set; }//分红金额
        public Nullable<System.DateTime> invest_at { get; set; }//投资时间
        public string investname { get; set; }//合伙人
        public string mobile { get; set; }//手机号


    }
}
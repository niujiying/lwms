//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LWMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class foods
    {
        public long id { get; set; }
        public Nullable<int> merchant_id { get; set; }
        public string name { get; set; }
        public Nullable<int> sequence { get; set; }
        public string pic { get; set; }
        public Nullable<decimal> price { get; set; }
        public string desc { get; set; }
        public Nullable<int> approval_status { get; set; }
        public string reject_reason { get; set; }
        public Nullable<int> sale_status { get; set; }
        public Nullable<bool> join_discount { get; set; }
        public Nullable<bool> join_buy_gift { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    }
}

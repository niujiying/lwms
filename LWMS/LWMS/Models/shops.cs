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
    
    public partial class shops
    {
        public long id { get; set; }
        public Nullable<int> gourmet_palace_id { get; set; }
        public string code { get; set; }
        public string position { get; set; }
        public Nullable<decimal> area { get; set; }
        public Nullable<decimal> rent { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public Nullable<decimal> management_cost { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    }
}

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
    
    public partial class merchant_day_collects
    {
        public long id { get; set; }
        public Nullable<int> merchant_id { get; set; }
        public Nullable<int> order_count { get; set; }
        public Nullable<decimal> sale_money { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    }
}

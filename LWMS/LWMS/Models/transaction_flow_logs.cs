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
    
    public partial class transaction_flow_logs
    {
        public long id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string order_id { get; set; }
        public Nullable<System.DateTime> send_time { get; set; }
        public string send_cotent { get; set; }
        public string back_content { get; set; }
        public string resp_code { get; set; }
        public string resp_desc { get; set; }
        public Nullable<System.DateTime> resp_time { get; set; }
        public string ssn { get; set; }
        public string transaction_type { get; set; }
        public Nullable<decimal> transaction_amount { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
    }
}
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
    
    public partial class customers
    {
        public long id { get; set; }
        public string wechat_id { get; set; }
        public string tel { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public string remark { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public string nickname { get; set; }
        public string access_token { get; set; }
        public string password_digest { get; set; }
    }
}

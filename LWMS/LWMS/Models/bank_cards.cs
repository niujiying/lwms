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
    
    public partial class bank_cards
    {
        public long id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string bank { get; set; }
        public string card_num { get; set; }
        public Nullable<int> card_type { get; set; }
        public Nullable<bool> is_default { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
    }
}
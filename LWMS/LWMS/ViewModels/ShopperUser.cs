using LWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LWMS.ViewModels
{
    public class ShopperUser
    {
        private repast_developmentEntities db = new repast_developmentEntities();
        public long id { get; set; }
        public string login_name { get; set; }
        public string name { get; set; }
        public Nullable<int> created_user { get; set; }
        public System.DateTime created_at { get; set; }

        //角色的下拉列表框,显示所有的用户角色
        public string ListName { get; set; }
        // ??以后再改
        public IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList =( db.roles).Where(a=>a.role_type==1).Select(a => new SelectListItem
            {
                Text = a.name,
                Value = a.id.ToString()
            });
            return selectList;
        }
    }
}
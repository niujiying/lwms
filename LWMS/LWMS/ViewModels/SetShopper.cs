using LWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LWMS.ViewModels
{
    public class SetShopper
    {
        public long id { get; set; }//merchantid
        public string gourmetplaceName { get; set; }
        public string code { get; set; }//商铺编号

        private repast_developmentEntities db = new repast_developmentEntities();
        //下拉列表框绑定很麻烦
        public string ListName { get; set; }
        // ??以后再改
        public IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = db.users.Select(a => new SelectListItem
            {
                Text = a.name,
                Value = a.id.ToString()
            });
            return selectList;
        }
    }
}
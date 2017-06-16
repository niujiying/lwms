using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;


namespace LWMS.ViewModels
{
    public class FoodsMerchant
    {
        //编号 名称 分类 排序号 价格 审批状态 上下架状态 描述  创建时间 创建人
        public long id { get; set; }       
        public string name { get; set; }
        public string categoryname { get; set; }
        public Nullable<int> sequence { get; set; }
        //public string pic { get; set; }
        public Nullable<decimal> price { get; set; }
        public string desc { get; set; }
        public Nullable<int> approval_status { get; set; }
        public string approval_status1 { get; set; }
        public string reject_reason { get; set; }
        public Nullable<int> sale_status { get; set; }
        public string sale_status1 { get; set; }
        public Nullable<bool> join_discount { get; set; }
        public Nullable<bool> join_buy_gift { get; set; }
        public string merchantname { get; set; }
        //public Nullable<int> merchant_id { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }

        private repast_developmentEntities db = new repast_developmentEntities();

        //下拉列表框绑定很麻烦
        public string ListName { get; set; }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = db.food_categories.Select(a => new SelectListItem
            {
                Text = a.name,
                Value = a.id.ToString()
            });
            return selectList;
        }
    }
}
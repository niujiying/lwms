using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;
using LWMS.ViewModels;

namespace LWMS.Controllers
{
    public class DDLModel
    {
        private repast_developmentEntities db = new repast_developmentEntities();
         public string ListName { get; set; }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = db.food_categories.Select(a => new SelectListItem
            {
                Text=a.name,
                Value=a.id.ToString()
            });
            return selectList;
        }
    }
    public class foodsController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();
        
        // GET: foods
        public ActionResult Index()
        {
            var catenamelist = (from n in db.foods_food_categories
                            join b in db.foods on (long)n.food_id equals b.id
                            join s in db.food_categories on (long)n.food_category_id equals s.id                           
                            select new
                            {
                              catenme=s.name,
                                foodid=n.food_id
                            });
            var foodmer = from mm in db.merchants
                          join fo in db.foods on mm.id equals (long)fo.merchant_id
                          select new FoodsMerchant
                          {
                              id = fo.id,
                              name = fo.name,
                              sequence = fo.sequence,
                              price = fo.price,
                              desc = fo.desc,
                              approval_status = fo.approval_status,
                              approval_status1 = (fo.approval_status==0)?"待审批":(fo.approval_status == 1?"审批通过":"审批驳回"),
                              reject_reason = fo.reject_reason,
                              sale_status = fo.sale_status,
                              sale_status1 = (fo.sale_status>0) ? "上架": "下架",
                              join_discount = fo.join_discount,
                              join_buy_gift = fo.join_buy_gift,
                              merchantname = mm.name,
                              categoryname = catenamelist.Where(cn=>cn.foodid==fo.id).Select(cn=>cn.catenme).FirstOrDefault(),
                              created_at = fo.created_at,
                              updated_at = fo.updated_at,
                          };


            //from fc in db.food_categories where (fc.id ==
            //              (long) (from ffc in db.foods_food_categories where ((long)ffc.food_id == fo.id) let ffcid=ffc.food_category_id select ffcid)
            //              select fc ).name,

            // public Nullable<int> merchant_id { get; set; }

            //foreach (var item in foodmer)
            //{
            //    if (item.approval_status == 0)
            //    {
            //        item.approval_status1 = "待审批";
            //    }
            //    else
            //    {
            //        if (item.approval_status == 1)
            //        {
            //            item.approval_status1 = "审批通过";
            //        }
            //        else
            //        {
            //            item.approval_status1 = "审批驳回";
            //        }
            //    }
            //    if (item.sale_status == 0)
            //    {
            //        item.sale_status1 = "下架";
            //    }
            //    else
            //    {
            //        item.sale_status1 = "上架";
            //    }

            //}
            
            return View(foodmer);
        }

        [HttpPost]
        public ActionResult Index(string foodname)
        {
            //查找
            var foo = from m in db.foods
                         select m;

            if (!String.IsNullOrEmpty(foodname))
            {
                foo = foo.Where(s => s.name.Contains(foodname));
            }
            var foodmer = from fo in foo

                          select new FoodsMerchant
                          {
                              id = fo.id,
                              name = fo.name,
                              sequence = fo.sequence,
                              price = fo.price,
                              desc = fo.desc,
                              approval_status = fo.approval_status,
                              approval_status1 = (fo.approval_status == 0) ? "待审批" : (fo.approval_status == 1 ? "审批通过" : "审批驳回"),
                              reject_reason = fo.reject_reason,
                              sale_status = fo.sale_status,
                              sale_status1 = (fo.sale_status > 0) ? "上架" : "下架",
                              join_discount = fo.join_discount,
                              join_buy_gift = fo.join_buy_gift,
                              merchantname =   (from ch in db.merchants where(ch.id ==(long)fo.merchant_id)select ( ch.name).FirstOrDefault()).ToString(),


                              categoryname =  
                             (from ff in db.foods_food_categories
                              join fc in db.food_categories on (long)ff.food_category_id  equals fc.id where ff.food_id==fo.id select fc.name).ToString(),                           
                            
                              created_at = fo.created_at,
                              updated_at = fo.updated_at,
                          };

            //查找必须返回FoodsMerchant类型
            return View("Index", foodmer);
        }

        // GET: foods/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foods foods = db.foods.Find(id);
            if (foods == null)
            {
                return HttpNotFound();
            }
            return View(foods);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "insert")]
        public ActionResult insert()
        {
            //?? .Where(s=>s.merchant_id==1 )此商户的菜品种类
            ViewData["AreId"] = from n in db.foods_food_categories
                                join b in db.food_categories on (long)n.food_category_id equals b.id
                                select new SelectListItem
                                {
                                    Text = n.food_id.ToString(),
                                    Value = b.name.ToString()
                                };

            return View("Create",new FoodsMerchant());
        }
        //试图 session["username"]

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "up")]
        public ActionResult up(foods mm)
        {
            //设置上架
            foods foods = db.foods.Find(mm.id);
            foods.sale_status = 1;
            db.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "down")]
        public ActionResult down(foods mm)
        {  //设置上架
            foods foods = db.foods.Find(mm.id);
            foods.sale_status = 0;
            db.SaveChanges();
            return View("Index");
        }

        // GET: foods/Create
        public ActionResult Create()
        {
          
            
          //  SelectList list = new SelectList(data, "FoodID", "CategoryName");
           // ViewBag.Roles = data;

            //model.Categories = db.CategoryModels.Where(c => c.SectionId == sectionId && c.Active == true)
            //    .OrderBy(ct => ct.CategoryName).ToList().Select
            //    (x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();

            return View();
        }

        // POST: foods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,merchant_id,name,sequence,pic,price,desc,approval_status,reject_reason,sale_status,join_discount,join_buy_gift,created_at,updated_at")] foods foods)
        {
            if (ModelState.IsValid)
            {
                db.foods.Add(foods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foods);
        }

        // GET: foods/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foods foods = db.foods.Find(id);
            if (foods == null)
            {
                return HttpNotFound();
            }
            return View(foods);
        }

        // POST: foods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,merchant_id,name,sequence,pic,price,desc,approval_status,reject_reason,sale_status,join_discount,join_buy_gift,created_at,updated_at")] foods foods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foods).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foods);
        }

        // GET: foods/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foods foods = db.foods.Find(id);
            if (foods == null)
            {
                return HttpNotFound();
            }
            return View(foods);
        }

        // POST: foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            foods foods = db.foods.Find(id);
            db.foods.Remove(foods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

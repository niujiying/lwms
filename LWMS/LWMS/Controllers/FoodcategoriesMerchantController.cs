using LWMS.Models;
using LWMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LWMS.Controllers
{
    public class FoodcategoriesMerchantController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();
        // GET: FoodcategoriesMerchant
        public ActionResult Index()
        {
            var mernameList = from mm in db.merchants
                              join fc in db.food_categories on mm.id equals (long)fc.merchant_id
                              select new FoodcategoriesMerchant
                              {
                                  id = fc.id,
                                  name = fc.name,
                                  sequence = fc.sequence,
                                  remark = fc.remark,
                                  merchant_name = mm.name,
                                  // public Nullable<int> merchant_id { get; set; }
                                  created_at = fc.created_at,
                                  updated_at = fc.updated_at,
                              };
//; IQueryable<FoodcategoriesMerchant> mernameList = from ff in db.food_categories
//                    select new FoodcategoriesMerchant
//                    {
//                        id = ff.id,
//                        name = ff.name,
//                        sequence = ff.sequence,
//                        remark = ff.remark,
//                        merchant_name =  db.merchants.Find(ff.id).name,
//                        // public Nullable<int> merchant_id { get; set; }
//                        created_at = ff.created_at,
//                        updated_at = ff.updated_at
//                    };

           // ViewData["merchantname"] = ;
            return View(mernameList );          
        }

        // GET: food_categories/Create
        public ActionResult Create()
        {
            var sequenccount = db.food_categories.Count();
            sequenccount = (int)sequenccount * 10;
            
            //？？
            ViewData["sequenccount"] = sequenccount;
            return View("Create");
        }

        // POST: food_categories/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,sequence,remark,merchant_id,created_at,updated_at")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                

                //Request["created_at"]= DateTime.Now;
                //var age = Request["age"];
                //var btn = Request["button"];
                food_categories.created_at = DateTime.Now;
                food_categories.updated_at = DateTime.Now;
                //？？当前的登陆名称，商户
                food_categories.merchant_id = 1;
                db.food_categories.Add(food_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food_categories);
        }

        // GET: food_categories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // POST: food_categories/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,sequence,remark,merchant_id,created_at,updated_at")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                //food_categories.updated_at = DateTime.Now;
                db.Entry(food_categories).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food_categories);
        }

        // GET: food_categories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }
        // POST: food_categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            food_categories food_categories = db.food_categories.Find(id);
            db.food_categories.Remove(food_categories);
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
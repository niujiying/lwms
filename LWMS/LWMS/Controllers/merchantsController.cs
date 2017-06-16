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
    public class merchantsController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: merchants
        public ActionResult Index()
        {
            //investments users, merchant_costs
            var userRoleList = from mm in db.merchants
                               join sh in db.shops on (long)mm.shop_id equals sh.id
                               join mc in db.merchant_costs on mm.id equals (long)mc.merchant_id
                               //where mm.ID == 1

                               select new MerchantShopCost
                               {
                                   id=mm.id,
                                   gourmetplaceName = mm.name,//有问题
                                   code = sh.code,//商铺编号
                                   bond=mc.bond,//保证金
                                   rent_type=mc.rent_type,//交租金方式
                                   rent_money=sh.rent,//租金/比例
                                                      //别的没有赋值合伙人???
                                   usernamae = (from inv in db.investments
                                                join u in db.users on (long)inv.user_id equals u.id
                                                where inv.merchant_id == mm.id
                                                select u.name).FirstOrDefault(),

                                   management_cost = mc.management_cost, //    物业费  

                                    enter_at = mm.enter_at,
                                   name = mm.name,//商户名称
                                   shopowner = mm.shopowner,//店长

                       // 

                                  
       
    };
            return View(userRoleList);
        }

        // GET: merchants/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchants merchants = db.merchants.Find(id);
            if (merchants == null)
            {
                return HttpNotFound();
            }
            return View(merchants);
        }

        // GET: merchants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: merchants/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,gourmet_palace_id,shop_id,boss,shopowner,name,enter_at,invest_status,pic,notice,qrcode,flavor,service,total_score,join_full_reduce,join_discount,join_buy_gift,created_at,updated_at")] merchants merchants)
        {
            if (ModelState.IsValid)
            {
                db.merchants.Add(merchants);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchants);
        }

        // GET: merchants/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchants merchants = db.merchants.Find(id);
            if (merchants == null)
            {
                return HttpNotFound();
            }
            return View(merchants);
        }

        // POST: merchants/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,gourmet_palace_id,shop_id,boss,shopowner,name,enter_at,invest_status,pic,notice,qrcode,flavor,service,total_score,join_full_reduce,join_discount,join_buy_gift,created_at,updated_at")] merchants merchants)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchants).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchants);
        }

        // GET: merchants/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchants merchants = db.merchants.Find(id);
            if (merchants == null)
            {
                return HttpNotFound();
            }
            return View(merchants);
        }

        // POST: merchants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            merchants merchants = db.merchants.Find(id);
            db.merchants.Remove(merchants);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;

namespace LWMS.Controllers
{
    public class buy_gift_activesController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: buy_gift_actives
        public ActionResult Index()
        {
            return View(db.buy_gift_actives.ToList());
        }

        // GET: buy_gift_actives/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_gift_actives buy_gift_actives = db.buy_gift_actives.Find(id);
            if (buy_gift_actives == null)
            {
                return HttpNotFound();
            }
            return View(buy_gift_actives);
        }

        // GET: buy_gift_actives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: buy_gift_actives/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,merchant_id,content,food_id,created_at,updated_at")] buy_gift_actives buy_gift_actives)
        {
            if (ModelState.IsValid)
            {
                db.buy_gift_actives.Add(buy_gift_actives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buy_gift_actives);
        }

        // GET: buy_gift_actives/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_gift_actives buy_gift_actives = db.buy_gift_actives.Find(id);
            if (buy_gift_actives == null)
            {
                return HttpNotFound();
            }
            return View(buy_gift_actives);
        }

        // POST: buy_gift_actives/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,merchant_id,content,food_id,created_at,updated_at")] buy_gift_actives buy_gift_actives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy_gift_actives).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buy_gift_actives);
        }

        // GET: buy_gift_actives/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_gift_actives buy_gift_actives = db.buy_gift_actives.Find(id);
            if (buy_gift_actives == null)
            {
                return HttpNotFound();
            }
            return View(buy_gift_actives);
        }

        // POST: buy_gift_actives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            buy_gift_actives buy_gift_actives = db.buy_gift_actives.Find(id);
            db.buy_gift_actives.Remove(buy_gift_actives);
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

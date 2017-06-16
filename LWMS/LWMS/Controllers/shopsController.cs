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
    public class shopsController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: shops
        public ActionResult Index()
        {
            return View(db.shops.ToList());
        }

        // GET: shops/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shops shops = db.shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // GET: shops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: shops/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,gourmet_palace_id,code,position,area,rent,created_at,updated_at")] shops shops)
        {
            if (ModelState.IsValid)
            {
                db.shops.Add(shops);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shops);
        }

        // GET: shops/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shops shops = db.shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // POST: shops/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,gourmet_palace_id,code,position,area,rent,created_at,updated_at")] shops shops)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shops).State = System.Data .Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shops);
        }

        // GET: shops/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shops shops = db.shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // POST: shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            shops shops = db.shops.Find(id);
            db.shops.Remove(shops);
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

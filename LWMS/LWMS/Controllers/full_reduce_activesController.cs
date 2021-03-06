﻿using System;
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
    public class full_reduce_activesController : Controller
    {
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: full_reduce_actives
        public ActionResult Index()
        {
            return View(db.full_reduce_actives.Where(f => f.merchant_id ==(long) Session["sesMerchantID"]));
        }

        [HttpPost]
        public ActionResult savefull(FormCollection fc,int[] full,int[] reduce)
        {
            int fcount = full.Count();
             Response.Write("<script>alert('更新成功!'+fcount.ToString());</script>");
            //删除所有的，重新添加??merchant_id=1
            var fra = from f in db.full_reduce_actives where f.merchant_id==270 select f;
            foreach (var item in fra)
            {
                db.full_reduce_actives.Remove(item);
            }
            db.SaveChanges();
           
            if (full.Count() > 0)
            {
                for (int i = 0; i < full.Count(); i++)
                {                   
                    full_reduce_actives fb = new full_reduce_actives();
                    fb.full = full[i];
                    fb.reduce = reduce[i];
                    fb.merchant_id = 270;
                    fb.created_at = DateTime.Now;
                    fb.updated_at = DateTime.Now;
                    db.full_reduce_actives.Add(fb);
                } 
            }
            db.SaveChanges();
            var fu = db.full_reduce_actives.Where(f => f.merchant_id == (long)Session["sesMerchantID"]);
            return View("index",fu);
        }
        // GET: full_reduce_actives/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            full_reduce_actives full_reduce_actives = db.full_reduce_actives.Find(id);
            if (full_reduce_actives == null)
            {
                return HttpNotFound();
            }
            return View(full_reduce_actives);
        }

        // GET: full_reduce_actives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: full_reduce_actives/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,merchant_id,full,reduce,created_at,updated_at")] full_reduce_actives full_reduce_actives)
        {
            if (ModelState.IsValid)
            {
                db.full_reduce_actives.Add(full_reduce_actives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(full_reduce_actives);
        }

        // GET: full_reduce_actives/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            full_reduce_actives full_reduce_actives = db.full_reduce_actives.Find(id);
            if (full_reduce_actives == null)
            {
                return HttpNotFound();
            }
            return View(full_reduce_actives);
        }

        // POST: full_reduce_actives/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,merchant_id,full,reduce,created_at,updated_at")] full_reduce_actives full_reduce_actives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(full_reduce_actives).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(full_reduce_actives);
        }

        // GET: full_reduce_actives/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            full_reduce_actives full_reduce_actives = db.full_reduce_actives.Find(id);
            if (full_reduce_actives == null)
            {
                return HttpNotFound();
            }
            return View(full_reduce_actives);
        }

        // POST: full_reduce_actives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            full_reduce_actives full_reduce_actives = db.full_reduce_actives.Find(id);
            db.full_reduce_actives.Remove(full_reduce_actives);
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

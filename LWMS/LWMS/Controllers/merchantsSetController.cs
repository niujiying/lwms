using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;
using System.IO;
using LWMS.ViewModels;

namespace LWMS.Controllers
{    
    public class merchantsSetController : Controller
    {
       
        private repast_developmentEntities db = new repast_developmentEntities();
       
        // GET: merchantsSet
        public ActionResult Index()
        {
      
            return View(getAllMerchant());
        }

        public merchantext getAllMerchant()
        {
            var mer = db.merchants.Find((long)Session["sesMerchantID"]);
            var merext = new merchantext
            {
                id = mer.id,
                name = mer.name,
                pic = mer.pic,
                notice = mer.notice,
                join_full_reduce1 = (mer.join_full_reduce == null) ? false : ((bool)mer.join_full_reduce ? true : false),
                join_discount1 = (mer.join_discount == null) ? false : ((bool)mer.join_discount ? true : false),
                join_buy_gift1 = (mer.join_buy_gift == null) ? false : ((bool)mer.join_buy_gift ? true : false),
            };
            return merext;

        }

        /// <summary>
        /// 提交方法
        /// </summary>
        /// <param name="tm">模型数据</param>
        /// <param name="file">上传的文件对象，此处的参数名称要与View中的上传标签名称相同</param>
        /// <returns></returns>     
       [HttpPost]
        [MultipleButton(Name = "action", Argument = "upload")]
        public ActionResult upload(merchantext ext, HttpPostedFileBase image)
        {
            var mer = db.merchants.Find((long)Session["sesMerchantID"]);
            var merext = new merchantext
            {
                id = mer.id,
                name = mer.name,
                pic = mer.pic,
                notice = mer.notice,
                join_full_reduce1 = (mer.join_full_reduce == null) ? false : ((bool)mer.join_full_reduce ? true : false),
                join_discount1 = (mer.join_discount == null) ? false : ((bool)mer.join_discount ? true : false),
                join_buy_gift1 = (mer.join_buy_gift == null) ? false : ((bool)mer.join_buy_gift ? true : false),
            };
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Upload"), fileName);
                    image.SaveAs(filePath);
                    mer.pic = "~/Upload/" + fileName;
                    db.SaveChanges();
                    merext.pic = mer.pic;
                   
                }
                else
                {
                    return Content("没有文件！", "text/plain");
                }
                return RedirectToAction("Index", merext);
                
            }
            catch
            {
                return Content("上传异常 ！", "text/plain");
            }
            
            //var fileName = Path.Combine(Request.MapPath("~/Upload"), Path.GetFileName(file.FileName));
            //try
            //{
            //    File.SaveAs(fileName);
            //    //tm.AttachmentPath = fileName;//得到全部model信息
            //    tm.pic = "../upload/" + Path.GetFileName(File.FileName);
            //    //return Content("上传成功！", "text/plain");
            //    return RedirectToAction("Show", tm);
            //}
            //catch
            //{
            //    return Content("上传异常 ！", "text/plain");
            //}
        }
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult save(merchantext ext, HttpPostedFileBase file_input)
        {
            var ses =Convert.ToInt64( Session["sesMerchantID"]) ;
            var mer = db.merchants.Find(ses );
                 
            try
            {
                if (file_input != null && file_input.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Path.GetFileName(file_input.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Upload"), fileName);
                    file_input.SaveAs(filePath);
                    mer.pic = "~/Upload/" + fileName;
                  
                    mer.name = ext.name;
                   
                    mer.notice = ext.notice;
                    mer.join_full_reduce = (ext.join_full_reduce1);
                    mer.join_discount = (ext.join_discount1);
                    mer.join_buy_gift = (ext.join_buy_gift1);
                    db.SaveChanges();
                    return RedirectToAction("Index", getAllMerchant());

                }
                else
                {
                    return Content("没有文件！", "text/plain");
                }
            

            }
            catch
            {
                return Content("上传异常 ！", "text/plain");
            }
           
        }
        // GET: merchantsSet/Details/5
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

        // GET: merchantsSet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: merchantsSet/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,gourmet_palace_id,shop_id,boss,shopowner,name,enter_at,invest_status,pic,notice,qrcode,flavor,service,total_score,join_full_reduce,join_discount,join_buy_gift,created_at,updated_at,kitchen,order_count")] merchants merchants)
        {
            if (ModelState.IsValid)
            {
                db.merchants.Add(merchants);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchants);
        }

        // GET: merchantsSet/Edit/5
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

        // POST: merchantsSet/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,gourmet_palace_id,shop_id,boss,shopowner,name,enter_at,invest_status,pic,notice,qrcode,flavor,service,total_score,join_full_reduce,join_discount,join_buy_gift,created_at,updated_at,kitchen,order_count")] merchants merchants)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchants).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchants);
        }

        // GET: merchantsSet/Delete/5
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

        // POST: merchantsSet/Delete/5
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

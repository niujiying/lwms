using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;
using System.Collections;

namespace LWMS.Controllers
{
    using LWMS.Models;
    using ViewModels;

    public class LoginController : Controller
    {
        repast_developmentEntities db = new repast_developmentEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        long merid = 0;
        //[ValidateAntiForgeryToken]
        public merchantext getAllMerchant()
        {
            var ses = (long)(Session["sesMerchantID"]);
            var mer = db.merchants.Find(ses);
            
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

        [HttpPost]     
        public ActionResult Denglu([Bind(Include = "login_name, password_digest")] users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                   
                    //var use= db.users.Where(u => u.login_name == user.login_name);
                    //??加上  &&encrypted_password==user.encrypted_password
                    var use = from u in db.users where(u.login_name == user.login_name) select u;

                    if (use.Count()>0)
                    {
                        Session["sesMerchantName"] = user.login_name;
                        long merchantid = (from u in db.merchants where (u.name == user.login_name) select u.id).First()  ;
                        Session["sesMerchantID"] = merchantid;
                         //merid = Convert.ToInt64( merchantid );
                        //RedirectToAction
                        return View("../MerchantsSet/Index",getAllMerchant());
                    }
                    else
                    {
                        return View("Index");
                    }
                    return RedirectToAction("HomePage");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("Fail To Login", "Fail To Login.Try again, and if the problem persists see your system administrator.");
            }
            return View("Index");
        }
    }
}
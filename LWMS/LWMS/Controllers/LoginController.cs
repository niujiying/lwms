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
    public class LoginController : Controller
    {
        repast_developmentEntities db = new repast_developmentEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]


        [HttpPost]     
        public ActionResult Denglu([Bind(Include = "login_name, password_digest")] users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.users.Add(user);
                    //db.SaveChanges(); 
                    // users  worker = db.users.Find(id);
                    //if (worker == null)
                    //    {
                    //        return HttpNotFound();
                    //    }
                    //    users use = db.users.Find(1);
                    IQueryable< users> use= db.users.Where(u => u.login_name == user.login_name);
                                        
                    if (use.Count<users>()>0)
                    {
                        orders order = db.orders.Find(28);//RedirectToAction
                        return View("HomePage");
                    }
                    else
                    {
                        return View();
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
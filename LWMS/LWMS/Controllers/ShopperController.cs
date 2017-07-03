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
    public class ShopperController : Controller
    {
        //password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
        private repast_developmentEntities db = new repast_developmentEntities();

        public void getList()
        {
            var data = from r in db.roles
                       where (r.role_type == 1)
                       select new
                       {
                           roleName = r.name,
                           roleid = r.id
                       };
             SelectList list = new SelectList(data, "roleid", "rolename");
           // ViewBag.StoreId = new SelectList(data, "roleid", "rolename");
            ViewBag.Roles = list;
            //ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "Name", defaultStoreId);
        }

        // GET: Shopper
        public ActionResult Index()
        {
            getList();
            var ushop = from u in db.users
                        select new ShopperUser
                        {
                            id = u.id,
                            login_name = u.login_name,
                            name = u.name,
                            created_at = u.created_at,
                            created_user = u.created_user,
                        }; ;
            return View(ushop);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "insert")]
        public ActionResult insert()
        {
            return View("Create");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "setpassword")]
        public ActionResult setpassword(ShopperUser su)
        {
            //?? .Where(s=>s.merchant_id==1 )此商户的菜品种类
            if (su == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //设置密码123 不知道加密是怎么弄的
            users u = db.users.Find(su.id);
            if (u == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            u.encrypted_password = "123";
            db.SaveChanges();
            return View("Index");  
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "search")]
        public ActionResult search(ShopperUser su,string login_name,string name,string Roles)
        {
            //账号姓名角色
            users us = null;
            bool bexp = false;
            var u = from w in db.users
                          select w;
            if (!string.IsNullOrEmpty(login_name))
            {
                u = u.Where(w => w.login_name == login_name);
            }
            if (!string.IsNullOrEmpty(name))
            {
                u = u.Where(w => w.name == name);
            }
            if (!string.IsNullOrEmpty(Roles))
            {
                getList();
                var uid = (from w in db.role_users where (w.role_id==Convert.ToInt32(Roles))
                        select w.user_id);
                //List<int> list = new ArrayList<int>();
                //int[] a = new int[list.size()];
                //for (int i = 0; i < list.size(); i++)
                //{
                //    a[i] = list.get(i);
                //}

                List<int> uidint = new List<int>();
                if ((uid != null) && (uid.Count() > 0))
                {
                    foreach (int item in uid)
                    {                         
                       uidint.Add(item);                        
                    }
                    u = u.Where(w => (uidint.ToArray()).Contains( (int)w.id));
                }
                else
                {
                    return View("Index",null);
                }               
            }


            // us=  (from s in db.users where ((s.login_name == login_name) && (s.name == name)) select s).FirstOrDefault();
            //if ((su.login_name!=string.Empty))
            //    {
            //        us = (from s in db.users where ((s.login_name == su.login_name)) select s).FirstOrDefault();
            //    }
            //    else
            //    {
            //        us = (from s in db.users where ((s.name == su.name )) select s).FirstOrDefault();
            //    }
           
            var shop =  from use in u select 
            new ShopperUser{
                id = use.id,
                login_name = use.login_name,
                name = use.name,
                created_at = use.created_at,
                created_user = use.created_user,
               
            };
            getList();
            return View("Index",shop);
        }

        // GET: Shopper/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Shopper/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Shopper/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,login_name,name,photo,created_user,created_at,updated_at,encrypted_password,remember_created_at,sign_in_count,current_sign_in_at,last_sign_in_at")] users users)
        {
            //得到当前的model 设置店长  角色中修改，商户中修改 
            if (ModelState.IsValid)
            {
                users us =( from u in db.users where ((u.login_name == users.login_name) &&(u.name==users.name)) select u).FirstOrDefault() ;
            role_users r = (from ru in db.role_users where (ru.user_id == us.id) select ru).FirstOrDefault();
            //找到userID 设置roleid，merchants的店长加上
            //  r.role_id = 0;
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Shopper/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Shopper/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,login_name,name,photo,created_user,created_at,updated_at,encrypted_password,remember_created_at,sign_in_count,current_sign_in_at,last_sign_in_at")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Shopper/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Shopper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
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

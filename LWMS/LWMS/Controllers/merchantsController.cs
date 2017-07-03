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
        enum RentType
        {
            //0全付，1分期，2营业额分成
           全付 ,
           分期 ,
           营业额分成  //利润分成    profitsharing        
        };
      
        private repast_developmentEntities db = new repast_developmentEntities();
        public IQueryable<MerchantShopCost> getMerchantcost()
        {
            //数据为空出现错误
            var sesid = (long)Session["sesMerchantID"];
            //相同的老板 王五
            var boss = (from mm in db.merchants
                        where mm.id ==sesid  select mm.boss).First();
            //investments users, merchant_costs
            var mershopcost = from mm in db.merchants
                              join sh in db.shops on (long)mm.shop_id equals sh.id
                              join mc in db.merchant_costs on mm.id equals (long)mc.merchant_id
                              where mm.boss == boss
                           //??Session

                              select new MerchantShopCost
                              {
                                  id = mm.id,//merchants id
                                  gourmetplaceName = (from gp in db.gourmet_palaces where gp.id == (long)mm.gourmet_palace_id select gp.name).FirstOrDefault(),//有问题
                                  code = sh.code,//商铺编号
                                  bond = mc.bond,//保证金
                                  rent_type = mc.rent_type,//交租金方式
                                                           //rent_type1=Enum.GetNames (typeof(RentType))[(int)mc.rent_type],//交租金方式
                                  rent_type1 = ((RentType)mc.rent_type).ToString(),
                                  rent_money = sh.rent,//租金/比例
                                                       //别的没有赋值合伙人???
                                  usernamae = (from inv in db.investments
                                               join u in db.users on (long)inv.user_id equals u.id
                                               where inv.merchant_id == mm.id
                                               select u.name).FirstOrDefault(),

                                  management_cost = mc.management_cost, //    物业费  
                                  enter_at = mm.enter_at,
                                  name = mm.name,//商户名称
                                  shopowner =(from u in db.users where (u.id == (long)mm.shopowner) select u.name).FirstOrDefault() //店长 字符串 数据库里是整型
                                                                                                                                      // ??有问题
                               };
            //foreach (var item in mershopcost)
            //{
            //    if (item.shopowner==null)
            //    {
            //        item.shopowner=item.
            //    }
            //}           
            return mershopcost;

        }

        // GET: merchants
        public ActionResult Index()
        {
            //investments users, merchant_costs
            var mershopcost = getMerchantcost();
            return View(mershopcost);
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


        // GET: merchants/ShowInvest ok
        public ActionResult ShowInvestment(long? id)
        {
            if (id==null)
            {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invests = from v in db.investments
                          join u in db.users on (long)v.user_id equals u.id
                          where (long)v.merchant_id == id
                          select new InvestmentUser
                          {
                              merchant_id=v.merchant_id,
                              invest_money = v.invest_money,//投资金额
                              bonus_ratio = v.bonus_ratio, //分红比例
                              invest_at = v.invest_at,//投资时间
                              investname = u.name,//合伙人
                              mobile = u.mobile  //手机号  ??以后再改
                          }; 
            return View(invests);
        }

        //取店长时从用户users表里查创建人是当前账号，并且关联角色类型为1（商户），角色为店长的用户

        // GET: merchants/ShowInvest ok
        public ActionResult SetShopowner(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //有问题
            var mer = db.merchants.Find(id);
            var sshop = new SetShopper
            {
                id = (long)id,
                code = (from s in  db.shops  
                        where s.id == mer.shop_id
                        select s.code).FirstOrDefault(),
                gourmetplaceName = (from g in  db.gourmet_palaces  
                                    where g.id == mer.gourmet_palace_id
                                    select g.name).FirstOrDefault()
            };
            ViewData["code"] = sshop.code;
            ViewData["gourmetplaceName"] = sshop.gourmetplaceName;
            ViewData["merchantid"] = sshop.id;

            return View("SetShopowner",sshop);
        }

        [HttpPost]
        public ActionResult SaveShopowner(SetShopper shop,string gourmetplaceName,string code,long ListName)
        {
            //当前的商户 
           long merchantid = (from p in db.merchants where (p.name == Session["sesMerchantName"]) select p.id).First();
            var gourmentplaceid = (from p in db.gourmet_palaces where (p.name == gourmetplaceName) select p.id).FirstOrDefault();
            var shid= (from p in db.shops where (p.code == code) select p.id).FirstOrDefault();
            //var userid = (from p in db.users where (p.id == (long)ListName) select p.id).FirstOrDefault();
            var userid = ListName;
            var mer = (from m in db.merchants where (m.id == merchantid) select m).FirstOrDefault();
            mer.gourmet_palace_id = (int)gourmentplaceid;
            mer.shop_id =(int) shid;
            mer.shopowner =(int) userid;
            db.Entry(mer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            // RedirectToAction("Index");
            return View("Index", getMerchantcost());
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

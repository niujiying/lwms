using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LWMS.Models;
using System.Web.UI.WebControls;

namespace LWMS.Controllers
{
    public class buy_gift_activesController : Controller
    {
        public static long[] savefoodid;
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: buy_gift_actives
        public ActionResult Index()
        {
          
            return View();
        }

        //保存设置
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "saveset")]
        public ActionResult saveset([Bind(Include = "id,merchant_id,content,food_id,created_at,updated_at")] buy_gift_actives buy_gift_actives,string[] pic,string[] name,long[] idlist,decimal[] price,string content,string giftfood)
        {
            var sesid = (long)Session["sesMerchantID"];
            gift_foods gf=new gift_foods ();
            buy_gift_actives bga=new buy_gift_actives ();
            bga.merchant_id = (int)sesid;//??.
            bga.content = content+"赠送"+giftfood+"一份";
           
            long fid = (from f in db.foods where (f.name == giftfood) select f.id).FirstOrDefault();
            bga.food_id =(int) fid;//赠送商品怎么得到
            bga.updated_at = DateTime.Now;
            db.buy_gift_actives.Add(bga);
            //db.SaveChanges();
            long buyid= (from f in db.buy_gift_actives where ((f.food_id ==(int) fid) &&(bga.merchant_id==1) && (bga.content == content)) select f.id).FirstOrDefault();
            //得到当前的序列ID   savefoodid 有可能在index删除
            foreach(long ideach in idlist)
            { 
            gf.merchant_id = (int)sesid;//??
                gf.food_id = (int)ideach;
            gf.buy_gift_active_id =(int) buyid;
            gf.updated_at = DateTime.Now;
            db.gift_foods.Add(gf);
               
           }
            db.SaveChanges();         

            return View("Index" );
        }


        // GET: discount_actives/Create
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "choicefoods")]
        public ActionResult choicefoods()
        {
            long sesid = (long)Session["sesMerchantID"];
            var food = from f in db.foods where (f.merchant_id== (int) sesid ) select f;

            return View(food);
        }

        //保存所有的菜品信息，以图形显示
        [HttpPost]
        public ActionResult SaveFoods(string Jszzdm,long[] subBox)
        {
            
            savefoodid = subBox;
            IQueryable<foods> food = null;
            IQueryable<foods> food0 = null;
            if (subBox == null)
            {
                return View("Index");
            }
            //var food2;
            if (subBox.Length>0)
            {
                long mid = (long)Session["sesMerchantID"];
                food = from f in db.foods where (subBox.Contains(f.id)&&(f.merchant_id ==(int)mid )  ) select f;
          
               // food0 = from f in db.foods where (subBox.Contains(f.id)) select f;

            }
            if (food==null)
            {
                return View("Index");
            }

           
            //         var query =
            //from _a in db.a
            //where !(from _b in db.b select _b.id).Contains(_a.id)
            //select _a;

            //商户id下的菜品的food表，discount_actives 商户ID，菜品ID，折扣率???登陆后再修改。两个地方
            //  food = from f in db.foods where (f.merchant_id == (int)Session["merchant_id"]) select f;
            //var dis = from d in db.discount_actives where (d.merchant_id == (int)Session["merchant_id"]) select d;//测试

         

            Type elementType = typeof(foods);
            //var ds = new DataSet();
            var t = new DataTable();
            // ds.Tables.Add(t);
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (foods item in food)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            //列数放在viewdata中
            List<long> ids = new List<long>();
            List<string> names = new List<string>();
            List<string> pics = new List<string>();
            List<decimal> prices = new List<decimal>();
            //  List<string> names = new List<string>();

            for (int j = 0; j < t.Rows.Count; j++)
            {
                ids.Add((long)t.Rows[j]["id"]);
                names.Add(t.Rows[j]["name"].ToString());
                pics.Add(t.Rows[j]["pic"].ToString());
                prices.Add((decimal)t.Rows[j]["price"]);
            }
            ViewData["listids"] = ids;
            ViewData["listnames"] = names;
            ViewData["listpics"] = pics;
            ViewData["listprices"] = prices;
            return View("Index");
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

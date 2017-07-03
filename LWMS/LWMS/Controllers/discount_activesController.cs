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

    public class discount_activesController : Controller
    {
     
        private repast_developmentEntities db = new repast_developmentEntities();

        // GET: discount_actives
        public ActionResult Index()
        {
           var  sesid= (long)Session["sesMerchantID"];//??.     
            return View(db.discount_actives.Where(d=>d.merchant_id==sesid).ToList());
        }

        // GET: discount_actives/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount_actives discount_actives = db.discount_actives.Find(id);
            if (discount_actives == null)
            {
                return HttpNotFound();
            }
            return View(discount_actives);
        }

        // GET: discount_actives/Create
        public ActionResult Create()
        {
            return View();
        }



        // GET: discount_actives/Create
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "choicefoods")]
        public ActionResult choicefoods()
        {
            var sesid = (long)Session["sesMerchantID"];
            var food = from f in db.foods where (f.merchant_id ==(int)sesid) select f;
            return View(food);
        }

 

        //保存所有的菜品信息，以图形显示,checkbox
        [HttpPost]
        public ActionResult SaveFoods(long[] subBox)
        {
            long sesid = (long)Session["sesMerchantID"]; ;
         
            //商户id下的菜品的food表，discount_actives 商户ID，菜品ID，折扣率???登陆后再修改。两个地方
            IQueryable food = null;
            if (subBox==null)
            {
                return View("Index");
            }
            if (subBox.Length > 0)
            {               
                food = from f in db.foods where (subBox.Contains(f.id) && (f.merchant_id == (int)sesid)) select f;
            }
            if (food == null)
            {
                return View("Index");
            }
            // return View("Index",dis);

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

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "saveset")]
        public ActionResult saveset(string[] pic, string[] name, int[] idlist, decimal[] discount,string field＿name)
        {
            var sesid = (long)Session["sesMerchantID"];//??.  
            //idlist传递不过来，不能使用全局变量
            if (idlist == null)
            {
                return View("Index");

            }
            //得到当前的序列ID merchant_id food_id discount
            for (int i = 0; i < idlist.Length; i++)
            {
                discount_actives dis = new discount_actives();
                dis.merchant_id =(int) sesid;//??.      
                dis.food_id = idlist[i];
                dis.discount = (decimal)discount[i];
                dis.created_at = DateTime.Now;
                dis.updated_at = DateTime.Now;
                db.discount_actives.Add(dis);
                var foo = (from f in db.foods where ((f.merchant_id == sesid) && (f.id == dis.food_id)) select f).First();
                if (foo!=null)
                {
                    foo.join_discount = true;
                }
                db.SaveChanges();
            }
            Response.Write("保存成功！");
            return View("Index");

        }

        // POST: discount_actives/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,merchant_id,food_id,discount,created_at,updated_at")] discount_actives discount_actives)
        {
            if (ModelState.IsValid)
            {
                db.discount_actives.Add(discount_actives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount_actives);
        }

        // GET: discount_actives/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount_actives discount_actives = db.discount_actives.Find(id);
            if (discount_actives == null)
            {
                return HttpNotFound();
            }
            return View(discount_actives);
        }

        // POST: discount_actives/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,merchant_id,food_id,discount,created_at,updated_at")] discount_actives discount_actives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount_actives).State =System.Data.Entity. EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount_actives);
        }

        // GET: discount_actives/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount_actives discount_actives = db.discount_actives.Find(id);
            if (discount_actives == null)
            {
                return HttpNotFound();
            }
            return View(discount_actives);
        }

        // POST: discount_actives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            discount_actives discount_actives = db.discount_actives.Find(id);
            db.discount_actives.Remove(discount_actives);
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

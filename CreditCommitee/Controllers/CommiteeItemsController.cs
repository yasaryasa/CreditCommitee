using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commitee.Models;
using CreditCommitee.ViewModels;

namespace CreditCommitee.Controllers
{
    //[Authorize(Roles = "User, Admin")]
    public class CommiteeItemsController : Controller
    {
        private CreditCommiteeDB db = new CreditCommiteeDB();

        // GET: /CommiteeItems/
        public ActionResult Index()
        {
            CommiteeProgramme programme = db.CommiteePrograms.Where(w => w.programmeStatus == CommiteeProgramme.CommiteeProgrammeStatus.ACTIVE).OrderByDescending(w => w.id).FirstOrDefault();
            if (programme == null)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.summary = "No Active Programme!";
                errorModel.detail = "Please contact manager to find out next programme.";
                ViewBag.ErrorMsg = errorModel;
                return View();
            }
            ViewBag.CommiteeProgramme = programme;
            List<CommiteeItem> commiteeitems = db.CommiteeItems.Where(w => w.programmeId == programme.id).OrderBy(w=>w.itemOrder).Include(c => c.commiteeProgramme).ToList();

            if (commiteeitems == null || commiteeitems.Count == 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.summary = "No Task Exists!";
                errorModel.detail = "There is no tasks to be discussed.";
                ViewBag.ErrorMsg = errorModel;
                return View();
            }

            
            return View(commiteeitems);
        }

        public ActionResult ListItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             CommiteeProgramme proragmme = db.CommiteePrograms.Where(w => w.id == id).FirstOrDefault();
            
            if (proragmme == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommiteeProgramme = proragmme;

            List<CommiteeItem> commiteeitems = db.CommiteeItems.Where(w => w.programmeId == id).OrderBy(w=>w.itemStatus).Include(c => c.commiteeProgramme).ToList();

            if (commiteeitems == null || commiteeitems.Count == 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel();
                errorModel.summary = "No Task Exists!";
                errorModel.detail = "There is no tasks to be discussed.";
                ViewBag.ErrorMsg = errorModel;
                return View();
            }
            return View(commiteeitems);
        }

        // GET: /CommiteeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeItem commiteeItem = db.CommiteeItems.Find(id);
            if (commiteeItem == null)
            {
                return HttpNotFound();
            }
            return View(commiteeItem);
        }

        [Authorize(Roles = "Admin")]
        // GET: /CommiteeItems/Create
        public ActionResult Create()
        {
            var programmes = db.CommiteePrograms.Where(w => w.programmeStatus == CommiteeProgramme.CommiteeProgrammeStatus.ACTIVE).ToList();
            ViewBag.programmeId = programmes;
            CommiteeItem item = new CommiteeItem();
            item.commiteeProgramme = programmes.First();
            item.createDate = System.DateTime.Today;
            return View(item);
        }

        // POST: /CommiteeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,itemName,itemEndDate,itemStartDate,itemOrder,itemStatus,programmeId,createdBy,createDate")] CommiteeItem commiteeItem)
        {
            if (ModelState.IsValid)
            {
                commiteeItem.createDate = System.DateTime.UtcNow.AddHours(2);
                commiteeItem.createdBy = User.Identity.Name;
                commiteeItem.itemStatus = Commitee.Models.CommiteeItem.ItemStatus.NOT_STARTED;
                commiteeItem.itemOrder = db.CommiteeItems.OrderByDescending(w => w.itemOrder).FirstOrDefault().itemOrder + 1;
                db.CommiteeItems.Add(commiteeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.programmeId = db.CommiteePrograms.Where(w => w.programmeStatus == CommiteeProgramme.CommiteeProgrammeStatus.ACTIVE).ToList();
            return View(commiteeItem);
        }

        // GET: /CommiteeItems/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeItem commiteeItem = db.CommiteeItems.Find(id);
            if (commiteeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.programmeId = new SelectList(db.CommiteePrograms, "id", "programmeName", commiteeItem.programmeId);
            return View(commiteeItem);
        }

        // POST: /CommiteeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,itemName,itemEndDate,itemStartDate,itemOrder,itemStatus,programmeId,createdBy,createDate")] CommiteeItem commiteeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commiteeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.programmeId = new SelectList(db.CommiteePrograms, "id", "programmeName", commiteeItem.programmeId);
            return View(commiteeItem);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeTaskStatus(int? id, CommiteeItem.ItemStatus status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeItem commiteeitem = db.CommiteeItems.Find(id);
            if (commiteeitem == null)
            {
                return HttpNotFound();
            }
            commiteeitem.itemStatus = status;

            switch (status)
            {
                case CommiteeItem.ItemStatus.STARTED:
                    commiteeitem.itemStartDate = System.DateTime.UtcNow.AddHours(2);
                    break;
                case CommiteeItem.ItemStatus.ENDED:
                    commiteeitem.itemEndDate = System.DateTime.UtcNow.AddHours(2);
                    break;
            }

            //commiteeitem.createdBy = User.Identity.Name;
            //commiteeitem.itemStartDate = System.DateTime.Today;
            db.Entry(commiteeitem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /CommiteeItems/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeItem commiteeItem = db.CommiteeItems.Find(id);
            if (commiteeItem == null)
            {
                return HttpNotFound();
            }
            return View(commiteeItem);
        }

        // POST: /CommiteeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            CommiteeItem commiteeItem = db.CommiteeItems.Find(id);
            db.CommiteeItems.Remove(commiteeItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult OgrenciyiGetir(IEnumerable<CommiteeItem> commiteeItems)
        {
            foreach (CommiteeItem item in commiteeItems)
            {
                CommiteeItem item2  = db.CommiteeItems.Where(w => w.id == item.id).FirstOrDefault();
                item2.itemOrder = item.itemOrder;
                db.Entry(item2).State = EntityState.Modified;
            }
            //Ogrenci ogrenci = Ogrenciler.FirstOrDefault(o => o.ID == id);
            //return Json(ogrenci, JsonRequestBehavior.AllowGet);
            //db.Entry(commiteeItems).State = EntityState.Modified;
            db.SaveChanges();
            return Json(commiteeItems, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ProgrammeItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            IEnumerable<CommiteeItem> itemList = db.CommiteeItems.Where(w => w.programmeId == id).ToList();

            if (itemList == null)
            {
                return HttpNotFound();
            }
            var chartData = new Dictionary<string, double>();

            List<String> itemNames = new List<String>();
            List<Double> itemData = new List<Double>();
            foreach (CommiteeItem item in itemList)
            {
                if (item.itemEndDate != null)
                {
                    System.TimeSpan diffResult = item.itemEndDate.Value.Subtract(item.itemStartDate.Value);
                    //chartData[item.itemName] = diffResult.Minutes;
                    itemNames.Add(item.itemName);
                    itemData.Add(diffResult.Minutes);
                }
            }
            ViewBag.ITEM_NAMES = itemNames.ToArray();
            ViewBag.ITEM_DATAS = itemData.ToArray();
            return View();
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

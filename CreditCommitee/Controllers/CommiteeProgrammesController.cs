using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commitee.Models;
using Commitee.Controllers;

namespace CreditCommitee.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommiteeProgrammesController : Controller
    {
        private CreditCommiteeDB db = new CreditCommiteeDB();

        public ActionResult Index(int? id)
        {
            return View(db.CommiteePrograms.OrderByDescending(w=>w.programmeStatus).ThenByDescending(w=>w.programmeDate).ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeProgramme commiteeProgram = db.CommiteePrograms.Find(id);
            if (commiteeProgram == null)
            {
                return HttpNotFound();
            }
            return View(commiteeProgram);
        }

        public ActionResult Create()
        {
            CommiteeProgramme program = new CommiteeProgramme();
            program.programmeDate = CommiteeControllerUtils.GetNextWeekday(System.DateTime.Today, DayOfWeek.Wednesday);
            program.programmeName = "Programme_" + program.programmeDate.ToString(string.Format("dd.MM.yyyy"));
            return View(program);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,programmeName,programmeDate")] CommiteeProgramme commiteeProgram)
        {
            List<CommiteeProgramme> activeProgrammes = db.CommiteePrograms.Where(w => w.programmeStatus == CommiteeProgramme.CommiteeProgrammeStatus.ACTIVE).ToList();
            if (activeProgrammes.Count > 0) {
                ModelState.AddModelError("", "There is already an ACTIVE programme. To create new one, please complete the previous one.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    commiteeProgram.programmeStatus = CommiteeProgramme.CommiteeProgrammeStatus.ACTIVE;
                    commiteeProgram.createDate = System.DateTime.UtcNow.AddHours(2);
                    commiteeProgram.createdBy = User.Identity.Name;
                    db.CommiteePrograms.Add(commiteeProgram);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(commiteeProgram);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeProgramme commiteeProgram = db.CommiteePrograms.Find(id);
            if (commiteeProgram == null)
            {
                return HttpNotFound();
            }
            return View(commiteeProgram);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,programmeName,programmeDate,programmeStatus")] CommiteeProgramme commiteeProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commiteeProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commiteeProgram);
        }

        // GET: /CommiteePrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeProgramme commiteeProgram = db.CommiteePrograms.Find(id);
            if (commiteeProgram == null)
            {
                return HttpNotFound();
            }
            return View(commiteeProgram);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommiteeProgramme commiteeProgram = db.CommiteePrograms.Find(id);
            db.CommiteePrograms.Remove(commiteeProgram);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeProgrammeStatus(int? id, CommiteeProgramme.CommiteeProgrammeStatus status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommiteeProgramme item = db.CommiteePrograms.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            item.programmeStatus = status;

            switch (status)
            {
                case CommiteeProgramme.CommiteeProgrammeStatus.PASSIVE:
                    item.programmeEndDate = System.DateTime.UtcNow.AddHours(2);
                    break;
            }
            db.Entry(item).State = EntityState.Modified;
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

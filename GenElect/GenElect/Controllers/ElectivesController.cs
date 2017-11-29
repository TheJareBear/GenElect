using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenElect.DAL;
using GenElect.Models;

namespace GenElect.Controllers
{
    public class ElectivesController : Controller
    {
        private CatalogContext db = new CatalogContext();

        // GET: Electives
        public ActionResult Index(string period)
        {
            var electives = db.Electives.Include(p => p.Period);
            if(!String.IsNullOrEmpty(period))
                electives = db.Electives.Where(e => e.Period.PeriodNumber.ToString() == period);

            return View(electives.ToList());
        }

        // GET: Electives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elective elective = db.Electives.Find(id);
            if (elective == null)
            {
                return HttpNotFound();
            }
            return View(elective);
        }

        // GET: Electives/Create
        public ActionResult Create()
        {
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "ID");
            return View();
        }

        // POST: Electives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Instructor,Description,Capacity,CurrentStudentCount,PeriodID")] Elective elective)
        {
            if (ModelState.IsValid)
            {
                db.Electives.Add(elective);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "ID", elective.PeriodID);
            return View(elective);
        }

        // GET: Electives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elective elective = db.Electives.Find(id);
            if (elective == null)
            {
                return HttpNotFound();
            }
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "ID", elective.PeriodID);
            return View(elective);
        }

        // POST: Electives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Instructor,Description,Capacity,CurrentStudentCount,PeriodID")] Elective elective)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elective).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "ID", elective.PeriodID);
            return View(elective);
        }

        // GET: Electives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elective elective = db.Electives.Find(id);
            if (elective == null)
            {
                return HttpNotFound();
            }
            return View(elective);
        }

        // POST: Electives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elective elective = db.Electives.Find(id);
            db.Electives.Remove(elective);
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

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
using Microsoft.AspNet.Identity;

namespace GenElect.Controllers
{
    public class ElectivesController : Controller
    {
        private CatalogContext db = new CatalogContext();

        // GET: Electives
        public ActionResult Index(string period, string search)
        {
            ApplicationDbContext appDb = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            ApplicationUser appUser = appDb.Users.Select(x => x).Where(x => x.Id == userId).FirstOrDefault();
            var electives = db.Electives.Include(p => p.Period);
            if(!String.IsNullOrEmpty(period))
                electives = db.Electives.Where(e => e.Period.PeriodNumber.ToString() == period);

            if(!String.IsNullOrEmpty(search))
            {
                electives = electives.Where(p => p.Name.Contains(search) || p.Description.Contains(search) || p.Instructor.Contains(search));
                ViewBag.Search = search;
            }

            var periods = electives.OrderBy(p => p.Period.ID).Select(p => p.Period.ID).Distinct();

            if(!String.IsNullOrEmpty(period))
            {
                electives = electives.Where(p => p.Period.ID.ToString() == period);
            }

            ViewBag.Period = new SelectList(periods);
            ViewBag.User = appUser;

            return View(electives.ToList());
        }

        public ActionResult Roster(int? id, int? period)
        {
            ApplicationDbContext appDb = new ApplicationDbContext();
            var students = appDb.Users.Where(s => s.Elective1 == -99);
            if (period == 1)
            {
                students = appDb.Users.Where(s => s.Elective1 == id);
            }
            if (period == 2)
            {
                students = appDb.Users.Where(s => s.Elective2 == id);
            }
            if (period == 3)
            {
                students = appDb.Users.Where(s => s.Elective3 == id);
            }

            Elective elective = db.Electives.Find(id);

            ViewBag.ElectiveName = elective.Name;
            ViewBag.PeriodNumber = period;
            ViewBag.Instructor = elective.Instructor;

            return View(students.ToList());
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

        public ActionResult Register(int electiveID, int period, int? id)
        {
            ApplicationDbContext appDb = new ApplicationDbContext();
            CatalogContext catDb = new CatalogContext();
            var userId = User.Identity.GetUserId();

            ApplicationUser appUser = appDb.Users.Select(x => x).Where(x => x.Id == userId).FirstOrDefault();
            Elective elective = catDb.Electives.Select(x => x).Where(x => x.ID == id).FirstOrDefault();

            if (period == 1)
            {
                if (appUser.Elective1 == 0 && elective.CurrentStudentCount < elective.Capacity)
                {
                    appUser.Elective1 = electiveID;
                    ViewBag.Message = "Successful registration!";
                    elective.CurrentStudentCount++;
                }
                else if (elective.CurrentStudentCount >= elective.Capacity)
                    ViewBag.Message = "Sorry, that class is now full";
                else
                    ViewBag.Message = "Sorry registration failed, you already have an elective selected for that period";
            }
            else if (period == 2)
            {
                if (appUser.Elective2 == 0 && elective.CurrentStudentCount < elective.Capacity)
                {
                    appUser.Elective2 = electiveID;
                    ViewBag.Message = "Successful registration!";
                    elective.CurrentStudentCount++;
                }
                else if (elective.CurrentStudentCount >= elective.Capacity)
                    ViewBag.Message = "Sorry, that class is now full";
                else
                    ViewBag.Message = "Sorry registration failed, you already have an elective selected for that period";
            }
            else
            {
                if (appUser.Elective3 == 0 && elective.CurrentStudentCount < elective.Capacity)
                {
                    appUser.Elective3 = electiveID;
                    ViewBag.Message = "Successful registration!";
                    elective.CurrentStudentCount++;
                }
                else if (elective.CurrentStudentCount >= elective.Capacity)
                    ViewBag.Message = "Sorry, that class is now full";
                else
                    ViewBag.Message = "Sorry registration failed, you already have an elective selected for that period";
            }

            appDb.SaveChangesAsync();
            catDb.SaveChangesAsync();

            return View();
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

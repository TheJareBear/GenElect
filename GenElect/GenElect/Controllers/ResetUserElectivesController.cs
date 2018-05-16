using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenElect.Models;
using GenElect.DAL;

namespace GenElect.Controllers
{
    public class ResetUserElectivesController : Controller
    {
        // GET: ResetUserElectives
        public ActionResult Index()
        {
            ApplicationDbContext appDb = new ApplicationDbContext();
            var users = appDb.Users;

            foreach (var user in users)
            {
                user.Elective1 = 0;
                user.Elective2 = 0;
                user.Elective3 = 0;
            }

            CatalogContext catDb = new CatalogContext();
            var electives = catDb.Electives;

            foreach (var elective in electives)
            {
                elective.CurrentStudentCount = 0;
            }

            appDb.SaveChangesAsync();
            catDb.SaveChangesAsync();

            return View();
        }
    }
}
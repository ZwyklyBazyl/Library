using Library1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library1.Controllers
{
    public class ZbiorWypozyczajacychController : Controller
    {
        BibliotekaEntities dbObj = new BibliotekaEntities();



        // GET: ZbiorKsiazek
        public ActionResult ZbiorWypozyczajacych()
        {
            return View(dbObj.Wypozyczajacies.ToList());
        }

        public ActionResult Usun(Wypozyczajacy w)
        {
            dbObj.Database.ExecuteSqlCommand("DELETE FROM Wypozyczajacy WHERE ID_Wypozyczajacego=" + w.ID_Wypozyczajacego.ToString());

            dbObj.SaveChanges();

            ViewBag.Deleted = true;

            return View("ZbiorWypozyczajacych", dbObj.Wypozyczajacies.ToList());
        }
    }
}
using Library1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library1.Controllers
{
    public class ZbiorKsiazekController : Controller
    {
        BibliotekaEntities dbObj = new BibliotekaEntities();

            

            // GET: ZbiorKsiazek
         public ActionResult ZbiorKsiazek()
        {
            return View(dbObj.Ksiazkas.ToList());
        }


        public ActionResult Zwroc(Ksiazka k)
        {
            k.ID_Wypozyczajacego = null;
            k.ID_StanuKsiazki = 1;

            dbObj.Entry(k).State = System.Data.Entity.EntityState.Modified;

            dbObj.SaveChanges();

            ViewBag.Returned = true;

            return View("ZbiorKsiazek", dbObj.Ksiazkas.ToList());
        }

        public ActionResult Usun(Ksiazka k)
        {
            dbObj.Database.ExecuteSqlCommand("DELETE FROM Ksiazka WHERE ID_Ksiazki=" + k.ID_Ksiazki.ToString());

            dbObj.SaveChanges();

            ViewBag.Deleted = true;

            return View("ZbiorKsiazek", dbObj.Ksiazkas.ToList());
        }
    }
}
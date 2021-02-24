using Library1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library1.Controllers
{
    public class KsiazkaController : Controller
    {
        // GET: Ksiazka
        BibliotekaEntities dbObj = new BibliotekaEntities();

        public ActionResult Ksiazka(Ksiazka k)
        {
            var wypozyczajacy = dbObj.Wypozyczajacies.ToList();
            if(wypozyczajacy != null)
            {
                ViewBag.data = wypozyczajacy;
            }

            if(k != null)
            {
                return View(k);
            }
            else
            {
                return View();
            }

            
        }

        [HttpPost]
        public ActionResult AddKsiazka(Ksiazka model)
        {
            Ksiazka obj = new Ksiazka();
            obj.ID_Ksiazki = model.ID_Ksiazki;
            obj.Nazwa_Ksiazki = model.Nazwa_Ksiazki;
            obj.Autor_Ksiazki = model.Autor_Ksiazki;
            obj.ID_Wypozyczajacego = model.ID_Wypozyczajacego;

            if (obj.ID_Wypozyczajacego != null) obj.ID_StanuKsiazki = 2;
            else obj.ID_StanuKsiazki = 1;

            if(obj.ID_Ksiazki == 0)
            {
                dbObj.Ksiazkas.Add(obj);
                ViewBag.Added = true;
            }
            else
            {
                dbObj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                ViewBag.Modified = true;
            }


            dbObj.SaveChanges();

            var wypozyczajacy = dbObj.Wypozyczajacies.ToList();
            if (wypozyczajacy != null)
            {
                ViewBag.data = wypozyczajacy;
            }

            return RedirectToAction("ZbiorKsiazek", "ZbiorKsiazek", dbObj.Ksiazkas.ToList());
        }
    }
}
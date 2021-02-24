using Library1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library1.Controllers
{
    public class WypozyczajacyController : Controller
    {
        BibliotekaEntities dbObj = new BibliotekaEntities();

        public ActionResult Wypozyczajacy(Wypozyczajacy w)
        {
            var ksiazki = dbObj.Ksiazkas.ToList();
            if (ksiazki != null)
            {
                ViewBag.Ksiazki = ksiazki;
            }

            if (w != null)
            {
                return View(w);
            }
            else
            {
                return View();
            }


        }

        public ActionResult Zwroc(Ksiazka k)
        {
            Wypozyczajacy w = dbObj.Wypozyczajacies.Find(k.ID_Wypozyczajacego);

            k.ID_Wypozyczajacego = null;
            k.ID_StanuKsiazki = 1;

            dbObj.Entry(k).State = System.Data.Entity.EntityState.Modified;

            dbObj.SaveChanges();

            ViewBag.Returned = true;

            var ksiazki = dbObj.Ksiazkas.ToList();
            if (ksiazki != null)
            {
                ViewBag.Ksiazki = ksiazki;
            }

            return View("Wypozyczajacy", w);
        }


        [HttpPost]
        public ActionResult AddWypozyczajacy(Wypozyczajacy model)
        {
            Wypozyczajacy obj = new Wypozyczajacy();
            obj.ID_Wypozyczajacego = model.ID_Wypozyczajacego;
            obj.Imie_Wypozyczajacego = model.Imie_Wypozyczajacego;
            obj.Nazwisko_Wypozyczajacego = model.Nazwisko_Wypozyczajacego;
            obj.Telefon_Wypozyczajacego = model.Telefon_Wypozyczajacego;

            if (obj.ID_Wypozyczajacego == 0)
            {
                dbObj.Wypozyczajacies.Add(obj);
                ViewBag.Added = true;
            }
            else
            {
                dbObj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                ViewBag.Modified = true;
            }


            dbObj.SaveChanges();

            var ksiazki = dbObj.Ksiazkas.ToList();
            if (ksiazki != null)
            {
                ViewBag.Ksiazki = ksiazki;
            }

            return RedirectToAction("ZbiorWypozyczajacych", "ZbiorWypozyczajacych", dbObj.Wypozyczajacies.ToList());
        }
    }
}
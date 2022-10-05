using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Caris.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cari cari)
        {
            cari.Durum = true;
            context.Caris.Add(cari);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var currentSil = context.Caris.Find(id);
            currentSil.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var currentGetir = context.Caris.Find(id);
            return View("CariGetir",currentGetir);
        }
        public ActionResult CariGuncelle(Cari cari)
        {
            var currentGuncelle = context.Caris.Find(cari.CariId);
            currentGuncelle.Durum = true;
            currentGuncelle.CariAdı = cari.CariAdı;
            currentGuncelle.CariSoyadı = cari.CariSoyadı;
            currentGuncelle.CariSehir = cari.CariSehir;
            currentGuncelle.CariMail = cari.CariMail;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
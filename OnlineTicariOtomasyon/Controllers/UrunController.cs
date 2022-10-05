using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var urunler = context.Uruns.Where(x=>x.Durum == true).ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAdı,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            context.Uruns.Add(urun);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var silinecekUrun = context.Uruns.Find(id);
            silinecekUrun.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id) 
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAdı,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urunDeger = context.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }

        public ActionResult UrunGuncelle(Urun urun)
        {
            var urn = context.Uruns.Find(urun.UrunId);
            urn.AlisFiyati = urun.AlisFiyati;
            urn.SatisFiyati = urun.SatisFiyati;
            urn.Durum = urun.Durum;
            urn.Marka = urun.Marka;
            urn.Stok = urun.Stok;
            urn.UrunAdı = urun.UrunAdı;
            urn.UrunImage = urun.UrunImage;
            urn.KategoriId = urun.KategoriId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
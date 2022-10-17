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
        public ActionResult Index(string ara)
        {
            var urunler = from x in context.Uruns select x;
            if (!string.IsNullOrEmpty(ara))
            {
                urunler = urunler.Where(y => y.UrunAdı.Contains(ara));
            }
            return View(urunler.ToList());
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
            urun.Durum = true;
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
        public ActionResult UrunListesi()
        {
            var degerler = context.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {       
            List<SelectListItem> deger2 = (from x in context.Caris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAdı + " " + x.CariSoyadı,
                                               Value = x.CariId.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdı + " " + x.PersonelSoyadı,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            var deger1 = context.Uruns.Find(id);
            ViewBag.dgr1 = deger1.UrunId;
            ViewBag.fiyat = deger1.SatisFiyati;
            ViewBag.dgr3 = deger3;
            return View();

        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("Index","Satis");
        }

    }
}
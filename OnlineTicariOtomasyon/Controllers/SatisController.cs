using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context context = new Context();
        public ActionResult Index()
        {
            var satislar = context.SatisHarekets.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in context.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAdı,
                                               Value = x.UrunId.ToString()
                                           }).ToList();
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
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satisHareket)
        {

            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAdı,
                                               Value = x.UrunId.ToString()
                                           }).ToList();
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
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var deger = context.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            var currentSatis = context.SatisHarekets.Find(satisHareket.SatisId);
            currentSatis.Fiyat = satisHareket.Fiyat;
            currentSatis.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            currentSatis.CariId = satisHareket.CariId;
            currentSatis.PersonelId = satisHareket.PersonelId;
            currentSatis.ToplamTutar = satisHareket.ToplamTutar;
            currentSatis.UrunId = satisHareket.UrunId;
            currentSatis.Adet = satisHareket.Adet;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = context.SatisHarekets.Where(x => x.SatisId == id).ToList();
            return View(degerler);
        }


    }
}
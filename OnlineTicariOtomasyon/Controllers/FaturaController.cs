using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context context = new Context();
        public ActionResult Index()
        {
            var fatura = context.Faturas.ToList();
            return View(fatura);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Fatura fatura)
        {
            context.Faturas.Add(fatura);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = context.Faturas.Find(id);
            return View("FaturaGetir",fatura);
        }

        public ActionResult FaturaGuncelle(Fatura fatura)
        {
            var guncellenecekFatura = context.Faturas.Find(fatura.FaturaId);
            guncellenecekFatura.FaturaSıraNo = fatura.FaturaSıraNo;
            guncellenecekFatura.FaturaSeriNo = fatura.FaturaSeriNo;
            guncellenecekFatura.Saat = fatura.Saat;
            guncellenecekFatura.TeslimAlan = fatura.TeslimAlan;
            guncellenecekFatura.TeslimEden = fatura.TeslimEden;
            guncellenecekFatura.Tarih = fatura.Tarih;
            guncellenecekFatura.VergiDairesi = fatura.VergiDairesi;
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = context.FaturaKalems.Where(x => x.FaturaId == id).ToList();          
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem faturaKalem)
        {
            context.FaturaKalems.Add(faturaKalem);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            ForFatura forFatura = new ForFatura();
            forFatura.Fatura = context.Faturas.ToList();
            forFatura.FaturaKalem = context.FaturaKalems.ToList();
            return View(forFatura);
        }

        public ActionResult FaturaKaydet
            (string FaturaSeriNo, 
            string FaturaSıraNo,
            DateTime Tarih,
            string VergiDairesi,
            string Saat,
            string TeslimAlan, 
            string TeslimEden, 
            string Toplam, 
            FaturaKalem[] kalemler)
        {
            Fatura fatura = new Fatura();
            fatura.FaturaSeriNo = FaturaSeriNo;
            fatura.FaturaSıraNo = FaturaSıraNo;
            fatura.Tarih = Tarih;
            fatura.VergiDairesi = VergiDairesi;
            fatura.Saat = Saat;
            fatura.TeslimAlan = TeslimAlan;
            fatura.TeslimEden = TeslimEden;
            fatura.Toplam = decimal.Parse(Toplam);
            context.Faturas.Add(fatura);
            foreach (var x in kalemler)
            {
                FaturaKalem faturaKalem = new FaturaKalem();
                faturaKalem.Aciklama = x.Aciklama;
                faturaKalem.BrimFiyat = x.BrimFiyat;
                faturaKalem.Tutar = x.Tutar;
                faturaKalem.FaturaId = x.FaturaId;
                faturaKalem.Miktar = x.Miktar;
                context.FaturaKalems.Add(faturaKalem);

            }
            context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);


        }

    }
}
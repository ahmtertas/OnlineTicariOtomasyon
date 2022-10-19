using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context context = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var cariMail = (string)Session["CariMail"];

            var degerler = context.Mesajlars.Where(x => x.Alici == cariMail).ToList();
            ViewBag.mail = cariMail;
            var mailId = context.Caris.Where(x => x.CariMail == cariMail).Select(y => y.CariId).FirstOrDefault();
            ViewBag.mId = mailId;
            var toplamSatis = context.SatisHarekets.Where(x => x.CariId == mailId).Count();
            ViewBag.totalSatis = toplamSatis;
            var toplamTutar = context.SatisHarekets.Where(x => x.CariId == mailId).Sum(y => (decimal?)y.ToplamTutar);
            ViewBag.totalTutar = toplamTutar;
            var toplamUrunSayisi = context.SatisHarekets.Where(x => x.CariId == mailId).Sum(y=> (decimal?)y.Adet);
            ViewBag.totalUrunSayisi = toplamUrunSayisi;
            var adSoyad = context.Caris.Where(x => x.CariMail == cariMail).Select(y => y.CariAdı + " " + y.CariSoyadı).FirstOrDefault();
            ViewBag.adSoyad = adSoyad;
            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = context.Caris
                .Where(x => x.CariMail == mail.ToString())
                .Select(y => y.CariId)
                .FirstOrDefault();
            var degerler = context.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(degerler);

        }
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x=>x.Alici == mail).OrderByDescending(x=>x.MesajId).ToList();
            var gelenSayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenMesajlar = gelenSayisi;
            var gidenSayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenMesajlar = gidenSayisi;
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajId).ToList();
            var gidenSayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenMesajlar = gidenSayisi;
            var gelenSayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenMesajlar = gelenSayisi;
            return View(mesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var degerler = context.Mesajlars.Where(x => x.MesajId == id).ToList();
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Alici == mail).ToList();
            var gelenSayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenMesajlar = gelenSayisi;
            var gidenSayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenMesajlar = gidenSayisi;
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Gonderici == mail).ToList();
            var gidenSayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenMesajlar = gidenSayisi;
            var gelenSayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenMesajlar = gelenSayisi;

            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar yeniMesaj)
        {
            var mail = (string)Session["CariMail"];
            yeniMesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            yeniMesaj.Gonderici = mail;
            context.Mesajlars.Add(yeniMesaj);
            context.SaveChanges();
            return View();
        }

        public ActionResult KargoTakip(string kargoTakip)
        {
            var kargo = from x in context.KargoDetays select x;
            kargo = kargo.Where(y => y.TakipKodu.Contains(kargoTakip));        
            return View(kargo.ToList());
        }

        public ActionResult CariKargoTakip(string id)
        {
            var degerler = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult  HesapGuncelle()
        {
            var mail = (string)Session["CariMail"];
            var id = context.Caris.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            var cariBul = context.Caris.Find(id);
            return PartialView("HesapGuncelle", cariBul);
        }

        public PartialViewResult Duyurular()
        {
            var veriler = context.Mesajlars.Where(x => x.Gonderici == "admin").ToList();

            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cari cari)
        {
            var cariGuncelle = context.Caris.Find(cari.CariId);
            cariGuncelle.CariAdı = cari.CariAdı;
            cariGuncelle.CariSoyadı = cari.CariSoyadı;
            cariGuncelle.CariMail = cari.CariMail;
            cariGuncelle.CariSifre = cari.CariSifre;
            cariGuncelle.CariSehir = cari.CariSehir;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var toplamCari = context.Caris.Count().ToString();
            ViewBag.d1 = toplamCari;
            var toplamUrun = context.Uruns.Count().ToString();
            ViewBag.d2 = toplamUrun;
            var personelCount = context.Personels.Count().ToString();
            ViewBag.d3 = personelCount;
            var toplamKategori = context.Kategoris.Count().ToString();
            ViewBag.d4 = toplamKategori;
            var toplamStok = context.Uruns.Sum(x=>x.Stok).ToString();
            ViewBag.d5 = toplamUrun;
            var kritikDurum = context.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = kritikDurum;
            var markaSayisi = (from x in context.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = markaSayisi;
            var enyuksekFiyat = (from x in context.Uruns orderby x.SatisFiyati descending select x.UrunAdı).FirstOrDefault();
            ViewBag.d8 = enyuksekFiyat;
            var endusukFiyat = (from x in context.Uruns orderby x.SatisFiyati select x.UrunAdı).FirstOrDefault();
            ViewBag.d9 = endusukFiyat;
            var buzdolabı = context.Uruns.Count(x => x.UrunAdı == "Buzdolabi").ToString();
            ViewBag.d10 = buzdolabı;
            var laptop = context.Uruns.Count(x => x.UrunAdı == "Laptop").ToString();
            ViewBag.d11 = laptop;
            var maxMarka = context.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count())
                .Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = maxMarka;

            var enCokSatan = context.Uruns.Where(u=>u.UrunId==(context.SatisHarekets.GroupBy(x => x.UrunId).OrderByDescending(z => z.Count())
                .Select(y => y.Key).FirstOrDefault())).Select(k=>k.UrunAdı).FirstOrDefault();
            ViewBag.d13 = enCokSatan;

            var toplamTutar = context.SatisHarekets.Sum(x=>x.ToplamTutar).ToString();
            ViewBag.d14 = toplamTutar;
            DateTime bugun = DateTime.Today;
            var bugunkuSatislar = context.SatisHarekets.Count(x=>x.Tarih == bugun).ToString();
            ViewBag.d15 = bugunkuSatislar;

            var bugunkuToplamSatıs = context.SatisHarekets.Where(x=>x.Tarih == bugun).Sum(y=>(decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = bugunkuToplamSatıs;


            return View();
        }

        public ActionResult KolayTablolar() 
        {
            var sorgu = from x in context.Caris
                        group x by x.CariSehir
                        into g
                        select new GroupSinifi {
                            Sehir = g.Key,
                            Sayi = g.Count()                    
                        };
            return View(sorgu.ToList());
        }

        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in context.Personels
                         group x by x.Departman.DepartmanName into g
                         select new GroupSinifi2
                         {
                             DepartmanAdı = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }

        public PartialViewResult Partial2()
        {
            var sorgu3 = context.Caris.ToList();
            return PartialView(sorgu3);
        }

        public PartialViewResult Partial3()
        {
            var sorgu4 = context.Uruns.ToList();
            return PartialView(sorgu4);
        }

        public PartialViewResult Partial4()
        {
            var sorgu5 = from x in context.Uruns
                         group x by x.Marka into g
                         select new GroupSinifi3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu5);
        }




    }
}
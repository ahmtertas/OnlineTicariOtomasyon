using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;
using QRCoder;

namespace OnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context context = new Context();
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult GrafikCiz() 
        {
            var grafikciz = new Chart(600,600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı")
                .AddLegend("Stok")
                .AddSeries("Değerler",xValue: new [] { "Mobilya", "Ofis Eşyaları","Bilgisayar" }
                ,yValues: new [] {85,56,98}).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Veriler()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = context.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAdı));
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(width: 800, height: 800)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Column", name: "Stok", xValue: xvalue, yValues:yvalue);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult GrafikGoogle()
        {

            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }
               
        public List<GrafikGoogle> UrunListesi()
        {
            List<GrafikGoogle> snf = new List<GrafikGoogle>();
            snf = context.Uruns.Select(x=> new GrafikGoogle 
            {
                UrunAd = x.UrunAdı,
                Stok = x.Stok
            }).ToList();

            return snf;
        }
    }
}
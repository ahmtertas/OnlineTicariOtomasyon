using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context context = new Context();
        public ActionResult Index(string ara)
        {
            var kargo = from x in context.KargoDetays select x;
            if (!string.IsNullOrEmpty(ara))
            {
                kargo = kargo.Where(y => y.TakipKodu.Contains(ara));
            }
            return View(kargo.ToList());
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rndm = new Random();
            string[] karakterler = { "A", "B", "C", "D","E", "F","G","H" };
            int k1, k2, k3;
            k1 = rndm.Next(0, karakterler.Length);
            k2 = rndm.Next(0, karakterler.Length);
            k3 = rndm.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rndm.Next(100, 1000); //10 karakter>> 3
            Thread.Sleep(50);
            s2 = rndm.Next(10, 99);
            Thread.Sleep(50);
            s3 = rndm.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kargoDetay)
        {
            context.KargoDetays.Add(kargoDetay);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakip(string id)
        {
            var degerler = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}
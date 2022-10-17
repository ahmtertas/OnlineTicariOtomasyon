using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context context = new Context();
        public ActionResult Index()
        {
            UrunListeleme urunListeleme = new UrunListeleme();
            //var degerler = context.Uruns.Where(x=>x.UrunId==1).ToList();
            urunListeleme.Deger1 = context.Uruns.Where(x => x.UrunId == 9).ToList();
            urunListeleme.Deger2 = context.Detays.Where(y => y.DetayId == 1).ToList();
            return View(urunListeleme);
        }
    }
}
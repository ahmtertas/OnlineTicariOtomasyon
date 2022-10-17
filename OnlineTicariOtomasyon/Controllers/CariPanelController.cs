using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
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
            var degerler = context.Caris.FirstOrDefault(x => x.CariMail == cariMail);
            ViewBag.mail = cariMail;
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
            var mesajlar = context.Mesajlars.ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        //[HttpPost] 
        //public ActionResult YeniMesaj()
        //{
        //    return View();
        //}
    }
}
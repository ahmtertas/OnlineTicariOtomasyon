using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanName,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            if(Request.Files.Count>0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelImage = "/Image/" + dosyaAdi + uzanti;
            }
            context.Personels.Add(personel);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanName,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var currentPersonel = context.Personels.Find(id);
            return View("PersonelGetir", currentPersonel);
        }
        public ActionResult PersonelGuncelle(Personel personel)
        {
            string dosyaAdi="";
            string uzanti="";
            if (Request.Files.Count > 0)
            {
                dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
            }

            var gelenPersonel = context.Personels.Find(personel.PersonelId);
            gelenPersonel.PersonelAdı = personel.PersonelAdı;
            gelenPersonel.PersonelSoyadı = personel.PersonelSoyadı;
            gelenPersonel.DepartmanId = personel.DepartmanId;
            gelenPersonel.PersonelImage = "/Image/" + dosyaAdi + uzanti;
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult PersonelList()
        {
            var deger = context.Personels.ToList();
            return View(deger);
        }
        
    }
}
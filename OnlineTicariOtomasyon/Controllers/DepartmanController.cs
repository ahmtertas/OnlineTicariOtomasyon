using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Departmans.Where(x=>x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            context.Departmans.Add(departman);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var departman = context.Departmans.Find(id);
            departman.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var departman = context.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var dprtGuncelle = context.Departmans.Find(departman.DepartmanId);
            dprtGuncelle.DepartmanName = departman.DepartmanName;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = context.Personels.Where(x => x.DepartmanId == id).ToList();
            var dpt = context.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanName)
                .FirstOrDefault();
            ViewBag.d = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id) 
        {
            var degerler = context.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var dper = context.Personels.Where(x => x.PersonelId == id)
                .Select(y => y.PersonelAdı + " " + y.PersonelSoyadı).FirstOrDefault();
            ViewBag.dpers = dper;
            return View(degerler);
        }
        


    }
}
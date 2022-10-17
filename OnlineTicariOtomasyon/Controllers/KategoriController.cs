﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;
using PagedList;
using PagedList.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = context.Kategoris.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            context.Kategoris.Add(kategori);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var silinecekEleman = context.Kategoris.Find(id);
            context.Kategoris.Remove(silinecekEleman);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = context.Kategoris.Find(id);
            return View("KategoriGetir",kategori);

        }
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var kategoriGuncelle = context.Kategoris.Find(kategori.KategoriId);
            kategoriGuncelle.KategoriAdı = kategori.KategoriAdı;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
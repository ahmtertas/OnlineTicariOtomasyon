using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class ToDoListController : Controller
    {
        // GET: ToDoList
        Context context = new Context();
        public ActionResult Index()
        {
            var deger1 = context.Caris.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = context.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = context.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = (from x in context.Caris select x.CariSehir)
                .Distinct().Count().ToString();
            ViewBag.d4 = deger4;

            var toDoList = context.ToDoLists.ToList();

            return View(toDoList);
        }       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class MesajController : Controller
    {
        // GET: Galeri
        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }       
    }
}
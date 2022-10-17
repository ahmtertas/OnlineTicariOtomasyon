using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class UrunListeleme
    {
        public IEnumerable<Urun> Deger1 { get; set; }
        public IEnumerable<Detay> Deger2 { get; set; }

    }
}
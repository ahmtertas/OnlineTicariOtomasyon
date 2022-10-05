using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class SatisHareket
    {
        [Key]
        public int SatisId { get; set; }
        //ürün
        //cari
        //personel
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }



        //ilişkili tablolar
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }

        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }


    }
}
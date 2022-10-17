using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Detay
    {
        [Key]
        public int DetayId { get; set; }
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string UrunAdı { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string UrunBilgi { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }

        [Display(Name ="Personel Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelAdı { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelSoyadı { get; set; }

        [Display(Name = "Personel Image")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelImage { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public int DepartmanId { get; set; }
        public virtual Departman Departman { get; set; }
    }
}
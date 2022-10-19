using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class ForFatura
    {
        public IEnumerable<Fatura> Fatura { get; set; }
        public IEnumerable<FaturaKalem> FaturaKalem { get; set; }
    }
}
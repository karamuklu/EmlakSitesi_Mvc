using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Emlak_Sitesi.Models
{
    public class Harita_Veri
    {
        public int ILAN_NO { get; set; }
        public double? LAT { get; set; }
        public double? LNG { get; set; }
        public string ILAN_BASLIGI { get; set; }
        public string ILAN_ICERIGI { get; set; }
        public string RESIM { get; set; }
        public int FIYAT { get; set; }
        public string ODA_SAYISI { get; set; }
        public int SATILIK_KIRALIK { get; set; }
        public int M2 { get; set; }
        public string KONUMU { get; set; }
    }
}
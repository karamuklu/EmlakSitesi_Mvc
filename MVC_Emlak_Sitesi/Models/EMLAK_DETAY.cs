using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Emlak_Sitesi.Models
{
    public class EMLAKCI_DETAY
    {
        public TBLSORUMLU EMLAKCI { get; set; }
        public List<TBLEMLAK> IlgilendigiEmlaklar { get; set; }
        //public TBLEMLAKDETAY EmlakDetay { get; set; }
        
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Emlak_Sitesi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLSORUMLU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLSORUMLU()
        {
            this.TBLEMLAK = new HashSet<TBLEMLAK>();
        }
    
        public int ID { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public string EMAIL { get; set; }
        public string TEL_NO { get; set; }
        public string RESIM { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLEMLAK> TBLEMLAK { get; set; }
    }
}

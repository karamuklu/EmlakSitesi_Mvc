﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EMLAK_SITESIEntities : DbContext
    {
        public EMLAK_SITESIEntities()
            : base("name=EMLAK_SITESIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBLEMLAK> TBLEMLAK { get; set; }
        public virtual DbSet<TBLEMLAKTIP> TBLEMLAKTIP { get; set; }
        public virtual DbSet<TBLKULLANICI> TBLKULLANICI { get; set; }
        public virtual DbSet<TBLRESIM> TBLRESIM { get; set; }
        public virtual DbSet<TBLSORUMLU> TBLSORUMLU { get; set; }
        public virtual DbSet<TBLEMLAK_DETAY> TBLEMLAK_DETAY { get; set; }
        public virtual DbSet<SonEklenenIlanlar> SonEklenenIlanlar { get; set; }
    }
}
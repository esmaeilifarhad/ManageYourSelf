//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManageYourSelfMVC.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Daramad
    {
        public int DaramadId { get; set; }
        public int Rial { get; set; }
        public int MojoodyBankId { get; set; }
        public string Description { get; set; }
        public int TypeHazinehId { get; set; }
        public string Date { get; set; }
        public Nullable<int> Before { get; set; }
        public Nullable<int> After { get; set; }
        public Nullable<bool> DaramadORHazineh { get; set; }
    
        public virtual MojoodyBank MojoodyBank { get; set; }
        public virtual TypeHazineh TypeHazineh { get; set; }
    }
}

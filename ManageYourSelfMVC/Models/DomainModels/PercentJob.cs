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
    
    public partial class PercentJob
    {
        public int PercentId { get; set; }
        public int JobId { get; set; }
        public int PercentValue { get; set; }
        public string Date { get; set; }
    
        public virtual Job Job { get; set; }
    }
}

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
    
    public partial class KarKard
    {
        public int KarkardId { get; set; }
        public Nullable<int> JobId { get; set; }
        public string DayDate { get; set; }
        public int SpendTimeMinute { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<System.DateTime> MiladyDate { get; set; }
    
        public virtual Job Job { get; set; }
    }
}

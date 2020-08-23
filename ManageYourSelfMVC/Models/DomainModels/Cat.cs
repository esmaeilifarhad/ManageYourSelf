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
    
    public partial class Cat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cat()
        {
            this.Sports = new HashSet<Sport>();
            this.Tasks = new HashSet<Task>();
        }
    
        public int CatId { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }
        public string Dsc { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<int> test { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sport> Sports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

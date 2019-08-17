using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.HazineMohem
{
    public class VMHazineMohem
    {
        public int KharjId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Rial { get; set; }
        public string Description { get; set; }
        public string DateKharj { get; set; }
        public string KharjTypeName { get; set; }
    }
}
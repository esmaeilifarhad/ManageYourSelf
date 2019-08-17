using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class DaramadVM:Models.DomainModels.Daramad
    {
        public List<Models.DomainModels.MojoodyBank> lstMojoodyBank { get; set; }
        public List<Models.DomainModels.TypeHazineh> lstTypeHazineh { get; set; }
        //public Models.DomainModels.Daramad Daramad { get; set; }
        //public Models.DomainModels.MojoodyBank MojoodyBank { get; set; }
        //public Models.DomainModels.TypeHazineh TypeHazineh { get; set; }
        public string CurrentDate { get; set; }
        public bool DariaftPardakht { get; set; }
        public string MojoodyBankName { get; set; }
       // public int MojoodyBankId { get; set; }
    }
}
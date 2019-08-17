using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VMSport:Models.DomainModels.Sport
    {
        public List<Models.DomainModels.Cat> lstCat { get; set; }
        public string Title { get; set; }
    }
}
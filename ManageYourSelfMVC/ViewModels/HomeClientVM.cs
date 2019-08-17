using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class HomeClientVM
    {
        public List<Models.DomainModels.MVCHomeHeaderThree> MVCHomeHeaderThree { get; set; }
        public List<Models.DomainModels.SliderPhoto> SliderPhoto { get; set; }
    }
}
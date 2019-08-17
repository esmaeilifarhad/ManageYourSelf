using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class MenuVm
    {
        public List<Models.DomainModels.Menu> MenuLst { get; set; }
        public List<Models.DomainModels.Menuha> MenuhaLst { get; set; }
    }
}
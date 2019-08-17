using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.BaseData
{
    public class JobVM:Models.DomainModels.Job
    {
        public string NameCategory { get; set; }
        public List<Models.DomainModels.Category> ListCategory { get; set; }
    }
}
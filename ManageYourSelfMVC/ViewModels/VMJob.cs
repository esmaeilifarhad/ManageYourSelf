using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VMJob:Models.DomainModels.Job
    {
        public string CurrentDate { get; set; }
    }
}
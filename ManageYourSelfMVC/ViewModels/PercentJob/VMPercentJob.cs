using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.PercentJob
{
    public class VMPercentJob:Models.DomainModels.PercentJob
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public double PercentJobInMounth { get; set; }
        public double PercentOneMinute { get; set; }


    }
}
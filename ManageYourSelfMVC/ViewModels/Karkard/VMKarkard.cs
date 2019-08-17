using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Karkard
{
    public class VMKarkard:Models.DomainModels.KarKard
    {
        public string TimePer { get; set; }
        public string JobName { get; set; }
        public int JobId { get; set; }
        public DataTable ShowKarkadPivotNotParamHeader { get; set; }
    }
}
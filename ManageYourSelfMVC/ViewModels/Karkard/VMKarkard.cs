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
        public string Date { get; set; }
        public int JobId { get; set; }
        public int Row { get; set; }
        public int weekday { get; set; }
        

        public DataTable ShowKarkadPivotNotParamHeader { get; set; }
    }
}
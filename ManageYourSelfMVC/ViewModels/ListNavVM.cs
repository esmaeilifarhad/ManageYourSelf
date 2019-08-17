using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class ListNavVM
    {
        public List<Models.DomainModels.dic_tbl> dic_tbl { get; set; }
        public List<Models.DomainModels.MojoodyBank> MojoodyBank { get; set; }
        public DataTable ShowKarkadPivotNotParamHeader { get; set; }
        public List<ViewModels.TaskVM> ListTask { get; set; }
    }
}
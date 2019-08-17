using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VMPivot
    {
        public DataTable RoutineJob { get; set; }
        public DataTable Timing { get; set; }
    }
}
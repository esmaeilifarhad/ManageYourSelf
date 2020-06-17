using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Task
{
    public class Task
    {
        public List<RateTaskDays> lstRateTaskDays { get; set; }
        public List<ListTaskFuture> lstListTaskFuture { get; set; }
    }
}
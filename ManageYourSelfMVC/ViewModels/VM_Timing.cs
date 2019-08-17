using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VM_Timing:VM_Master
    {
        public List<Models.DomainModels.Task> lstTask { get; set; }
        public List<Models.DomainModels.ManageTime> lstManageTime { get; set; }
        public int TaskId { get; set; }
        public Models.DomainModels.Task Task { get; set; }
        public int CurrentHour { get; set; }
    }
}
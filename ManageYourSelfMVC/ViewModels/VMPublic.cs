using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VMPublic:Models.DomainModels.RoutineJob
    {
        public int RoutineJobId { get; set; }
        public int RoozDailySplit { get; set; }
        public string Job { get; set; }
        public string Currentdate { get; set; }
        public bool Check { get; set; }
    }
}
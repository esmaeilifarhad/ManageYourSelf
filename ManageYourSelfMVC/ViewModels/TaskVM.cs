using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class TaskVM:Models.DomainModels.Task
    {
        //public int TaskId { get; set; }
        //public string Name { get; set; }
        //public string DateStart { get; set; }
        //public string DateEnd { get; set; }
        
        //public Nullable<bool> IsActive { get; set; }
        //public Nullable<bool> IsCheck { get; set; }
        //public Nullable<int> DarsadPishraft { get; set; }
        public int Gozashteh { get; set; }
        public int MandehRooz { get; set; }
        public string Label { get; set; }
        public string Title { get; set; }
    }
}
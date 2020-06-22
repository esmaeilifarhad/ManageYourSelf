using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Task
{


    public class VMTask : ITask,ITiming
    {
      
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int Olaviat { get; set; }
        public int Rate { get; set; }
        public int TimingId  { get; set; }
        public int Value { get; set; }
        public string Label { get; set; }

    }
}
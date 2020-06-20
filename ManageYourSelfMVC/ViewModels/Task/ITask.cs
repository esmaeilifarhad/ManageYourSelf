using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Task
{
    public interface ITask
    {
         int TaskId { get; set; }
         string Name { get; set; }
         string DateStart { get; set; }
         string DateEnd { get; set; }
         int Olaviat { get; set; }
         int Rate { get; set; }
       
    }
}
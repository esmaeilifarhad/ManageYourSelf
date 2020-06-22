using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Task
{
    public interface ITiming
    {
         int TimingId { get; set; }
        string Label { get; set; }
        int Value { get; set; }
    }
}
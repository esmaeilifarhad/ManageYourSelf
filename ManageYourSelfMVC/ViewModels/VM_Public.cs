using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VM_Public
    {
        public string Currentdate { get; set; }
        public List<Models.DomainModels.Job> ListJob { get; set; }
        public List<Models.DomainModels.Cat> ListCat { get; set; }
        public List<ViewModels.TaskVM> ListTask { get; set; }
        public Models.DomainModels.Task Task { get; set; }
        public Models.DomainModels.Cat Cat { get; set; }
        public string CurrentDate {
            get {
              
             return  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
        }
        public string CurrentDate6Char
        {
            get
            {

                return  Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            }
        }
        public string labelManageTime { get; set; }

       
    }


}
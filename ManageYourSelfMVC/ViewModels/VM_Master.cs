using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VM_Master
    {
        public string CurrentDate
        {
            get
            {

                return Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
        }
    }
}
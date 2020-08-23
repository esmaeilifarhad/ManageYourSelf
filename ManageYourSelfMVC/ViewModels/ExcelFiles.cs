using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class ExcelFiles
    {
        public string FileName { get; set; }
        public string FileAddress { get; set; }
        public string ShamsyDate { get; set; }
        public string SheetName { get; set; }
        public int Row { get; set; }
    }
}
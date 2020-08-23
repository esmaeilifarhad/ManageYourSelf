using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class NamadDetail
    {
        public string Namad { get; set; }
        public string Name { get; set; }
        public Int64 Hajm { get; set; }
        public float DarsadGheymatPayany { get; set; }
        public Int64 GheymatPayany { get; set; }
        public string CodeSherkat { get; set; }
        public int TedadMoamelat { get; set; }
        public string DateShamsy { get; set; }
    }
    public class AllNamad
    {
        public List<NamadDetail> lstNamadDetail { get; set; }
        public string ShamsyDate { get; set; }
    }
    public class ExcelSheetData
    {
        public DataTable lstDataTable { get; set; }
        public string fileName { get; set; }
    }
}
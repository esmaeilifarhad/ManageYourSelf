using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class VDataCard
    {
        public DataTable ShowTaradod { get; set; }
        public DataTable Rotbehshakhsy { get; set; }
        /*
        personelid,datecard2,chandshanbeh,i,o,diff ,rtb,TedadKol
        */
        public string personelid { get; set; }
        public string DateCard { get; set; }
        public string chandshanbeh { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public string diff { get; set; }
        public int rtb { get; set; }
        public string TedadKol { get; set; }

    }
}
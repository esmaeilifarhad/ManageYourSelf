using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class DakhloKharj
    {
        public List<Models.DomainModels.MojoodyBank> lstMojoodyBank { get; set; }
        public List<Models.DomainModels.TypeHazineh> lstTypeHazineh { get; set; }
        public List<Models.DomainModels.Daramad> lstDaramad { get; set; }
        public Models.DomainModels.Daramad Daramad { get; set; }
        public Models.DomainModels.TypeHazineh TypeHazineh { get; set; }
        public Models.DomainModels.MojoodyBank MojoodyBank { get; set; }
        public ReportMahane ReportMahane { get; set; }
        public List<ReportMahane> lstReportMahane { get; set; }
    }
    public class ReportMahane
    {
        public int TypeHazinehId { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public int Rial { get; set; }
    }
}
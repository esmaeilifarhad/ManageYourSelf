using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Task
{
    public class ListTaskFuture
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int Olaviat { get; set; }
        public int CatId { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public int IsHolyDay { get; set; }
        public string ChandShanbeh { get; set; }
        public int HafteChandom { get; set; }
        public int MaheChandom { get; set; }
        public int SaleChandom { get; set; }
    }
}
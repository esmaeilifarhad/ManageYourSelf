using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Dictionary
{
    public class VMDictionary:Models.DomainModels.dic_tbl
    {
        // CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount)
        public int CountMoroor { get; set; }
        public int Grade { get; set; }
        public int CountOfWord { get; set; }
        public int CountLevel { get; set; }
        public string NameLevel { get; set; }
        public int HasExample { get; set; }
        public int ExampleId { get; set; }
        public int Radif { get; set; }
        public bool statusCheck { get; set; }
        public List<Models.DomainModels.example_tbl> lstExample { get; set; }
    }
}
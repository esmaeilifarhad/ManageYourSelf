using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class DictionaryVM
    {
        public int id { get; set; }
        public string eng { get; set; }
        public string per { get; set; }
        public string Phonetic { get; set; }
        public Nullable<int> level { get; set; }
        public string date_s { get; set; }
        public string date_refresh { get; set; }
        public Nullable<int> SuccessCount { get; set; }
        public Nullable<int> UnSuccessCount { get; set; }
        public int HasExample { get; set; }
    }
}
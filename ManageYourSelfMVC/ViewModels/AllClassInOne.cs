using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class AllClassInOne
    {
        public int MyProperty { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class List
    {
        public int id { get; set; }
        public string nameFa { get; set; }
        public int price { get; set; }
        public string changeStatus { get; set; }
        public double? changePercent { get; set; }
        public int? changePrice { get; set; }
    }

    public class Root
    {
        public bool ok { get; set; }
        public List<object> timeUpdate { get; set; }
        public List<object> timeCurrent { get; set; }
        public List<List> list { get; set; }
    }
}
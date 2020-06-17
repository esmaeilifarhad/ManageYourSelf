using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.Saham
{
    public class NamadVM:Models.DomainModels.NamadDetail
    {
       // public int NamadI { get; set; }
        public Int64 Avgg { get; set; }
        public float Rate { get; set; }
        public float SumDarsadGheymatPayany { get; set; }
        public int Summ { get; set; }
        public string tseAddress { get; set; }
        public string NamadName { get; set; }
        public string namadNameTse { get; set; }
        

        public int IdNamad { get; set; }
        public int TedadP { get; set; }
        public int TedadM { get; set; }
        public int Tedad { get; set; }
        public int IdRahavard { get; set; }
        public string tseId { get; set; }
    }
}
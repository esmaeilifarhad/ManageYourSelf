using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ManageYourSelfMVC.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
           // bundles.IgnoreList.Clear();

         

            bundles.Add(new ScriptBundle("~/bundles/MyTemplate").Include(
                      "~/MyTemplate/MasterTemplate/lib/jquery/jquery-migrate.min.js",
                      "~/MyTemplate/MasterTemplate/lib/jquery/jquery.min.js",
                      "~/MyTemplate/MasterTemplate/lib/bootstrap/js/bootstrap.bundle.min.js",
                      "~/MyTemplate/MasterTemplate/lib/easing/easing.min.js",
                      "~/MyTemplate/MasterTemplate/lib/superfish/hoverIntent.js",
                      "~/MyTemplate/MasterTemplate/lib/superfish/superfish.min.js",
                      "~/MyTemplate/MasterTemplate/lib/wow/wow.min.js",
                      "~/MyTemplate/MasterTemplate/lib/waypoints/waypoints.min.js",
                      "~/MyTemplate/MasterTemplate/lib/counterup/counterup.min.js",
                      "~/MyTemplate/MasterTemplate/lib/owlcarousel/owl.carousel.min.js",
                      "~/MyTemplate/MasterTemplate/lib/isotope/isotope.pkgd.min.js",
                      "~/MyTemplate/MasterTemplate/lib/lightbox/js/lightbox.min.js",
                      "~/MyTemplate/MasterTemplate/lib/touchSwipe/jquery.touchSwipe.min.js",
                      "~/MyTemplate/MasterTemplate/contactform/contactform.js",
                      "~/MyTemplate/MasterTemplate/js/main.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Persian_Jalali_Calendar").Include(
                     "~/Scripts/Persian-Jalali-Calendar-Data-Picker-Plugin-With-jQuery-kamaDatepicker/src/kamadatepicker.js"));

            
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }

    }
}
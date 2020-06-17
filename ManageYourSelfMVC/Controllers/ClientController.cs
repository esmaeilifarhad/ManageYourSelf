using ManageYourSelfMVC.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace ManageYourSelfMVC.Controllers
{
    //[Models.Filtering.Filter]
    public class ClientController : Controller
    {
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        // GET: Client
        public ActionResult Home()
        {
            Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            ViewModels.HomeClientVM VM = new ViewModels.HomeClientVM();
            //Photo Unser Slider Show
            Models.Repository.MVCHomeHeaderThreeRepository RepM = new Models.Repository.MVCHomeHeaderThreeRepository();
            List<Models.DomainModels.MVCHomeHeaderThree> ListMVCHomeHeaderThree = RepM.Select().ToList();
            VM.MVCHomeHeaderThree = ListMVCHomeHeaderThree;
            //SliderShow
            List<Models.DomainModels.SliderPhoto> SliderShow = DB.SliderPhotoes.Select(q => q).ToList();
            VM.SliderPhoto = SliderShow;

            return View(VM);
        }

        public ActionResult MainHome()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        

        [HttpGet]
        // [Security.CustomAthorize(Roles = "Karbari")]
        [Models.Filtering.Filter]
        public ActionResult ListNav()
        {
   
            var obj = T.ListNavVM();
            return View("ListNav", obj);
        }


    }
}
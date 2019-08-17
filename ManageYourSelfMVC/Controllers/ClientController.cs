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
        // [Models.Filtering.Filter]

        [HttpGet]
        [Security.CustomAthorize(Roles = "Karbari")]
        public ActionResult ListNav()
        {
            /*
            try
            {
                string _NameUser = System.Web.HttpContext.Current.Session["_NameUser"].ToString();
                Thread thread = new Thread(() => Utility.Utility.SendMail(_NameUser));
                thread.Start();
            }
            catch (Exception ex)
            {
                //TempData["Error"] = ex.ToString();
                TempData["Message"]= ex.ToString();
            }
            */
            /*
            GMailer.GmailUsername = "FeMyHostSender@gmail.com";
            GMailer.GmailPassword = "861130928";

            GMailer mailer = new GMailer();
            mailer.ToEmail = "esmaili.farhad67@gmail.com";
            mailer.Subject =Utility.Utility.shamsi_date();
            mailer.Body = _NameUser + " در ساعت " + DateTime.Now.ToString("hh:mm:ss") + " وارد بخش فوتبال شد ";
            mailer.IsHtml = true;
            mailer.Send();

            GMailer mailer2 = new GMailer();
            mailer2.ToEmail = "esmaeili.farhad@golrang.com";
            mailer2.Subject = Utility.Utility.shamsi_date();
            mailer2.Body = _NameUser + " در ساعت " + DateTime.Now.ToString("hh:mm:ss") + " وارد بخش فوتبال شد ";
            mailer2.IsHtml = true;
            mailer2.Send();
            */
            Configuration conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            SessionStateSection section = (SessionStateSection)conf.GetSection("system.web/sessionState");
            int timeout = (int)section.Timeout.TotalMinutes;

       


            var obj = T.ListNavVM();
            return View("ListNav", obj);
        }

        //[HttpGet]
        //public JsonResult ExampleList(int id)
        //{
        //    //Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        //    //List<Models.DomainModels.example_tbl> lstExample = T.ExampleList(id);
        //    //return Json(lstExample, JsonRequestBehavior.AllowGet);
        //}

    }
}
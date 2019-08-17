using ManageYourSelfMVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAthorize(Roles = "superadmin")]
        public ActionResult Work1()
        {
            return View("Work1");
        }
        [CustomAthorize(Roles = "superadmin,admin")]
        public ActionResult Work2()
        {
            return View("Work2");
        }
        [CustomAthorize(Roles = "superadmin,admin,employee")]
        public ActionResult Work3()
        {
            return View("Work3");
        }
    }
}
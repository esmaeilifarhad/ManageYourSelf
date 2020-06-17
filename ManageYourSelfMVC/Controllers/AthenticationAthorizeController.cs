using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class AthenticationAthorizeController : Controller
    {
        // GET: AthenticationAthorize
        [HttpPost]
        public ActionResult Login(string Username,string Password)
        {
            return View();
        }
    }
}
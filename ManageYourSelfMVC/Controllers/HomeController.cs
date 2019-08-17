using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult ListWordExampleFalse()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AjaxMethod(string name)
        {
            ViewModels.PersonelModel person = new ViewModels.PersonelModel
            {
                Name = name,
                DateTime = DateTime.Now.ToString()
            };
            return Json(person, JsonRequestBehavior.AllowGet);
        }
    }
}
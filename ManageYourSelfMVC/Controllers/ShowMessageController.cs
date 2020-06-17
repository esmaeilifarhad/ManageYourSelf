using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class ShowMessageController : Controller
    {
        // GET: ShowMessage
        public ActionResult message()
        {
            ViewModels.ErrorMessage E = new ViewModels.ErrorMessage();
            E.message = "شما به این بخش دسترسی ندارید";
           
            var s = Models.staticClass.staticClass.UserId;
            return Json(E, JsonRequestBehavior.AllowGet);
        }
    }
}
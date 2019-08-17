using ManageYourSelfMVC.Models.Account;
using ManageYourSelfMVC.Security;
using ManageYourSelfMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class AccountController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        // GET: Account
        public ActionResult Index()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            AccountModel am = new AccountModel();
            if (string.IsNullOrEmpty(avm.Account.UserName) || string.IsNullOrEmpty(avm.Account.Password) || am.login(avm.Account.UserName, avm.Account.Password) == null)
            {
                ViewBag.Error = "Account's Invalid";
                return PartialView("Index");
            }
            //در صورتی که کد کاربری درست باشد
            //مقدار سشن را با یوزرنیم پر مینماییم
            SessionPersister.Username = avm.Account.UserName;

           

            //SessionPersister.UserId=در دیتابیس بگرد و UserId را پیدا کن
            return PartialView("Success");
        }
        public ActionResult Logout()
        {
            SessionPersister.Username = string.Empty;
            Session["_NameUser"] = string.Empty;
            Session["UserId"] = string.Empty;
            return RedirectToAction("Index");
        }
    }
}
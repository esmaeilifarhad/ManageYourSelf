using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Models.Filtering
{
    public class FilterSaham : ActionFilterAttribute
    {
        public FilterSaham()
        {

        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            Models.DomainModels.ManageYourSelfEntities DB = new DomainModels.ManageYourSelfEntities();
            string Username = string.Empty;
            string Password = string.Empty;
            var ActionName = filterContext.ActionDescriptor.ActionName;
            var ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;



            var UsernameCookie = filterContext.HttpContext.Request.Cookies["SUsername"];
            var PasswordCookie = filterContext.HttpContext.Request.Cookies["SPassword"];
            if (UsernameCookie != null && PasswordCookie != null)
            {
                Username = UsernameCookie.Value;
                Password = PasswordCookie.Value;

                if (Username == "" && Password == "")
                {

                    filterContext.HttpContext.Response.Write("ssss");
                  //  filterContext.Result = new RedirectResult(@"http://localhost:1812/ShowMessage/message");
                    return;
                }
                var OldUser = DB.Users.SingleOrDefault(q => q.UserName == Username && q.Password == Password);
                if (OldUser == null && Username != null && Password != null)
                {
                    Models.DomainModels.User U = new DomainModels.User();
                    U.UserName = Username;
                    U.Password = Password;
                    DB.Users.Add(U);
                    DB.SaveChanges();
                }
                if (OldUser != null && Username != null && Password != null)
                {
                    //-------------------------create cookie
                    HttpCookie myCookie = new HttpCookie("SName");
                    myCookie.Value = OldUser.Name;
                    myCookie.Expires = DateTime.Now.AddDays(1d);
                    filterContext.HttpContext.Response.Cookies.Add(myCookie);
                    //-----------------------------------------------------
                    Models.staticClass.staticClass.UserId = OldUser.UserId;
                }
            }
            else
            {
              //  filterContext.Result = new EmptyResult();
              //  filterContext.Result = new RedirectToAction("message", "ShowMessage");


                // filterContext.HttpContext.Response.Write("ssss");
                filterContext.Result = new RedirectResult(@"http://localhost:1812/ShowMessage/message");
                return;
            }
            //if (ControllerName == "Saham")
            //{
            //    if (ActionName == "CompareToAvg")
            //    {
            //        if (Username == "fery")
            //        {
            //            filterContext.Result = new RedirectResult(@"http://localhost:1812/Saham/showMessage");
            //            return;
            //        }
            //    }

            //}
            //else
            //{
            //}


        }
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        public override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

    }
}
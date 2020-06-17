using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace ManageYourSelfMVC.Models.Filtering
{
    public class Filter : ActionFilterAttribute
    {
        public Filter()
        {

            //string s = "";
            //var controllers = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).Select(t => t);
            //foreach (Type controller in controllers)
            //{
            //    var actions = controller.GetMethods().Where(t => t.Name != "Dispose" && !t.IsSpecialName && t.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && t.IsPublic && !t.IsStatic).ToList();
            //    foreach (var action in actions)
            //    {
            //        var myAttributes = action.GetCustomAttributes(false);
            //        for (int j = 0; j < myAttributes.Length; j++)
            //            s += string.Format("ActionName: {0}, Attribute: {1}<br>", action.Name, myAttributes[j]);


            //    }
            //}

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
                    filterContext.Result = new RedirectResult(@"http://pushakshik.ir/Account/Index");
                    //filterContext.HttpContext.Response.Write("ssss");
                    //  filterContext.Result = new RedirectResult(@"http://localhost:1812/ShowMessage/message");
                    return;
                }
                var OldUser = DB.Users.SingleOrDefault(q => q.UserName == Username && q.Password == Password);
                if (OldUser == null && Username != null && Password != null)
                {
                   // filterContext.Result = new RedirectResult(@"http://localhost:1812/Account/Index");
                    filterContext.Result = new RedirectResult(@"http://pushakshik.ir/Account/Index");
                    //filterContext.Result = new EmptyResult();
                    /*
                    Models.DomainModels.User U = new DomainModels.User();
                    U.UserName = Username;
                    U.Password = Password;
                    DB.Users.Add(U);
                    DB.SaveChanges();
                    */
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
                    if (ActionName == "InsertUpdateJson" && ControllerName == "Saham")
                    {
                        //if (OldUser.UserId != 1 || OldUser.UserId != 36)
                        //{
                        //    filterContext.Result = new RedirectResult(@"http://localhost:1812/ShowMessage/message");
                        //}

                    }
                }
            }
            else
            {
                //  filterContext.Result = new EmptyResult();
                //  filterContext.Result = new RedirectToAction("message", "ShowMessage");


                // filterContext.HttpContext.Response.Write("ssss");
                //filterContext.Result = new RedirectResult(@"http://localhost:1812/ShowMessage/message");
                //filterContext.Result = new RedirectResult(@"http://localhost:1812/Account/Index");
                filterContext.Result = new RedirectResult(@"http://pushakshik.ir/Account/Index");
                

                return;
            }
          
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
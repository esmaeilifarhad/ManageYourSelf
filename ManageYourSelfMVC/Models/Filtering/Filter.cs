using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ManageYourSelfMVC.Models.Filtering
{
    public class Filter : ActionFilterAttribute
    {
        public Filter()
        {

        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url;
            var Segment = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            //var MyHost = HttpContext.Current.Request.Url.AbsolutePath.Split('//');
            var h = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1].Split('.')[0].TrimStart('/');
            string Scheme = string.Empty;
            string Host = string.Empty;
            string Path = string.Empty;
            string QueryStrin = string.Empty;

            bool Result = false;
            //if (filterContext.HttpContext.Session["UserId"] != null)
            //{
            //    Models.CheckAccess.UserRoleAccess C = new CheckAccess.UserRoleAccess();
            //    Result = C.IsRegister(int.Parse(filterContext.HttpContext.Session["UserId"].ToString()));
            //}

            if (Result == false)
            {
                //RedirectToAction("IdentificationForm", "Identity");


                filterContext.Result = new RedirectResult(@"http://localhost:1812/Register/Registerfrm");
                return;

            }
            else
            {
                //var U = new Models.UIDS.IdentityUser();
                //var lst2 = U.DontAccess(Session["UserName"].ToString());
                //foreach (var item in lst2)
                //{
                //    TempData[item.ControlName] = "none";
                //}
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
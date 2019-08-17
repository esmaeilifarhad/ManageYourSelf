using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class SettingController : Controller
    {
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = 0;
        public SettingController()
        {
            //if(System.Web.HttpContext.Current.Session["UserId"])
            object UserId1 = System.Web.HttpContext.Current.Session["UserId"];
            if (UserId1 == null || UserId1 =="")
            {
                UserId = 0;
            }
            else
            {
                UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
            }
        }
        #endregion

        //شروع فعالیت
        public JsonResult CreateSTime(Models.DomainModels.Setting NewS)
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
           
            try
            {
               
                List<Models.DomainModels.Setting> OldSetting = DB.Settings.Where(q => q.UserId == UserId && q.Key == NewS.Key).ToList();
                if (UserId == 0)
                {
                    Error.result = true;
                    Error.message = "00:00";
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
                if (OldSetting.Count == 0 && NewS.Value!="0" && NewS.Value != "1")
                {
                    NewS.UserId = UserId;
                    NewS.Key = NewS.Key;
                    NewS.Description =NewS.Key== "StartJobTime" ? "شروع فعالیت" : "پایان فعالیت";
                    DB.Settings.Add(NewS);
                    DB.SaveChanges();
                    Error.result = true;
                    Error.message = NewS.Value;
                }
               
                else if (OldSetting.Count == 1)
                {
                  Models.DomainModels.Setting OldS= DB.Settings.SingleOrDefault(q => q.UserId == UserId && q.Key == NewS.Key);
                    if (NewS.Value == "0")
                    {
                        Error.result = true;
                        Error.message = OldS.Value;
                    }
                    else if (NewS.Value == "1")
                    {
                        Error.result = true;
                        Error.message = OldS.Value;
                    }
                    else
                    {
                        OldS.Value = NewS.Value;
                        DB.SaveChanges();
                        Error.result = true;
                        Error.message = NewS.Value;
                    }
                }
                if (UserId != 0 && OldSetting.Count == 0)
                {
                    Error.result = true;
                    Error.message = "00:00";
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                Error.message = ex.InnerException.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);
            }
           
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class HolyDayController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
        #region HolyDay
        public ActionResult ListHolyDay()
        {
            var res = DB.HolyDays.Where(q => q.UserId == UserId).OrderByDescending(q => q.HolyDayRooz).ToList();
            return PartialView(res);
        }
        [HttpPost]
        public JsonResult CreateHolyDay(Models.DomainModels.HolyDay New)
        {
            bool result = false;
            New.UserId = UserId;
            DB.HolyDays.Add(New);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateHolyDay()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditHolyDay(int HolyDayId)
        {
            var HolyDays = DB.HolyDays.SingleOrDefault(q => q.HolyDayId == HolyDayId);
            return PartialView(HolyDays);
        }
        [HttpPost]
        public ActionResult UpdateHolyDay(Models.DomainModels.HolyDay New)
        {
            var Old = DB.HolyDays.SingleOrDefault(q => q.HolyDayId == New.HolyDayId);
            Old.HolyDayRooz = New.HolyDayRooz;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteHolyDay(int HolyDayId)
        {
            bool result = false;
            Models.DomainModels.HolyDay Old = DB.HolyDays.SingleOrDefault(q => q.HolyDayId == HolyDayId);
            DB.HolyDays.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
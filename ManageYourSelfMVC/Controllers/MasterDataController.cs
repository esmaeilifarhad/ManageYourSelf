using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class MasterDataController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
        #region MasterData
        public ActionResult List()
        {
            var res = DB.Cats.Where(q => q.UserId == UserId).OrderByDescending(q => q.Code).ThenBy(q=>q.Order).ToList();
            return PartialView(res);
        }
        [HttpPost]
        public JsonResult Create(Models.DomainModels.Cat New)
        {
            bool result = false;
            New.UserId = UserId;
            DB.Cats.Add(New);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Old = DB.Cats.SingleOrDefault(q => q.CatId == Id);
            return PartialView(Old);
        }
        [HttpPost]
        public ActionResult Update(Models.DomainModels.Cat New)
        {
            var Old = DB.Cats.SingleOrDefault(q => q.CatId == New.CatId);
            Old.Dsc = New.Dsc;
            Old.Title = New.Title;
            Old.Code = New.Code;
            Old.Order = New.Order;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int Id)
        {
            var tasks=DB.Tasks.Where(q => q.CatId == Id);
            foreach (var item in tasks)
            {
                item.CatId = null;
            }
            //کل ورزش ها را حذف میکنم
            var sports = DB.Sports.Where(q=>q.CatId==Id);
            foreach (var item in sports)
            {
                DB.Sports.Remove(item);
            }
            DB.SaveChanges();
            bool result = false;
            var Old = DB.Cats.SingleOrDefault(q => q.CatId == Id);
            DB.Cats.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
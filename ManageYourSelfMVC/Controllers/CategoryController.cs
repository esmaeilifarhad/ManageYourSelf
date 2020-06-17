using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class CategoryController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = Models.staticClass.staticClass.UserId;// (int)System.Web.HttpContext.Current.Session["UserId"];
        // GET: Category
        public ActionResult ListCategory()
        {
            //"~/Views/BaseData/ListCategory",
            List<ManageYourSelfMVC.Models.DomainModels.Category> res = DB.Categories.Where(q=>q.UserId==UserId).ToList();
            return PartialView(res);
        }
        [HttpPost]
        public JsonResult CreateCategory(Models.DomainModels.Category NewCategory)
        {
            bool result = false;
            NewCategory.UserId = UserId;
            DB.Categories.Add(NewCategory);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return PartialView();
        }
        public ActionResult EditCategory(int CategoryId)
        {
            Models.DomainModels.Category OldCategory = DB.Categories.SingleOrDefault(q => q.CategoryId == CategoryId);
            return PartialView(OldCategory);
        }
        public JsonResult UpdateCategory(Models.DomainModels.Category NewCategory)
        {
            bool result=false;
            Models.DomainModels.Category OldCategory =DB.Categories.SingleOrDefault(q => q.CategoryId == NewCategory.CategoryId);
            OldCategory.CategoryName = NewCategory.CategoryName;
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCategory(int CategoryId)
        {
            bool result = false;
            Models.DomainModels.Category OldCategory=DB.Categories.SingleOrDefault(q => q.CategoryId == CategoryId);
            DB.Categories.Remove(OldCategory);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
    }
}
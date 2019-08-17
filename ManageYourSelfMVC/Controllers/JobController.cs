using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class JobController : Controller
    {
        // GET: Job

        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
        // GET: Category
        public ActionResult ListJob()
        {
            List<ManageYourSelfMVC.ViewModels.BaseData.JobVM> lstJobVM=new List<ViewModels.BaseData.JobVM>();
            List<Models.DomainModels.Job> lstJob= DB.Jobs.Where(q=>q.Category.UserId== UserId).OrderBy(q=>q.CategoryId).ToList();
            foreach (var item in lstJob)
            {
                ViewModels.BaseData.JobVM V = new ViewModels.BaseData.JobVM();
                V.CategoryId = item.CategoryId;
                V.Name = item.Name;
                V.Mohasebe = item.Mohasebe;
                V.GridShow = item.GridShow;
                var Cate= DB.Categories.SingleOrDefault(q => q.CategoryId == item.CategoryId);
                V.NameCategory = Cate.CategoryName;
                V.JobId = item.JobId;
                lstJobVM.Add(V);
            }
            return PartialView(lstJobVM);
        }
        [HttpPost]
        public JsonResult CreateJob(Models.DomainModels.Job NewJob)
        {
            bool result = false;
            DB.Jobs.Add(NewJob);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateJob()
        {
            var res= DB.Categories.Where(q=>q.UserId==UserId).OrderBy(q => q.CategoryId).ToList();
            return PartialView(res);
        }
        public ActionResult EditJob(int JobId)
        {
            Models.DomainModels.Job OldJob = DB.Jobs.SingleOrDefault(q => q.JobId == JobId);
            ViewModels.BaseData.JobVM V = new ViewModels.BaseData.JobVM();
            V.CategoryId = OldJob.CategoryId;
            V.JobId = OldJob.JobId;
            V.Name = OldJob.Name;
            V.GridShow = OldJob.GridShow;
            V.Mohasebe = OldJob.Mohasebe;
            V.ListCategory = DB.Categories.Where(q=>q.UserId==UserId).ToList();
            return PartialView(V);
        }
        public JsonResult UpdateJob(Models.DomainModels.Job NewJob)
        {
            bool result = false;
            Models.DomainModels.Job OldJob = DB.Jobs.SingleOrDefault(q => q.JobId == NewJob.JobId);
            OldJob.Name = NewJob.Name;
            OldJob.GridShow = NewJob.GridShow;
            OldJob.Mohasebe = NewJob.Mohasebe;
            OldJob.CategoryId = NewJob.CategoryId;
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteJob(int JobId)
        {
            bool result = false;
            Models.DomainModels.Job OldJob = DB.Jobs.SingleOrDefault(q => q.JobId == JobId);
            DB.Jobs.Remove(OldJob);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
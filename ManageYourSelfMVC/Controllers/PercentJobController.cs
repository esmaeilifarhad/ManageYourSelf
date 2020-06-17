using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class PercentJobController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        string toDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
        string firstDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().Substring(0, 4) + "01";
        //int UserId = (int)System.Web.HttpContext.Current.Session["UserId"];

        int UserId = Models.staticClass.staticClass.UserId;//0;
        public PercentJobController()
        {
            //if(System.Web.HttpContext.Current.Session["UserId"])
            //object UserId1 = System.Web.HttpContext.Current.Session["UserId"];
            //if (UserId1 == null || UserId1 == "")
            //{
            //    UserId = 0;
            //}
            //else
            //{
            //    UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
            //}
        }

        // GET: PercentJob
        public ActionResult List()
        {
            string today= Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            today = today.Substring(0, 6);

            var res = (from P in DB.PercentJobs join J in DB.Jobs on P.JobId equals J.JobId
                       join C in DB.Categories on J.CategoryId equals C.CategoryId
                       where P.Date.Substring(0,6)==today && C.UserId==UserId
                      select new { J.JobId, J.Name, P.PercentId, P.PercentValue,P.Date }).AsEnumerable().Select(x=> new {x.JobId,x.PercentId,x.Name,x.PercentValue,Date= x.Date.Substring(0,4)}).OrderByDescending(q=>q.PercentValue).ToList();
            List<ViewModels.PercentJob.VMPercentJob> lstV = new List<ViewModels.PercentJob.VMPercentJob>();

            int SumInMounth=int.Parse(U.OneRecord(@"select isnull(sum(SpendTimeMinute),0) from KarKard where JobId in (
select JobId from Job where CategoryId in (
select CategoryId from Category where UserId=" + UserId .ToString()+ ")) and  left(DayDate,6)=" + today.ToString()));
//برای هر درصد چند دقیقه باید مطالعه کرد
            int PercentageMinute = int.Parse(U.OneRecord(@"select isnull((sum(SpendTimeMinute)/100),0) from KarKard where JobId in (
select JobId from Job where CategoryId in (
select CategoryId from Category where UserId=" + UserId.ToString() + ")) and  left(DayDate,6)=" + today.ToString()));
//-------------------------------------
            var Karkards=DB.KarKards.Where(q => q.DayDate.Substring(0, 6) == today && q.Job.Category.UserId==UserId);

           
            foreach (var item in res)
            {
                int SumJobid = 0;

                ViewModels.PercentJob.VMPercentJob V = new ViewModels.PercentJob.VMPercentJob();
                V.Date = item.Date;
                V.JobId = item.JobId;
                V.JobName = item.Name;
                V.PercentId = item.PercentId;
                V.PercentValue = item.PercentValue;
                V.PercentOneMinute = PercentageMinute;
                foreach (var item2 in Karkards)
                {
                    if (item.JobId == item2.JobId)
                    {
                        SumJobid = SumJobid + item2.SpendTimeMinute;
                    }
                }
                V.PercentJobInMounth = Math.Round((((double)SumJobid * 100) / SumInMounth),1);
                lstV.Add(V);
            }
            return PartialView(lstV);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var Jobs=DB.Jobs.Where(q=>q.Category.UserId==UserId && q.GridShow==true).ToList();
            return PartialView(Jobs);
        }
        [HttpPost]
        public JsonResult Create(Models.DomainModels.PercentJob New)
        {
            var MyMounth=Utility.Utility.shamsi_date().ConvertDateToSqlFormat().Substring(0,6);
            var res=DB.PercentJobs.SingleOrDefault(q => q.JobId == New.JobId && q.Date == MyMounth);
            if (res == null)
            {
                New.Date = MyMounth;
                DB.PercentJobs.Add(New);
                DB.SaveChanges();
            }
            else
            {
               var Old=DB.PercentJobs.SingleOrDefault(q => q.JobId == New.JobId && q.Date == MyMounth);
                Old.PercentValue = New.PercentValue;
                DB.SaveChanges();
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult Edit(int PercentId)
        {
           Models.DomainModels.PercentJob Old= DB.PercentJobs.SingleOrDefault(q => q.PercentId == PercentId);
            return PartialView(Old);
        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult CreateReport()
        {
           
            return PartialView();
        }
        public ActionResult ListPercentJob(string Date) {
          var res=  DB.PercentJobs.Where(q => q.Date == Date).Select(q=>new {q.Date,q.JobId,q.PercentId,q.PercentValue,q.Job.Name }).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
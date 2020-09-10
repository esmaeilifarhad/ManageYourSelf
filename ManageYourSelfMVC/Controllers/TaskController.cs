using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageYourSelfMVC.Help;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ManageYourSelfMVC.Controllers
{
    [Models.Filtering.Filter]
    public class TaskController : Controller
    {
        // GET: Task
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = Models.staticClass.staticClass.UserId;
        public TaskController()
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
        #endregion
        public ActionResult ExchangeRate()
        
        {
            //https://json2csharp.com/
            try
            {
                //https://hamyarandroid.com/api?t=currency

                // using System.Net;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

                WebRequest request = HttpWebRequest.Create("https://hamyarandroid.com/api?t=currency");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string jsons = reader.ReadToEnd();
                ViewModels.Root r = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewModels.Root>(jsons);

                return Json(r, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public ActionResult ListTaskAnjamnashode(string typeTask, List<string> MyData)
        {

            List<ViewModels.TaskVM> ListTaskVM = T.ListTask(typeTask, UserId, MyData);
            return Json(ListTaskVM, JsonRequestBehavior.AllowGet);
            // return Json(ListTaskVM, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateTask(Models.DomainModels.Task T)
        {
            string[] parts = T.Name.Split(new string[] { "@@" }, StringSplitOptions.None);
            if (parts.Length > 1)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    Models.DomainModels.Task NewTask = new Models.DomainModels.Task();
                    NewTask.Name = parts[i];
                    NewTask.Olaviat = T.Olaviat;
                    NewTask.Rate = T.Rate;
                    NewTask.DarsadPishraft = 0;
                    NewTask.DateStart = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                    NewTask.DateEnd = Utility.Utility.ConvertDateToSqlFormat(T.DateEnd);
                    NewTask.IsActive = true;
                    NewTask.IsCheck = false;
                    NewTask.UserId = UserId;
                    NewTask.CatId = T.CatId;
                    DB.Tasks.Add(NewTask);
                    DB.SaveChanges();
                }

            }
            else
            {
                Models.MyData.MyDataTransfer DT = new Models.MyData.MyDataTransfer();
                T.DarsadPishraft = 0;
                T.DateStart = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                T.DateEnd = Utility.Utility.ConvertDateToSqlFormat(T.DateEnd);
                T.IsActive = true;
                T.IsCheck = false;
                T.UserId = UserId;
                T.Olaviat = T.Olaviat;
                T.Rate = T.Rate;
                T.CatId = T.CatId;
                DB.Tasks.Add(T);
                DB.SaveChanges();

            }



            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateTaskView(int CatId = 0)
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            if (CatId == 0)
            {
                V.Cat = DB.Cats.SingleOrDefault(q => q.CatId == CatId);
                V.ListCat = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
                V.Currentdate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
            else
            {
                V.Cat = DB.Cats.SingleOrDefault(q => q.CatId == CatId);
                V.ListCat = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
                V.Currentdate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
            return PartialView(V);
        }
        [HttpPost]
        public ActionResult EditTask(int TaskId)
        {
            ViewModels.VM_Public T = new ViewModels.VM_Public();
            Models.DomainModels.Task task = new Models.DomainModels.Task();
            List<Models.DomainModels.Cat> lstcat = new List<Models.DomainModels.Cat>();

            // ViewModels.Task.Task T = new ViewModels.Task.Task();
            var oldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
            task.CatId = oldTask.CatId;
            task.DarsadPishraft = oldTask.DarsadPishraft;
            task.DateEnd = oldTask.DateEnd;
            task.DateStart = oldTask.DateStart;
            task.IsActive = oldTask.IsActive;
            task.IsCheck = oldTask.IsCheck;
            task.Name = oldTask.Name;
            task.Olaviat = oldTask.Olaviat;
            task.Rate = oldTask.Rate;
            task.TaskId = oldTask.TaskId;

            T.Task = task;


            List<Models.DomainModels.Cat> lst = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
            foreach (var item in lst)
            {
                Models.DomainModels.Cat C = new Models.DomainModels.Cat();
                C.CatId = item.CatId;
                C.Code = item.Code;
                C.Dsc = item.Dsc;
                C.Order = item.Order;
                C.Title = item.Title;
                lstcat.Add(C);
            }
            T.ListCat = lstcat;


            return Json(T, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeTodayTask(int CatId)
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            var res = DB.Cats.SingleOrDefault(q => q.CatId == CatId);
            V.Cat = res;
            return PartialView(V);
        }
        [HttpPost]
        public ActionResult ChangeTodayTask(int CatId, string DateEnd, bool chkIsTransfer)
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;

            ViewModels.VM_Public V = new ViewModels.VM_Public();
            try
            {
                List<Models.DomainModels.Task> res;
                if (chkIsTransfer == true)
                {
                    res = DB.Tasks.Where(q => q.CatId == CatId && q.IsCheck == false).ToList();
                }
                else
                {
                    res = DB.Tasks.Where(q => q.CatId == CatId && q.DateEnd == V.CurrentDate6Char && q.IsCheck == false).ToList();
                }

                foreach (var item in res)
                {
                    var old = DB.Tasks.SingleOrDefault(q => q.TaskId == item.TaskId);
                    old.DateEnd = Utility.Utility.ConvertDateToSqlFormat(DateEnd);

                }
                if (DB.SaveChanges() > 0)
                {
                    Error.result = true;
                }

            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }


            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TimingTask(int TaskId)
        {
            //var src = DateTime.Now;
            //var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
            DateTime date = DateTime.Now;
            var h = date.Hour;
            var M = date.Minute;
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            ViewModels.VM_Timing V = new ViewModels.VM_Timing();
            V.lstTask = DB.Tasks.Where(q => q.DateEnd == CurrentDate).ToList();
            V.CurrentHour = h;
            //V.lstManageTime = DB.ManageTimes.Where(q => q.Value >= h).ToList();
            V.lstManageTime = DB.ManageTimes.ToList();
            V.TaskId = TaskId;
            V.Task = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
            //int s = TaskId;
            //var Task = T.FindTask(TaskId);
            return PartialView(V);
        }

        [HttpPost]
        public JsonResult CreateTiming(Models.DomainModels.Timing New)
        {
            bool result = false;
            DateTime date = DateTime.Now;
            var h = date.Hour;
            var M = date.Minute;
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            var Timings = DB.Timings.SingleOrDefault(q => q.TaskId == New.TaskId);
            if (Timings != null)
            {
                //--Update
                Timings.ManageTimeId = New.ManageTimeId;
                if (DB.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            else
            {
                //--Insert
                Models.DomainModels.Timing T = new Models.DomainModels.Timing();
                T.ManageTimeId = New.ManageTimeId;
                T.TaskId = New.TaskId;
                DB.Timings.Add(T);
                if (DB.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateTask(Models.DomainModels.Task NewTask)
        {
            string[] parts = new string[0];
            if (NewTask.Name != null)
            {
                parts = NewTask.Name.Split(new string[] { "@@" }, StringSplitOptions.None);

            }
            if (parts.Length > 1)
            {
                int TaskId = NewTask.TaskId;
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);

                for (int i = 0; i < parts.Length; i++)
                {


                    Models.DomainModels.Task newTask = new Models.DomainModels.Task();
                    newTask.IsActive = (NewTask.IsActive == null ? OldTask.IsActive : NewTask.IsActive);
                    newTask.IsCheck = (NewTask.IsCheck == null ? OldTask.IsCheck : NewTask.IsCheck);
                    newTask.DateStart = (NewTask.DateStart == null ? OldTask.DateStart : Utility.Utility.ConvertDateToSqlFormat(NewTask.DateStart));
                    newTask.DateEnd = (NewTask.DateEnd == null ? OldTask.DateEnd : Utility.Utility.ConvertDateToSqlFormat(NewTask.DateEnd));
                    newTask.DarsadPishraft = (NewTask.DarsadPishraft == null ? OldTask.DarsadPishraft : NewTask.DarsadPishraft);
                    newTask.Name = parts[i];
                    newTask.Olaviat = (NewTask.Olaviat == null ? OldTask.Olaviat : NewTask.Olaviat);
                    newTask.Rate = (NewTask.Rate == null ? OldTask.Rate : NewTask.Rate);
                    newTask.CatId = (NewTask.CatId == null ? OldTask.CatId : NewTask.CatId);
                    newTask.UserId = UserId;
                    DB.Tasks.Add(newTask);
                    DB.SaveChanges();
                }
                DB.Tasks.Remove(OldTask);
                DB.SaveChanges();

            }
            else
            {
                int TaskId = NewTask.TaskId;
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
                OldTask.IsActive = (NewTask.IsActive == null ? OldTask.IsActive : NewTask.IsActive);
                OldTask.IsCheck = (NewTask.IsCheck == null ? OldTask.IsCheck : NewTask.IsCheck);
                OldTask.DateStart = (NewTask.DateStart == null ? OldTask.DateStart : Utility.Utility.ConvertDateToSqlFormat(NewTask.DateStart));
                OldTask.DateEnd = (NewTask.DateEnd == null ? OldTask.DateEnd : Utility.Utility.ConvertDateToSqlFormat(NewTask.DateEnd));
                OldTask.DarsadPishraft = (NewTask.DarsadPishraft == null ? OldTask.DarsadPishraft : NewTask.DarsadPishraft);
                OldTask.Name = (NewTask.Name == null ? OldTask.Name : NewTask.Name);
                OldTask.Olaviat = (NewTask.Olaviat == null ? OldTask.Olaviat : NewTask.Olaviat);
                OldTask.Rate = (NewTask.Rate == null ? OldTask.Rate : NewTask.Rate);
                OldTask.CatId = (NewTask.CatId == null ? OldTask.CatId : NewTask.CatId);

                DB.SaveChanges();

            }



            return Json(true, JsonRequestBehavior.AllowGet);




        }
        public JsonResult DeleteTask(int Id)
        {
            try
            {
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == Id);
                DB.Tasks.Remove(OldTask);
                if (DB.SaveChanges() > 0)
                {

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw new ArgumentException("خطا در حذف", ex);
                // return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult TaskToday()
        {
            string res = "";
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            int date = int.Parse(CurrentDate);
            var listtask = DB.Tasks.AsEnumerable().Where(q => int.Parse(q.DateEnd) < date && q.IsActive == true && q.IsCheck == false && q.UserId == UserId);
            foreach (var item in listtask)
            {
                var old = DB.Tasks.SingleOrDefault(q => q.TaskId == item.TaskId);
                old.DateEnd = CurrentDate;

            }
            DB.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListTaskGeneral()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<Models.DomainModels.Cat> lstC = new List<Models.DomainModels.Cat>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
  select Tbl.CatId,isnull(Title,N'تعریف نشده') Title from [5069_ManageYourSelf].[5069_Esmaeili].Cat right join
  (
  select isnull(CatId,0) CatId 
  from [5069_ManageYourSelf].[dbo].[Task] 
  where IsActive=1 and IsCheck=0 and DateEnd=" + Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date()) + @" and UserId=" + UserId + @"
  group by CatId
  ) tbl
  on Cat.CatId=tbl.CatId
  order by [Order] asc
                            ");

                foreach (DataRow item in DT.Rows)
                {
                    Models.DomainModels.Cat C = new Models.DomainModels.Cat();
                    // V.Radif = int.Parse(item["Radif"].ToString());
                    C.CatId = int.Parse(item["CatId"].ToString());
                    C.Title = item["Title"].ToString();
                    lstC.Add(C);
                }
                V.ListCat = lstC;
                string CurrentDate = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                var lstTask = DB.Tasks
                     .Where(q => q.IsActive == true && q.IsCheck == false && q.UserId == UserId && q.DateEnd == CurrentDate)
                     .OrderBy(q => q.DateEnd)
                     .ThenBy(q => q.Olaviat).ToList();
                List<ViewModels.TaskVM> lstTaskVM = new List<ViewModels.TaskVM>();
                foreach (var item in lstTask)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.Olaviat = item.Olaviat;
                    T.TaskId = item.TaskId;
                    T.Name = item.Name;
                    T.CatId = item.CatId;

                    if (DB.Timings.SingleOrDefault(q => q.TaskId == T.TaskId) != null)
                        T.Label = DB.Timings.SingleOrDefault(q => q.TaskId == T.TaskId).ManageTime.Label;
                    else
                        T.Label = "";
                    lstTaskVM.Add(T);
                }
                V.ListTask = lstTaskVM;

                var ListData = from T in DB.Timings
                               join MT in DB.ManageTimes on T.ManageTimeId equals MT.ManageTimeId
                               select new { T.TaskId, MT.Label };

                return PartialView(V);
            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public ActionResult ListTaskAnjamShode(string today)
        {
            ViewModels.Task.Task task = new ViewModels.Task.Task();
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
select * from 
(
select 
Task.TaskId
,Task.Name
,Task.Rate
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,isnull(Cat.CatId,0) CatId
,isnull(Cat.Title,N'بدون عنوان') Title
from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
where Task.IsCheck=1 
and Task.IsActive=1
and Task.UserId=" + UserId + @"
and Task.DateEnd=cast(" + today + @" as nvarchar) 

union All


select   RoutineJob.RoutineJobId as taskId,Job,[Rate],[Date],[Date],[Order],0 as CatId,N'تکراری' as Title
from [5069_ManageYourSelf].[5069_Esmaeili].RoutineJob inner join [5069_ManageYourSelf].[5069_Esmaeili].RoutineJobHa
on RoutineJob.RoutineJobId=RoutineJobHa.RoutineJobId 
where 
 UserId=" + UserId + @"
and [Date]=cast(" + today + @" as nvarchar) 
) as tbl
order by Title,Rate desc
 ");

                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                    T.CatId = int.Parse(item["CatId"].ToString());
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());
                    T.Name = item["Name"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.DateEnd = item["DateEnd"].ToString();
                    T.Title = item["Title"].ToString();
                    T.Rate = int.Parse(item["Rate"].ToString());

                    lstT.Add(T);
                }
                task.lstListTaskFuture = lstT;

                DataTable DT2 = U.Select(@"
select top 30
DateEnd,sum(Rate)  as Rate
from
(
select  
DateEnd,Rate
from Task
where IsCheck=1
and IsActive=1
and UserId=" + UserId + @"
union All
select  [Date],[Rate]
from [5069_ManageYourSelf].[5069_Esmaeili].RoutineJob inner join [5069_ManageYourSelf].[5069_Esmaeili].RoutineJobHa
on RoutineJob.RoutineJobId=RoutineJobHa.RoutineJobId 
where 
UserId=" + UserId + @"
) as tbl
group by DateEnd
order by DateEnd desc

 ");

                List<ViewModels.Task.RateTaskDays> lstR = new List<ViewModels.Task.RateTaskDays>();
                foreach (DataRow item in DT2.Rows)
                {
                    ViewModels.Task.RateTaskDays R = new ViewModels.Task.RateTaskDays();
                    R.Rate = int.Parse(item["Rate"].ToString());
                    R.DateEnd = item["DateEnd"].ToString();

                    lstR.Add(R);
                }
                task.lstRateTaskDays = lstR;

                return Json(task, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.message = ex.ToString();
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public ActionResult SearchTask(string Name)
        {
            ViewModels.Task.Task task = new ViewModels.Task.Task();
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();

                DataTable DT = U.Select(@"
select 
Task.TaskId
,Task.Name
,Task.Rate
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,isnull(Cat.CatId,0) CatId
,isnull(Cat.Title,N'بدون عنوان') Title
from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
where 
Task.Name like N'%" + Name + @"%'
and Task.UserId=" + UserId + @"
order by DateEnd desc,isnull(Olaviat,0)
 ");

                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                    T.CatId = int.Parse(item["CatId"].ToString());
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());

                    T.Name = item["Name"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.DateEnd = item["DateEnd"].ToString();
                    T.Title = item["Title"].ToString();
                    T.Rate = int.Parse(item["Rate"].ToString());

                    lstT.Add(T);
                }
                task.lstListTaskFuture = lstT;
                return Json(task, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.message = ex.ToString();
                Error.result = false;
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ListTaskFuture()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
select 
Task.TaskId
,Task.Name
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,isnull(Cat.CatId,0) CatId
,isnull(Cat.Title,N'بدون عنوان') Title
,ManageTime.Label
,Taghvim.IsHolyDay
,Taghvim.ChandShanbeh
,Taghvim.HafteChandom
,SUBSTRING(Taghvim.DayDate,5,2) MaheChandom
,SUBSTRING(Taghvim.DayDate,1,4) SaleChandom
from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
left join [5069_ManageYourSelf].[5069_Esmaeili].Timing
on Task.TaskId=Timing.TaskId
left join [5069_ManageYourSelf].[5069_Esmaeili].ManageTime
on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
and Task.UserId=" + UserId + @"
order by DateEnd,isnull(Olaviat,0)
 ");

                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                    T.CatId = int.Parse(item["CatId"].ToString());
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());
                    //T.IsHolyDay = int.Parse(item["IsHolyDay"].ToString());
                    //T.HafteChandom = int.Parse(item["HafteChandom"].ToString());
                    //T.MaheChandom = int.Parse(item["MaheChandom"].ToString());
                    //T.SaleChandom = int.Parse(item["SaleChandom"].ToString());

                    T.Name = item["Name"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.DateEnd = item["DateEnd"].ToString();
                    T.Title = item["Title"].ToString();
                    T.Label = item["Label"].ToString();
                    T.ChandShanbeh = item["ChandShanbeh"].ToString();
                    lstT.Add(T);
                }
                return PartialView(lstT);
            }
            catch (Exception ex)
            {
                Error.message = ex.ToString();
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ListTaskFutureChkPost(List<string> MyData)
        {
            string str = string.Empty;
            if (MyData != null)
            {
                str = MyData[0].TrimEnd(',');

                string[] ids = str.Split(',');
            }
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT;
                if (str == "")
                {
                    DT = U.Select(@"
select 
Task.TaskId
,Task.Name
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,Cat.CatId
,Cat.Title
,ManageTime.Label
,Taghvim.IsHolyDay
,Taghvim.ChandShanbeh
,Taghvim.HafteChandom
,SUBSTRING(Taghvim.DayDate,5,2) MaheChandom
,SUBSTRING(Taghvim.DayDate,1,4) SaleChandom
from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
left join [5069_ManageYourSelf].[5069_Esmaeili].Timing
on Task.TaskId=Timing.TaskId
left join [5069_ManageYourSelf].[5069_Esmaeili].ManageTime
on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
--and Cat.CatId in (" + str + @")
and Task.UserId=" + UserId + @"
order by DateEnd,isnull(Olaviat,0)
 ");
                }
                else
                {
                    DT = U.Select(@"
select 
Task.TaskId
,Task.Name
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,Cat.CatId
,Cat.Title
,ManageTime.Label
,Taghvim.IsHolyDay
,Taghvim.ChandShanbeh
,Taghvim.HafteChandom
,SUBSTRING(Taghvim.DayDate,5,2) MaheChandom
,SUBSTRING(Taghvim.DayDate,1,4) SaleChandom
from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
left join [5069_ManageYourSelf].[5069_Esmaeili].Timing
on Task.TaskId=Timing.TaskId
left join [5069_ManageYourSelf].[5069_Esmaeili].ManageTime
on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
and Cat.CatId in (" + str + @")
and Task.UserId=" + UserId + @"
order by DateEnd,isnull(Olaviat,0)
 ");
                }


                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                    T.CatId = int.Parse(item["CatId"].ToString());
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());
                    //T.IsHolyDay = int.Parse(item["IsHolyDay"].ToString());
                    //T.HafteChandom = int.Parse(item["HafteChandom"].ToString());
                    //T.MaheChandom = int.Parse(item["MaheChandom"].ToString());
                    //T.SaleChandom = int.Parse(item["SaleChandom"].ToString());

                    T.Name = item["Name"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.DateEnd = item["DateEnd"].ToString();
                    T.Title = item["Title"].ToString();
                    T.Label = item["Label"].ToString();
                    T.ChandShanbeh = item["ChandShanbeh"].ToString();
                    lstT.Add(T);
                }
                return PartialView("ListTaskFuture", lstT);
            }
            catch (Exception ex)
            {
                Error.message = ex.Message;
                Error.result = false;
                Error.description = "موردی برای مشاهده وجود ندارد";
                //throw new ArgumentException(ex.ToString());
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ListTaskFutureChk()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<Models.DomainModels.Cat> lstCat = new List<Models.DomainModels.Cat>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
SELECT  [CatId]
      ,[Title]
  FROM [5069_ManageYourSelf].[5069_Esmaeili].[Cat]
  where userid=" + UserId + @"
  and code=2
  order by [Order]
 ");

                foreach (DataRow item in DT.Rows)
                {
                    Models.DomainModels.Cat C = new Models.DomainModels.Cat();
                    C.CatId = int.Parse(item["CatId"].ToString());
                    C.Title = item["Title"].ToString();
                    lstCat.Add(C);
                }
                return PartialView(lstCat);
            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ListTiming(int x)
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                ViewModels.VMPivot V = new ViewModels.VMPivot();
                if (x == 0)
                {
                    string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
                    string firestDate = CurrentDate.Substring(0, 4) + "01";

                    Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                    DataTable DT = U.Select(@"exec [5069_ManageYourSelf].[5069_Esmaeili].SP_ManageTime " + CurrentDate + "," + UserId);
                    V.Timing = DT;
                }
                if (x == 1)
                {
                    var today = DateTime.Now;
                    var tomorrow = today.AddDays(1);
                    var yesterday = today.AddDays(-1);

                    string CurrentDate = Utility.Utility.shamsi_dateTomarrow().ConvertDateToSqlFormat();
                    string firestDate = CurrentDate.Substring(0, 4) + "01";
                    // ViewModels.VMPivot V = new ViewModels.VMPivot();
                    Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                    DataTable DT = U.Select(@"exec [5069_ManageYourSelf].[5069_Esmaeili].SP_ManageTime " + CurrentDate + "," + UserId);
                    V.Timing = DT;
                }
                return PartialView(V);
            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }


            //return Json(Error, JsonRequestBehavior.AllowGet);
            // return PartialView(V);
        }
        [HttpPost]
        public ActionResult ListTimingForListTask(int x)
        {
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            List<ViewModels.Task.VMTask> lstVMTask = new List<ViewModels.Task.VMTask>();
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {



                DataTable DT = U.Select(@"select * 
from [5069_ManageYourSelf].[5069_Esmaeili].Timing  inner join Task 
on [5069_ManageYourSelf].[5069_Esmaeili].Timing.TaskId=Task.TaskId
inner join [5069_ManageYourSelf].[5069_Esmaeili].ManageTime
on Timing.ManageTimeId=ManageTime.ManageTimeId
where IsCheck=0 and UserId=" + UserId + @"
order by DateEnd,Value,Olaviat");

                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.VMTask V = new ViewModels.Task.VMTask();
                    V.DateEnd = item["DateEnd"].ToString();
                    V.Name = item["Name"].ToString();
                    V.TaskId = int.Parse(item["TaskId"].ToString());
                    V.Rate = int.Parse(item["Rate"].ToString());
                    V.Olaviat = int.Parse(item["Olaviat"].ToString());
                    V.TimingId = int.Parse(item["TimingId"].ToString());
                    V.Label = item["Label"].ToString();
                    V.Value = int.Parse(item["Value"].ToString());
                    lstVMTask.Add(V);

                }

                return Json(lstVMTask, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }


            //return Json(Error, JsonRequestBehavior.AllowGet);
            // return PartialView(V);
        }
        [HttpPost]
        public ActionResult DeleteTiming()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;

            ViewModels.VM_Public V = new ViewModels.VM_Public();
            try
            {
                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                bool b = U.Delete(@"  delete from Timing where TimingId in
  (
  select TimingId from Task Ta inner join Timing Ti
  on Ta.TaskId=Ti.TaskId
  where Ta.UserId=" + UserId + @"
  )");


                Error.result = b;


            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }


            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult removeTimeTask(int TaskId)
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;

            ViewModels.VM_Public V = new ViewModels.VM_Public();
            try
            {
                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                bool b = U.Delete(@"  delete from Timing where TaskId=" + TaskId);


                Error.result = b;


            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }


            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TaskUpLevel(int TaskId)
        {
            try
            {
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
                if (OldTask.Olaviat == null)
                {
                    OldTask.Olaviat = 0;
                }
                OldTask.Olaviat = OldTask.Olaviat + 1;
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        [HttpPost]
        public JsonResult TaskDownLevel(int TaskId)
        {
            try
            {
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
                if (OldTask.Olaviat == null)
                {
                    OldTask.Olaviat = 0;
                }
                OldTask.Olaviat = OldTask.Olaviat - 1;
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        /// <summary>
        /// نمایش رکورد های دارای اولویت 1 در مسترپیج
        /// </summary>
        /// <returns></returns>
        public ActionResult ListTaslLevelHigh()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
select 
Task.TaskId
,Task.Name
,Task.DateStart
,Task.DateEnd
,isnull(Task.Olaviat,0) Olaviat
,isnull(Cat.CatId,0) CatId
,isnull(Cat.Title,N'بدون عنوان') Title
,isnull(ManageTime.Label,'NoClock') Label
,Taghvim.IsHolyDay
,Taghvim.ChandShanbeh
,Taghvim.HafteChandom
,SUBSTRING(Taghvim.DayDate,5,2) MaheChandom
,SUBSTRING(Taghvim.DayDate,1,4) SaleChandom

from task left join [5069_ManageYourSelf].[5069_Esmaeili].Cat 
on task.CatId=Cat.CatId
left join [5069_ManageYourSelf].[5069_Esmaeili].Timing
on Task.TaskId=Timing.TaskId
left join [5069_ManageYourSelf].[5069_Esmaeili].ManageTime

on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
and Task.Olaviat in (1,2,3,4,5)
and Task.DateEnd=" + Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date()) + @"
and Task.UserId=" + UserId + @"
order by isnull(Olaviat,0),Label,Cat.[Order],Cat.Title,Task.DateEnd
 ");

                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                    T.CatId = int.Parse(item["CatId"].ToString());
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());
                    T.IsHolyDay = int.Parse(item["IsHolyDay"].ToString());
                    T.HafteChandom = int.Parse(item["HafteChandom"].ToString());
                    T.MaheChandom = int.Parse(item["MaheChandom"].ToString());
                    T.SaleChandom = int.Parse(item["SaleChandom"].ToString());

                    T.Name = item["Name"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.DateEnd = item["DateEnd"].ToString();
                    T.Title = item["Title"].ToString();
                    T.Label = item["Label"].ToString();
                    T.ChandShanbeh = item["ChandShanbeh"].ToString();
                    lstT.Add(T);
                }
                return PartialView(lstT);
            }
            catch (Exception ex)
            {
                Error.message = ex.InnerException.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
                return Json(Error, JsonRequestBehavior.AllowGet);

            }
        }
        //upload gile 
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        Models.DomainModels.TaskImage img = new Models.DomainModels.TaskImage();
                        img.TaskId = 11871;
                        img.img = Models.Help.Utility.ConvertToByte(file);
                        img.Name = file.FileName;
                        DB.TaskImages.Add(img);



                        DB.SaveChanges();
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult ImageUpload(ViewModels.TaskImage.TaskImageVM model)
        {
            int TaskImageId = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            if (file != null)
            {
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);

                Models.DomainModels.TaskImage img = new Models.DomainModels.TaskImage();
                img.img = imagebyte;
                img.Name = file.FileName;
                img.TaskId = 11871;
                DB.TaskImages.Add(img);
                DB.SaveChanges();
                TaskImageId = img.TaskImageId;
            }
            return Json(TaskImageId, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImageRetrieve(int imgID)
        {
            var img = DB.TaskImages.SingleOrDefault(q => q.TaskImageId == imgID);
            return File(img.img, "image/jpg");
        }
        public ActionResult Document(int imgID)
        {

            var obj = DB.TaskImages.SingleOrDefault(q => q.TaskImageId == imgID);
            FileContentResult File = new FileContentResult(obj.img, "jpg");
            var imag = new MemoryStream(obj.img);

            string[] stringParts = obj.Name.Split(new char[] { '.' });
            string strType = stringParts[1];
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Name);

            var asciiCode = System.Text.Encoding.ASCII.GetString(obj.img);
            var datas = Convert.FromBase64String(asciiCode.Substring(asciiCode.IndexOf(',') + 1));
            //Set the content type as file extension type
            Response.ContentType = strType;
            //Write the file content
            this.Response.BinaryWrite(datas);
            this.Response.End();
            return new FileStreamResult(Response.OutputStream, obj.Name);
        }
        public ActionResult RenderImageBytes(int id)
        {
            var img = DB.TaskImages.SingleOrDefault(q => q.TaskImageId == id);
            ViewModels.TaskImage.TaskImageVM V = new ViewModels.TaskImage.TaskImageVM();
            V.imgByte = img.img;
            return PartialView(V);
        }
        public FileContentResult GetImage(int id)
        {
            var res = DB.TaskImages.SingleOrDefault(q => q.TaskImageId == id);
            byte[] byteArray = res.img;
            return new FileContentResult(byteArray, "image/jpeg");
        }
        public ActionResult FirstDateTask()
        {
            Models.DomainModels.Task T = new Models.DomainModels.Task();
            var z = DB.Tasks.OrderBy(q => q.DateEnd).Select(q => new { q.DateEnd, q.Name }).First();

            return Json(z, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListTaskDateToDate(string Date1, string Date2)
        {
            var lstTask = DB.Tasks.Where(q => q.DateEnd.
              CompareTo(Date1) >= 0 && q.DateEnd.CompareTo(Date2) <= 0).OrderBy(q => q.DateEnd).ToList();

            List<ViewModels.Task.ListTaskFuture> lstT = new List<ViewModels.Task.ListTaskFuture>();
            foreach (var item in lstTask)
            {

                ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                T.CatId = item.CatId == null ? 0 : (int)item.CatId;
                T.TaskId = item.TaskId;
                T.Olaviat = item.Olaviat == null ? 0 : (int)item.Olaviat;
                T.Name = item.Name;
                T.DateStart = item.DateStart;
                T.DateEnd = item.DateEnd;
                //  T.Title = item.ti;
                T.Rate = item.Rate == null ? 0 : (int)item.Rate;

                lstT.Add(T);

            }

            // var vw_Letters = db.vw_Letters.AsNoTracking().Where(x => x.RecPersianDate.CompareTo(fromDate) >= 0 && x.RecPersianDate.CompareTo(toDate) <= 0).ToList();

            return Json(lstT, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveAllTask(int[] TasKIds)
        {

            // users.Where(user => ids.Contains(user.id ?? 0));

            DB.Tasks.RemoveRange(DB.Tasks.Where(x => TasKIds.Contains(x.TaskId)));
            var res = DB.SaveChanges();

            //DB.Tasks.RemoveRange(TaslIds);
            //DB.SaveChanges();

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
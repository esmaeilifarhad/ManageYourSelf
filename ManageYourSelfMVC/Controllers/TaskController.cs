using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageYourSelfMVC.Help;
using System.Data;

namespace ManageYourSelfMVC.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = 0;
        public TaskController()
        {
            //if(System.Web.HttpContext.Current.Session["UserId"])
            object UserId1 = System.Web.HttpContext.Current.Session["UserId"];
            if (UserId1 == null || UserId1 == "")
            {
                UserId = 0;
            }
            else
            {
                UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
            }
        }
        #endregion
        public ActionResult ListTaskAnjamnashode(string typeTask)
        {
            List<ViewModels.TaskVM> ListTaskVM = T.ListTask(typeTask, UserId);
            return PartialView(ListTaskVM);
            // return Json(ListTaskVM, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateTask(Models.DomainModels.Task T)
        {
            bool Result = false;
            Models.MyData.MyDataTransfer DT = new Models.MyData.MyDataTransfer();
            T.DarsadPishraft = 0;
            T.DateStart = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
            T.DateEnd = Utility.Utility.ConvertDateToSqlFormat(T.DateEnd);
            T.IsActive = true;
            T.IsCheck = false;
            T.UserId = UserId;
            T.Olaviat = T.Olaviat;
            Result = DT.TaskInsert(T);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateTaskView(int CatId=0)
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            if (CatId == 0)
            {
                V.Cat = DB.Cats.SingleOrDefault(q => q.CatId == CatId); 
                V.ListCat = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
                V.Currentdate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
            else
            {
                V.Cat=DB.Cats.SingleOrDefault(q =>  q.CatId == CatId);
                V.ListCat = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
                V.Currentdate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            }
            return PartialView(V);
        }
        public ActionResult EditTask(int TaskId)
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            int s = TaskId;
            V.Task = T.FindTask(TaskId);
            V.ListCat = DB.Cats.Where(q => q.UserId == UserId && q.Code == 2).OrderBy(q => q.Order).ToList();
            return PartialView(V);
        }
        public ActionResult ChangeTodayTask(int CatId)
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            var res = DB.Cats.SingleOrDefault(q => q.CatId == CatId);
            V.Cat = res;
            return PartialView(V);
        }
        [HttpPost]
        public ActionResult ChangeTodayTask(int CatId, string DateEnd,bool chkIsTransfer)
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
                    res = DB.Tasks.Where(q => q.CatId == CatId && q.IsCheck==false).ToList();
                }
                else
                {
                     res = DB.Tasks.Where(q => q.CatId == CatId && q.DateEnd == V.CurrentDate6Char).ToList();
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
            try
            {
                int TaskId = NewTask.TaskId;
                var OldTask = DB.Tasks.SingleOrDefault(q => q.TaskId == TaskId);
                OldTask.IsActive = NewTask.IsActive;
                OldTask.IsCheck = NewTask.IsCheck;
                OldTask.DateStart = Utility.Utility.ConvertDateToSqlFormat(NewTask.DateStart);
                OldTask.DateEnd = Utility.Utility.ConvertDateToSqlFormat(NewTask.DateEnd);
                OldTask.DarsadPishraft = NewTask.DarsadPishraft;
                OldTask.Name = NewTask.Name;
                OldTask.Olaviat = NewTask.Olaviat;
                OldTask.CatId = NewTask.CatId;
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
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
  select Tbl.CatId,isnull(Title,N'تعریف نشده') Title from Cat right join
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
from task left join Cat 
on task.CatId=Cat.CatId
left join Timing
on Task.TaskId=Timing.TaskId
left join ManageTime
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
                    T.TaskId =int.Parse(item["TaskId"].ToString());
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
                DataTable DT = U.Select(@"
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
from task left join Cat 
on task.CatId=Cat.CatId
left join Timing
on Task.TaskId=Timing.TaskId
left join ManageTime
on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
and Cat.CatId in (" + str + @")
and Task.UserId=" + UserId + @"
order by DateEnd,isnull(Olaviat,0)
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
                return PartialView("ListTaskFuture", lstT);
            }
            catch (Exception ex)
            {
                Error.message = ex.Message;
                Error.result = false;
                //throw new ArgumentException(ex.InnerException.Message);
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
                    Models.DomainModels.Cat C= new Models.DomainModels.Cat();
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
                    DataTable DT = U.Select(@"exec SP_ManageTime " + CurrentDate+","+UserId);
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
                    DataTable DT = U.Select(@"exec SP_ManageTime " + CurrentDate + "," + UserId);
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
                    OldTask.Olaviat=0;
                }
                OldTask.Olaviat = OldTask.Olaviat+1;
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
,ManageTime.Label
,Taghvim.IsHolyDay
,Taghvim.ChandShanbeh
,Taghvim.HafteChandom
,SUBSTRING(Taghvim.DayDate,5,2) MaheChandom
,SUBSTRING(Taghvim.DayDate,1,4) SaleChandom
from task left join Cat 
on task.CatId=Cat.CatId
left join Timing
on Task.TaskId=Timing.TaskId
left join ManageTime
on ManageTime.ManageTimeId=Timing.ManageTimeId
left join Taghvim on Task.DateEnd=Taghvim.DayDate
where Task.IsCheck=0 
and Task.IsActive=1
and Task.Olaviat=1
and Task.DateEnd=" + Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date()) + @"
and Task.UserId=" + UserId + @"
order by Cat.[Order],Cat.Title,Task.DateEnd,isnull(Olaviat,0)
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

    }
}
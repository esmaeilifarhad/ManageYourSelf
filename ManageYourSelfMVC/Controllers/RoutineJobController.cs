using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class RoutineJobController : Controller
    {
        // GET: RoutineJob
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
       // Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
     
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();

        int UserId = Models.staticClass.staticClass.UserId;// 0;
        public RoutineJobController()
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
        public ActionResult List()
        {
            var res = DB.RoutineJobs.Where(q => q.UserId == UserId).OrderByDescending(q => q.Order).ThenBy(q=>q.Rate).ToList();
            return PartialView(res);
        }
        public JsonResult SaveRoutineJob(List<string> MyData)
        {
            bool result = false;
            foreach (var item in MyData)
            {
              string StrData=  item.TrimEnd(',');
              int RoutineJobId=int.Parse(StrData.Split('_')[0]);                
              string RoozDaily= StrData.Split('_')[1];
              var R=  DB.RoutineJobs.SingleOrDefault(q=>q.RoutineJobId== RoutineJobId);
              R.RoozDaily = RoozDaily;
                if (DB.SaveChanges() > 0)
                    result = true;
              
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoutineJobHa(ViewModels.RoutineJob.RoutineJobVM VM)
        {
            bool result = false;
           
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            //string[] MyDataArray = VM.Date.Split('_');
            int RoutineJobId = VM.RoutineJobId;//int.Parse(MyDataArray[0]);
            string MyDate =VM.Date==null?CurrentDate:VM.Date.ConvertDateToSqlFormat();
            var Exist=DB.RoutineJobHas.SingleOrDefault(q => q.Date == MyDate && q.RoutineJobId == RoutineJobId);
            if (Exist == null && VM.IsCheck==true)
            {
                Models.DomainModels.RoutineJobHa R = new Models.DomainModels.RoutineJobHa();
                R.Date = MyDate;
                R.IsCheck = true;
                R.RoutineJobId = RoutineJobId;
                DB.RoutineJobHas.Add(R);
                DB.SaveChanges();
                result = true;
            }
            else if (Exist != null && VM.IsCheck == false)
            {
                DB.RoutineJobHas.Remove(Exist);
                DB.SaveChanges();
                result = true;
            }
            //foreach (var item in MyData)
            //{
            //    string StrData = item.TrimEnd(',');
            //    int RoutineJobId = int.Parse(StrData.Split('_')[0]);
            //    string RoozDaily = StrData.Split('_')[1];
            //    var R = DB.RoutineJobs.SingleOrDefault(q => q.RoutineJobId == RoutineJobId);
            //    R.RoozDaily = RoozDaily;
            //    if (DB.SaveChanges() > 0)
            //        result = true;

            //}
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Create(Models.DomainModels.RoutineJob New)
        {
            //update
            if (New.RoutineJobId > 0)
            {
                var OldRoutineJob = DB.RoutineJobs.SingleOrDefault(q => q.RoutineJobId == New.RoutineJobId);
                OldRoutineJob.Rate = New.Rate;
                OldRoutineJob.Order = New.Order;
                OldRoutineJob.Job = New.Job;
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //insert
            else
            {
                bool result = false;
                New.UserId = UserId;
                DB.RoutineJobs.Add(New);
                if (DB.SaveChanges() > 0)
                    result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EditRoutineJob(int RoutineJobId)
        {
            try
            {
                var RoutineJobOld = DB.RoutineJobs.SingleOrDefault(q => q.RoutineJobId == RoutineJobId);
                ViewModels.RoutineJob.RoutineJobVMMaster R = new ViewModels.RoutineJob.RoutineJobVMMaster();
                R.Rate = RoutineJobOld.Rate;
                R.RoutineJobId = RoutineJobOld.RoutineJobId;
                R.RoozDaily = RoutineJobOld.RoozDaily;
                R.Order = RoutineJobOld.Order;
                R.Job = RoutineJobOld.Job;
                return Json(R, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException("RoutineJobController/Edit", ex);
            }

          
        }
        public JsonResult Delete(int RoutineJobId)
        {
            bool result = false;
            Models.DomainModels.RoutineJob Old = DB.RoutineJobs.SingleOrDefault(q => q.RoutineJobId == RoutineJobId);
            DB.RoutineJobs.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowPivot()
        {
            #region ShowPivot
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            string firestDate = CurrentDate.Substring(0,6) + "01";
            ViewModels.VMPivot V = new ViewModels.VMPivot();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)
DECLARE @sql AS NVARCHAR(max)

SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((select Job )) 
from RoutineJob where UserId="+UserId.ToString()+@"
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
SELECT -- [RoutineJobHa]
   [IsCheck]
     , T.DayDate
     -- ,R.[RoutineJobId]
  
	  ,T.ChandShanbeh
	  ,J.Job
  FROM taghvim T 
  left join 
  [5069_ManageYourSelf].[5069_Esmaeili].[RoutineJobHa] R
  on T.DayDate=R.Date
  left join RoutineJob J
  on J.RoutineJobId=R.RoutineJobId
  where  T.DayDate between " + firestDate + " and "+ CurrentDate + @"and 
  R.RoutineJobId in (select RoutineJobId from RoutineJob where UserId="+UserId.ToString()+@")
) as OrginalTable
PIVOT
(
Count([IsCheck])
FOR Job IN('+@cols+')
 ) AS PivotTable
ORDER BY DayDate desc
'
EXECUTE(@sql)

--select * from taghvim
");
            DataTable reversedDt = new DataTable();
            // reversedDt = DT.Clone();
            //for (var row = DT.Rows.Count - 1; row >= 0; row--)
            //    reversedDt.ImportRow(DT.Rows[row]);

            //V.RoutineJob = reversedDt;
            V.RoutineJob = DT;
            #endregion
            return PartialView(V);
        }
        public ActionResult RoutineJobCreate(string MyData)
        {
            MyData = MyData.ConvertDateToSqlFormat();
            List<ViewModels.VMPublic> lstV = new List<ViewModels.VMPublic>();
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            if (MyData ==null || MyData=="")
            {
                MyData = CurrentDate;
            }
          
            string RoozHafte= U.OneRecord(" select RoozHafte from Taghvim where DayDate="+ MyData);
          //  var R= DB.RoutineJobs.ToList();
            var R = DB.RoutineJobs.Where(q => q.UserId == UserId).ToList();
            foreach (var item in R)
            {
                string[] Array = item.RoozDaily.Split(',');
                foreach (var item2 in Array)
                {
                    if (RoozHafte == item2)
                    {
                        ViewModels.VMPublic V = new ViewModels.VMPublic();
                        var Exist= DB.RoutineJobHas.SingleOrDefault(q=>q.Date== MyData && q.RoutineJobId== item.RoutineJobId);
                        if (Exist != null)
                        {
                            V.Check = true;
                        }
                        else
                        {
                            V.Check = false;
                        }
                       
                        V.RoutineJobId=item.RoutineJobId;
                        V.RoozDailySplit =int.Parse(item2);
                        V.Job = item.Job;
                        V.Currentdate = MyData.ConvertDateToDateFormat();
                        lstV.Add(V);
                    }
                }
            }
            return PartialView(lstV);
        }

        public ActionResult RoutineJobListMasterPage(string MyData = null)
        {
            MyData = MyData.ConvertDateToSqlFormat();
            List<ViewModels.VMPublic> lstV = new List<ViewModels.VMPublic>();
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            if (MyData == null || MyData == "")
            {
                MyData = CurrentDate;
            }

            string RoozHafte = U.OneRecord(" select RoozHafte from Taghvim where DayDate=" + MyData);
            var R = DB.RoutineJobs.Where(q=>q.UserId==UserId).OrderBy(q=>q.Order).ToList();
            foreach (var item in R)
            {
                string[] Array = item.RoozDaily.Split(',');
                foreach (var item2 in Array)
                {
                    if (RoozHafte == item2)
                    {
                        ViewModels.VMPublic V = new ViewModels.VMPublic();
                        var Exist = DB.RoutineJobHas.SingleOrDefault(q => q.Date == MyData && q.RoutineJobId == item.RoutineJobId);
                        if (Exist != null)
                        {
                            V.Check = true;
                        }
                        else
                        {
                            V.Check = false;
                        }

                        V.RoutineJobId = item.RoutineJobId;
                        V.Rate = item.Rate;
                        V.Order = item.Order;
                        V.RoozDailySplit = int.Parse(item2);
                        V.Job = item.Job;
                        V.Currentdate =  MyData.ConvertDateToDateFormat();
                        lstV.Add(V);
                    }
                }
            }
            return PartialView(lstV);
        }
    }
}
using ManageYourSelfMVC.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class KarkardController : Controller
    {
        Models.Repository.GenericRepository<Models.DomainModels.KarKard> Rep = new Models.Repository.GenericRepository<Models.DomainModels.KarKard>();
        Models.Repository.KarkardsRepository _Repository = new Models.Repository.KarkardsRepository();
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        string toDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
        string firstDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().Substring(0, 6) + "01";
        int UserId = Models.staticClass.staticClass.UserId;//0;
        public KarkardController()
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
            var res = DB.KarKards.Select(x => x).Where(q => q.Job.Category.UserId == (int)UserId).
                OrderByDescending(q => q.DayDate).AsEnumerable().Where(s => int.Parse(s.DayDate) == int.Parse(toDate)).
                                              //  Where(s => int.Parse(s.DayDate) >= int.Parse(firstDate) && int.Parse(s.DayDate) <= int.Parse(toDate)).
                Select(q => new { SpendTimeMinute = Utility.Utility.ConvertTotime(q.SpendTimeMinute.ToString()), DayDate = Utility.Utility.ConvertDateToSlash(q.DayDate), q.KarkardId, q.JobId,q.StartTime,q.EndTime })
                .ToList();
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            foreach (var item in res)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();
                Models.DomainModels.Job J = DB.Jobs.SingleOrDefault(q => q.JobId == item.JobId);
                V.JobName = J.Name;
                V.JobId = J.JobId;
                V.DayDate = item.DayDate;
                V.KarkardId = item.KarkardId;
                V.StartTime = item.StartTime;
                V.EndTime = item.EndTime;
                V.TimePer = item.SpendTimeMinute;
                lstV.Add(V);
            }
            return PartialView(lstV);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            V.Currentdate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            V.ListJob = DB.Jobs.Where(q => q.Category.User.UserId == (int)UserId).ToList();
            //List<Models.DomainModels.Job> lstJ= DB.Jobs.Where(q=>q.Category.User.UserId==UserId).ToList();
            return PartialView(V);
        }
        [HttpPost]
        public JsonResult Create(Models.DomainModels.KarKard New)
        {
           
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            if (New.DayDate == null)
            {
                New.DayDate = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                New.MiladyDate = DateTime.Now;
            }
            else
            {
                New.DayDate = Utility.Utility.ConvertDateToSqlFormat(New.DayDate);
              
            }
           List<Models.DomainModels.KarKard> lstKarkard= DB.KarKards.Where(q => q.DayDate == New.DayDate && q.StartTime == New.StartTime && q.EndTime == New.EndTime).ToList();
            if (lstKarkard.Count > 0)
            {
                Error.result = false;
                Error.message = "رکورد تکراری میباشد";
                foreach (var item in lstKarkard)
                {
                    Error.description = item.Job.Name + " " + item.StartTime+ " "+item.EndTime;
                }
                return Json(Error, JsonRequestBehavior.AllowGet);
            }
            Models.DomainModels.KarKard oldKarkard = DB.KarKards.SingleOrDefault(q => q.DayDate == New.DayDate && q.StartTime == New.StartTime);
            if (oldKarkard!=null)
            {
                if (oldKarkard.JobId != New.JobId)
                {
                    Error.result = false;
                    Error.message = "این رکورد برای مورد دیگری ثبت شده است";
                    Error.description= oldKarkard.Job.Name + " " + oldKarkard.StartTime + " " + oldKarkard.EndTime;
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
                oldKarkard.SpendTimeMinute = New.SpendTimeMinute * 60;
                oldKarkard.EndTime = New.EndTime;
                if (DB.SaveChanges() > 0)
                {
                    Error.result = true;
                    Error.message = "با موفقیت ویرایش شد";
                    Error.description =oldKarkard.Job.Name +" "+New.StartTime+" "+New.EndTime ;
                }
                return Json(Error, JsonRequestBehavior.AllowGet);
            }
            New.SpendTimeMinute = New.SpendTimeMinute * 60;
            DB.KarKards.Add(New);
            if (DB.SaveChanges() > 0)
            {
                Error.result = true;
                Error.message = "با موفقیت ثبت شد";
            }
            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteKarkard(int KarkardId)
        {
            bool result = false;
            Models.DomainModels.KarKard OldKarkard = DB.KarKards.SingleOrDefault(q => q.KarkardId == KarkardId);
            DB.KarKards.Remove(OldKarkard);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowKarkadPivot()
        {
            #region ShowKarkadPivot
            ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();
            DataTable DT = U.Select("exec [ShowKarkadPivotDateToDate] " + firstDate + "," + toDate + "," + UserId.ToString());
            DataTable reversedDt = new DataTable();
            reversedDt = DT.Clone();
            for (var row = DT.Rows.Count - 1; row >= 0; row--)
                reversedDt.ImportRow(DT.Rows[row]);
            V.ShowKarkadPivotNotParamHeader = reversedDt;
            #endregion
            return PartialView(V);
        }
        public PartialViewResult ListJobs()
        {
            if (UserId == null)
            {
                return null;
            }
            else
            {
                ViewModels.VM_Public V = new ViewModels.VM_Public();
                V.Currentdate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
                V.ListJob = DB.Jobs.Where(q => q.Category.User.UserId == (int)UserId).ToList();
                return PartialView(V);
            }
        }
        public ActionResult ListKarkard(int dateParam) {
            var res = (from k in DB.KarKards
                       join j in DB.Jobs
                       on k.JobId equals j.JobId
                       select new { DayDate= k.DayDate, Name= j.Name,k.SpendTimeMinute,j.JobId }).AsEnumerable().
                       Where(y=>int.Parse(y.DayDate)>= dateParam).
                       Select(x =>new {x.Name,x.DayDate,x.SpendTimeMinute,x.JobId }).OrderByDescending(q=>q.DayDate).ThenBy(q=>q.JobId);
                     
                  return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SumMounthKarkard() {
           List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT =U.Select(@"
select 
left(DayDate,6) date,
sum(SpendTimeMinute)/3600 clock
from KarKard
where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by left(DayDate,6)
order by left(DayDate,6) desc
 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();
     
                V.Date = item["date"].ToString();
                V.TimePer = item["clock"].ToString();
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SumKarkardPerJob()
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select 
(select top 1 Name from Job where JobId=KarKard.JobId) as JobName,
JobId,
cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,1))   SpendTimeMinute 
 from KarKard
 where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by JobId
having sum(SpendTimeMinute)/3600>0
order by cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,1)) desc
 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.JobName = item["JobName"].ToString();
                V.JobId =int.Parse(item["JobId"].ToString());
                V.TimePer = item["SpendTimeMinute"].ToString();

                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SumMounthKarkardPerJob()
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select 
left(DayDate,6) Date,
(select top 1 Name from Job where JobId=KarKard.JobId) as JobName,
 JobId,
 cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,1))   SpendTimeMinute 
 from KarKard
  where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by JobId,left(DayDate,6)
--having sum(SpendTimeMinute)/3600>0
order by JobName, Date desc,cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,1)) desc
 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.JobName = item["JobName"].ToString();
                V.JobId = int.Parse(item["JobId"].ToString());
                V.TimePer = item["SpendTimeMinute"].ToString();
                V.Date= item["Date"].ToString();
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SumKarkardUntilToToday(string today)
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select left(DayDate,6) Date,sum(SpendTimeMinute)/3600  SpendTimeMinute
from karkard
where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
and right(DayDate,2)<=" + today + @"
group by left(DayDate,6)

 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.Date = item["Date"].ToString();
                V.TimePer = item["SpendTimeMinute"].ToString();

                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopBestKarkard(string today, int Offset, int FETCH)
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();

            DataTable DT = U.Select(@"exec TopBestKarkard "+ UserId + ","+ today + ","+ Offset + ","+ FETCH + "");
            /*DataTable DT = U.Select(@"
select top 30 
ROW_NUMBER() OVER(ORDER BY  sum(SpendTimeMinute) desc) AS Row,
DayDate,
sum(SpendTimeMinute)/60  SpendTimeMinute
from karkard
where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by DayDate
	order by sum(SpendTimeMinute) asc
	OFFSET     0 ROWS       -- skip 10 rows
FETCH NEXT 20 ROWS ONLY; -- take 10 rows
 ");
*/
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.Date = item["DayDate"].ToString();
                V.TimePer = item["SpendTimeMinute"].ToString();
                V.Row=int.Parse(item["Row"].ToString());
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListDaysOfWeek(string weekday) {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select 
 ROW_NUMBER() OVER(ORDER BY cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,2)) desc) AS Row,
--DATEPART ( weekday , MiladyDate ) weekday,
sum(SpendTimeMinute)/60  SpendTimeMinute,
DayDate
from karkard
where DATEPART ( weekday , MiladyDate )="+ weekday + @"
and JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by DATEPART ( weekday , MiladyDate ),DayDate
 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.Date = item["DayDate"].ToString();
                V.TimePer = item["SpendTimeMinute"].ToString();
                V.Row = int.Parse(item["Row"].ToString());
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListSumDaysOfWeek()
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select 
 ROW_NUMBER() OVER(ORDER BY cast(sum(SpendTimeMinute)/(3600*1.0) as decimal(10,2)) desc) AS Row,
DATEPART ( weekday , MiladyDate ) weekday,
sum(SpendTimeMinute)/60  SpendTimeMinute
from karkard
where JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
group by DATEPART ( weekday , MiladyDate )
 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.weekday =int.Parse(item["weekday"].ToString());
                V.TimePer = item["SpendTimeMinute"].ToString();
                V.Row = int.Parse(item["Row"].ToString());
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListToCurrentTime()
        {
            List<ViewModels.Karkard.VMKarkard> lstV = new List<ViewModels.Karkard.VMKarkard>();
            DataTable DT = U.Select(@"
select 
ROW_NUMBER() OVER(ORDER BY sum(SpendTimeMinute) desc) AS Row,
DayDate,
sum(SpendTimeMinute) SpendTimeMinute
from KarKard
where 
JobId in (select JobId from Job where CategoryId in (select CategoryId from Category where UserId=" + UserId + @"))
and
cast(MiladyDate as Time)<=(
select Convert(Time, GetDate())
)
group by DayDate
having
sum(SpendTimeMinute)>=(	select sum(SpendTimeMinute) 
	from KarKard 
	where cast(MiladyDate as date)=(select cast(CURRENT_TIMESTAMP as date))
	)

 ");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Karkard.VMKarkard V = new ViewModels.Karkard.VMKarkard();

                V.DayDate = item["DayDate"].ToString();
                V.TimePer = item["SpendTimeMinute"].ToString();
                V.Row = int.Parse(item["Row"].ToString());
                lstV.Add(V);
            }
            return Json(lstV, JsonRequestBehavior.AllowGet);
        }

    }
}
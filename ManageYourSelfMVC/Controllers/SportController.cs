using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    [Models.Filtering.Filter]
    public class SportController : Controller
    {
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = Models.staticClass.staticClass.UserId;// 0;
        public SportController()
        {
            //if(System.Web.HttpContext.Current.Session["UserId"])
            //object UserId1 = System.Web.HttpContext.Current.Session["UserId"];
            //if (UserId1 == null)
            //{
            //    UserId = 0;
            //}
            //else
            //{
            //    UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
            //}
        }
        #endregion

        #region Sport
        // [Security.CustomAthorize(Roles = "Karbari")]
       
        public ActionResult MainSport()
        {
            return View();
        }
        public ActionResult ListSportChk()
        {
            ViewModels.ErrorMessage Error = new ViewModels.ErrorMessage();
            Error.result = false;
            Error.message = string.Empty;
            try
            {
                ViewModels.VM_Public v = new ViewModels.VM_Public();
                // ViewModels.Task.ListTaskFuture T = new ViewModels.Task.ListTaskFuture();
                // ViewModels.VM_Public V = new ViewModels.VM_Public();
                List<Models.DomainModels.Cat> lstCat = new List<Models.DomainModels.Cat>();
                // DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                DataTable DT = U.Select(@"
SELECT  [CatId]
      ,[Title]
  FROM [5069_ManageYourSelf].[5069_Esmaeili].[Cat]
  where userid=" + UserId + @"
  and code=1
  order by [Order]
 ");

                foreach (DataRow item in DT.Rows)
                {
                    Models.DomainModels.Cat C = new Models.DomainModels.Cat();
                    C.CatId = int.Parse(item["CatId"].ToString());
                    C.Title = item["Title"].ToString();
                    lstCat.Add(C);
                }
                v.ListCat = lstCat;
                return Json(v,JsonRequestBehavior.AllowGet);
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
        public ActionResult ListSportFilter(int _CatId)
        {
            List<ViewModels.VMSport> lstV = new List<ViewModels.VMSport>();

            var res = (from S in DB.Sports
                       join C in DB.Cats on S.CatId equals C.CatId
                       where C.Code == 1 && C.CatId==_CatId
                       select new { S.Date, S.SportId, S.Tedad, C.CatId, C.Title, S.Set }

                      ).OrderByDescending(q => q.Date);
            foreach (var item in res)
            {
                ViewModels.VMSport V = new ViewModels.VMSport();
                V.Tedad = item.Tedad;
                V.Title = item.Title;
                V.SportId = item.SportId;
                V.Date = item.Date;
                V.CatId = item.CatId;
                V.Set = item.Set;
                //  V.lstCat = DB.Cats.Where(q => q.Code == 1 && q.UserId == UserId).ToList();
                lstV.Add(V);
            }
            return Json(lstV,JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            List<ViewModels.VMSport> lstV = new List<ViewModels.VMSport>();
          
            var res = (from S in DB.Sports join C in DB.Cats on S.CatId equals C.CatId
                       where C.Code == 1 
                      select new {S.Date,S.SportId,S.Tedad,C.CatId,C.Title,S.Set }
                      
                      ).OrderByDescending(q=>q.Date);
            foreach (var item in res)
            {
                ViewModels.VMSport V = new ViewModels.VMSport();
                V.Tedad = item.Tedad;
                V.Title = item.Title;
                V.SportId = item.SportId;
                V.Date = item.Date;
                V.CatId = item.CatId;
                V.Set = item.Set;
              //  V.lstCat = DB.Cats.Where(q => q.Code == 1 && q.UserId == UserId).ToList();
                lstV.Add(V);
            }
            return PartialView(lstV);
        }
        [HttpPost]
        public JsonResult Create(Models.DomainModels.Sport New,string StrTedad)
        {
            New.Date = New.Date.ConvertDateToSqlFormat();
            string result = "";
            int i = 1;
            try
            {
                if (New.Tedad == 0)
                {
                    string StrData = StrTedad.TrimEnd('-');
                    string[] Tedad = StrData.Split('-');
                    foreach (var item in Tedad)
                    {
                        New.Set = i;
                        New.Tedad = int.Parse(item);
                        DB.Sports.Add(New);
                        DB.SaveChanges();
                        i = i + 1;
                    }
                }
                else
                {
                    DB.Sports.Add(New);
                    DB.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                result = ex.ToString();
            }       
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateNewSport(Models.DomainModels.Sport New)
        {
           
            New.Date = New.Date.ConvertDateToSqlFormat();
            List<Models.DomainModels.Sport> lstSport = DB.Sports.Where(q => q.CatId == New.CatId && q.Date == New.Date).ToList();
            int Set = 0;
            foreach (var item in lstSport)
            {
                Set += 1;
            }
            New.Set = Set;
            string result = "";
            try
            {             
                    DB.Sports.Add(New);
                    DB.SaveChanges();
               
            }
            catch (Exception ex)
            {

                result = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewModels.VM_Public V = new ViewModels.VM_Public();
            V.Currentdate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            V.ListCat = DB.Cats.Where(q => q.Code == 1).ToList();
            return PartialView(V);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Old = DB.Sports.SingleOrDefault(q => q.SportId == Id);
            return PartialView(Old);
        }
        [HttpPost]
        public ActionResult Update(Models.DomainModels.Sport New)
        {
            var Old = DB.Sports.SingleOrDefault(q => q.SportId == New.SportId);
            Old.Date = New.Date;
            Old.Tedad = New.Tedad;
            Old.CatId = New.CatId;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int Id)
        {
            bool result = false;
            var Old = DB.Sports.SingleOrDefault(q => q.SportId == Id);
            DB.Sports.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Pivot
        public ActionResult ShowPivot()
        {
            
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            string firestDate = CurrentDate.Substring(0, 4) + "01";
            ViewModels.VMPivot V = new ViewModels.VMPivot();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)
DECLARE @sql AS NVARCHAR(max)

SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((select [Set] )) 
from Sport
group by [Set]
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
select Date,Tedad,Title,[Set] from sport S inner join cat C
on S.CatId=C.CatId
)as OrginalTable
PIVOT
(
sum([Tedad])
FOR [Set] IN('+@cols+')
 ) AS PivotTable
ORDER BY Date desc
'
EXECUTE(@sql)
");
            DataTable reversedDt = new DataTable();
            // reversedDt = DT.Clone();
            //for (var row = DT.Rows.Count - 1; row >= 0; row--)
            //    reversedDt.ImportRow(DT.Rows[row]);

            //V.RoutineJob = reversedDt;
            V.RoutineJob = DT;
           
            return PartialView(V);
        }
        public ActionResult ShowPivotOrder()
        {
            
            string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            string firestDate = CurrentDate.Substring(0, 4) + "01";
            ViewModels.VMPivot V = new ViewModels.VMPivot();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)
DECLARE @sql AS NVARCHAR(max)

SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((select [Set] )) 
from Sport
group by [Set]
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
select Date,Tedad,Title,[Set] from sport S inner join cat C
on S.CatId=C.CatId
)as OrginalTable
PIVOT
(
sum([Tedad])
FOR [Set] IN('+@cols+')
 ) AS PivotTable
ORDER BY title,Date desc
'
EXECUTE(@sql)
");
            DataTable reversedDt = new DataTable();
            // reversedDt = DT.Clone();
            //for (var row = DT.Rows.Count - 1; row >= 0; row--)
            //    reversedDt.ImportRow(DT.Rows[row]);

            //V.RoutineJob = reversedDt;
            V.RoutineJob = DT;
           
            return PartialView(V);
        }
        public ActionResult ShowPivotGroupingSets(string CatId)
        {
            if (CatId == null || CatId == string.Empty || CatId == "" || CatId == "undefined")
            {
                string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
                string firestDate = CurrentDate.Substring(0, 4) + "01";
                ViewModels.VMPivot V = new ViewModels.VMPivot();
                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)
DECLARE @sql AS NVARCHAR(max)
DECLARE @CatId AS NVARCHAR(50)
SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((select [Set] )) 
from Sport
group by [Set]
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
set @cols=@cols+',[99]'
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
select isnull(Date,''Sum'') Date,sum(Tedad) Tedad,Title,isnull([Set],99) as ''Set'' from sport S inner join cat C
on S.CatId=C.CatId
group by 
grouping sets
(
(date,Title),
(title),
(title,[Set]),
(date,title,[Set])
)
)as OrginalTable
PIVOT
(
sum([Tedad])
FOR [Set] IN('+@cols+')
 ) AS PivotTable
ORDER BY title desc,date asc
'
EXECUTE(@sql)
");
                DataTable reversedDt = new DataTable();
                // reversedDt = DT.Clone();
                //for (var row = DT.Rows.Count - 1; row >= 0; row--)
                //    reversedDt.ImportRow(DT.Rows[row]);

                //V.RoutineJob = reversedDt;
                V.RoutineJob = DT;

                return PartialView(V);
            }
            else
            {
                string CurrentDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
                string firestDate = CurrentDate.Substring(0, 4) + "01";
                ViewModels.VMPivot V = new ViewModels.VMPivot();
                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)
DECLARE @sql AS NVARCHAR(max)
DECLARE @CatId AS NVARCHAR(50)
set @CatId=" + CatId + @"
SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((select [Set] )) 
from Sport
group by [Set]
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
set @cols=@cols+',[99]'
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
select isnull(Date,''Sum'') Date,sum(Tedad) Tedad,Title,isnull([Set],99) as ''Set'' from sport S inner join cat C
on S.CatId=C.CatId
where S.CatId='+@CatId+'
group by 
grouping sets
(
(date,Title),
(title),
(title,[Set]),
(date,title,[Set])
)
)as OrginalTable
PIVOT
(
sum([Tedad])
FOR [Set] IN('+@cols+')
 ) AS PivotTable
ORDER BY Date desc
'
EXECUTE(@sql)
");
                DataTable reversedDt = new DataTable();
                // reversedDt = DT.Clone();
                //for (var row = DT.Rows.Count - 1; row >= 0; row--)
                //    reversedDt.ImportRow(DT.Rows[row]);

                //V.RoutineJob = reversedDt;
                V.RoutineJob = DT;

                return PartialView(V);
            }
        }
#endregion
    }
}
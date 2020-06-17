using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class PersonnelGsystemController : Controller
    {
        // GET: PersonnelGsystem
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        string Connection = Models.Connection.ConnectionString._ConnectionStringPersonnelGsystem;
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = Models.staticClass.staticClass.UserId;//(int)System.Web.HttpContext.Current.Session["UserId"];
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SelectInsertDatacard(string StartDateCard)
        {
            bool result = false;
            List<Models.DomainModels.DataCard> lstDataCard = new List<Models.DomainModels.DataCard>();
         
            string query = @"
                            select * from
                            (
                            select personelid,DateCard,min(TimeUpdate) I,MAX(TimeUpdate) O 
                            from datacard 
                            where DateCard>="+ StartDateCard + @"
                            group by personelid,DateCard
                            ) tbl
                             where personelid is not null
                            order by DateCard,personelid
                            ";

            
            DataTable DT= U.Select(query, Connection);
            foreach (DataRow item in DT.Rows)
            {
                Models.DomainModels.DataCard D = new Models.DomainModels.DataCard();

                D.personelid =int.Parse(item["personelid"].ToString());
                D.DateCard = item["DateCard"].ToString();
                D.I =int.Parse(item["I"].ToString());
                D.O =int.Parse(item["O"].ToString());
                lstDataCard.Add(D);
            }
            var lstMyDatacard= DB.DataCards.ToList();

            var list = (from F in lstDataCard
                        where !DB.DataCards.Any(f => f.personelid == F.personelid && f.DateCard == F.DateCard && f.I == F.I && f.O == F.O)
                        select F).ToList();

            foreach (var item in list)
            {
                Models.DomainModels.DataCard D = new Models.DomainModels.DataCard();
                D.DateCard = item.DateCard;
                D.I = item.I;
                D.O = item.O;
                D.personelid = item.personelid;
                DB.DataCards.Add(D);
            }
            if (DB.SaveChanges() > 0)
                result = true;
            /*--------------------------------------------------*/
            List<Models.DomainModels.MasterData> lstMasterData = new List<Models.DomainModels.MasterData>();
            string query2 = @"
                           select personelid,personelname+' '+PersonelFamily as FullName,Status from Personel
                            ";
            DataTable DT2 = U.Select(query2, Connection);
            foreach (DataRow item in DT2.Rows)
            {

                Models.DomainModels.MasterData M = new Models.DomainModels.MasterData();
                M.Personelid = int.Parse(item["personelid"].ToString());
                M.PersonelName = item["FullName"].ToString();
                lstMasterData.Add(M);
            }
            var lstMyMasterData = DB.MasterDatas.ToList();
            var list2 = (from F in lstMasterData
                         where !DB.MasterDatas.Any(f => f.Personelid == F.Personelid)
                        select F).ToList();
            foreach (var item in list2)
            {
                Models.DomainModels.MasterData M = new Models.DomainModels.MasterData();
                M.Personelid = item.Personelid;
                M.PersonelName = item.PersonelName;
                DB.MasterDatas.Add(M);
            }
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListTaradod()
        {
            string query = @"                           
select personelid,datecard2,chandshanbeh,i,o,diff ,rtb,TedadKol
from
(
select personelid,
datecard,
dbo.ChangeToDateSlash(datecard) datecard2,
dbo.ConvertTo_HH_MM(min(I)) I,
dbo.ConvertTo_HH_MM(max(O)) O,
dbo.ConvertTo_HH_MM(max(O)-min(I)) diff
from datacard D 
where personelid=997  
group by personelid,datecard
) as tbl
left join Taghvim T
on tbl.DateCard=T.DayDate
left join
(
select 
*,
ROW_NUMBER() OVER (PARTITION BY dd ORDER BY II ASC) rtb  
from
(
SELECT 
      [personelid] pp
      ,[DateCard] dd
      ,min([I]) II
  FROM [5069_ManageYourSelf].[dbo].[DataCard]
  group by [personelid],[DateCard]
  )as tbltight
) as ss
on tbl.Personelid=ss.pp and tbl.DateCard=ss.dd 
left join
(
select DateCard,count(*) TedadKol from
(
select personelid,DateCard 
from datacard
group by personelid,DateCard
)TTT
group by DateCard
) F
on F.DateCard=ss.dd
order by tbl.DateCard desc


";
            DataTable DT = U.Select(query);
            ViewModels.VDataCard V = new ViewModels.VDataCard();
            V.ShowTaradod = DT;
            return PartialView(V);
        }
        public ActionResult ListTaradod_angular()
        {
            DB.Configuration.ProxyCreationEnabled = false;
            string query = @"                           
select personelid,datecard2,chandshanbeh,i,o,diff ,rtb,TedadKol
from
(
select personelid,
datecard,
dbo.ChangeToDateSlash(datecard) datecard2,
dbo.ConvertTo_HH_MM(min(I)) I,
dbo.ConvertTo_HH_MM(max(O)) O,
dbo.ConvertTo_HH_MM(max(O)-min(I)) diff
from datacard D 
where personelid=997  
group by personelid,datecard
) as tbl
left join Taghvim T
on tbl.DateCard=T.DayDate
left join
(
select 
*,
ROW_NUMBER() OVER (PARTITION BY dd ORDER BY II ASC) rtb  
from
(
SELECT 
      [personelid] pp
      ,[DateCard] dd
      ,min([I]) II
  FROM [5069_ManageYourSelf].[dbo].[DataCard]
  group by [personelid],[DateCard]
  )as tbltight
) as ss
on tbl.Personelid=ss.pp and tbl.DateCard=ss.dd 
left join
(
select DateCard,count(*) TedadKol from
(
select personelid,DateCard 
from datacard
group by personelid,DateCard
)TTT
group by DateCard
) F
on F.DateCard=ss.dd
order by tbl.DateCard desc


";
            List<ViewModels.VDataCard> lstV = new List<ViewModels.VDataCard>();
            DataTable DT = U.Select(query);
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.VDataCard V = new ViewModels.VDataCard();

                V.personelid = item["personelid"].ToString();
                V.DateCard = item["datecard2"].ToString();
                V.chandshanbeh = item["chandshanbeh"].ToString();
                V.I = item["i"].ToString();
                V.O = item["o"].ToString();
                V.rtb= int.Parse(item["rtb"].ToString());
                V.diff = item["diff"].ToString();
                V.TedadKol = item["TedadKol"].ToString();
                lstV.Add(V);
            }
           // ViewModels.VDataCard V = new ViewModels.VDataCard();
   
            return Json(lstV,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListTaradod_angularView()
        {
            return PartialView();
        }
        public ActionResult ListRotbeh()
        {
            string query = @"                           
select 
personelid,
(select PersonelName from MasterData where personelid=tbl.personelid)as 'نام' ,
--personelid,
DateCard as 'تاریخ',
II as 'ورود',
O as 'خروج', 
ROW_NUMBER() OVER (PARTITION BY DateCard ORDER BY I ASC) AS 'رتبه'  
from
(
SELECT 
      [personelid]
      ,dbo.ChangeToDateSlash([DateCard]) DateCard
      ,min([I]) I
	 , dbo.ConvertTo_HH_MM(min(I)) II
      , dbo.ConvertTo_HH_MM(max([O])) O
  FROM [5069_ManageYourSelf].[dbo].[DataCard]
  group by [personelid],[DateCard]
  )as tbl
  order by DateCard desc
";
            DataTable DT = U.Select(query);
            ViewModels.VDataCard V = new ViewModels.VDataCard();
            V.ShowTaradod = DT;
            return PartialView(V);
        }
        public ActionResult ListRotbehByDate(string SDate)
        {

            string query = @"                           
select 
personelid,
(select PersonelName from MasterData where personelid=tbl.personelid)as 'نام' ,
--personelid,
DateCard as 'تاریخ',
II as 'ورود',
O as 'خروج', 
ROW_NUMBER() OVER (PARTITION BY DateCard ORDER BY I ASC) AS 'رتبه'  
from
(
SELECT 
      [personelid]
      ,dbo.ChangeToDateSlash([DateCard]) DateCard
      ,min([I]) I
	 , dbo.ConvertTo_HH_MM(min(I)) II
      , dbo.ConvertTo_HH_MM(max([O])) O
  FROM [5069_ManageYourSelf].[dbo].[DataCard]
where datecard in ("+SDate+@")
  group by [personelid],[DateCard]
  )as tbl
  order by DateCard desc
";
            DataTable DT = U.Select(query);
            ViewModels.VDataCard V = new ViewModels.VDataCard();
            V.ShowTaradod = DT;
            return PartialView("ListRotbeh", V);
        }
        public ActionResult ListRotbehshakhsy()
        {
            string query = @"                           
select personelid,datecard2,chandshanbeh,i,o,diff ,rtb,TedadKol
from
(
select personelid,
datecard,
dbo.ChangeToDateSlash(datecard) datecard2,
dbo.ConvertTo_HH_MM(min(I)) I,
dbo.ConvertTo_HH_MM(max(O)) O,
dbo.ConvertTo_HH_MM(max(O)-min(I)) diff
from datacard D 
where personelid=997  
group by personelid,datecard
) as tbl
left join Taghvim T
on tbl.DateCard=T.DayDate
left join
(
select 
*,
ROW_NUMBER() OVER (PARTITION BY dd ORDER BY II ASC) rtb  
from
(
SELECT 
      [personelid] pp
      ,[DateCard] dd
      ,min([I]) II
  FROM [5069_ManageYourSelf].[dbo].[DataCard]
  group by [personelid],[DateCard]
  )as tbltight
) as ss
on tbl.Personelid=ss.pp and tbl.DateCard=ss.dd 
left join
(
select DateCard,count(*) TedadKol from
(
select personelid,DateCard 
from datacard
group by personelid,DateCard
)TTT
group by DateCard
) F
on F.DateCard=ss.dd
order by tbl.DateCard desc


";
            DataTable DT = U.Select(query);
            ViewModels.VDataCard V = new ViewModels.VDataCard();
            V.Rotbehshakhsy = DT;
            return PartialView(V);
        }
    }
}
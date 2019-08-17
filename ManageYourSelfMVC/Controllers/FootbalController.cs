using ManageYourSelfMVC.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace ManageYourSelfMVC.Controllers
{
    public class FootbalController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        // GET: Footbal
        public ActionResult ListPlayers()
        {
            var ListPlayers = DB.Players.ToList();
            return PartialView(ListPlayers);
        }
        [CustomAthorize(Roles = "SuperAdmin,AdminFootbal,footbal")]
        public ActionResult Team()
        {

            //ارسال ایمیل
            //ThreadStart start = new ThreadStart(Utility.Utility.SendMailMethod);
            //Thread thread = new Thread(start);
            //thread.Start();
            try
            {
                Utility.Utility.CreateLog("قبل از ایمیل", "  public ActionResult Team()");
                string _NameUser = System.Web.HttpContext.Current.Session["_NameUser"].ToString();
                Thread thread = new Thread(() => Utility.Utility.SendMailMethod(_NameUser));
                thread.Start();
                Utility.Utility.CreateLog("بعد از ایمیل", "  public ActionResult Team()");
            }
            catch (Exception ex)
            {
                Utility.Utility.CreateLog(ex.ToString(), "public ActionResult Team()");
                Utility.Utility.Write_ReadFile(ex.ToString());
            }
        
           
            return View();
        }
        [CustomAthorize(Roles = "SuperAdmin,AdminFootbal")]
        [HttpGet]
        public ActionResult CreatePlayer()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditPlayer(int PlayerId)
        {
            var Player = DB.Players.SingleOrDefault(q => q.PlayersId == PlayerId);
            return PartialView(Player);
        }
        [HttpPost]
        public ActionResult UpdatePlayer(Models.DomainModels.Player New)
        {
            var Old = DB.Players.SingleOrDefault(q => q.PlayersId == New.PlayersId);
            Old.IsActive = New.IsActive;
            Old.IsHozoor = New.IsHozoor;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreatePlayer(Models.DomainModels.Player NewPlayer)
        {
            DB.Players.Add(NewPlayer);
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ثبت شد", JsonRequestBehavior.AllowGet);
            return Json("خطا در ثبت", JsonRequestBehavior.AllowGet);
        }
        [CustomAthorize(Roles = "SuperAdmin,AdminFootbal")]
        public ActionResult DeletePlayer(int PlayerId)
        {
            var Player = DB.Players.SingleOrDefault(q => q.PlayersId == PlayerId);
            DB.Players.Remove(Player);
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت حذف شد", JsonRequestBehavior.AllowGet);
            return Json("خطا در ثبت", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreatePlayerScore(int UserId)
        {
            List<ViewModels.PlayerScoreVM> ListV = new List<ViewModels.PlayerScoreVM>();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.SelectWhere(@"
If (select object_id('tempdb..#T_TempTbl')) is not null 
 Drop Table #T_TempTbl 
 Create Table #T_TempTbl(PlayersId int ,name nvarchar(60),Score int) 

Declare mycursor cursor
For 
---<Master Select>
  select PlayersId,Name from players
--</Master Select>
Open mycursor
---<Declare>
Declare @PlayersId int,@name nvarchar(50),@Score int
--</Declare>
Fetch next from mycursor Into @PlayersId ,@name 
While @@fetch_status <>-1
BEGIN
---------------<Body>
set @Score=1
select @Score=Score from playerscore where UserId=@UserId and PlayerId=@PlayersId
insert into  #T_TempTbl
select @PlayersId ,@name,@Score

--------------</Body>
FETCH next from mycursor Into @PlayersId ,@name 
End
Close mycursor
DEALLOCATE mycursor
select * from #T_TempTbl order by Score desc
", new string[] { "UserId" }, new string[] { UserId.ToString() });


            foreach (DataRow item in DT.Rows)
            {
                ViewModels.PlayerScoreVM V = new ViewModels.PlayerScoreVM();
                V.PlayerName = item["name"].ToString();
                V.PlayerId = int.Parse(item["PlayersId"].ToString());
                V.Score = int.Parse(item["Score"].ToString());
                ListV.Add(V);
            }
            return PartialView(ListV);
        }
        [HttpPost]
        public ActionResult CreatePlayerScore(List<Models.DomainModels.PlayerScore> lstPlayerScore)
        {
            var UserScore = lstPlayerScore.FirstOrDefault();
            int UserId = UserScore.UserId;
            DB.PlayerScores.RemoveRange(DB.PlayerScores.Where(q => q.UserId == UserId));
            DB.SaveChanges();
            foreach (var item in lstPlayerScore)
            {
                Models.DomainModels.PlayerScore PS = new Models.DomainModels.PlayerScore();
                PS.PlayerId = item.PlayerId;
                PS.Score = item.Score;
                PS.UserId = item.UserId;
                DB.PlayerScores.Add(PS);
            }
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ثبت شد", JsonRequestBehavior.AllowGet);
            return Json("خطا در ثبت", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListBestPlayers()
        {
            List<ViewModels.PlayerScoreVM> ListV = new List<ViewModels.PlayerScoreVM>();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"

  select P.PlayersId,P.name,SumScore,CountVote,AVGScore from Players P inner join
(
SELECT [PlayerId]
      ,sum([Score]) SumScore,
	  count([PlayerId]) CountVote,
 cast( (sum([Score])*1.0)/count([PlayerId]) as numeric(36,2)) AVGScore
  FROM [5069_ManageYourSelf].[dbo].[PlayerScore] PS
  group by [PlayerId]
  ) tbl
  on P.PlayersId=tbl.PlayerId
  order by AVGScore desc
");


            foreach (DataRow item in DT.Rows)
            {
                ViewModels.PlayerScoreVM V = new ViewModels.PlayerScoreVM();
                V.PlayerName = item["name"].ToString();
                V.PlayerId = int.Parse(item["PlayersId"].ToString());
                V.SumScore = int.Parse(item["SumScore"].ToString());
                V.AVGScore=float.Parse(item["AVGScore"].ToString());
                V.CountVote= int.Parse(item["CountVote"].ToString());
                ListV.Add(V);
            }
            return PartialView(ListV);
        }
        public ActionResult ListVoteToPlayers()
        {
            ViewModels.PlayerScoreVM V = new ViewModels.PlayerScoreVM();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"exec ListVoteToPlayers");
            V.VoteToPlayersPIVOT = DT;
            return PartialView(V);
        }
        public ActionResult CreateTeam()
        {
            List<ViewModels.PlayerScoreVM> ListV = new List<ViewModels.PlayerScoreVM>();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"exec CreateTeam");

            foreach (DataRow item in DT.Rows)
            {
                ViewModels.PlayerScoreVM V = new ViewModels.PlayerScoreVM();
                V.PlayerName = item["PlayerName"].ToString();
               // V.PlayerId = int.Parse(item["PlayersId"].ToString());
                V.SumScore = int.Parse(item["Score"].ToString());
                V.TeamName = item["TeamName"].ToString();
                V.AVGScore =float.Parse(item["AVGScore"].ToString());
                ListV.Add(V);

            }
            return PartialView(ListV);
        }
    }
}
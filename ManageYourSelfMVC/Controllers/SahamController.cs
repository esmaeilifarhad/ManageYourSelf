using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageYourSelfMVC.Models.Filtering;

namespace ManageYourSelfMVC.Controllers
{

    [Models.Filtering.Filter]
    public class SahamController : Controller
    {
        public string cookieValue { get; set; }
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        public SahamController() {
          

        }

        // GET: Saham
        public ActionResult MainSaham()
        {
            return View();
        }
        public ActionResult LastPositiveAlMinus(int TedadRooz) {
            ViewModels.Saham.SahamVM SahamObj = new ViewModels.Saham.SahamVM();
            try
            {

                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"exec LastPositiveAlMinus "+ TedadRooz.ToString() + "");
                List<ViewModels.Saham.NamadVM> lstN = new List<ViewModels.Saham.NamadVM>();
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Saham.NamadVM N = new ViewModels.Saham.NamadVM();
                    N.IdNamad = int.Parse(item["Id"].ToString());
                    N.tseAddress= item["tseAdrs"].ToString();
                    N.NamadName = item["Namad"].ToString();
                    N.SumDarsadGheymatPayany = float.Parse(item["SumDarsadGheymatPayany"].ToString());
                    N.TedadM = int.Parse(item["TedadM"].ToString());
                    N.TedadP = int.Parse(item["TedadP"].ToString());
                    N.Tedad = int.Parse(item["Tedad"].ToString());
                    N.Rate = float.Parse(item["Rate"].ToString());
                    if (item["RahavardId"].ToString() != "")
                        N.IdRahavard = int.Parse(item["RahavardId"].ToString());


                    lstN.Add(N);
                }
                SahamObj.lstNamadVM = lstN;

           

                return Json(SahamObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());
        

            }
        }
     
        public ActionResult CompareToAvg(string sortBy, string today, string yesterday)
        {
           
            ViewModels.Saham.SahamVM SahamObj = new ViewModels.Saham.SahamVM();
            try
            {

                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"exec CompareToAvg "+sortBy+","+ today + ","+ yesterday + "");
                List<ViewModels.Saham.NamadVM> lstN = new List<ViewModels.Saham.NamadVM>();
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Saham.NamadVM N = new ViewModels.Saham.NamadVM();
                    N.Hajm = Int64.Parse(item["Hajm"].ToString());
                    N.IdNamad = int.Parse(item["Id"].ToString());
                    N.Avgg = Int64.Parse(item["Avgg"].ToString());
                    N.Rate = float.Parse(item["Rate"].ToString());
                    N.SumDarsadGheymatPayany= float.Parse(item["SumDarsadGheymatPayany"].ToString()); 
                    N.ShamsyDate = item["ShamsyDate"].ToString();
                    N.NamadName = item["Namad"].ToString();
                    N.namadNameTse = item["namadName"].ToString();
                    N.tseAddress = item["tseAdrs"].ToString();
                    if(item["RahavardId"].ToString()!="")
                    N.IdRahavard = int.Parse(item["RahavardId"].ToString());
                    N.TedadM = int.Parse(item["TedadM"].ToString());
                    N.TedadP = int.Parse(item["TedadP"].ToString());
                   N.tseId= item["tseId"].ToString();
                    lstN.Add(N);
                }
                SahamObj.lstNamadVM = lstN;



                return Json(SahamObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());


            }
        }

        public ActionResult MoreMinusFive() {
            ViewModels.Saham.SahamVM SahamObj = new ViewModels.Saham.SahamVM();
            try
            {

                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"select * from namadDetail D inner join Namad M
on D.NamadId=M.Id
 where DarsadGheymatPayany<-5
 order by shamsydate desc,DarsadGheymatPayany");
                List<ViewModels.Saham.NamadVM> lstN = new List<ViewModels.Saham.NamadVM>();
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Saham.NamadVM N = new ViewModels.Saham.NamadVM();
                    N.Hajm = Int64.Parse(item["Hajm"].ToString());
                    N.IdNamad = int.Parse(item["NamadId"].ToString());
                    N.ShamsyDate = item["ShamsyDate"].ToString();
                    N.NamadName = item["NamadSahih"].ToString();
                    N.tseAddress = item["tseAdrs"].ToString();
                    N.DarsadGheymatPayany= float.Parse(item["DarsadGheymatPayany"].ToString());
                    if (item["RahavardId"].ToString() != "")
                        N.IdRahavard = int.Parse(item["RahavardId"].ToString());
                    lstN.Add(N);
                }
                SahamObj.lstNamadVM = lstN;



                return Json(SahamObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());


            }
        }
        public ActionResult NamadDetail(int NamadId)
        {
            ViewModels.Saham.SahamVM SahamObj = new ViewModels.Saham.SahamVM();
            try
            {

                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"select * from NamadDetail D
inner join Namad M
on M.Id=D.NamadId
 where NamadId="+NamadId.ToString()+ " order by ShamsyDate desc");
                List<ViewModels.Saham.NamadVM> lstN = new List<ViewModels.Saham.NamadVM>();
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Saham.NamadVM N = new ViewModels.Saham.NamadVM();
                    N.Hajm =Int64.Parse(item["Hajm"].ToString());
                    N.TedadMoamelat = Int64.Parse(item["TedadMoamelat"].ToString());
                    N.DarsadGheymatPayany = double.Parse(item["DarsadGheymatPayany"].ToString());
                    N.IdNamad = int.Parse(item["NamadId"].ToString());
                   // N.Avgg = int.Parse(item["Avgg"].ToString());
                   // N.Rate = float.Parse(item["Rate"].ToString());
                    N.ShamsyDate = item["ShamsyDate"].ToString();
                    N.NamadName = item["Namad"].ToString();


                    lstN.Add(N);
                }
                SahamObj.lstNamadVM = lstN;



                return Json(SahamObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());


            }
        }
        [HttpPost]
        public ActionResult SaveTseAdrs(int IdNamad,string tseAdrs) {
            try
            {

              var OldNamad=  DB.Namads.SingleOrDefault(q => q.Id == IdNamad);
                OldNamad.tseAdrs = tseAdrs;
                DB.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());


            }
        }
        public ActionResult UpdateNamad(int IdNamad,int? IdRahavard,string tseId="") {
            if (tseId.ToString() != "")
            {
                var OldNamad = DB.Namads.SingleOrDefault(q => q.Id == IdNamad);
                OldNamad.tseId = tseId.ToString();
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var OldNamad = DB.Namads.SingleOrDefault(q => q.Id == IdNamad);
                OldNamad.RahavardId = IdRahavard;
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InsertUpdateJson(string tseId ,string title,string TedadMoamelat,string hajm,float DarsadGheymatPayany,string ShamsyDate,string GheymatPayany)
        {
          var oldNamad=  DB.Namads.SingleOrDefault(q => q.tseId == tseId);
            if (oldNamad != null)
            {
                oldNamad.tseId = tseId;
                DB.SaveChanges();

            }
            else
            {
                //----------------Master
                Models.DomainModels.Namad N = new Models.DomainModels.Namad();
                N.tseId = tseId;
                N.Namad1 = title;
                N.CodeSherkat = title;
                DB.Namads.Add(N);
                DB.SaveChanges();
                //-----------------Detail
                Models.DomainModels.NamadDetail D = new Models.DomainModels.NamadDetail();
                D.TedadMoamelat = int.Parse(TedadMoamelat);
                D.ShamsyDate = ShamsyDate;
                D.NamadId = N.Id;
                D.Hajm = Int64.Parse(hajm);
                D.DarsadGheymatPayany = (float)Math.Round(DarsadGheymatPayany * 100f) / 100f;
                D.GheymatPayany = Int64.Parse(GheymatPayany);
                DB.NamadDetails.Add(D);
                DB.SaveChanges();
            }
            if (oldNamad != null)
            {
                var oldDetail = DB.NamadDetails.SingleOrDefault(q => q.NamadId == oldNamad.Id && q.ShamsyDate == ShamsyDate);
                if (oldDetail != null)
                {

                    oldDetail.TedadMoamelat = int.Parse(TedadMoamelat);
                    //oldDetail.ShamsyDate = "13990208";
                    //oldDetail.NamadId = oldNamad.Id;
                    oldDetail.Hajm = hajm == null ? 0 : Int64.Parse(hajm);
                    oldDetail.DarsadGheymatPayany = (float)Math.Round(DarsadGheymatPayany * 100f) / 100f;
                    oldDetail.GheymatPayany = Int64.Parse(GheymatPayany);
                    //DB.NamadDetails.Add(oldDetail);
                    DB.SaveChanges();
                }
                else
                {
                    Models.DomainModels.NamadDetail D = new Models.DomainModels.NamadDetail();
                    D.TedadMoamelat = int.Parse(TedadMoamelat);
                    D.ShamsyDate = ShamsyDate;
                    D.NamadId = oldNamad.Id;
                    D.Hajm = Int64.Parse(hajm);
                    D.DarsadGheymatPayany = (float)Math.Round(DarsadGheymatPayany * 100f) / 100f;
                    D.GheymatPayany = Int64.Parse(GheymatPayany);
                    DB.NamadDetails.Add(D);
                    DB.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult showMessage()
        {
           var s= Models.staticClass.staticClass.UserId;
            return Json("شما دسترسی به این عمل را ندارید", JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class DakhloKharjController : Controller
    {
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = Models.staticClass.staticClass.UserId;// 0;
        public DakhloKharjController()
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


        //#region Initial
        //Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        //Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        //Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        //int UserId = (int)System.Web.HttpContext.Current.Session["UserId"];
        //#endregion

        #region MojoodyBank
        public ActionResult ListMojoodyBank()
        {
           
            try
            {
                var res = DB.MojoodyBanks.Where(q => q.UserId == UserId).ToList();
                return PartialView(res);
            }
            catch (Exception ex)
            {

                var s = ex.Message;
                return null;
            }
           
        }
        [HttpPost]
        public JsonResult CreateMojoodyBank(Models.DomainModels.MojoodyBank NewMojoodyBank)
        {
            bool result = false;
            NewMojoodyBank.UserId = UserId;
            DB.MojoodyBanks.Add(NewMojoodyBank);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateMojoodyBank()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditMojoodyBank(int MojoodyBankId)
        {
            var MojoodyBank = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankId);
            return PartialView(MojoodyBank);
        }
        [HttpPost]
        public ActionResult UpdateMojoodyBank(Models.DomainModels.MojoodyBank New)
        {
            var Old = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == New.MojoodyBankId);
            Old.MojoodyName = New.MojoodyName;
            // Old.UserId = UserId;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMojoodyBank(int MojoodyBankId)
        {
            bool result = false;
            Models.DomainModels.MojoodyBank Old = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankId);
            DB.MojoodyBanks.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TypeHazineh
        public ActionResult ListTypeHazineh()
        {
            var res = DB.TypeHazinehs.Where(q => q.UserId == UserId).OrderBy(q => q.DaramadOrKharj).ToList();
            return PartialView(res);
        }
        [HttpPost]
        public JsonResult CreateTypeHazineh(Models.DomainModels.TypeHazineh New)
        {
            bool result = false;
            New.UserId = UserId;
            DB.TypeHazinehs.Add(New);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateTypeHazineh()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditTypeHazineh(int TypeHazinehId)
        {
            var TypeHazinehs = DB.TypeHazinehs.SingleOrDefault(q => q.TypeHazinehId == TypeHazinehId);
            return PartialView(TypeHazinehs);
        }
        [HttpPost]
        public ActionResult UpdateTypeHazineh(Models.DomainModels.TypeHazineh New)
        {
            var Old = DB.TypeHazinehs.SingleOrDefault(q => q.TypeHazinehId == New.TypeHazinehId);
            Old.name = New.name;
            Old.DaramadOrKharj = New.DaramadOrKharj;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTypeHazineh(int TypeHazinehId)
        {
            bool result = false;
            Models.DomainModels.TypeHazineh Old = DB.TypeHazinehs.SingleOrDefault(q => q.TypeHazinehId == TypeHazinehId);
            DB.TypeHazinehs.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Daramad
        public ActionResult ListDaramad(int MojoodyBankId = 0)
        {
            List<ViewModels.DaramadVM> lstDaramadVM = new List<ViewModels.DaramadVM>();
            string toDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            string firstDate = Utility.Utility.shamsi_date().ConvertDateToSqlFormat().Substring(0,6) + "01";

            var res = DB.Daramads.Where(q => q.MojoodyBank.UserId == UserId && q.MojoodyBankId==MojoodyBankId).
                OrderBy(q => q.MojoodyBankId).
                ThenByDescending(q => q.Date).
                ThenByDescending(q=>q.DaramadId).AsEnumerable().
                Where(q=> int.Parse(q.Date) >= int.Parse(firstDate) &&
                int.Parse(q.Date) <= int.Parse(toDate)).
                ToList();
            foreach (var item in res)
            {
                ViewModels.DaramadVM V = new ViewModels.DaramadVM();
                V.lstTypeHazineh = DB.TypeHazinehs.Where(q => q.TypeHazinehId == item.TypeHazinehId).ToList();
                V.lstMojoodyBank = DB.MojoodyBanks.Where(q => q.MojoodyBankId == item.MojoodyBankId).ToList();
                V.Rial = item.Rial;
                V.After = item.After;
                V.Before = item.Before;
                V.DaramadId = item.DaramadId;
                //V.DaramadORHazineh = item.DaramadORHazineh;
                V.Date = item.Date.ConvertDateToSlash(); ;
                V.Description = item.Description;
                V.MojoodyBankId = item.MojoodyBankId;
                lstDaramadVM.Add(V);
            }
            return PartialView(lstDaramadVM);
        }
        [HttpPost]
        public JsonResult CreateDaramad(Models.DomainModels.Daramad New)
        {
            bool result = false;
            New.Date = New.Date.ConvertDateToSqlFormat();
            var TH = DB.TypeHazinehs.SingleOrDefault(q => q.TypeHazinehId == New.TypeHazinehId);
            var Old = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == New.MojoodyBankId);
            if (TH.DaramadOrKharj == true)
            {
                New.DaramadORHazineh = true;
                New.Before = Old.Rial;
                New.After = New.Before + New.Rial;
            }
            else
            {
                New.DaramadORHazineh = false;
                New.Before = Old.Rial;
                New.After = New.Before - New.Rial;
            }
            DB.Daramads.Add(New);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateDaramad(int MojoodyBankId)
        {
            List<ViewModels.DaramadVM> lstDaramadVM = new List<ViewModels.DaramadVM>();

            // var res = DB.Daramads.Where(q => q.MojoodyBank.UserId == UserId).OrderByDescending(q => q.Date).ToList();
            //foreach (var item in res)
            //{
            ViewModels.DaramadVM V = new ViewModels.DaramadVM();
            V.MojoodyBankId = MojoodyBankId;
            V.lstTypeHazineh = DB.TypeHazinehs.Where(q => q.UserId == UserId).ToList();
            V.lstMojoodyBank = DB.MojoodyBanks.Where(q => q.UserId == UserId).ToList();
            V.CurrentDate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();

            lstDaramadVM.Add(V);
            // }
            return PartialView(lstDaramadVM);
        }
        [HttpGet]
        public ActionResult EditDaramad(int DaramadId)
        {
          //  List<ViewModels.DaramadVM> lstDaramadVM = new List<ViewModels.DaramadVM>();
            ViewModels.DaramadVM V = new ViewModels.DaramadVM();
            var Old = DB.Daramads.SingleOrDefault(q => q.DaramadId == DaramadId);
            V.DaramadId= Old.DaramadId;
            V.After = Old.After;
            V.Before = Old.Before;
            V.Date = Old.Date;
            V.Description = Old.Description;
            V.MojoodyBankId = Old.MojoodyBankId;
            V.Rial = Old.Rial;
            V.DariaftPardakht = Old.TypeHazineh.DaramadOrKharj;
            V.TypeHazinehId = Old.TypeHazinehId;
            V.lstTypeHazineh = DB.TypeHazinehs.Where(q => q.UserId == UserId).ToList();
            V.lstMojoodyBank = DB.MojoodyBanks.Where(q => q.UserId == UserId).ToList();
            V.CurrentDate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
           // lstDaramadVM.Add(V);          
            return PartialView(V);
        }
        [HttpPost]
        public ActionResult UpdateDaramad(Models.DomainModels.Daramad New)
        {
            var Old = DB.Daramads.SingleOrDefault(q => q.DaramadId == New.DaramadId);
            //Old.DaramadORHazineh = New.DaramadORHazineh;
            Old.Date = New.Date;
            Old.Description = New.Description;
            Old.MojoodyBankId = New.MojoodyBankId;
            Old.Rial = New.Rial;
            Old.TypeHazinehId = New.TypeHazinehId;
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDaramad(int DaramadId)
        {
            bool result = false;
            Models.DomainModels.Daramad Old = DB.Daramads.SingleOrDefault(q => q.DaramadId == DaramadId);
            DB.Daramads.Remove(Old);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListTypeHazinehcmb(bool DaramadOrKLharj)
        {
            DB.Configuration.ProxyCreationEnabled = false;
            var res = DB.TypeHazinehs.Where(q => q.DaramadOrKharj == DaramadOrKLharj && q.UserId == UserId).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Balance
        [HttpGet]
        public ActionResult EditMojoodyBankBalance(int MojoodyBankId)
        {
            var MojoodyBank = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankId);
            return PartialView(MojoodyBank);
        }
        public ActionResult UpdateMojoodyBankBalance(int MojoodyBankId,string FalseRial,string OkRial)
        {
            Models.DomainModels.Daramad D = new Models.DomainModels.Daramad();
            FalseRial = FalseRial.Replace(",", "");
            OkRial = OkRial.Replace(",", "");
            var OldMojoodyBank = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankId);
            if (OldMojoodyBank.Rial < int.Parse(OkRial))
            {
                D.Date = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
                D.Rial = int.Parse(OkRial) - (int)OldMojoodyBank.Rial;
                D.Description = "برای بالانس اضافه شده است";
                D.MojoodyBankId = OldMojoodyBank.MojoodyBankId;
                D.TypeHazinehId = 39;
                D.Before = OldMojoodyBank.Rial;
                D.After = OldMojoodyBank.Rial + D.Rial;
                DB.Daramads.Add(D);
               // DB.SaveChanges();
            }
            if (OldMojoodyBank.Rial > int.Parse(OkRial))
            {
                D.Date = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
                D.Rial = (int)OldMojoodyBank.Rial- int.Parse(OkRial);
                D.Description = "برای بالانس کم شده است";
                D.MojoodyBankId = OldMojoodyBank.MojoodyBankId;
                D.TypeHazinehId = 40;
                D.Before = OldMojoodyBank.Rial;
                D.After = OldMojoodyBank.Rial - D.Rial;
                DB.Daramads.Add(D);
               // DB.SaveChanges();
            }
            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Exchange
        [HttpGet]
        public ActionResult EditMojoodyBankExchange(int MojoodyBankId)
        {
            var MojoodyBank = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankId);

            List<ViewModels.DaramadVM> lstDaramadVM = new List<ViewModels.DaramadVM>();
            ViewModels.DaramadVM V = new ViewModels.DaramadVM();
            V.lstTypeHazineh = DB.TypeHazinehs.Where(q => q.UserId == UserId).ToList();
            V.lstMojoodyBank = DB.MojoodyBanks.Where(q => q.UserId == UserId).ToList();
            V.CurrentDate =  Utility.Utility.shamsi_date().ConvertDateToSqlFormat().ConvertDateToSlash();
            V.MojoodyBankName = MojoodyBank.MojoodyName;
            V.MojoodyBankId = MojoodyBankId;
            lstDaramadVM.Add(V);
            return PartialView(lstDaramadVM);

        }
        public ActionResult UpdateMojoodyBankExchange(int MojoodyBankIdSource, int MojoodyBankIdDestination,int Rial,string Date)
        {
            Models.DomainModels.Daramad DSource = new Models.DomainModels.Daramad();
            var Source= DB.MojoodyBanks.SingleOrDefault(q=>q.MojoodyBankId==MojoodyBankIdSource);
            var Destination = DB.MojoodyBanks.SingleOrDefault(q => q.MojoodyBankId == MojoodyBankIdDestination);

            DSource.MojoodyBankId = MojoodyBankIdSource;
            DSource.TypeHazinehId = 36;
            DSource.Rial = Rial;
            DSource.Date = Date.ConvertDateToSqlFormat();
            DSource.DaramadORHazineh = false;
            DSource.Before = Source.Rial;
            DSource.Description = " کاسته شد "+Source.MojoodyName+" از "+Rial+" ملبغ";
            DSource.After = Source.Rial - Rial;
            DB.Daramads.Add(DSource);

            Models.DomainModels.Daramad DDestination = new Models.DomainModels.Daramad();
            DDestination.MojoodyBankId = MojoodyBankIdDestination;
            DDestination.TypeHazinehId = 37;
            DDestination.Rial = Rial;
            DDestination.Date = Date.ConvertDateToSqlFormat(); 
            DDestination.DaramadORHazineh = true;
            DDestination.Before = Destination.Rial;
            DDestination.Description = " افزوده شد " + Destination.MojoodyName + " به " + Rial + " ملبغ ";
            DDestination.After = Destination.Rial + Rial;
            DB.Daramads.Add(DDestination);


           // D.Date = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();


            if (DB.SaveChanges() > 0)
                return Json("با موفقیت ویرایش شد", JsonRequestBehavior.AllowGet);
            else
                return Json("خطا در ویرایش", JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Reports
        public ActionResult Rpt_ListGroupHazine()
        {
            ViewModels.DakhloKharj D = new ViewModels.DakhloKharj();

            DataTable DT = U.Select("exec ListGroupHazine " + UserId.ToString()+","+0);

            List<ViewModels.ReportMahane> lstV = new List<ViewModels.ReportMahane>();
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.ReportMahane V = new ViewModels.ReportMahane();
                // V.Radif = int.Parse(item["Radif"].ToString());
                V.TypeHazinehId = int.Parse(item["TypeHazinehId"].ToString());
                V.Date = item["Datee"].ToString();
                V.Name = item["Name"].ToString();
                V.Rial = int.Parse(item["Rial"].ToString());
                lstV.Add(V);
            }
            D.lstReportMahane = lstV;

            return PartialView(D);
        }
        #endregion
        #region Kendo
        public ActionResult ListTypeHazinehKendo()
        {
            var res = DB.TypeHazinehs.OrderBy(q => q.DaramadOrKharj).ToList();
            return PartialView(res);
        }
        #endregion
    }
}
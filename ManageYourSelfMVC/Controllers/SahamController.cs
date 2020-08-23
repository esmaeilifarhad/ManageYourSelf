using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageYourSelfMVC.Models.Filtering;

namespace ManageYourSelfMVC.Controllers
{

    [Models.Filtering.Filter]
    public class SahamController : Controller
    {
        
        List<ViewModels.AllNamad> lstAllNamad = new List<ViewModels.AllNamad>();
        //Models.Entities DB = new Models.Entities();
        List<ViewModels.ExcelFiles> lstFiles = new List<ViewModels.ExcelFiles>();
        List<DataTable> lstDataTable = new List<DataTable>();
        List<ViewModels.ExcelSheetData> lstSheetData = new List<ViewModels.ExcelSheetData>();
        
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

                        if (file != null && file.ContentLength > 0)
                            try
                            {
                          
                                bool exists = System.IO.Directory.Exists(Server.MapPath("~/Files"));
                                if (!exists)
                                    Directory.CreateDirectory(Server.MapPath("~/Files"));

                                string path = Path.Combine(Server.MapPath("~/Files"),
                                                           Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                TempData["Message"] = file.FileName + " File uploaded successfully";
                            }
                            catch (Exception ex)
                            {
                                TempData["Message"] = "ERROR:" + ex.Message.ToString();
                            }
                        else
                        {
                            TempData["Message"] = "You have not specified a file.";
                        }

                    }
                    // Returns message that successfully uploaded  
                   // return Json( " File uploaded successfully");
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
            string PathFolderAddress = Server.MapPath("~/Files");
            /*
          
            */
            //Read All File Path
            string[] fileEntries = Directory.GetFiles(PathFolderAddress);
            int j = 1;
            foreach (string fileName in fileEntries)
            {
                ViewModels.ExcelFiles file = new ViewModels.ExcelFiles();
                file.FileAddress = fileName;
                file.FileName = Path.GetFileName(fileName);
                file.ShamsyDate = Path.GetFileName(fileName).Split('.')[0];
                file.Row = j;
                lstFiles.Add(file);
                j += 1;
            }

            var ads = "";
            foreach (var item in lstFiles)
            {
                ads = item.FileAddress;

                //  List<string> lstNameSheet = new List<string>();
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               item.FileAddress +
                               ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);

                con.Open();

                DataTable Sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    item.SheetName = sht;
                    // lstNameSheet.Add(sht);
                    //  OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select * from [" + sht + "]", con);
                }
                con.Close();
            }
            // ExcelToDataTable();
            // return Json(ads, JsonRequestBehavior.AllowGet);
            foreach (var item in lstFiles)
            {
                ViewModels.ExcelSheetData SheetData = new ViewModels.ExcelSheetData();

                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           item.FileAddress +
                           ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                OleDbCommand oconn = new OleDbCommand("Select * From [" + item.SheetName + "]", con);
                // OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "]", con);

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);

                SheetData.fileName = item.ShamsyDate;
                SheetData.lstDataTable = data;
                lstSheetData.Add(SheetData);
                con.Close();
            }

            // BindDataTablesToModel();
            // return Json(lstSheetData, JsonRequestBehavior.AllowGet);

            foreach (var item in lstSheetData)
            {
                List<ViewModels.NamadDetail> lstNamadDetail = new List<ViewModels.NamadDetail>();
                foreach (DataRow row in item.lstDataTable.Rows)
                {
                    ViewModels.NamadDetail D = new ViewModels.NamadDetail();
                    D.Namad = row[0].ToString();
                    D.Name = row[1].ToString();
                    D.Hajm = Int64.Parse(row[2].ToString());
                    D.TedadMoamelat = int.Parse(row[4].ToString());
                    if (row[12].ToString() == "")
                    {
                        D.DarsadGheymatPayany = 0;
                    }
                    else
                    {
                        D.DarsadGheymatPayany = float.Parse(row[12].ToString());
                    }
                    if (row[10].ToString() == "")
                    {
                        D.GheymatPayany = 0;
                    }
                    else
                    {
                        D.GheymatPayany = Int64.Parse(row[10].ToString());
                    }

                    D.CodeSherkat = row[0].ToString();
                    D.DateShamsy = item.fileName;
                    lstNamadDetail.Add(D);
                }
                ViewModels.AllNamad AllNamad = new ViewModels.AllNamad();
                AllNamad.lstNamadDetail = lstNamadDetail;
                // AllNamad.ShamsyDate = "12";
                lstAllNamad.Add(AllNamad);
            }

            int MasterId = 0;
            // تعداد فایل ها 
            foreach (var item in lstAllNamad)
            {
                foreach (var sheetdata in item.lstNamadDetail)
                {
                    //  string codeSherkatExcel= sheetdata.CodeSherkat.Replace("1", "");
                    string codeSherkatExcel = sheetdata.CodeSherkat;
                    var OldNamad = DB.Namads.FirstOrDefault(q => q.CodeSherkat == codeSherkatExcel);
                    if (OldNamad == null)
                    {
                        Models.DomainModels.Namad N = new Models.DomainModels.Namad();
                        N.CodeSherkat = codeSherkatExcel;
                        N.Namad1 = sheetdata.Namad;
                        DB.Namads.Add(N);
                        DB.SaveChanges();
                        MasterId = N.Id;
                    }
                    else
                    {
                        MasterId = OldNamad.Id;
                    }
                    //Details
                    //if (MasterId == 3780)
                    //{
                    //    var x = 1;
                    //}
                    var res = DB.NamadDetails.SingleOrDefault(q => q.ShamsyDate == sheetdata.DateShamsy && q.NamadId == MasterId);
                    if (res != null)
                    {
                        // res.NamadId = MasterId;
                        res.Hajm = sheetdata.Hajm;
                        res.TedadMoamelat = sheetdata.TedadMoamelat;
                        res.ShamsyDate = sheetdata.DateShamsy;
                        res.DarsadGheymatPayany = (float)Math.Round(sheetdata.DarsadGheymatPayany * 100f) / 100f;// sheetdata.DarsadGheymatPayany.ToString("0.00");
                        res.GheymatPayany = sheetdata.GheymatPayany;                                                                                   // DB.NamadDetail.Add(D);
                        DB.SaveChanges();
                    }
                    else
                    {
                        Models.DomainModels.NamadDetail D = new Models.DomainModels.NamadDetail();
                        D.NamadId = MasterId;
                        D.Hajm = sheetdata.Hajm;
                        D.TedadMoamelat = sheetdata.TedadMoamelat;
                        D.ShamsyDate = sheetdata.DateShamsy;
                        D.DarsadGheymatPayany = (float)Math.Round(sheetdata.DarsadGheymatPayany * 100f) / 100f;
                        D.GheymatPayany = sheetdata.GheymatPayany;
                        DB.NamadDetails.Add(D);
                        DB.SaveChanges();
                    }

                }

            }

            //string path = Path.Combine(Server.MapPath("~/Files"),Path.GetFileName(file.FileName));

            //string fullPath = Request.MapPath("~/Images/Cakes/" + photoName);
            //if (System.IO.File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //}

            System.IO.DirectoryInfo di = new DirectoryInfo(PathFolderAddress);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            return Json("finish");

        }
        public ActionResult Sheet() {
            string PathFolderAddress = Server.MapPath("~/Files");
            /*
          
            */
            //Read All File Path
            string[] fileEntries = Directory.GetFiles(PathFolderAddress);
            int i = 1;
            foreach (string fileName in fileEntries)
            {
                ViewModels.ExcelFiles file = new ViewModels.ExcelFiles();
                file.FileAddress = fileName;
                file.FileName = Path.GetFileName(fileName);
                file.ShamsyDate = Path.GetFileName(fileName).Split('.')[0];
                file.Row = i;
                lstFiles.Add(file);
                i += 1;
            }
            // SheetName();
            return Json(lstFiles, JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult SheetName()
        {
            var ads = "";
            foreach (var item in lstFiles)
            {
                ads = item.FileAddress;

                //  List<string> lstNameSheet = new List<string>();
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               item.FileAddress +
                               ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);

                con.Open();

                DataTable Sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    item.SheetName = sht;
                    // lstNameSheet.Add(sht);
                    //  OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select * from [" + sht + "]", con);
                }
                con.Close();
            }
            // ExcelToDataTable();
            return Json(ads, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExcelToDataTable()
        {
            foreach (var item in lstFiles)
            {
                ViewModels.ExcelSheetData SheetData = new ViewModels.ExcelSheetData();

                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           item.FileAddress +
                           ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                OleDbCommand oconn = new OleDbCommand("Select * From [" + item.SheetName + "]", con);
                // OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "]", con);

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);

                SheetData.fileName = item.ShamsyDate;
                SheetData.lstDataTable = data;
                lstSheetData.Add(SheetData);
                con.Close();
            }

           // BindDataTablesToModel();
            return Json(lstSheetData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ShamsyDateCount()
        {
            ViewModels.Saham.SahamVM SahamObj = new ViewModels.Saham.SahamVM();
            try
            {

                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT = U.Select(@"select ShamsyDate,count(ShamsyDate) count 
from NamadDetail
group by ShamsyDate
order by ShamsyDate");
                List<ViewModels.Saham.NamadVM> lstN = new List<ViewModels.Saham.NamadVM>();
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Saham.NamadVM N = new ViewModels.Saham.NamadVM();

                    N.Tedad = int.Parse(item["count"].ToString());
                    N.ShamsyDate = item["ShamsyDate"].ToString();

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
    }
}
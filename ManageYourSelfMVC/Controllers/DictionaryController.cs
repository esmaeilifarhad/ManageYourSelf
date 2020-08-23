using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ManageYourSelfMVC.Controllers
{
    [Models.Filtering.Filter]
    public class DictionaryController : Controller
    {
        #region Initial
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = Models.staticClass.staticClass.UserId;// 0;
        public DictionaryController()
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
     
        public ActionResult MainDictionary()
        {
            return View();
        }
        #region CRUD
        public ActionResult CreateWord(Models.DomainModels.dic_tbl D)
        {
            bool Result = false;
            D.level = 10;
            D.UnSuccessCount = 0;
            D.SuccessCount = 0;
            D.IsArchieve = false;
            D.timeword = 0;
            D.date_s = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
            D.date_refresh = D.date_s;
            D.CreateDateM = DateTime.Now;
            D.DateRefreshM = DateTime.Now;
            D.UserId = UserId;
            if (D.per != null)
                D.per = D.per.Trim();
            D.eng = D.eng.Trim();
            Result = T.DicInsert(D);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateWord()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult CreateWordAngular()
        {
            return PartialView();
        }
        public JsonResult UpdateWord(Models.DomainModels.dic_tbl NewObj)
        {
            try
            {
                int WordId = NewObj.id;
                var OldObj = DB.dic_tbl.SingleOrDefault(q => q.id == WordId);
                OldObj.eng = NewObj.eng;
                OldObj.per = NewObj.per.Trim();
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        public JsonResult UpdateExample(Models.DomainModels.example_tbl NewObj)
        {
            ViewModels.ErrorMessage error = new ViewModels.ErrorMessage();
            try
            {

                var OldObj = DB.example_tbl.SingleOrDefault(q => q.id == NewObj.id);
                //-----------------
                string[] parts = NewObj.example.Split(new string[] { "@@" }, StringSplitOptions.None);

                // string[] parts = NewObj.example.Split('@');

                if (parts.Length > 1)
                {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        Models.DomainModels.example_tbl E = new Models.DomainModels.example_tbl();
                        E.example = parts[i];
                        E.id_dic_tbl = OldObj.id_dic_tbl;
                        DB.example_tbl.Add(E);
                    }
                    if (DB.SaveChanges() > 0)
                    {
                        error.result = true;
                        DB.example_tbl.Remove(OldObj);
                        DB.SaveChanges();
                    }
                    else
                        error.result = false;
                }
                else
                {
                    OldObj.example = NewObj.example.Trim();
                    if (DB.SaveChanges() > 0)
                        error.result = true;
                    else
                        error.result = false;
                }
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.result = false;
                error.message = ex.Message;
                return Json(error, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditWord(int WordId)
        {
            var dic_tbl = DB.dic_tbl.SingleOrDefault(q => q.id == WordId);
            return PartialView(dic_tbl);
        }
        public ActionResult EditExample(int ExampleId)
        {
            var res = DB.example_tbl.SingleOrDefault(q => q.id == ExampleId);
            return PartialView(res);
        }
        public JsonResult DeleteWord(int id)
        {
            bool Result = false;
            Result = T.WordDelete(id);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ArchieveWord(int id, bool res)
        {
            try
            {
                var OldObj = DB.dic_tbl.SingleOrDefault(q => q.id == id);
                if (OldObj.level != 1)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                if (res == true)
                {
                    OldObj.IsArchieve = true;
                    OldObj.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                    OldObj.DateRefreshM = DateTime.Now;
                }
                else
                {
                    OldObj.IsArchieve = false;
                    OldObj.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                    OldObj.DateRefreshM = DateTime.Now;
                }
                DB.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult RemoveExample(int WordId)
        {
            var examples = DB.example_tbl.Where(q => q.id_dic_tbl == WordId).ToList();
            if (examples.Count == 0)
                return null;
            return PartialView(examples);
        }
        [HttpPost]
        public JsonResult RemoveExamplePost(int ExampleId)
        {
            var example = DB.example_tbl.SingleOrDefault(q => q.id == ExampleId);
            DB.example_tbl.Remove(example);
            if (DB.SaveChanges() > 0)
                return Json("Remove Success", JsonRequestBehavior.AllowGet);
            else return Json("Error in RemoveExamplePost", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteExample(int ExampleId)
        {
            bool Result = false;
            Result = T.ExampleDelete(ExampleId);
            if (Result == true)
            {
                TempData["Message"] = "حذف با موفقیت انجام شد";
            }
            // return RedirectToAction("ListNav", "Client");
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewExample()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult CreateExample(Models.DomainModels.example_tbl NewExample)
        {
            bool result = false;
            DB.example_tbl.Add(NewExample);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region List
        [HttpGet]
        public JsonResult ListExample(int id)
        {
            ViewModels.Dictionary.VMDictionary lstExample = T.ExampleList(id);
            //List<Models.DomainModels.example_tbl> lstExample = T.ExampleList(id);
            //string json = JsonConvert.SerializeObject(lstExample, Formatting.None);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstExample);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListDictionary()
        {
            //int x = (int)System.Web.HttpContext.Current.Session["UserId"];
            /*
            A circular reference was detected while serializing an object of type 'System.Data.Entity.DynamicProxies
            Its because it is trying to load child objects and it may be creating some circular loop that will never ending( a=>b, b=>c, c=>d, d=>a)
            you can turn it off only for that particular moment as following.So dbcontext will not load customers child objects unless Include method is called on your object
            db.Configuration.ProxyCreationEnabled = false;
            User ma = db.user.First(x => x.u_id == id);
            return Json(ma, JsonRequestBehavior.AllowGet);
            */
            DB.Configuration.ProxyCreationEnabled = false;
            List<ViewModels.DictionaryVM> ListDicVM = new List<ViewModels.DictionaryVM>();
            List<Models.DomainModels.dic_tbl> lstdic_tbl = DB.dic_tbl.Where(q => q.UserId == UserId && q.IsArchieve == false).OrderBy(q => q.date_refresh).OrderByDescending(q => new { q.level }).ToList();
            foreach (var item in lstdic_tbl)
            {
                ViewModels.DictionaryVM D = new ViewModels.DictionaryVM();
                D.date_refresh = item.date_refresh;
                D.date_s = item.date_s;
                D.eng = item.eng;
                D.id = item.id;
                D.level = item.level;
                D.per = item.per;
                D.Phonetic = item.Phonetic;
                D.SuccessCount = item.SuccessCount;
                D.UnSuccessCount = item.UnSuccessCount;
                D.HasExample = T.HasExample(item.id);
                ListDicVM.Add(D);
            }
            //List<Models.DomainModels.example_tbl> lstExample = T.ExampleList(id);
            //string json = JsonConvert.SerializeObject(lstExample, Formatting.None);
            //var jsonSerialiser = new JavaScriptSerializer();
            //var json = jsonSerialiser.Serialize(lstdic_tbl);

            return Json(ListDicVM, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListDictionaryHamrahBaExample(int SkipN)
        {
            SkipN = SkipN - 1;
            Models.Help.Setting.takeCount = 1;
            int takeCount = Models.Help.Setting.takeCount;
            SkipN = SkipN * takeCount;
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
            var lstDic = DB.dic_tbl.Where(q => q.UserId == UserId && q.IsArchieve == false).OrderBy(q => q.date_refresh).OrderByDescending(q => new { q.level }).Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh }).Skip(SkipN).Take(takeCount).ToList();
            foreach (var item in lstDic)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.eng = item.eng;
                V.per = item.per;
                V.id = item.id;
                V.level = (int)item.level;
                V.date_refresh = item.date_refresh;
                V.HasExample = T.HasExample(item.id);
                //V.CountOfWord = T.CountWord();
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                lstV.Add(V);
            }
            return PartialView(lstV);
        }
        public ActionResult ListWordExampleDiv(string str)
        {
            if (str == null || str == "" || str == string.Empty)
            {
                int MaxLevelCount = 0;
                var results = (from W in DB.dic_tbl
                               group W.eng by W.level into g
                               select new { level = g.Key, GroupWord = g.ToList() }).
                               OrderByDescending(x => x.GroupWord.Count).Take(1);
                // int AllCount = 0;
                foreach (var item in results)
                {
                    // ViewModels.Dictionary.VMDictionary V2 = new ViewModels.Dictionary.VMDictionary();
                    // V2.NameLevel = item.level.ToString();
                    MaxLevelCount = (int)item.level;
                    // AllCount += item.GroupWord.Count();
                    //lstCount.Add(V2);
                }

                List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
                var lstDic = DB.dic_tbl.Where(q => q.level == MaxLevelCount && q.UserId == UserId).
                    Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.SuccessCount, q.UnSuccessCount, q.IsArchieve }).
                    OrderBy(q => q.date_refresh).
                    ThenByDescending(q => new { q.level }).
                    Take(5).ToList();
                foreach (var item in lstDic)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.eng = item.eng;
                    V.per = item.per;
                    V.id = item.id;
                    V.IsArchieve = item.IsArchieve;
                    V.SuccessCount = item.SuccessCount;
                    V.UnSuccessCount = item.UnSuccessCount;
                    V.level = (int)item.level;
                    V.date_refresh = item.date_refresh;
                    V.HasExample = T.HasExample(item.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                    lstV.Add(V);
                }
                return PartialView(lstV);
            }
            else//-------------search
            {
                List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
                var lstDic = DB.dic_tbl.
                    Where(q => q.eng.Contains(str) && q.UserId == UserId).
                    Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.SuccessCount, q.UnSuccessCount, q.IsArchieve }).
                    OrderBy(q => q.date_refresh).ThenByDescending(q => new { q.level }).
                    Take(5).ToList();
                foreach (var item in lstDic)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.eng = item.eng;
                    V.per = item.per;
                    V.id = item.id;
                    V.IsArchieve = item.IsArchieve;
                    V.SuccessCount = item.SuccessCount;
                    V.UnSuccessCount = item.UnSuccessCount;
                    V.level = (int)item.level;
                    V.date_refresh = item.date_refresh;
                    V.HasExample = T.HasExample(item.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                    lstV.Add(V);
                }
                return PartialView(lstV);
            }
        }
        public ActionResult SearchWordInExamples(string str)
        {
            if (str == null || str == "" || str == string.Empty)
            {
                int MaxLevelCount = 0;
                var results = (from W in DB.dic_tbl
                               group W.eng by W.level into g
                               select new { level = g.Key, GroupWord = g.ToList() }).OrderByDescending(x => x.GroupWord.Count).Take(1);
                // int AllCount = 0;
                foreach (var item in results)
                {
                    // ViewModels.Dictionary.VMDictionary V2 = new ViewModels.Dictionary.VMDictionary();
                    // V2.NameLevel = item.level.ToString();
                    MaxLevelCount = (int)item.level;
                    // AllCount += item.GroupWord.Count();
                    //lstCount.Add(V2);
                }

                List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
                var lstDic = DB.dic_tbl.Where(q => q.level == MaxLevelCount && q.UserId == UserId).Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.SuccessCount, q.UnSuccessCount, q.IsArchieve }).OrderBy(q => q.date_refresh).ThenByDescending(q => new { q.level }).Take(10).ToList();
                foreach (var item in lstDic)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.eng = item.eng;
                    V.per = item.per;
                    V.id = item.id;
                    V.IsArchieve = item.IsArchieve;
                    V.SuccessCount = item.SuccessCount;
                    V.UnSuccessCount = item.UnSuccessCount;
                    V.level = (int)item.level;
                    V.date_refresh = item.date_refresh;
                    V.HasExample = T.HasExample(item.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                    lstV.Add(V);
                }
                return PartialView(lstV);
            }
            else//-------------search
            {
                List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
                DataTable DT = U.Select(@"
select 
dic_tbl.id
,dic_tbl.eng
,dic_tbl.per
,dic_tbl.level
,dic_tbl.IsArchieve
,dic_tbl.date_refresh
,dic_tbl.date_s
,dic_tbl.SuccessCount
,dic_tbl.UnSuccessCount
from example_tbl inner join dic_tbl
on example_tbl.id_dic_tbl=dic_tbl.id
where example like '%" + str + @"%' 
 group by eng,dic_tbl.id,dic_tbl.per,dic_tbl.level,dic_tbl.IsArchieve,dic_tbl.date_refresh,dic_tbl.date_s,dic_tbl.SuccessCount,dic_tbl.UnSuccessCount,dic_tbl.IsArchieve
");
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.id = int.Parse(item["id"].ToString());
                    V.eng = item["eng"].ToString();
                    V.per = item["per"].ToString();
                    V.IsArchieve = (bool)item["IsArchieve"];
                    V.level = int.Parse(item["level"].ToString());
                    V.date_refresh = item["date_refresh"].ToString();
                    V.date_s = item["date_s"].ToString();
                    V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                    V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                    V.HasExample = T.HasExample(V.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id && q.example.Contains(str)).ToList();
                    lstV.Add(V);
                }
                return PartialView("ListWordExampleDiv", lstV);
            }
        }
        public ActionResult RandomWordWithSound(string str)
        {
            try { 
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
            if (str == "106")
            {

                DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.id = int.Parse(item["id"].ToString());
                    V.eng = item["eng"].ToString();
                    V.per = item["per"].ToString();
                    V.level = int.Parse(item["level"].ToString());
                    V.date_refresh = item["date_refresh"].ToString();
                    V.date_s = item["date_s"].ToString();
                    V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                    V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                    V.HasExample = T.HasExample(V.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();
                    V.statusCheck = false;
                    lstV.Add(V);
                }
                int CountWord = lstV.Count();
                Random rnd = new System.Random();
                int RadifWord1 = rnd.Next(0, CountWord);
                int RadifWord2 = rnd.Next(0, CountWord);
                int RadifWord3 = rnd.Next(0, CountWord);
                int RadifWord4 = rnd.Next(0, CountWord);
                lstV[RadifWord1].statusCheck = true;
            }
            else if (int.Parse(str) >= 0 && int.Parse(str) <= 10)
            {
                int lvl = int.Parse(str);
                var lstDic = DB.dic_tbl.
                   Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.UserId, q.SuccessCount, q.UnSuccessCount, q.IsArchieve, q.time }).
                   Where(q => q.level == lvl & q.UserId == UserId & q.IsArchieve == false).
                   OrderBy(q => q.date_refresh).
                   ThenByDescending(q => new { q.level }).ThenBy(q => q.time).ThenBy(q => q.id).
                   Take(5).
                   ToList();
                foreach (var item in lstDic)
                {
                    ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                    V.eng = item.eng;
                    V.per = item.per;
                    V.SuccessCount = item.SuccessCount;
                    V.UnSuccessCount = item.UnSuccessCount;
                    V.id = item.id;
                    V.level = (int)item.level;
                    V.date_refresh = item.date_refresh;
                    V.HasExample = T.HasExample(item.id);
                    V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                    V.statusCheck = false;
                    lstV.Add(V);
                }
                int CountWord = lstV.Count();
                Random rnd = new System.Random();
                int RadifWord1 = rnd.Next(0, CountWord);
                int RadifWord2 = rnd.Next(0, CountWord);
                int RadifWord3 = rnd.Next(0, CountWord);
                int RadifWord4 = rnd.Next(0, CountWord);
                lstV[RadifWord1].statusCheck = true;
            }
            else
            {

            }
            return PartialView(lstV);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
        public ActionResult ListWordExampleDivChk(List<string> MyData)
        {
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
            if (MyData != null)
            {
                string str = MyData[0].TrimEnd(',');

                string[] ids = str.Split(',');

                //badGhelegh
                if (ids.Contains("101"))
                {
                    var lstDic = (from p in DB.dic_tbl
                                  where p.UserId == UserId && p.IsArchieve == false
                                  orderby (((int)p.UnSuccessCount - (int)p.SuccessCount)) descending, p.date_refresh ascending, p.time, p.id
                                  //orderby p.UnSuccessCount descending
                                  select p).AsEnumerable().Select(q => new
                                  { q.eng, q.SuccessCount, q.UnSuccessCount, q.date_refresh, q.per, q.id, q.level, Grade = (q.UnSuccessCount - q.SuccessCount) }).
                                  Take(3).ToList();
                    foreach (var item in lstDic)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }
                //Best for Remove
                if (ids.Contains("104"))
                {
                    var lstDic = (from p in DB.dic_tbl
                                  where p.UserId == UserId && p.IsArchieve == false
                                  orderby (((int)p.SuccessCount - (int)p.UnSuccessCount)) descending, p.date_refresh ascending, p.time, p.id
                                  //orderby p.UnSuccessCount descending
                                  select p).AsEnumerable().Select(q => new
                                  { q.eng, q.SuccessCount, q.UnSuccessCount, q.date_refresh, q.per, q.id, q.level, Grade = (q.UnSuccessCount - q.SuccessCount) }).Take(5).ToList();
                    foreach (var item in lstDic)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }
                //Top10LastMoroor
                else if (ids.Contains("102"))
                {
                    var res = (from W in DB.dic_tbl
                               where W.UserId == UserId && W.IsArchieve == false
                               orderby W.date_refresh, W.time, W.id
                               select new { CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount), W.id, W.eng, W.per, WordId = W.id, W.date_refresh, W.level, W.SuccessCount, W.UnSuccessCount }
                                 ).Take(3).ToList();
                    foreach (var item in res)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }
                //کمترین تعداد مرور
                else if (ids.Contains("103"))
                {
                    var res = (from W in DB.dic_tbl
                               where W.UserId == UserId && W.IsArchieve == false
                               orderby (int)W.UnSuccessCount + (int)W.SuccessCount, W.date_refresh, W.time, W.id
                               select new { CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount), W.eng, W.per, WordId = W.id, W.SuccessCount, W.UnSuccessCount, W.id, W.date_refresh, W.level }
                             ).Take(3).ToList();

                    foreach (var item in res)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }
                //آرشیو
                else if (ids.Contains("105"))
                {
                    var res = (from W in DB.dic_tbl
                               where W.UserId == UserId && W.IsArchieve == true
                               orderby (W.date_refresh), W.time, W.id
                               select new { CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount), W.eng, W.per, WordId = W.id, W.SuccessCount, W.UnSuccessCount, W.id, W.date_refresh, W.level, W.IsArchieve }
                             ).Take(10).ToList();

                    foreach (var item in res)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.IsArchieve = item.IsArchieve;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }
                else if (ids.Contains("106"))
                {
                    // List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();

                    DataTable DT = U.Select("exec [PersianToEnglish] " + UserId.ToString());
                    foreach (DataRow item in DT.Rows)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.id = int.Parse(item["id"].ToString());
                        V.eng = item["eng"].ToString();
                        V.per = item["per"].ToString();
                        V.level = int.Parse(item["level"].ToString());
                        V.date_refresh = item["date_refresh"].ToString();
                        V.date_s = item["date_s"].ToString();
                        V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                        V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                        V.HasExample = T.HasExample(V.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();

                        lstV.Add(V);
                    }
                }
                else
                {

                    var lstDic = DB.dic_tbl.
                    Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.UserId, q.SuccessCount, q.UnSuccessCount, q.IsArchieve, q.time }).
                    Where(q => ids.Contains(q.level.ToString()) & q.UserId == UserId & q.IsArchieve == false).
                    OrderBy(q => q.date_refresh).
                    ThenByDescending(q => new { q.level }).ThenBy(q => q.time).ThenBy(q => q.id).
                    Take(3).
                    ToList();
                    foreach (var item in lstDic)
                    {
                        ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                        V.eng = item.eng;
                        V.per = item.per;
                        V.SuccessCount = item.SuccessCount;
                        V.UnSuccessCount = item.UnSuccessCount;
                        V.id = item.id;
                        V.level = (int)item.level;
                        V.date_refresh = item.date_refresh;
                        V.HasExample = T.HasExample(item.id);
                        V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                        lstV.Add(V);
                    }
                }






            }
            return PartialView("ListWordExampleDiv", lstV);
        }
        //یرای مواردی که اشتباه کاربر جواب داده از کوئری زیر استفاده مینماییم
        public ActionResult ListWordExampleSucc_OR_UnSucc()
        {
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();

            DataTable DT = U.Select("exec [5069_ManageYourSelf].[5069_Esmaeili].[PersianToEnglish] " + UserId.ToString());
            /*     DataTable DT = U.Select(@"

     select * from dic_tbl D
     inner join
     (
     select isnull(A.Countt,0)-isnull(B.Countt,0) diff,A.WordId  from 
     (
     -----------
     select WordId,Succ_OR_UnSucc,count(Succ_OR_UnSucc) Countt   
     from [DaysExercise]
     where Succ_OR_UnSucc=1
     group by
     GROUPING sets(
     (WordId,Succ_OR_UnSucc)
     )
     --------
     ) A
     left join
     (
     ---------
     select WordId,Succ_OR_UnSucc,isnull(count(Succ_OR_UnSucc),0) Countt   
     from [DaysExercise]
     where Succ_OR_UnSucc=0
     group by
     GROUPING sets(
     (WordId,Succ_OR_UnSucc)
     )
     -----------
     ) B
     on A.WordId=B.WordId --and A.Succ_OR_UnSucc=B.Succ_OR_UnSucc
     )
     DE
     on  DE.WordId=D.id
     where DE.diff>0 and UserId="+UserId.ToString()+@"
     order by DE.diff desc,D.Date_Refresh asc
     ");
                 */
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                // V.Radif = int.Parse(item["Radif"].ToString());
                V.id = int.Parse(item["id"].ToString());
                V.eng = item["eng"].ToString();
                V.per = item["per"].ToString();
                V.level = int.Parse(item["level"].ToString());
                V.date_refresh = item["date_refresh"].ToString();
                V.date_s = item["date_s"].ToString();
                V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                V.HasExample = T.HasExample(V.id);
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();
                lstV.Add(V);
            }

            /*
            var lstDic = DB.dic_tbl.Select(q => new { q.id, q.eng, q.per, q.level, q.date_refresh, q.UserId, q.SuccessCount, q.UnSuccessCount }).Where(q => q.UserId == UserId).OrderBy(q => q.date_refresh).ThenByDescending(q => new { q.level }).Take(10).ToList();
            foreach (var item in lstDic)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.eng = item.eng;
                V.per = item.per;
                V.SuccessCount = item.SuccessCount;
                V.UnSuccessCount = item.UnSuccessCount;
                V.id = item.id;
                V.level = (int)item.level;
                V.date_refresh = item.date_refresh;
                V.HasExample = T.HasExample(item.id);
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                lstV.Add(V);
            }
            */
            return PartialView("ListWordExampleDiv", lstV);
        }
        public ActionResult ListPersianToEnglish()
        {
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();

            DataTable DT = U.Select("exec [5069_ManageYourSelf].[5069_Esmaeili].[PersianToEnglish] " + UserId.ToString());
            /* DataTable DT = U.Select(@"

 select * from dic_tbl D
 inner join
 (
 select isnull(A.Countt,0)-isnull(B.Countt,0) diff,A.WordId  from 
 (
 -----------
 select WordId,Succ_OR_UnSucc,count(Succ_OR_UnSucc) Countt   
 from [DaysExercise]
 where Succ_OR_UnSucc=1
 group by
 GROUPING sets(
 (WordId,Succ_OR_UnSucc)
 )
 --------
 ) A
 left join
 (
 ---------
 select WordId,Succ_OR_UnSucc,isnull(count(Succ_OR_UnSucc),0) Countt   
 from [DaysExercise]
 where Succ_OR_UnSucc=0
 group by
 GROUPING sets(
 (WordId,Succ_OR_UnSucc)
 )
 -----------
 ) B
 on A.WordId=B.WordId --and A.Succ_OR_UnSucc=B.Succ_OR_UnSucc
 )
 DE
 on  DE.WordId=D.id
 where DE.diff>0 and UserId=" + UserId.ToString() + @"
 order by DE.diff desc,D.Date_Refresh asc
 ");
             */
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                // V.Radif = int.Parse(item["Radif"].ToString());
                V.id = int.Parse(item["id"].ToString());
                V.eng = item["eng"].ToString();
                V.per = item["per"].ToString();
                V.level = int.Parse(item["level"].ToString());
                V.date_refresh = item["date_refresh"].ToString();
                V.date_s = item["date_s"].ToString();
                V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                V.HasExample = T.HasExample(V.id);
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();
                lstV.Add(V);
            }


            return PartialView(lstV);
        }
        #endregion

        public JsonResult MaxLevelCount()
        {
            int MaxLevelCount = 0;
            var results = (from W in DB.dic_tbl
                           group W.eng by W.level into g
                           select new { level = g.Key, GroupWord = g.ToList() }).OrderByDescending(x => x.GroupWord.Count).Take(1);
            foreach (var item in results)
            {
                MaxLevelCount = (int)item.level;
            }

            return Json(MaxLevelCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SerchWord(string Word)
        {
            int i = 1;
            string FindWord = string.Empty;
            var SerchWords = DB.dic_tbl.Where(q => q.eng.Contains(Word));
            foreach (var item in SerchWords)
            {
                FindWord += item.eng.ToString() + " _ " + i.ToString() + " ";
                i = i + 1;
            }
            return Json(FindWord, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SearchWordList()
        {
            return PartialView();
        }
        public ActionResult SearchWordList2(string Word)
        {
            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();
            var SerchWords = DB.dic_tbl.Where(q => q.eng.Contains(Word));
            foreach (var item in SerchWords)
            {
                ViewModels.Dictionary.VMDictionary v = new ViewModels.Dictionary.VMDictionary();
                v.eng = item.eng;
                v.per = item.per;
                v.IsArchieve = item.IsArchieve;
                v.id = item.id;
                v.SuccessCount = item.SuccessCount;
                v.UnSuccessCount = item.UnSuccessCount;
                v.level = (int)item.level;
                v.date_refresh = item.date_refresh;
                v.HasExample = T.HasExample(item.id);
                v.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();

                lstV.Add(v);
            }
            return PartialView(lstV);
        }
        [HttpPost]
        public async Task<ActionResult> TTS(string str)
        {
            Task<ViewResult> task = Task.Run(() =>
            {
                using (SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer())
                {
                    speechSynthesizer.Speak(str);
                    return View();
                }
            });
            return await task;
        }
        public ActionResult TestSound()
        {
            return View();
        }
        #region LevelChange
        public JsonResult LevelChangeDown(int WordId)
        {
            bool result = false;
            var OldDic = DB.dic_tbl.SingleOrDefault(q => q.id == WordId);
            //دیگه سطح کمتر از یک نشود
            if (OldDic.level > 1)
            {
                OldDic.level = OldDic.level - 1;
                OldDic.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                OldDic.DateRefreshM = DateTime.Now;
                OldDic.time = DateTime.Now.ToString("HH:mm:ss");
                OldDic.SuccessCount = OldDic.SuccessCount + 1;

            }
            else
            {
                // OldDic.level = OldDic.level - 1;
                OldDic.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                OldDic.DateRefreshM = DateTime.Now;
                OldDic.time = DateTime.Now.ToString("HH:mm:ss");
                OldDic.IsArchieve = true;
                 OldDic.SuccessCount = OldDic.SuccessCount + 1;
            }
            // DB.Entry(OldDic).State = EntityState.Modified;
            if (DB.SaveChanges() > 0)
                result = true;
            Models.DomainModels.DaysExercise DE = new Models.DomainModels.DaysExercise();
            DE.Succ_OR_UnSucc = false;
            DE.WordId = WordId;
            DE.DateExercise = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            DB.DaysExercises.Add(DE);
            // DB.Entry(DE).State = EntityState.Added;
            DB.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LevelChangeUp(int WordId)
        {
            bool result = false;
            var OldDic = DB.dic_tbl.SingleOrDefault(q => q.id == WordId);
            //دیگه سطح بیشتر از ده نشود
            if (OldDic.level < 10)
            {
                OldDic.level = OldDic.level + 1;
                OldDic.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                OldDic.DateRefreshM = DateTime.Now;
                OldDic.time = DateTime.Now.ToString("HH:mm:ss");
                OldDic.UnSuccessCount = OldDic.UnSuccessCount + 1;
                if (OldDic.IsArchieve == true)
                {
                    OldDic.IsArchieve = false;
                }
            }
            else
            {
                //OldDic.level = OldDic.level + 1;
                OldDic.date_refresh = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
                OldDic.DateRefreshM = DateTime.Now;
                OldDic.time = DateTime.Now.ToString("HH:mm:ss");
                OldDic.UnSuccessCount = OldDic.UnSuccessCount + 1;

            }
            if (DB.SaveChanges() > 0)
                result = true;
            Models.DomainModels.DaysExercise DE = new Models.DomainModels.DaysExercise();
            DE.Succ_OR_UnSucc = true;
            DE.WordId = WordId;
            DE.DateExercise = Utility.Utility.shamsi_date().ConvertDateToSqlFormat();
            DB.DaysExercises.Add(DE);
            DB.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 5 Ta
        public ActionResult ShowLevel()
        {

            List<ViewModels.Dictionary.VMDictionary> lstCount = new List<ViewModels.Dictionary.VMDictionary>();
            var results = from W in DB.dic_tbl
                          where W.UserId == UserId && W.IsArchieve == false
                          group W.eng by W.level into g

                          select new { level = g.Key, GroupWord = g.ToList() };
            int AllCount = 0;
            foreach (var item in results)
            {
                ViewModels.Dictionary.VMDictionary V2 = new ViewModels.Dictionary.VMDictionary();
                V2.NameLevel = item.level.ToString();
                V2.CountLevel = item.GroupWord.Count();
                AllCount += item.GroupWord.Count();
                lstCount.Add(V2);
            }
            ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
            V.NameLevel = "جمع";
            V.CountLevel = AllCount;
            lstCount.Add(V);

            ViewModels.Dictionary.VMDictionary V3 = new ViewModels.Dictionary.VMDictionary();
            V3.NameLevel = "آرشیو";
            V3.CountLevel = DB.dic_tbl.Where(q => q.UserId == UserId && q.IsArchieve == true).Count();
            lstCount.Add(V3);

            ViewModels.Dictionary.VMDictionary V4 = new ViewModels.Dictionary.VMDictionary();
            V4.NameLevel = "جمع کل";
            V4.CountLevel = DB.dic_tbl.Where(q => q.UserId == UserId).Count();
            lstCount.Add(V4);
            return PartialView(lstCount);
        }
        public ActionResult Top10MaxGroupBy()
        {

            List<ViewModels.Dictionary.VMDictionary> lstCount = new List<ViewModels.Dictionary.VMDictionary>();
            int MaxLevelCount = 0;
            var results = (from W in DB.dic_tbl
                           where W.UserId == UserId
                           group W.eng by W.level into g
                           select new { level = g.Key, GroupWord = g.ToList() }).OrderByDescending(x => x.GroupWord.Count).Take(1);
            // int AllCount = 0;
            foreach (var item in results)
            {
                // ViewModels.Dictionary.VMDictionary V2 = new ViewModels.Dictionary.VMDictionary();
                // V2.NameLevel = item.level.ToString();
                MaxLevelCount = (int)item.level;
                // AllCount += item.GroupWord.Count();
                //lstCount.Add(V2);
            }
            var QuerySelect = (from w in DB.dic_tbl
                               where w.level == MaxLevelCount && w.UserId == UserId
                               select new { w.eng, w.per, w.level, w.id, w.date_refresh, w.SuccessCount, w.UnSuccessCount }
                            ).OrderBy(q => q.date_refresh).Take(10);
            List<Models.DomainModels.example_tbl> ListExa = new List<Models.DomainModels.example_tbl>();
            foreach (var item in QuerySelect)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.eng = item.eng;
                V.per = item.per;
                V.level = item.level;
                V.id = item.id;
                V.SuccessCount = item.SuccessCount;
                V.UnSuccessCount = item.UnSuccessCount;
                V.Grade = (int)item.SuccessCount - (int)item.UnSuccessCount;
                V.date_refresh = item.date_refresh;
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == item.id).ToList();
                lstCount.Add(V);
            }

            return PartialView(lstCount);
        }
        public ActionResult BadGheleghtarinWord()
        {

            var data = (from p in DB.dic_tbl
                        where p.UserId == UserId
                        orderby (((int)p.UnSuccessCount - (int)p.SuccessCount)) descending, p.date_refresh

                        //orderby p.UnSuccessCount descending
                        select p).AsEnumerable().Select(q => new
                        { q.eng, q.per, q.id, q.level, Grade = (q.UnSuccessCount - q.SuccessCount) }).Take(10).ToList();


            List<ViewModels.Dictionary.VMDictionary> lstVMBadGheleghtarinWord = new List<ViewModels.Dictionary.VMDictionary>();
            foreach (var item in data)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.eng = item.eng;
                V.per = item.per;
                V.Grade = (int)item.Grade;
                V.level = (int)item.level;
                V.id = item.id;
                lstVMBadGheleghtarinWord.Add(V);
            }
            return PartialView("BadGheleghtarinWord", lstVMBadGheleghtarinWord);
        }
        public ActionResult LessMoroor()
        {
            List<ViewModels.Dictionary.VMDictionary> lstLessMoroorVM = new List<ViewModels.Dictionary.VMDictionary>();
            var res = (from W in DB.dic_tbl
                       where W.UserId == UserId
                       orderby (((int)W.UnSuccessCount + (int)W.SuccessCount))
                       select new { CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount), W.eng, W.per, WordId = W.id }
                              ).Take(10).ToList();
            foreach (var item in res)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.CountMoroor = item.CountMoroor;
                V.eng = item.eng;
                V.per = item.per;
                V.id = item.WordId;
                lstLessMoroorVM.Add(V);
            }
            return PartialView(lstLessMoroorVM);
        }
        public ActionResult Top10LastMoroor()
        {
            List<ViewModels.Dictionary.VMDictionary> lstVM = new List<ViewModels.Dictionary.VMDictionary>();
            var res = (from W in DB.dic_tbl
                       where W.UserId == UserId
                       orderby W.date_refresh
                       select new { CountMoroor = ((int)W.UnSuccessCount + (int)W.SuccessCount), W.eng, W.per, WordId = W.id, W.date_refresh, W.level, W.SuccessCount, W.UnSuccessCount }
                              ).Take(10).ToList();
            foreach (var item in res)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.CountMoroor = item.CountMoroor;
                V.eng = item.eng;
                V.per = item.per;
                V.id = item.WordId;
                V.level = item.level;
                V.date_refresh = item.date_refresh;
                lstVM.Add(V);
            }
            return PartialView(lstVM);
        }
        #endregion
        #region RandomWord
        public ActionResult RandomWord()
        {
            List<ViewModels.Dictionary.VMDictionary> lstDicVM = new List<ViewModels.Dictionary.VMDictionary>();
            int CountWord = DB.dic_tbl.Where(q => q.UserId == UserId).Count();
            Random rnd = new System.Random();
            string RadifWord1 = rnd.Next(1, CountWord).ToString();
            string RadifWord2 = rnd.Next(1, CountWord).ToString();
            string RadifWord3 = rnd.Next(1, CountWord).ToString();
            string RadifWord4 = rnd.Next(1, CountWord).ToString();
            DataTable DT = U.Select(@"select * from
(
select ROW_NUMBER() over(order by id) Radif,* from [dbo].[dic_tbl]
) as TblWords
where Radif in (" + RadifWord1 + "," + RadifWord2 + "," + RadifWord3 + "," + RadifWord4 + ")");
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary DicVM = new ViewModels.Dictionary.VMDictionary();
                DicVM.Radif = int.Parse(item["Radif"].ToString());
                DicVM.id = int.Parse(item["id"].ToString());
                DicVM.eng = item["eng"].ToString();
                DicVM.per = item["per"].ToString();
                DicVM.level = int.Parse(item["level"].ToString());
                DicVM.date_refresh = item["date_refresh"].ToString();
                DicVM.date_s = item["date_s"].ToString();
                DicVM.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                DicVM.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                DicVM.lstExample = DB.example_tbl.Where(q => q.id == DicVM.id).ToList();
                lstDicVM.Add(DicVM);
            }
            return PartialView(lstDicVM);
        }
        public ActionResult RandomWord_HardWord()
        {
            //int CountWord=int.Parse(U.OneRecord("select count(*) from [dbo].[dic_tbl] where UserId=" + UserId));
            //if (CountWord < 12)
            //{
            //    return null;
            //}
            List<ViewModels.Dictionary.VMDictionary> lstDicVM = new List<ViewModels.Dictionary.VMDictionary>();
            DataTable DT = U.Select(@"
select top 10 ROW_NUMBER() over(order by (UnSuccessCount-SuccessCount) desc) Radif, * from [dbo].[dic_tbl]
");
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary DicVM = new ViewModels.Dictionary.VMDictionary();
                DicVM.Radif = int.Parse(item["Radif"].ToString());
                DicVM.id = int.Parse(item["id"].ToString());
                DicVM.eng = item["eng"].ToString();
                DicVM.per = item["per"].ToString();
                DicVM.level = int.Parse(item["level"].ToString());
                DicVM.date_refresh = item["date_refresh"].ToString();
                DicVM.date_s = item["date_s"].ToString();
                DicVM.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                DicVM.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                DicVM.lstExample = DB.example_tbl.Where(q => q.id == DicVM.id).ToList();
                lstDicVM.Add(DicVM);
            }
            return PartialView(lstDicVM);
        }
        public ActionResult RandomWord_HardWordExample()
        {
            List<ViewModels.Dictionary.VMDictionary> lstDicVM = new List<ViewModels.Dictionary.VMDictionary>();
            DataTable DT = U.Select(@"
select top 6 ROW_NUMBER() over(order by (UnSuccessCount-SuccessCount) desc) Radif,* from [dbo].[dic_tbl] W inner join
(
select distinct id_dic_tbl from dbo.example_tbl
) E
on W.id=E.id_dic_tbl");
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary DicVM = new ViewModels.Dictionary.VMDictionary();
                DicVM.Radif = int.Parse(item["Radif"].ToString());
                DicVM.id = int.Parse(item["id"].ToString());
                DicVM.eng = item["eng"].ToString();
                DicVM.per = item["per"].ToString();
                DicVM.level = int.Parse(item["level"].ToString());
                DicVM.date_refresh = item["date_refresh"].ToString();
                DicVM.date_s = item["date_s"].ToString();
                DicVM.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                DicVM.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                DicVM.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == DicVM.id).ToList();
                lstDicVM.Add(DicVM);
            }
            return PartialView(lstDicVM);
        }
        #endregion
        #region Angular
        public ActionResult ListWordExampleFalse()
        {
            return View();
        }
        public JsonResult ListWordExampleFals()
        {
            DB.Configuration.ProxyCreationEnabled = false;

            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();

            DataTable DT = U.Select(@"
                            SELECT top 10 *
                            FROM [5069_ManageYourSelf].[dbo].[DaysExercise] DE
                            inner join dic_tbl D
                            on DE.WordId=D.id
                            where DE.Succ_OR_UnSucc=1
                            order by DateExercise desc");
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.id = int.Parse(item["WordId"].ToString());
                V.eng = item["eng"].ToString();
                V.per = item["per"].ToString();
                V.level = int.Parse(item["level"].ToString());
                V.date_refresh = item["date_refresh"].ToString();
                V.date_s = item["date_s"].ToString();
                V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                V.HasExample = T.HasExample(V.id);
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();
                lstV.Add(V);
            }


            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListWordExampleAngular()
        {
            DB.Configuration.ProxyCreationEnabled = false;

            List<ViewModels.Dictionary.VMDictionary> lstV = new List<ViewModels.Dictionary.VMDictionary>();

            DataTable DT = U.Select(@"
                            SELECT top 10 *
                            FROM [5069_ManageYourSelf].[dbo].[DaysExercise] DE
                            inner join dic_tbl D
                            on DE.WordId=D.id
                            where DE.Succ_OR_UnSucc=1
                            order by DateExercise desc");
            foreach (DataRow item in DT.Rows)
            {
                ViewModels.Dictionary.VMDictionary V = new ViewModels.Dictionary.VMDictionary();
                V.id = int.Parse(item["WordId"].ToString());
                V.eng = item["eng"].ToString();
                V.per = item["per"].ToString();
                V.level = int.Parse(item["level"].ToString());
                V.date_refresh = item["date_refresh"].ToString();
                V.date_s = item["date_s"].ToString();
                V.SuccessCount = int.Parse(item["SuccessCount"].ToString());
                V.UnSuccessCount = int.Parse(item["UnSuccessCount"].ToString());
                V.HasExample = T.HasExample(V.id);
                V.lstExample = DB.example_tbl.Where(q => q.id_dic_tbl == V.id).ToList();
                lstV.Add(V);
            }

            // var jsonSerialiser = new JavaScriptSerializer();
            // var json = jsonSerialiser.Serialize(lstV);

            return Json(lstV, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
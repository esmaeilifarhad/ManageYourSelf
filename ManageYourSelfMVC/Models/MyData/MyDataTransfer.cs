using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageYourSelfMVC.Models.DomainModels;

namespace ManageYourSelfMVC.Models.MyData
{
    public class MyDataTransfer
    {
        Models.DomainModels.ManageYourSelfEntities DB = new DomainModels.ManageYourSelfEntities();
        public ViewModels.MenuVm MenuList()
        {
            ViewModels.MenuVm V = new ViewModels.MenuVm();
            V.MenuLst = DB.Menus.OrderBy(q => q.Order).ToList();
            V.MenuhaLst = DB.Menuhas.OrderBy(q => q.Order).ToList();
            return V;
        }
        public ViewModels.ListNavVM ListNavVM()
        {
            ViewModels.ListNavVM V = new ViewModels.ListNavVM();

            //V.dic_tbl = DB.dic_tbl.OrderByDescending(q => new { q.level, q.eng }).ToList();
            V.MojoodyBank = DB.MojoodyBanks.OrderBy(q => q.Rial).ToList();
            #region ShowKarkadPivot
            //Models.ADO.UIDSConnection U = new ADO.UIDSConnection();
            //DataTable DT = U.Select("exec ShowKarkadPivotNotParam");
            //DataTable reversedDt = new DataTable();
            //reversedDt = DT.Clone();
            //for (var row = DT.Rows.Count - 1; row >= 0; row--)
            //    reversedDt.ImportRow(DT.Rows[row]);

            //V.ShowKarkadPivotNotParamHeader = reversedDt;
            #endregion
            #region ListTask
            var Objects = (from T in DB.Tasks
                               // where (T.IsCheck == false && T.IsActive == true)
                           select new { T.TaskId, T.Name, T.DateStart, T.DateEnd, T.DarsadPishraft, T.IsActive, T.IsCheck }
                    ).AsEnumerable().Select(x => new { x.IsCheck, x.IsActive, x.TaskId, onvan = x.Name, DateStart = x.DateStart.ConvertDateToDateFormat(), DateEnd = x.DateEnd.ConvertDateToDateFormat(), x.DarsadPishraft, RoozGozashteh = int.Parse(Help.Utility.RoozFromDate(x.DateStart)), Rooz = int.Parse(Help.Utility.RoozToDate(x.DateEnd)) }).OrderBy(x => x.Rooz).ToList().OrderByDescending(q => q.DateStart);
            List<ViewModels.TaskVM> lstTaskVM = new List<ViewModels.TaskVM>();
            foreach (var item in Objects)
            {
                ViewModels.TaskVM T = new ViewModels.TaskVM();
                T.DarsadPishraft = item.DarsadPishraft;
                T.DateEnd = item.DateEnd;
                T.DateStart = item.DateStart;
                T.Gozashteh = item.RoozGozashteh;
                T.IsActive = item.IsActive;
                T.IsCheck = item.IsCheck;
                T.MandehRooz = item.Rooz;
                T.Name = item.onvan;
                T.TaskId = item.TaskId;
                lstTaskVM.Add(T);
            }
            V.ListTask = lstTaskVM;
            #endregion
            return V;
        }

        internal bool TaskInsert(Task t)
        {
            bool Result = false;
            DB.Tasks.Add(t);
            if (DB.SaveChanges() > 0)
                Result = true;
            return Result;
        }

        internal Models.DomainModels.Task FindTask(int taskId)
        {

            var res = DB.Tasks.SingleOrDefault(q => q.TaskId == taskId);
            res.DateStart = res.DateStart.ConvertDateToSlash();
            res.DateEnd = res.DateEnd.ConvertDateToSlash();
            return res;
        }
        public bool DicInsert(Models.DomainModels.dic_tbl D)
        {

            bool Result = false;
            /*
            D.level = 10;
            D.SuccessCount = 0;
            D.UnSuccessCount = 0;
            D.timeword = 0;
            D.date_s = Utility.Utility.ConvertDateToSqlFormat(Utility.Utility.shamsi_date());
            D.date_refresh = D.date_s;
            */
            DB.dic_tbl.Add(D);
            if (DB.SaveChanges() > 0)
                Result = true;
            return Result;
        }
        public bool UserRegisterInsert(ViewModels.UserRegisterVm R)
        {
            try
            {
                bool Result = false;
                Models.DomainModels.User U = new DomainModels.User();
                U.UserName = R.UserName;
                U.Password = R.Password;
                DB.Users.Add(U);
                if (DB.SaveChanges() > 0)
                    Result = true;
                return Result;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.ToString());
            }

        }
        public ViewModels.Dictionary.VMDictionary ExampleList(int Wordid)
        {

            //List<ViewModels.Dictionary.VMDictionary> lstWordExa = new List<ViewModels.Dictionary.VMDictionary>();
            ViewModels.Dictionary.VMDictionary NewExa = new ViewModels.Dictionary.VMDictionary();

            var res = from E in DB.example_tbl
                      join W in DB.dic_tbl on E.id_dic_tbl equals W.id
                      where (E.id_dic_tbl == Wordid)
                      select new { WordId = W.id, ExampleId = E.id, Word = W.eng, E.example };
            List<Models.DomainModels.example_tbl> ListExample = new List<example_tbl>();
            foreach (var item in res)
            {
                Models.DomainModels.example_tbl E = new example_tbl();
                E.id = item.ExampleId;
                E.example = item.example;
                ListExample.Add(E);

                // NewExa.ExampleId = item.ExampleId;
                NewExa.id = item.WordId;

                //ListExample.Add(E);
                NewExa.eng = item.Word;


                // lstWordExa.Add(NewExa);
            }
            NewExa.lstExample = ListExample;

            return NewExa;
        }
        public bool WordDelete(int id)
        {
            bool Result = false;
            //FindWord(id);
            DB.dic_tbl.Remove(FindWord(id));
            if (DB.SaveChanges() > 0)
                Result = true;
            return Result;

        }
        public bool ExampleDelete(int ExampleId)
        {
            bool Result = false;
            DB.example_tbl.Remove(FindExample(ExampleId));
            if (DB.SaveChanges() > 0)
                Result = true;
            return Result;
        }
        public Models.DomainModels.dic_tbl FindWord(int id)
        {
            return DB.dic_tbl.FirstOrDefault(q => q.id == id);
        }
        public Models.DomainModels.example_tbl FindExample(int ExampleId)
        {
            return DB.example_tbl.FirstOrDefault(q => q.id == ExampleId);
        }
        //public Models.DomainModels.User User(int id)
        //{
        //    return DB.Users.SingleOrDefault(q => q.UserId == id);
        //}
        public Models.DomainModels.User User(string UserName, string Password)
        {
            return DB.Users.SingleOrDefault(q => q.Password == Password && q.UserName == UserName);
        }
        public List<ViewModels.TaskVM> ListTask(string typeTask, int UserId, List<string> MyData)
        {
            #region ListTask
            /*
       A circular reference was detected while serializing an object of type 'System.Data.Entity.DynamicProxies
       Its because it is trying to load child objects and it may be creating some circular loop that will never ending( a=>b, b=>c, c=>d, d=>a)
       you can turn it off only for that particular moment as following.So dbcontext will not load customers child objects unless Include method is called on your object
       db.Configuration.ProxyCreationEnabled = false;
       User ma = db.user.First(x => x.u_id == id);
       return Json(ma, JsonRequestBehavior.AllowGet);
       */
            DB.Configuration.ProxyCreationEnabled = false;

            string str = string.Empty;
            if (MyData != null)
            {
                str = MyData[0].TrimEnd(',');

                string[] ids = str.Split(',');
            }


            List<ViewModels.TaskVM> lstTaskVM = new List<ViewModels.TaskVM>();
            //-------------------------------------------------------------------

            if (typeTask == "anjamnashode")
            {
                Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
                DataTable DT;
                if (str == "")
                {
                    DT = U.SelectWhere(

                       @"
  select 
DarsadPishraft,
Title,
T.Rate,
T.DateEnd,
DateStart,
IsActive,
IsCheck,
Name,
T.TaskId,
isnull(Olaviat,0) Olaviat,
Label
   from Task T left join
  timing g on T.TaskId=g.TaskId
  left join ManageTime M 
  on g.ManageTimeId=M.ManageTimeId
    left join Cat on T.CatId=Cat.CatId
  where IsCheck=0 and IsActive=1 and T.UserId=@UserId
  order by DateEnd asc,Olaviat asc,isnull(Label,'23:59 - 24:00') asc
", new string[] { "UserId" }, new string[] { UserId.ToString() });
                }
                else
                {
                    DT = U.SelectWhere(

                       @"
  select 
DarsadPishraft,
Title,
T.Rate,
T.DateEnd,
DateStart,
IsActive,
IsCheck,
Name,
T.TaskId,
isnull(Olaviat,0) Olaviat,
Label

   from Task T left join
  timing g on T.TaskId=g.TaskId
  left join ManageTime M 
  on g.ManageTimeId=M.ManageTimeId
    left join Cat on T.CatId=Cat.CatId
  where IsCheck=0 and IsActive=1 and T.UserId=@UserId
 and T.CatId in (" + str + @")
  order by DateEnd asc,Olaviat asc,Value asc

", new string[] { "UserId" }, new string[] { UserId.ToString() });
                }


                foreach (DataRow item in DT.Rows)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.DarsadPishraft = int.Parse(item["DarsadPishraft"].ToString());
                    T.DateEnd = item["DateEnd"].ToString();
                    T.DateStart = item["DateStart"].ToString();
                    T.Gozashteh = int.Parse(Models.Help.Utility.RoozFromDate(T.DateStart));
                    T.IsActive = (bool)item["IsActive"];
                    T.IsCheck = (bool)item["IsCheck"];
                    T.MandehRooz = int.Parse(Models.Help.Utility.RoozToDate(T.DateEnd));// int.Parse(item["MandehRooz"].ToString());
                    T.Name = item["Name"].ToString();
                    T.TaskId = int.Parse(item["TaskId"].ToString());
                    T.Olaviat = int.Parse(item["Olaviat"].ToString());
                    T.Rate = int.Parse(item["Rate"].ToString());
                    T.Label = item["Label"].ToString();
                    T.Title = item["Title"].ToString();
                    lstTaskVM.Add(T);
                }

            }
            //---------------
            /*
            if (typeTask == "anjamnashode")
            {
                var Objects = (from T in DB.Tasks
                               where (T.IsCheck == false && T.IsActive == true) && T.UserId==UserId
                               select new { T.TaskId, T.Name, T.DateStart, T.DateEnd, T.DarsadPishraft, T.IsActive, T.IsCheck ,T.Olaviat} 
                        ).AsEnumerable().Select(x => new {x.Olaviat, x.IsCheck, x.IsActive, x.TaskId, onvan = x.Name, DateStart = x.DateStart.ConvertDateToDateFormat(), DateEnd = x.DateEnd.ConvertDateToDateFormat(), x.DarsadPishraft, RoozGozashteh = int.Parse(Models.Help.Utility.RoozFromDate(x.DateStart)), Rooz = int.Parse(Models.Help.Utility.RoozToDate(x.DateEnd)) }).ToList().OrderBy(q => q.DateEnd).ThenBy(q=>q.Olaviat);

                foreach (var item in Objects)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.DarsadPishraft = item.DarsadPishraft;
                    T.DateEnd = item.DateEnd;
                    T.DateStart = item.DateStart;
                    T.Gozashteh = item.RoozGozashteh;
                    T.IsActive = item.IsActive;
                    T.IsCheck = item.IsCheck;
                    T.MandehRooz = item.Rooz;
                    T.Name = item.onvan;
                    T.TaskId = item.TaskId;
                    T.Olaviat = item.Olaviat;
                    lstTaskVM.Add(T);
                }
            }
            */
            if (typeTask == "anjamshode")
            {
                var Objects = (from T in DB.Tasks
                               where (T.IsCheck == true && T.IsActive == true) && T.UserId == UserId
                               select new { T.TaskId, T.Name, T.DateStart, T.DateEnd, T.DarsadPishraft, T.IsActive, T.IsCheck, T.Olaviat }
                        ).AsEnumerable().Select(x => new { x.Olaviat, x.IsCheck, x.IsActive, x.TaskId, onvan = x.Name, DateStart = x.DateStart.ConvertDateToDateFormat(), DateEnd = x.DateEnd.ConvertDateToDateFormat(), x.DarsadPishraft, RoozGozashteh = int.Parse(Models.Help.Utility.RoozFromDate(x.DateStart)), Rooz = int.Parse(Models.Help.Utility.RoozToDate(x.DateEnd)) }).OrderBy(x => x.Rooz).ToList().OrderByDescending(q => q.DateStart);

                foreach (var item in Objects)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.DarsadPishraft = item.DarsadPishraft;
                    T.DateEnd = item.DateEnd;
                    T.DateStart = item.DateStart;
                    T.Gozashteh = item.RoozGozashteh;
                    T.IsActive = item.IsActive;
                    T.IsCheck = item.IsCheck;
                    T.MandehRooz = item.Rooz;
                    T.Name = item.onvan;
                    T.TaskId = item.TaskId;
                    T.Olaviat = item.Olaviat;
                    lstTaskVM.Add(T);
                }
            }
            if (typeTask == "gheirefal")
            {
                var Objects = (from T in DB.Tasks
                               where (T.IsActive == false) && T.UserId == UserId
                               select new { T.TaskId, T.Name, T.DateStart, T.DateEnd, T.DarsadPishraft, T.IsActive, T.IsCheck, T.Olaviat }
                        ).AsEnumerable().Select(x => new { x.Olaviat, x.IsCheck, x.IsActive, x.TaskId, onvan = x.Name, DateStart = x.DateStart.ConvertDateToDateFormat(), DateEnd = x.DateEnd.ConvertDateToDateFormat(), x.DarsadPishraft, RoozGozashteh = int.Parse(Models.Help.Utility.RoozFromDate(x.DateStart)), Rooz = int.Parse(Models.Help.Utility.RoozToDate(x.DateEnd)) }).OrderBy(x => x.Rooz).ToList().OrderByDescending(q => q.DateStart);

                foreach (var item in Objects)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.DarsadPishraft = item.DarsadPishraft;
                    T.DateEnd = item.DateEnd;
                    T.DateStart = item.DateStart;
                    T.Gozashteh = item.RoozGozashteh;
                    T.IsActive = item.IsActive;
                    T.IsCheck = item.IsCheck;
                    T.MandehRooz = item.Rooz;
                    T.Name = item.onvan;
                    T.TaskId = item.TaskId;
                    T.Olaviat = item.Olaviat;
                    lstTaskVM.Add(T);
                }
            }
            if (typeTask == "koly")
            {
                var Objects = (from T in DB.Tasks
                               where T.UserId == UserId
                               // where (T.IsCheck == false && T.IsActive == true)
                               select new { T.TaskId, T.Name, T.DateStart, T.DateEnd, T.DarsadPishraft, T.IsActive, T.IsCheck, T.Olaviat }
                        ).AsEnumerable().Select(x => new { x.Olaviat, x.IsCheck, x.IsActive, x.TaskId, onvan = x.Name, DateStart = x.DateStart.ConvertDateToDateFormat(), DateEnd = x.DateEnd.ConvertDateToDateFormat(), x.DarsadPishraft, RoozGozashteh = int.Parse(Models.Help.Utility.RoozFromDate(x.DateStart)), Rooz = int.Parse(Models.Help.Utility.RoozToDate(x.DateEnd)) }).OrderBy(x => x.Rooz).ToList().OrderBy(q => q.DateStart);

                foreach (var item in Objects)
                {
                    ViewModels.TaskVM T = new ViewModels.TaskVM();
                    T.DarsadPishraft = item.DarsadPishraft;
                    T.DateEnd = item.DateEnd;
                    T.DateStart = item.DateStart;
                    T.Gozashteh = item.RoozGozashteh;
                    T.IsActive = item.IsActive;
                    T.IsCheck = item.IsCheck;
                    T.MandehRooz = item.Rooz;
                    T.Name = item.onvan;
                    T.TaskId = item.TaskId;
                    T.Olaviat = item.Olaviat;
                    lstTaskVM.Add(T);
                }
            }
            return lstTaskVM;
        }
        public int HasExample(int WordId)
        {
            int Result = 0;
            Result = DB.example_tbl.Count(s => s.id_dic_tbl == WordId);
            return Result;
        }
        public int CountWord()
        {
            int Result = 0;
            Result = DB.dic_tbl.Count();
            return Result;
        }
        #endregion
    }

}
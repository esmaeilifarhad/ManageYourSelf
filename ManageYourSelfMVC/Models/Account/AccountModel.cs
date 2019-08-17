using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Models.Account
{
    public class AccountModel
    {
        Models.DomainModels.ManageYourSelfEntities DB = new DomainModels.ManageYourSelfEntities();

        //private List<Account> listAccount = new List<Account>();
        //public AccountModel()
        //{
        //    listAccount.Add(new Account { Username = "farhad", Password = "23565", Roles = new string[] { "superadmin", "admin", "employee","Karbari" } });
        //    listAccount.Add(new Account { Username = "acc2", Password = "123", Roles = new string[] { "admin", "employee" } });
        //    listAccount.Add(new Account { Username = "acc3", Password = "123", Roles = new string[] { "employee" } });
        //}
        public ViewModels.UserVM find(string username)
        {
            ViewModels.UserVM V = new ViewModels.UserVM();
            Models.DomainModels.User U= DB.Users.SingleOrDefault(q => q.UserName.Trim()==username.Trim());
            if (U == null)
                return null;
            var ListRoles = (from US in DB.Users join UR in DB.UserRoles on US.UserId equals UR.UserId
                            join RO in DB.Roles on UR.RoleId equals RO.RoleId
                            where US.UserId==U.UserId
                            select new { RO.RoleName,RO.IsActive,RO.RoleId }).ToList();

            List<Models.DomainModels.Role> LstRoles = new List<DomainModels.Role>();
            foreach (var item in ListRoles)
            {
                Models.DomainModels.Role R = new DomainModels.Role();
                R.IsActive = item.IsActive;
                R.RoleId = item.RoleId;
                R.RoleName = item.RoleName;
                LstRoles.Add(R);
            }
        
            V.UserName = U.UserName;
            V.Password = U.Password;
            V.Roles = LstRoles;
            return V;
        }
        public ViewModels.UserVM login(string username, string password)
        {
            ViewModels.UserVM V = new ViewModels.UserVM();
            Models.DomainModels.User U = DB.Users.SingleOrDefault(q => (q.UserName.Trim() == username.Trim()) &&(q.Password.Trim()==password.Trim()));
            if (U == null)
                return null;
            V.UserName = U.UserName;
            V.Password = U.Password;


            HttpContext.Current.Session["_NameUser"] = U.Name;
            HttpContext.Current.Session["UserId"] = U.UserId;

            return V;
        }
    }
}
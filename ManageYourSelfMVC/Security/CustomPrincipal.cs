using ManageYourSelfMVC.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ManageYourSelfMVC.Security
{
    public class CustomPrincipal:IPrincipal
    {
        private ViewModels.UserVM Account;
        //  private AccountModel am = new AccountModel();
        public CustomPrincipal(ViewModels.UserVM account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity(account.UserName);
        }
        public IIdentity Identity
        {
            get;
            set;
            // get { throw new NotImplementedException(); }
        }
        public bool IsInRole(string role)
        {
            bool Result = false;
            var roles = role.Split(new char[] { ',' });
            var RolesUser = this.Account.Roles;
            // return true;
            // return role.Any(r =>this.Account.Roles.Contains(r));
            /*
             Role مربوط به فرم لاگین
             role مروبط به صفحه درخواستی
             */
            foreach (var item in roles)
            {
                foreach (var item2 in RolesUser)
                {
                    if (item == item2.RoleName)
                    {
                        Result = true;
                    }
                }
            }
            return Result;
        }
    }
}
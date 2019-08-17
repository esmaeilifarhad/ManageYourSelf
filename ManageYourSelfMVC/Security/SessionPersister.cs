using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Security
{
    public class SessionPersister
    {
      
        static string usernameSessionvar = "username";
        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session.Timeout = 2400;
                HttpContext.Current.Session[usernameSessionvar] = value;
              
                //در اینجا میتوانیم کد کاربری را پیدا کرده و در سشن پر نماییم و هر کاربر دیتای خودش را مشاهده نماید
                // HttpContext.Current.Session[UserId] = value;
            }
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class UserRegisterVm
    {
        private string _UserName;
        private string _Password;
        public string UserName {
            set
            {
                _UserName = value;
            }
            get
            {
                return _UserName;
            }
        }
        public string Password {
            set
            {
                if (int.Parse(value) >= 1000 && int.Parse(value) <= 9999)
                    _Password = value;
                else
                {
                    throw new ArgumentException("پسورد باید چهار رقمی باشد");
                }
            }
            get
            {
                return _Password;
            }
        }
    }
}
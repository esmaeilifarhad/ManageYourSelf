using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class UserVM:Models.DomainModels.User
    {
        public Guid IdUser { get; set; }
        public List<Models.DomainModels.Role> Roles { get; set; }
    }
}
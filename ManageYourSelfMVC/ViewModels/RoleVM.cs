using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class RoleVM:Models.DomainModels.Role
    {
        public bool HasRole { get; set; }
        public int UserId { get; set; }
    }
}
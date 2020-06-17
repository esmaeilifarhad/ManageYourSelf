using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class RegisterController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Register(Models.DomainModels.User U)
        {
            bool result = false;
            var User = DB.Users.SingleOrDefault(q => q.UserName.Trim() == U.UserName.Trim());
            if (User != null)
            {
                return Json("این کد کاربری قبلا تعریف شده است", JsonRequestBehavior.AllowGet);
            }
            var UserPhone = DB.Users.SingleOrDefault(q => q.PhoneNumber.Trim() == U.PhoneNumber.Trim());
            if (UserPhone != null)
            {
                return Json("این شماره تلفن قبلا ثبت شده است", JsonRequestBehavior.AllowGet);
            }
            DB.Users.Add(U);
            if (DB.SaveChanges() > 0)
            {
                //Models.DomainModels.UserRole UR = new Models.DomainModels.UserRole();
                //UR.RoleId = 3;
                //UR.UserId = U.UserId;
                //DB.UserRoles.Add(UR);
                //DB.SaveChanges();
                return Json("با موفقیت ثبت شد", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("خطا در ثبت", JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ListRegister()
        {
            var lstUsers = DB.Users.ToList();
            return PartialView(lstUsers);

        }
        public ActionResult DeleteRegister(int UserId)
        {
            bool result = false;
            Models.DomainModels.User OldUser = DB.Users.SingleOrDefault(q => q.UserId == UserId);
            DB.Users.Remove(OldUser);
            if (DB.SaveChanges() > 0)
                result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserRole(int UserId)
        {
            List<ViewModels.RoleVM> lstV = new List<ViewModels.RoleVM>();
            var ListRoles = DB.Roles.ToList();
            var ListUserRole = DB.UserRoles.Where(q => q.UserId == UserId).ToList();

            bool HasRole = false;
            foreach (var itemRole in ListRoles)
            {
                ViewModels.RoleVM V = new ViewModels.RoleVM();
                V.RoleName = itemRole.RoleName;
                V.IsActive = itemRole.IsActive;
                V.RoleId = itemRole.RoleId;
                V.UserId = UserId;
                if (ListUserRole != null)
                {
                    foreach (var itemUserRole in ListUserRole)
                    {
                        if (itemUserRole.RoleId == itemRole.RoleId)
                        {
                            HasRole = true;
                        }
                    }
                }
                V.HasRole = HasRole;
                lstV.Add(V);
                HasRole = false;
            }
            return PartialView(lstV);
        }
        public ActionResult TakhsisUserRole(int UserId,string CollectionRoleId)
        {
            CollectionRoleId= CollectionRoleId.TrimEnd(',');
            var RoleId= CollectionRoleId.Split(',');
            DB.UserRoles.RemoveRange(DB.UserRoles.Where(q => q.UserId == UserId));
            DB.SaveChanges();
            if (CollectionRoleId != "")
            {
                foreach (var item in RoleId)
                {
                    Models.DomainModels.UserRole UR = new Models.DomainModels.UserRole();
                    UR.RoleId = int.Parse(item);
                    UR.UserId = UserId;
                    DB.UserRoles.Add(UR);
                    DB.SaveChanges();
                }
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}
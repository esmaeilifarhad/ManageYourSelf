using ManageYourSelfMVC.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ManageYourSelfMVC.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult MVCHomeHeaderThreeList()
        {
         
            Models.Repository.MVCHomeHeaderThreeRepository Rep = new Models.Repository.MVCHomeHeaderThreeRepository();
            List<Models.DomainModels.MVCHomeHeaderThree> M = new List<Models.DomainModels.MVCHomeHeaderThree>();
            M = Rep.Select().ToList();
            return View("MVCHomeHeaderThreeList", M);
        }
        public ActionResult SliderPhoto()
        {
          
            Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            List<Models.DomainModels.SliderPhoto> SP= DB.SliderPhotoes.Select(q => q).ToList();
           // Models.DomainModels.SliderPhoto S = new Models.DomainModels.SliderPhoto();
            return View(SP);
        }
        public PartialViewResult Menu()
        {
            //ManageYourSelfMVC.Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            Models.MyData.MyDataTransfer Menulst = new Models.MyData.MyDataTransfer();
            return PartialView("MyMenu",Menulst.MenuList());
        }
        [HttpGet]
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UserRegister(ViewModels.UserRegisterVm V)
        {
            //ViewModels.UserRegisterVm U = new ViewModels.UserRegisterVm();
            //U.UserName = InpUser;
            //U.Password = InpPass;
            ViewBag.Farhad = "Farhad";
            if (ModelState.IsValid == true)
            {
                Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
                if (T.UserRegisterInsert(V))
                {
                    TempData["Message"] = "ثبت به درستی صورت گرفت";
                    TempData["Color"] = "Green";
                }
                else
                {
                    TempData["Message"] = "خطایی در ثبت وجود دارد";
                    TempData["Color"] = "Red";
                }
            }
            else
            {
                TempData["Message"] = "مقادیر را به درستی وارد نمایید";
                TempData["Color"] = "Red";
            }
            //return RedirectToAction("UserRegister", "Admin");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Login(ViewModels.UserRegisterVm V)
        {
            //ViewModels.UserRegisterVm U = new ViewModels.UserRegisterVm();
            //U.UserName = InpUser;
            //U.Password = InpPass;
            Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
            Models.DomainModels.User U = T.User(V.UserName, V.Password);
            if (ModelState.IsValid == true)
            {
              
                if (U!=null)
                {
                   
                    Session["UserId"] =   U.UserId;
                    Session["UserName"] = U.UserName;
                    Session["Password"] = U.Password;
                    TempData["Message"] = "خوش آمدید";
                    TempData["Color"] = "Green";
                }
                else
                {
                    TempData["Message"] = "کاربر وجود ندارد";
                    TempData["Color"] = "Red";
                }
            }
            else
            {
                TempData["Message"] = "مقادیر را به درستی وارد نمایید";
                TempData["Color"] = "Red";
            }
            //return RedirectToAction("UserRegister", "Admin");
            return Json(U,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SliderPhoto_Create(Models.DomainModels.SliderPhoto SP, HttpPostedFileBase imagee)
        {
            //Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            Models.DomainModels.SliderPhoto S = new Models.DomainModels.SliderPhoto();
            S.Header = SP.Header;
            S.Matn = SP.Matn;
            //--------------------------------Save To Folder
            string Address = "~/MyPhoto/Slider";
            string Namee = System.IO.Path.GetFileName(imagee.FileName);
            string Name = Namee.Substring(0, Namee.LastIndexOf("."));

            string path = System.IO.Path.Combine(Server.MapPath(Address), Namee);
            // file is uploaded
            imagee.SaveAs(path);
            string t = @"../../MyPhoto/Slider/"+ Namee;
            S.URL = t;
            //------------------------------

            //Save To Databse
            if (imagee != null && imagee.ContentLength > 0)
            {

                S.PhotoImg = new byte[imagee.ContentLength];
                imagee.InputStream.Read(S.PhotoImg, 0, imagee.ContentLength);
                using (var context = new Models.DomainModels.ManageYourSelfEntities())
                {
                    context.SliderPhotoes.Add(S);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("SliderPhoto", "Admin");
        }
        [HttpGet]
        public ActionResult SliderPhoto_Create()
        {
            //Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
           
            return View();
        }
        [HttpPost]
        public ActionResult DicInsert(Models.DomainModels.dic_tbl D)
        {
            bool Result = false;
            Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
            Result=T.DicInsert(D);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
       // [Models.Filtering.Filter]
        //public PartialViewResult ExampleList(int id)
        //{
        //    Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        //    var res= T.ExampleList(id);
        //    return PartialView("ExampleList", res);
        //}
        [HttpGet]
        //public JsonResult ExampleList(int id)
        //{
        //    Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        //    List<ViewModels.VMExapleWord> lstExample = T.ExampleList(id);
        //    //List<Models.DomainModels.example_tbl> lstExample = T.ExampleList(id);
        //    //string json = JsonConvert.SerializeObject(lstExample, Formatting.None);
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(lstExample);

        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}
        
        public ActionResult WordDelete(int id)
        {
            Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
            bool Result= T.WordDelete(id);
            if (Result == true)
            {
                TempData["Message"] = "حذف با موفقیت انجام شد";
            }
            return RedirectToAction("ListNav","Client");
            //return View("Client/ListNav", Result);
        }
        [CustomAthorize(Roles = "SuperAdmin")]
        public ActionResult AdminPage()
        {
            return View();
        }
        public ActionResult ExampleDelete(int ExampleId)
        {
            Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
            bool Result = false;
            Result = T.ExampleDelete(ExampleId);
            if (Result == true)
            {
                TempData["Message"] = "حذف با موفقیت انجام شد";
            }
           // return RedirectToAction("ListNav", "Client");
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class HazineMohemController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        int UserId = Models.staticClass.staticClass.UserId;// (int)System.Web.HttpContext.Current.Session["UserId"];
        // GET: HazineMohem

        public ActionResult ListKharj()
        {
            var res = (from K in DB.Kharjs
                       join
                       KT in DB.KharjTypes
                       on K.KharjTypeId equals KT.KharjTypeId
                       where KT.UserId== UserId
                       select new {K.KharjId, K.Name, K.Rial, K.DateKharj, K.Description, TName = KT.Name });
            List<ViewModels.HazineMohem.VMHazineMohem> lstVMHazineMohemlst = new List<ViewModels.HazineMohem.VMHazineMohem>();
            foreach (var item in res)
            {
                ViewModels.HazineMohem.VMHazineMohem V = new ViewModels.HazineMohem.VMHazineMohem();

                V.DateKharj = item.DateKharj;
                V.Description = item.Description;
                V.KharjId = item.KharjId;
                V.KharjTypeName = item.TName;
                V.Name = item.Name;
                V.Rial = item.Rial;

                lstVMHazineMohemlst.Add(V);               
            }
            return PartialView(lstVMHazineMohemlst);
        }
        public ActionResult ListKharjType() {
            var res= DB.KharjTypes.Where(q=>q.UserId==UserId).ToList();
            return PartialView(res);
        }
    }
}
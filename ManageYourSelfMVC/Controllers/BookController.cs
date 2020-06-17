using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    [Models.Filtering.Filter]
    public class BookController : Controller
    {
        Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
        Models.MyData.MyDataTransfer T = new Models.MyData.MyDataTransfer();
        Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
        int UserId = Models.staticClass.staticClass.UserId;// 0;
        // GET: Book
        public ActionResult GetBook()
        {
            /*
            Random rand = new Random();
            int toSkip = rand.Next(0, DB.Books.Count());

           //var book= DB.Books.Skip(toSkip).Take(1).First();

            var book = DB.Books.Where(q=>q.UserId==UserId).OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1).FirstOrDefault();
            ViewModels.Book.VMBook vmB = new ViewModels.Book.VMBook();
            vmB.dsc = book.dsc;
            vmB.BookId = book.BookId;
            return Json(vmB, JsonRequestBehavior.AllowGet);
            */
            DataTable DT = U.Select(@"exec findBookDsc "+ UserId +" ");
            ViewModels.Book.VMBook vmB = new ViewModels.Book.VMBook();
            foreach (DataRow item in DT.Rows)
            {
                Models.DomainModels.Cat C = new Models.DomainModels.Cat();
                vmB.dsc = item["dsc"].ToString();
                vmB.BookId =int.Parse(item["BookId"].ToString());
                if (item["RepeatedNumber"].ToString() == "")
                {
                    vmB.RepeatedNumber = 0;
                }
                else
                {
                    vmB.RepeatedNumber = int.Parse(item["RepeatedNumber"].ToString());
                }
                vmB.date= item["date"].ToString();
                vmB.time= item["time"].ToString();
            }
            return Json(vmB, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateBook(string dsc,string time,string date)
        {
            Models.DomainModels.Book B = new Models.DomainModels.Book();
            B.dsc = dsc;
            B.UserId = UserId;
            B.time = time;
            B.date = date;
            DB.Books.Add(B);
            DB.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteBook(int BookId)
        {
           var oldBook= DB.Books.SingleOrDefault(q => q.BookId == BookId);
            DB.Books.Remove(oldBook);
            DB.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditBook(int BookId) {
            ViewModels.Book.VMBook B = new ViewModels.Book.VMBook();
            var oldBook = DB.Books.SingleOrDefault(q => q.BookId == BookId);
            B.date = oldBook.date;
            B.dsc = oldBook.dsc;
            B.RepeatCount = oldBook.RepeatCount;
            B.RepeatedNumber = oldBook.RepeatedNumber;
            B.time = oldBook.time;
            B.BookId = oldBook.BookId;
         


            return Json(B, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateBook(int BookId, string dsc)
        {
            var oldBook = DB.Books.SingleOrDefault(q => q.BookId == BookId);
            oldBook.dsc = dsc;
            DB.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
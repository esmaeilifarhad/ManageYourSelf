using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageYourSelfMVC.Models.DomainModels;

namespace ManageYourSelfMVC.Controllers
{
    public class LogTBLsController : Controller
    {
        private ManageYourSelfEntities db = new ManageYourSelfEntities();

        // GET: LogTBLs
        public ActionResult Index()
        {
            return View(db.LogTBLs.ToList());
        }

        // GET: LogTBLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogTBL logTBL = db.LogTBLs.Find(id);
            if (logTBL == null)
            {
                return HttpNotFound();
            }
            return View(logTBL);
        }

        // GET: LogTBLs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogTBLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,Name,dsc,date,Time")] LogTBL logTBL)
        {
            if (ModelState.IsValid)
            {
                db.LogTBLs.Add(logTBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logTBL);
        }

        // GET: LogTBLs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogTBL logTBL = db.LogTBLs.Find(id);
            if (logTBL == null)
            {
                return HttpNotFound();
            }
            return View(logTBL);
        }

        // POST: LogTBLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,Name,dsc,date,Time")] LogTBL logTBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logTBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logTBL);
        }

        // GET: LogTBLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogTBL logTBL = db.LogTBLs.Find(id);
            if (logTBL == null)
            {
                return HttpNotFound();
            }
            return View(logTBL);
        }

        // POST: LogTBLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogTBL logTBL = db.LogTBLs.Find(id);
            db.LogTBLs.Remove(logTBL);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

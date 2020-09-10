using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Quartz;

namespace ManageYourSelfMVC.MyJobs
{
    public class FirstJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Models.WebServices.Services s = new Models.WebServices.Services();
            s.ExchangeRate();

            Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            Models.DomainModels.LogTBL L = new Models.DomainModels.LogTBL();
            L.dsc = "RepeatJob : FirstJob";
            L.date =ManageYourSelfMVC.Utility.Utility.ConvertDateToSqlFormat(ManageYourSelfMVC.Utility.Utility.shamsi_date());
            L.Time = DateTime.Now.ToShortTimeString();
            L.Name = "اجرای جاب";
            DB.LogTBLs.Add(L);
            DB.SaveChanges();

      
            Utility.GMailer G = new Utility.GMailer();
            Utility.GMailer.GmailUsername = "FeMyHostSender@gmail.com";
            Utility.GMailer.GmailPassword = "861130928";
            G.Body = "سلام";
            G.Subject ="eyval";
            G.ToEmail ="esmaili.farhad67@gmail.com";
            
            G.Send();

        }
    }
}
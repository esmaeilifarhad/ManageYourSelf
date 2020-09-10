using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ManageYourSelfMVC.Utility
{
    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            try
            {


               // Utility.CreateLog(new Exception { }, "1" + "قبل از ایمیل", " public class GMailer   public void Send()");
                SmtpClient smtp = new SmtpClient();
                smtp.Host = GmailHost;
                smtp.Port = GmailPort;
                smtp.EnableSsl = GmailSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);


                using (var message = new MailMessage(GmailUsername, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    Utility.CreateLog(new Exception { }, "2" + "قبل از ایمیل", " public class GMailer   public void Send()");
                    smtp.Send(message);
                    Utility.CreateLog(new Exception { }, "3" + "بعد از ایمیل", " public class GMailer   public void Send()");
                }
                Utility.CreateLog(new Exception { }, "پایان", " public class GMailer   public void Send()");
            }
            catch (Exception ex)
            {
                Utility.CreateLog(new Exception { }, "خطا : " + ex.ToString(), " public class GMailer   public void Send()");
            }
        }
    }
}
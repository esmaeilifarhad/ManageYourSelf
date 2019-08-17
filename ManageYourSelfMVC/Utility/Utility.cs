using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System.Speech.Synthesis;

namespace ManageYourSelfMVC.Utility
{
    public static class Utility
    {
       
        //public static void SpeakEnglish(string str)
        //{

        //    SpeechSynthesizer reader; //declare the object 
        //    reader = new SpeechSynthesizer(); //create new object 
        //    reader.Dispose();
            

        //        reader = new SpeechSynthesizer();
        //        reader.SpeakAsync(str);
        //        reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
            

        //}
        //event handler 
       static void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
           
        }
        public static void Write_ReadFile(string Message)
        {
            string sss= HttpContext.Current.Server.MapPath("~");

            bool exists = System.IO.Directory.Exists(sss + "Log");
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(sss+ "Log");
            }
            string pathRoot = (sss+ "Log");
            string PathFile = "LogInfo.txt";
            string Path = System.IO.Path.Combine(pathRoot, PathFile);

            File.AppendAllText(Path,shamsi_date()+" _ "+DateTime.Now.ToString() +" : "+ Message+ Environment.NewLine);

            //using (StreamWriter sw = File.CreateText(Path))
            //{
            //    sw.WriteLine("esmaili.farhad67@gmail.com");
            //}

            string ss;
            using (StreamReader sr = File.OpenText(Path))
            {
                ss = sr.ReadLine();
            }
        }
        public static byte[] Serialize(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static byte[] Serializee(this Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object DeSerialize(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        public static string shamsi_date()
        {
            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();
            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(DateTime.Now);
            m = g.GetMonth(DateTime.Now);
            y = g.GetYear(DateTime.Now);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            rd = yy + "/" + mm + "/" + dd;
            return rd;
           // return rd.Remove(0, 2);
        }
        public static string shamsi_dateTomarrow()
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);
            var yesterday = today.AddDays(-1);

            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();
            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(tomorrow);
            m = g.GetMonth(tomorrow);
            y = g.GetYear(tomorrow);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            rd = yy + "/" + mm + "/" + dd;
            return rd.Remove(0, 2);
        }
        //public static string ChangeDateToSqlFormat(this string DateSlash)
        //{
        //    // String strPara = "Hello (1) Date is (15 May 2011)";
        //    string strNew = DateSlash.Replace("/", string.Empty);
        //    //  strNew=strNew.Remove(0, 2);
        //    return strNew;
        //}
        public static string ConvertDateToSqlFormat(this string DateSlash)
        {
            string strNew = DateSlash;
            if (DateSlash.Length == 8)
            {
                 strNew = DateSlash.Replace("/", string.Empty);
            }
            if (DateSlash.Length == 10)
            {
                strNew = DateSlash.Replace("/", string.Empty);
                /*string Year = strNew.Substring(2, 2);
                string Month = strNew.Substring(4, 2);
                string Day = strNew.Substring(6, 2);
                strNew= Year+ Month+ Day;*/
            }
            return strNew;
        }
        public static string ConvertDateToDateFormat(this string DateSlash)
        {
            string strNew = string.Empty;
            if (DateSlash.Length == 8)
            {
                string Year = DateSlash.Substring(0, 2);
                string Month = DateSlash.Substring(2, 2);
                string Day = DateSlash.Substring(04, 2);
                 strNew = Year + '/' + Month + '/' + Day;
               
            }
            if (DateSlash.Length == 10)
            {
                string Year = DateSlash.Substring(0, 4);
                string Month = DateSlash.Substring(4, 2);
                string Day = DateSlash.Substring(6, 2);
                 strNew = Year + '/' + Month + '/' + Day;
            }
            return strNew;


        }
        public static string ConvertDateToSlash(this string str)
        {
            string Result = str;
            if (str.Length == 6)
            {              
                string Yaer = str.Substring(0, 2);
                string Moth = str.Substring(2, 2);
                string Dayy = str.Substring(4, 2);
                Result = (string.Format("{0}/{1}/{2}", Yaer, Moth, Dayy));
            }
            if (str.Length == 8)
            {
                string Yaer = str.Substring(0, 4);
                string Moth = str.Substring(4, 2);
                string Dayy = str.Substring(6, 2);
                Result = (string.Format("{0}/{1}/{2}", Yaer, Moth, Dayy));
            }
            return Result;
        }
        /// <summary>
        /// Farsy
        /// </summary>
        //public static void farsyBenevis()
        //{
        //    //فارسی
        //    System.Globalization.CultureInfo language = new System.Globalization.CultureInfo("fa-ir");
        //    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(language);
        //}
        /// <summary>
        /// English
        /// </summary>
        //public static void EnglishBenevis()
        //{
        //    //انگلیسی
        //    System.Globalization.CultureInfo language = new System.Globalization.CultureInfo("en-us");
        //    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(language);
        //}
        /// <summary>
        /// تبدیل به ریال
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Rial(string str)
        {
            string Rial = string.Empty;
            // int amount = 2000000;
            // Rial = str.ToString("#,#");



            Rial = string.Format("{0:N0}", double.Parse(str.Replace(",", "")));
               // textbox.Select(textbox.TextLength, 0);
            


            /*
            if (str != "")
            {
                Int64 number = Int64.Parse(str);
                List<string> lst = new List<string>();
                long j;
                int k = 0;

                while (number > 0)
                {
                    k = k + 1;
                    j = number % 10;
                    number /= 10;
                    if (k % 3 == 0)
                    {
                        lst.Add("," + j.ToString());
                    }
                    else
                    {
                        lst.Add(j.ToString());
                    }
                }
                lst.Reverse();

                foreach (var item in lst)
                {
                    Rial += item.ToString();
                }
                if (str.Length % 3 == 0)
                {
                    Rial = Rial.Remove(0, 1);
                }
            }
            */
            return Rial;
        }

        public static string RemoveComma(string str)
        {
            // String strPara = "Hello (1) Date is (15 May 2011)";
            string strNew = str.Replace(",", string.Empty);
            //  strNew=strNew.Remove(0, 2);
            return strNew;
        }
        public static string ConvertToSecond(this string str)
        {
            string Result = string.Empty;
            int Hour = int.Parse(str.Substring(0, 2));
            int Minute = int.Parse(str.Substring(3, 2));
            Result = ((Hour * 60 * 60) + (Minute * 60)).ToString();
            return Result;
        }
        public static string ConvertTotime(this string str)
        {
            string Result = string.Empty;
            string Hour = (int.Parse(str) / 3600).ToString();
            if (Hour.Length == 1)
            {
                Hour = "0" + Hour;
            }
            //if ((int.Parse(Hour) / 10) < 1)
            //{

            //}
            string Minute = ((int.Parse(str) % 3600) / 60).ToString();
            if (Minute.Length == 1)
            {
                Minute = "0" + Minute;
            }

            Result = (string.Format("{0}:{1}", Hour, Minute));
            return Result;
        }
        public static DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
        {
            //We really want to know if there is any data at all
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            /*Okay, we have some data. Time to work.*/

            //So dear record, what do you have?
            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            //Our table should have the columns to support the properties
            DataTable table = new DataTable();

            //Add, add, add the columns
            foreach (var info in infos)
            {

                Type propType = info.PropertyType;

                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Nullable types should be handled too
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            //Hmm... we are done with the columns. Let's begin with rows now.
            DataRow row;

            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }

                table.Rows.Add(row);
            }

            //Table is ready to serve.
            table.AcceptChanges();

            return table;
        }
        public static void SendMail(string Messagee = "No Text", string Attachment = null,string _Subject="بدون موضوع")
        {
            try
            {
                Utility.CreateLog("Start SendMail", "Utility");
                //bool exists = System.IO.Directory.Exists(Application.StartupPath + "\\Email");
                //if (!exists)
                //{
                //    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Email");
                //}
                //string pathRoot = Application.StartupPath + "\\Email";
                //string PathFile = "EmailInfo.txt";
                //string Path = System.IO.Path.Combine(pathRoot, PathFile);
                //using (StreamWriter sw = File.CreateText(Path))
                //{
                //    sw.WriteLine("esmaili.farhad67@gmail.com");
                //}

                //string ss;
                //using (StreamReader sr = File.OpenText(Path))
                //{
                //    ss = sr.ReadLine();
                //}
                MailMessage message = new MailMessage();
                message.From = new MailAddress("FeMyHostSender@gmail.com");
                message.Subject = _Subject + "  " + Utility.shamsi_date();// + " - " + DateTime.Now;
                message.Body = Messagee;
                message.IsBodyHtml = true;
                // string[] emails = ss.Split(';');

                // for (int i = 0; i < emails.Length; i++)
                //{
               
                string Emails = "Esmaeili.Farhad@Golrang.com,esmaili.farhad67@gmail.com";
                string[] emails = Emails.Split(',');

                for (int i = 0; i < emails.Length; i++)
                {
                    message.To.Add(new MailAddress(emails[i]));
                }


                //message.To.Add(new MailAddress(emails));
                // }
                //Attach start
                //if (Attachment != null || Attachment != "")
                //{
                //    System.Net.Mail.Attachment attachment;
                //    attachment = new System.Net.Mail.Attachment(Attachment);
                //    message.Attachments.Add(attachment);
                //}

                /*
                if (attachmentFilename != null)
                {
                    Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                    disposition.FileName =Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    message.Attachments.Add(attachment);
                }
                */
                //attach ned

                SmtpClient client = new SmtpClient();
                // client.UseDefaultCredentials = true;
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("FeMyHostSender@gmail.com", "861130928");
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Send(message);
                Utility.CreateLog("Finish SendMail", "Utility");
            }
            catch (Exception ex)
            {
                Utility.CreateLog(ex.Message+"SendMail", "Utility");
                throw new ArgumentException(ex.ToString());
            }


        }
        public static void SendMailMethod(string _NameUser)
        {
            try
            {

                // string _NameUser = System.Web.HttpContext.Current.Session["_NameUser"].ToString();
                string MyMessage = string.Empty;
                MyMessage = _NameUser + " در ساعت " + DateTime.Now.ToString("hh:mm:ss") + " وارد بخش فوتبال شد ";

                SendMail_Base(_NameUser);

            }
            catch (Exception ex)
            {
                Utility.CreateLog(ex.ToString(), "public static void SendMailMethod(string _NameUser)");
            }
        }
        public static void SendMail_Base(string UserName)
        {

            GMailer.GmailUsername = "FeMyHostSender@gmail.com";
            GMailer.GmailPassword = "861130928";

            GMailer mailer = new GMailer();
            //mailer.ToEmail = "esmaili.farhad67@gmail.com";
            mailer.ToEmail = "Esmaeili.Farhad@Golrang.com";
            mailer.Subject = Utility.shamsi_date()+ "   :  "+"SendMail_Base";
            mailer.Body = UserName + " در ساعت " + DateTime.Now.ToString("hh:mm:ss") + " وارد بخش sss شد ";
            mailer.IsHtml = true;
            mailer.Send();
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string RoozToDate(string OldDate)
        {
            string Rooz = string.Empty;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            string DatePersian = Utility.ConvertDateToSqlFormat(Utility.shamsi_date());
            string DateRefresh = Utility.ConvertDateToSqlFormat(OldDate);
            //-------------------------------------------------------------------------------------
            int year_new = 1300 + int.Parse(DatePersian.Substring(0, 2));
            int month_new = int.Parse(DatePersian.Substring(2, 2));
            int day_new = int.Parse(DatePersian.Substring(4, 2));

            int year_old = 1300 + int.Parse(DateRefresh.Substring(0, 2));
            int month_old = int.Parse(DateRefresh.Substring(2, 2));
            int day_old = int.Parse(DateRefresh.Substring(4, 2));
            //-----------------------------------------------------------------------------------

            DateTime dt1 = calendar.ToDateTime(year_old, month_old, day_old, 0, 0, 0, 0);
            DateTime dt2 = calendar.ToDateTime(year_new, month_new, day_new, 0, 0, 0, 0);
            TimeSpan ts = dt1.Subtract(dt2);
            int days = ts.Days;
            //if (days < 0)
            //{
            //    days = days * (-1);
            //}
            //else
            //{
            //    days = 0;
            //}
            Rooz = days.ToString();

            return Rooz;
        }
        public static string RoozFromDate(string OldDate)
        {
            string Rooz = string.Empty;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            string DatePersian = Utility.ConvertDateToSqlFormat(Utility.shamsi_date());
            string DateRefresh = Utility.ConvertDateToSqlFormat(OldDate);
            //-------------------------------------------------------------------------------------
            int year_new = 1300 + int.Parse(DatePersian.Substring(0, 2));
            int month_new = int.Parse(DatePersian.Substring(2, 2));
            int day_new = int.Parse(DatePersian.Substring(4, 2));

            int year_old = 1300 + int.Parse(DateRefresh.Substring(0, 2));
            int month_old = int.Parse(DateRefresh.Substring(2, 2));
            int day_old = int.Parse(DateRefresh.Substring(4, 2));
            //-----------------------------------------------------------------------------------

            DateTime dt1 = calendar.ToDateTime(year_old, month_old, day_old, 0, 0, 0, 0);
            DateTime dt2 = calendar.ToDateTime(year_new, month_new, day_new, 0, 0, 0, 0);
            TimeSpan ts = dt2.Subtract(dt1);
            int days = ts.Days;
            //if (days < 0)
            //{
            //    days = days * (-1);
            //}
            //else
            //{
            //    days = 0;
            //}
            Rooz = days.ToString();

            return Rooz;
        }
        public static void CreateLog(string MatnKhata,string Name)
        {
            Models.DomainModels.ManageYourSelfEntities DB = new Models.DomainModels.ManageYourSelfEntities();
            Models.DomainModels.LogTBL L = new Models.DomainModels.LogTBL();
            L.dsc = MatnKhata;
            L.date = Utility.shamsi_date();
            L.Time = DateTime.Now.ToShortTimeString();
            L.Name = Name;
            DB.LogTBLs.Add(L);
            DB.SaveChanges();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;




public static class Utility
{
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
        string strNew = string.Empty;
         if (DateSlash == null)
        {
            return null;
        }
        else if (DateSlash.Length == 10)
        {
            strNew = DateSlash.Replace("/", string.Empty);
          //  strNew = strNew.Substring(2, 6);
        }
        
        else
        {
            strNew = DateSlash.Replace("/", string.Empty);
        }
        return strNew;
    }

    public static string ConvertDateToDateFormat(this string DateSlash)
    {
        if (DateSlash.Length == 8)
        {
            string Year = DateSlash.Substring(0, 4);
            string Month = DateSlash.Substring(4, 2);
            string Day = DateSlash.Substring(6, 2);
            string strNew = Year + '/' + Month + '/' + Day;
            return strNew;
        }
        else
        {
            return null;
        }
    }
    public static string ConvertDateToSlash(this string str)
    {
        try
        {
            if (str.Length != 8)
            {
                return "";
            }
            string Result = string.Empty;
            string Yaer = str.Substring(0, 4);
            string Moth = str.Substring(4, 2);
            string Dayy = str.Substring(6, 2);
            Result = (string.Format("{0}/{1}/{2}", Yaer, Moth, Dayy));
            return Result;
        }
        catch (Exception ex )
        {

            throw new ArgumentException(ex.Message);
        }
       
    }

    public static string Rial(string str)
    {
        if (str != "")
            return string.Format("{0:N0}", double.Parse(str.Replace(",", "")));
        else return "";
       // str.Select(str);

        /*
        string MosbatORManfy=str.Substring(0, 1);
        if (str == "-50000")
        {
            var x = 5;
        }
        str = str.Replace('-','+');
        string Rial = string.Empty;
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
        if(MosbatORManfy=="-")
        return Rial+ "-";
        return Rial;*/
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
    public static void SendMail(string Messagee = "No Text", string Attachment = null)
    {
        try
        {
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
            message.Subject = "Task" + "  " + Utility.shamsi_date();// + " - " + DateTime.Now;
            message.Body = Messagee;
            message.IsBodyHtml = true;
            // string[] emails = ss.Split(';');

            // for (int i = 0; i < emails.Length; i++)
            //{
            message.To.Add(new MailAddress("Esmaeili.Farhad@Golrang.com"));
            // }
            //Attach start
            if (Attachment != null)
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(Attachment);
                message.Attachments.Add(attachment);
            }

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
        }
        catch (Exception ex)
        {

            throw new ArgumentException(ex.ToString());
        }


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
}

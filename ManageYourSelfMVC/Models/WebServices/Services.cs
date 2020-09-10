using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ManageYourSelfMVC.Models.WebServices
{
    public class Services
    {
        public ViewModels.Root ExchangeRate() {
            //https://json2csharp.com/
            try
            {
               
                //string path = @"D:\ExchangeRate.txt";
                //using (StreamWriter sw = new StreamWriter(path,true))
                //{
                //    sw.WriteLine("Message from ExchangeRate " + DateTime.Now);
                //}
                //https://hamyarandroid.com/api?t=currency

                // using System.Net;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

                WebRequest request = HttpWebRequest.Create("https://hamyarandroid.com/api?t=currency");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string jsons = reader.ReadToEnd();
                ViewModels.Root r = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewModels.Root>(jsons);

               ManageYourSelfMVC.Utility.Utility.CreateLog(new Exception { }, "ExchangeRate اجرا شد ", " public ViewModels.Root ExchangeRate()");

                return r;
              //  return Json(r, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ManageYourSelfMVC.Utility.Utility.CreateLog(new Exception { }, "ExchangeRate  خطا ", ex.ToString());
                throw;
            }
        }
    }
}
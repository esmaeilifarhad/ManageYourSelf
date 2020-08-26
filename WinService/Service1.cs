using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinService
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        int ScheduleTime = Convert.ToInt32(ConfigurationSettings.AppSettings["ThreadTime"]);
        public Thread Worker = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart start = new ThreadStart(Working);
                Worker = new Thread(start);
                Worker.Start();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void Working()
        {
            while (true)
            {
                string path = "C:\\Sample.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format("win Service is Called on"+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")+""));
                    writer.Close();
                }
                Thread.Sleep(ScheduleTime*60*1000);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if ((Worker != null) & Worker.IsAlive)
                {
                    Worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}

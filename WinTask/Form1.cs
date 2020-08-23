using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Runner();
        }

        private void Runner()
        {
            while (true)
            {
                Run();
                int interval = Properties.Settings.Default.IntervalMin;
                int delay = (int)new TimeSpan(0, interval, 0).TotalMilliseconds;
                System.Threading.Thread.Sleep(delay);
            }
        }
        private void Run()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}

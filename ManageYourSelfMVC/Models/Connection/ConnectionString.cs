using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Models.Connection
{
    public static class ConnectionString
    {

        //public static string _ConnectionString = @"Data Source=gig001P997\ESMAILI;Initial Catalog=ManageYourSelf;User ID=sa;Password=123";
        /// <summary>
        /// خانه
        /// </summary>
        //public static string _ConnectionString = @"Data Source=ESMAEILI\FARHAD;Initial Catalog=ManageYourSelf;User ID=sa;Password=23565; MultipleActiveResultSets=True";
        /// <summary>
        /// سرور موجود در برتینا
        /// </summary>
        public static string _ConnectionString = @"Data Source=185.88.153.14,1430;Initial Catalog=5069_ManageYourSelf;User ID=5069_Esmaeili;Password=861130928; MultipleActiveResultSets=True";
        /// <summary>
        /// سرکار کوروش
        /// </summary>
        //public static string _ConnectionString = @"Data Source=ESMAEILII-PC\SA;Initial Catalog=5069_ManageYourSelf;Integrated Security=True";
 
        public static string _ConnectionStringPersonnelGsystem = @"Data Source=192.168.87.200;Initial Catalog=PersonnelGSystem;User ID=SharePoint;Password=$HareP0int; MultipleActiveResultSets=True";
    }
}
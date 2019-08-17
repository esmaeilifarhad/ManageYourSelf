using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Models.ADO
{
    public class UIDSConnection
    {
        //*****************************************************Insert
        /// <summary>
        /// Insert To Table
        /// </summary>
        /// <param name="Operand"></param>
        /// <param name="MyQuery"></param>
        /// <param name="ParamField"></param>
        /// <param name="ParamVal"></param>
        /// <returns></returns>
        public bool insert(string Operand, string MyQuery, string[] ParamField, string[] ParamVal)
        {
            bool IsSuccess = false;

            //CreateConnection
            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = Models.Connection.ConnectionString._ConnectionString;
            //CreateCommand
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = MyQuery;
            for (int i = 0; i < ParamField.Length; i++)
            {
                Command.Parameters.AddWithValue(ParamField[i], ParamVal[i]);
            }
            //Open Connection
            try
            {
                Connection.Open();
                if (Command.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("هیچ رکوردی درج نشد");
                }
                else
                {
                    IsSuccess = true;
                }


            }
            catch (Exception)
            {
                // System.Windows.Forms.MessageBox.Show(ex.Message);

            }
            finally
            {
                Connection.Close();

            }
            return IsSuccess;

        }
        //******************************************************one Record
        /// <summary>
        /// Return One Record
        /// </summary>
        /// <param name="MyQuery"></param>
        /// <returns></returns>
        public string OneRecord(string MyQuery)
        {
            var result = string.Empty;
            string connection = Models.Connection.ConnectionString._ConnectionString;
            // string connection = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand objCommand = new SqlCommand(MyQuery, con))
                {
                    try
                    {
                        con.Open();
                        result = objCommand.ExecuteScalar().ToString();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
            return result;




        }
        //****************************************************select
        /// <summary>
        /// Select From Tables
        /// </summary>
        /// <param name="MyQuery"></param>
        /// <returns></returns>
        public DataTable Select(string MyQuery)
        {
            DataTable t = null;
            try
            {
                string connection = Models.Connection.ConnectionString._ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand objCommand = new SqlCommand(MyQuery, con))
                    {
                        using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                        {
                            DataTable dt = new DataTable();
                            objSqlDataAdapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ArgumentException(ex.Message);
            }
            return t;
        }
        public DataTable Select(string MyQuery,string ConnectionString)
        {
            DataTable t = null;
            try
            {
                string connection = ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand objCommand = new SqlCommand(MyQuery, con))
                    {
                        using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                        {
                            DataTable dt = new DataTable();
                            objSqlDataAdapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ArgumentException(ex.Message);
            }
            return t;
        }
        public DataTable SelectWhere(string MyQuery, string[] ParamField, string[] ParamVal)
        {
            string connection = Models.Connection.ConnectionString._ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand objCommand = new SqlCommand(MyQuery, con))
                {
                    for (int i = 0; i < ParamField.Length; i++)
                    {
                        objCommand.Parameters.AddWithValue(ParamField[i], ParamVal[i]);
                    }
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                    {
                        DataTable dt = new DataTable();
                        objSqlDataAdapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        //-------------------------------
        public DataSet SelectRetunDatase(string MyQuery)
        {
            try
            {
                string connection = Models.Connection.ConnectionString._ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand objCommand = new SqlCommand(MyQuery, con))
                    {
                        using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                        {
                            DataSet ds = new DataSet();
                            objSqlDataAdapter.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
        //---------------------
        /// <param name="Operand"></param>
        /// <param name="MyQuery"></param>
        /// <param name="ParamField"></param>
        /// <param name="ParamVal"></param>
        /// <returns></returns>
        //***************************************************Update
        public bool Update(string Operand, string MyQuery, string[] ParamField, string[] ParamVal)
        {
            bool IsSuccess = false;

            //CreateConnection
            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = Models.Connection.ConnectionString._ConnectionString;
            //CreateCommand
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = MyQuery;
            for (int i = 0; i < ParamField.Length; i++)
            {
                Command.Parameters.AddWithValue(ParamField[i], ParamVal[i]);
            }
            //Open Connection
            try
            {
                Connection.Open();
                if (Command.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("هیچ رکوردی درج نشد");
                }
                else
                {
                    IsSuccess = true;
                }


            }
            catch (ArgumentException)
            {
                throw new ArgumentException("خطا در ویرایش");
            }
            finally
            {
                Connection.Close();

            }
            return IsSuccess;

        }
        public bool Delete(string MyQuery)
        {
            bool IsSuccess = false;
            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = Models.Connection.ConnectionString._ConnectionString;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = MyQuery;
            try
            {
                Connection.Open();
                if (Command.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("هیچ رکوردی حذف نشد");
                }
                else
                {
                    IsSuccess = true;
                }


            }
            catch (ArgumentException)
            {
                throw new ArgumentException("خطا در حذف");
            }
            finally
            {
                Connection.Close();

            }
            return IsSuccess;

        }
        public DataSet Select_SomeDataTable(string[] MyQuery, string ConnectionString)
        {
            // DataTable t = null;
            try
            {
                DataSet ds = new DataSet();

                string connection = ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    for (int i = 0; i < MyQuery.Length; i++)
                    {
                        using (SqlCommand objCommand = new SqlCommand(MyQuery[i], con))
                        {
                            using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                            {
                                DataTable dt = new DataTable();
                                objSqlDataAdapter.Fill(dt);
                                ds.Tables.Add(dt);

                            }
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا" + ex.Message);
            }
        }
        public DataSet Select_SomeDataTable(string[] MyQuery, string ConnectionString, string[] TableName)
        {
            // DataTable t = null;
            try
            {
                DataSet ds = new DataSet();

                string connection = ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    for (int i = 0; i < MyQuery.Length; i++)
                    {
                        using (SqlCommand objCommand = new SqlCommand(MyQuery[i], con))
                        {
                            using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objCommand))
                            {
                                DataTable dt = new DataTable();
                                dt.TableName = TableName[i];
                                objSqlDataAdapter.Fill(dt);
                                ds.Tables.Add(dt);

                            }
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا" + ex.Message);
            }
        }
        public int BackUp(string FileName)
        {


            string Query = @"Backup Database [ManageYourSelf] To Disk='" + FileName + "'";

            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = Models.Connection.ConnectionString._ConnectionString;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = Query;


            try
            {
                //SqlCommand cmd = null;
                //con = new SqlConnection(Models.ConnectionString.ConnectionString._ConnectionString);
                Connection.Open();
                //cmd = new SqlCommand(BackUp, Connection);
                int Result = Command.ExecuteNonQuery();
                return Result;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            finally
            {
                Connection.Close();

            }
        }
        public int Restore(string FileName)
        {
            string Query = string.Empty;
            string Data_Source =Help.Utility.getBetween(Models.Connection.ConnectionString._ConnectionString, "Data Source=", ";");
            if (Data_Source == "gig001P997\\ESMAILI")
            {
                Query = @"Alter Database [ManageYourSelf] Set Single_User with RollBack Immediate " + " Use Master " + " Restore Database [ManageYourSelf] from Disk=N'" + FileName + @"' with recovery,replace,
                             MOVE 'ManageYourSelf' TO 'C:\Program Files\Microsoft SQL Server\MSSQL12.ESMAILI\MSSQL\DATA\ManageYourSeld.mdf',
                             MOVE 'ManageYourSelf_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL12.ESMAILI\MSSQL\DATA\ManageYourSeld_log.ldf'";
            }
            else if (Data_Source == "ESMAEILI\\FARHAD")
            {
                //C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.FARHAD\MSSQL\DATA
                //C:\Program Files\Microsoft SQL Server\MSSQL10_50.SA\MSSQL\DATA
                Query = @"Alter Database [ManageYourSelf] Set Single_User with RollBack Immediate " + " Use Master " + " Restore Database [ManageYourSelf] from Disk=N'" + FileName + @"' with recovery,replace,
                             MOVE 'ManageYourSelf' TO 'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.FARHAD\MSSQL\DATA\ManageYourSeld.mdf',
                             MOVE 'ManageYourSelf_log' TO 'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.FARHAD\MSSQL\DATA\ManageYourSeld_log.ldf'";
            }
            else
            {
                throw new ArgumentException("در ConnectionString ، DataSource مشخصی پیدا نشد");
            }
            /*
            برای اینکه بتوانیم فایل را در یک سیستم دیگر که آدرسش فرق دارد ریستور نمایم از روش زیر استفاده نمودم
            در این روش از 
            move
            استفاده شده به 
            query
            که نوشته شده دقت کن

            https://stackoverflow.com/questions/6267273/how-to-restore-to-a-different-database-in-sql-server
            */
            //string Query = @"Alter Database [ManageYourSelf] Set Single_User with RollBack Immediate " + " Use Master " + " Restore Database [ManageYourSelf] from Disk=N'" + FileName + "'with recovery,replace";
            //string Query = @"Alter Database [ManageYourSelf] Set Single_User with RollBack Immediate " + " Use Master " + " Restore Database [ManageYourSelf] from Disk=N'" + FileName + @"' with recovery,replace,
            //                 MOVE 'ManageYourSelf' TO 'C:\Program Files\Microsoft SQL Server\MSSQL12.ESMAILI\MSSQL\DATA\ManageYourSeld.mdf',
            //                 MOVE 'ManageYourSelf_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL12.ESMAILI\MSSQL\DATA\ManageYourSeld_log.ldf'";


            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = Models.Connection.ConnectionString._ConnectionString;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = Query;


            try
            {
                //SqlCommand cmd = null;
                //con = new SqlConnection(Models.ConnectionString.ConnectionString._ConnectionString);
                Connection.Open();
                //cmd = new SqlCommand(BackUp, Connection);
                int Result = Command.ExecuteNonQuery();
                return Result;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            finally
            {
                Connection.Close();

            }
        }
    }
}
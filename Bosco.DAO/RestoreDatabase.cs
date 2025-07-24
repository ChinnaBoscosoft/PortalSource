using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Bosco.Utility;

namespace Bosco.DAO
{
    public class RestoreDatabase
    {
        public bool RestoreACPERPdatabase(string databaseName, string connectionString)
        {
            new ErrorLog().WriteError("Inside Restore Method" + connectionString);
            bool Rtn = false;
            try
            {
                //string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                string mysqlDefaultConnection = connectionString;
                if (isACPERPDatabaseExists(databaseName, connectionString))
                {
                    new ErrorLog().WriteError("Head Office Database Restore Started");
                    string sql = string.Empty;
                    Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    using (Stream strmScript = this.GetType().Assembly.GetManifestResourceStream(abyAssembly.GetName().Name + ".ACPERP_HO.sql"))
                    {
                        using (StreamReader sr = new StreamReader(strmScript))
                        {
                            sql = sr.ReadToEnd();
                            sql = sql.Replace("@DB@", databaseName);
                        }

                    }
                    new ErrorLog().WriteError("DB script ");

                    using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
                    {
                        using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                        {
                            try
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.Connection.Open();
                                sqlCommand.ExecuteNonQuery();
                                Rtn = true;
                                new ErrorLog().WriteError("Head Office Database Restore Ended");
                            }
                            catch (Exception ex)
                            {
                                new ErrorLog().WriteError("Exception Thrown:: "+ ex.Message+" "+ex.StackTrace); 
                            }
                           
                        }
                    }
                }
                else
                {
                    Rtn = true;//Database Exists
                    new ErrorLog().WriteError(databaseName + " ACPERP database is Exists");
                }

            }
            catch (Exception err)
            {

                new ErrorLog().WriteError("Error in restore ACPERP database", "HeadOfficeDB Creation", err.StackTrace, "0");
            }
            return Rtn;
        }

        /// <summary>
        /// To verify wheather the Database is Created or not
        /// </summary>
        /// <returns></returns>
        private bool isACPERPDatabaseExists(string databaseName, string connectionString)
        {
            new ErrorLog().WriteError("Is Database Exists" + connectionString);
            bool Rtn = false;
            //string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            string mysqlDefaultConnection = connectionString;
            string sql = "SELECT COUNT(SCHEMA_NAME) AS COUNT FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '" + databaseName + "'";
            int Val = 0;
            using (MySqlConnection sqlCnn = new MySqlConnection(mysqlDefaultConnection))
            {
                using (MySqlCommand sqlCommand = new MySqlCommand(sql, sqlCnn))
                {
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    object ObjVal = sqlCommand.ExecuteScalar();
                    if (ObjVal != null)
                    {
                        Val = int.Parse(ObjVal.ToString());
                    }
                    Rtn = Val > 0 ? false : true;
                }
            }

            new ErrorLog().WriteError("Check DB "+ Val.ToString());

            return Rtn;
        }

        /// <summary>
        /// To check Database Connection to create Head Office Database when Head Office is Created
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool CheckConnection(string connectionString)
        {
            bool result = false;

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                result = true;

                connection.Close();

            }
            catch
            {
                result = false;
            }

            return result;

        }

    }
}

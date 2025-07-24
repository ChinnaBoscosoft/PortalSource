/* Class        : ConfigurationHandler.cs
 * Purpose      : This class provides the dataobject (Handler), connectionstring 
 *                to access data from configured backend in web.config
 * 
 * Author       : CS
 * Created on   : 25-Jul-2010
 */

using System;
using System.Configuration;
using Bosco.Utility;

namespace Bosco.DAO.Configuration
{
    public class ConfigurationHandler
    {
        private string connectionString = "";
        private string databaseType = "";
        private string database = "";

        private static ConfigurationHandler instance;

        public ConfigurationHandler()
        {
            //Map the Application Configuration file to access all the Projects
            connectionString = ConfigurationManager.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ToString();
            databaseType = ConfigurationManager.AppSettings[AppSettingName.DatabaseProvider.ToString()].ToString();
            DatabaseHandler dbHandler = ConfigurationManager.GetSection("app.dataaccess") as DatabaseHandler;

            //Reads the list of data object from collection and get the active data object assembly name
            database = dbHandler.GetDatabaseItem(databaseType);
        }

        //Creational - Singleton Pattern implemented (Global access)
        public static ConfigurationHandler Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ConfigurationHandler();
                }
                return instance;
            }
        }

        public string ConnectionString
        {
            set 
            { 
                connectionString = value;

                if (connectionString == "")
                {
                    connectionString = ConfigurationManager.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ToString();
                }
            }
            get { return connectionString; }
        }

        //
        public string DatabaseLoader
        {
            get { return database; }
        }
    }
}

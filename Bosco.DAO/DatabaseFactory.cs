/***** Applied Pattern of Abstract Factory Method *****
 * Abstract Factory :: DatabaseFactory
 * Abstract Product :: IDatabase
 * Concrete Product :: SQLDataHandler or SQLServerDataHandler, .....
 * 
 * Purpose      :: Provides an interface to which Data object to be created and
 *                  provides an entry point of Data object (Ex: Bosco.SQLServer.SQLDataHandler)
 *                  wich is configured in web.config
 * 
 * Author       :: CS
 * Created on   :: 08-Jul-2010
 */

using System;
using System.Collections.Generic;

using Bosco.DAO.Configuration;
using Bosco.DAO.Data;

namespace Bosco.DAO
{
    //Factory Pattern for handling Multiple Databases
    public class DatabaseFactory
    {
        private static DatabaseFactory databaseFactory;
        private static Object obj = new Object();

        //Singleton Pattern implemented (because of access is global of the application)
        public static DatabaseFactory Instance
        {
            get
            {
                lock (obj)
                {
                    if (databaseFactory == null) databaseFactory = new DatabaseFactory();
                }
                return databaseFactory;
            }
        }

        //Returns the instance of Data Handler of type IDatabase
        public IDatabase CurrentDatabase()
        {
            IDatabase databaseActive = null;
            string type = "";
            object databaseInstance = null;

            type = ConfigurationHandler.Instance.DatabaseLoader;
            Type dbType = Type.GetType(type);

            if (dbType != null)
            {
                databaseInstance = Activator.CreateInstance(dbType);
                databaseActive = databaseInstance as IDatabase;
            }
            return databaseActive;
        }
    }
}

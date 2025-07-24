/* Pattern  : Creational - Factory Method
 *          : Creator -> SQLQueryHandler (defines the interface of objects the factory method creates)
 *          : Product -> IDatabaseQuery
 *          : ConcreteProduct -> Method GetQuery() (Creates the object of type IDatabaseQuery)
 * Created  : 14-Jul-2010
 * Author   : CS
 */

using System;
using System.Configuration;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.DAO
{
    public class SQLQueryHandler : SQLParser, IDatabaseQuery
    {
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            string typeName = "";
            sqlType = SQLType.SQLStatic;
            IDatabaseQuery iSQLServerQuery = null;

            string sqladapter = "SQLAdapter";
            //Change SQL adapter as HOSQLAdapter for DataSyn
            if (dataCommandArgs.ActiveSQLAdapterType == SQLAdapterType.HOSQL)
            {
                sqladapter = "HOSQLAdapter";
            }

            typeName = ConfigurationManager.AppSettings[sqladapter];

            if (typeName != "")
            {
                object instance = this.GetDynamicInstance(typeName, null);

                if (instance != null)
                {
                    iSQLServerQuery = instance as IDatabaseQuery;
                }
            }

            if (iSQLServerQuery != null)
            {
                query = iSQLServerQuery.GetQuery(dataCommandArgs, ref sqlType);
            }
            else
            {
                throw new Exception("SQL Adapter assembly type not found or not properly configured in the config file :: " + this.GetType().Assembly.FullName + ".GetQuery()");
            }

            return query;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class SQLAdapter : CommonMember, IDatabaseQuery
    {
        #region ISQLServerQuery Members

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            string typeName = "";
            IDatabaseQuery iSQLServerQuery = null;

            typeName = GetSQLAssemblyType(dataCommandArgs.Name);

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
                throw new Exception("Invalid SQL Assembly type for " + dataCommandArgs.Name  + " :: " + 
                          this.GetType().Assembly.FullName + ".GetQuery()");
            }

            return query;
        }
        #endregion

        public string GetSQLAssemblyType(string commandName)
        {
            string sqlAssemblyTypeName = "";

            SQLAssemblyTypeSetting.ResourceManager.IgnoreCase = true;
            object objSQLAssemblyTypeName = SQLAssemblyTypeSetting.ResourceManager.GetString(commandName);

            if (objSQLAssemblyTypeName != null)
            {
                sqlAssemblyTypeName = objSQLAssemblyTypeName.ToString();
            }
            else
            {
                throw new Exception("SQL assembly type for " + commandName + " not found :: " +
                          this.GetType().Assembly.FullName + ".GetSQLAssemblyType()");
            }

            return sqlAssemblyTypeName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class ManageSecuritySQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ManageSecurity).FullName)
            {
                query = GetUserRole();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetUserRole()
        {
            string query = "";
            SQLCommand.ManageSecurity sqlCommandId = (SQLCommand.ManageSecurity)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.ManageSecurity.Fetch:
                    {
                        query = " SELECT USER_ID, " +
                                " USER_NAME , " +
                                " UR.USERROLE" +
                                " FROM USER_INFO AS UI " +
                                " INNER JOIN USER_ROLE AS UR ON " +
                                " UI.ROLE_ID=UR.USERROLE_ID ORDER BY USER_NAME ";
                        break;
                    }

                case SQLCommand.ManageSecurity.Edit:
                    {
                        query = " UPDATE USER_INFO SET " +
                                " ROLE_ID=?USER_TYPE" +
                                " WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.ManageSecurity.FetchUserRole:
                    {
                        query = "SELECT USERROLE_ID,USERROLE FROM USER_ROLE WHERE USERROLE IS NOT NULL";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL

        #endregion
    }
}

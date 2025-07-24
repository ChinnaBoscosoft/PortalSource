using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UserRightsSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.UserRights).FullName)
            {
                query = GetUserRights();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetUserRights()
        {
            string query = "";
            SQLCommand.UserRights sqlCommandId = (SQLCommand.UserRights)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.UserRights.FetchAll:
                    {
                        query = "SELECT " +
                               " AR.ID, " +
                               " AR.PARENT_ID, " +
                               " AR.OBJECT_NAME, " +
                               " AR.OBJECT_TYPE " +
                           " FROM " +
                               " ACTIVITIY_RIGHTS AS AR WHERE PARENT_ID <>0  ORDER BY OBJECT_NAME ASC ";
                        break;
                    }
                case SQLCommand.UserRights.Fetch:
                    {
                        query = " SELECT " +
                                " USER_NAME, " +
                                " CASE ROLE_ID WHEN  1  THEN 'Admin' " +
                                " ELSE '' END AS USERROLE, " +
                                " ADDRESS,CONTACT_NO, " +
                                " EMAIL_ID " +
                                " FROM " +
                                " USER_INFO" +
                                " WHERE USER_NAME IS NOT NULL " +
                                " ORDER BY USER_NAME ASC";
                        break;
                    }
                case SQLCommand.UserRights.Update:
                    {
                        query = "UPDATE ACTIVITIY_RIGHTS AS AR SET " +
                            "OBJECT_NAME=?OBJECT_NAME ," +
                            " AR.ADD=?ADD ," +
                            " AR.EDIT=?EDIT ," +
                            " AR.DELETE=?DELETE ," +
                            " AR.VIEW=?VIEW ," +
                            " AR.PRINT=?PRINT ," +
                            " AR.EXPORT=?EXPORT " +
                            " WHERE AR.ID=?ID";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UserRoleSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.UserRole).FullName)
            {
                query = GetUserRole();
            }

            sqlType = this.sqlType;
            return query;
        }



        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetUserRole()
        {
            string query = "";
            SQLCommand.UserRole sqlCommandId = (SQLCommand.UserRole)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.UserRole.Add:
                    {
                        query = " INSERT INTO USER_ROLE ( " +
                                " USERROLE,ACCESSIBILITY)" +
                                " VALUES(?USERROLE,?ACCESSIBILITY)";
                        break;
                    }
                case SQLCommand.UserRole.Edit:
                    {
                        query = " UPDATE USER_ROLE SET " +
                                " USERROLE=?USERROLE" +
                                " WHERE USERROLE_ID=?USERROLE_ID";
                        break;
                    }

                case SQLCommand.UserRole.Delete:
                    {
                        query = "DELETE FROM USER_ROLE WHERE USERROLE_ID=?USERROLE_ID";
                        break;
                    }

                case SQLCommand.UserRole.Fetch:
                    {
                        query = " SELECT " +
                                " USERROLE_ID, " +
                                " USERROLE " +
                                " FROM " +
                                " USER_ROLE" +
                                " WHERE USERROLE_ID=?USERROLE_ID ";
                        break;
                    }
                case SQLCommand.UserRole.FetchAll:
                    {
                        query = @"SELECT USERROLE_ID,USERROLE AS Name FROM USER_ROLE
                                  WHERE USERROLE IS NOT NULL AND ACCESSIBILITY=0  { OR ACCESSIBILITY=?ACCESSIBILITY } ORDER BY USERROLE ASC";
                        break;
                    }
                case SQLCommand.UserRole.FetchUserRole:
                    {
                        query = " SELECT USERROLE_ID,USERROLE AS 'USERROLE' FROM  USER_ROLE WHERE USERROLE_ID=?USERROLE_ID ORDER BY USERROLE ASC";
                        break;
                    }
                case SQLCommand.UserRole.FetchRole:
                    {
                        query = "SELECT USERROLE_ID,USERROLE  FROM  USER_ROLE WHERE USERROLE_ID<>?USERROLE_ID AND ACCESSIBILITY=?ACCESSIBILITY";
                        break;
                    }
                case SQLCommand.UserRole.FetchRoleByAdmin:
                    {
                        query = "SELECT USERROLE_ID,USERROLE  FROM  USER_ROLE WHERE USERROLE_ID = ?USERROLE_ID OR ACCESSIBILITY=?ACCESSIBILITY";
                        break;
                    }
                case SQLCommand.UserRole.FetchRoleByRoleId:
                    {
                        query = "SELECT USERROLE_ID,USERROLE  FROM  USER_ROLE WHERE USERROLE_ID<>?USERROLE_ID AND USERROLE_ID<>1";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}

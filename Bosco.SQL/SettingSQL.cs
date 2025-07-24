using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class SettingSQL :IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Setting).FullName)
            {
                query = GetSettingSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetSettingSQL()
        {
            string query = "";
            SQLCommand.Setting sqlCommandId = (SQLCommand.Setting)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Setting.InsertUpdate:
                    {
                        query = "INSERT INTO MASTER_SETTING ( " +
                               "SETTING_NAME, " +
                               "VALUE ) VALUES( " +
                               "?SETTING_NAME, " +
                               "?VALUE) ON DUPLICATE KEY UPDATE VALUE=?VALUE";
                        break;
                    }
                case SQLCommand.Setting.Update:
                    {
                        query = "UPDATE MASTER_SETTING SET " +
                                    "SETTING_NAME = ?SETTING_NAME, " +
                                    "VALUE=?VALUE " +
                                    " WHERE SETTING_NAME=?SETTING_NAME ";
                        break;
                    }
                case SQLCommand.Setting.Fetch:
                    {
                        query = "SELECT " +
                                "SETTING_NAME AS Name, " +
                                "VALUE AS Value " +
                            "FROM" +
                                " MASTER_SETTING ORDER BY SETTING_NAME ASC";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}

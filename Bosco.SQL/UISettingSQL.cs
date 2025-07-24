using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UISettingSQL :IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.UISetting).FullName)
            {
                query = GetUISettingSQL();
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
        private string GetUISettingSQL()
        {
            string query = "";
            SQLCommand.UISetting sqlCommandId = (SQLCommand.UISetting)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {

                case SQLCommand.UISetting.InsertUpdateUI:
                    {
                        query = "INSERT INTO MASTER_SETTING ( " +
                               "SETTING_NAME, " +
                               "VALUE, " +
                               "USER_ID ) VALUES( " +
                               "?SETTING_NAME, " +
                               "?VALUE, " +
                               "?USER_ID) ON DUPLICATE KEY UPDATE VALUE=?VALUE";
                        break;
                    }

               
                case SQLCommand.UISetting.FetchUI:
                    {
                        query = "SELECT " +
                                "SETTING_NAME AS Name, " +
                                "VALUE AS Value " +
                            "FROM" +
                                " MASTER_SETTING WHERE USER_ID=?USER_ID ORDER BY SETTING_NAME ASC";
                        break;
                    }
                case SQLCommand.UISetting.DeleteUI:
                    {
                        query = "DELETE FROM MASTER_SETTING WHERE USER_ID=?USER_ID ";
                        break;
                    }
            }

            return query;
        }
        #endregion UI Setting SQL
    }
}

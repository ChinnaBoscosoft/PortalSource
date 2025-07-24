using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class MasterRightsSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.MasterRights).FullName)
            {
                query = GetMasterRights();
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
        private string GetMasterRights()
        {
            string query = "";
            SQLCommand.MasterRights sqlCommandId = (SQLCommand.MasterRights)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.MasterRights.FetchByMasterName:
                    {
                        query = " SELECT " +
                                " ALLOW_ACCESS" +
                                " FROM " +
                                " MASTER_RIGHTS" +
                                " WHERE MASTER_NAME IS NOT NULL AND MASTER_NAME=?MASTER_NAME ";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}

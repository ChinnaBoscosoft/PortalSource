using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class InKindUtilisedSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.InKindUtilised).FullName)
            {
                query = GetInKindUtilised();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the FD Renewal.
        /// </summary>
        /// <returns></returns>
        private string GetInKindUtilised()
        {
            string query = "";
            SQLCommand.InKindUtilised sqlCommandId = (SQLCommand.InKindUtilised)(dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.InKindUtilised.Add:
                    {
                        query = " ";
                        break;
                    }

                case SQLCommand.InKindUtilised.Update:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.InKindUtilised.Fetch:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.InKindUtilised.FetchAll:
                    {
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}

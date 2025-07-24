using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class PurposeSQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Purposes).FullName)
            {
                query = GetPurposeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetPurposeSQL()
        {
            string query = "";
            SQLCommand.Purposes sqlCommandId = (SQLCommand.Purposes)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.Purposes.Add:
                    {
                        query = "INSERT INTO MASTER_CONTRIBUTION_HEAD ( " +
                               "CODE, " +
                               "FC_PURPOSE ) VALUES( " +
                               "?CODE, " +
                               "?FC_PURPOSE) ";

                        break;
                    }
                case SQLCommand.Purposes.Update:
                    {
                        query = "UPDATE MASTER_CONTRIBUTION_HEAD SET " +
                                    "CODE =?CODE, " +
                                    "FC_PURPOSE =?FC_PURPOSE " +
                                    "WHERE CONTRIBUTION_ID=?CONTRIBUTION_ID ";

                        break;
                    }

                case SQLCommand.Purposes.Delete:
                    {
                        query = "DELETE FROM MASTER_CONTRIBUTION_HEAD WHERE CONTRIBUTION_ID=?CONTRIBUTION_ID ";
                        break;
                    }

                case SQLCommand.Purposes.Fetch:
                    {
                        query = "SELECT " +
                               "CONTRIBUTION_ID AS CONTRIBUTION_HEAD_ID, " +
                               "CODE, " +
                               "FC_PURPOSE " +
                               "FROM " +
                               "MASTER_CONTRIBUTION_HEAD WHERE CONTRIBUTION_ID=?CONTRIBUTION_ID";
                        break;
                    }

                case SQLCommand.Purposes.FetchAll:
                    {
                        query = "SELECT " +
                               "CONTRIBUTION_ID, " +
                               "CODE AS Code, " +
                               "FC_PURPOSE AS Purpose " +
                               "FROM " +
                               "MASTER_CONTRIBUTION_HEAD ORDER BY FC_PURPOSE ASC";
                        break;
                    }
                case SQLCommand.Purposes.PurposeFetchAll:
                    {
                        query = "SELECT CODE, FC_PURPOSE FROM master_contribution_head";
                        break;
                    }
            }
            return query;
        }
    }
}

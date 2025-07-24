using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class GeneralateOpeningBalanceSQL : IDatabaseQuery
    {
        #region ISQLedgerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.CongregationOpeningBalance).FullName)
            {
                query = GetGeneralateOpeningSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetGeneralateOpeningSQL()
        {
            string query = "";
            SQLCommand.CongregationOpeningBalance sqlCommandId = (SQLCommand.CongregationOpeningBalance)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CongregationOpeningBalance.FetchGeneralateAssetLiabilty:
                    {
                        query = @"SELECT CL.CON_LEDGER_ID,
                                       CL.CON_LEDGER_CODE,
                                CL.CON_LEDGER_NAME AS CON_LEDGER_NAME,
                                MBSC.AMOUNT AS AMOUNT
                                FROM CONGREGATION_LEDGER AS CL
                                LEFT JOIN MASTER_BALANCE_SHEET_CLOSING AS MBSC ON CL.CON_LEDGER_ID = MBSC.CON_LEDGER_ID AND ACC_YEAR_FROM =?ACC_YEAR_FROM
                                WHERE (CL.CON_LEDGER_CODE LIKE '%A%' OR CL.CON_LEDGER_CODE LIKE '%B%')
                                AND (CL.CON_LEDGER_ID <> CON_PARENT_LEDGER_ID AND CL.CON_LEDGER_CODE NOT IN ('A.4.1'))
                                OR CL.CON_LEDGER_CODE IN ('A.2','A.3','A.5','B.1','B.4') ORDER BY CL.CON_LEDGER_CODE, CL.CON_LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.CongregationOpeningBalance.SaveGeneralateOpeningDetails:
                    {
                        query = @"INSERT INTO MASTER_BALANCE_SHEET_CLOSING (CON_LEDGER_ID,AMOUNT,ACC_YEAR_FROM,IS_CB) VALUES (?CON_LEDGER_ID,?AMOUNT,?ACC_YEAR_FROM,?IS_CB);";
                        break;
                    }
                case SQLCommand.CongregationOpeningBalance.DeleteGenOpeningBalance:
                    {
                        query = @"DELETE FROM MASTER_BALANCE_SHEET_CLOSING WHERE ACC_YEAR_FROM =?ACC_YEAR_FROM";
                        break;
                    }
            }
            return query;
        }
        #endregion

    }
}

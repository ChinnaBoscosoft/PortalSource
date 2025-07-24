using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class InKindReceivedSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.InKindReceived).FullName)
            {
                query = GetInKindReceivedSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the In Kind Receid.
        /// </summary>
        /// <returns></returns>
        private string GetInKindReceivedSQL()
        {
            string query = "";
            SQLCommand.InKindReceived sqlCommandId = (SQLCommand.InKindReceived)(dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.InKindReceived.Add:
                    {
                        query = "INSERT INTO INKINDTRANS( " +
                                "INKIND_TRANS_DATE, " +
                                "SEQUENCE_NO," +
                                "PROJECT_ID," +
                                "LEDGER_ID," +
                                "ARTICLE_ID," +
                                "PURPOSE_ID," +
                                "CONTRIBUTION_TYPE," +
                                "INKIND_QUANTITY," +
                                "INKIND_UNIT," +
                                "INKIND_VALUE," +
                                "RECEIVED_INFORMATION," +
                                "DONOR_ID," +
                                "BANK_ACCOUNT_NO," +
                                "CHEQUE_NO," +
                                "NARRATION," +
                                "STATUS," +
                                "CREATED_ON," +
                                "CREATED_BY)" +
                                "VALUES( " +
                                "?INKIND_TRANS_DATE," +
                                "?SEQUENCE_NO," +
                                "?PROJECT_ID," +
                                "?LEDGER_ID," +
                                "?ARTICLE_ID," +
                                "?PURPOSE_ID," +
                                "?CONTRIBUTION_TYPE," +
                                "?INKIND_QUANTITY," +
                                "?INKIND_UNIT," +
                                "?INKIND_VALUE," +
                                "?RECEIVED_INFORMATION, " +
                                "?DONOR_ID, " +
                                "?BANK_ACCOUNT_NO," +
                                "?CHEQUE_NO," +
                                "?NARRATION," +
                                "?STATUS," +
                                "?CREATED_ON, " +
                                "?CREATED_BY) ";
                        break;
                    }

                case SQLCommand.InKindReceived.Update:
                    {
                        query = "UPDATE INKINDTRANS SET " +
                               " INKIND_TRANS_DATE=?INKIND_TRANS_DATE," +
                                "SEQUENCE_NO=?SEQUENCE_NO, " +
                                "PROJECT_ID=?PROJECT_ID, " +
                                "LEDGER_ID=?LEDGER_ID, " +
                                "ARTICLE_ID=?ARTICLE_ID, " +
                                "PURPOSE_ID=?PURPOSE_ID, " +
                                "CONTRIBUTION_TYPE=?CONTRIBUTION_TYPE," +
                                "INKIND_QUANTITY=?INKIND_QUANTITY, " +
                                "INKIND_UNIT=?INKIND_UNIT, " +
                                "INKIND_VALUE=?INKIND_VALUE," +
                                "RECEIVED_INFORMATION=?RECEIVED_INFORMATION, " +
                                "DONOR_ID=?DONOR_ID," +
                                "BANK_ACCOUNT_NO=?BANK_ACCOUNT_NO," +
                                "CHEQUE_NO=?CHEQUE_NO, " +
                                "NARRATION=?NARRATION, " +
                                "STATUS=?STATUS, " +
                                "MODIFIED_ON=?MODIFIED_ON," +
                                "MODIFIED_BY=?MODIFIED_BY " +
                                "WHERE INKIND_TRANS_ID=?INKIND_TRANS_ID";
                        break;
                    }
                case SQLCommand.InKindReceived.Delete:
                    {
                        query = "DELETE FROM INKINDTRANS WHERE INKIND_TRANS_ID=?INKIND_TRANS_ID";
                        break;
                    }

                case SQLCommand.InKindReceived.Fetch:
                    {
                        query = " SELECT INKIND_TRANS_ID, INKIND_TRANS_DATE, SEQUENCE_NO, " +
                                "PROJECT_ID, LEDGER_ID, ARTICLE_ID, " +
                                "PURPOSE_ID, CASE CONTRIBUTION_TYPE WHEN 'F' THEN 0 ELSE 1 END AS 'CONTRIBUTION_TYPE'," +
                                "INKIND_QUANTITY, INKIND_UNIT, INKIND_VALUE, " +
                                "RECEIVED_INFORMATION, DONOR_ID, " +
                                "BANK_ACCOUNT_NO,CHEQUE_NO,NARRATION," +
                                "STATUS,CREATED_ON,MODIFIED_ON,CREATED_BY, MODIFIED_BY " +
                                " FROM INKINDTRANS WHERE INKIND_TRANS_ID=?INKIND_TRANS_ID AND STATUS=1 ";
                        // "AND RECEIVED_INFORMATION=?RECEIVED_INFORMATION";
                        break;
                    }
                case SQLCommand.InKindReceived.FetchAll:
                    {
                        query = "SELECT "+
                                "IK.INKIND_TRANS_ID, "+
                                "ML.LEDGER_NAME, "+
                                "MCH.FC_PURPOSE, " +
                                "MD.NAME, " +
                                "MIA.ARTICLE, "+
                                "CASE IK.CONTRIBUTION_TYPE WHEN 'F' THEN 'FIRST' "+
                                "ELSE  'SUBSEQUENT' END AS 'CONTRIBUTION_TYPE', "+
                                "IK.INKIND_QUANTITY, "+
                                "IK.INKIND_UNIT, "+
                                "IK.INKIND_VALUE, "+
                                "IK.RECEIVED_INFORMATION, "+
                                "IK.NARRATION FROM INKINDTRANS IK "+
                                "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=IK.LEDGER_ID "+
                                "INNER JOIN MASTER_CONTRIBUTION_HEAD MCH ON MCH.CONTRIBUTION_ID=IK.PURPOSE_ID "+
                                "INNER JOIN MASTER_INKIND_ARTICLE MIA ON MIA.ARTICLE_ID=IK.ARTICLE_ID "+
                                "INNER JOIN MASTER_DONAUD MD ON MD.DONAUD_ID =IK.DONOR_ID " +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID =?PROJECT_ID "+
                                "AND RECEIVED_INFORMATION = ?RECEIVED_INFORMATION "+
                                "AND IK.INKIND_TRANS_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND MP.DELETE_FLAG<>1";
                              break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}

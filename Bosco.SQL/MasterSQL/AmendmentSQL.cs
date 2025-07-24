using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AmendmentSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        #region ISQLServerQuery Members

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Amendments).FullName)
            {
                query = GetAmendmentSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        /// <summary>
        /// Purpose:To Perform the action of the software details.
        /// </summary>
        /// <returns></returns>
        private string GetAmendmentSQL()
        {

            string query = "";
            SQLCommand.Amendments sqlCommandId = (SQLCommand.Amendments)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {

                case SQLCommand.Amendments.UpdateStatus:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET AMENDMENT_FLAG=1 WHERE VOUCHER_ID =?VOUCHER_ID AND BRANCH_ID=?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.Amendments.Save:
                    {
                        
                        query=@"INSERT INTO AMENDMENT_HISTORY
                                (BRANCH_ID,
                                AMENDMENT_DATE,
                                VOUCHER_ID,
                                REMARKS,
                                STATUS)
                                VALUES
                                (?BRANCH_OFFICE_ID,
                                ?AMENDMENT_DATE,
                                ?VOUCHER_ID,
                                ?REMARKS,
                                ?STATUS)";
                        break;
                    }
                case SQLCommand.Amendments.FetchRemark:
                    {
                        query = "SELECT REMARKS FROM AMENDMENT_HISTORY WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.Amendments.UpdateRemark:
                    {
                        query="UPDATE AMENDMENT_HISTORY SET REMARKS=?REMARKS WHERE VOUCHER_ID=?VOUCHER_ID"; 
                        break;
                    }
                case SQLCommand.Amendments.FetchAmendmentHistory:
                    {
                        query = "SELECT VM.VOUCHER_ID,\n" +
                                "       VM.VOUCHER_DATE,\n" +
                                "       A.AMENDMENT_DATE, "+
                                "       ML.LEDGER_NAME," +
                                "       MP.PROJECT,\n" +
                                "       VM.VOUCHER_NO,\n" + 
                                "       CASE\n" + 
                                "         WHEN VM.VOUCHER_TYPE = 'RC' THEN\n" + 
                                "          'Receipt'\n" + 
                                "         WHEN VM.VOUCHER_TYPE = 'PY' THEN\n" + 
                                "          'Payment'\n" + 
                                "         WHEN VM.VOUCHER_TYPE = 'CN' THEN\n" + 
                                "          'Contra'\n" + 
                                "       END AS VOUCHER_TYPE,\n" + 
                                "       VT.AMOUNT,\n" + 
                                "       A.REMARKS\n" + 
                                "  FROM AMENDMENT_HISTORY AS A\n" + 
                                "  LEFT JOIN VOUCHER_MASTER_TRANS AS VM\n" + 
                                "    ON VM.VOUCHER_ID = A.VOUCHER_ID\n" + 
                                "   AND VM.BRANCH_ID = A.BRANCH_ID\n" + 
                                "  LEFT JOIN VOUCHER_TRANS AS VT\n" + 
                                "    ON VT.VOUCHER_ID = A.VOUCHER_ID\n" + 
                                "   AND VT.BRANCH_ID = A.BRANCH_ID\n" + 
                                " LEFT JOIN MASTER_PROJECT AS MP \n" + 
                                " ON MP.PROJECT_ID=VM.PROJECT_ID\n" + 
                                "   AND VM.BRANCH_ID=A.BRANCH_ID\n" + 
                                "LEFT JOIN MASTER_LEDGER ML "+
                                "ON ML.LEDGER_ID=VT.LEDGER_ID "+
                                "  LEFT JOIN BRANCH_OFFICE AS BF\n" + 
                                "    ON BF.BRANCH_OFFICE_ID = A.BRANCH_ID\n" +
                                " WHERE BF.BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE\n" + 
                                "   AND A.STATUS = 1\n" + 
                                " GROUP BY A.AMENDMENT_ID";
                        break;
                    }


            }
            return query;
        }
        #endregion
    }
}

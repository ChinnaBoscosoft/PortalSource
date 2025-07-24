using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;


namespace Bosco.SQL
{
    public class DeducteeTypeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DeducteeType).FullName)
            {
                query = GetDeducteeTypeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion
        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Deductee details.
        /// </summary>
        /// <returns></returns>
        private string GetDeducteeTypeSQL()
        {
            string query = "";
            SQLCommand.DeducteeType sqlCommandId = (SQLCommand.DeducteeType)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.DeducteeType.Add:
                    {
                        query = "INSERT INTO TDS_DEDUCTEE_TYPE\n" +
                                "  (NAME, RESIDENTIAL_STATUS, DEDUCTEE_TYPE, STATUS) VALUES(\n" +
                                "   ?NAME,\n" +
                                "   ?RESIDENTIAL_STATUS,\n" +
                                "   ?DEDUCTEE_TYPE,\n" +
                                "   ?STATUS)";
                        break;
                    }
                case SQLCommand.DeducteeType.Update:
                    {
                        query = "UPDATE TDS_DEDUCTEE_TYPE\n" +
                                " SET NAME= ?NAME,\n" +
                                " RESIDENTIAL_STATUS = ?RESIDENTIAL_STATUS,\n" +
                                " DEDUCTEE_TYPE = ?DEDUCTEE_TYPE,\n" +
                                " STATUS = ?STATUS\n" +
                                " WHERE DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID";

                        break;
                    }
                case SQLCommand.DeducteeType.Delete:
                    {
                        query = "DELETE FROM TDS_DEDUCTEE_TYPE WHERE DEDUCTEE_TYPE_ID=?DEDUCTEE_TYPE_ID";
                        break;
                    }
                case SQLCommand.DeducteeType.FetchAll:
                    {
                        query = "   SELECT DEDUCTEE_TYPE_ID,NAME,\n" +
                                "       (CASE\n" +
                                "         WHEN RESIDENTIAL_STATUS = 0 THEN\n" +
                                "          \"Resident\"\n" +
                                "         ELSE\n" +
                                "          \"Non_Resident\"\n" +
                                "       END) AS RESIDENTIAL_STATUS,\n" +
                                "       (CASE\n" +
                                "         WHEN DEDUCTEE_TYPE = 0 THEN\n" +
                                "          \"Company\"\n" +
                                "         ELSE\n" +
                                "          \"Non Company\"\n" +
                                "       END) AS DEDUCTEE_TYPE,\n" +
                                "       (CASE\n" +
                                "         WHEN STATUS = 1 THEN\n" +
                                "          \"Active\"\n" +
                                "         ELSE\n" +
                                "          \"Inactive\"\n" +
                                "       END) AS STATUS\n" +
                                "  FROM TDS_DEDUCTEE_TYPE";
                        break;
                    }
                case SQLCommand.DeducteeType.Fetch:
                    {

                        query = " SELECT DEDUCTEE_TYPE_ID,NAME,\n" +
                                "  RESIDENTIAL_STATUS,\n" +
                                "  DEDUCTEE_TYPE,\n" +
                                "    STATUS \n" +
                                "  FROM TDS_DEDUCTEE_TYPE\n" +
                                " WHERE DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID";

                        break;
                    }
                case SQLCommand.DeducteeType.FetchActiveDeductTypes:
                    {
                        query = "   SELECT DEDUCTEE_TYPE_ID,NAME,\n" +
                                 "       (CASE\n" +
                                 "         WHEN RESIDENTIAL_STATUS = 0 THEN\n" +
                                 "          \"Resident\"\n" +
                                 "         ELSE\n" +
                                 "          \"Non_Resident\"\n" +
                                 "       END) AS RESIDENTIAL_STATUS,\n" +
                                 "       (CASE\n" +
                                 "         WHEN DEDUCTEE_TYPE = 0 THEN\n" +
                                 "          \"Company\"\n" +
                                 "         ELSE\n" +
                                 "          \"Non Company\"\n" +
                                 "       END) AS DEDUCTEE_TYPE,\n" +
                                 "       (CASE\n" +
                                 "         WHEN STATUS = 1 THEN\n" +
                                 "          \"Active\"\n" +
                                 "         ELSE\n" +
                                 "          \"Inactive\"\n" +
                                 "       END) AS STATUS\n" +
                                 "  FROM TDS_DEDUCTEE_TYPE WHERE STATUS=1";
                        break;
                    }
                case SQLCommand.DeducteeType.FetchDeductType:
                    {
                        query = "SELECT DEDUTEE_TYPE_ID AS ID,\n" +
                            "       NATURE_OF_PAYMENT_ID,\n" +
                                                 "       TDT.NAME,\n" +
                                                 "       TCP.LEDGER_ID,\n" +
                                                 "       ML.LEDGER_NAME\n" +
                                                 "  FROM TDS_CREDTIORS_PROFILE AS TCP\n" +
                                                 "  LEFT JOIN TDS_DEDUCTEE_TYPE AS TDT\n" +
                                                 "    ON TCP.DEDUTEE_TYPE_ID = TDT.DEDUCTEE_TYPE_ID\n" +
                                                 "  LEFT JOIN MASTER_LEDGER AS ML\n" +
                                                 "    ON TCP.LEDGER_ID = ML.LEDGER_ID\n" +
                                                 " WHERE ML.GROUP_ID = 26\n" +
                                                 "   AND ML.IS_TDS_LEDGER = 1\n" +
                                                 "   AND TCP.LEDGER_ID = ?LEDGER_ID\n" +
                                                 " GROUP BY TDT.NAME";
                        break;
                    }
            }

            return query;
        }
        #endregion DeducteeType SQL
    }
}

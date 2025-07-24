using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;


namespace Bosco.SQL
{
    public class NatureofPaymentSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.NatureofPayments).FullName)
            {
                query = GetNatureofPaymentSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Payment details.
        /// </summary>
        /// <returns></returns>
        private string GetNatureofPaymentSQL()
        {
            string query = "";
            SQLCommand.NatureofPayments sqlCommandId = (SQLCommand.NatureofPayments)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.NatureofPayments.Add:
                    {
                        query = "INSERT INTO TDS_NATURE_PAYMENT\n" +
                                "  ( NAME, PAYMENT_CODE,TDS_SECTION_ID,DESCRIPTION,STATUS)\n" +
                                "VALUES(\n" +
                                "   ?PAYMENT_NAME,\n" +
                                "   ?PAYMENT_CODE,\n" +
                                "   ?TDS_SECTION_ID,\n" +
                                "   ?NOTES,?STATUS)";
                        break;
                    }
                case SQLCommand.NatureofPayments.Update:
                    {
                        query = "UPDATE  TDS_NATURE_PAYMENT SET \n" +
                                " NAME = ?PAYMENT_NAME,\n" +
                                " PAYMENT_CODE=?PAYMENT_CODE,\n" +
                                " TDS_SECTION_ID=?TDS_SECTION_ID,\n" +
                                 " DESCRIPTION= ?NOTES, \n" +
                                 " STATUS=?STATUS\n" +
                                 "WHERE NATURE_PAY_ID=?NATUREOFPAYMENTS_ID";
                        break;
                    }
                case SQLCommand.NatureofPayments.Delete:
                    {
                        query = "DELETE FROM TDS_NATURE_PAYMENT WHERE NATURE_PAY_ID=?NATUREOFPAYMENTS_ID";
                        break;
                    }
                case SQLCommand.NatureofPayments.FetchAll:
                    {
                        query = "SELECT NATURE_PAY_ID,\n" +
                                "NAME AS NAME,\n" +
                                "PAYMENT_CODE,\n" +
                                "DESCRIPTION,STATUS\n" +
                                "  FROM TDS_NATURE_PAYMENT  WHERE STATUS=1;";

                        break;
                    }
                case SQLCommand.NatureofPayments.Fetch:
                    {
                        query = "SELECT NATURE_PAY_ID,\n" +
                                "NAME AS PAYMENT_NAME,\n" +
                                "PAYMENT_CODE,\n" +
                                "TDS_SECTION_ID,\n" +
                                "DESCRIPTION AS NOTES,STATUS\n" +
                                "  FROM TDS_NATURE_PAYMENT WHERE NATURE_PAY_ID=?NATUREOFPAYMENTS_ID";
                        break;
                    }
                case SQLCommand.NatureofPayments.FetchSectionCodes:
                    {
                        query = "SELECT TDS_SECTION_ID,CONCAT(CODE,CONCAT(' - ',SECTION_NAME)) AS 'SECTION_NAME' FROM TDS_SECTION WHERE STATUS=1 ";

                        break;
                    }
                case SQLCommand.NatureofPayments.FetchNatureofPaymentsSection:
                    {
                        query = "SELECT TN.NATURE_PAY_ID,\n" +
                        "       TN.PAYMENT_CODE,\n" +
                        "       TN.NAME,\n" +
                        "       TS.SECTION_NAME,\n" +
                        "       CASE\n" +
                        "         WHEN TN.STATUS = 1 THEN\n" +
                        "          'Active'\n" +
                        "         ELSE\n" +
                        "          'Inactive'\n" +
                        "       END AS STATUS\n" +
                        "  FROM TDS_NATURE_PAYMENT TN\n" +
                        "  LEFT JOIN TDS_SECTION TS\n" +
                        "    ON TN.TDS_SECTION_ID = TS.TDS_SECTION_ID";

                        break;
                    }
                case SQLCommand.NatureofPayments.FetchNatureofPayments:
                    {
                        query = "SELECT NATURE_PAY_ID, NAME FROM TDS_NATURE_PAYMENT WHERE STATUS=1";
                        break;
                    }

                case SQLCommand.NatureofPayments.FetchTaxRate:
                    {
                        query = "SELECT TTR.TAX_RATE_ID,\n" +
                        "       TP.TDS_POLICY_ID,\n" +
                        "       TDS_RATE,\n" +
                        "       TDS_EXEMPTION_LIMIT,\n" +
                        "       TP.APPLICABLE_FROM AS APPLICABLE_FROM,\n" +
                        "       TTR.TDS_TAX_TYPE_ID,\n" +
                        "       TDT.STATUS\n" +
                        "  FROM TDS_POLICY AS TP\n" +
                        "  LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "    ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "  LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
                        "    ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID\n" +
                        " WHERE TP.TDS_NATURE_PAYMENT_ID = ?NATURE_PAY_ID\n" +
                        "   AND TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        "   AND TP.APPLICABLE_FROM <=  ?APPLICABLE_FROM\n" +
                        "   AND TDS_TAX_TYPE_ID NOT IN (2)\n" +
                        "   AND TDT.STATUS = 1 ORDER BY TDS_TAX_TYPE_ID ";
                        //" GROUP BY TP.TDS_POLICY_ID\n" +
                        //" ORDER BY TAX_RATE_ID DESC LIMIT 1";

                        break;
                    }
                case SQLCommand.NatureofPayments.FetchTDSWithoutPAN:
                    {
                        query = "SELECT TTR.TAX_RATE_ID,\n" +
                        "       TP.TDS_POLICY_ID,\n" +
                        "       TDS_RATE,\n" +
                        "       TDS_EXEMPTION_LIMIT,\n" +
                        "       TP.APPLICABLE_FROM AS APPLICABLE_FROM,\n" +
                        "       TTR.TDS_TAX_TYPE_ID,\n" +
                        "       TDT.STATUS\n" +
                        "  FROM TDS_POLICY AS TP\n" +
                        "  LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "    ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "  LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
                        "    ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID\n" +
                        " WHERE TP.TDS_NATURE_PAYMENT_ID = ?NATURE_PAY_ID\n" +
                        "   AND TDS_DEDUCTEE_TYPE_ID = ?DEDUCTEE_TYPE_ID\n" +
                        "   AND TP.APPLICABLE_FROM <=  ?APPLICABLE_FROM\n" +
                        "   AND TDS_TAX_TYPE_ID NOT IN (1)\n" +
                       "   AND TDT.STATUS = 1 ORDER BY TDS_TAX_TYPE_ID ";
                        //" GROUP BY TP.TDS_POLICY_ID\n" +
                        //" ORDER BY TAX_RATE_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.NatureofPayments.FetchTDSLedger:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_NAME\n" +
                                        "  FROM MASTER_LEDGER AS ML\n" +
                                        "  LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                                        "    ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                                        "    WHERE IS_TDS_LEDGER = 1\n" +
                                        "   {AND NATURE_OF_PAYMENT_ID = ?NATURE_PAY_ID}";
                        break;
                    }

                case SQLCommand.NatureofPayments.FetchNatureOfPaymentWithCode:
                    {
                        query = "SELECT  NATURE_PAY_ID, CONCAT(PAYMENT_CODE,' - ', NAME) AS NAME FROM TDS_NATURE_PAYMENT WHERE STATUS=1";
                        break;
                    }
            }
            return query;
        }
        #endregion NatureofPayment SQL

    }
}

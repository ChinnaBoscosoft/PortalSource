using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class FDAccountSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FDAccount).FullName)
            {
                query = GetFDAccount();
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
        private string GetFDAccount()
        {
            string query = "";
            SQLCommand.FDAccount sqlCommandId = (SQLCommand.FDAccount)(dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.FDAccount.FetchLedgers:
                    {
                        query = "SELECT ML.LEDGER_ID, LEDGER_CODE, LEDGER_NAME, MP.PROJECT, MP.PROJECT_ID\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PROJECT_LEDGER AS PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " WHERE ML.LEDGER_SUB_TYPE = 'FD' GROUP BY PL.LEDGER_ID";

                        break;
                    }
                case SQLCommand.FDAccount.FetchProjectByLedger:
                    {

                        query = "SELECT ML.LEDGER_ID, LEDGER_CODE, LEDGER_NAME, MP.PROJECT, MP.PROJECT_ID\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PROJECT_LEDGER AS PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " WHERE ML.LEDGER_SUB_TYPE = 'FD'";

                        break;
                    }
                case SQLCommand.FDAccount.FetchAllProjectId:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDRegistersView:
                    {

                        query = " SELECT FDA.INVESTMENT_DATE,\n" +
                                " IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                                " FDA.FD_ACCOUNT_NUMBER,\n" +
                                " CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                                " MLG.LEDGER_NAME,\n" +
                                " MPR.PROJECT,\n" +
                                " IFNULL(FDR.INTEREST_RATE, FDA.INTEREST_RATE) AS INTEREST_RATE,\n" +
                                " FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) AS PRINCIPLE_AMOUNT,\n" +
                                " IFNULL(FDR.INTEREST_AMOUNT, 0) AS INTEREST_AMOUNT,\n" +
                                " IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                " FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                " IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) AS TOTAL_AMOUNT,\n" +
                                " IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS WITHDRAWAL_AMOUNT,\n" +
                                " FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                " IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS BALANCE_AMOUNT,\n" +
                                " IF(FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                                " IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" +
                                " 'Closed',\n" +
                                " 'Active') AS CLOSING_STATUS\n" +
                                " FROM FD_ACCOUNT AS FDA\n" +
                                " LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                " MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                " MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                " SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                " SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                " SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                " FROM FD_RENEWAL\n" +
                                " WHERE STATUS = 1\n" +
                                " AND RENEWAL_DATE < ?DATE_FROM \n" +
                                " GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
                                " ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                                " LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                                " MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                                " MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                                " INTEREST_RATE,\n" +
                                " SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                                " SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                                " SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                                " FROM FD_RENEWAL\n" +
                                " WHERE STATUS = 1\n" +
                                " AND RENEWAL_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                                " GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                                " ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                                " LEFT JOIN MASTER_BANK AS MBK\n" +
                                " ON FDA.BANK_ID = MBK.BANK_ID\n" +
                                " LEFT JOIN MASTER_PROJECT MPR\n" +
                                " ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                                " LEFT JOIN MASTER_LEDGER MLG\n" +
                                " ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                                " WHERE FDA.STATUS = 1\n" +
                                " AND FDA.INVESTMENT_DATE <= ?DATE_TO\n" +
                                " { AND MPR.PROJECT_ID IN (?PROJECT_ID) }\n" +
                                " AND FDA.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                                " AND FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                                " IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) <> 0;";


                        break;
                    }
                case SQLCommand.FDAccount.FetchDate:
                    {
                        query = " SELECT MIN(FDA.INVESTMENT_DATE) AS MIN_DATE ,MAX(FDR.MATURITY_DATE) AS MAX_DATE FROM FD_ACCOUNT FDA\n" +
                                " INNER JOIN FD_RENEWAL FDR ON\n" +
                                " FDA.BRANCH_ID=FDR.BRANCH_ID\n" +
                                " WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.FDAccount.DeleteProjectLedger:
                    {
                        query = "DELETE FROM PROJECT_LEDGER\n" +
                                " WHERE LEDGER_ID = ?LEDGER_ID";

                        break;
                    }
                case SQLCommand.FDAccount.Add:
                    {
                        query = "INSERT INTO FD_ACCOUNT\n" +
                        "  (FD_ACCOUNT_NUMBER,\n" +
                        "   PROJECT_ID,\n" +
                        "   LEDGER_ID,\n" +
                        "   BANK_ID,\n" +
                        "   FD_VOUCHER_ID,\n" +
                        "   AMOUNT,\n" +
                        "   TRANS_TYPE,\n" +
                        "   RECEIPT_NO,\n" +
                        "   ACCOUNT_HOLDER,\n" +
                        "   INVESTMENT_DATE,\n" +
                        "   TRANS_MODE,\n" +
                        "   MATURED_ON,\n" +
                        "   INTEREST_RATE,\n" +
                        "   INTEREST_AMOUNT,\n" +
                        "   STATUS,NOTES)\n" +
                        "VALUES\n" +
                        "  (?FD_ACCOUNT_NUMBER,\n" +
                        "   ?PROJECT_ID,\n" +
                        "   ?LEDGER_ID,\n" +
                        "   ?BANK_ID,\n" +
                        "   ?FD_VOUCHER_ID,\n" +
                        "   ?AMOUNT,\n" +
                        "   ?TRANS_TYPE,\n" +
                        "   ?RECEIPT_NO,\n" +
                        "   ?ACCOUNT_HOLDER,\n" +
                        "   ?INVESTMENT_DATE,\n" +
                        "   ?TRANS_MODE,\n" +
                        "   ?MATURED_ON,\n" +
                        "   ?INTEREST_RATE,\n" +
                        "   ?INTEREST_AMOUNT,\n" +
                        "   ?STATUS,?NOTES)";

                        break;
                    }
                case SQLCommand.FDAccount.Update:
                    {
                        query = "UPDATE FD_ACCOUNT\n" +
                        "   SET FD_ACCOUNT_NUMBER = ?FD_ACCOUNT_NUMBER,\n" +
                        "                           PROJECT_ID = ?PROJECT_ID,\n" +
                        "                           LEDGER_ID = ?LEDGER_ID,\n" +
                        "                           BANK_ID = ?BANK_ID,\n" +
                        "                           FD_VOUCHER_ID=?FD_VOUCHER_ID,\n" +
                        "                           AMOUNT = ?AMOUNT,\n" +
                        "                           TRANS_TYPE = ?TRANS_TYPE,\n" +
                        "                           RECEIPT_NO = ?RECEIPT_NO,\n" +
                        "                           ACCOUNT_HOLDER = ?ACCOUNT_HOLDER,\n" +
                        "                           INVESTMENT_DATE = ?INVESTMENT_DATE,\n" +
                        "                           TRANS_MODE = ?TRANS_MODE,\n" +
                        "                           MATURED_ON = ?MATURED_ON,\n" +
                        "                           INTEREST_RATE = ?INTEREST_RATE,\n" +
                        "                           INTEREST_AMOUNT = ?INTEREST_AMOUNT,\n" +
                        "                           STATUS = ?STATUS,\n" +
                        "                           NOTES = ?NOTES\n" +
                        "                           WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";

                        break;
                    }
                case SQLCommand.FDAccount.Delete:
                    {
                        query = "DELETE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchLedgerCurBalance:
                    {
                        query = "SELECT SUM(AMOUNT) AS 'AMOUNT', TRANS_MODE, TRANS_FLAG\n" +
                        "  FROM LEDGER_BALANCE\n" +
                        " WHERE LEDGER_ID =?LEDGER_ID  AND PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP'";


                        //query = "SELECT SUM(LB.AMOUNT) AS 'AMOUNT', FD.TRANS_MODE, FD.TRANS_TYPE\n" +
                        //"  FROM FD_ACCOUNT AS FD\n" +
                        //" INNER JOIN LEDGER_BALANCE AS LB\n" +
                        //"    ON FD.PROJECT_ID = LB.PROJECT_ID\n" +
                        //"   AND FD.LEDGER_ID = LB.LEDGER_ID\n" +
                        //" WHERE FD.LEDGER_ID =?LEDGER_ID\n" +
                        //"   AND FD.PROJECT_ID =?PROJECT_ID\n" +
                        //"   AND TRANS_TYPE = 'OP'";

                        break;
                    }
                case SQLCommand.FDAccount.Fetch:
                    {
                        //query = " SELECT FDA.FD_ACCOUNT_ID,\n" +
                        //        "       FDA.PROJECT_ID,\n" +
                        //        "       FDA.LEDGER_ID,FDA.FD_VOUCHER_ID,\n" +
                        //        "       FDA.BANK_ID,\n" +
                        //        "       FDA.TRANS_TYPE,\n" +
                        //        "       FDA.INVESTMENT_DATE,\n" +
                        //        "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        //        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        //        "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        //        "       MLG.LEDGER_NAME,\n" +
                        //        "       MPR.PROJECT,\n" +
                        //        "       FDA.AMOUNT,FDA.INTEREST_AMOUNT,\n" +
                        //        "       IF(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //        "          IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" +
                        //        "          'Closed',\n" +
                        //        "          'Active') AS CLOSING_STATUS,FDA.NOTES,FDA.STATUS\n" +
                        //        "  FROM FD_ACCOUNT AS FDA\n" +
                        //        "\n" +
                        //        "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        //        "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //        "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //        "                    INTEREST_RATE,\n" +
                        //        "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //        "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //        "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //        "               FROM FD_RENEWAL\n" +
                        //        "              WHERE STATUS = 1\n" +
                        //        "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        //        "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        //        "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        //        "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        //        "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        //        "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        //        "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        //        "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //        " WHERE FDA.STATUS = 1 AND FDA.FD_STATUS=1 \n" +
                        //        "   AND FDA.TRANS_TYPE IN (?TRANS_TYPE)";
                         query="SELECT FDA.FD_ACCOUNT_ID,\n" +
                                "       FDA.PROJECT_ID,\n" + 
                                "       FDA.LEDGER_ID,\n" + 
                                "       FDA.FD_VOUCHER_ID,\n" + 
                                "       FDA.BANK_ID,\n" + 
                                "       FDA.TRANS_TYPE,\n" + 
                                "       FDA.INVESTMENT_DATE,\n" + 
                                "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" + 
                                "       FDA.FD_ACCOUNT_NUMBER,\n" + 
                                "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" + 
                                "       MLG.LEDGER_NAME,\n" + 
                                "       MPR.PROJECT,\n" + 
                                //"       CASE\n" + 
                                //"         WHEN FDR.RENEWAL_TYPE = 'ACI' THEN\n" + 
                                //"          FDA.AMOUNT + FDR.ACCUMULATED_INTEREST_AMOUNT\n" + 
                                //"         ELSE\n" + 
                                //"          FDA.AMOUNT\n" + 
                                //"       END AS AMOUNT,\n" + 
                                "        FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) - "+
                                "        IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT, "+
                                "       FDA.INTEREST_AMOUNT,\n" + 
                                "       IF(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" + 
                                "          IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" + 
                                "          'Closed',\n" + 
                                "          'Active') AS CLOSING_STATUS,\n" + 
                                "       FDA.NOTES,\n" + 
                                "       FDR.RENEWAL_TYPE,\n" + 
                                "       FDA.STATUS\n" + 
                                "  FROM FD_ACCOUNT AS FDA\n" + 
                                "\n" + 
                                "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" + 
                                "                    RENEWAL_TYPE,\n" + 
                                "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" + 
                                "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" + 
                                "                    INTEREST_RATE,\n" + 
                                "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" + 
                                "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" + 
                                "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" + 
                                "               FROM FD_RENEWAL\n" + 
                                "              WHERE STATUS = 1\n" + 
                                "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" + 
                                "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" + 
                                "  LEFT JOIN MASTER_BANK AS MBK\n" + 
                                "    ON FDA.BANK_ID = MBK.BANK_ID\n" + 
                                "  LEFT JOIN MASTER_PROJECT MPR\n" + 
                                "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" + 
                                "  LEFT JOIN MASTER_LEDGER MLG\n" + 
                                "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" + 
                                " WHERE FDA.STATUS = 1\n" + 
                                "   AND FDA.FD_STATUS = 1\n" + 
                                "   AND FDA.TRANS_TYPE IN (?TRANS_TYPE)";

                        break;
                    }
                case SQLCommand.FDAccount.FetchFDById:
                    {
                        query = "SELECT FD.LEDGER_ID,FD.FD_ACCOUNT_ID,\n" +
                                "       FD.PROJECT_ID,\n" +
                                "       FD.BANK_ID,\n" +
                                "       FD.FD_VOUCHER_ID,\n" +
                                "       VMS.VOUCHER_NO,\n" +
                                "       MP.PROJECT,\n" +
                                "       FD.INVESTMENT_DATE,\n" +
                                "       MATURED_ON,\n" +
                                "       FD.FD_ACCOUNT_NUMBER, \n" +
                                "       ML.LEDGER_CODE,\n" +
                                "       ML.LEDGER_NAME AS 'FD_LEDGER_NAME',\n" +
                                "       FD.AMOUNT,\n" +
                                "       FD.INTEREST_RATE,\n" +
                                "       FD.INTEREST_AMOUNT,\n" +
                                "       FD.TRANS_MODE,\n" +
                                "       FD.TRANS_TYPE,\n" +
                                "       FD.RECEIPT_NO,\n" +
                                "       FD.NOTES,\n" +
                                "       FD.ACCOUNT_HOLDER,\n" +
                                "       CONCAT(MB.BRANCH, ' / ', MB.BANK) AS 'BANK'\n" +
                                "  FROM FD_ACCOUNT AS FD\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_PROJECT AS MP\n" +
                                "    ON FD.PROJECT_ID = MP.PROJECT_ID\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS AS VMS\n" +
                                "    ON FD.FD_VOUCHER_ID=VMS.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_BANK AS MB\n" +
                                "    ON FD.BANK_ID = MB.BANK_ID\n" +
                                " WHERE FD.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID";
                        break;
                    }

                case SQLCommand.FDAccount.FetchLedgerByProject:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_NAME\n" +
                        "  FROM MASTER_LEDGER AS ML\n" +
                        " INNER JOIN PROJECT_LEDGER AS PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " WHERE PROJECT_ID = ?PROJECT_ID AND ML.GROUP_ID=14 AND ML.LEDGER_SUB_TYPE='FD'";
                        break;
                    }
                case SQLCommand.FDAccount.FetchProjectId:
                    {
                        query = "SELECT TRANS_TYPE FROM FD_ACCOUNT WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID ORDER BY FD_ACCOUNT_ID ASC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchLedgerBalance:
                    {
                        query = "SELECT IFNULL(SUM(AMOUNT),0) AS 'AMOUNT',TRANS_MODE FROM FD_ACCOUNT WHERE PROJECT_ID=?PROJECT_ID AND LEDGER_ID=?LEDGER_ID AND TRANS_TYPE IN('Op','IN') AND STATUS=1 ";
                        break;
                    }
                case SQLCommand.FDAccount.DeleteFDAcountDetails:
                    {
                        query = "UPDATE FD_ACCOUNT SET STATUS=0 WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchFDRenewalById:
                    {
                        query = "SELECT FD_RENEWAL_ID,VMT.VOUCHER_NO,FR.FD_VOUCHER_ID,FR.FD_INTEREST_VOUCHER_ID,\n" +
                                "       FD_ACCOUNT_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       RECEIPT_NO,\n" +
                                "       INTEREST_RATE,\n" +
                                "       INTEREST_AMOUNT,\n" +
                                "       MATURITY_DATE,\n" +
                                "       INTEREST_LEDGER_ID,\n" +
                                "       BANK_LEDGER_ID,\n" +
                                "       RENEWAL_TYPE\n" +
                                " FROM FD_RENEWAL AS FR\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS  VMT ON\n" +
                                " FR.FD_VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE FR.STATUS =1 AND FR.RENEWAL_TYPE NOT IN('WDI') AND FD_ACCOUNT_ID IN(?FD_ACCOUNT_ID) ORDER BY MATURITY_DATE DESC";

                        break;
                    }
                case SQLCommand.FDAccount.GetLastFDRenewalDate:
                    {
                        query = @" SELECT RENEWAL_DATE,MATURITY_DATE,RECEIPT_NO,FD_ACCOUNT_ID,INTEREST_AMOUNT,INTEREST_RATE,RENEWAL_TYPE FROM
                                    FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID ORDER BY RENEWAL_DATE ASC";
                        break;
                    }
                case SQLCommand.FDAccount.FetchAccumulatedAmount:
                    {
                        query = "SELECT IFNULL(SUM(INTEREST_AMOUNT),0) AS 'AMOUNT' FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID  AND RENEWAL_TYPE='ACI'";
                        break;
                    }
                case SQLCommand.FDAccount.FetchRenewalByRenewalId:
                    {

                        query = "SELECT FDR.FD_RENEWAL_ID,FDR.FD_INTEREST_VOUCHER_ID,\n" +
                        "       FDR.FD_ACCOUNT_ID,\n" +
                        "       FDA.PROJECT_ID,\n" +
                        "       MP.PROJECT,\n" +
                        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        "       FDA.LEDGER_ID,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       FDA.BANK_ID,\n" +
                        "       FDA.RECEIPT_NO,\n" +
                            //  "       FDA.INTEREST_AMOUNT    AS 'STARTING_INT_AMOUNT',\n" +
                            // "       FDA.INTEREST_RATE      AS 'STARTING_INT_RATE',\n" +
                        "       FDR.INTEREST_LEDGER_ID,\n" +
                        "       FDR.BANK_LEDGER_ID,\n" +
                        "       FDR.INTEREST_AMOUNT ,\n" +
                        "       FDR.PRINICIPAL_AMOUNT,\n" +
                        "       FDR.INTEREST_RATE ,\n" +
                        "       FDR.RENEWAL_DATE,\n" +
                        "       FDR.MATURITY_DATE,FDR.RENEWAL_TYPE,FDA.NOTES\n" +
                        "  FROM FD_ACCOUNT AS FDA\n" +
                        " INNER JOIN FD_RENEWAL AS FDR\n" +
                        "    ON FDR.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                        " INNER JOIN MASTER_LEDGER AS ML\n" +
                        "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                        " WHERE FDR.FD_RENEWAL_ID = ?FD_RENEWAL_ID;";

                        break;
                    }
                case SQLCommand.FDAccount.UpdateFDStatus:
                    {
                        query = "UPDATE FD_ACCOUNT SET FD_STATUS=0 WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID";
                        break;
                    }

                case SQLCommand.FDAccount.FetchVoucherId:
                    {
                        query = "SELECT FD_VOUCHER_ID,FD_INTEREST_VOUCHER_ID,RENEWAL_TYPE FROM FD_RENEWAL WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND RENEWAL_TYPE NOT IN('WDI') AND STATUS NOT IN (0)";
                        break;
                    }
                case SQLCommand.FDAccount.FetchAccountIdByVoucherId:
                    {
                        query = @"SELECT FR.FD_ACCOUNT_ID,PROJECT_ID,FD_STATUS FROM  FD_ACCOUNT FA
                                    LEFT JOIN FD_RENEWAL FR
                                    ON FA.FD_ACCOUNT_ID=FR.FD_ACCOUNT_ID  WHERE FR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FDAccount.FetchRenewalDetailsById:
                    {
                        //query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                        //"       VMT.VOUCHER_NO,\n" +
                        //"       FDR.FD_RENEWAL_ID,\n" +
                        //"       FDA.PROJECT_ID,\n" +
                        //"       FDR.FD_VOUCHER_ID,FDR.FD_INTEREST_VOUCHER_ID,\n" +
                        //"       FDR.FD_VOUCHER_ID,\n" +
                        //"       FDR.INTEREST_LEDGER_ID,\n" +
                        //"       FDR.BANK_LEDGER_ID,\n" +
                        //"       FDA.LEDGER_ID,\n" +
                        //"       FDA.BANK_ID,\n" +
                        //"       FDA.TRANS_TYPE,FDR.RECEIPT_NO,FDA.INTEREST_RATE,\n" +
                        //"       FDR.RENEWAL_TYPE,\n" +
                        //"       FDA.INVESTMENT_DATE,\n" +
                        //"       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        //"       FDA.FD_ACCOUNT_NUMBER,\n" +
                        //"       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        //"       MLG.LEDGER_NAME,\n" +
                        //"       MPR.PROJECT,\n" +
                        //"       FDA.AMOUNT,\n" +
                        //"       FDA.INTEREST_AMOUNT,\n" +
                        //"       IF(FDA.AMOUNT + IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //"          IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) = 0,\n" +
                        //"          'Closed',\n" +
                        //"          'Active') AS CLOSING_STATUS,\n" +
                        //"       FDA.NOTES,\n" +
                        //"       FDA.STATUS\n" +
                        //"  FROM FD_ACCOUNT AS FDA\n" +
                        //"\n" +
                        //"  LEFT JOIN (SELECT FD_RENEWAL_ID,FD_INTEREST_VOUCHER_ID,\n" +
                        //"                    FD_ACCOUNT_ID,\n" +
                        //"                    FD_VOUCHER_ID,\n" +
                        //"                    INTEREST_LEDGER_ID,\n" +
                        //"                    BANK_LEDGER_ID,RENEWAL_TYPE,RECEIPT_NO,\n" +
                        //"                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //"                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //"                    INTEREST_RATE,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //"                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //"                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //"               FROM FD_RENEWAL\n" +
                        //"              WHERE STATUS = 1\n" +
                        //"              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        //"    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        //"  LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        //"    ON FDR.FD_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //"  LEFT JOIN MASTER_BANK AS MBK\n" +
                        //"    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        //"  LEFT JOIN MASTER_PROJECT MPR\n" +
                        //"    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        //"  LEFT JOIN MASTER_LEDGER MLG\n" +
                        //"    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //" WHERE FDA.STATUS = 1\n" +
                        //"   AND FDA.FD_STATUS = 1\n" +
                        //"   AND FDA.FD_ACCOUNT_ID =?FD_ACCOUNT_ID AND FDR.FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID";

                        query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       FDR.FD_RENEWAL_ID,\n" +
                        "       FDA.PROJECT_ID,\n" +
                        "       FDR.FD_VOUCHER_ID,\n" +
                        "       FDR.FD_INTEREST_VOUCHER_ID,\n" +
                        "       FDR.FD_VOUCHER_ID,\n" +
                        "       FDR.INTEREST_LEDGER_ID,\n" +
                        "       VT.AMOUNT AS 'INTEREST_AMOUNT',\n" +
                        "       FDR.BANK_LEDGER_ID,\n" +
                        "       FDA.LEDGER_ID,\n" +
                        "       FDA.BANK_ID,\n" +
                        "       FDA.TRANS_TYPE,\n" +
                        "       FDR.RECEIPT_NO,\n" +
                        "       FDA.INTEREST_RATE,\n" +
                        "       FDR.RENEWAL_DATE,\n" +
                        "       FDR.RENEWAL_TYPE,\n" +
                        "       FDA.INVESTMENT_DATE,\n" +
                        "       IFNULL(FDR.MATURITY_DATE, FDA.MATURED_ON) AS MATURITY_DATE,\n" +
                        "       FDA.FD_ACCOUNT_NUMBER,\n" +
                        "       CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        "       MLG.LEDGER_NAME,\n" +
                        "       MPR.PROJECT,\n" +
                        "       FDA.AMOUNT,\n" +
                        "       FDA.INTEREST_AMOUNT,\n" +
                        "       FDA.NOTES,\n" +
                        "       FDA.STATUS\n" +
                        "  FROM FD_ACCOUNT AS FDA\n" +
                        "  LEFT JOIN FD_RENEWAL AS FDR\n" +
                        "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        "  LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                        "    ON FDR.FD_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        "  LEFT JOIN VOUCHER_TRANS AS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        " WHERE FDA.STATUS = 1\n" +
                        "   AND FDA.FD_STATUS = 1\n" +
                        "   AND FDA.FD_ACCOUNT_ID = ?FD_ACCOUNT_ID\n" +
                        "   AND FDR.FD_INTEREST_VOUCHER_ID = ?FD_INTEREST_VOUCHER_ID\n" +
                        " GROUP BY VMT.VOUCHER_ID";

                        break;
                    }
                case SQLCommand.FDAccount.FetchFDStatus:
                    {
                        query = "SELECT FD_STATUS FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID=?FD_ACCOUNT_ID AND STATUS=1";
                        break;
                    }

            }
            return query;
        }
        #endregion Bank SQL
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherTransSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.VoucherTransDetails).FullName)
            {
                query = GetVoucherTransactionSQL();
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
        private string GetVoucherTransactionSQL()
        {
            string query = "";
            SQLCommand.VoucherTransDetails sqlCommandId = (SQLCommand.VoucherTransDetails)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherTransDetails.Add:
                    {
                        query = "INSERT INTO VOUCHER_TRANS ( " +
                               "VOUCHER_ID, " +
                               "SEQUENCE_NO, " +
                               "LEDGER_ID, " +
                               "AMOUNT," +
                               "TRANS_MODE," +
                               "LEDGER_FLAG," +
                               "CHEQUE_NO," +
                               "MATERIALIZED_ON," +
                               "STATUS ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?SEQUENCE_NO, " +
                               "?LEDGER_ID, " +
                               "?AMOUNT," +
                               "?TRANS_MODE," +
                               "?LEDGER_FLAG," +
                               "?CHEQUE_NO," +
                               "?MATERIALIZED_ON," +
                               "?STATUS)";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.Delete:
                    {
                        //    query = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID ";
                        query = " DELETE FROM VOUCHER_TRANS  " +
                                " WHERE BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.DeleteFDRenewal:
                    {
                        query = " DELETE FROM FD_RENEWAL  " +
                                " WHERE BRANCH_ID=?BRANCH_OFFICE_ID";

                        break;
                    }
                case SQLCommand.VoucherTransDetails.DeleteFDAccount:
                    {
                        query = " DELETE FROM FD_ACCOUNT  " +
                                " WHERE BRANCH_ID=?BRANCH_OFFICE_ID ";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchAll:
                    {
                        query = " SELECT VOUCHER_ID, ML.LEDGER_NAME ,VT.AMOUNT ,CASE TRANS_MODE WHEN 'CR' THEN 'Credit' ELSE 'Debit' END AS TRANSMODE," +
                               "LEDGER_FLAG, MB.ACCOUNT_NUMBER, " +
                               " CHEQUE_NO ,MATERIALIZED_ON FROM VOUCHER_TRANS AS VT " +
                               " INNER JOIN MASTER_LEDGER AS ML ON VT.LEDGER_ID =ML.LEDGER_ID " +
                               " LEFT JOIN MASTER_BANK_ACCOUNT AS MB ON " +
                               " ML.BANK_ACCOUNT_ID=MB.BANK_ACCOUNT_ID;";
                        break;
                    }

                case SQLCommand.VoucherTransDetails.FetchTransactionDetails:
                    {
                        query = " SELECT VT.VOUCHER_ID, ML.LEDGER_NAME,VT.LEDGER_ID ,CASE TRANS_MODE WHEN 'CR' THEN 'Credit' ELSE 'Debit' END AS TRANSMODE," +
                              " CASE TRANS_MODE WHEN 'CR' THEN VT.AMOUNT ELSE 0.00 END AS 'CREDIT'," +
                              " CASE TRANS_MODE WHEN 'DR' THEN VT.AMOUNT ELSE 0.00 END AS 'DEBIT'," +
                              " LEDGER_FLAG, MB.ACCOUNT_NUMBER, " +
                              " CHEQUE_NO ,DATE_FORMAT(MATERIALIZED_ON,'%d/%m/%Y') AS MATERIALIZED_ON FROM VOUCHER_TRANS AS VT " +
                              " INNER JOIN MASTER_LEDGER AS ML ON VT.LEDGER_ID =ML.LEDGER_ID " +
                              " LEFT JOIN MASTER_BANK_ACCOUNT AS MB ON " +
                              " ML.LEDGER_ID = MB.LEDGER_ID WHERE FIND_IN_SET(VT.VOUCHER_ID,?VOUCHER_ID)>0 AND VT.BRANCH_ID=?BRANCH_OFFICE_ID AND VT.LOCATION_ID=?LOCATION_ID " +
                              "GROUP BY VT.BRANCH_ID,VT.LOCATION_ID,VT.VOUCHER_ID,VT.SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchJournalDetailById:
                    {
                        query = "SELECT VMT.VOUCHER_ID,\n" +
                      "       VMT.VOUCHER_NO,\n" +
                      "       VMT.VOUCHER_DATE,\n" +
                      "       ML.LEDGER_NAME,\n" +
                      "       ML.LEDGER_ID ,\n" +
                      "       VMT.NARRATION,\n" +
                      "       CASE\n" +
                      "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                      "          IFNULL(AMOUNT, 0)\n" +
                      "       END AS DEBIT,\n" +
                      "       CASE\n" +
                      "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                      "          IFNULL(AMOUNT, 0)\n" +
                      "       END AS CREDIT\n" +
                      "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                      " INNER JOIN VOUCHER_TRANS VT\n" +
                      "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                      " INNER JOIN MASTER_LEDGER ML\n" +
                      "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                      " WHERE \n" +
                      "    VMT.VOUCHER_ID =?VOUCHER_ID" +
                      "   AND VMT.VOUCHER_TYPE = 'JN' ORDER BY VMT.VOUCHER_NO,VT.LEDGER_ID";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchTransDetails:
                    {
                        query = "SELECT VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, AMOUNT, TRANS_MODE, LEDGER_FLAG, CHEQUE_NO, " +
                                "MATERIALIZED_ON, STATUS,'' AS LEDGER_BALANCE,0 AS TEMP_AMOUNT " +
                                "FROM voucher_trans WHERE VOUCHER_ID=?VOUCHER_ID AND TRANS_MODE=?TRANS_MODE";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchCashBankDetails:
                    {
                        query = "SELECT VOUCHER_ID, SEQUENCE_NO, LEDGER_ID, AMOUNT, TRANS_MODE, LEDGER_FLAG, CHEQUE_NO, " +
                                 "MATERIALIZED_ON, STATUS,'' AS LEDGER_BALANCE,0 AS TEMP_AMOUNT " +
                                 "FROM voucher_trans WHERE VOUCHER_ID=?VOUCHER_ID AND TRANS_MODE=?TRANS_MODE";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchReceipts:
                    {
                        query = "SELECT \n" +
                                "       LG.LEDGER_GROUP,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       IFNULL(SUM(VT.AMOUNT), 0) AS TEMP_AMOUNT,\n" +
                                "       '' AS AMOUNT\n" +
                                "\n" +
                                "  FROM MASTER_LEDGER_GROUP LG\n" +
                                "\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "  AND MT.BRANCH_ID = VT.BRANCH_ID\n" +
                                "AND MT.LOCATION_ID=VT.LOCATION_ID\n" +
                            //"\n" +
                            //"  LEFT JOIN PROJECT_BRANCH PB\n" +
                            //"    ON PB.BRANCH_ID = MT.BRANCH_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                "    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                                " WHERE MT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "  { AND vt.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                                "  { AND MIP.CUSTOMERID IN (?LEGAL_ENTITY_ID) }\n" +
                                "\n" +
                                "   AND MT.VOUCHER_TYPE in ('RC', 'PY')\n" +
                                "   AND VT.TRANS_MODE = 'CR'\n" +
                                "   AND MT.STATUS = 1\n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND LG.GROUP_ID NOT IN (12, 13)\n" +
                                " GROUP BY VT.LEDGER_ID ORDER BY ML.LEDGER_NAME";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchPayment:
                    {
                        query = "SELECT \n" +
                                "       LG.LEDGER_GROUP,\n" +
                                "       ML.LEDGER_NAME,\n" +
                               "       IFNULL(SUM(VT.AMOUNT), 0) AS TEMP_AMOUNT,\n" +
                                "       '' AS AMOUNT\n" +
                                "\n" +
                                "  FROM MASTER_LEDGER_GROUP LG\n" +
                                "\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "  AND MT.BRANCH_ID = VT.BRANCH_ID\n" +
                                "  AND MT.LOCATION_ID=VT.LOCATION_ID\n" +
                            //"\n" + 
                            //"  LEFT JOIN PROJECT_BRANCH PB\n" + 
                            //"    ON PB.BRANCH_ID = MT.BRANCH_ID\n" + 
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                "    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                                " WHERE MT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " { AND vt.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                                "  { AND MIP.CUSTOMERID IN (?LEGAL_ENTITY_ID) }\n" +
                                "\n" +
                                "   AND MT.VOUCHER_TYPE in ('RC', 'PY')\n" +
                                "   AND VT.TRANS_MODE = 'DR'\n" +
                                "   AND MT.STATUS = 1\n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND LG.GROUP_ID NOT IN (12, 13)\n" +
                                " GROUP BY VT.LEDGER_ID ORDER BY ML.LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.FetchLedgerSummary:
                    {
                        query = "SELECT SUM(FFNL.AMOUNT) AS AMOUNT,BRANCH_OFFICE_NAME,BRANCH_OFFICE_CODE FROM (\n" +
                          "SELECT\n" +
                              "\n" +
                              " @OPAMT := IF(@PRVBNAME = BRANCH_OFFICE_NAME AND @PRVMNAME <> MONTH_NAME,\n" +
                              "    @OPAMT,\n" +
                              "    OP_AMT) AS FNL,\n" +
                              " MONTH_NAME,\n" +
                              " BRANCH_OFFICE_NAME,\n" +
                              " @PRVBNAME := BRANCH_OFFICE_NAME,\n" +
                              " @PRVMNAME := MONTH_NAME,\n" +
                              " RECEIPT,\n" +
                              " PAYMENT,\n" +
                              " ((@OPAMT + RECEIPT) - PAYMENT) AS AMOUNT,BRANCH_OFFICE_CODE,\n" +
                              " @OPAMT := (@OPAMT + RECEIPT) - PAYMENT\n" +
                              "  FROM (SELECT SUM(RECEIPT) AS RECEIPT,\n" +
                              "               SUM(PAYMENT) AS PAYMENT,\n" +
                              "               BRANCH_OFFICE_NAME,\n" +
                              "               MONTH_NAME,\n" +
                              "               OP_AMT,\n" +
                              "               MONTH_YEAR,BRANCH_OFFICE_CODE\n" +
                              "\n" +
                              "          FROM (SELECT YEAR(MONTH_YEAR) AS 'YEAR',\n" +
                              "                       MONTH(MONTH_YEAR) AS 'MONTH',\n" +
                              "                       CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3),\n" +
                              "                              '-',\n" +
                              "                              YEAR(MONTH_YEAR)) AS MONTH_NAME,\n" +
                              "                       MONTH_YEAR,\n" +
                              "                       VOUCHER_DATE,\n" +
                              "                       CASE\n" +
                              "                         WHEN MONTH(MONTH_YEAR) = MONTH(VOUCHER_DATE) THEN\n" +
                              "                          RECEIPT\n" +
                              "                         ELSE\n" +
                              "                          0\n" +
                              "                       END AS RECEIPT,\n" +
                              "                       CASE\n" +
                              "                         WHEN MONTH(MONTH_YEAR) = MONTH(VOUCHER_DATE) THEN\n" +
                              "                          PAYMENT\n" +
                              "                         ELSE\n" +
                              "                          0\n" +
                              "                       END AS PAYMENT,\n" +
                              "                       OP_AMT,\n" +
                              "                       BRANCH_OFFICE_NAME,\n" +
                              "                       BRANCH_PART_CODE AS BRANCH_OFFICE_CODE,\n" +
                              "                       BRANCH_ID\n" +
                              "\n" +
                              "                  FROM (SELECT *\n" +
                              "                          FROM (SELECT *\n" +
                              "                                  FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                              "                                                DAYOFMONTH(?DATE_FROM) - 1 DAY) +\n" +
                              "                                               INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                              "                                               NO_OF_MONTH\n" +
                              "                                          FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH\n" +
                              "                                                  FROM (SELECT 1 UNION\n" +
                              "                                                                SELECT 2 UNION\n" +
                              "                                                                        SELECT 3 UNION\n" +
                              "                                                                                SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                                        ) AS T1,\n" +
                              "                                                       (SELECT 1 UNION\n" +
                              "                                                                SELECT 2 UNION\n" +
                              "                                                                        SELECT 3 UNION\n" +
                              "                                                                                SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                                        ) AS T2,\n" +
                              "                                                       (SELECT 1 UNION\n" +
                              "                                                                SELECT 2 UNION\n" +
                              "                                                                        SELECT 3 UNION\n" +
                              "                                                                                SELECT 4\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "\n" +
                              "                                                        ) AS T3,\n" +
                              "                                                       (SELECT @rownum := -1) AS T0) D1) D2\n" +
                              "                                 WHERE D2.MONTH_YEAR BETWEEN ?DATE_FROM  AND ?DATE_TO) AS T1\n" +
                              "\n" +
                              "                          JOIN (SELECT VMT.VOUCHER_TYPE,\n" +
                              "                                      VMT.VOUCHER_DATE,\n" +
                              "                                      IF(VOUCHER_TYPE IN ('RC') OR\n" +
                              "                                         (VOUCHER_TYPE = 'CN' AND\n" +
                              "                                         TRANS_MODE = 'DR'),\n" +
                              "                                         AMOUNT,\n" +
                              "                                         0) AS RECEIPT,\n" +
                              "                                      IF(VOUCHER_TYPE IN ('PY') OR\n" +
                              "                                         (VOUCHER_TYPE = 'CN' AND\n" +
                              "                                         TRANS_MODE = 'CR'),\n" +
                              "                                         AMOUNT,\n" +
                              "                                         0) AS PAYMENT,\n" +
                              "                                      OP_AMT,\n" +
                              "                                      VT.TRANS_MODE,\n" +
                              "                                      VMT.BRANCH_ID,\n" +
                              "                                      BO.BRANCH_OFFICE_CODE,\n" +
                              "                                      BO.BRANCH_OFFICE_NAME,\n" +
                              "                                      BRANCH_PART_CODE\n" +
                              "                                 FROM VOUCHER_MASTER_TRANS VMT\n" +
                              "                                INNER JOIN VOUCHER_TRANS VT\n" +
                              "                                   ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              "                                  AND VMT.BRANCH_ID = VT.BRANCH_ID\n" +
                              "                                INNER JOIN MASTER_LEDGER ML\n" +
                              "                                   ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                                INNER JOIN BRANCH_OFFICE BO\n" +
                              "                                   ON BO.BRANCH_OFFICE_ID = VT.BRANCH_ID\n" +
                              "                                INNER JOIN (SELECT SUM(DR) AS RECEIPT,\n" +
                              "                                                  SUM(CR) AS PAYMENT,\n" +
                              "                                                  CASE\n" +
                              "                                                    WHEN SUM(DR) > SUM(CR) THEN\n" +
                              "                                                     SUM(DR) - SUM(CR)\n" +
                              "                                                    ELSE\n" +
                              "                                                     - (SUM(CR) - SUM(DR))\n" +
                              "                                                  END AS OP_AMT,\n" +
                              "                                                  BRANCH_ID\n" +
                              "\n" +
                              "                                             FROM (SELECT IF(TRANS_MODE = 'DR',\n" +
                              "                                                             SUM(AMOUNT),\n" +
                              "                                                             0) AS DR,\n" +
                              "                                                          IF(TRANS_MODE = 'CR',\n" +
                              "                                                             SUM(AMOUNT),\n" +
                              "                                                             0) AS CR,\n" +
                              "                                                          LB.BRANCH_ID\n" +
                              "\n" +
                              "                                                     FROM LEDGER_BALANCE LB\n" +
                              "\n" +
                              "                                                    INNER JOIN (SELECT MAX(BALANCE_DATE) AS BAL_DATE,\n" +
                              "                                                                      BRANCH_ID AS BID\n" +
                              "                                                                 FROM LEDGER_BALANCE\n" +
                              "                                                                WHERE LEDGER_ID IN (?LEDGER_ID)\n" +
                              "                                                                  AND BALANCE_DATE <?DATE_FROM\n" +
                              "                                                                GROUP BY BRANCH_ID) AS TT1\n" +
                              "                                                       ON LB.BRANCH_ID = TT1.BID\n" +
                              "                                                      AND LB.BALANCE_DATE = TT1.BAL_DATE\n" +
                              "                                                      AND LEDGER_ID IN (?LEDGER_ID)\n" +
                              "                                                    GROUP BY LB.BRANCH_ID,\n" +
                              "                                                             TRANS_MODE) AS OPBAL\n" +
                              "                                            GROUP BY BRANCH_ID) AS OP1\n" +
                              "                                   ON VMT.BRANCH_ID = OP1.BRANCH_ID\n" +
                              "                                  AND VT.BRANCH_ID = OP1.BRANCH_ID\n" +
                              "                                WHERE VMT.VOUCHER_DATE BETWEEN ?DATE_FROM  AND ?DATE_TO\n" +
                              "                                  AND VMT.STATUS = 1\n" +
                              "                                  AND VT.LEDGER_ID IN (?LEDGER_ID)) AS T) AS T1) AS FNL\n" +
                              "         WHERE RECEIPT > 0\n" +
                              "            OR PAYMENT > 0\n" +
                              "         GROUP BY BRANCH_ID,\n" +
                              "                  YEAR(VOUCHER_DATE),\n" +
                              "                  MONTH(VOUCHER_DATE),\n" +
                              "                  YEAR(MONTH_YEAR),\n" +
                              "                  MONTH(MONTH_YEAR)) AS FINALSM,\n" +
                              "       (SELECT @OPAMT := 0) AS X,\n" +
                              "       (SELECT @PRVBNAME := NULL) AS Y,\n" +
                              "       (SELECT @PRVMNAME := NULL) AS Z\n" +
                              " ORDER BY BRANCH_OFFICE_NAME, YEAR(MONTH_YEAR), MONTH(MONTH_YEAR)) as FFNL \n" +
                              "GROUP BY BRANCH_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.TransOPBalance:
                    {
                        query = "SELECT ML.LEDGER_ID AS 'ID', ML.LEDGER_NAME AS 'LEDGER_NAME', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_DR, " +
                                "SUM(CASE WHEN LB2.TRANS_MODE = 'CR' THEN LB2.AMOUNT ELSE 0 END) AS AMOUNT_CR, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' " +
                                "               THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "     THEN 'DR' ELSE 'CR' END AS TRANSMODE " +
                                "FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG " +
                                "ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN " +
                                "     (SELECT LB.BALANCE_DATE, LB.BRANCH_ID, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "      FROM LEDGER_BALANCE AS LB " +
                                "      LEFT JOIN (SELECT LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "                 FROM LEDGER_BALANCE LBA " +
                                "                 WHERE 1 = 1 {AND LBA.BALANCE_DATE <= ?BALANCE_DATE} " +
                                "                 GROUP BY LBA.BRANCH_ID, LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                "      ON LB.BRANCH_ID = LB1.BRANCH_ID " +
                                "      AND LB.PROJECT_ID = LB1.PROJECT_ID " +
                                "      AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                "      WHERE LB.BRANCH_ID IN (?BRANCH_OFFICE_ID) AND LB.PROJECT_ID IN (?PROJECT_ID) " +
                                "      AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                                "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                                "WHERE LG.GROUP_ID IN (?GROUP_ID) " +
                                "{AND ML.LEDGER_ID in(?LEDGER_ID)} AND ML.STATUS=0 " +
                                "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP";
                        break;
                    }
                case SQLCommand.VoucherTransDetails.TransFDCBalance:
                    {
                        //query = "SELECT FDA.FD_ACCOUNT_NUMBER AS 'LEDGER_NAME',FDA.TRANS_MODE AS 'TRANSMODE',\n" +
                        //          "       --    CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
                        //          "       --  MPR.PROJECT,MPR.PROJECT_ID,\n" +
                        //          "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //          "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
                        //          "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //          "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT\n" +
                        //          "\n" +
                        //          "  FROM FD_ACCOUNT AS FDA\n" +
                        //          "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        //          "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //          "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //          "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //          "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //          "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //          "               FROM FD_RENEWAL\n" +
                        //          "              WHERE STATUS = 1\n" +
                        //          "                AND RENEWAL_DATE < ?BALANCE_DATE\n" +
                        //          "              GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
                        //          "    ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
                        //          "\n" +
                        //          "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
                        //          "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
                        //          "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
                        //          "                    INTEREST_RATE,\n" +
                        //          "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
                        //          "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
                        //          "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
                        //          "               FROM FD_RENEWAL\n" +
                        //          "              WHERE STATUS = 1\n" +
                        //          "                AND RENEWAL_DATE BETWEEN ?BALANCE_DATE AND ?BALANCE_DATE\n" +
                        //          "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
                        //          "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
                        //          "  LEFT JOIN MASTER_BANK AS MBK\n" +
                        //          "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
                        //          "  LEFT JOIN MASTER_PROJECT MPR\n" +
                        //          "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
                        //          "  LEFT JOIN MASTER_LEDGER MLG\n" +
                        //          "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
                        //          " WHERE FDA.STATUS = 1\n" +
                        //          "  { AND FDA.INVESTMENT_DATE <= ?BALANCE_DATE\n}" +
                        //          "   AND FDA.PROJECT_ID IN (?PROJECT_ID) AND FDA.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                        //          "   AND FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
                        //          "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) >= 0";

                        query = "SELECT FDA.FD_ACCOUNT_NUMBER AS 'LEDGER_NAME',\n" +
              "       --    CONCAT(MBK.BANK, ' (', MBK.BRANCH, ')') AS BANK,\n" +
              "       --  MPR.PROJECT,MPR.PROJECT_ID,\n" +
              "       FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
              "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
              "       IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
              "       IFNULL(FDR.WITHDRAWAL_AMOUNT, 0) AS AMOUNT,\n" +
              "       CASE\n" +
               "         WHEN (FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
               "              IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) +\n" +
               "              IFNULL(FDR.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
               "              IFNULL(FDR.WITHDRAWAL_AMOUNT, 0)) >= 0 THEN\n" +
               "          'DR'\n" +
               "         ELSE\n" +
               "          'CR'\n" +
               "       END AS 'TRANSMODE'\n" +
              "\n" +
              "  FROM FD_ACCOUNT AS FDA\n" +
              "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
              "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
              "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
              "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
              "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
              "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
              "               FROM FD_RENEWAL AS FDR \n" +
              "              WHERE STATUS = 1 AND FDR.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
              "                AND RENEWAL_DATE < ?BALANCE_DATE\n" +
              "              GROUP BY FD_ACCOUNT_ID) AS FDRO\n" +
              "    ON FDA.FD_ACCOUNT_ID = FDRO.FD_ACCOUNT_ID\n" +
              "\n" +
              "  LEFT JOIN (SELECT FD_ACCOUNT_ID,\n" +
              "                    MAX(MATURITY_DATE) AS MATURITY_DATE,\n" +
              "                    MAX(RENEWAL_DATE) AS RENEWAL_DATE,\n" +
              "                    INTEREST_RATE,\n" +
              "                    SUM(IF(RENEWAL_TYPE = 'ACI', 0, INTEREST_AMOUNT)) AS INTEREST_AMOUNT,\n" +
              "                    SUM(IF(RENEWAL_TYPE = 'ACI', INTEREST_AMOUNT, 0)) AS ACCUMULATED_INTEREST_AMOUNT,\n" +
              "                    SUM(WITHDRAWAL_AMOUNT) AS WITHDRAWAL_AMOUNT\n" +
              "               FROM FD_RENEWAL AS FDR\n" +
              "              WHERE STATUS = 1 AND FDR.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
              "                AND RENEWAL_DATE BETWEEN ?BALANCE_DATE AND ?BALANCE_DATE\n" +
              "              GROUP BY FD_ACCOUNT_ID) AS FDR\n" +
              "    ON FDA.FD_ACCOUNT_ID = FDR.FD_ACCOUNT_ID\n" +
              "  LEFT JOIN MASTER_BANK AS MBK\n" +
              "    ON FDA.BANK_ID = MBK.BANK_ID\n" +
              "  LEFT JOIN MASTER_PROJECT MPR\n" +
              "    ON FDA.PROJECT_ID = MPR.PROJECT_ID\n" +
              "  LEFT JOIN MASTER_LEDGER MLG\n" +
              "    ON FDA.LEDGER_ID = MLG.LEDGER_ID\n" +
              " WHERE FDA.STATUS = 1\n" +
              "  { AND FDA.INVESTMENT_DATE <= ?BALANCE_DATE\n}" +
              "   AND FDA.PROJECT_ID IN(?PROJECT_ID) AND FDA.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
              "   AND FDA.AMOUNT + IFNULL(FDRO.ACCUMULATED_INTEREST_AMOUNT, 0) -\n" +
              "       IFNULL(FDRO.WITHDRAWAL_AMOUNT, 0) != 0";// changed by sugan --to load the negative FDs
                        break;
                    }
                case SQLCommand.VoucherTransDetails.TransCBBalance:
                    {
                        query = "SELECT ML.LEDGER_ID AS 'ID', LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP, " +
                                "CASE WHEN ML.LEDGER_SUB_TYPE='BK' THEN CONCAT(CONCAT(ML.LEDGER_NAME,' - '),CONCAT(MB.BANK,' - '),MB.BRANCH) " +
                                "ELSE ML.LEDGER_NAME END AS 'LEDGER_NAME'," +
                                "ABS(SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END)) AS AMOUNT, " +
                                "CASE WHEN (SUM(CASE WHEN LB2.TRANS_MODE = 'DR' THEN LB2.AMOUNT ELSE - LB2.AMOUNT END) >= 0 ) " +
                                "THEN 'DR' ELSE 'CR' END AS 'TRANSMODE' FROM MASTER_LEDGER AS ML " +
                                "INNER JOIN MASTER_LEDGER_GROUP AS LG ON ML.GROUP_ID = LG.GROUP_ID " +
                                "INNER JOIN (SELECT LB.BALANCE_DATE, LB.PROJECT_ID, LB.LEDGER_ID, LB.AMOUNT, LB.TRANS_MODE " +
                                "FROM LEDGER_BALANCE AS LB LEFT JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE " +
                                "FROM LEDGER_BALANCE LBA WHERE 1 = 1 AND LBA.BALANCE_DATE <= ?BALANCE_DATE " +
                                "GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) AS LB1 " +
                                     "ON LB.PROJECT_ID = LB1.PROJECT_ID " +
                                     "AND LB.LEDGER_ID = LB1.LEDGER_ID " +
                                     "WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND LB.BRANCH_ID IN(?BRANCH_OFFICE_ID) " +
                                     "AND LB.BALANCE_DATE = LB1.BAL_DATE) LB2 " +
                               "ON ML.LEDGER_ID = LB2.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK_ACCOUNT MBA " +
                               "ON MBA.LEDGER_ID=ML.LEDGER_ID " +
                               "LEFT JOIN MASTER_BANK MB " +
                               "ON MB.BANK_ID=MBA.BANK_ID " +
                               "WHERE LG.GROUP_ID IN (?GROUP_ID) AND ML.STATUS=0 " +
                               "GROUP BY LG.GROUP_ID, LG.GROUP_CODE, LG.LEDGER_GROUP,ML.LEDGER_NAME";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}

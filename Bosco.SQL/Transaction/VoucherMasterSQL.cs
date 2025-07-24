using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherMasterSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.VoucherMaster).FullName)
            {
                query = GetVoucherMasterSQL();
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
        private string GetVoucherMasterSQL()
        {
            string query = "";
            SQLCommand.VoucherMaster sqlCommandId = (SQLCommand.VoucherMaster)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherMaster.Add:
                    {
                        query = "INSERT INTO VOUCHER_MASTER_TRANS ( " +
                               "VOUCHER_DATE, " +
                               "PROJECT_ID, " +
                               "VOUCHER_NO, " +
                               "VOUCHER_TYPE," +
                               "VOUCHER_SUB_TYPE," +
                               "DONOR_ID," +
                               "PURPOSE_ID," +
                               "CONTRIBUTION_TYPE," +
                               "CONTRIBUTION_AMOUNT," +
                               "CURRENCY_COUNTRY_ID," +
                               "EXCHANGE_RATE," +
                               "CALCULATED_AMOUNT," +
                               "ACTUAL_AMOUNT," +
                               "EXCHANGE_COUNTRY_ID," +
                               "NARRATION," +
                               "STATUS," +
                               "CREATED_ON," +
                               "CREATED_BY,NAME_ADDRESS," +
                               "MODIFIED_BY ) VALUES( " +
                               "?VOUCHER_DATE, " +
                               "?PROJECT_ID, " +
                               "?VOUCHER_NO, " +
                               "?VOUCHER_TYPE," +
                               "?VOUCHER_SUB_TYPE," +
                               "?DONOR_ID," +
                               "?PURPOSE_ID," +
                               "?CONTRIBUTION_TYPE," +
                               "?CONTRIBUTION_AMOUNT," +
                               "?CURRENCY_COUNTRY_ID," +
                               "?EXCHANGE_RATE," +
                               "?CALCULATED_AMOUNT," +
                               "?ACTUAL_AMOUNT," +
                               "?EXCHANGE_COUNTRY_ID," +
                               "?NARRATION," +
                               "?STATUS," +
                               "?CREATED_ON," +
                                "?CREATED_BY,?NAME_ADDRESS," +
                               "?MODIFIED_BY)";
                        break;
                    }
                case SQLCommand.VoucherMaster.Update:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET " +
                                    "VOUCHER_DATE = ?VOUCHER_DATE, " +
                                    "PROJECT_ID =?PROJECT_ID, " +
                                    "VOUCHER_NO =?VOUCHER_NO, " +
                                    "VOUCHER_TYPE=?VOUCHER_TYPE, " +
                                    "VOUCHER_SUB_TYPE=VOUCHER_SUB_TYPE," +
                                    "DONOR_ID=?DONOR_ID," +
                                    "PURPOSE_ID=?PURPOSE_ID," +
                                    "CONTRIBUTION_TYPE=?CONTRIBUTION_TYPE ," +
                                    "CONTRIBUTION_AMOUNT=?CONTRIBUTION_AMOUNT," +
                                    "CURRENCY_COUNTRY_ID=?CURRENCY_COUNTRY_ID," +
                                    "EXCHANGE_RATE=?EXCHANGE_RATE ," +
                                    "CALCULATED_AMOUNT=?CALCULATED_AMOUNT ," +
                                    "ACTUAL_AMOUNT=?ACTUAL_AMOUNT ," +
                                    "EXCHANGE_COUNTRY_ID=?EXCHANGE_COUNTRY_ID," +
                                    "NARRATION=?NARRATION ," +
                                    "STATUS=?STATUS," +
                                    "MODIFIED_ON=?MODIFIED_ON," +
                                    "MODIFIED_BY=?MODIFIED_BY, " +
                                    "NAME_ADDRESS=?NAME_ADDRESS " +
                                    " WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.Delete:
                    {
                        //query = "UPDATE  VOUCHER_MASTER_TRANS SET STATUS=0 WHERE VOUCHER_ID=?VOUCHER_ID";
                        query = " DELETE FROM VOUCHER_MASTER_TRANS WHERE BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteCCVoucher:
                    {
                        query = "DELETE FROM VOUCHER_CC_TRANS WHERE BRANCH_ID = ?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteFDVoucher:
                    {
                        query = " DELETE FROM VOUCHER_MASTER_TRANS WHERE BRANCH_ID=?BRANCH_OFFICE_ID " +
                                " { AND PROJECT_ID=?PROJECT_ID  }" +
                                " { AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED }" +
                                " AND VOUCHER_SUB_TYPE IN (?VOUCHER_SUB_TYPE)  ";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteOPBalance:
                    {
                        query = " DELETE  FROM LEDGER_BALANCE WHERE BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteOPFDOPBalance:
                    {
                        query = " DELETE  FROM LEDGER_BALANCE WHERE BRANCH_ID=?BRANCH_OFFICE_ID " +
                             " { AND PROJECT_ID=?PROJECT_ID } " +
                             " { AND BALANCE_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED } " +
                             " AND TRANS_FLAG=?TRANS_FLAG" + ";" +

                             " DELETE FROM FD_RENEWAL WHERE { PROJECT_ID=?PROJECT_ID AND }  FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID  FROM FD_ACCOUNT WHERE BRANCH_ID=?BRANCH_OFFICE_ID AND TRANS_TYPE=?TRANS_FLAG)" + ";" +

                               " DELETE  FROM FD_ACCOUNT WHERE BRANCH_ID=?BRANCH_OFFICE_ID " +
                               " { AND PROJECT_ID=?PROJECT_ID } " +
                               " AND TRANS_TYPE=?TRANS_FLAG;";

                        break;
                    }

                case SQLCommand.VoucherMaster.Fetch:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_DATE, " +
                                "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT', " +
                                "VOUCHER_NO, CASE VOUCHER_TYPE WHEN 'RC' THEN 'Receipts' WHEN 'PY' THEN 'Payments'" +
                                "WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHERTYPE," +
                                "CASE VOUCHER_TYPE WHEN 'RC' THEN CONTRIBUTION_AMOUNT WHEN 'CN'  THEN CONTRIBUTION_AMOUNT ELSE '' END AS DEBIT, " +
                                "CASE VOUCHER_TYPE WHEN 'PY' THEN CONTRIBUTION_AMOUNT ELSE '' END AS CREDIT, " +
                                "NAME AS DONOR_NAME FROM VOUCHER_MASTER_TRANS AS VM " +
                                "INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID=MP.PROJECT_ID " +
                                "INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID=MD.DIVISION_ID " +
                                "INNER JOIN MASTER_DONAUD AS MAD ON VM.DONOR_ID=MAD.DONAUD_ID WHERE VOUCHER_TYPE IN('RC','PY','CN') AND  VM.PROJECT_ID=?PROJECT_ID AND MP.DELETE_FLAG<>1 ORDER BY VOUCHER_DATE,VOUCHER_ID ASC  ";// FIND_IN_SET(VOUCHER_TYPE,?VOUCHER_TYPE) >0 
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchAll:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_DATE, " +
                                "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT'," +
                                "VOUCHER_NO, CASE VOUCHER_TYPE WHEN 'RC' THEN 'Receipts' WHEN 'PY' THEN 'Payments'" +
                                "WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHERTYPE," +
                                "CASE VOUCHER_TYPE WHEN 'RC' THEN CONTRIBUTION_AMOUNT WHEN 'CN'  THEN CONTRIBUTION_AMOUNT ELSE '' END AS DEBIT," +
                                "CASE VOUCHER_TYPE WHEN 'PY' THEN CONTRIBUTION_AMOUNT ELSE '' END AS CREDIT," +
                                "NAME AS DONOR_NAME FROM VOUCHER_MASTER_TRANS AS VM " +
                                "INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID=MP.PROJECT_ID " +
                                "INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID=MD.DIVISION_ID " +
                                "INNER JOIN MASTER_DONAUD AS MAD ON VM.DONOR_ID=MAD.DONAUD_ID ";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchJournalDetails:
                    {
                        //query = "SELECT VMT.VOUCHER_ID,\n" +
                        // "       VMT.VOUCHER_NO,\n" +
                        // "       VMT.VOUCHER_DATE,\n" +
                        // "       T.DEBIT AS DEBIT,\n" +
                        // "       VMT.NARRATION\n" +
                        // "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        // " INNER JOIN VOUCHER_TRANS VT\n" +
                        // "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        // "  LEFT JOIN (SELECT VMT.VOUCHER_ID, IFNULL(SUM(AMOUNT), 0) AS DEBIT\n" +
                        // "               FROM VOUCHER_MASTER_TRANS VMT\n" +
                        // "              INNER JOIN VOUCHER_TRANS VT\n" +
                        // "                 ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        // "              WHERE VMT.PROJECT_ID = ?PROJECT_ID\n" +
                        // "                AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        // "                AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        // "                AND VT.TRANS_MODE = 'DR'\n" +
                        // "              GROUP BY VMT.VOUCHER_NO\n" +
                        // "              ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID) AS T\n" +
                        // "    ON VT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        // " WHERE VMT.PROJECT_ID =  ?PROJECT_ID\n" +
                        // "   AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        // "   AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        // "  AND VMT.STATUS=1\n" +
                        // " GROUP BY VMT.VOUCHER_NO\n" +
                        // " ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID";

                        query = "SELECT VMT.VOUCHER_ID,\n" +
                        "       FD.FD_ACCOUNT_ID,\n" +
                        "       FD.RECEIPT_NO,\n" +
                        "       FD_STATUS,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       VMT.VOUCHER_DATE,\n" +
                        "       VMT.VOUCHER_SUB_TYPE,\n" +
                        "       T.DEBIT AS DEBIT,\n" +
                        "       VMT.NARRATION\n" +
                        "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        " INNER JOIN VOUCHER_TRANS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "\n" +
                        "  LEFT JOIN (SELECT VMT.VOUCHER_ID, IFNULL(SUM(AMOUNT), 0) AS DEBIT\n" +
                        "               FROM VOUCHER_MASTER_TRANS VMT\n" +
                        "              INNER JOIN VOUCHER_TRANS VT\n" +
                        "                 ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "              WHERE VMT.PROJECT_ID = ?PROJECT_ID\n" +
                        "              AND VMT.VOUCHER_DATE\n" +
                        "              BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        "                AND VT.TRANS_MODE = 'DR'\n" +
                        "              GROUP BY VMT.VOUCHER_NO\n" +
                        "              ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID) AS T\n" +
                        "    ON VT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        "  LEFT JOIN FD_RENEWAL AS FD\n" +
                        "    ON VT.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                        "  LEFT JOIN FD_ACCOUNT AS FA\n" +
                        "    ON FD.FD_ACCOUNT_ID = FA.FD_ACCOUNT_ID\n" +
                        " WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        "   AND VMT.STATUS = 1\n" +
                        " GROUP BY VMT.VOUCHER_NO\n" +
                        " ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID";


                        break;

                    }
                case SQLCommand.VoucherMaster.FetchJournalTransDetails:
                    {
                        query = "SELECT VMT.VOUCHER_ID,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       CASE\n" +
                        "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        "          IFNULL(AMOUNT, 0)\n" +
                        "       END AS DEBIT,\n" +
                        "       CASE\n" +
                        "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        "          IFNULL(AMOUNT, 0)\n" +
                        "       END AS CREDIT \n" +
                        "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        " INNER JOIN VOUCHER_TRANS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " WHERE \n" +
                        "  FIND_IN_SET(VMT.VOUCHER_ID ,?VOUCHER_ID)\n" +
                            //  "   AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND VMT.VOUCHER_TYPE = 'JN' ORDER BY VMT.VOUCHER_NO,VT.LEDGER_ID";
                        break;

                    }

                case SQLCommand.VoucherMaster.FetchMasterDetails:
                    {
                        // "       VT.AMOUNT AS AMOUNT,\n" +
                        //query = "SELECT VM.VOUCHER_ID,FD.FD_ACCOUNT_ID,\n" +
                        //"       VOUCHER_DATE, VT.LEDGER_ID, \n" +
                        //"       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                        //"       VOUCHER_NO,VOUCHER_SUB_TYPE,\n" +
                        //"       CASE VT.TRANS_MODE WHEN 'DR' THEN SUM(VT.AMOUNT) END AS AMOUNT,\n" +
                        //"       CASE VOUCHER_TYPE\n" +
                        //"         WHEN 'RC' THEN\n" +
                        //"          'Receipts'\n" +
                        //"         WHEN 'PY' THEN\n" +
                        //"          'Payments'\n" +
                        //"         WHEN 'CN' THEN\n" +
                        //"          'Contra'\n" +
                        //"         ELSE\n" +
                        //"          'JOURNAL'\n" +
                        //"       END AS VOUCHERTYPE,\n" +
                        //"       NAME AS DONOR_NAME,\n" +
                        //"       CASE VOUCHER_TYPE\n" +
                        //"         WHEN 'RC' THEN\n" +
                        //"          CONTRIBUTION_AMOUNT\n" +
                        //"         WHEN 'CN' THEN\n" +
                        //"          CONTRIBUTION_AMOUNT\n" +
                        //"         ELSE\n" +
                        //"          ''\n" +
                        //"       END AS DEBIT,\n" +
                        //"       CASE VOUCHER_TYPE\n" +
                        //"         WHEN 'PY' THEN\n" +
                        //"          CONTRIBUTION_AMOUNT\n" +
                        //"         ELSE\n" +
                        //"          ''\n" +
                        //"       END AS CREDIT\n" +
                        //"  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        //" INNER JOIN MASTER_PROJECT AS MP\n" +
                        //"    ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        //" INNER JOIN MASTER_DIVISION AS MD\n" +
                        //"    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        //"  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        //"    ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        //" INNER JOIN VOUCHER_TRANS VT\n" +
                        //"    ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        //" LEFT JOIN FD_ACCOUNT AS FD\n" +
                        //"   ON VM.VOUCHER_ID=FD.FD_VOUCHER_ID\n" +
                        //"  LEFT JOIN (SELECT VT.VOUCHER_ID, SUM(VT.AMOUNT) AS AMT\n" +
                        //"               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        //"              INNER JOIN MASTER_PROJECT AS MP\n" +
                        //"                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        //"              INNER JOIN MASTER_DIVISION AS MD\n" +
                        //"                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        //"               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        //"                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        //"              INNER JOIN VOUCHER_TRANS VT\n" +
                        //"                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        //"              LEFT JOIN FD_ACCOUNT AS FD\n" +
                        //"                   ON VM.VOUCHER_ID=FD.FD_VOUCHER_ID\n" +
                        //"              WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                        //"                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        //"                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        //"                AND VT.TRANS_MODE='DR'\n" +
                        //"                AND VT.TRANS_MODE = 'CR') AS T\n" +
                        //"    ON T.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        //" WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                        //"   AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        //"   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        //"   AND VT.TRANS_MODE='DR'\n" +
                        //"   AND VM.STATUS=1 \n" +
                        //" GROUP BY VT.VOUCHER_ID ORDER BY VOUCHER_DATE,VOUCHER_ID ASC";
                        query = "SELECT VM.VOUCHER_ID,\n" +
                            "       FD.FD_ACCOUNT_ID,\n" +
                            "       CASE VOUCHER_TYPE\n" +
                            "         WHEN 'CN' THEN\n" +
                            "         ML.LEDGER_NAME ELSE \n" +
                            "       LED.LEDGER_NAME END AS LEDGER_NAME,\n" +
                            "       VOUCHER_DATE,\n" +
                            "       VT.LEDGER_ID,\n" +
                            "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                            "       VOUCHER_NO,\n" +
                            "       VOUCHER_SUB_TYPE,\n" +
                            "       CASE VT.TRANS_MODE\n" +
                            "         WHEN 'DR' THEN\n" +
                            "          SUM(VT.AMOUNT)\n" +
                            "       END AS AMOUNT,\n" +
                            "       CASE VOUCHER_TYPE\n" +
                            "         WHEN 'RC' THEN\n" +
                            "          'Receipts'\n" +
                            "         WHEN 'PY' THEN\n" +
                            "          'Payments'\n" +
                            "         WHEN 'CN' THEN\n" +
                            "          'Contra'\n" +
                            "         ELSE\n" +
                            "          'JOURNAL'\n" +
                            "       END AS VOUCHERTYPE,\n" +
                            "       NAME AS DONOR_NAME,\n" +
                            "       CASE VOUCHER_TYPE\n" +
                            "         WHEN 'RC' THEN\n" +
                            "          CONTRIBUTION_AMOUNT\n" +
                            "         WHEN 'CN' THEN\n" +
                            "          CONTRIBUTION_AMOUNT\n" +
                            "         ELSE\n" +
                            "          ''\n" +
                            "       END AS DEBIT,\n" +
                            "       CASE VOUCHER_TYPE\n" +
                            "         WHEN 'PY' THEN\n" +
                            "          CONTRIBUTION_AMOUNT\n" +
                            "         ELSE\n" +
                            "          ''\n" +
                            "       END AS CREDIT\n" +
                            "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                            " INNER JOIN MASTER_PROJECT AS MP\n" +
                            "    ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                            " INNER JOIN MASTER_DIVISION AS MD\n" +
                            "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                            "  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                            "    ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                            " INNER JOIN VOUCHER_TRANS VT\n" +
                            "    ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                            "  LEFT JOIN MASTER_LEDGER ML\n" +
                            "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                            "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "  LEFT JOIN FD_ACCOUNT AS FD\n" +
                            "    ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                            "\n" +
                            "  LEFT JOIN (SELECT VT.VOUCHER_ID, SUM(VT.AMOUNT) AS AMT\n" +
                            "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                            "              INNER JOIN MASTER_PROJECT AS MP\n" +
                            "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                            "              INNER JOIN MASTER_DIVISION AS MD\n" +
                            "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                            "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                            "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                            "              INNER JOIN VOUCHER_TRANS VT\n" +
                            "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                            "\n" +
                            "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                            "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                            "              WHERE FIND_IN_SET(VOUCHER_TYPE,?VOUCHER_TYPE) > 0\n" +
                            "                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                            "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                            "                AND VT.TRANS_MODE = 'DR'\n" +
                            "                AND VT.TRANS_MODE = 'CR') AS T\n" +
                            "    ON T.VOUCHER_ID = VT.VOUCHER_ID\n" +
                            "  LEFT JOIN (SELECT VT.VOUCHER_ID, VT.LEDGER_ID, ML.LEDGER_NAME\n" +
                            "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                            "              INNER JOIN MASTER_PROJECT AS MP\n" +
                            "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                            "              INNER JOIN MASTER_DIVISION AS MD\n" +
                            "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                            "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                            "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                            "              INNER JOIN VOUCHER_TRANS VT\n" +
                            "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                            "               LEFT JOIN MASTER_LEDGER ML\n" +
                            "                 ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "              INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                            "                 ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                            "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                            "              WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                            "                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                            "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                            "                   --  AND VT.TRANS_MODE='DR'\n" +
                            "                and ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                            "             -- AND VT.TRANS_MODE = 'CR'\n" +
                            "             ) AS LED\n" +
                            "    ON LED.VOUCHER_ID = VT.VOUCHER_ID\n" +
                            " WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                            "   AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                            "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                            "   AND VT.TRANS_MODE = 'DR'\n" +
                            "\n" +
                            "   AND VM.STATUS = 1\n" +
                            " GROUP BY VT.VOUCHER_ID\n" +
                            " ORDER BY VOUCHER_DATE, VOUCHER_ID ASC";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchMasterByID:
                    {
                        query = "SELECT " +
                                "VOUCHER_ID," +
                                "MT.PROJECT_ID," +
                                "MP.PROJECT," +
                                "VOUCHER_DATE," +
                                "VOUCHER_NO," +
                                "VOUCHER_TYPE," +
                                "DONOR_ID," +
                                "PURPOSE_ID," +
                                "CONTRIBUTION_TYPE," +
                                "CONTRIBUTION_AMOUNT," +
                                "CURRENCY_COUNTRY_ID," +
                                "EXCHANGE_RATE," +
                                "CALCULATED_AMOUNT," +
                                "ACTUAL_AMOUNT," +
                                "EXCHANGE_COUNTRY_ID," +
                                "STATUS," +
                            //  "CREATED_ON," +
                            //   "MODIFIED_ON," +
                                "CREATED_BY," +
                                "MODIFIED_BY," +
                                "NARRATION,NAME_ADDRESS " +
                                "FROM VOUCHER_MASTER_TRANS MT INNER JOIN MASTER_PROJECT MP ON MT.PROJECT_ID=MP.PROJECT_ID WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchMasterByBranchLocationVoucherId:
                    {
                        query = @"SELECT VOUCHER_ID, MP.PROJECT, BL.LOCATION_NAME, VOUCHER_DATE, VOUCHER_NO, VOUCHER_TYPE, VOUCHER_SUB_TYPE, STATUS
                                FROM VOUCHER_MASTER_TRANS MT 
                                INNER JOIN MASTER_PROJECT MP ON MT.PROJECT_ID = MP.PROJECT_ID 
                                LEFT JOIN BRANCH_LOCATION BL ON BL.LOCATION_ID = MT.LOCATION_ID AND BL.LOCATION_ID=?LOCATION_ID
                                WHERE MT.BRANCH_ID = ?BRANCH_ID AND MT.LOCATION_ID = ?LOCATION_ID AND MT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherStartingNo:
                    {
                        query = "SELECT" +
                                " CONCAT(PREFIX_CHAR,CONCAT('#',STARTING_NUMBER ),CONCAT('#',SUFFIX_CHAR)) AS 'VOUCHER_NUMBER' " +
                                " FROM MASTER_VOUCHER AS MV " +
                                " INNER JOIN PROJECT_VOUCHER AS MPV ON " +
                                " MV.VOUCHER_ID=MPV.VOUCHER_ID " +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID " +
                                " AND MV.VOUCHER_TYPE=?VOUCHER_TYPE AND VOUCHER_TYPE NOT IN (4)";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTransactionMadeForProject:
                    {
                        query = "SELECT VT.LEDGER_ID,LEDGER_NAME FROM VOUCHER_TRANS VT " +
                                         "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= VT.LEDGER_ID " +
                                         "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID =VT.VOUCHER_ID " +
                                 "WHERE  VMT.PROJECT_ID=?PROJECT_ID AND FIND_IN_SET(VT.LEDGER_ID,?IDs) AND VMT.STATUS=1 " +
                                 "GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTransactionMadeForLedger:
                    {
                        query = "SELECT VT.LEDGER_ID,LEDGER_NAME FROM VOUCHER_TRANS VT " +
                                        "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= VT.LEDGER_ID " +
                                        "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID =VT.VOUCHER_ID " +
                                "WHERE VT.LEDGER_ID=?LEDGER_ID AND FIND_IN_SET(VMT.PROJECT_ID,?PROJECT_IDs) " +
                                "GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchLastVoucherDate:
                    {
                        query = " SELECT VOUCHER_DATE FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID  AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO AND STATUS=1 ORDER BY VOUCHER_DATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchBRS:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
                        "       MT.SEQUENCE_NO,\n" +
                        "       VOUCHER_DATE,\n" +
                        "       MATERIALIZED_ON,\n" +
                        "       T.LEDGER_ID,\n" +
                        "       T.LEDGER_NAME,\n" +
                        "       CASE\n" +
                        "         WHEN VMT.VOUCHER_TYPE = 'PY' THEN\n" +
                        "          IFNULL(MT.AMOUNT, 0) ELSE 0.00\n" +
                        "       END AS 'PAYMENT',\n" +
                        "       CASE\n" +
                        "         WHEN VMT.VOUCHER_TYPE = 'RC' THEN\n" +
                        "          IFNULL(MT.AMOUNT, 0) ELSE 0.00\n" +
                        "       END AS 'RECEIPT',\n" +
                        "       MT.AMOUNT,\n" +
                        "       TRANS_MODE,\n" +
                        "       -- MT.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'PY' THEN\n" +
                        "          'UnCleared'\n" +
                        "         WHEN MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'PY' THEN\n" +
                        "          'Cleared'\n" +
                        "         WHEN MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'RC' THEN\n" +
                        "          'UnReconciled'\n" +
                        "         WHEN MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'RC' THEN\n" +
                        "          'Reconciled'\n" +
                        "       END AS 'STATUS',\n" +
                        "\n" +
                        "       CONCAT(CONCAT(BA.ACCOUNT_NUMBER, ' - '), BANK) AS BANK\n" +
                        "  FROM MASTER_PROJECT PL\n" +
                        " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "    ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                        " INNER JOIN VOUCHER_TRANS MT\n" +
                        "    ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        " INNER JOIN (SELECT MT.VOUCHER_ID,\n" +
                        "                    MT.LEDGER_ID,\n" +
                        "                    ML.LEDGER_CODE,\n" +
                        "                    ML.LEDGER_NAME AS LEDGER_NAME\n" +
                        "               FROM PROJECT_LEDGER PL\n" +
                        "              INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "                 ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                        "              INNER JOIN VOUCHER_TRANS MT\n" +
                        "                 ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        "              INNER JOIN MASTER_LEDGER ML\n" +
                        "                 ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "\n" +
                        "              WHERE PL.PROJECT_ID = ?PROJECT_ID\n" +
                        "                AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED \n" +
                        "                AND MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "              GROUP BY VMT.VOUCHER_ID\n" +
                        "              ORDER BY VMT.VOUCHER_ID) AS T\n" +
                        "    ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        "\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN master_bank_ACCOUNT ba\n" +
                        "    ON ML.BANK_ACCOUNT_ID = BA.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_BANK MB\n" +
                        "    ON BA.BANK_ID = MB.BANK_ID\n" +
                        " WHERE VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED \n" +
                        "   AND ML.LEDGER_SUB_TYPE = 'BK'\n" +
                        "   AND VMT.STATUS=1\n" +
                        "   AND VMT.VOUCHER_TYPE IN ('PY', 'RC')\n" +
                        "   AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                        " ORDER BY STATUS DESC";

                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateBRS:
                    {
                        query = "UPDATE VOUCHER_TRANS SET MATERIALIZED_ON=?MATERIALIZED_ON WHERE VOUCHER_ID= ?VOUCHER_ID AND SEQUENCE_NO=?SEQUENCE_NO ";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckProjectExist:
                    {
                        query = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.VoucherFDInterestAdd:
                    {
                        query = "INSERT INTO VOUCHER_FD_INTEREST\n" +
                        "  (FD_VOUCHER_ID, FD_LEDGER_ID, BK_INT_VOUCHER_ID, BK_INT_LEDGER_ID)\n" +
                        "VALUES\n" +
                        "  (?FD_VOUCHER_ID,\n" +
                        "   ?FD_LEDGER_ID,\n" +
                        "   ?BK_INT_VOUCHER_ID,\n" +
                        "   ?BK_INT_LEDGER_ID)";

                        break;
                    }
                case SQLCommand.VoucherMaster.VoucherFDInterestDelete:
                    {
                        query = "DELETE FROM VOUCHER_FD_INTEREST WHERE BK_INT_VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherInterest:
                    {
                        query = "SELECT FD_VOUCHER_ID,BK_INT_VOUCHER_ID\n" +
                        "  FROM VOUCHER_FD_INTEREST\n" +
                        " WHERE FD_VOUCHER_ID = ?VOUCHER_ID\n" +
                        "    OR BK_INT_VOUCHER_ID = ?VOUCHER_ID";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherInterestByVoucherId:
                    {
                        query = "SELECT VT.LEDGER_ID, VT.AMOUNT, VMT.NARRATION, VT.TRANS_MODE\n" +
                         "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                         " INNER JOIN VOUCHER_TRANS VT\n" +
                         "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                         " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherPostedInterest:
                    {
                        query = "SELECT  ROUND(AMOUNT * (INTEREST_RATE / 100),2) AS INTEREST_RATE\n" +
                        "  FROM MASTER_BANK_ACCOUNT\n" +
                        " WHERE BANK_ACCOUNT_ID =?BANK_ACCOUNT_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchMasterVoucherDetails:
                    {
                        query = "SELECT VM.VOUCHER_ID,VM.AMENDMENT_FLAG,VM.BRANCH_ID,VM.LOCATION_ID,CONCAT(VM.VOUCHER_ID,',',VM.LOCATION_ID) AS KeyValue,\n" +
                       "       FD.FD_ACCOUNT_ID,AH.REMARKS,\n" +
                       "       CASE VOUCHER_TYPE\n" +
                       "         WHEN 'CN' THEN\n" +
                       "         ML.LEDGER_NAME ELSE\n" +
                       "       LED.LEDGER_NAME END AS LEDGER_NAME,\n" +
                       "       DATE_FORMAT(VOUCHER_DATE,'%d/%m/%Y') AS VOUCHER_DATE ,\n" +
                       "       VT.LEDGER_ID,\n" +
                       "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                       "       VOUCHER_NO,\n" +
                       "       VOUCHER_SUB_TYPE,\n" +
                       "       CASE WHEN VM.VOUCHER_TYPE ='RC'\n" +
                       "           THEN\n" +
                       "              RECEIPT.RECEIPT\n" +
                       "           WHEN VM.VOUCHER_TYPE ='PY'\n" +
                       "             THEN PAYMENT.PAYMENT ELSE VT.AMOUNT\n" +
                       "       END AS AMOUNT,\n" +
                       "       CASE VOUCHER_TYPE\n" +
                       "         WHEN 'RC' THEN\n" +
                       "          'Receipts'\n" +
                       "         WHEN 'PY' THEN\n" +
                       "          'Payments'\n" +
                       "         WHEN 'CN' THEN\n" +
                       "          'Contra'\n" +
                       "         ELSE\n" +
                       "          'Journal'\n" +
                       "       END AS VOUCHERTYPE,\n" +
                       "       NAME AS DONOR_NAME,\n" +
                       "       CASE VOUCHER_TYPE\n" +
                       "         WHEN 'RC' THEN\n" +
                       "          CONTRIBUTION_AMOUNT\n" +
                       "         WHEN 'CN' THEN\n" +
                       "          CONTRIBUTION_AMOUNT\n" +
                       "         ELSE\n" +
                       "          ''\n" +
                       "       END AS DEBIT,\n" +
                       "       CASE VOUCHER_TYPE\n" +
                       "         WHEN 'PY' THEN\n" +
                       "          CONTRIBUTION_AMOUNT\n" +
                       "         ELSE\n" +
                       "          ''\n" +
                       "       END AS CREDIT\n" +
                       "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                       " INNER JOIN MASTER_PROJECT AS MP\n" +
                       "    ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                       " INNER JOIN MASTER_DIVISION AS MD\n" +
                       "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                       "  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                       "    ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                       " INNER JOIN VOUCHER_TRANS VT\n" +
                       "    ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                       "    AND VM.LOCATION_ID=VT.LOCATION_ID\n" +
                       "  AND VM.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "  LEFT JOIN MASTER_LEDGER ML\n" +
                       "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                       " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                       "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                       "  LEFT JOIN FD_ACCOUNT AS FD\n" +
                       "    ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                       "    AND VM.LOCATION_ID = FD.LOCATION_ID\n" +
                       " LEFT JOIN AMENDMENT_HISTORY AS AH\n" +
                       " ON AH.VOUCHER_ID=VM.VOUCHER_ID AND AH.BRANCH_ID=VM.BRANCH_ID\n" +
                       "  LEFT JOIN (SELECT VT.VOUCHER_ID,VT.LOCATION_ID,  SUM(VT.AMOUNT) AS RECEIPT\n" +
                       "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                       "              INNER JOIN MASTER_PROJECT AS MP\n" +
                       "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                       "              INNER JOIN MASTER_DIVISION AS MD\n" +
                       "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                       "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                       "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                       "              INNER JOIN VOUCHER_TRANS VT\n" +
                       "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                       "                AND VM.LOCATION_ID=VT.LOCATION_ID\n" +
                       "                AND VM.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                       "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                       "                 AND VM.LOCATION_ID = FD.LOCATION_ID\n" +
                       "                WHERE VT.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "                AND VOUCHER_TYPE IN ('RC','PY')\n" +
                       "                { AND VM.PROJECT_ID = ?PROJECT_ID }\n" +
                       "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                       "                AND VT.TRANS_MODE = 'CR' GROUP BY VM.VOUCHER_ID,VM.LOCATION_ID) AS RECEIPT\n" +
                       "    ON RECEIPT.VOUCHER_ID = VT.VOUCHER_ID AND RECEIPT.LOCATION_ID = VT.LOCATION_ID\n" +
                       "\n" +
                       "     LEFT JOIN (SELECT VT.VOUCHER_ID,VT.LOCATION_ID,  SUM(VT.AMOUNT) AS PAYMENT\n" +
                       "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                       "              INNER JOIN MASTER_PROJECT AS MP\n" +
                       "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                       "              INNER JOIN MASTER_DIVISION AS MD\n" +
                       "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                       "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                       "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                       "              INNER JOIN VOUCHER_TRANS VT\n" +
                       "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                       "                AND VM.LOCATION_ID = VT.LOCATION_ID\n" +
                       "                AND VM.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                       "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                       "                WHERE VT.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "                AND VOUCHER_TYPE IN ('RC','PY' )\n" +
                       "               { AND VM.PROJECT_ID = ?PROJECT_ID }\n" +
                       "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                       "                AND VT.TRANS_MODE = 'DR'  GROUP BY VM.VOUCHER_ID,VM.LOCATION_ID) AS PAYMENT\n" +
                       "    ON PAYMENT.VOUCHER_ID = VT.VOUCHER_ID AND PAYMENT.LOCATION_ID = VT.LOCATION_ID\n" +
                       "\n" +
                       "      LEFT JOIN (SELECT VT.VOUCHER_ID,VT.LOCATION_ID, VT.LEDGER_ID, ML.LEDGER_NAME\n" +
                       "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                       "              INNER JOIN MASTER_PROJECT AS MP\n" +
                       "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                       "              INNER JOIN MASTER_DIVISION AS MD\n" +
                       "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                       "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                       "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                       "              INNER JOIN VOUCHER_TRANS VT\n" +
                       "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                       "                AND VM.LOCATION_ID = VT.LOCATION_ID\n" +
                       "                AND VM.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "               LEFT JOIN MASTER_LEDGER ML\n" +
                       "                 ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                       "              INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                       "                 ON ML.GROUP_ID = LG.GROUP_ID\n" +
                       "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                       "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                       "                 AND VM.LOCATION_ID = FD.LOCATION_ID\n" +
                       "                WHERE VT.BRANCH_ID=?BRANCH_OFFICE_ID\n" +
                       "                AND FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                       "               { AND VM.PROJECT_ID = ?PROJECT_ID }\n" +
                       "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                       "                   --  AND VT.TRANS_MODE='DR'\n" +
                       "                and ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                       "             -- AND VT.TRANS_MODE = 'CR'\n" +
                       "             ) AS LED\n" +
                       "    ON LED.VOUCHER_ID = VT.VOUCHER_ID\n" +
                       "   AND LED.LOCATION_ID=VT.LOCATION_ID\n" +
                       " WHERE  VT.BRANCH_ID=?BRANCH_OFFICE_ID \n" +
                       "   AND  FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0 \n" +
                       "  { AND VM.PROJECT_ID = ?PROJECT_ID }\n" +
                       "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                       "   AND VT.TRANS_MODE = 'DR'\n" +
                       "   AND VM.STATUS = 1\n" +
                       " GROUP BY VT.VOUCHER_ID,VT.LOCATION_ID\n" +
                       " ORDER BY VOUCHER_DATE ASC";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherDate:
                    {
                        query = "SELECT MIN(VOUCHER_DATE) AS STARTING_VOUCHER_DATE,\n" +
                                "MAX(VOUCHER_DATE) AS ENDING_VOUCHER_DATE\n" +
                                "FROM VOUCHER_MASTER_TRANS { WHERE BRANCH_ID=?BRANCH_OFFICE_ID }";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchOPBalanceDate:
                    {
                        query = "SELECT MIN(BALANCE_DATE) FROM LEDGER_BALANCE WHERE { PROJECT_ID =?PROJECT_ID AND } { BRANCH_ID=?BRANCH_OFFICE_ID AND } TRANS_FLAG='OP'";
                        break;
                    }
                case SQLCommand.VoucherMaster.DashboardCashBankFlow:
                    {
                        query = "SELECT TT.VOUCHER_ID,\n" +
                            "       CAST(CONCAT(CONCAT(LEFT(MONTHNAME(TT.VOUCHER_DATE), 3),' '),YEAR(TT.VOUCHER_DATE)) AS CHAR ) AS DATE,\n" +
                            "       SUM(REC_IN) AS 'IN_FLOW',\n" +
                            "       SUM(PAY_OUT) AS 'OUT_FLOW',\n" +
                            "    --  SUM(REC_IN) - SUM(PAY_OUT) AS 'IN_FLOW',\n" +
                            "       SUM(BANK_REC_IN) AS 'BANK_IN_FLOW',\n" +
                            "       SUM(BANK_PAY_OUT) AS 'BANK_OUT_FLOW'\n" +
                             "    --  SUM(BANK_REC_IN) - SUM(BANK_PAY_OUT) AS 'BANK_IN_FLOW'\n" +
                            "  FROM (SELECT MT.VOUCHER_ID,\n" +
                            "               MT.VOUCHER_DATE,\n" +
                            "               IFNULL(T.AMOUNT, 0) AS REC_IN,\n" +
                            "               0.00 AS PAY_OUT,\n" +
                            "               0 AS BANK_REC_IN,\n" +
                            "               0 AS BANK_PAY_OUT\n" +
                            "          FROM master_ledger_GROUP LG\n" +
                            "          LEFT JOIN master_ledger ML\n" +
                            "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "          LEFT JOIN voucher_trans VT\n" +
                            "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "          LEFT JOIN voucher_master_trans MT\n" +
                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "          LEFT JOIN MASTER_PROJECT MP\n" +
                            "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "          JOIN (SELECT VT.VOUCHER_ID,\n" +
                            "                      VT.LEDGER_ID,\n" +
                            "                      ML.LEDGER_CODE,\n" +
                            "                      SUM(VT.AMOUNT) AS AMOUNT\n" +
                            "                 FROM master_ledger_GROUP LG\n" +
                            "\n" +
                            "                 LEFT JOIN master_ledger ML\n" +
                            "                   ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                 LEFT JOIN voucher_trans VT\n" +
                            "                   ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                 LEFT JOIN voucher_master_trans MT\n" +
                            "                   ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                 LEFT JOIN MASTER_PROJECT MP\n" +
                            "                   ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                 LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                   ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                WHERE MT.VOUCHER_TYPE IN ('RC')\n" +
                            "                  AND mt.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                  { AND MT.PROJECT_ID IN (?PROJECT_ID) }\n" +
                            "                  and lg.group_id in (13)\n" +
                            "                  AND MT.STATUS = 1\n" +
                            "                    { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                     { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                GROUP BY MONTH(MT.VOUCHER_DATE)) AS T\n" +
                            "            ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                            "         WHERE MT.VOUCHER_TYPE IN ('RC')\n" +
                            "          { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND MT.STATUS = 1\n" +
                            "           AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "           AND LG.GROUP_ID NOT IN (12)\n" +
                            "            { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "             { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "         GROUP BY VT.BRANCH_ID, MT.PROJECT_ID, MT.VOUCHER_DATE\n" +
                            "        UNION ALL\n" +
                            "        SELECT MT.VOUCHER_ID,\n" +
                            "               MT.VOUCHER_DATE,\n" +
                            "               0.0 AS REC_IN,\n" +
                            "               IFNULL(T.AMOUNT, 0) AS PAY_OUT,\n" +
                            "               0 AS BANK_REC_IN,\n" +
                            "               0 AS BANK_PAY_OUT\n" +
                            "          FROM master_ledger_GROUP LG\n" +
                            "          LEFT JOIN master_ledger ML\n" +
                            "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "          LEFT JOIN voucher_trans VT\n" +
                            "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "          LEFT JOIN voucher_master_trans MT\n" +
                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "          LEFT JOIN MASTER_PROJECT MP\n" +
                            "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "          JOIN (SELECT VT.VOUCHER_ID,\n" +
                            "                       VT.LEDGER_ID,\n" +
                            "                       ML.LEDGER_CODE,\n" +
                            "                       SUM(VT.AMOUNT) AS AMOUNT\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE MT.VOUCHER_TYPE IN ('PY')\n" +
                            "                   AND mt.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                   { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "                   and lg.group_id in (13)\n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                     { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                      { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 GROUP BY MONTH(MT.VOUCHER_DATE)) AS T\n" +
                            "            ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                            "         WHERE MT.VOUCHER_TYPE IN ('PY')\n" +
                            "          { AND  MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND MT.STATUS = 1\n" +
                            "           AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "           AND LG.GROUP_ID NOT IN (12)\n" +
                            "             { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "              { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "         GROUP BY VT.BRANCH_ID, MT.PROJECT_ID, MT.VOUCHER_DATE\n" +
                            "        UNION ALL\n" +
                            "        SELECT T.VOUCHER_ID,\n" +
                            "               T.VOUCHER_DATE,\n" +
                            "               SUM(T.AMOUNT) AS REC_IN,\n" +
                            "               '' AS PAY_OUT,\n" +
                            "               0 AS BANK_REC_IN,\n" +
                            "               0 AS BANK_PAY_OUT\n" +
                            "          FROM (SELECT VT.VOUCHER_ID,\n" +
                            "                       LG.GROUP_ID,\n" +
                            "                       MT.VOUCHER_TYPE,\n" +
                            "                       VT.AMOUNT,\n" +
                            "                       MT.VOUCHER_DATE,\n" +
                            "                       MT.PROJECT_ID\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE VT.TRANS_MODE = 'DR'\n" +
                            "                  { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                   AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                     { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                      { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 ORDER BY VT.VOUCHER_ID ASC) AS T\n" +
                            "         WHERE T.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "           { AND T.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND T.GROUP_ID IN (13)\n" +
                            "           AND T.VOUCHER_TYPE = 'CN'\n" +
                            "         GROUP BY T.PROJECT_ID, T.VOUCHER_DATE\n" +
                            "        UNION\n" +
                            "        SELECT T.VOUCHER_ID,\n" +
                            "               T.VOUCHER_DATE,\n" +
                            "               '' AS REC_IN,\n" +
                            "               SUM(T.AMOUNT) AS PAY_OUT,\n" +
                            "               0 AS BANK_REC_IN,\n" +
                            "               0 AS BANK_PAY_OUT\n" +
                            "\n" +
                            "          FROM (SELECT VT.VOUCHER_ID,\n" +
                            "                       LG.GROUP_ID,\n" +
                            "                       MT.VOUCHER_TYPE,\n" +
                            "                       VT.AMOUNT,\n" +
                            "                       MT.VOUCHER_DATE,\n" +
                            "                       MT.PROJECT_ID\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE VT.TRANS_MODE = 'CR'\n" +
                            "                  { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                   AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                    { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                     { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 ORDER BY VT.VOUCHER_ID ASC) AS T\n" +
                            "         WHERE T.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "          { AND T.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND T.GROUP_ID IN (13)\n" +
                            "           AND T.VOUCHER_TYPE = 'CN'\n" +
                            "         GROUP BY T.PROJECT_ID, T.VOUCHER_DATE\n" +
                            "        UNION ALL\n" +
                            "\n" +
                            "        SELECT MT.VOUCHER_ID,\n" +
                            "               MT.VOUCHER_DATE,\n" +
                            "               0 AS REC_IN,\n" +
                            "               0 AS PAY_OUT,\n" +
                            "               IFNULL(T.AMOUNT, 0) AS BANK_REC_IN,\n" +
                            "               0.00 AS BANK_PAY_OUT\n" +
                            "          FROM master_ledger_GROUP LG\n" +
                            "          LEFT JOIN master_ledger ML\n" +
                            "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "          LEFT JOIN voucher_trans VT\n" +
                            "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "          LEFT JOIN voucher_master_trans MT\n" +
                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "          LEFT JOIN MASTER_PROJECT MP\n" +
                            "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "          JOIN (SELECT VT.VOUCHER_ID,\n" +
                            "                       VT.LEDGER_ID,\n" +
                            "                       ML.LEDGER_CODE,\n" +
                            "                       SUM(VT.AMOUNT) AS AMOUNT\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE MT.VOUCHER_TYPE IN ('RC')\n" +
                            "                   AND mt.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                  { AND MT.PROJECT_ID IN (?PROJECT_ID) }\n" +
                            "                   and lg.group_id in (12)\n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                    { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                     { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 GROUP BY MONTH(MT.VOUCHER_DATE)) AS T\n" +
                            "            ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                            "         WHERE MT.VOUCHER_TYPE IN ('RC')\n" +
                            "           { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND MT.STATUS = 1\n" +
                            "           AND VOUCHER_DATE BETWEEN  ?DATE_FROM AND ?DATE_TO \n" +
                            "           AND LG.GROUP_ID NOT IN (13)\n" +
                            "             { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "              { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "         GROUP BY VT.BRANCH_ID, MT.PROJECT_ID, MT.VOUCHER_DATE\n" +
                            "        UNION ALL\n" +
                            "        SELECT MT.VOUCHER_ID,\n" +
                            "               MT.VOUCHER_DATE,\n" +
                            "               0 AS REC_IN,\n" +
                            "               0 AS PAY_OUT,\n" +
                            "               0.0 AS BANK_REC_IN,\n" +
                            "               IFNULL(T.AMOUNT, 0) AS BANK_PAY_OUT\n" +
                            "          FROM master_ledger_GROUP LG\n" +
                            "          LEFT JOIN master_ledger ML\n" +
                            "            ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "          LEFT JOIN voucher_trans VT\n" +
                            "            ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "          LEFT JOIN voucher_master_trans MT\n" +
                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "          LEFT JOIN MASTER_PROJECT MP\n" +
                            "            ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "          LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "            ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "          JOIN (SELECT VT.VOUCHER_ID,\n" +
                            "                       VT.LEDGER_ID,\n" +
                            "                       ML.LEDGER_CODE,\n" +
                            "                       SUM(VT.AMOUNT) AS AMOUNT\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE MT.VOUCHER_TYPE IN ('PY')\n" +
                            "                   AND mt.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                   { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "                   and lg.group_id in (12)\n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                     { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                      { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 GROUP BY MONTH(MT.VOUCHER_DATE)) AS T\n" +
                            "            ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                            "         WHERE  MT.VOUCHER_TYPE IN ('PY')\n" +
                            "          { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND MT.STATUS = 1\n" +
                            "           AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "           AND LG.GROUP_ID NOT IN (13)\n" +
                            "             { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "              { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "         GROUP BY VT.BRANCH_ID, MT.PROJECT_ID, MT.VOUCHER_DATE\n" +
                            "        UNION ALL\n" +
                            "        SELECT T.VOUCHER_ID,\n" +
                            "               T.VOUCHER_DATE,\n" +
                            "               0 AS REC_IN,\n" +
                            "               0 AS PAY_OUT,\n" +
                            "               SUM(T.AMOUNT) AS BANK_REC_IN,\n" +
                            "               0 AS BANK_PAY_OUT\n" +
                            "          FROM (SELECT VT.VOUCHER_ID,\n" +
                            "                       LG.GROUP_ID,\n" +
                            "                       MT.VOUCHER_TYPE,\n" +
                            "                       VT.AMOUNT,\n" +
                            "                       MT.VOUCHER_DATE,\n" +
                            "                       MT.PROJECT_ID\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE VT.TRANS_MODE = 'DR'\n" +
                            "                   { AND MT.PROJECT_ID IN (?PROJECT_ID) }\n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                   AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                    { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                     { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 ORDER BY VT.VOUCHER_ID ASC) AS T\n" +
                            "         WHERE T.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "          { AND T.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND T.GROUP_ID IN (12)\n" +
                            "           AND T.VOUCHER_TYPE = 'CN'\n" +
                            "         GROUP BY T.PROJECT_ID, T.VOUCHER_DATE\n" +
                            "        UNION\n" +
                            "        SELECT T.VOUCHER_ID,\n" +
                            "               T.VOUCHER_DATE,\n" +
                            "               0 AS REC_IN,\n" +
                            "               0 AS PAY_OUT,\n" +
                            "               0 AS BANK_REC_IN,\n" +
                            "               SUM(T.AMOUNT) AS BANK_PAY_OUT\n" +
                            "\n" +
                            "          FROM (SELECT VT.VOUCHER_ID,\n" +
                            "                       LG.GROUP_ID,\n" +
                            "                       MT.VOUCHER_TYPE,\n" +
                            "                       VT.AMOUNT,\n" +
                            "                       MT.VOUCHER_DATE,\n" +
                            "                       MT.PROJECT_ID\n" +
                            "                  FROM master_ledger_GROUP LG\n" +
                            "                  LEFT JOIN master_ledger ML\n" +
                            "                    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                            "                  LEFT JOIN voucher_trans VT\n" +
                            "                    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "                  LEFT JOIN voucher_master_trans MT\n" +
                            "                    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                            "                  LEFT JOIN MASTER_PROJECT MP\n" +
                            "                    ON MP.PROJECT_ID = MT.PROJECT_ID\n" +
                            "                  LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                            "                    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                            "                 WHERE VT.TRANS_MODE = 'CR'\n" +
                            "                   { AND MT.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "                   AND MT.STATUS = 1\n" +
                            "                   AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "                     { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                            "                      { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                            "                 ORDER BY VT.VOUCHER_ID ASC) AS T\n" +
                            "         WHERE T.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                            "           { AND T.PROJECT_ID IN (?PROJECT_ID) } \n" +
                            "           AND T.GROUP_ID IN (12)\n" +
                            "           AND T.VOUCHER_TYPE = 'CN'\n" +
                            "         GROUP BY T.PROJECT_ID, T.VOUCHER_DATE) AS TT\n" +
                            " GROUP BY MONTH(TT.VOUCHER_DATE);";

                        break;
                    }
                case SQLCommand.VoucherMaster.DashboardReceiptPayments:
                    {
                        query = "SELECT T.YEAR,\n" +
                                    "       T.MONTH,\n" +
                                    "       CAST(T.MONTH_NAME AS CHAR) AS MONTH_NAME,\n" +
                                    "       SUM(RECEIPT) AS RECEIPT,\n" +
                                    "       SUM(PAYMENT) AS PAYMENT\n" +
                                    "  FROM (SELECT YEAR(MONTH_YEAR) AS 'YEAR',\n" +
                                    "               MONTH(MONTH_YEAR) AS 'MONTH',\n" +
                                    "               CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS MONTH_NAME,\n" +
                                    "               0 AS RECEIPT,\n" +
                                    "               ifnull(NULLIF(IF(('PY' = 'RC' AND VT.TRANS_MODE = 'DR') OR\n" +
                                    "                                ('PY' = 'PY' AND VT.TRANS_MODE = 'CR'),\n" +
                                    "                                -IFNULL(SUM(VT.AMOUNT), 0),\n" +
                                    "                                IFNULL(SUM(VT.AMOUNT), 0)),\n" +
                                    "                             0),\n" +
                                    "                      0) AS PAYMENT\n" +
                                    "\n" +
                                    "          FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                    "                        DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                    "                       NO_OF_MONTH\n" +
                                    "                  FROM (SELECT @rownum1 := @rownum1 + 1 AS NO_OF_MONTH\n" +
                                    "                          FROM (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T1,\n" +
                                    "                               (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T2,\n" +
                                    "                               (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T3,\n" +
                                    "                               (SELECT @rownum1 := -1) AS T0) D1) D2\n" +
                                    "          LEFT JOIN PROJECT_LEDGER AS PL\n" +
                                    "         INNER JOIN MASTER_LEDGER AS ML\n" +
                                    "         INNER JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                                    "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                    "           AND MLG.GROUP_ID NOT IN (12, 13, 14) ON\n" +
                                    "         PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "           { AND PL.PROJECT_ID IN (?PROJECT_ID) } \n" +
                                    "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                                    "        LEFT JOIN MASTER_PROJECT MP\n" +
                                    "             ON MP.PROJECT_ID=VMT.PROJECT_ID\n" +
                                    "         LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                    "             ON MIP.CUSTOMERID=MP.CUSTOMERID \n" +
                                    "         INNER JOIN VOUCHER_TRANS AS VT\n" +
                                    "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "            AND VMT.BRANCH_ID = VT.BRANCH_ID\n" +
                                    "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                                    "           AND VMT.STATUS = 1\n" +
                                    "  { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID)}\n" +
                                    "  { AND MP.CUSTOMERID IN (?CUSTOMERID)}\n" +
                                    "           AND VMT.VOUCHER_TYPE = 'PY' ON\n" +
                                    "         PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                                    "           AND PL.LEDGER_ID = VT.LEDGER_ID\n" +
                                    "           AND YEAR(D2.MONTH_YEAR) = YEAR(VMT.VOUCHER_DATE)\n" +
                                    "           AND MONTH(D2.MONTH_YEAR) = MONTH(VMT.VOUCHER_DATE)\n" +
                                    "         WHERE D2.MONTH_YEAR <= ?DATE_TO\n" +
                                    "         GROUP BY MONTH_YEAR\n" +
                                    "        union\n" +
                                    "        SELECT YEAR(MONTH_YEAR) AS 'YEAR',\n" +
                                    "               MONTH(MONTH_YEAR) AS 'MONTH',\n" +
                                    "               CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS MONTH_NAME,\n" +
                                    "               ifnull(NULLIF(IF(('RC' = 'RC' AND VT.TRANS_MODE = 'DR') OR\n" +
                                    "                                ('RC' = 'PY' AND VT.TRANS_MODE = 'CR'),\n" +
                                    "                                -IFNULL(SUM(VT.AMOUNT), 0),\n" +
                                    "                                IFNULL(SUM(VT.AMOUNT), 0)),\n" +
                                    "                             0),\n" +
                                    "                      0) AS RECEIPT,\n" +
                                    "               0 AS PAYMENT\n" +
                                    "\n" +
                                    "          FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                    "                        DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                    "                       NO_OF_MONTH\n" +
                                    "                  FROM (SELECT @rownum := @rownum + 1 AS NO_OF_MONTH\n" +
                                    "                          FROM (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T1,\n" +
                                    "                               (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T2,\n" +
                                    "                               (SELECT 1 UNION\n" +
                                    "                                        SELECT 2 UNION\n" +
                                    "                                                SELECT 3 UNION\n" +
                                    "                                                        SELECT 4\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "\n" +
                                    "                                ) AS T3,\n" +
                                    "                               (SELECT @rownum := -1) AS T0) D1) D2\n" +
                                    "          LEFT JOIN PROJECT_LEDGER AS PL\n" +
                                    "         INNER JOIN MASTER_LEDGER AS ML\n" +
                                    "         INNER JOIN MASTER_LEDGER_GROUP AS MLG\n" +
                                    "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                                    "           AND MLG.GROUP_ID NOT IN (12, 13, 14) ON\n" +
                                    "         PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "          { AND PL.PROJECT_ID IN (?PROJECT_ID) } \n" +
                                    "          LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                                     "        LEFT JOIN MASTER_PROJECT MP\n" +
                                    "             ON MP.PROJECT_ID=VMT.PROJECT_ID\n" +
                                    "         LEFT JOIN MASTER_INSTI_PERFERENCE MIP\n" +
                                    "             ON MIP.CUSTOMERID=MP.CUSTOMERID \n" +
                                    "         INNER JOIN VOUCHER_TRANS AS VT\n" +
                                    "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "            AND VMT.BRANCH_ID = VT.BRANCH_ID\n" +
                                    "           AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO  \n" +
                                    "           AND VMT.STATUS = 1\n" +
                                      "  { AND VT.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                                    "  { AND MP.CUSTOMERID IN (?CUSTOMERID) }\n" +
                                    "           AND VMT.VOUCHER_TYPE = 'RC' ON\n" +
                                    "         PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                                    "           AND PL.LEDGER_ID = VT.LEDGER_ID\n" +
                                    "           AND YEAR(D2.MONTH_YEAR) = YEAR(VMT.VOUCHER_DATE)\n" +
                                    "           AND MONTH(D2.MONTH_YEAR) = MONTH(VMT.VOUCHER_DATE)\n" +
                                    "         WHERE D2.MONTH_YEAR <= ?DATE_TO\n" +
                                    "         GROUP BY MONTH_YEAR) as t\n" +
                                    " GROUP BY T.YEAR, T.MONTH";
                        break;
                    }
                case SQLCommand.VoucherMaster.DataSynStatusbyMonth:
                    {
                        query = "SELECT CAST(CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS CHAR) AS MONTH_NAME,\n" +
                                 "       FNL.BRANCH_OFFICE_CODE,\n" +
                                 "       FNL.BRANCH_OFFICE_NAME,\n" +
                                 "       CASE\n" +
                                 "         WHEN MONTH(FNL.MONTH_YEAR) = MONTH(FNL.VOUCHER_DATE) THEN\n" +
                                 "          FNL.RESULT\n" +
                                 "         ELSE\n" +
                                 "          0\n" +
                                 "       END AS RESULT\n" +
                                 "  FROM (SELECT *\n" +
                                 "          FROM (SELECT *\n" +
                                 "                  FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                 "                                DAYOFMONTH(?DATE_FROM) - 1 DAY) +\n" +
                                 "                               INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                 "                               NO_OF_MONTH\n" +
                                 "                          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" +
                                 "                                  FROM (SELECT 1 UNION\n" +
                                 "                                                SELECT 2 UNION\n" +
                                 "                                                        SELECT 3 UNION\n" +
                                 "                                                                SELECT 4\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "                                        ) AS T1,\n" +
                                 "                                       (SELECT 1 UNION\n" +
                                 "                                                SELECT 2 UNION\n" +
                                 "                                                        SELECT 3 UNION\n" +
                                 "                                                                SELECT 4\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "                                        ) AS T2,\n" +
                                 "                                       (SELECT 1 UNION\n" +
                                 "                                                SELECT 2 UNION\n" +
                                 "                                                        SELECT 3 UNION\n" +
                                 "                                                                SELECT 4\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "\n" +
                                 "                                        ) AS T3,\n" +
                                 "                                       (SELECT @ROWNUM1 := -1) AS T0) D1) AS T\n" +
                                 "                 WHERE T.MONTH_YEAR BETWEEN ?DATE_FROM AND ?DATE_TO) AS T1\n" +
                                 "          JOIN\n" +
                                 "\n" +
                                 "         (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                 "                BO.BRANCH_OFFICE_NAME,\n" +
                                 "                VMT.BRANCH_ID,\n" +
                                 "                COUNT(VMT.VOUCHER_ID) AS RESULT,\n" +
                                 "                VMT.VOUCHER_DATE\n" +
                                 "           FROM VOUCHER_MASTER_TRANS VMT\n" +
                                 "           LEFT JOIN BRANCH_OFFICE BO\n" +
                                 "             ON BO.BRANCH_OFFICE_ID = VMT.BRANCH_ID\n" +
                                 "          WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                 "          GROUP BY BRANCH_ID,YEAR(VOUCHER_DATE),MONTH(VOUCHER_DATE)) AS T2) AS FNL\n" +
                                 " { WHERE FNL.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }\n" +
                                 " ORDER BY FNL.BRANCH_OFFICE_CODE, YEAR(MONTH_YEAR), MONTH(MONTH_YEAR)";


                        break;
                    }
                case SQLCommand.VoucherMaster.DataSynStatusNobyMonth:
                    {
                        query = "SELECT YEAR(D2.MONTH_YEAR) AS 'YEAR',\n" +
                        "       MONTH(D2.MONTH_YEAR) AS 'MONTH',\n" +
                        "       BO.BRANCH_OFFICE_NAME,\n" +
                        "       BO.BRANCH_OFFICE_CODE,\n" +
                        "       CONCAT(LEFT(MONTHNAME(D2.MONTH_YEAR), 3), '-', YEAR(D2.MONTH_YEAR)) AS MONTH_NAME,\n" +
                        "       CAST('N' AS CHAR) AS Result\n" +
                        "  FROM BRANCH_OFFICE BO,\n" +
                        "       VOUCHER_MASTER_TRANS VMT,\n" +
                        "       (SELECT (?DATE_FROM - INTERVAL DAYOFMONTH(?DATE_FROM) - 1 DAY) +\n" +
                        "               INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                        "               NO_OF_MONTH\n" +
                        "          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" +
                        "                  FROM (SELECT 1 UNION\n" +
                        "                                SELECT 2 UNION\n" +
                        "                                        SELECT 3 UNION\n" +
                        "                                                SELECT 4\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "                        ) AS T1,\n" +
                        "                       (SELECT 1 UNION\n" +
                        "                                SELECT 2 UNION\n" +
                        "                                        SELECT 3 UNION\n" +
                        "                                                SELECT 4\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "                        ) AS T2,\n" +
                        "                       (SELECT 1 UNION\n" +
                        "                                SELECT 2 UNION\n" +
                        "                                        SELECT 3 UNION\n" +
                        "                                                SELECT 4\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "\n" +
                        "                        ) AS T3,\n" +
                        "                       (SELECT @ROWNUM1 := -1) AS T0) AS D1) AS D2\n" +
                        " WHERE D2.MONTH_YEAR <= ?DATE_TO\n" +
                        "   AND BO.BRANCH_OFFICE_ID NOT IN\n" +
                        "       (SELECT BRANCH_ID\n" +
                        "          FROM VOUCHER_MASTER_TRANS\n" +
                        "         WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)\n" +
                        " GROUP BY BRANCH_OFFICE_CODE, MONTH_YEAR";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchDataSynStatusProjectWise:
                    {
                        #region OldQuery
                        //query ="SELECT BRANCH_OFFICE_NAME,TT.PROJECT,TT.MONTH_NAME, TT.RESULT\n" +
                        //        " FROM\n" + 
                        //        "(SELECT CAST(CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS CHAR) AS MONTH_NAME,\n" + 
                        //        "       FNL.BRANCH_OFFICE_CODE,\n" + 
                        //        "       FNL.BRANCH_OFFICE_NAME,\n" + 
                        //        "       CASE\n" + 
                        //        "         WHEN MONTH(FNL.MONTH_YEAR) = MONTH(FNL.VOUCHER_DATE) THEN\n" + 
                        //        "          FNL.RESULT\n" + 
                        //        "         ELSE\n" + 
                        //        "          0\n" + 
                        //        "       END AS RESULT,MONTH_YEAR,PROJECT\n" + 
                        //        "  FROM (SELECT *\n" + 
                        //        "          FROM (SELECT *\n" + 
                        //        "                  FROM (SELECT (?DATE_FROM - INTERVAL\n" + 
                        //        "                                DAYOFMONTH(?DATE_FROM) - 1 DAY) +\n" + 
                        //        "                               INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" + 
                        //        "                               NO_OF_MONTH\n" + 
                        //        "                          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" + 
                        //        "                                  FROM (SELECT 1 UNION\n" + 
                        //        "                                                SELECT 2 UNION\n" + 
                        //        "                                                        SELECT 3 UNION\n" + 
                        //        "                                                                SELECT 4\n" + 
                        //        "                                        ) AS T1,\n" + 
                        //        "                                       (SELECT 1 UNION\n" + 
                        //        "                                                SELECT 2 UNION\n" + 
                        //        "                                                        SELECT 3 UNION\n" + 
                        //        "                                                                SELECT 4\n" + 
                        //        "                                        ) AS T2,\n" + 
                        //        "                                       (SELECT 1 UNION\n" + 
                        //        "                                                SELECT 2 UNION\n" + 
                        //        "                                                        SELECT 3 UNION\n" + 
                        //        "                                                                SELECT 4\n" + 
                        //        "                                        ) AS T3,\n" + 
                        //        "                                       (SELECT @ROWNUM1 := -1) AS T0) D1) AS T\n" + 
                        //        "                 WHERE T.MONTH_YEAR BETWEEN ?DATE_FROM AND ?DATE_TO) AS T1\n" + 
                        //        "          JOIN\n" + 
                        //        "         (SELECT BO.BRANCH_OFFICE_CODE,\n" + 
                        //        "                BO.BRANCH_OFFICE_NAME,\n" + 
                        //        "                VMT.BRANCH_ID,\n" + 
                        //        "                COUNT(VMT.VOUCHER_ID) AS RESULT,\n" + 
                        //        "                VMT.VOUCHER_DATE,BO.INCHARGE_NAME,PROJECT\n" + 
                        //        "           FROM VOUCHER_MASTER_TRANS VMT\n" + 
                        //        "           LEFT JOIN BRANCH_OFFICE BO\n" + 
                        //        "             ON BO.BRANCH_OFFICE_ID = VMT.BRANCH_ID\n" + 
                        //        "             LEFT JOIN MASTER_PROJECT MP\n" + 
                        //        "            ON VMT.PROJECT_ID=MP.PROJECT_ID\n" + 
                        //        "            LEFT JOIN PROJECT_BRANCH PB\n" + 
                        //        "              ON PB.BRANCH_ID=BO.BRANCH_OFFICE_ID\n" + 
                        //        "              AND PB.PROJECT_ID=MP.PROJECT_ID\n" + 
                        //        "          WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" + 
                        //        "AND BO.BRANCH_OFFICE_ID=?BRANCH_OFFICE_ID\n" + 
                        //        "          GROUP BY BRANCH_ID,PROJECT,YEAR(VOUCHER_DATE),MONTH(VOUCHER_DATE)) AS T2) AS FNL\n" + 
                        //        "ORDER BY RESULT DESC) AS TT\n" + 
                        //        "GROUP BY BRANCH_OFFICE_CODE,project,YEAR(MONTH_YEAR),MONTH(MONTH_YEAR)\n" + 
                        //        "ORDER BY project,YEAR(MONTH_YEAR),MONTH(MONTH_YEAR)";
                        #endregion

                        query = "SELECT FT.MONTH_NAME,FT.BRANCH_OFFICE_CODE,FT.BRANCH_OFFICE_NAME,ROUND(SUM(FT.RESULT),0) AS RESULT, FT.MONTH_YEAR,FT.PROJECT FROM (SELECT CAST(CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS CHAR) AS MONTH_NAME,\n" +
                                "       FNL.BRANCH_OFFICE_CODE,\n" +
                                "       FNL.BRANCH_OFFICE_NAME,\n" +
                                "       CASE\n" +
                                "         WHEN MONTH(FNL.MONTH_YEAR) = MONTH(FNL.VOUCHER_DATE) THEN\n" +
                                "          FNL.RESULT\n" +
                                "         ELSE\n" +
                                "          0\n" +
                                "       END AS RESULT,\n" +
                                "       MONTH_YEAR,\n" +
                                "       PROJECT\n" +
                                "  FROM (SELECT *\n" +
                                "          FROM (SELECT *\n" +
                                "                  FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                "                                DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                "                               NO_OF_MONTH\n" +
                                "                          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" +
                                "                                  FROM (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T1,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T2,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T3,\n" +
                                "                                       (SELECT @ROWNUM1 := -1) AS T0) D1) AS T\n" +
                                "                 WHERE T.MONTH_YEAR BETWEEN ?DATE_FROM AND ?DATE_TO) AS T1\n" +
                                "          JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                      BO.BRANCH_OFFICE_NAME,\n" +
                                "                      MP.PROJECT,\n" +
                                "                      TTT.RESULT,\n" +
                                "                      TTT.INCHARGE_NAME,\n" +
                                "                      TTT.VOUCHER_DATE,\n" +
                                "                      TTT.BRANCH_ID\n" +
                                "                 FROM PROJECT_BRANCH PB\n" +
                                "                 LEFT JOIN MASTER_PROJECT MP\n" +
                                "                   ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                 LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                   ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "\n" +
                                "                 LEFT JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                                  BO.BRANCH_OFFICE_NAME,\n" +
                                "                                  VMT.BRANCH_ID,\n" +
                                "                                  COUNT(VMT.VOUCHER_ID) AS RESULT,\n" +
                                "                                  VMT.VOUCHER_DATE,\n" +
                                "                                  BO.INCHARGE_NAME,\n" +
                                "                                  PROJECT AS PRJ,\n" +
                                "                                  VMT.PROJECT_ID\n" +
                                "                             FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                             LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                               ON BO.BRANCH_OFFICE_ID = VMT.BRANCH_ID\n" +
                                "                             LEFT JOIN MASTER_PROJECT MP\n" +
                                "                               ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                            WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "                             { AND VMT.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                                "                            GROUP BY BRANCH_ID,\n" +
                                "                                     PROJECT,\n" +
                                "                                     YEAR(VOUCHER_DATE),\n" +
                                "                                     MONTH(VOUCHER_DATE)) AS TTT\n" +
                                "                   on MP.PROJECT_ID = TTT.PROJECT_ID\n" +
                                "               { WHERE BO.BRANCH_OFFICE_ID IN (?BRANCH_OFFICE_ID) } GROUP BY PROJECT,YEAR(VOUCHER_DATE),MONTH(VOUCHER_DATE)) AS T2) AS FNL\n" +
                                "\n" +
                                " ORDER BY BRANCH_OFFICE_CODE, PROJECT DESC )AS FT GROUP BY PROJECT,MONTH_NAME,MONTH_YEAR";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchDataSynStatusProjectUserWise:
                    {
                        query = "SELECT FT.MONTH_NAME,FT.BRANCH_OFFICE_CODE,FT.BRANCH_OFFICE_NAME,ROUND(SUM(FT.RESULT),0) AS RESULT, FT.MONTH_YEAR,FT.PROJECT FROM (SELECT CAST(CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS CHAR) AS MONTH_NAME,\n" +
                                "       FNL.BRANCH_OFFICE_CODE,\n" +
                                "       FNL.BRANCH_OFFICE_NAME,\n" +
                                "       CASE\n" +
                                "         WHEN MONTH(FNL.MONTH_YEAR) = MONTH(FNL.VOUCHER_DATE) THEN\n" +
                                "          FNL.RESULT\n" +
                                "         ELSE\n" +
                                "          0\n" +
                                "       END AS RESULT,\n" +
                                "       MONTH_YEAR,\n" +
                                "       PROJECT\n" +
                                "  FROM (SELECT *\n" +
                                "          FROM (SELECT *\n" +
                                "                  FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                "                                DAYOFMONTH(?DATE_FROM) - 1 DAY) + INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                "                               NO_OF_MONTH\n" +
                                "                          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" +
                                "                                  FROM (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T1,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T2,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "\n" +
                                "                                        ) AS T3,\n" +
                                "                                       (SELECT @ROWNUM1 := -1) AS T0) D1) AS T\n" +
                                "                 WHERE T.MONTH_YEAR BETWEEN ?DATE_FROM AND ?DATE_TO) AS T1\n" +
                                "          JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                      BO.BRANCH_OFFICE_NAME,\n" +
                                "                      MP.PROJECT,\n" +
                                "                      TTT.RESULT,\n" +
                                "                      TTT.INCHARGE_NAME,\n" +
                                "                      TTT.VOUCHER_DATE,\n" +
                                "                      TTT.BRANCH_ID\n" +
                                "                 FROM PROJECT_BRANCH PB\n" +
                                "                 LEFT JOIN MASTER_PROJECT MP\n" +
                                "                   ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                 LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                   ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "                INNER JOIN PROJECT_USER PU\n" +
                                "               ON MP.PROJECT_ID = PU.PROJECT_ID\n" +
                                "               AND PU.USER_ID =?USER_ID\n" +
                                "\n" +
                                "                 LEFT JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                                  BO.BRANCH_OFFICE_NAME,\n" +
                                "                                  VMT.BRANCH_ID,\n" +
                                "                                  COUNT(VMT.VOUCHER_ID) AS RESULT,\n" +
                                "                                  VMT.VOUCHER_DATE,\n" +
                                "                                  BO.INCHARGE_NAME,\n" +
                                "                                  PROJECT AS PRJ,\n" +
                                "                                  VMT.PROJECT_ID\n" +
                                "                             FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                             LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                               ON BO.BRANCH_OFFICE_ID = VMT.BRANCH_ID\n" +
                                "                             LEFT JOIN MASTER_PROJECT MP\n" +
                                "                               ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                             INNER JOIN PROJECT_USER PU\n" +
                                "                             ON MP.PROJECT_ID = PU.PROJECT_ID\n" +
                                "                             AND PU.USER_ID =?USER_ID\n" +
                                "                            WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "                             { AND VMT.BRANCH_ID IN (?BRANCH_OFFICE_ID) }\n" +
                                "                            GROUP BY BRANCH_ID,\n" +
                                "                                     PROJECT,\n" +
                                "                                     YEAR(VOUCHER_DATE),\n" +
                                "                                     MONTH(VOUCHER_DATE)) AS TTT\n" +
                                "                   on MP.PROJECT_ID = TTT.PROJECT_ID\n" +
                                "               { WHERE BO.BRANCH_OFFICE_ID IN (?BRANCH_OFFICE_ID) } GROUP BY PROJECT,YEAR(VOUCHER_DATE),MONTH(VOUCHER_DATE)) AS T2) AS FNL\n" +
                                "\n" +
                                " ORDER BY BRANCH_OFFICE_CODE, PROJECT DESC )AS FT GROUP BY PROJECT,MONTH_NAME,MONTH_YEAR";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchNonConformityBranches:
                    {
                        query = "SELECT TD.BRANCH_OFFICE_ID,TD.BRANCH_OFFICE_CODE,\n" +
                                "BRANCH_OFFICE_NAME,\n" +
                                "GROUP_CONCAT(MONTH_NAME ORDER BY BRANCH_OFFICE_CODE,YEAR(MONTH_YEAR),MONTH(MONTH_YEAR)) AS MONTH_NAME,INCHARGE_NAME,BRANCH_EMAIL_ID,MOBILE_NO\n" +
                                " FROM\n" +
                                "(SELECT TT.RESULT,TT.BRANCH_OFFICE_CODE,TT.BRANCH_OFFICE_ID,TT.MONTH_NAME,TT.MONTH_YEAR,BRANCH_OFFICE_NAME,TT.INCHARGE_NAME,TT.BRANCH_EMAIL_ID,TT.MOBILE_NO \n" +
                                " FROM\n" +
                                "(SELECT CAST(CONCAT(LEFT(MONTHNAME(MONTH_YEAR), 3), '-', YEAR(MONTH_YEAR)) AS CHAR) AS MONTH_NAME,\n" +
                                "       FNL.BRANCH_OFFICE_CODE,\n" +
                                "       FNL.BRANCH_OFFICE_ID,\n" +
                                "       FNL.BRANCH_OFFICE_NAME,\n" +
                                "       CASE\n" +
                                "         WHEN MONTH(FNL.MONTH_YEAR) = MONTH(FNL.VOUCHER_DATE) THEN\n" +
                                "          FNL.RESULT\n" +
                                "         ELSE\n" +
                                "          0\n" +
                                "       END AS RESULT,MONTH_YEAR,INCHARGE_NAME,BRANCH_EMAIL_ID,MOBILE_NO\n" +
                                "  FROM (SELECT *\n" +
                                "          FROM (SELECT *\n" +
                                "                  FROM (SELECT (?DATE_FROM - INTERVAL\n" +
                                "                                DAYOFMONTH(?DATE_FROM) - 1 DAY) +\n" +
                                "                               INTERVAL NO_OF_MONTH MONTH AS MONTH_YEAR,\n" +
                                "                               NO_OF_MONTH\n" +
                                "                          FROM (SELECT @ROWNUM1 := @ROWNUM1 + 1 AS NO_OF_MONTH\n" +
                                "                                  FROM (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "                                        ) AS T1,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "                                        ) AS T2,\n" +
                                "                                       (SELECT 1 UNION\n" +
                                "                                                SELECT 2 UNION\n" +
                                "                                                        SELECT 3 UNION\n" +
                                "                                                                SELECT 4\n" +
                                "                                        ) AS T3,\n" +
                                "                                       (SELECT @ROWNUM1 := -1) AS T0) D1) AS T\n" +
                                "                 WHERE T.MONTH_YEAR BETWEEN ?DATE_FROM AND ?DATE_TO) AS T1\n" +
                               "          JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                      BO.BRANCH_OFFICE_NAME,\n" +
                                "                       BO.BRANCH_EMAIL_ID,\n" +
                                "                      MP.PROJECT,\n" +
                                "                      TTT.RESULT,\n" +
                                "                      BO.INCHARGE_NAME,\n" +
                                "                      TTT.VOUCHER_DATE,\n" +
                                "                      TTT.BRANCH_ID,BO.BRANCH_OFFICE_ID,BO.MOBILE_NO\n" +
                                "                 FROM PROJECT_BRANCH PB\n" +
                                "                 LEFT JOIN MASTER_PROJECT MP\n" +
                                "                   ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                 LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                   ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "\n" +
                                "                 LEFT JOIN (SELECT BO.BRANCH_OFFICE_CODE,\n" +
                                "                                  BO.BRANCH_OFFICE_NAME,\n" +
                                "                                   BO.BRANCH_EMAIL_ID,\n" +
                                "                                  VMT.BRANCH_ID,\n" +
                                "                                  COUNT(VMT.VOUCHER_ID) AS RESULT,\n" +
                                "                                  VMT.VOUCHER_DATE,\n" +
                                "                                  BO.INCHARGE_NAME,\n" +
                                "                                  PROJECT AS PRJ,\n" +
                                "                                  VMT.PROJECT_ID\n" +
                                "                             FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                             LEFT JOIN BRANCH_OFFICE BO\n" +
                                "                               ON BO.BRANCH_OFFICE_ID = VMT.BRANCH_ID\n" +
                                "                             LEFT JOIN MASTER_PROJECT MP\n" +
                                "                               ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                                "                            WHERE VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "                            GROUP BY BRANCH_ID,\n" +
                                "                                     PROJECT,\n" +
                                "                                     YEAR(VOUCHER_DATE),\n" +
                                "                                     MONTH(VOUCHER_DATE)) AS TTT\n" +
                                "                   on MP.PROJECT_ID = TTT.PROJECT_ID\n" +
                                "            GROUP BY BRANCH_OFFICE_ID,YEAR(VOUCHER_DATE),MONTH(VOUCHER_DATE)) AS T2) AS FNL\n" +
                                "ORDER BY RESULT DESC) AS TT\n" +
                                "GROUP BY BRANCH_OFFICE_CODE,YEAR(MONTH_YEAR),MONTH(MONTH_YEAR)) AS TD\n" +
                                "WHERE RESULT=0\n" +
                                " { AND BRANCH_OFFICE_ID=?BRANCH_OFFICE_ID } \n" +
                                "GROUP BY BRANCH_OFFICE_CODE\n" +
                                "ORDER BY BRANCH_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherMasterById:
                    {
                        query = "SELECT VM.VOUCHER_ID,VM.AMENDMENT_FLAG,\n" +
                                "       VM.VOUCHER_NO,\n" +
                                "       DATE_FORMAT(VM.VOUCHER_DATE,'%d/%m/%Y') AS VOUCHER_DATE,\n" +
                                "CASE VM.VOUCHER_TYPE\n" +
                                "        WHEN 'RC' THEN\n" +
                                "          'Receipts'\n" +
                                "        WHEN 'PY' THEN\n" +
                                "          'Payments'\n" +
                                "        WHEN 'CN' THEN\n" +
                                "          'Contra'\n" +
                                "        ELSE\n" +
                                "         'JOURNAL'\n" +
                                "        END AS VOUCHER_TYPE,\n" +
                                "       VM.PROJECT_ID,\n" +
                                "       MP.PROJECT,\n" +
                                "       VM.DONOR_ID,\n" +
                                "       MD.NAME,\n" +
                                "       VT.AMOUNT,\n" +
                                "       VT.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       VM.BRANCH_ID,\n" +
                                "       BF.BRANCH_OFFICE_NAME,BF.BRANCH_OFFICE_CODE,\n" +
                                "       VM.NARRATION,VM.NAME_ADDRESS,AH.REMARKS,MP.PROJECT \n" +
                                "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                                " INNER JOIN VOUCHER_TRANS AS VT\n" +
                                "    ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                "   AND VT.BRANCH_ID = VM.BRANCH_ID\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD AS MD\n" +
                                "    ON VM.DONOR_ID = MD.DONAUD_ID\n" +
                                " INNER JOIN BRANCH_OFFICE AS BF\n" +
                                "    ON BF.BRANCH_OFFICE_ID = VM.BRANCH_ID\n" +
                                " INNER JOIN MASTER_PROJECT AS MP\n" +
                                "    ON MP.PROJECT_ID = VM.PROJECT_ID\n" +
                                " LEFT JOIN AMENDMENT_HISTORY AS AH \n" +
                                "    ON AH.VOUCHER_ID=VM.VOUCHER_ID AND AH.BRANCH_ID=VM.BRANCH_ID \n" +
                                " WHERE VM.BRANCH_ID =?BRANCH_OFFICE_ID \n" +
                                "   AND VM.VOUCHER_ID =?VOUCHER_ID \n" +
                                "   AND VM.LOCATION_ID =?LOCATION_ID \n" +
                                " GROUP BY VM.VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchDeducteeTypes:
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
                case SQLCommand.VoucherMaster.FetchTaxDetails:
                    {
                        query = "SELECT T.TDS_POLICY_ID,\n" +
                                "       T.NATURE_PAY_ID,\n" +
                                "       T.NAME AS NATURE_OF_PAYMENTS,\n" +
                                "      DATE_FORMAT(T.APPLICABLE_FROM,'%d/%m/%Y') AS APPLICABLE_FROM,\n" +
                                "       T.TAX_TYPE_NAME,\n" +
                                "       SUM(T.TDS_RATE) AS TDS_RATE,\n" +
                                "       SUM(T.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT,\n" +
                                "       SUM(T.TDS_EXEMPTION_LIMIT_WITHOUT_PAN) AS TDSEXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "       SUM(T.TDS_RATE_WITHOUT_PAN) AS TDSRATE_WITHOUT_PAN,\n" +
                                "       SUM(T.SUR_RATE) AS SUR_RATE,\n" +
                                "       SUM(T.SUR_EXEMPTION) AS SUR_EXEMPTION,\n" +
                                "       SUM(T.ED_CESS_RATE) AS ED_CESS_RATE,\n" +
                                "       SUM(T.ED_CESS_EXEMPTION) AS ED_CESS_EXEMPTION,\n" +
                                "       SUM(T.SEC_ED_CESS_RATE) AS SEC_ED_CESS_RATE,\n" +
                                "       SUM(T.SEC_ED_CESS_EXEMPTION) AS SEC_ED_CESS_EXEMPTION\n" +
                                "\n" +
                                "  FROM (SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS TDS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 1\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 2\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS SUR_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 3\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) ED_CESS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 4\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS SEC_ED_CESS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 5\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "         ORDER BY NAME) AS T\n" +
                                " GROUP BY T.APPLICABLE_FROM, T.NAME\n" +
                                " ORDER BY NAME";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckTransExistsByDateProject:
                    {
                        query = "SELECT COUNT(*) \n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE > ?VOUCHER_DATE\n" +
                                "   AND VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS = 1 ";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckBranchTransExist:
                    {
                        query = "SELECT COUNT(*) \n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.BRANCH_ID = ?BRANCH_OFFICE_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                " AND VMT.STATUS = 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVouchersInOtherProjectsOrDates:
                    {
                        query = @"SELECT VMT.VOUCHER_ID, VMT.PROJECT_ID, MP.PROJECT, VMT.VOUCHER_DATE, VMT.VOUCHER_NO, VMT.VOUCHER_TYPE, VMT.VOUCHER_SUB_TYPE
                                    FROM VOUCHER_MASTER_TRANS VMT
                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = VMT.PROJECT_ID
                                    WHERE VMT.BRANCH_ID = ?BRANCH_ID AND VMT.LOCATION_ID=?LOCATION_ID {AND VMT.PROJECT_ID IN (?PROJECT_ID)}";
                        //AND (VMT.PROJECT_ID <> ?PROJECT_ID OR (VMT.PROJECT_ID = ?PROJECT_ID AND VMT.VOUCHER_DATE NOT BETWEEN ?DATE_FROM AND ?DATE_TO) )
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchBRSByMaterialized:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
                        "       T.LEDGER_ID,VMT.VOUCHER_SUB_TYPE,T.TRANS_MODE,VMT.VOUCHER_TYPE,\n" +
                        "       MT.CHEQUE_NO,\n" +
                        "       VMT.VOUCHER_DATE,\n" +
                        "       T.LEDGER_CODE,\n" +
                        "       MATERIALIZED_ON as 'DATE',\n" +
                        "       T.LEDGER_NAME,\n" +
                        "       CASE\n" +
                        "         WHEN VMT.VOUCHER_TYPE = 'PY' THEN\n" +
                        "        IF(T.TRANS_MODE ='DR',T.AMOUNT, -T.AMOUNT)\n" +
                       "         WHEN (VMT.VOUCHER_TYPE = 'CN' AND T.TRANS_MODE ='DR')  THEN\n" +
                        "        T.AMOUNT\n" +
                        "         ELSE\n" +
                        "          0.00\n" +
                        "       END AS 'UnCleared',\n" +
                        "       CASE\n" +
                        "         WHEN VMT.VOUCHER_TYPE = 'RC' THEN\n" +
                        "   IF(T.TRANS_MODE ='CR',T.AMOUNT, -T.AMOUNT)\n" +
                        "    WHEN (VMT.VOUCHER_TYPE = 'CN' AND T.TRANS_MODE ='CR') THEN\n" +
                        "    T.AMOUNT\n" +
                        "         ELSE\n" +
                        "          0.00\n" +
                        "       END AS 'Unrealised'\n" +
                        "  FROM MASTER_PROJECT PL\n" +
                        " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "    ON PL.PROJECT_ID = VMT.PROJECT_ID AND VMT.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
                        " INNER JOIN VOUCHER_TRANS MT\n" +
                        "    ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        "\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                         " INNER JOIN\n" +
                        "(SELECT MT.VOUCHER_ID,MT.LEDGER_ID,ML.GROUP_ID,ML.LEDGER_CODE,ML.LEDGER_NAME,MT.AMOUNT,MT.TRANS_MODE,MT.SEQUENCE_NO\n" +
                                  "FROM MASTER_LEDGER AS ML\n" +
                                  "INNER JOIN VOUCHER_TRANS AS MT\n" +
                                  "ON MT.LEDGER_ID=ML.LEDGER_ID\n" +
                                  "INNER JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                                  " ON VMT.VOUCHER_ID = MT.VOUCHER_ID AND VMT.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
                                  "WHERE (VMT.VOUCHER_TYPE IN ('RC', 'PY') AND ML.GROUP_ID NOT IN(12)) OR (VMT.VOUCHER_TYPE = 'CN' {AND ML.LEDGER_ID NOT IN (?LEDGER_ID)})) AS T\n" +
                        " ON VMT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        " WHERE\n" +
                        "   ML.LEDGER_SUB_TYPE = 'BK'\n" +
                        "   AND VMT.STATUS = 1\n" +
                        "   AND PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "   {AND ML.LEDGER_ID IN (?LEDGER_ID)}\n" +
                        "   AND ((MT.MATERIALIZED_ON > ?DATE_AS_ON AND VMT.VOUCHER_DATE <= ?DATE_AS_ON)\n" +
                        "        OR IF(MT.MATERIALIZED_ON IS NULL,VMT.VOUCHER_DATE<=?DATE_AS_ON,''))\n" +
                        "   AND VMT.VOUCHER_TYPE IN ('PY', 'RC','CN')  AND VMT.VOUCHER_SUB_TYPE NOT IN ('FD') AND VMT.BRANCH_ID =?BRANCH_OFFICE_ID\n" +
                        " ORDER BY VOUCHER_DATE,CHEQUE_NO DESC, MT.VOUCHER_ID, T.SEQUENCE_NO";
                        break;
                    }
            }

            return query;
        }

        #endregion Bank SQL
    }
}

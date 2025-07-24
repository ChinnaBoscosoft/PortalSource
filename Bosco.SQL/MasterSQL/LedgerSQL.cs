using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LedgerSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LedgerBank).FullName)
            {
                query = GetLedgerSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Ledger details.
        /// </summary>
        /// <returns></returns>
        private string GetLedgerSQL()
        {
            string query = "";
            SQLCommand.LedgerBank sqlCommandId = (SQLCommand.LedgerBank)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LedgerBank.Add:
                    {
                        query = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "GROUP_ID,  " +
                                    "LEDGER_TYPE, " +
                                    "LEDGER_SUB_TYPE, " +
                                    "BANK_ACCOUNT_ID, " +
                                    "IS_COST_CENTER,NOTES, SORT_ID, IS_BANK_INTEREST_LEDGER, IS_INKIND_LEDGER, IS_DEPRECIATION_LEDGER," +
                                    "IS_ASSET_GAIN_LEDGER, IS_ASSET_LOSS_LEDGER, IS_DISPOSAL_LEDGER," +
                                    "IS_BANK_FD_PENALTY_LEDGER, IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER," +
                                    "BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID, FD_INVESTMENT_TYPE_ID, DATE_CLOSED, CLOSED_BY) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "?GROUP_ID,  " +
                                    "?LEDGER_TYPE, " +
                                    "?LEDGER_SUB_TYPE, " +
                                    "?BANK_ACCOUNT_ID, " +
                                    "?IS_COST_CENTER, ?NOTES,?SORT_ID, ?IS_BANK_INTEREST_LEDGER, ?IS_INKIND_LEDGER, ?IS_DEPRECIATION_LEDGER," +
                                    "?IS_ASSET_GAIN_LEDGER, ?IS_ASSET_LOSS_LEDGER, ?IS_DISPOSAL_LEDGER," +
                                    "?IS_BANK_FD_PENALTY_LEDGER, ?IS_BANK_SB_INTEREST_LEDGER, ?IS_BANK_COMMISSION_LEDGER," +
                                    "?BUDGET_GROUP_ID, ?BUDGET_SUB_GROUP_ID, ?FD_INVESTMENT_TYPE_ID, ?DATE_CLOSED, ?CLOSED_BY) ";
                        break;
                    }
                case SQLCommand.LedgerBank.Update:
                    {
                        query = "UPDATE MASTER_LEDGER SET " +
                                "LEDGER_CODE =?LEDGER_CODE, " +
                                "LEDGER_NAME =?LEDGER_NAME, " +
                                "GROUP_ID=?GROUP_ID, " +
                                "LEDGER_TYPE=?LEDGER_TYPE," +
                                "LEDGER_SUB_TYPE=?LEDGER_SUB_TYPE," +
                                "BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID, " +
                                "IS_COST_CENTER=?IS_COST_CENTER, " +
                                "IS_BANK_INTEREST_LEDGER=?IS_BANK_INTEREST_LEDGER, " +
                                "NOTES=?NOTES,IS_INKIND_LEDGER=?IS_INKIND_LEDGER,IS_DEPRECIATION_LEDGER=?IS_DEPRECIATION_LEDGER, " +
                                "IS_ASSET_GAIN_LEDGER=?IS_ASSET_GAIN_LEDGER,IS_ASSET_LOSS_LEDGER=?IS_ASSET_LOSS_LEDGER," +
                                "IS_BANK_FD_PENALTY_LEDGER=?IS_BANK_FD_PENALTY_LEDGER, IS_BANK_SB_INTEREST_LEDGER=?IS_BANK_SB_INTEREST_LEDGER," +
                                "IS_BANK_COMMISSION_LEDGER=?IS_BANK_COMMISSION_LEDGER," +
                                "IS_DISPOSAL_LEDGER=?IS_DISPOSAL_LEDGER,BUDGET_GROUP_ID= ?BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID=?BUDGET_SUB_GROUP_ID, FD_INVESTMENT_TYPE_ID=?FD_INVESTMENT_TYPE_ID," +
                                "DATE_CLOSED=?DATE_CLOSED, CLOSED_BY=?CLOSED_BY" +
                                " WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.Delete:
                    {
                        query = "DELETE FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountDelete:
                    {
                        query = "DELETE FROM MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.Fetch:
                    {
                        query = "SELECT ML.LEDGER_ID,\n" +
                                 "       LEDGER_CODE,\n" +
                                 "       DEDUTEE_TYPE_ID,\n" +
                                 "       NATURE_OF_PAYMENT_ID,\n" +
                                 "       IS_TDS_LEDGER,\n" +
                                 "       CREDITORS_PROFILE_ID,\n" +
                                 "       NAME,\n" +
                                 "       ADDRESS,\n" +
                                 "       STATE_ID,\n" +
                                 "       COUNTRY_ID,\n" +
                                 "       PIN_CODE,\n" +
                                 "       CONTACT_NUMBER,\n" +
                                 "       EMAIL,\n" +
                                 "       LEDGER_NAME,\n" +
                                 "       PAN_NUMBER,\n" +
                                 "       GROUP_ID,\n" +
                                 "       CLM.CON_LEDGER_ID,\n" +
                                 "       LEDGER_TYPE,\n" +
                                 "       LEDGER_SUB_TYPE,\n" +
                                 "       BANK_ACCOUNT_ID,\n" +
                                 "       IS_COST_CENTER,\n" +
                                 "       IS_BANK_INTEREST_LEDGER,\n" +
                                 "       NOTES,\n" +
                                 "       LEDGER_TYPE,\n" +
                                 "       IS_DEPRECIATION_LEDGER,\n" +
                                 "       IS_INKIND_LEDGER,\n" +
                                 "       IS_ASSET_GAIN_LEDGER,\n" +
                                 "       IS_ASSET_LOSS_LEDGER, IS_DISPOSAL_LEDGER, IS_BANK_FD_PENALTY_LEDGER, IS_BANK_SB_INTEREST_LEDGER, IS_BANK_COMMISSION_LEDGER,\n" +
                                 "       BUDGET_GROUP_ID, BUDGET_SUB_GROUP_ID,FD_INVESTMENT_TYPE_ID, DATE_CLOSED \n" +
                                 "  FROM MASTER_LEDGER AS ML\n" +
                                 "  LEFT JOIN tds_credtiors_profile AS TCP\n" +
                                 "    ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                                 "  LEFT JOIN CONGREGATION_LEDGER_MAP CLM\n" +
                                 "     ON CLM.LEDGER_ID = ML.LEDGER_ID\n" +
                                 " WHERE ML.LEDGER_ID = ?LEDGER_ID\n" +
                                 "   AND STATUS = 0";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchAll:
                    {
                        /*ML.ACCESS_FLAG 2- It is branch default ledgers and
                        should not be modified and sent to branch in project category and ledger mapping*/
                        query = "SELECT " +
                                  "ML.LEDGER_ID, MG.GROUP_ID, " +
                                   "ML.LEDGER_CODE AS 'Code', " +
                                   "ML.LEDGER_NAME AS 'Name',  " +
                                   "ML.ACCESS_FLAG,  " +
                                    "CASE WHEN ML.LEDGER_SUB_TYPE='BK' " +
                                         "THEN 'Bank Accounts' " +
                                      "ELSE CASE WHEN ML.LEDGER_SUB_TYPE='FD' " +
                                              "THEN 'Fixed Deposits' " +
                                     "ELSE  MG.LEDGER_GROUP END END  AS 'Group', " +
                                   "ML.LEDGER_TYPE, " +
                                   "ML.LEDGER_SUB_TYPE, " +
                                   "ML.BANK_ACCOUNT_ID, " +
                                   "MN.NATURE_ID, " +
                                   "MN.NATURE AS 'Nature', " +
                                   "IF(BG.BUDGET_GROUP_ID=0,'', BG.BUDGET_GROUP) AS 'Budget Group', " +
                                   "IF(BSG.BUDGET_SUB_GROUP_ID=0,'',BSG.BUDGET_SUB_GROUP) AS 'Budget Sub Group' " +
                             " FROM " +
                                 "MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP MG " +
                                 "ON ML.GROUP_ID=MG.GROUP_ID AND ML.STATUS=0 " +
                            //  AND ML.ACCESS_FLAG<>2 
                        " AND ML.IS_BRANCH_LEDGER =0 LEFT JOIN MASTER_NATURE MN on MN.NATURE_ID =MG.NATURE_ID" +
                        " LEFT JOIN BUDGET_GROUP BG ON BG.BUDGET_GROUP_ID = ML.BUDGET_GROUP_ID LEFT JOIN BUDGET_SUB_GROUP BSG ON BSG.BUDGET_SUB_GROUP_ID = ML.BUDGET_SUB_GROUP_ID" +
                        " ORDER BY ACCESS_FLAG DESC, ML.LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchAllWithNature:
                    {
                        /*ML.ACCESS_FLAG 2- It is branch default ledgers and
                        should not be modified and sent to branch in project category and ledger mapping*/
                        query = "SELECT ML.LEDGER_ID, MG.GROUP_ID, MG.NATURE_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, MG.LEDGER_GROUP, ML.ACCESS_FLAG\n" +
                                "FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP MG ON ML.GROUP_ID = MG.GROUP_ID\n" +
                                 "WHERE ML.STATUS=0 AND ML.ACCESS_FLAG<>2 AND ML.IS_BRANCH_LEDGER =0";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBranchLedger:
                    {
                        query = "SELECT ML.LEDGER_ID, MLG.GROUP_ID, " +
                             "ML.LEDGER_CODE AS 'Code', " +
                            "ML.LEDGER_NAME AS 'Name',  " +
                            "CASE WHEN ML.LEDGER_SUB_TYPE='BK' " +
                            "THEN 'Bank Accounts' " +
                            "ELSE CASE WHEN ML.LEDGER_SUB_TYPE='FD' " +
                            "THEN 'Fixed Deposit' " +
                            "ELSE  MLG.LEDGER_GROUP END END  AS 'Group', " +
                            "ML.LEDGER_TYPE, " +
                            "ML.LEDGER_SUB_TYPE, " +
                            "ML.BANK_ACCOUNT_ID " +
                            "  FROM MASTER_LEDGER ML\n" +
                            " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                            "    ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                            " INNER JOIN LEDGER_BALANCE LB\n" +
                            "    ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                            "   AND ML.IS_BRANCH_LEDGER = 1\n" +
                            "   AND LB.BRANCH_ID IN\n" +
                            "       (SELECT BRANCH_OFFICE_ID\n" +
                            "          FROM BRANCH_OFFICE\n" +
                            "         WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE)";
                        break;
                    }

                case SQLCommand.LedgerBank.BankAccountFetchAll:
                    {
                        query = "SELECT " +
                                      "MA.BANK_ACCOUNT_ID, " +
                                      "MA.ACCOUNT_CODE, " +
                                      "MA.ACCOUNT_NUMBER, " +
                                      "MB.BANK, " +
                                      "MB.BRANCH, " +
                                      "MA.DATE_OPENED, " +
                                      "MA.DATE_CLOSED " +
                                    "FROM " +
                                      "MASTER_BANK_ACCOUNT MA LEFT JOIN  MASTER_BANK MB ON MA.BANK_ID=MB.BANK_ID  WHERE MA.ACCOUNT_TYPE_ID=1 ORDER BY MB.BANK";
                        //ACCOUNT_TYPE_ID=1 Saving Account
                        break;
                    }
                case SQLCommand.LedgerBank.FixedDepositFetchAll:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        "       MA.ACCOUNT_CODE,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       CONCAT(MP.PROJECT, ' - ', MD.DIVISION) AS 'PROJECT',\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED,\n" +
                        "       MA.MATURITY_DATE\n" +
                        "  FROM MASTER_BANK_ACCOUNT MA\n" +
                        "  LEFT JOIN MASTER_BANK MB\n" +
                        "    ON MA.BANK_ID = MB.BANK_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER ML\n" +
                        "    ON MA.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        "  LEFT JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        "  LEFT JOIN MASTER_DIVISION MD\n" +
                        "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                        " ORDER BY MB.BANK ASC";




                        break;
                    }
                case SQLCommand.LedgerBank.FetchFixedDepositCodes:
                    {
                        query = "SELECT MA.ACCOUNT_CODE\n" +
                                "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                                " ORDER BY MA.BANK_ACCOUNT_ID DESc";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerForLookup:
                    {
                        query = "SELECT " +
                                  "ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                   "CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP)) AS LEDGER_NAME " +
                             "FROM " +
                                 "MASTER_LEDGER ML,MASTER_LEDGER_GROUP MP WHERE ML.GROUP_ID=MP.GROUP_ID AND ML.STATUS=0 ORDER BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.LedgerIdFetch:
                    {
                        query = "SELECT " +
                                      "LEDGER_ID " +
                                    "FROM " +
                                      "MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountIdFetch:
                    {
                        query = "SELECT " +
                                      "BANK_ACCOUNT_ID " +
                                    "FROM " +
                                      "MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchMaturityDate:
                    {
                        query = "SELECT " +
                                    "MATURITY_DATE " +
                                    "FROM " +
                                    "MASTER_BANK_ACCOUNT WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCostCenterId:
                    {
                        query = "SELECT " +
                                     "IS_COST_CENTER " +
                                   "FROM " +
                                     "MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }

                case SQLCommand.LedgerBank.FetchLedgerByGroup:
                    {
                        query = " SELECT " +
                                     " ML.BANK_ACCOUNT_ID, " +
                                     " ML.GROUP_ID, " +
                                     " MP.NATURE_ID, " +
                                     " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                     " ML.IS_COST_CENTER, " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP),CONCAT(' (',ML.LEDGER_CODE,')')) AS LEDGER_NAME " +
                            //  " CONCAT(MB.BANK,CONCAT(' - ',MBA.ACCOUNT_NUMBER),CONCAT(' - ',MB.BRANCH)) AS 'BANK'," +
                                " FROM " +
                                     " MASTER_LEDGER  ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                     " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID NOT IN(12,13,14)) " +
                                     " AND  ML.GROUP_ID=MP.GROUP_ID " +
                                     " AND ML.STATUS=0 " +
                                     " AND ML.LEDGER_TYPE='GN' " +
                                     " AND PROJECT_ID=?PROJECT_ID " +
                                     " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankInterestLedger:
                    {
                        query = " SELECT " +
                                     " ML.BANK_ACCOUNT_ID, " +
                                     " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                     " ML.IS_COST_CENTER, " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP)) AS LEDGER_NAME " +
                                " FROM " +
                                     " MASTER_LEDGER  ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                     " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID NOT IN(12,13,14)) " +
                                     " AND  ML.GROUP_ID=MP.GROUP_ID " +
                                     " AND ML.STATUS=0 " +
                                     " AND ML.LEDGER_TYPE='GN' " +
                                     " AND IS_BANK_INTEREST_LEDGER=1 " +
                                     " AND PROJECT_ID=?PROJECT_ID " +
                                     " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankLedger:
                    {
                        query = " SELECT " +
                                   " ML.BANK_ACCOUNT_ID, " +
                                   " MP.NATURE_ID, " +
                                   " ML.GROUP_ID, " +
                                   " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                   " ML.IS_COST_CENTER, " +
                                   " CONCAT(ML.LEDGER_NAME,' - ', MP.LEDGER_GROUP, ' ( ', ML.LEDGER_CODE,')' ) AS LEDGER_NAME " +
                              " FROM " +
                                   " MASTER_LEDGER  ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                   " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID  IN(12,13)) " +
                                   " AND  ML.GROUP_ID=MP.GROUP_ID " +
                                   " AND ML.STATUS=0 " +
                                   " AND ML.LEDGER_TYPE='GN' " +
                                   " AND PROJECT_ID=?PROJECT_ID " +
                                   " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
                case SQLCommand.LedgerBank.IsBankLedger:
                    {
                        query = "SELECT " +
                                    "BANK_ACCOUNT_ID " +
                                "FROM " +
                                    "MASTER_LEDGER  " +
                                "WHERE LEDGER_TYPE ='GN' AND LEDGER_SUB_TYPE IN('BK','FD') AND LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchCashBankFDLedger:
                    {
                        query = "SELECT " +
                                    " ML.BANK_ACCOUNT_ID,10 AS STATUS, " +
                                    "ML.LEDGER_ID, " +
                                    "ML.GROUP_ID, " +
                                    "ML.LEDGER_CODE, " +
                                    "ML.LEDGER_NAME " +
                                "FROM " +
                                    "MASTER_LEDGER ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID  " +
                                "WHERE ML.GROUP_ID =13 AND ML.STATUS=0   AND PL.PROJECT_ID=?PROJECT_ID  " +
                                "UNION " +
                                "SELECT " +
                                    " ML.BANK_ACCOUNT_ID, " +
                                    " CASE WHEN GROUP_ID =12 THEN 10 ELSE FR.STATUS END AS STATUS, " +
                                    "ML.LEDGER_ID, " +
                                    "ML.GROUP_ID, " +
                                    "ML.LEDGER_CODE, " +
                                "CASE\n" +
                                "        WHEN GROUP_ID = 12 THEN\n" +
                                "         CONCAT(CONCAT(LEDGER_CODE),\n" +
                                "                CONCAT(' - ', LEDGER_NAME),\n" +
                                "                ' (BankAccount)')\n" +
                                "        ELSE\n" +
                                "         CASE\n" +
                                "           WHEN GROUP_ID = 14 THEN\n" +
                                "            CONCAT(CONCAT(LEDGER_CODE),\n" +
                                "                   CONCAT(' - ', LEDGER_NAME),\n" +
                                "                   ' (Fixed Deposit)')\n" +
                                "         END\n" +
                                "      END AS LEDGER_NAME\n" +
                                "FROM " +
                                    "MASTER_LEDGER ML LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID , MASTER_BANK_ACCOUNT BA LEFT JOIN FD_REGISTERS FR " +
                                "ON BA.BANK_ACCOUNT_ID = FR.BANK_ACCOUNT_ID " +
                                "WHERE " +
                                  "GROUP_ID IN(12,14)  " +
                                  "AND ML.BANK_ACCOUNT_ID=BA.BANK_ACCOUNT_ID AND ML.STATUS=0 AND PL.PROJECT_ID=?PROJECT_ID ";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgers:
                    {
                        //query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                        //"       ML.LEDGER_ID,\n" +
                        //"       ML.GROUP_ID,\n" +
                        //"       ML.LEDGER_CODE,\n" +
                        //"       CASE\n" +
                        //"         WHEN GROUP_ID = 14 THEN\n" +
                        //"          CONCAT(CONCAT(LEDGER_CODE),\n" +
                        //"                 CONCAT(' - ', LEDGER_NAME),\n" +
                        //"                 ' (Fixed Deposit)')\n" +
                        //"       END AS LEDGER_NAME\n" +
                        //"  FROM MASTER_LEDGER ML\n" +
                        //"  LEFT JOIN PROJECT_LEDGER PL\n" +
                        //"    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_BANK_ACCOUNT BA\n" +
                        //"  LEFT JOIN FD_REGISTERS FR\n" +
                        //"    ON BA.ACCOUNT_NUMBER = FR.ACCOUNT_NO\n" +
                        //" WHERE GROUP_ID IN (14)\n" +
                        //"   AND ML.BANK_ACCOUNT_ID = BA.BANK_ACCOUNT_ID\n" +
                        //"   AND ML.STATUS = 0 \n" +
                        //"   AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                        //" GROUP BY BANK_ACCOUNT_ID";
                        query = "SELECT T.LEDGER_ID,\n" +
                                "       T.LEDGER_CODE,\n" +
                                "       T.LEDGER_NAME,\n" +
                                "       CASE\n" +
                                "         WHEN T.LEDGER_SUB_TYPE = 'FD' THEN\n" +
                                "          'Fixed Deposit'\n" +
                                "       END AS 'GROUP'\n" +
                                "  FROM MASTER_LEDGER_GROUP AS MLG\n" +
                                "  JOIN (SELECT ML.LEDGER_ID,\n" +
                                "               ML.LEDGER_CODE,\n" +
                                "               ML.LEDGER_NAME,\n" +
                                "               ML.LEDGER_SUB_TYPE\n" +
                                "          FROM MASTER_LEDGER AS ML\n" +
                                "         WHERE GROUP_ID = 14) AS T\n" +
                                "    ON MLG.GROUP_ID = 14";

                        break;
                    }
                case SQLCommand.LedgerBank.FixedDepositByLedger:
                    {
                        query = "SELECT ML.BANK_ACCOUNT_ID,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       ML.GROUP_ID,\n" +
                        "       ML.LEDGER_CODE,\n" +
                        "       CASE\n" +
                        "         WHEN GROUP_ID = 14 THEN\n" +
                        "          CONCAT(CONCAT(LEDGER_CODE),\n" +
                        "                 CONCAT(' - ', LEDGER_NAME),\n" +
                        "                 ' (Fixed Deposit)')\n" +
                        "       END AS LEDGER_NAME\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        "  LEFT JOIN PROJECT_LEDGER PL\n" +
                        "    ON PL.LEDGER_ID = ML.LEDGER_ID, MASTER_BANK_ACCOUNT BA\n" +
                        "  LEFT JOIN FD_REGISTERS FR\n" +
                        "    ON BA.ACCOUNT_NUMBER = FR.ACCOUNT_NO\n" +
                        " WHERE GROUP_ID IN (14)\n" +
                        "   AND ML.BANK_ACCOUNT_ID = BA.BANK_ACCOUNT_ID\n" +
                        "   AND ML.STATUS = 0 \n" +
                        "   AND PL.PROJECT_ID = ?PROJECT_ID AND ML.BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID\n" +
                        " GROUP BY BANK_ACCOUNT_ID";

                        break;
                    }
                //case SQLCommand.LedgerBank.FetchFDLedgers:
                //    {
                //        query = "";
                //        break;
                //    }
                case SQLCommand.LedgerBank.BankAccountAdd:
                    {

                        query = "INSERT INTO MASTER_BANK_ACCOUNT ( " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID, " +
                                    "BANK_ID, " +
                                    "DATE_OPENED,  " +
                                    "DATE_CLOSED, " +
                                    "OPERATED_BY, " +
                                    "PERIOD_YEAR,  " +
                                    "PERIOD_MTH,   " +
                                    "PERIOD_DAY,   " +
                                    "INTEREST_RATE, " +
                                    "AMOUNT, " +
                                    "MATURITY_DATE,  " +
                                    "NOTES,LEDGER_ID,IS_FCRA_ACCOUNT ) " +
                                    "VALUES( " +
                                    "?ACCOUNT_CODE, " +
                                    "?ACCOUNT_NUMBER, " +
                                    "?ACCOUNT_HOLDER_NAME, " +
                                    "?ACCOUNT_TYPE_ID, " +
                                    "?BANK_ID, " +
                                    "?DATE_OPENED,  " +
                                    "?DATE_CLOSED, " +
                                    "?OPERATED_BY, " +
                                    "?PERIOD_YEAR,  " +
                                    "?PERIOD_MTH,   " +
                                    "?PERIOD_DAY,   " +
                                    "?INTEREST_RATE, " +
                                    "?AMOUNT, " +
                                    "?MATURITY_DATE,  " +
                                    "?NOTES,?LEDGER_ID,?IS_FCRA_ACCOUNT) ";
                        break;
                    }
                case SQLCommand.LedgerBank.BankAccountUpdate:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT SET " +
                                    "ACCOUNT_CODE = ?ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER = ?ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME = ?ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID = ?ACCOUNT_TYPE_ID, " +
                                    "BANK_ID = ?BANK_ID," +
                                    "DATE_OPENED = ?DATE_OPENED, " +
                                    "DATE_CLOSED = ?DATE_CLOSED, " +
                                    "OPERATED_BY=?OPERATED_BY, " +
                            //  "DATE_CLOSED = NULL, " +
                                    "PERIOD_YEAR = ?PERIOD_YEAR,  " +
                                    "PERIOD_MTH = ?PERIOD_MTH,   " +
                                    "PERIOD_DAY = ?PERIOD_DAY, " +
                                    "INTEREST_RATE = ?INTEREST_RATE, " +
                                    "AMOUNT = ?AMOUNT, " +
                                    "MATURITY_DATE = ?MATURITY_DATE,  " +
                            //"MATURITY_DATE = NULL, " +
                                    "NOTES = ?NOTES, " +
                            //"LEDGER_ID = ?LEDGER_ID " +
                            "IS_FCRA_ACCOUNT = ?IS_FCRA_ACCOUNT " +
                                    "WHERE BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID";
                        break;
                    }

                case SQLCommand.LedgerBank.BankAccountFetch:
                    {
                        query = "SELECT " +
                                 "BANK_ACCOUNT_ID, " +
                                    "ACCOUNT_CODE, " +
                                    "ACCOUNT_NUMBER, " +
                                    "ACCOUNT_HOLDER_NAME, " +
                                    "ACCOUNT_TYPE_ID, " +
                                    "BANK_ID, " +
                                    "DATE_OPENED,  " +
                                    "OPERATED_BY, " +
                                    "PERIOD_YEAR,  " +
                                    "PERIOD_MTH,   " +
                                    "PERIOD_DAY, " +
                                    "INTEREST_RATE, " +
                                    "MATURITY_DATE,  " +
                                    "DATE_CLOSED,NOTES,AMOUNT,IS_FCRA_ACCOUNT " +
                            "FROM " +
                                "MASTER_BANK_ACCOUNT " +
                                " WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerNature:
                    {
                        query = "SELECT MG.NATURE_ID " +
                                    "FROM MASTER_LEDGER_GROUP MG " +
                                    "LEFT JOIN MASTER_LEDGER ML " +
                                   "ON MG.GROUP_ID=ML.GROUP_ID " +
                                   "WHERE ML.LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerNatureByLedgerGroup:
                    {
                        query = "SELECT NATURE_ID FROM MASTER_LEDGER_GROUP WHERE GROUP_ID=?GROUP_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerIdsByLedgerGroup:
                    {
                        query = "SELECT IFNULL(GROUP_CONCAT(LEDGER_ID),0) AS LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID IN (?GROUP_ID);";
                        break;
                    }

                case SQLCommand.LedgerBank.SetLedgerSource:
                    {
                        //LEDGER_GROUP
                        //query = "SELECT MLG.GROUP_ID,IF (MLG.GROUP_CODE<>null, CONCAT(MLG.LEDGER_GROUP,CONCAT(' - ',MLG.GROUP_CODE)),MLG.LEDGER_GROUP) AS 'GROUP',0 AS 'SELECT' " +
                        //        " FROM " +
                        //        " MASTER_LEDGER_GROUP AS MLG INNER JOIN MASTER_LEDGER AS ML ON MLG.GROUP_ID=ML.GROUP_ID GROUP BY MLG.GROUP_ID ORDER BY MLG.LEDGER_GROUP ASC";

                        query = "SELECT MLG.GROUP_ID, IF(MLG.GROUP_CODE IS NOT NULL, CONCAT(MLG.LEDGER_GROUP,CONCAT(' - ',MLG.GROUP_CODE)),MLG.LEDGER_GROUP) AS 'GROUP',\n" +
                                "0 AS 'SELECT' FROM MASTER_LEDGER_GROUP AS MLG";
                        //"LEFT JOIN MASTER_LEDGER AS ML ON MLG.GROUP_ID=ML.GROUP_ID GROUP BY MLG.GROUP_ID ORDER BY MLG.LEDGER_GROUP ASC";

                        break;
                    }
                case SQLCommand.LedgerBank.SetCongregationLedgerSource:
                    {
                        //CONGREGATION_LEDGER
                        query = "SELECT CL.CON_LEDGER_ID,  CL1.CON_LEDGER_CODE AS PARENT_CON_CODE, CL.CON_LEDGER_CODE AS CON_CODE,\n" +
                                    "CL1.CON_LEDGER_NAME AS CON_LEDGER_GROUP,\n" +
                                    "IF(GRP.PARENT_GROUP_CODE IS NULL, CL.CON_LEDGER_NAME, CONCAT(CL.CON_LEDGER_NAME, ' - ', GRP.PARENT_GROUP_CODE)) AS CON_LEDGER_NAME,\n" +
                                    "CONCAT(',', IFNULL(GRP.PARENT_GROUP_ID, ''), ',') AS PARENT_GROUP_ID, CONCAT(',', IFNULL(GRP.GROUP_ID, ''), ',') AS GROUP_ID, 0 AS 'SELECT'\n" +
                                    "FROM CONGREGATION_LEDGER AS CL\n" +
                                    "LEFT JOIN CONGREGATION_LEDGER AS CL1 ON CL1.CON_LEDGER_ID = CL.CON_PARENT_LEDGER_ID\n" +
                                    "LEFT JOIN (SELECT CLM.CON_LEDGER_ID,\n" +
                                    "GROUP_CONCAT(DISTINCT CASE WHEN PG.GROUP_ID IN (1, 2, 3, 4) THEN LG.GROUP_CODE\n" +
                                        "ELSE PG.GROUP_CODE END ORDER BY PG.GROUP_CODE) AS PARENT_GROUP_CODE,\n" +
                                    "GROUP_CONCAT(DISTINCT PG.GROUP_ID) AS PARENT_GROUP_ID,\n" +
                                    "GROUP_CONCAT(DISTINCT LG.GROUP_ID) AS GROUP_ID\n" +
                                    "FROM MASTER_LEDGER_GROUP LG\n" +
                                    "INNER JOIN MASTER_LEDGER_GROUP PG ON LG.PARENT_GROUP_ID = PG.GROUP_ID\n" +
                                    "INNER JOIN MASTER_LEDGER ML ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                    "INNER JOIN CONGREGATION_LEDGER_MAP CLM ON CLM.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "WHERE LG.GROUP_ID NOT IN (12, 13, 14)\n" +
                                    "GROUP BY CLM.CON_LEDGER_ID) AS GRP ON GRP.CON_LEDGER_ID = CL.CON_LEDGER_ID\n" +
                                    "WHERE CL.CON_LEDGER_CODE NOT IN ('01', '02','03', '04', '05', '06', '07', '08', '09')" +
                                    "ORDER BY CL.CON_LEDGER_CODE";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchConLedgerIdByLedgerGroupId:
                    {
                        query = "SELECT CLM.CON_LEDGER_ID, PG.GROUP_ID AS PARENT_GROUP_ID, LG.GROUP_ID\n" +
                                  "FROM MASTER_LEDGER_GROUP LG\n" +
                                  "INNER JOIN MASTER_LEDGER_GROUP PG ON LG.PARENT_GROUP_ID = PG.GROUP_ID\n" +
                                  "INNER JOIN MASTER_LEDGER ML ON ML.GROUP_ID = LG.GROUP_ID\n" +
                                  "INNER JOIN CONGREGATION_LEDGER_MAP CLM ON CLM.LEDGER_ID = ML.LEDGER_ID\n" +
                                  "WHERE LG.GROUP_ID NOT IN (12, 13, 14)";
                        break;
                    }
                case SQLCommand.LedgerBank.SetLedgerDetailSource:
                    {
                        // LEDGER_NAME

                        query = "SELECT ML.LEDGER_ID," +
                                " IF(ML.LEDGER_CODE<>null ,CONCAT(ML.LEDGER_NAME,CONCAT(' - ',ML.LEDGER_CODE)),ML.LEDGER_NAME) AS 'LEDGER',MLG.GROUP_ID,0 AS 'SELECT',MLG.LEDGER_GROUP, ML.IS_BRANCH_LEDGER FROM " +
                                " MASTER_LEDGER_GROUP AS MLG INNER JOIN MASTER_LEDGER AS ML ON MLG.GROUP_ID=ML.GROUP_ID ORDER BY ML.LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerDetailSourcebyBranch:
                    {
                        query = "SELECT ML.LEDGER_ID,\n" +
                       "       IF(ML.LEDGER_CODE <> null,\n" +
                       "          CONCAT(ML.LEDGER_NAME, CONCAT(' - ', ML.LEDGER_CODE)),\n" +
                       "          ML.LEDGER_NAME) AS 'LEDGER',\n" +
                       "       MLG.GROUP_ID,\n" +
                       "       0 AS 'SELECT',ML.IS_BRANCH_LEDGER\n" +
                       "  FROM MASTER_LEDGER_GROUP AS MLG\n" +
                       " INNER JOIN MASTER_LEDGER AS ML\n" +
                       "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                       " INNER JOIN LEDGER_BALANCE LB\n" +
                       "    ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                       "   AND ML.IS_BRANCH_LEDGER = 1\n" +
                       " WHERE LB.BRANCH_ID IN (?BRANCH_ID)\n" +
                       " GROUP BY ML.LEDGER_ID\n" +
                       " ORDER BY ML.LEDGER_NAME ASC";

                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerGroupbyLedgerId:
                    {
                        query = "SELECT LG.GROUP_ID\n" +
                          "  FROM MASTER_LEDGER ML\n" +
                          " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                          "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                          " WHERE ML.LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankAccountById:
                    {
                        query = "SELECT BANK_ACCOUNT_ID FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.UpdateFDBankAccount:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT SET " +
                                "INTEREST_RATE=?INTEREST_RATE, " +
                                "MATURITY_DATE=?MATURITY_DATE, " +
                                "AMOUNT=?AMOUNT " +
                                "WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerByLedgerGroup:
                    {
                        query = "SELECT" +
                               " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                "ML.LEDGER_NAME " +
                                "FROM " +
                                "MASTER_LEDGER ML,PROJECT_LEDGER PL " +
                                "WHERE ML.LEDGER_ID=PL.LEDGER_ID " +
                                "AND PROJECT_ID=?PROJECT_ID AND LEDGER_SUB_TYPE='IK'";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckProjectExist:
                    {
                        query = "SELECT COUNT(*) FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerCodes:
                    {
                        query = "SELECT LEDGER_CODE FROM MASTER_LEDGER ORDER BY LEDGER_ID DESC;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBankAccountCodes:
                    {
                        query = "SELECT MA.ACCOUNT_CODE\n" +
                            "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                " WHERE MA.ACCOUNT_TYPE_ID = 1\n" +
                                " ORDER BY MA.BANK_ACCOUNT_ID DESc";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDLedgerById:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_CODE, ML.LEDGER_NAME, PL.PROJECT_ID\n" +
                                "  FROM MASTER_LEDGER AS ML\n" +
                                " INNER JOIN PROJECT_LEDGER AS PL\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                " WHERE ML.LEDGER_ID =?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FDLedgerUpdate:
                    {
                        query = "UPDATE MASTER_LEDGER AS ML\n" +
                                "   SET ML.LEDGER_CODE = ?LEDGER_CODE, LEDGER_NAME = ?LEDGER_NAME\n" +
                                " WHERE ML.LEDGER_ID =?LEDGERID";
                        break;
                    }
                case SQLCommand.LedgerBank.LedgerFetchAll:
                    {
                        query = "SELECT ML.LEDGER_CODE, ML.LEDGER_NAME,MLG.LEDGER_GROUP,MLG.NATURE_ID, MLG.GROUP_CODE, ML.LEDGER_TYPE, ML.LEDGER_SUB_TYPE, " +
                                "ML.BANK_ACCOUNT_ID, ML.IS_COST_CENTER, ML.NOTES, ML.IS_BANK_INTEREST_LEDGER, ML.SORT_ID, STATUS, " +
                                "ML.ACCESS_FLAG,ML.IS_TDS_LEDGER FROM MASTER_LEDGER ML INNER JOIN MASTER_LEDGER_GROUP MLG ON ML.GROUP_ID=MLG.GROUP_ID AND ML.GROUP_ID NOT IN(12,13,14)                                 AND ML.IS_BRANCH_LEDGER=0 " +
                                "INNER JOIN PROJECT_CATEGORY_LEDGER PCL ON PCL.LEDGER_ID=ML.LEDGER_ID " +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_CATEGORY_ID=PCL.PROJECT_CATEGORY_ID " +
                                "INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID " +
                                "WHERE PB.BRANCH_ID IN(SELECT BRANCH_OFFICE_ID FROM BRANCH_OFFICE " +
                                "WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE) GROUP BY PCL.LEDGER_ID";
                        break;
                    }
                //AND ML.ACCESS_FLAG=0 (To load all the Ledgers-(22.01.2020))
                case SQLCommand.LedgerBank.FetchLedgersByProjectCategory:
                    {
                        query = @"SELECT ML.LEDGER_ID,concat(ML.LEDGER_NAME,' - ', ML.LEDGER_CODE) AS LEDGER_NAME,MLG.LEDGER_GROUP,
                               IF(PCL.PROJECT_CATEGORY_ID=?PROJECT_CATOGORY_ID,1,0) AS 'SELECT'
                               FROM MASTER_LEDGER AS ML LEFT JOIN PROJECT_CATEGORY_LEDGER AS PCL ON ML.LEDGER_ID=PCL.LEDGER_ID
                               AND PCL.PROJECT_CATEGORY_ID=?PROJECT_CATOGORY_ID
                               LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID=ML.GROUP_ID
                               WHERE ML.GROUP_ID NOT IN (12,13) AND ML.IS_BRANCH_LEDGER=0 AND ML.ACCESS_FLAG<>2 GROUP BY LEDGER_ID ";
                        //WHERE ML.GROUP_ID NOT IN (12,13) AND ML.IS_BRANCH_LEDGER=0 AND ML.ACCESS_FLAG <>2 AND ML.STATUS=0 GROUP BY LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerDefaultByProjectCategory:
                    {
                        query = @"SELECT GROUP_CONCAT(ML.LEDGER_NAME SEPARATOR ', ') AS LEDGER_NAME
                                    FROM PROJECT_CATEGORY_LEDGER PCL LEFT JOIN MASTER_LEDGER ML ON PCL.LEDGER_ID = ML.LEDGER_ID
                                    WHERE PROJECT_CATEGORY_ID=?PROJECT_CATOGORY_ID AND ML.ACCESS_FLAG=2";

                        //                        query = @"SELECT @ROWNUM, GROUP_CONCAT(ML.LEDGER_NAME SEPARATOR ', ') AS LEDGER_NAME
                        //                                    FROM PROJECT_CATEGORY_LEDGER PCL LEFT JOIN MASTER_LEDGER ML ON PCL.LEDGER_ID = ML.LEDGER_ID,
                        //                                    (SELECT @ROWNUM :=1) AS ROWVALUE
                        //                                    WHERE PROJECT_CATEGORY_ID=?PROJECT_CATOGORY_ID AND ML.ACCESS_FLAG=2";

                        //                        query = @"SELECT @ROWNUM, CONCAT(GROUP_CONCAT(ML.LEDGER_NAME SEPARATOR ', '),'<BR>') AS LEDGER_NAME
                        //                                    FROM PROJECT_CATEGORY_LEDGER PCL LEFT JOIN MASTER_LEDGER ML ON PCL.LEDGER_ID = ML.LEDGER_ID,
                        //                                    (SELECT @ROWNUM :=1) AS ROWVALUE
                        //                                    WHERE PROJECT_CATEGORY_ID=?PROJECT_CATOGORY_ID AND ML.ACCESS_FLAG=2 GROUP BY ROUND((@ROWNUM :=@ROWNUM+1)/3)";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerIdByLedgerName:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBranchOfficeDeafultLedger:
                    {
                        /*ACCESS_FLAG=2 Master ledger can not be deleted and mapped silently 
                        to the Branch office to the project Category in Ledger Mapping*/
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE ACCESS_FLAG=2 AND STATUS=0";
                        break;
                    }
                case SQLCommand.LedgerBank.MapDefaultLedgerMapping:
                    {
                        query = @"INSERT INTO PROJECT_CATEGORY_LEDGER (PROJECT_CATEGORY_ID, LEDGER_ID)
                                    SELECT MPC.PROJECT_CATOGORY_ID, ML.LEDGER_ID FROM MASTER_LEDGER AS ML,
                                    MASTER_PROJECT_CATOGORY AS MPC WHERE ML.ACCESS_FLAG =2
                                    ON DUPLICATE KEY UPDATE PROJECT_CATEGORY_ID=MPC.PROJECT_CATOGORY_ID,LEDGER_ID =ML.LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBudgetGroup:
                    {
                        query = "SELECT BUDGET_GROUP_ID, BUDGET_GROUP FROM BUDGET_GROUP ORDER BY BUDGET_GROUP_SORT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchBudgetSubGroup:
                    {
                        query = "SELECT BUDGET_SUB_GROUP_ID, BUDGET_SUB_GROUP FROM BUDGET_SUB_GROUP ORDER BY BUDGET_SUB_GROUP_SORT_ID";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchFDInvestment:
                    {
                        query = "SELECT INVESTMENT_TYPE_ID, INVESTMENT_TYPE FROM MASTER_INVESTMENT_TYPE ORDER BY INVESTMENT_TYPE";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchProjectCategorybyLedger:
                    {
                        query = "SELECT PROJECT_CATOGORY_ID,\n" +
                                "       PROJECT_CATOGORY_NAME,\n" +
                                "       SUM(IF(PCL.LEDGER_ID = ?LEDGER_ID, 1, 0)) AS 'SELECT'\n" +
                                "  FROM MASTER_PROJECT_CATOGORY PC\n" +
                                "  LEFT JOIN PROJECT_CATEGORY_LEDGER PCL\n" +
                                "    ON PC.PROJECT_CATOGORY_ID = PCL.PROJECT_CATEGORY_ID\n" +
                                " GROUP BY PC.PROJECT_CATOGORY_ID\n" +
                                " ORDER BY SUM(IF(PCL.LEDGER_ID = ?LEDGER_ID, 1, 0)) DESC, PROJECT_CATOGORY_NAME;";
                        break;
                    }
                case SQLCommand.LedgerBank.DeleteProjectCategoryByLedger:
                    {
                        query = "DELETE FROM PROJECT_CATEGORY_LEDGER WHERE LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.LedgerBank.FetchLedgerNameByLedgerIds:
                    {
                        query = "SELECT ML.LEDGER_ID, CONCAT(ML.LEDGER_NAME, ' (', MN.NATURE,')') AS LEDGER_NAME\n" +
                                "FROM MASTER_LEDGER ML\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "INNER JOIN MASTER_NATURE MN ON MN.NATURE_ID = LG.NATURE_ID\n" +
                                "WHERE ML.LEDGER_ID IN (?LEDGER_IDS)\n" +
                                "ORDER BY ML.LEDGER_NAME";
                        break;
                    }
                case SQLCommand.LedgerBank.CheckTransactionExistsByDateClose:
                    {
                        query = "SELECT VMT.VOUCHER_ID, VT.LEDGER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE > ?DATE_CLOSED\n" +
                                "   AND VT.LEDGER_ID = ?LEDGER_ID AND VMT.STATUS = 1 LIMIT 1";
                        break;
                    }
            }
            return query;
        #endregion LEDGER SQL
        }
    }
}

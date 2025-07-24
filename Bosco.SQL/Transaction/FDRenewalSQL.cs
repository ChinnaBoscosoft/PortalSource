using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class FDRenewalSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FDRenewal).FullName)
            {
                //  query = GetFDRenewalSQL();
                query = GetFixedRenewalSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the FD Renewal.
        /// </summary>
        /// <returns></returns>
        private string GetFDRenewalSQL()
        {
            string query = "";
            SQLCommand.FDRenewal sqlCommandId = (SQLCommand.FDRenewal)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.FDRenewal.Add:
                    {
                        query = "INSERT INTO FD_REGISTERS " +
                               " (ACCOUNT_NO,FD_NO ," +
                               " INVESTED_ON,MATURITY_DATE, " +
                               " AMOUNT,INTEREST_RATE, " +
                               " INTEREST_AMOUNT, " +
                               " BANK_ACCOUNT_ID, " +
                               " STATUS,TRANS_MODE,PERIOD_YEAR,PERIOD_MTH,PERIOD_DAY) " +
                               " VALUES(?ACCOUNT_NO,?FD_NO, " +
                               " ?INVESTED_ON,?MATURITY_DATE, " +
                               " ?AMOUNT,?INTEREST_RATE,?INTEREST_AMOUNT, " +
                               " ?BANK_ACCOUNT_ID,?STATUS,?TRANS_MODE,?PERIOD_YEAR,?PERIOD_MTH,?PERIOD_DAY) ";
                        break;
                    }

                case SQLCommand.FDRenewal.Update:
                    {
                        query = "UPDATE FD_REGISTERS SET " +
                               " ACCOUNT_NO=?ACCOUNT_NO, " +
                               " FD_NO=?FD_NO, " +
                               " INVESTED_ON=?INVESTED_ON, " +
                               " MATURITY_DATE=?MATURITY_DATE, " +
                               " AMOUNT=?AMOUNT, " +
                               " INTEREST_RATE=?INTEREST_RATE, " +
                               " INTEREST_AMOUNT=?INTEREST_AMOUNT, " +
                               " STATUS=?STATUS, " +
                               " PERIOD_YEAR=?PERIOD_YEAR," +
                               " PERIOD_MTH=?PERIOD_MTH," +
                               " PERIOD_DAY=?PERIOD_DAY" +
                               " WHERE FD_REGISTER_ID=?FD_REGISTER_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.Fetch:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       FD.INVESTED_ON,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IN (4) THEN\n" +
                        "          'Inactive'\n" +
                        "         WHEN  FD.STATUS IN(3) THEN 'Closed' ELSE \n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       DATEDIFF(FD.MATURITY_DATE, CURDATE()) AS DAYS\n" +
                        "\n" +
                        "  FROM FD_REGISTERS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT MA\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MA.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_BANK MB\n" +
                        "    ON MB.BANK_ID = MA.BANK_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON FD.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN LEDGER_BALANCE LB\n" +
                        "    ON PL.LEDGER_ID = LB.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID\n" +
                        " WHERE GROUP_ID = 14\n" +
                        "   AND FD.STATUS IN (?STATUS)\n" +
                        "   AND FIND_IN_SET(FD.TRANS_MODE, 'OP,TR')\n" +
                        " GROUP BY PROJECT_ID, BANK, LEDGER_ID, FD_NO\n" +
                        " ORDER BY STATUS DESC";


                        break;
                    }
                case SQLCommand.FDRenewal.FetchAll:
                    {
                        query = "SELECT MA.BANK_ACCOUNT_ID, FD.FD_REGISTER_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED,\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       FD.INVESTED_ON,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IN (4) THEN\n" +
                        "          'Inactive'\n" +
                        "         WHEN FD.STATUS IN(2) THEN 'Closed' ELSE\n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       DATEDIFF(FD.MATURITY_DATE,  FD.INVESTED_ON) AS DAYS,\n" +
                        "       CASE \n" +
                        "       WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 0 THEN \n" +
                        "       'No'\n" +
                        "       WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 1 THEN \n" +
                        "       'Yes' \n" +
                        "       ELSE \n" +
                        "       '' \n" +
                        "       END AS 'PERIODICALLY'\n" +
                        "\n" +
                        "  FROM FD_REGISTERS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT MA\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MA.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN MASTER_BANK MB\n" +
                        "    ON MB.BANK_ID = MA.BANK_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON FD.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN LEDGER_BALANCE LB\n" +
                        "    ON PL.LEDGER_ID = LB.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID\n" +
                        " WHERE GROUP_ID = 14\n" +
                        "   AND FD.STATUS IN (?STATUS)\n" +
                        "   AND FIND_IN_SET(FD.TRANS_MODE, 'OP,TR')\n" +
                        " GROUP BY FD_REGISTER_ID\n" +
                        " ORDER BY DAYS ASC";

                        break;
                    }
                case SQLCommand.FDRenewal.FetchById:
                    {
                        query = "SELECT FD.BANK_ACCOUNT_ID, FD_NO, FD_REGISTER_ID,\n" +
                        "       INVESTED_ON, ACCOUNT_NO,\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.INTEREST_RATE,\n" +
                        "       FD.AMOUNT,\n" +
                        "       FD.INTEREST_AMOUNT,\n" +
                        "       STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN STATUS IN(2,4) THEN\n" +
                        "          'Inactive'\n" +
                        "         ELSE\n" +
                        "          'Active'\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       MB.DATE_OPENED\n" +
                        "  FROM FD_REGISTERS AS FD\n" +
                        " INNER JOIN MASTER_BANK_ACCOUNT AS MB\n" +
                        "    ON FD.BANK_ACCOUNT_ID = MB.BANK_ACCOUNT_ID\n" +
                        " WHERE FD.BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID\n" +
                        "   AND STATUS IN (4,1,3)\n" +
                        " ORDER BY STATUS";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateById:
                    {
                        query = "UPDATE  FD_REGISTERS SET STATUS=4 WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateFDStatus:
                    {
                        query = "UPDATE FD_REGISTERS SET STATUS=2 WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS=1";
                        break;
                    }
                case SQLCommand.FDRenewal.FetchFixedDepositStatus:
                    {
                        query = "SELECT STATUS FROM FD_REGISTERS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS!=0";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateStatusByID:
                    {
                        query = "UPDATE FD_REGISTERS SET STATUS=?STATUS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND STATUS!=0";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDByID:
                    {
                        query = "DELETE FROM FD_REGISTERS WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                case SQLCommand.FDRenewal.DeleteFDRegisters:
                    {
                        query = "DELETE FROM FD_REGISTERS\n" +
                                " WHERE FD_REGISTER_ID =?FD_REGISTER_ID";
                        //"   AND FD_NO = ?FD_NO\n" +
                        //"   AND MATURITY_DATE = ?MATURITY_DATE\n" +
                        //"   AND BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID\n" +
                        //"   AND STATUS = 1";
                        break;
                    }
                case SQLCommand.FDRenewal.UpdateLastFDRow:
                    {
                        //query = "UPDATE FD_REGISTERS\n" +
                        //        "SET STATUS = 1\n" +
                        //        "WHERE ACCOUNT_NO =?ACCOUNT_NO\n" +
                        //        "AND FD_NO =?FD_NO\n" +
                        //        "AND MATURITY_DATE =?MATURITY_DATE\n" +
                        //        "AND BANK_ACCOUNT_ID =?BANK_ACCOUNT_ID";

                        query = "UPDATE FD_REGISTERS\n" +
                        "   SET STATUS = 1\n" +
                        " WHERE BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID\n" +
                        "   AND FD_REGISTER_ID = ?FD_REGISTER_ID";

                        break;
                    }
                case SQLCommand.FDRenewal.FetchFDRegisters:
                    {
                        //query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        //"       FD_REGISTER_ID,\n" +
                        //"       MA.ACCOUNT_NUMBER,\n" +
                        //"       PL.PROJECT_ID,\n" +
                        //"       MB.BANK,\n" +
                        //"       MB.BRANCH,MA.DATE_OPENED AS 'CREATED_ON',\n" +
                        //"       CASE\n" +
                        //"         WHEN FD.STATUS IS NULL THEN\n" +
                        //"          ''\n" +
                        //"      WHEN FD.STATUS=1 THEN \n" +
                        //"      DATE_FORMAT(FD.INVESTED_ON,'%d-%m-%Y')\n" +
                        //"         ELSE\n" +
                        //"           ''\n" +
                        //"       END AS 'DATE_OPENED',\n" +
                        //"       ML.LEDGER_ID,\n" +
                        //"       FD.INTEREST_AMOUNT,\n" +
                        //"       FD.AMOUNT,\n" +
                        //"       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        //"       FD.FD_NO,\n" +
                        //"       ML.LEDGER_NAME,\n" +
                        //"       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        //"       FD.TRANS_MODE,\n" +
                        //"       FD.INVESTED_ON,\n" +
                        //"       FD.MATURITY_DATE,\n" +
                        //"       FD.STATUS,\n" +
                        //"       CASE\n" +
                        //"         WHEN FD.STATUS = 0 OR (FD.STATUS = 1 AND FD.TRANS_MODE = 'OP') THEN\n" +
                        //"          'Invested'\n" +
                        //"         WHEN FD.STATUS = 1 THEN\n" +
                        //"          'ReInvested'\n" +
                        //"         WHEN FD.STATUS = 2 THEN\n" +
                        //"          'Realized'\n" +
                        //"         ELSE\n" +
                        //"          ''\n" +
                        //"       END AS 'FD_STATUS',\n" +
                        //"       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        //"       DATEDIFF(FD.MATURITY_DATE, FD.INVESTED_ON) AS DAYS,\n" +
                        //"       CASE\n" +
                        //"         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 0 THEN\n" +
                        //"          'No'\n" +
                        //"         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 1 THEN\n" +
                        //"          'Yes'\n" +
                        //"         ELSE\n" +
                        //"          ''\n" +
                        //"       END AS 'PERIODICALLY'\n" +
                        //"  FROM MASTER_BANK_ACCOUNT MA\n" +
                        //"  LEFT JOIN FD_REGISTERS FD\n" +
                        //"    ON MA.BANK_ACCOUNT_ID = FD.BANK_ACCOUNT_ID\n" +
                        //" INNER JOIN MASTER_LEDGER ML\n" +
                        //"    ON MA.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        //" INNER JOIN PROJECT_LEDGER PL\n" +
                        //"    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        //" INNER JOIN MASTER_PROJECT MP\n" +
                        //"    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        //" INNER JOIN MASTER_DIVISION MDI\n" +
                        //"    ON MP.DIVISION_ID = MDI.DIVISION_ID, MASTER_BANK MB\n" +
                        //" WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                        //"   AND MA.BANK_ID = MB.BANK_ID\n" +
                        //" ORDER BY DAYS";

                        query = "SELECT MA.BANK_ACCOUNT_ID,\n" +
                        "       FD_REGISTER_ID,\n" +
                        "       MA.ACCOUNT_NUMBER,\n" +
                        "       PL.PROJECT_ID,\n" +
                        "       MB.BANK,\n" +
                        "       MB.BRANCH,\n" +
                        "       MA.DATE_OPENED AS 'CREATED_ON',\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS IS NULL THEN\n" +
                        "          ''\n" +
                        "         WHEN FD.STATUS IN(1,2,3) THEN\n" +
                        "          DATE_FORMAT(FD.INVESTED_ON, '%d-%m-%Y')\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'DATE_OPENED',\n" +
                        "       ML.LEDGER_ID,\n" +
                        "       FD.INTEREST_AMOUNT,\n" +
                        "       FD.AMOUNT,\n" +
                        "       CONCAT(MB.BANK, CONCAT(' - ', MA.ACCOUNT_NUMBER)) AS 'BANKACCOUNT',\n" +
                        "       FD.FD_NO,\n" +
                        "       ML.LEDGER_NAME,\n" +
                        "       IFNULL(FD.AMOUNT, 0) AS AMOUNT,\n" +
                        "       FD.TRANS_MODE,\n" +
                        "       CASE\n" +
                        "         WHEN FD.INVESTED_ON IS NOT NULL AND FD.STATUS IN(2,3) THEN\n" +
                        "         DATE_FORMAT(FD.INVESTED_ON,'%d-%m-%Y')\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'INVESTED_ON',\n" +
                        "       FD.MATURITY_DATE,\n" +
                        "       FD.STATUS,\n" +
                        "       CASE\n" +
                        "         WHEN FD.STATUS = 1 OR (FD.STATUS = 1 AND FD.TRANS_MODE = 'OP') THEN\n" +
                        "          'Invested'\n" +
                        "         WHEN FD.STATUS = 3 THEN\n" +
                        "          'ReInvested'\n" +
                        "         WHEN FD.STATUS = 2 THEN\n" +
                        "          'Realized'\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'FD_STATUS',\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MDI.DIVISION)) AS 'PROJECT',\n" +
                        "       CASE\n" +
                        "       WHEN FD.MATURITY_DATE <=CURDATE() THEN\n" +
                        "            DATEDIFF(FD.MATURITY_DATE,CURDATE())\n" +
                        "         WHEN FD.INVESTED_ON IS NULL THEN\n" +
                        "          DATEDIFF(FD.MATURITY_DATE, MA.DATE_OPENED)\n" +
                        "         ELSE\n" +
                        "          DATEDIFF(FD.MATURITY_DATE, FD.INVESTED_ON)\n" +
                        "       END AS 'DAYS',\n" +
                        "       CASE\n" +
                        "         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 0 THEN\n" +
                        "          'No'\n" +
                        "         WHEN FD.IS_INTEREST_RECEIVED_PERIODICALLY = 1 THEN\n" +
                        "          'Yes'\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS 'PERIODICALLY'\n" +
                        "  FROM MASTER_BANK_ACCOUNT MA\n" +
                        "  LEFT JOIN FD_REGISTERS FD\n" +
                        "    ON MA.BANK_ACCOUNT_ID = FD.BANK_ACCOUNT_ID AND FD.STATUS NOT IN(4)\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON MA.BANK_ACCOUNT_ID = ML.BANK_ACCOUNT_ID\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON PL.PROJECT_ID = MP.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION MDI\n" +
                        "    ON MP.DIVISION_ID = MDI.DIVISION_ID, MASTER_BANK MB\n" +
                        " WHERE MA.ACCOUNT_TYPE_ID = 2\n" +
                        "   AND MA.BANK_ID = MB.BANK_ID\n" +
                        " ORDER BY DAYS";

                        break;
                    }
            }
            return query;
        }

        public string GetFixedRenewalSQL()
        {
            string query = "";
            SQLCommand.FDRenewal sqlCommandId = (SQLCommand.FDRenewal)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.FDRenewal.Add:
                    {
                        query = @"INSERT INTO FD_RENEWAL
                                  (FD_ACCOUNT_ID,
                                   INTEREST_LEDGER_ID,
                                   BANK_LEDGER_ID,
                                   FD_INTEREST_VOUCHER_ID,
                                   FD_VOUCHER_ID,
                                   INTEREST_AMOUNT,
                                   WITHDRAWAL_AMOUNT,
                                   RENEWAL_DATE,
                                   MATURITY_DATE,
                                   INTEREST_RATE,
                                   RECEIPT_NO, 
                                   RENEWAL_TYPE,
                                   STATUS)
                                VALUES
                                  (?FD_ACCOUNT_ID,
                                   ?INTEREST_LEDGER_ID,
                                   ?BANK_LEDGER_ID,
                                   ?FD_INTEREST_VOUCHER_ID,
                                   ?FD_VOUCHER_ID,
                                   ?INTEREST_AMOUNT,
                                   ?WITHDRAWAL_AMOUNT,
                                   ?RENEWAL_DATE,
                                   ?MATURITY_DATE,
                                   ?INTEREST_RATE,
                                   ?RECEIPT_NO,
                                   ?RENEWAL_TYPE,
                                   ?STATUS)";
                        break;
                    }
                case SQLCommand.FDRenewal.Update:
                    {
                        query = @"UPDATE  FD_RENEWAL  SET
                                   INTEREST_LEDGER_ID=?INTEREST_LEDGER_ID,
                                   BANK_LEDGER_ID=?BANK_LEDGER_ID,
                                   FD_INTEREST_VOUCHER_ID=?FD_INTEREST_VOUCHER_ID,
                                   FD_VOUCHER_ID=?FD_VOUCHER_ID,
                                   INTEREST_AMOUNT=?INTEREST_AMOUNT,
                                   WITHDRAWAL_AMOUNT=?WITHDRAWAL_AMOUNT,
                                   RENEWAL_DATE=?RENEWAL_DATE,
                                   MATURITY_DATE=?MATURITY_DATE,
                                   INTEREST_RATE=?INTEREST_RATE,
                                   RECEIPT_NO=?RECEIPT_NO,
                                   RENEWAL_TYPE=?RENEWAL_TYPE,
                                   STATUS=?STATUS WHERE FD_RENEWAL_ID=?FD_RENEWAL_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}

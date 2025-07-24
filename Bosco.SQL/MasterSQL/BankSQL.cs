using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class BankSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Bank).FullName)
            {
                query = GetBankSQL();
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
        private string GetBankSQL()
        {
            string query = "";
            SQLCommand.Bank sqlCommandId = (SQLCommand.Bank)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Bank.Add:
                    {
                        query = "INSERT INTO MASTER_BANK ( " +
                               "BANK_CODE, " +
                               "BANK, " +
                               "BRANCH, " +
                               "ADDRESS," +
                               "IFSCCODE," +
                               "MICRCODE," +
                               "CONTACTNUMBER," +
                               "SWIFTCODE," +
                               "NOTES ) VALUES( " +
                               "?BANK_CODE, " +
                               "?BANK, " +
                               "?BRANCH, " +
                               "?ADDRESS," +
                               "?IFSCCODE," +
                               "?MICRCODE," +
                               "?CONTACTNUMBER," +
                               "?SWIFTCODE," +
                               "?NOTES)";
                        break;
                    }
                case SQLCommand.Bank.Update:
                    {
                        query = "UPDATE MASTER_BANK SET " +
                                    "BANK_CODE = ?BANK_CODE, " +
                                    "BANK =?BANK, " +
                                    "BRANCH =?BRANCH, " +
                                    "ADDRESS=?ADDRESS, " +
                                    "IFSCCODE=?IFSCCODE," +
                                    "MICRCODE=?MICRCODE," +
                                    "CONTACTNUMBER=?CONTACTNUMBER ," +
                                    "SWIFTCODE=?SWIFTCODE," +
                                    "NOTES=?NOTES " +
                                    " WHERE BANK_ID=?BANK_ID ";
                        break;
                    }
                case SQLCommand.Bank.Delete:
                    {
                        query = "DELETE FROM  MASTER_BANK WHERE BANK_ID=?BANK_ID";
                        break;
                    }
                case SQLCommand.Bank.Fetch:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER, " +
                                "SWIFTCODE," +
                                "NOTES " +
                            " FROM " +
                                "MASTER_BANK " +
                                " WHERE BANK_ID=?BANK_ID  ";
                        break;
                    }
                case SQLCommand.Bank.FetchAll:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "BANK_CODE, " +
                                "BANK, " +
                                "BRANCH, " +
                                "ADDRESS, " +
                                "IFSCCODE," +
                                "MICRCODE," +
                                "CONTACTNUMBER ," +
                                "SWIFTCODE " +
                            " FROM" +
                                " MASTER_BANK ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchforLookup:
                    {
                        query = "SELECT " +
                                "BANK_ID, " +
                                "CONCAT(CONCAT(BRANCH,' - '), BANK) AS BANK " +
                            "FROM" +
                                " MASTER_BANK ORDER BY BANK_ID DESC";
                        break;
                    }
                case SQLCommand.Bank.SetBankAccountSource:
                    {
                        query = "SELECT MB.BANK_ID, " +
                               " CONCAT(MB.BANK,CONCAT(' - ',MBA.ACCOUNT_NUMBER),CONCAT(' - ',MB.BRANCH)) AS 'BANK'," +
                               " ML.GROUP_ID " +
                               " FROM MASTER_BANK_ACCOUNT AS MBA " +
                               " INNER JOIN MASTER_BANK AS MB ON MBA.BANK_ID=MB.BANK_ID " +
                               " INNER JOIN MASTER_LEDGER AS ML ON MBA.BANK_ACCOUNT_ID=ML.BANK_ACCOUNT_ID " +
                               " INNER JOIN PROJECT_LEDGER AS PL ON PL.LEDGER_ID=ML.LEDGER_ID " +
                               " WHERE PL.PROJECT_ID IN(?PROJECT_ID) " +
                               " GROUP BY MBA.ACCOUNT_NUMBER " +
                               " ORDER BY BANK_ID ASC";
                        break;
                    }
                case SQLCommand.Bank.FetchSettingBankAccount:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "MBA.LEDGER_ID,\n" +
                                "CONCAT(MB.BANK,\n" +
                                "CONCAT(' - ', MBA.ACCOUNT_NUMBER),\n" +
                                "CONCAT(' - ', MB.BRANCH)) AS 'BANK'\n" +
                                "  FROM MASTER_BANK_ACCOUNT AS MBA\n" +
                                " INNER JOIN MASTER_BANK AS MB\n" +
                                "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                                " INNER JOIN MASTER_LEDGER AS ML\n" +
                                "    ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN PROJECT_LEDGER AS PL\n" +
                                "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE ML.GROUP_ID=12 " +
                                " GROUP BY MBA.LEDGER_ID\n" +
                                " ORDER BY BANK_ID ASC;";

                        break;
                    }
                case SQLCommand.Bank.SelectAllBank:
                    {

                        query = "SELECT MB.BANK_ID,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ',MB.BANK ),\n" +
                                "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "       ML.GROUP_ID,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       MBA.BANK_ACCOUNT_ID,ML.LEDGER_ID,0 AS 'SELECT'\n" +
                                "  FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                " ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                " ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB\n" +
                                " ON   MBA.BANK_ID=MB.BANK_ID\n" +
                                " { WHERE MBA.BRANCH_ID IN(?BRANCH_ID) }\n" +
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";
                        break;
                    }
                case SQLCommand.Bank.SelectAllFD:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                  "       CONCAT(ML.LEDGER_NAME,\n" +
                                  "              CONCAT(' - ', MB.BANK),\n" +
                                  "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                  "       ML.GROUP_ID,\n" +
                                  "       PL.PROJECT_ID,\n" +
                                  "       ML.BANK_ACCOUNT_ID,\n" +
                                  "       ML.LEDGER_ID, 0 AS 'SELECT' \n" +
                                  "  FROM PROJECT_LEDGER PL\n" +
                                  " INNER JOIN MASTER_LEDGER ML\n" +
                                  "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN FD_ACCOUNT FD\n" +
                                  "    ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN MASTER_BANK MB\n" +
                                  "    ON MB.BANK_ID = FD.BANK_ID\n" +
                                  "{ WHERE FD.BRANCH_ID IN(?BRANCH_ID) }\n" +
                                  " GROUP BY ML.LEDGER_ID\n" +
                                  " ORDER BY BANK ASC;";

                        break;
                    }
                case SQLCommand.Bank.FetchFDByProject:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                  "       CONCAT(ML.LEDGER_NAME,\n" +
                                  "              CONCAT(' - ', MB.BANK),\n" +
                                  "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                  "       ML.GROUP_ID,\n" +
                                  "       PL.PROJECT_ID,\n" +
                                  "       MBA.BANK_ACCOUNT_ID,\n" +
                                  "       ML.LEDGER_ID\n" +
                                  "  FROM PROJECT_LEDGER PL\n" +
                                  " INNER JOIN MASTER_LEDGER ML\n" +
                                  "    ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN FD_ACCOUNT FD\n" +
                                  "    ON FD.LEDGER_ID = ML.LEDGER_ID\n" +
                                  " INNER JOIN MASTER_BANK MB\n" +
                                  "    ON MB.BANK_ID = FD.BANK_ID\n" +
                                  " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                  "    ON MBA.BANK_ID=FD.BANK_ID\n" +
                                  " WHERE PL.PROJECT_ID  IN (?PROJECT_ID) \n" +
                                   " { AND MBA.BRANCH_ID IN(?BRANCH_ID) }\n" +
                                  " GROUP BY ML.LEDGER_ID\n" +
                                  " ORDER BY BANK ASC;";

                        break;
                    }
                case SQLCommand.Bank.FetchBankByProject:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "       CONCAT(ML.LEDGER_NAME,\n" +
                                "              CONCAT(' - ',MB.BANK ),\n" +
                                "              CONCAT(' - ', MB.BRANCH)) AS 'BANK',\n" +
                                "       ML.GROUP_ID,\n" +
                                "       PL.PROJECT_ID,\n" +
                                "       MBA.BANK_ACCOUNT_ID,ML.LEDGER_ID, 0 AS 'SELECT' \n" +
                                "  FROM  PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                " ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                " ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_BANK MB\n" +
                                " ON   MBA.BANK_ID=MB.BANK_ID\n" +
                                " WHERE PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " { AND MBA.BRANCH_ID IN(?BRANCH_ID) }\n" +
                                " GROUP BY MBA.LEDGER_ID \n" +
                                " ORDER BY BANK ASC";

                        break;
                    }
                case SQLCommand.Bank.FetchBankCodes:
                    {
                        query = "SELECT BANK_CODE FROM MASTER_BANK ORDER BY BANK_ID DESC";
                        break;    
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}

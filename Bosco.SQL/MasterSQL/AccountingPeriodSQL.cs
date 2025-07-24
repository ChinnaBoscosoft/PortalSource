using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
    public class AccountingPeriodSQL : IDatabaseQuery
    {

        #region ISQLServerQueryMembers

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AccountingPeriod).FullName)
            {
                query = GetAccountingPeriodSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion
        public string GetAccountingPeriodSQL()
        {
            string query = "";
            SQLCommand.AccountingPeriod SqlCommand = (SQLCommand.AccountingPeriod)(this.dataCommandArgs.SQLCommandId);
            switch (SqlCommand)
            {
                case SQLCommand.AccountingPeriod.Add:
                    {

                        query = "INSERT INTO ACCOUNTING_YEAR ( " +
                               "YEAR_FROM, " +
                               "YEAR_TO) " +
                               "VALUES( " +
                               "?YEAR_FROM, " +
                               "?YEAR_TO ) ";
                        break;

                    }
                case SQLCommand.AccountingPeriod.InsertAccountingYear:
                    {
                        query = "\n" +
                                "INSERT INTO ACCOUNTING_YEAR\n" +
                                "  (YEAR_FROM, YEAR_TO, STATUS,IS_FIRST_ACCOUNTING_YEAR)\n" +
                                "VALUES\n" +
                                "  (?YEAR_FROM, ?YEAR_TO, 1,1);";
                        break;
                    }
                case SQLCommand.AccountingPeriod.Update:
                    {
                        query = "UPDATE ACCOUNTING_YEAR  SET " +
                                  "YEAR_FROM=?YEAR_FROM, " +
                                  "YEAR_TO=?YEAR_TO " +
                                  " WHERE ACC_YEAR_ID=?ACC_YEAR_ID ";

                        break;
                    }
                case SQLCommand.AccountingPeriod.Delete:
                    {
                        query = "DELETE FROM ACCOUNTING_YEAR   WHERE ACC_YEAR_ID=?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.AccountingPeriod.Fetch:
                    {
                        query = "SELECT " +
                               "YEAR_FROM, " +
                               "YEAR_TO, " +
                               "STATUS FROM " +
                               "ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchAll:
                    {
                        query = "SELECT ACC_YEAR_ID," +
                                "DATE_FORMAT(YEAR_FROM,'%d/%m/%Y') AS 'Year From', " +
                                "DATE_FORMAT(YEAR_TO,'%d/%m/%Y') AS 'Year To', " +
                                "CASE WHEN STATUS=0 THEN 'Inactive' ElSE 'Active' END AS 'Year Status'  FROM  " +
                                "ACCOUNTING_YEAR ORDER BY DATE_FORMAT(YEAR_FROM,'%d/%m/%Y') ";
                        break;
                    }
                case SQLCommand.AccountingPeriod.CheckIstransacton:
                    {
                        query = "SELECT BALANCE_DATE FROM LEDGER_BALANCE WHERE BALANCE_DATE BETWEEN " +
                                "( SELECT YEAR_FROM FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID) " +
                                 "AND ( SELECT YEAR_TO FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID) ";
                        break;
                    }

                case SQLCommand.AccountingPeriod.FetchForSettings:
                    {
                        query = "SELECT ACC_YEAR_ID, " +
                                "YEAR_FROM, " +
                                "YEAR_TO,BOOKS_BEGINNING_FROM, " +
                                "CASE WHEN STATUS=0 THEN 'Inactive' ElSE 'Active' END AS STATUS  FROM  " +
                                "ACCOUNTING_YEAR ";
                        break;
                    }

                case SQLCommand.AccountingPeriod.UpdateStatus:
                    {
                        query = "UPDATE ACCOUNTING_YEAR SET STATUS =(CASE ACC_YEAR_ID WHEN ?ACC_YEAR_ID THEN 1 ELSE 0 END )";
                        break;
                    }
                case SQLCommand.AccountingPeriod.UpdateBooksbeginningDate:
                    {
                        query = " UPDATE ACCOUNTING_YEAR SET BOOKS_BEGINNING_FROM=?BOOKS_BEGINNING_FROM,IS_FIRST_ACCOUNTING_YEAR=1 " +
                                        " WHERE ACC_YEAR_ID=?ACC_YEAR_ID ";
                        break;
                    }
                case SQLCommand.AccountingPeriod.IsActivePeriod:
                    {
                        query = "SELECT YEAR_FROM,YEAR_TO FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID AND STATUS=1;";
                        break;
                    }
                case SQLCommand.AccountingPeriod.IsActivePeriodId:
                    {
                        query = "SELECT ACC_YEAR_ID FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID AND STATUS=1;";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchBooksBeginingFrom:
                    {
                        query = "SELECT ACC_YEAR_ID,BOOKS_BEGINNING_FROM FROM ACCOUNTING_YEAR LIMIT 1 ";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchMaxAccountingPeriod:
                    {
                        query = "SELECT MAX(YEAR_TO) AS YEAR_TO FROM ACCOUNTING_YEAR";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchIsFirstAccountingyear:
                    {
                        query = "SELECT IS_FIRST_ACCOUNTING_YEAR FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID=?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchTransactionYearTo:
                    {
                        query = "SELECT YEAR_TO FROM ACCOUNTING_YEAR ORDER BY ACC_YEAR_ID DESC LIMIT 1  ";
                        break;
                    }
                case SQLCommand.AccountingPeriod.ValidateBooksBegining:
                    {
                        query = " SELECT BALANCE_DATE\n" +
                                "  FROM LEDGER_BALANCE\n" +
                                " WHERE TRANS_FLAG = 'OP'\n" +
                                "   AND BALANCE_DATE <= ?BOOKS_BEGINNING_FROM LIMIT 1\n" +
                                " UNION\n" +
                                " SELECT VOUCHER_DATE AS BALANCE_DATE\n" +
                                "  FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE VOUCHER_DATE >= ?BOOKS_BEGINNING_FROM LIMIT 1";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchActiveTransactionperiod:
                    {
                        query = " SELECT ACC_YEAR_ID, " +
                               " YEAR_FROM, " +
                               " YEAR_TO, " +
                               " (SELECT BOOKS_BEGINNING_FROM FROM " +
                               " ACCOUNTING_YEAR ORDER BY ACC_YEAR_ID ASC LIMIT 1) AS  BOOKS_BEGINNING_FROM " +
                               " FROM ACCOUNTING_YEAR WHERE STATUS=1";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FecthRecentProjectDetails:
                    {
                        query= "SELECT VMT.VOUCHER_DATE, MP.PROJECT_ID, MP.PROJECT\n" +
                        "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = VMT.PROJECT_ID\n" +
                        " WHERE VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO\n" +
                        "   AND STATUS = 1\n" +
                        "   AND MP.DELETE_FLAG <> 1\n" +
                        "   AND CREATED_BY = ?CREATED_BY\n" +
                        " ORDER BY VOUCHER_DATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.AccountingPeriod.FetchRecentVoucherDate:
                    {
                        query = "SELECT VMT.VOUCHER_DATE, MP.PROJECT_ID, MP.PROJECT\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" + 
                                " INNER JOIN MASTER_PROJECT MP\n" + 
                                "    ON MP.PROJECT_ID = VMT.PROJECT_ID\n" + 
                                " WHERE VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO\n" + 
                                "   AND STATUS = 1\n" + 
                                "   AND MP.DELETE_FLAG <> 1\n" + 
                                "   { AND VMT.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" + 
                                "   { AND VMT.PROJECT_ID = ?PROJECT_ID } \n" + 
                                " ORDER BY VOUCHER_DATE DESC LIMIT 1";
                        break;
                    }

                case SQLCommand.AccountingPeriod.IsAccountingPeriodExists:
                    {
                        query="SELECT YEAR_FROM,YEAR_TO FROM ACCOUNTING_YEAR WHERE YEAR_FROM>=?YEAR_FROM AND YEAR_TO<=?YEAR_TO";
                        break;
                    }
            }
            return query;
        }
    }
}

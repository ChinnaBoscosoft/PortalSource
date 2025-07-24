using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class FixedDepositSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FixedDeposit).FullName)
            {
                query = GetFixedDepositSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Fixed Deposit details.
        /// </summary>
        /// <returns></returns>
        private string GetFixedDepositSQL()
        {
            string query = "";
            SQLCommand.FixedDeposit sqlCommandId = (SQLCommand.FixedDeposit)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {

                case SQLCommand.FixedDeposit.FixedDepositFetchAll:
                    {

                        #region Fixed Deposit

                        query = "SELECT " +
                                      "MA.BANK_ACCOUNT_ID, " +
                                      "MA.ACCOUNT_CODE, " +
                                      "MA.ACCOUNT_NUMBER, " +
                                      "MB.BANK, " +
                                      "MB.BRANCH, " +
                                      "MA.DATE_OPENED, " +
                                      "MA.DATE_CLOSED " +
                                    "FROM " +
                                      "MASTER_BANK_ACCOUNT MA LEFT JOIN  MASTER_BANK MB ON MA.BANK_ID=MB.BANK_ID ";

                        break;
                    }
                case SQLCommand.FixedDeposit.FixedDepositFetch:
                    {
                        query = "SELECT " +
                                      "LEDGER_ID " +
                                    "FROM " +
                                      "MASTER_LEDGER WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID";
                        break;
                    }
                        #endregion

                #region BreakUp
                case SQLCommand.FixedDeposit.BreakUpAdd:
                    {
                        query = @"INSERT INTO FD_REGISTERS 
                                (
                                    ACCOUNT_NO,
                                    FD_NO,
                                    INVESTED_ON,
                                    MATURITY_DATE,       
                                    AMOUNT,
                                    INTEREST_RATE,
                                    INTEREST_AMOUNT,
                                    BANK_ACCOUNT_ID,
                                    STATUS,
                                    TRANS_MODE,
                                    IS_INTEREST_RECEIVED_PERIODICALLY,
                                    INTEREST_TERM,
                                    INTEREST_PERIOD
                                 )
                                VALUES
                               (
                                    ?ACCOUNT_NO,
                                    ?FD_NO,
                                    ?INVESTED_ON,
                                    ?MATURITY_DATE,       
                                    ?AMOUNT,
                                    ?INTEREST_RATE,
                                    ?INTEREST_AMOUNT,
                                    ?BANK_ACCOUNT_ID,
                                    ?STATUS,
                                    ?TRANS_MODE,
                                    ?IS_INTEREST_RECEIVED_PERIODICALLY,
                                    ?INTEREST_TERM,
                                    ?INTEREST_PERIOD
                                );";
                        break;
                    }
                case SQLCommand.FixedDeposit.BreakUpDelete:
                    {
                        query = "DELETE FROM FD_REGISTERS WHERE ACCOUNT_NO=?ACCOUNT_NUMBER AND TRANS_MODE='OP' AND STATUS =0;";
                        break;
                    }
                case SQLCommand.FixedDeposit.BreakUpFetchByAccountNo:
                    {
                        query = "SELECT FD_NO,INVESTED_ON,MATURITY_DATE,AMOUNT,INTEREST_RATE,INTEREST_AMOUNT " +
                                "FROM FD_REGISTERS WHERE ACCOUNT_NO=?ACCOUNT_NO AND TRANS_MODE='OP' AND STATUS=0;;";
                        break;
                    }
                case SQLCommand.FixedDeposit.FetchFDByID:
                    {
                        query = @"SELECT FD_REGISTER_ID,
                                        DATE_FORMAT(DATE_OPENED, '%d-%m-%Y') AS DATE_OPENED,
                                        INTEREST_AMOUNT,
                                        BANK,
                                        BRANCH,
                                        MBA.BANK_ACCOUNT_ID,
                                        ACCOUNT_NUMBER,
                                        DATE_OPENED,
                                        MBA.PERIOD_YEAR,
                                        MBA.PERIOD_MTH,
                                        MBA.PERIOD_DAY,
                                        MBA.INTEREST_RATE,
                                        MBA.MATURITY_DATE,
                                        FD.IS_INTEREST_RECEIVED_PERIODICALLY,
                                        FD.INTEREST_TERM,
                                        FD.INTEREST_PERIOD
                                    FROM MASTER_BANK_ACCOUNT MBA
                                    LEFT JOIN FD_REGISTERS FD
                                    ON MBA.BANK_ACCOUNT_ID = FD.BANK_ACCOUNT_ID
                                    LEFT JOIN MASTER_BANK MB
                                    ON MBA.BANK_ID = MB.BANK_ID
                                    WHERE MBA.BANK_ACCOUNT_ID = ?BANK_ACCOUNT_ID;";

                        break;
                    }
                case SQLCommand.FixedDeposit.UpdateFD:
                    {
                        query = "UPDATE MASTER_BANK_ACCOUNT " +
                                 "SET " +
                                      "PERIOD_YEAR=?PERIOD_YEAR," +
                                      "PERIOD_MTH=?PERIOD_MTH," +
                                      "PERIOD_DAY=?PERIOD_DAY," +
                                      "INTEREST_RATE=?INTEREST_RATE," +
                                      "MATURITY_DATE=?MATURITY_DATE," +
                                      "AMOUNT=?AMOUNT " +
                                "WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID;";
                        break;
                    }
                case SQLCommand.FixedDeposit.FDRegisterAdd:
                    {
                        query = @"INSERT INTO FD_REGISTERS
                                  (
                                        ACCOUNT_NO, 
                                        INVESTED_ON, 
                                        MATURITY_DATE, 
                                        AMOUNT, 
                                        INTEREST_RATE, 
                                        INTEREST_AMOUNT, 
                                        BANK_ACCOUNT_ID, 
                                        STATUS, 
                                        TRANS_MODE,
                                        PERIOD_YEAR,
                                        PERIOD_MTH,
                                        PERIOD_DAY,
                                        IS_INTEREST_RECEIVED_PERIODICALLY,
                                        INTEREST_TERM,
                                        INTEREST_PERIOD
                                    )
                             VALUES(
                                        ?ACCOUNT_NO, 
                                        ?INVESTED_ON, 
                                        ?MATURITY_DATE, 
                                        ?AMOUNT, 
                                        ?INTEREST_RATE, 
                                        ?INTEREST_AMOUNT, 
                                        ?BANK_ACCOUNT_ID, 
                                        ?STATUS, 
                                        ?TRANS_MODE,
                                        ?PERIOD_YEAR,
                                        ?PERIOD_MTH,
                                        ?PERIOD_DAY,
                                        ?IS_INTEREST_RECEIVED_PERIODICALLY,
                                        ?INTEREST_TERM,
                                        ?INTEREST_PERIOD
                                )";
                        break;
                    }
                case SQLCommand.FixedDeposit.FDRegisterUpdate:
                    {
                        query = @"UPDATE FD_REGISTERS 
                                  SET
                                        ACCOUNT_NO=?ACCOUNT_NO, 
                                        FD_NO=?FD_NO, 
                                        INVESTED_ON=?INVESTED_ON, 
                                        MATURITY_DATE=?MATURITY_DATE, 
                                        AMOUNT=?AMOUNT, 
                                        INTEREST_RATE=?INTEREST_RATE, 
                                        INTEREST_AMOUNT=?INTEREST_AMOUNT, 
                                        PERIOD_YEAR=?PERIOD_YEAR,
                                        PERIOD_MTH=?PERIOD_MTH,
                                        PERIOD_DAY=?PERIOD_DAY, 
                                        BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID, 
                                        STATUS=?STATUS, 
                                        TRANS_MODE=?TRANS_MODE,
                                        IS_INTEREST_RECEIVED_PERIODICALLY=?IS_INTEREST_RECEIVED_PERIODICALLY,
                                        INTEREST_TERM=?INTEREST_TERM,
                                        INTEREST_PERIOD=?INTEREST_PERIOD
                                    WHERE FD_REGISTER_ID=?FD_REGISTER_ID ";
                        break;
                    }

                case SQLCommand.FixedDeposit.FetchFDNumber:
                    {
                        query = "SELECT FD_REGISTER_ID FROM FD_REGISTERS  WHERE BANK_ACCOUNT_ID=?BANK_ACCOUNT_ID AND TRANS_MODE=?TRANS_MODE AND STATUS =1";
                        break;
                    }


                #endregion

            }
            return query;
        }
        #endregion
    }
}

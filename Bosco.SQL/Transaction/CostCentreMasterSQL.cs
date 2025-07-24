using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class CostCentreMasterSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.MasterTransactionCostCentre).FullName)
            {
                query = GetMasterCostCentre();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetMasterCostCentre()
        {
            string query = "";
            SQLCommand.MasterTransactionCostCentre sqlCommandId = (SQLCommand.MasterTransactionCostCentre)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.MasterTransactionCostCentre.Add:
                    {
                        query = "INSERT INTO VOUCHER_CC_TRANS ( " +
                               "VOUCHER_ID, " +
                               "LEDGER_ID," +
                               "COST_CENTRE_ID, " +
                               "AMOUNT ) VALUES( " +
                               "?VOUCHER_ID, " +
                               "?LEDGER_ID, " +
                               "?COST_CENTRE_ID, " +
                               "?AMOUNT)";
                        break;
                    }

                case SQLCommand.MasterTransactionCostCentre.Delete:
                    {
                        query = "DELETE FROM  VOUCHER_CC_TRANS WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }

                case SQLCommand.MasterTransactionCostCentre.FetchAll:
                    {
                        query = "SELECT " +
                                "MC.COST_CENTRE_ID , " +
                                "VC.AMOUNT " +
                                "FROM VOUCHER_CC_TRANS AS VC " +
                                "INNER JOIN MASTER_COST_CENTRE AS MC ON VC.COST_CENTRE_ID=MC.COST_CENTRE_ID " +
                                " WHERE VC.COST_CENTRE_ID=0 ";
                        break;
                    }
               
                case SQLCommand.MasterTransactionCostCentre.FetchCostCentreByLedger:
                    {
                        query = "SELECT VC.VOUCHER_ID, MC.COST_CENTRE_ID ,COST_CENTRE_NAME , AMOUNT " +
                                "FROM VOUCHER_CC_TRANS AS VC " +
                                "INNER JOIN MASTER_COST_CENTRE MC ON " +
                                "VC.COST_CENTRE_ID=MC.COST_CENTRE_ID " +
                                "WHERE VC.VOUCHER_ID=?VOUCHER_ID AND LEDGER_ID=?LEDGER_ID";
                        break;
                    }

            }

            return query;
        }
        #endregion Bank SQL
    }
}

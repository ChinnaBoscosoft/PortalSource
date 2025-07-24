using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.Business
{
    public class BalanceProperty
    {
        public double Amount { get; set; }
        public string TransMode { get; set; }
        public ResultArgs Result { get; set; }
    }

    public class BalanceSystem : SystemBase
    {
        public enum BalanceType
        {
            OpeningBalance,
            ClosingBalance,
            CurrentBalance
        }

        private enum LiquidBalanceGroup
        {
            None = 0,
            BankBalance = 12,
            CashBalance = 13,
            FDBalance = 14
        }

        #region public Execute Methods

        public ResultArgs UpdateOpBalance(string opBalDate, int projectId, int ledgerId, double amountCurrent,
            string transModeCurrent, TransactionAction transAction)
        {
            ResultArgs result = null;
            DateTime dateOpBal = DateTime.Parse(opBalDate).AddDays(-1);
            bool hasOpBal = false;
            string ledgerOpBalDate = dateOpBal.ToShortDateString();
            double amount = 0;
            string transMode = "";
            string transFlag = "OP";

            result = GetLedgerOpBalance(projectId, ledgerId);

            if (result.Success)
            {
                DataTable dtLedgerOpBal = result.DataSource.Table;

                if (dtLedgerOpBal != null && dtLedgerOpBal.Rows.Count > 0)
                {
                    hasOpBal = true;
                    DataRow drLedgerOpBal = dtLedgerOpBal.Rows[0];
                    ledgerOpBalDate = drLedgerOpBal[this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString();
                    amount = this.NumberSet.ToDouble(drLedgerOpBal[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    transMode = drLedgerOpBal[this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                }
            }

            //Reset Balance
            if (hasOpBal)
            {
                amount = -amount;
                result = UpdateBalance(ledgerOpBalDate, projectId, ledgerId, amount, transMode, transFlag);
            }

            //if (transAction == TransactionAction.EditBeforeSave || transAction == TransactionAction.Cancel)
            //{
            //    amount = -amount;
            //}

            if (transAction != TransactionAction.Cancel)
            {
                result = UpdateBalance(ledgerOpBalDate, projectId, ledgerId, amountCurrent, transModeCurrent, transFlag);
            }

            if (result.Success && transAction == TransactionAction.Cancel)
            {
                result = DeleteBalance(projectId, ledgerId);
            }

            return result;
        }

        public ResultArgs UpdateBulkTransBalance()
        {
            int voucherId = 0;
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager())
            {
                result = dataManager.FetchData(DataSource.DataTable, "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 ORDER BY VOUCHER_DATE");

                DataTable dtTrans = result.DataSource.Table;

                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {
                        voucherId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                        result = UpdateTransBalance(voucherId, TransactionAction.New);
                        if (!result.Success) { break; }
                    }
                }
            }

            return result;
        }

        public ResultArgs UpdateTransBalance(int voucherId, TransactionAction transAction)
        {
            ResultArgs result = GetTransaction(voucherId);

            if (result.Success)
            {
                string transDate = "";
                int projectId = 0;
                int ledgerId = 0;
                double amount = 0;
                string transMode = "";
                string transFlag = "TR";

                DataTable dtTrans = result.DataSource.Table;

                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {

                        transDate = drTrans[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                        projectId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                        ledgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        amount = this.NumberSet.ToDouble(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                        transMode = drTrans[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();

                        if (transAction == TransactionAction.EditBeforeSave || transAction == TransactionAction.Cancel)
                        {
                            amount = -amount;
                        }

                        result = UpdateBalance(transDate, projectId, ledgerId, amount, transMode, transFlag);

                        if (!result.Success) { break; }
                    }
                }
            }

            return result;
        }

        public ResultArgs DeleteBalance(int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();
            bool hasBalance = HasBalance(projectId, ledgerId);

            if (!hasBalance)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.DeleteBalance))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                    result = dataManager.UpdateData();
                }
            }
            else
            {
                result.Message = "Cannot Delete the Ledger Balance. Because it contains balance";
            }

            return result;
        }

        #endregion

        #region Private Execute Methods

        private ResultArgs UpdateBalance(string transDate, int projectId, int ledgerId, double amount, 
            string transMode, string transFlag)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.UpdateBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, transDate);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, amount);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, transMode.ToUpper());
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_FLAGColumn, transFlag);
                result = dataManager.UpdateData();
            }

            return result;
        }

        #endregion

        #region private Get Methods

        private ResultArgs GetTransaction(int voucherId)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchTransaction))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        private ResultArgs GetLedgerOpBalance(int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        #endregion

        #region Public Get Methods

        public BalanceProperty GetBalance(int projectId, int ledgerId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, ledgerId, balanceDate, (int)LiquidBalanceGroup.None, balanceType);
            return balanceProperty;
        }

        public BalanceProperty GetCashBalance(int projectId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.CashBalance, balanceType);
            return balanceProperty;
        }

        public BalanceProperty GetBankBalance(int projectId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.BankBalance, balanceType);
            return balanceProperty;
        }

        public BalanceProperty GetFDBalance(int projectId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.FDBalance, balanceType);
            return balanceProperty;
        }

        private BalanceProperty GetTransBalance(int projectId, int ledgerId, string balanceDate, int groupId, BalanceType balanceType)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";
            string balance_date = balanceDate;
            object sqlCommandId = string.Empty;
            if (balanceType == BalanceType.OpeningBalance)
            {
                DateTime dateBal = DateTime.Parse(balanceDate).AddDays(-1);
                balance_date = dateBal.ToShortDateString();
            }
            else if (balanceType == BalanceType.CurrentBalance)
            {
                balance_date = "";
            }

            sqlCommandId =groupId>0? SQLCommand.TransBalance.FetchGroupSumBalance:SQLCommand.TransBalance.FetchBalance;
            using (DataManager dataManager = new DataManager(sqlCommandId,DataBaseType.HeadOffice))
            {
                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchBalance;

                if (groupId > 0)
                {
                    dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchGroupSumBalance;
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                if (balance_date != "")
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balance_date);
                }

                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);

                if (groupId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupId);
                }
                else if (ledgerId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }

                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                }
            }

            balanceProperty.Amount = amount;
            balanceProperty.TransMode = transMode;
            balanceProperty.Result = result;
            return balanceProperty;
        }

        public bool HasBalance(int projectId, int ledgerId)
        {
            bool hasBalance = false;
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.HasBalance,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {

                DataTable dtBal = result.DataSource.Table;

                if (dtBal != null && dtBal.Rows.Count > 0)
                {
                    hasBalance = (dtBal.Rows.Count > 0);
                }
            }

            return hasBalance;
        }

        #endregion
    }
}

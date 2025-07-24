using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.SQL;

namespace Bosco.Report.Base
{
    public class BalanceProperty
    {
        public double Amount { get; set; }
        public string TransMode { get; set; }
        public ResultArgs Result { get; set; }
    }

    public class BalanceSystem1 : IDisposable
    {
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        private CommonMember utilityMember = null;


        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get { return appSchema; }
        }

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        public enum BalanceType
        {
            None,
            OpeningBalance,
            ClosingBalance,
            CurrentBalance
        }

        public enum LiquidBalanceGroup
        {
            None = 0,
            BankBalance = 12,
            CashBalance = 13,
            FDBalance = 14

        }
        public enum LiquidBalanceIEGroup
        {

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
                    amount = UtilityMember.NumberSet.ToDouble(drLedgerOpBal[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
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
                result = dataManager.FetchData(DataSource.DataTable, "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS ORDER BY VOUCHER_DATE");

                DataTable dtTrans = result.DataSource.Table;

                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {
                        voucherId = UtilityMember.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
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
                        projectId = UtilityMember.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                        ledgerId = UtilityMember.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        amount = UtilityMember.NumberSet.ToDouble(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
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
                using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.DeleteBalance,DataBaseType.HeadOffice))
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

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.UpdateBalance,DataBaseType.HeadOffice))
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

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchTransaction,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        private ResultArgs GetLedgerOpBalance(int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalance,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        //Fetch CostCentre OPBalance
        private ResultArgs GetCostCentreOpBalance(int projectId, int CostCenreId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchCCOPBalance,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, CostCenreId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        //Get Leger Name & Group Name

        public string GetLegerName(int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchLedgerName,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.Scalar);
            }

            return result.DataSource.Sclar.ToString;
        }

        public string GetGroupName(int groupId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchGroupName,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, groupId);
                result = dataManager.FetchData(DataSource.Scalar);
            }

            return result.DataSource.Sclar.ToString;
        }


        #endregion

        #region Public Get Methods

        public BalanceProperty GetBalance(string projectId, int ledgerId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, ledgerId, balanceDate, (int)LiquidBalanceGroup.None, balanceType, "");
            return balanceProperty;
        }

        public BalanceProperty GetCashBalance(string projectId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.CashBalance, balanceType, "");
            return balanceProperty;
        }

        public BalanceProperty GetBankBalance(string projectId, string balanceDate, BalanceType balanceType, string bankAccId)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.BankBalance, balanceType, bankAccId);
            return balanceProperty;
        }

        public BalanceProperty GetFDBalance(string projectId, string balanceDate, BalanceType balanceType)
        {
            BalanceProperty balanceProperty = GetTransBalance(projectId, 0, balanceDate, (int)LiquidBalanceGroup.FDBalance, balanceType, "0");
            return balanceProperty;
        }

        private BalanceProperty GetTransBalance(string projectId, int ledgerId, string balanceDate, int groupId, BalanceType balanceType, string bankAccId)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";
            string balance_date = balanceDate;

            if (balanceType == BalanceType.OpeningBalance)
            {
                DateTime dateBal = DateTime.Parse(balanceDate).AddDays(-1);
                balance_date = dateBal.ToShortDateString();
            }
            else if (balanceType == BalanceType.CurrentBalance)
            {
                balance_date = "";
            }

            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
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
                    if (groupId == 13)
                        dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupId);

                    else if (groupId == 12 || groupId == 14)
                    {
                        if (bankAccId != "0" )
                        {
                            dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupId);
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, bankAccId);
                        }
                        else
                            dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupId);
                        //dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, bankAccId);
                    }

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

                    amount = UtilityMember.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
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
        public virtual void Dispose()
        {
            GC.Collect();
        }

               
        #endregion
    }
}

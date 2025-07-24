using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class BankAccountSystem : SystemBase
    {

        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BankAccountSystem()
        {
        }

        public BankAccountSystem(int BankAccountId)
        {
            FillBankAccountProperties(BankAccountId);
        }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int GroupId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsFCRAAccount { get; set; }
        public int IsBankInterestLedger { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountDate { get; set; }
        public int AccountTypeId { get; set; }
        public int BankId { get; set; }
        public string OpenedDate { get; set; }
        public string ClosedDate { get; set; }
        public string OperatedBy { get; set; }
        public string LedgerNotes { get; set; }
        public string BankAccNotes { get; set; }
        public int PeriodYr { get; set; }
        public int PeriodMth { get; set; }
        public int PeriodDay { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestAmount { get; set; }
        public string MaturityDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public bool IsFDUpdation { get; set; }
        public string FDNumber { get; set; }
        public new string TransMode { get; set; }
        public int Status { get; set; }
        public int ProjectId { get; set; }
        public int IsInterestReceivedPeriodically { get; set; }
        public int InterestTerm { get; set; }
        public int InterestPeriod { get; set; }
        public DataTable dtBreakUpDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FDRegisterId { get; set; }
        public DateTime InvestedOn { get; set; }


        #endregion

        #region Methods


        public ResultArgs DeleteBankAccountDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.BankAccountDelete))
            {
                dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchFDDetailsByID(string Trans_Mode)
        {
            if (BankAccountId != 0 && Trans_Mode != string.Empty)
            {
                using (DataManager dataMember = new DataManager(SQLCommand.FixedDeposit.FetchFDByID))
                {
                    dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                    dataMember.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, Trans_Mode);
                    resultArgs = dataMember.FetchData(DataSource.DataTable);
                    DataTable dtFDDetails = resultArgs.DataSource.Table;
                    if (resultArgs.Success && dtFDDetails != null && dtFDDetails.Rows.Count > 0)
                    {
                        FDRegisterId = dtFDDetails.Rows[0][this.AppSchema.FDRegisters.FD_REGISTER_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.FDRegisters.FD_REGISTER_IDColumn.ColumnName].ToString()) : 0;
                        BankAccountId = this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName].ToString());
                        OpenedDate = dtFDDetails.Rows[0][this.AppSchema.BankAccount.DATE_OPENEDColumn.ColumnName].ToString();
                        AccountNumber = dtFDDetails.Rows[0][this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                        PeriodDay = this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.BankAccount.PERIOD_DAYColumn.ColumnName].ToString());
                        PeriodMth = this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.BankAccount.PERIOD_MTHColumn.ColumnName].ToString());
                        PeriodYr = this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.BankAccount.PERIOD_YEARColumn.ColumnName].ToString());
                        InterestRate = this.NumberSet.ToDecimal(dtFDDetails.Rows[0][this.AppSchema.BankAccount.INTEREST_RATEColumn.ColumnName].ToString());
                        MaturityDate = this.DateSet.ToDate(dtFDDetails.Rows[0][this.AppSchema.BankAccount.MATURITY_DATEColumn.ColumnName].ToString());
                        IsInterestReceivedPeriodically = dtFDDetails.Rows[0][this.AppSchema.FDRegisters.IS_INTEREST_RECEIVED_PERIODICALLYColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.FDRegisters.IS_INTEREST_RECEIVED_PERIODICALLYColumn.ColumnName].ToString()) : 0;
                        InterestTerm = dtFDDetails.Rows[0][this.AppSchema.FDRegisters.INTEREST_TERMColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.FDRegisters.INTEREST_TERMColumn.ColumnName].ToString()) : 0;
                        InterestPeriod = dtFDDetails.Rows[0][this.AppSchema.FDRegisters.INTEREST_PERIODColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dtFDDetails.Rows[0][this.AppSchema.FDRegisters.INTEREST_PERIODColumn.ColumnName].ToString()) : 0;
                        BankName = dtFDDetails.Rows[0][this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                        BranchName = dtFDDetails.Rows[0][this.AppSchema.Bank.BRANCHColumn.ColumnName].ToString();
                        OpenedDate = dtFDDetails.Rows[0][this.AppSchema.BankAccount.DATE_OPENEDColumn.ColumnName].ToString();
                        InterestAmount = this.NumberSet.ToDecimal(dtFDDetails.Rows[0][this.AppSchema.FDRegisters.INTEREST_AMOUNTColumn.ColumnName].ToString());
                    }
                }
                // return resultArgs;
            }
            return resultArgs;
        }

        public ResultArgs FetchFDNumberById(string FDNumber, string TransMode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.FetchFDNumber))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, FDNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, TransMode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateFD(DataManager dataManager, DataTable dtBreakUpDetails, int BankId = 0)
        {
            resultArgs = UpdateFDMasterBankAccount(dataManager, dtBreakUpDetails);
            //This line skips if Fd is not edited
            // if (resultArgs.Success && FDNumber != null && FDAccountNo != null)
            resultArgs = UpdateFDRegisters(dataManager, dtBreakUpDetails, BankId);
            //Making the FD edit option as false
            IsFDUpdation = false;
            return resultArgs;
        }

        public ResultArgs UpdateTransFD(DataManager dataManager, string FDAccountNo)
        {
            resultArgs = UpdateTransFDMasterBankAccount(dataManager);
            //This line skips if Fd is not edited
            if (resultArgs.Success && MaturityDate != null)
                resultArgs = UpdateTransFDRegisters(dataManager, FDAccountNo);
            //Making the FD edit option as false
            IsFDUpdation = false;
            return resultArgs;
        }

        private ResultArgs UpdateTransFDMasterBankAccount(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.UpdateFD))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_YEARColumn, PeriodYr);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_MTHColumn, PeriodMth);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_DAYColumn, PeriodDay);
                dataManager.Parameters.Add(AppSchema.BankAccount.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(AppSchema.BankAccount.MATURITY_DATEColumn, MaturityDate);
                dataManager.Parameters.Add(AppSchema.BankAccount.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateTransFDRegisters(DataManager dataManagers, string FDAccountNo)
        {
            //using (DataManager dataManager = new DataManager(IsFDUpdation.Equals(true) ? SQLCommand.FixedDeposit.FDRegisterUpdate : SQLCommand.FixedDeposit.FDRegisterAdd))
            using (DataManager dataManager = new DataManager(FDRegisterId != 0 ? SQLCommand.FixedDeposit.FDRegisterUpdate : SQLCommand.FixedDeposit.FDRegisterAdd))
            {
                dataManager.Database = dataManagers.Database;
                //decimal intAmt = 0;
                dataManager.Parameters.Add(AppSchema.FDRegisters.FD_REGISTER_IDColumn, FDRegisterId);
                dataManager.Parameters.Add(AppSchema.FDRegisters.ACCOUNT_NOColumn, FDAccountNo);
                dataManager.Parameters.Add(AppSchema.FDRegisters.FD_NOColumn, FDNumber);
                dataManager.Parameters.Add(AppSchema.FDRegisters.INVESTED_ONColumn, InvestedOn);//Need to be changed as CurrentDate
                //dataManager.Parameters.Add(AppSchema.FDRegisters.INVESTED_ONColumn, null);//Need to be changed as CurrentDate
                dataManager.Parameters.Add(AppSchema.FDRegisters.MATURITY_DATEColumn, MaturityDate);
                dataManager.Parameters.Add(AppSchema.FDRegisters.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_AMOUNTColumn, InterestAmount);
                dataManager.Parameters.Add(AppSchema.FDRegisters.STATUSColumn, 1);
                dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_YEARColumn, PeriodYr);
                dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_MTHColumn, PeriodMth);
                dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_DAYColumn, PeriodDay);
                dataManager.Parameters.Add(AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(AppSchema.FDRegisters.TRANS_MODEColumn, "TR");
                dataManager.Parameters.Add(AppSchema.FDRegisters.IS_INTEREST_RECEIVED_PERIODICALLYColumn, IsInterestReceivedPeriodically);
                dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_TERMColumn, InterestTerm);
                dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_PERIODColumn, InterestPeriod);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateFDMasterBankAccount(DataManager dataManagers, DataTable dtBreakUpDetails)
        {
            if (dtBreakUpDetails != null)
            {
                foreach (DataRow dr in dtBreakUpDetails.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.UpdateFD))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_YEARColumn, NumberSet.ToInteger(dr["Year"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_MTHColumn, NumberSet.ToInteger(dr["Month"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_DAYColumn, NumberSet.ToInteger(dr["Day"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.INTEREST_RATEColumn, NumberSet.ToDecimal(dr["InterestRate"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.MATURITY_DATEColumn, DateSet.ToDate(dr["MaturityDate"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.AMOUNTColumn, NumberSet.ToDecimal(dr["Amount"].ToString()));
                        dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, NumberSet.ToInteger(dr["BankAccountId"].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateFDRegisters(DataManager dataManagers, DataTable dtBreakUpDetails, int BankId = 0)
        {
            if (dtBreakUpDetails != null)
            {
                foreach (DataRow dr in dtBreakUpDetails.Rows)
                {
                    if (dr["BreakUpAccountNo"].ToString() != null)
                    {
                        using (DataManager dataManager = new DataManager(dr["DataFlag"].ToString().Equals("Add") ? SQLCommand.FixedDeposit.FDRegisterAdd : SQLCommand.FixedDeposit.FDRegisterUpdate))
                        {
                            dataManager.Database = dataManagers.Database;
                            dataManager.Parameters.Add(AppSchema.FDRegisters.FD_REGISTER_IDColumn, NumberSet.ToInteger(dr["FDREGISTERID"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.ACCOUNT_NOColumn, dr["BreakUpAccountNo"].ToString());
                            // dataManager.Parameters.Add(AppSchema.FDRegisters.FD_NOColumn, dr["FDNumber"].ToString());
                            dataManager.Parameters.Add(AppSchema.FDRegisters.INVESTED_ONColumn, DateSet.ToDate(dr["CREATED_ON"].ToString()));//Need to be changed as CurrentDate
                            dataManager.Parameters.Add(AppSchema.FDRegisters.MATURITY_DATEColumn, DateSet.ToDate(dr["MaturityDate"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.AMOUNTColumn, NumberSet.ToDecimal(dr["Amount"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_RATEColumn, NumberSet.ToDecimal(dr["InterestRate"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_AMOUNTColumn, NumberSet.ToDecimal(dr["InterestAmount"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.STATUSColumn, 1);
                            dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_YEARColumn, NumberSet.ToInteger(dr["Year"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_MTHColumn, NumberSet.ToInteger(dr["Month"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.PERIOD_DAYColumn, NumberSet.ToInteger(dr["Day"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankId.Equals(0) ? NumberSet.ToInteger(dr["BankAccountId"].ToString()) : BankId);// NumberSet.ToInteger(dr["BankAccountId"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.TRANS_MODEColumn, dr["TransMode"].ToString());
                            dataManager.Parameters.Add(AppSchema.FDRegisters.IS_INTEREST_RECEIVED_PERIODICALLYColumn, NumberSet.ToInteger(dr["IS_INTEREST_RECEIVED_PERIODICALLY"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_TERMColumn, NumberSet.ToInteger(dr["INTEREST_TERM"].ToString()));
                            dataManager.Parameters.Add(AppSchema.FDRegisters.INTEREST_PERIODColumn, NumberSet.ToInteger(dr["INTEREST_PERIOD"].ToString()));
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                }

            }
            return resultArgs;
        }
        public void FillBankAccountProperties(int BankAccountId)
        {
            resultArgs = FetchBankAccountDetailsById(BankAccountId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BankAccountId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn.ColumnName].ToString());
                AccountCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName].ToString();
                AccountNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                AccountHolderName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn.ColumnName].ToString();
                AccountTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn.ColumnName].ToString());
                AccountTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn.ColumnName].ToString());
                BankId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.BANK_IDColumn.ColumnName].ToString());
                OpenedDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_OPENEDColumn.ColumnName].ToString();
                ClosedDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString();
                OperatedBy = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.OPERATED_BYColumn.ColumnName].ToString();
                PeriodYr = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.PERIOD_YEARColumn.ColumnName].ToString());
                PeriodMth = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.PERIOD_MTHColumn.ColumnName].ToString());
                PeriodDay = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.PERIOD_DAYColumn.ColumnName].ToString());
                InterestRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString());
                ClosedDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString();
                PeriodMth = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.PERIOD_MTHColumn.ColumnName].ToString());
                PeriodYr = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.PERIOD_YEARColumn.ColumnName].ToString());
                InterestRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.INTEREST_RATEColumn.ColumnName].ToString());
                Amount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.AMOUNTColumn.ColumnName].ToString());
                MaturityDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.MATURITY_DATEColumn.ColumnName].ToString());
                BankAccNotes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.NOTESColumn.ColumnName].ToString();
                IsFCRAAccount =this.NumberSet.ToInteger( resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn.ColumnName].ToString());
            }
        }

        public int FetchLedgerId(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.LedgerIdFetch))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchBankAccountId(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountIdFetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchMaturityDate(int BankAcctId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchMaturityDate))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAcctId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        public int FetchLedgerCostCenterId(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCostCenterId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs FetchBankAccountDetailsById(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountFetch))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchBankAccountCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankAccountCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using AcMEDSync.Model;

namespace Bosco.Model.Transaction
{
    public class FDRenewalSystem : SystemBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int FDId { get; set; }
        public int FDRegisterId { get; set; }
        public string BankAccount { get; set; }
        public string FDAccountNumber { get; set; }
        public string FDNumber { get; set; }
        public DateTime DepostiedOn { get; set; }
        public DateTime MaturedOn { get; set; }
        public decimal FDAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public int ProjectId { get; set; }
        public DateTime BalanceDate { get; set; }
        public int BankAccountId { get; set; }
        public decimal InterestRate { get; set; }
        public int Status { get; set; }
        public int LedgerId { get; set; }
        public new string TransMode { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int PeriodDate { get; set; }
        public string FDStatus { get; set; }
        public DataTable FDInterestSource { get; set; }
        public bool InterestType { get; set; }


        #endregion

        #region FD Renewal Properties
        public int FDAccountId { get; set; }
        public double IntrestAmount { get; set; }
        public double WithdrawAmount { get; set; }
        public int IntrestLedgerId { get; set; }
        public int BankLedgerId { get; set; }
        public DateTime RenewedDate { get; set; }
        public DateTime WithdrawDate { get; set; }
        public string RenewalType { get; set; }
        public double IntrestRate { get; set; }
        public int FDRenewalStatus { get; set; }

        #endregion

        #region Constructor
        public FDRenewalSystem()
        {

        }

        public FDRenewalSystem(int FDId, string maturedOn, string FDNo, string TransMode)
        {
            BankAccountId = FDId;
            this.MaturedOn = this.DateSet.ToDate(maturedOn, false);
            FDNumber = FDNo;
            this.TransMode = TransMode;
            FillProperties();
        }

        #endregion

        #region Public Methods
        public ResultArgs FetchFixedDeposit()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                // dataManager.Parameters.Add(this.AppSchema.FDRegisters.MATURITY_DATEColumn, BalanceDate);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllFD()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.FDRenewal.FetchAll))
            {
                dataManger.Parameters.Add(this.AppSchema.FDRegisters.STATUSColumn, FDStatus);
                dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveFixedDeposit(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(FDRegisterId == 0 ? SQLCommand.FDRenewal.Add : SQLCommand.FDRenewal.Update))
            {
                resultArgs = UpdateFDStatusById();
                if (resultArgs.Success)
                {
                    resultArgs = UpdateBankAccount();
                    if (resultArgs.Success)
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_REGISTER_IDColumn, FDRegisterId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.ACCOUNT_NOColumn, FDAccountNumber);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_NOColumn, FDNumber);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.INVESTED_ONColumn, DepostiedOn);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.MATURITY_DATEColumn, MaturedOn);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, FDAmount);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.INTEREST_RATEColumn, InterestRate);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.INTEREST_AMOUNTColumn, InterestAmount);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, TransMode);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.STATUSColumn, Status);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.PERIOD_YEARColumn, PeriodYear);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.PERIOD_MTHColumn, PeriodMonth);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.PERIOD_DAYColumn, PeriodDate);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                if (resultArgs.Success)
                {
                    if (FDInterestSource != null && FDInterestSource.Rows.Count > 0)
                        resultArgs = SaveFDInterest(dataManager);
                    //  resultArgs = UpdateFDOpBalance(TransactionAction.EditAfterSave);
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveFDInterest(DataManager renewwalManager)
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.Narration = FDInterestSource.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = DepostiedOn;
                voucherTransaction.dtTransInfo = FDInterestSource;
                voucherTransaction.Status = 1;
                voucherTransaction.FDLedgerId = LedgerId;
                voucherTransaction.InterestType = InterestType;
                voucherTransaction.CreatedOn = this.DateSet.ToDate(this.DateSet.GetDateToday(false), false);
                voucherTransaction.ModifiedOn = this.DateSet.ToDate(this.DateSet.GetDateToday(false), false);
                voucherTransaction.CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
                voucherTransaction.ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
                resultArgs = voucherTransaction.SaveFixedDepositInterest(renewwalManager);

            }
            return resultArgs;
        }


        public void FillProperties()
        {
            resultArgs = FetchRenewalbyId();
            try
            {
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DepostiedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INVESTED_ONColumn.ColumnName] != DBNull.Value ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INVESTED_ONColumn.ColumnName].ToString(), false) : DateTime.Now;
                    MaturedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INVESTED_ONColumn.ColumnName] != DBNull.Value ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                    FDNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.FD_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.FD_NOColumn.ColumnName].ToString() : string.Empty;
                    FDAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName].ToString()) : 0;
                    InterestRate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                    InterestAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                    PeriodYear = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_YEARColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_YEARColumn.ColumnName].ToString()) : 0;
                    PeriodMonth = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_MTHColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_MTHColumn.ColumnName].ToString()) : 0;
                    PeriodDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_DAYColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRegisters.PERIOD_DAYColumn.ColumnName].ToString()) : 0;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        public ResultArgs UpdateBankAccount()
        {
            // resultArgs = UpdateFDOpBalance(TransactionAction.EditBeforeSave);
            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateFDBankAccount))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.INTEREST_RATEColumn, InterestRate);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.MATURITY_DATEColumn, MaturedOn);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.AMOUNTColumn, FDAmount);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveFDRenewal()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveFixedDeposit(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs UpdateFDOpBalance(TransactionAction transAction)
        {
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                resultArgs = balanceSystem.UpdateOpBalance(MaturedOn.ToShortDateString(), ProjectId, LedgerId, this.NumberSet.ToDouble(FDAmount.ToString()), string.Empty, transAction);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDRenewalByAccountNumber()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.ACCOUNT_NOColumn, FDAccountNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_REGISTER_IDColumn, FDRegisterId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteFDRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDRegisters))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.ACCOUNT_NOColumn, FDAccountNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_NOColumn, FDNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.MATURITY_DATEColumn, MaturedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_REGISTER_IDColumn, FDRegisterId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateFDRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateLastFDRow))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.ACCOUNT_NOColumn, FDAccountNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_NOColumn, FDNumber);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.MATURITY_DATEColumn, MaturedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_REGISTER_IDColumn, FDRegisterId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchFDRegisterDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFDRegisters))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.STATUSColumn, FDStatus);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region PrivatedMethods
        private ResultArgs FetchRenewalbyId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, TransMode);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.MATURITY_DATEColumn, MaturedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.FD_NOColumn, FDNumber);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private ResultArgs UpdateFDStatusById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveFdRenewalDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveFdDetails();
                if (resultArgs.Success)
                {

                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveFdDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, IntrestLedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, BankLedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, RenewedDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.MATURITY_DATEColumn, MaturedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn, IntrestAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn, WithdrawAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_RATEColumn, IntrestRate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_TYPEColumn, RenewalType);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, FDStatus);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}

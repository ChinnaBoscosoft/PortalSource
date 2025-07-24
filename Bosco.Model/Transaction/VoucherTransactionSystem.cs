using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace Bosco.Model.Transaction
{
    public class VoucherTransactionSystem : SystemBase
    {
        #region Constructor
        public VoucherTransactionSystem()
        {

        }
        public VoucherTransactionSystem(int VoucherID)
        {
            FillVoucherDetails(VoucherID);
        }
        #endregion

        #region Decelaration
        ResultArgs resultArgs = new ResultArgs();
        bool isEditMode = false;
        public DataTable dtTransInfo = null;
        private DataSet dsCostCentre = new DataSet();
        private DataSet dsVoucher = new DataSet();
        CommonMember Member = new CommonMember();
        #endregion

        #region Properties
        #region Voucher Master Properties
        public int VoucherId { get; set; }
        public int FDVoucherId { get; set; }  //FD Realization
        public int FDInterestVoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectIDs { get; set; }
        public string ProjectName { get; set; }
        public int TransVoucherMethod { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public int DonorId { get; set; }
        public int PurposeId { get; set; }
        public string ContributionType { get; set; }
        public decimal ContributionAmount { get; set; }
        public int CurrencyCountryId { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal CalculatedAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public int ExchageCountryId { get; set; }
        public string Narration { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int FDGroupId { get; set; }
        //public int FDYear { get; set; }
        //public int FDMth { get; set; }
        //public int FDDay { get; set; }
        public int ModifiedBy { get; set; }
        public string VoucherSubType { get; set; }
        public string FDTransType { get; set; }
        public string NameAddress { get; set; }
        public int BranchId { get; set; }
        public string BranchIds { get; set; }
        public string BranchOfficeCode { get; set; }
        public DateTime VoucherDateFrom { get; set; }
        public DateTime VoucherDateTo { get; set; }

        #endregion

        #region Common Properties
        public int TransVoucherType { get; set; }
        public int GroupId { get; set; }
        public DateTime BalanceDate { get; set; }
        #endregion

        #region Voucher Transaction Properties
        public int SequenceNo { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
        public new string TransMode { get; set; }
        public string CashTransMode { get; set; }
        public string LedgerFlag { get; set; }
        public string ChequeNo { get; set; }
        public string MaterializedOn { get; set; }
        public int LocationId { get; set; }

        #endregion

        #region Voucher CostCentre Properties
        public int CostCenterId { get; set; }
        public decimal CostCentreAmount { get; set; }
        #endregion

        #region Dashboard Properties
        public DateTime dtdsDateFrom { get; set; }
        public DateTime dtdsDateTo { get; set; }
        public int dtdsBranchOfficeId { get; set; }
        public int dtdsLegalentityId { get; set; }
        public string dtdsProjectId { get; set; }
        #endregion

        #region TDS Properties

        public int DeducteeTypeId { get; set; }

        #endregion
        #endregion

        #region Voucher FD Properties
        // public decimal InterestRate { get; set; }
        // public decimal FDAmount { get; set; }
        // public DateTime MaturedDate { get; set; }
        public string FDAccountNo { get; set; }
        public int FDLedgerId { get; set; }
        public string FDInterestId { get; set; }
        public bool InterestType { get; set; }
        //  public string FDFlag { get; set; }
        //  public string FDNumber { get; set; }

        #endregion

        #region Voucher Master Methods
        private ResultArgs FetchVoucherMasterDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherMaster.FetchAll))
            {
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchMaterDetailsById(int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterByID))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMasterByBranchLocationVoucherId(int BranchId, int LocationId, int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterByBranchLocationVoucherId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransaction(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransactionMadeForProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, LedgerIDCollection);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs MadeTransactionForLedger(string ProjectIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransactionMadeForLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIDCollection);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherMasterDetails(DataManager dm)
        {
            using (DataManager dataManager = new DataManager((VoucherId == 0) ? SQLCommand.VoucherMaster.Add : SQLCommand.VoucherMaster.Update))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId, true);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubType);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn, (string.IsNullOrEmpty(ContributionType) ? "N" : ContributionType));
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, ContributionAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CurrencyCountryId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn, ExchangeRate);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, CalculatedAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, ActualAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn, ExchageCountryId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_ONColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_ONColumn, ModifiedOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NAME_ADDRESSColumn, NameAddress);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs RemoveVoucher(DataManager dManager)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dManager.Database;
                resultArgs = DeleteCCVoucherDetails();
                if (resultArgs.Success)
                {
                    resultArgs = DeleteVoucherDetails();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs = DeleteVoucherMasterDetails();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            resultArgs = DeleteFDRenewalDetails();
                        }
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteFDAccountDetails();
                        }
                        if (resultArgs.Success)
                        {
                            resultArgs = ClearBalance(BranchId);
                        }
                        if (resultArgs.Success)
                        {
                            new ErrorLog().WriteError("Delete Voucher" + "Delete Voucher Balance is success");
                        }
                        else
                        {
                            new ErrorLog().WriteError("Delete Voucher " + resultArgs.Message);
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherTrans(int branchid)
        {
            BranchId = branchid;
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                resultArgs = RemoveVoucher(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherDetails()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.Delete, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteFDAccountDetails()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.DeleteFDAccount, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;

        }

        private ResultArgs DeleteFDRenewalDetails()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.DeleteFDRenewal, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherMasterDetails()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.Delete, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();

            }
            return resultArgs;
        }

        private ResultArgs DeleteCCVoucherDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteCCVoucher, DataBaseType.HeadOffice))
            {
                //dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();

            }
            return resultArgs;
        }

        public ResultArgs FetchTransFDBalance(string branchId, string projectId, string balanceDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransFDCBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchId);
                DateTime balanceDte = this.DateSet.ToDate(balanceDate, false).AddDays(-1);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balanceDte);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        ///  Chinna on 18.04.2023
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="projectId"></param>
        /// <param name="balanceDate"></param>
        /// <returns></returns>
        public ResultArgs FetchTransBankBalance(string branchId, string projectId, string balanceDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransCBBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchId);
                DateTime balanceDte = this.DateSet.ToDate(balanceDate, false);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balanceDte);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, 12);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveTransactions(BankAccountSystem fdUpdation = null)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveVoucherDetails(dataManager, fdUpdation);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }


        //private ResultArgs RemoveFixedDeposit()
        //{
        //    resultArgs = FetchTransactions(VoucherId.ToString());
        //    if (resultArgs.Success)
        //    {
        //        DataTable dtTrans = resultArgs.DataSource.Table;
        //        if (dtTrans != null)
        //        {
        //            foreach (DataRow dr in dtTrans.Rows)
        //            {
        //                int ledId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
        //                using (LedgerSystem ledgersystem = new LedgerSystem())
        //                {
        //                    ledgersystem.LedgerId = ledId;
        //                    int LedgerGroupId = ledgersystem.FetchLedgerGroupById();
        //                    if (LedgerGroupId == (int)FixedLedgerGroup.FixedDeposit)
        //                    {
        //                        int BankAccountId;
        //                        int FDStatus;
        //                        BankAccountId = new LedgerSystem().FetchBankAccountById(ledId);
        //                        FDStatus = FetchFixedDepositStatus(BankAccountId);
        //                        if (FDStatus == (int)FixedDepositStatus.Deposited)    //Deposited FD
        //                        {
        //                            resultArgs = DeleteFixedDepositByID(BankAccountId);
        //                            if (resultArgs.Success)
        //                            {
        //                                resultArgs = UpdateBankAccountById(BankAccountId);
        //                            }
        //                        }
        //                        else     //Realized FD
        //                        {
        //                            resultArgs = UpdateFDStatusByID((int)FixedDepositStatus.Realized, BankAccountId);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}


        private ResultArgs FetchMasterVoucherDetails(int branchid, int projectid, DateTime datefrom, DateTime dateto, string vouchertype)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterVoucherDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchid);
                if (projectid != 0)
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, datefrom);
                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, dateto);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, vouchertype);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchVouchers(int branchid, int projectid, DateTime datefrom, DateTime dateto, string vouchertype)
        {
            try
            {
                resultArgs = FetchMasterVoucherDetails(branchid, projectid, datefrom, dateto, vouchertype);
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }

        //public ResultArgs ClearBalance(int BranchId, int ProjectId, DateTime DateFrom, DateTime DateTo, string TransFlag)
        //{
        //    try
        //    {
        //        if (TransFlag == "TR")
        //        {
        //            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteOPBalance, DataBaseType.HeadOffice))
        //            {
        //                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
        //                if (ProjectId != 0)
        //                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //                if (!DateFrom.ToString().Equals("01/01/0001 00:00:00"))
        //                {
        //                    dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom.AddDays(-1));
        //                    dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo.AddDays(-1));
        //                }
        //                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_FLAGColumn, TransFlag);
        //                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //                resultArgs = dataManager.UpdateData();
        //            }
        //        }
        //        else
        //        {
        //            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteOPFDOPBalance, DataBaseType.HeadOffice))
        //            {
        //                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
        //                if (ProjectId != 0)
        //                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //                if (!DateFrom.ToString().Equals("01/01/0001 00:00:00"))
        //                {
        //                    dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom.AddDays(-1));
        //                    dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo.AddDays(-1));
        //                }
        //                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_FLAGColumn, TransFlag);
        //                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //                resultArgs = dataManager.UpdateData();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return resultArgs;
        //}

        public ResultArgs ClearBalance(int BranchId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteOPBalance, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherMasterById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherMasterById, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ExecuteHeadOfficeUpdateQuery(string query)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                new ErrorLog().WriteError("Query Executed from portal user");
                new ErrorLog().WriteError(query);
                resultArgs = dataManager.UpdateData(query);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs ExecuteHeadOfficeSelectQuery(string query)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                new ErrorLog().WriteError("Query Executed from portal user");
                new ErrorLog().WriteError(query);
                resultArgs = dataManager.FetchData(DataSource.DataTable, query);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }
        #endregion

        #region voucher Transaction Methods
        public ResultArgs Deletevouchertransactiondetails(DataManager dm, int voucherId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.VoucherTransDetails.Delete))
            {
                datamanager.Database = dm.Database;
                datamanager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchTransactionDetails(int ProjectID, string Voucher_Type, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, Voucher_Type);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchJournalTransactionDetails(int ProjectID, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchJournalDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransactions(int VoucherID, int BranchId, int LocationId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransactionDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherID);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        private ResultArgs DeleteFixedDepositByID(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDByID))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private int FetchFixedDepositStatus(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFixedDepositStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs UpdateFDStatusByID(int Status, int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateStatusByID))
            {

                dataManager.Parameters.Add(AppSchema.FDRegisters.STATUSColumn, 1);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateBankAccountById(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.UpdateFD))
            {
                int zero = 0;
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_YEARColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_MTHColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_DAYColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.INTEREST_RATEColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.MATURITY_DATEColumn, string.Empty);
                dataManager.Parameters.Add(AppSchema.BankAccount.AMOUNTColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs AssignTransactionDetails()
        {
            ResultArgs result = FetchTransDetails();
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();

                if (result.Success)
                {
                    int ledgerId = 0;
                    dsCostCentre.Clear();

                    DataTable dtTrans = result.DataSource.Table;

                    if (dtTrans != null)
                    {
                        foreach (DataRow drTrans in dtTrans.Rows)
                        {
                            ledgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());

                            result = CheckLedgerMapped(ledgerId);
                            if (!result.Success) { break; }

                            result = AssignCostCentreDetails();
                            if (!result.Success) { break; }
                        }
                    }

                    if (resultArgs.Success)
                    {
                        this.TransInfo = dtTrans.DefaultView;
                        this.CostCenterInfo = dsCostCentre;

                        ResultArgs results = FetchCashBankDetails();

                        if (result.Success)
                        {
                            int LedgerId = 0;

                            DataTable dtCashTrans = results.DataSource.Table;

                            if (dtCashTrans != null)
                            {
                                foreach (DataRow drCashTrans in dtCashTrans.Rows)
                                {
                                    LedgerId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());

                                    result = CheckLedgerMapped(LedgerId);

                                    if (!result.Success) { break; }
                                }
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return result;
        }

        public ResultArgs AssignCostCentreDetails()
        {
            resultArgs = GetCostCentreDetails();

            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable CostCentreInfo = resultArgs.DataSource.Table;
                CostCentreInfo.TableName = LedgerId.ToString();
                dsCostCentre.Tables.Add(CostCentreInfo);

                resultArgs = MapCostCentresToProject(CostCentreInfo);
            }
            return resultArgs;
        }
        public ResultArgs FetchVoucherDate(int BranchId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherDate, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    DataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DateTime FetchOPBalanceDate(int BranchId, int ProjectId)
        {
            DateTime dtBalanceDate = new DateTime();
            using (DataManager DataManager = new DataManager(SQLCommand.VoucherMaster.FetchOPBalanceDate, DataBaseType.HeadOffice))
            {
                if (BranchId != 0)
                {
                    DataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                }
                if (ProjectId != 0)
                {
                    DataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = DataManager.FetchData(DataSource.Scalar);

                dtBalanceDate = DateSet.ToDate(resultArgs.DataSource.Sclar.ToString, false) == DateTime.Now.Date ? this.Member.DateSet.ToDate("01/01/0001 00:00:00", false) : DateSet.ToDate(resultArgs.DataSource.Sclar.ToString, false);

            }
            return dtBalanceDate;
        }

        private ResultArgs MapCostCentresToProject(DataTable dtCostCentreInfo)
        {
            DataTable dtCostCentre = dtCostCentreInfo;
            using (MappingSystem mapping = new MappingSystem())
            {
                foreach (DataRow drCostCentreInfo in dtCostCentreInfo.Rows)
                {
                    int CostCentreId = 0;
                    string TransMode = string.Empty;
                    CostCentreId = this.NumberSet.ToInteger(drCostCentreInfo[mapping.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());

                    mapping.CostCenterId = CostCentreId;
                    mapping.ProjectId = ProjectId;

                    resultArgs = mapping.CheckCostCentreMapped();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                    {
                        resultArgs.Message = "Cost Centre is not mapped to this project";
                        resultArgs.Success = false;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs CheckLedgerMapped(int LedgerId)
        {
            using (MappingSystem mapping = new MappingSystem())
            {
                mapping.LedgerId = LedgerId;
                mapping.ProjectId = ProjectId;

                resultArgs = mapping.CheckLedgerMapped();

                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                {
                    resultArgs.Message = "Ledgers are not mapped to this project";
                    resultArgs.Success = false;
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchTransDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransDetails, DataBaseType.HeadOffice))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            MessageRender.ShowMessage("");
            return resultArgs;
        }

        public ResultArgs FetchJournalDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchJournalDetailById, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchCashBankDetails, DataBaseType.HeadOffice))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, CashTransMode);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransOPBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIDs);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransClosingBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransOPBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIDs);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherTransactionDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherTransDetails.FetchAll, DataBaseType.HeadOffice))
            {
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private void CloseFixedDeposit()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.LedgerId = LedgerId;
                int FDGroup = ledgerSystem.FetchLedgerGroupById();
                if (FDGroup == (int)FixedLedgerGroup.FixedDeposit) { UpdateFDStatus(); }
            }
        }

        private ResultArgs SaveTransactionDetails(DataManager dm, BankAccountSystem fdUpdation)
        {
            DataTable dtTransInfo = this.TransInfo.ToTable();
            int Count = 1;
            if (dtTransInfo != null)
            {
                foreach (DataRow drTrans in dtTransInfo.Rows)
                {
                    //Journal Trasaction Save
                    if (VoucherType == "JN")
                    {
                        LedgerId = this.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                        if (this.NumberSet.ToDecimal(drTrans["DEBIT"].ToString()) > 0)
                        {
                            Amount = this.NumberSet.ToDecimal(drTrans["DEBIT"].ToString());
                            TransMode = "DR";
                        }
                        else if (this.NumberSet.ToDecimal(drTrans["CREDIT"].ToString()) > 0)
                        {
                            Amount = this.NumberSet.ToDecimal(drTrans["CREDIT"].ToString());
                            TransMode = "CR";
                        }
                        SequenceNo = Count++;
                    }
                    else
                    {
                        LedgerId = FDLedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        Amount = this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                        TransMode = TransMode;
                        ChequeNo = drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();

                        MaterializedOn = drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ? drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;
                        LedgerFlag = string.Empty;
                        SequenceNo = Count++;

                        if (VoucherType == "CN")
                        {
                            //  CloseFixedDeposit();
                        }
                        else { if (this.HasCostCentre(LedgerId.ToString())) { resultArgs = SaveCostCentreInfo(); } }
                    }

                    resultArgs = SaveVoucherTransactionDetails(dm);

                    if (!resultArgs.Success) { break; }
                }
            }


            if (this.CashTransInfo != null && VoucherType != "JN")
            {
                DataTable dvCashTransInfo = this.CashTransInfo.ToTable();
                foreach (DataRow drTrans in dvCashTransInfo.Rows)
                {
                    LedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                    Amount = this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                    ChequeNo = drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                    //  fdUpdation.InvestedOn = VoucherDate;
                    MaterializedOn = drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ? drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;
                    LedgerFlag = string.Empty;
                    TransMode = CashTransMode;
                    SequenceNo = Count++;
                    if (FDGroupId == (int)FixedLedgerGroup.FixedDeposit) { resultArgs = UpdateFDDetails(fdUpdation); }
                    resultArgs = SaveVoucherTransactionDetails(dm);

                    if (!resultArgs.Success) { break; }
                }
            }
            //  FDInterestVoucherId = resultArgs.RowUniqueId != null ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
            return resultArgs;
        }

        public ResultArgs UpdateFDDetails(BankAccountSystem fdUpdation)
        {
            using (DataManager dataManager = new DataManager())
            {
                using (BankAccountSystem fd = new BankAccountSystem())
                {
                    fdUpdation.BankAccountId = new LedgerSystem().FetchBankAccountById(LedgerId);
                    fdUpdation.TransMode = "TR";
                    fdUpdation.InvestedOn = VoucherDate;
                    resultArgs = fdUpdation.UpdateTransFD(dataManager, FDAccountNo);
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherTransactionDetails(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.Add))  //((VoucherId == 0) ? SQLCommand.VoucherTransDetails.Add : SQLCommand.VoucherTransDetails.Edit))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_FLAGColumn, LedgerFlag);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.STATUSColumn, Status);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchTransactionDetailsById(int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransactionByID, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                    SequenceNo = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                    LedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                    Amount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                    TransMode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                    LedgerFlag = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_FLAGColumn.ColumnName].ToString();
                    ChequeNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                    if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName] != DBNull.Value)
                    {
                        MaterializedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString();
                    }
                    Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.STATUSColumn.ColumnName].ToString());
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchTransVoucherDetails(string VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchJournalTransDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 05/07/2018, Check transaction is avilable upto given date for particular project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckTransVoucherDetailsByDateProject(Int32 ProjectId, DateTime VoucherDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckTransExistsByDateProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/2018, Check transaction is avilable upto given date for particular project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckTransVoucherExists(int BranchOfficeId, DateTime datefrom, DateTime dateto)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckBranchTransExist, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchOfficeId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, datefrom);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, dateto);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        #endregion

        #region Voucher CostCentre Methods
        public ResultArgs FetchVoucherCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }



        public ResultArgs GetCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.FetchCostCentreByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs SaveVoucherCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, CostCenterId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.AMOUNTColumn, CostCentreAmount);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherFDInterest(DataManager voucherDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.VoucherFDInterestAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.FD_VOUCHER_IDColumn, FDVoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.FD_LEDGER_IDColumn, FDLedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.BK_INT_VOUCHER_IDColumn, FDInterestVoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.BK_INT_LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateFDStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateFDStatus))
            {
                int BankAcctId = new LedgerSystem().FetchBankAccountById(LedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAcctId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveCostCentreInfo()
        {
            DataTable dvCostCentreInfo = this.GetCostCentreByLedgerID(LedgerId.ToString()).ToTable();

            foreach (DataRow drCostCentre in dvCostCentreInfo.Rows)
            {
                CostCenterId = this.NumberSet.ToInteger(drCostCentre[this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                CostCentreAmount = this.NumberSet.ToDecimal(drCostCentre[this.AppSchema.VoucherCostCentre.AMOUNTColumn.ColumnName].ToString());

                resultArgs = SaveVoucherCostCentre();

                if (!resultArgs.Success) { break; }
            }

            return resultArgs;
        }

        public ResultArgs DeleteVoucherCostCentreDetails(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.Delete))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherFDInterest(DataManager voucherDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.VoucherFDInterestDelete))
            {
                dataManager.Database = voucherDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, FDInterestVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Dashboard Methods
        public ResultArgs FetchDashboardCashBankFlow()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DashboardCashBankFlow, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                if (dtdsProjectId != "0" && !string.IsNullOrEmpty(dtdsProjectId))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dtdsProjectId);
                }
                if (dtdsLegalentityId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, dtdsLegalentityId);
                }
                if (dtdsBranchOfficeId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Datasync_Task.BRANCH_OFFICE_IDColumn, dtdsBranchOfficeId);
                }
                // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDashboardReceiptPayment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DashboardReceiptPayments, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                if (dtdsProjectId != "0" && !string.IsNullOrEmpty(dtdsProjectId))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dtdsProjectId);
                }
                if (dtdsLegalentityId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, dtdsLegalentityId);
                }
                if (dtdsBranchOfficeId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Datasync_Task.BRANCH_OFFICE_IDColumn, dtdsBranchOfficeId);
                }
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDataSynStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DataSynStatusbyMonth, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                if (!string.IsNullOrEmpty(BranchOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDataSynNoStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DataSynStatusNobyMonth, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //Fetches Total vouchers available for the particular project by branch
        public ResultArgs FetchDataSynStatusProjectWise()
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDataSynStatusProjectWise, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                    if (BranchId > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchProjectsbyLoginUser(BranchId);
                }
            }
            return resultArgs;
        }

        /// <summary>
        ///  fetch 
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        private ResultArgs FetchBranchProjectsbyLoginUser(int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDataSynStatusProjectUserWise, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //Fetches non Conformity Branches
        public ResultArgs FetchNonConformityBranches()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchNonConformityBranches, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dtdsDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dtdsDateTo);
                if ((base.IsBranchOfficeAdminUser || base.IsBranchOfficeUser) && BranchId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVouchersInOtherProjectsOrDates(int BranchId, int LocationId, string ProjectId, DateTime dateform, DateTime dateto)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVouchersInOtherProjectsOrDates, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                if (!string.IsNullOrEmpty(ProjectId))
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);

                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, dateform);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region Common Methods
        private void SetVoucherMethod()
        {

            if (VoucherType == VoucherTransType.Receipt.ToString() || VoucherType == "RC")
            {
                VoucherType = "RC";
                TransVoucherType = (int)VoucherTransType.Receipt;
                TransMode = TransactionMode.CR.ToString();
                CashTransMode = TransactionMode.DR.ToString();
            }
            else if (VoucherType == VoucherTransType.Payment.ToString() || VoucherType == "PY")
            {
                VoucherType = "PY";
                TransVoucherType = (int)VoucherTransType.Payment;
                TransMode = TransactionMode.DR.ToString();
                CashTransMode = TransactionMode.CR.ToString();
            }
            else if (VoucherType == VoucherTransType.Contra.ToString() || VoucherType == "CN")
            {
                VoucherType = "CN";
                TransVoucherType = (int)VoucherTransType.Contra;
                TransMode = TransactionMode.CR.ToString();
                CashTransMode = TransactionMode.DR.ToString();
            }
            else
            {
                TransVoucherType = (int)VoucherTransType.Journal;
            }
        }

        public DataSet LoadVoucherDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            string VoucherID = string.Empty;
            DataSet dsTransaction = new DataSet();
            try
            {
                resultArgs = FetchTransactionDetails(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                    for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                    {
                        VoucherID += resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString() + ",";
                    }
                    VoucherID = VoucherID.TrimEnd(',');
                    // resultArgs = FetchVoucherTransactionDetails();

                    //VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                    //resultArgs = FetchTransactions(VoucherID);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Ledger";
                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                        dsTransaction.Relations.Add(dsTransaction.Tables[1].TableName, dsTransaction.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTransaction.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dsTransaction;
        }

        public ResultArgs SaveVoucherDetails(DataManager saveTransDataManager, BankAccountSystem fdUpdation = null)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = saveTransDataManager.Database;
                //isEditMode = false;
                // dataManager.BeginTransaction();
                SetVoucherMethod();
                if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                {
                    using (NumberSystem numberSystem = new NumberSystem())
                    {
                        VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.VoucherNumber, ProjectId.ToString(), TransVoucherType);
                    }
                }

                if (VoucherId > 0)
                {
                    isEditMode = true;
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditBeforeSave);
                    }
                    Deletevouchertransactiondetails(dataManager, VoucherId);
                    DeleteVoucherCostCentreDetails(dataManager);
                }

                resultArgs = SaveVoucherMasterDetails(dataManager);

                if (resultArgs.Success && resultArgs.RowsAffected != 0)
                {
                    VoucherId = VoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                    FDVoucherId = VoucherId;
                    resultArgs = SaveTransactionDetails(dataManager, fdUpdation);

                    if (resultArgs.Success)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (isEditMode)
                                balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditAfterSave);
                            else
                                balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.New);
                        }
                    }
                }

                //To post the FD Interest
                dtTransInfo = this.FixedDepositInterestInfo;
                if (dtTransInfo != null && dtTransInfo.Rows.Count > 0)
                    SaveFixedDepositInterest(dataManager);

                //dataManager.EndTransaction();
            }

            return resultArgs;
        }

        public ResultArgs SaveFixedDepositInterest(DataManager voucherdataManager)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = voucherdataManager.Database;
                // bool isEditMode = false;
                //dataManager.BeginTransaction();
                //SetVoucherMethod();
                FDVoucherId = VoucherId;
                VoucherId = 0;
                //if (FDTransType != FDTypes.WD.ToString())
                //{
                VoucherType = (InterestType == true) ? "JN" : "RC";
                TransVoucherType = (InterestType == true) ? (int)VoucherTransType.Journal : (int)VoucherTransType.Receipt;
                //}
                //else
                //{
                //    VoucherType = "CN";
                //    TransVoucherType = (int)VoucherTransType.Contra;
                //}
                TransMode = TransactionMode.CR.ToString();
                CashTransMode = TransactionMode.DR.ToString();
                if (string.IsNullOrEmpty(VoucherNo))
                {
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        using (NumberSystem numberSystem = new NumberSystem())
                        {
                            VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.VoucherNumber, ProjectId.ToString(), TransVoucherType);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VoucherNo))
                {
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        using (NumberSystem numberSystem = new NumberSystem())
                        {
                            if (FDTransType == FDTypes.WD.ToString())
                            {
                                VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.ReceiptNumber, ProjectId.ToString(), TransVoucherType);
                            }
                            else
                            {
                                VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.VoucherNumber, ProjectId.ToString(), TransVoucherType);
                            }
                        }
                    }
                }

                // placed on 27-03-2014 at 10.05 pm
                //if (VoucherId > 0)
                //{
                //    isEditMode = true;
                //    using (BalanceSystem balanceSystem = new BalanceSystem())
                //    {
                //        balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditBeforeSave);
                //    }
                //    Deletevouchertransactiondetails(dataManager, VoucherId);
                //    DeleteVoucherCostCentreDetails(dataManager);
                //}
                if (FDInterestVoucherId > 0)
                {
                    isEditMode = true;
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        balanceSystem.UpdateTransBalance(FDInterestVoucherId, TransactionAction.EditBeforeSave);
                    }
                    Deletevouchertransactiondetails(dataManager, FDInterestVoucherId);
                    // DeleteVoucherFDInterest(dataManager);
                }


                Narration = dtTransInfo.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                if (isEditMode) { VoucherId = FDInterestVoucherId; }
                resultArgs = SaveVoucherMasterDetails(dataManager);

                if (resultArgs.Success && resultArgs.RowsAffected != 0)
                {
                    VoucherId = FDInterestVoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : FDInterestVoucherId;
                    SequenceNumber.ReSetSequenceNumber();
                    foreach (DataRow dr in dtTransInfo.Rows)
                    {
                        LedgerId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        Amount = this.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                        TransMode = dr["TRANS_MODE"].ToString();
                        SequenceNo = SequenceNumber.GetSequenceNumber();
                        resultArgs = SaveVoucherTransactionDetails(dataManager);
                    }
                    //if (resultArgs.Success)
                    //{
                    //    FDInterestVoucherId = VoucherId;
                    //    resultArgs = SaveVoucherFDInterest(dataManager);
                    //}

                    if (resultArgs.Success)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (isEditMode)
                                //balanceSystem.UpdateTransBalance(NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()), TransactionAction.EditAfterSave);
                                balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditAfterSave);
                            else
                                balanceSystem.UpdateTransBalance(resultArgs.RowUniqueId != null ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0, TransactionAction.New);
                        }
                    }
                }

                // dataManager.EndTransaction();
            }
            return resultArgs;
        }


        public ResultArgs FillVoucherDetails(int VoucherID)
        {
            resultArgs = FetchMaterDetailsById(VoucherID);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName] != DBNull.Value)
                {
                    VoucherDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                }
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                ProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                VoucherType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                DonorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.DONOR_IDColumn.ColumnName].ToString());
                PurposeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PURPOSE_IDColumn.ColumnName].ToString());
                ContributionType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn.ColumnName].ToString();
                ContributionAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                CurrencyCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                ExchangeRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                CalculatedAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                ActualAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                ExchageCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn.ColumnName].ToString());
                Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                ///   Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.STATUSColumn.ColumnName].ToString());

                //if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_ONColumn.ColumnName] != DBNull.Value)
                //{
                //    CreatedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_ONColumn.ColumnName].ToString(), false);
                //}

                //if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_ONColumn.ColumnName] != DBNull.Value)
                //{
                //    ModifiedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_ONColumn.ColumnName].ToString(), false);
                //}
                CreatedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString());
                ModifiedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_BYColumn.ColumnName].ToString());
                NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherNo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherStartingNo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransVoucherType);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public string FetchLastVoucherDate(int proId, DateTime dtYearFrom, DateTime dtYearTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLastVoucherDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, dtYearFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, dtYearTo);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }


        public ResultArgs FetchBRSDetails(int ProjectID, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRS))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs IsFDInterestPosted()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDPostedInterest()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherInterestByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, FDInterestVoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchFDVoucherPostedInterest(int FDAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherPostedInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public ResultArgs UpdateBRSDetails(DataTable dtBRS)
        {
            foreach (DataRow dr in dtBRS.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateBRS))
                {
                    MaterializedOn = dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ? dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, dr[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName]);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, dr[AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName]);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }



        public DataSet LoadJournalVoucherDetails(int projectId, DateTime dtDateFrom, DateTime dtDateTo)
        {
            string VoucherId = string.Empty;
            DataSet dsJournal = new DataSet();

            resultArgs = FetchJournalTransactionDetails(projectId, dtDateFrom, dtDateTo);

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                resultArgs.DataSource.Table.TableName = "Master";
                dsJournal.Tables.Add(resultArgs.DataSource.Table);

                for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                {
                    VoucherId += this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString()) + ",";
                }
                VoucherId = VoucherId.TrimEnd(',');

                resultArgs = FetchTransVoucherDetails(VoucherId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Ledgers";
                    dsJournal.Tables.Add(resultArgs.DataSource.Table);
                    dsJournal.Relations.Add(dsJournal.Tables[1].TableName, dsJournal.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsJournal.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                }
            }
            return dsJournal;
        }
        #endregion

        #region TDS

        public ResultArgs FetchActiveDeductTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDeducteeTypes, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDeducteeTaxDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchTaxDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel.Master;
using AcMEDSync.Model;

namespace Bosco.Model.UIModel
{
    public class LedgerSystem : SystemBase
    {

        #region Variable Decelaration
        ResultArgs resultArgs = null;
        public string BranchOfficeCode { get; set; }
        public int MappingLedgerId { get; set; }// This ledgeris is to be mapped with the Project Category.
        public List<object> lProjectCategoryId;
        #endregion

        #region Constructor
        public LedgerSystem()
        {
        }

        public LedgerSystem(int LedgerId)
        {
            FillLedgerProperties(LedgerId, DataBaseType.HeadOffice);
        }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int CreditorsProfileId { get; set; }

        public int NatureOfPaymentId { get; set; }
        public int DeducteeTypeId { get; set; }
        public string LedgerProfileName { get; set; }
        public string LedgerProfileAddress { get; set; }
        public string LedgerProfileEmail { get; set; }
        public string LedgerProfileContactNo { get; set; }
        public string LedgerProfilePincode { get; set; }
        public string LedgerProfilePanNo { get; set; }
        public int LedgerProfileCountryId { get; set; }
        public int LedgerProfileStateId { get; set; }


        public int GroupId { get; set; }
        public int BudgetGroupId { get; set; }
        public int BudgetSubGroupId { get; set; }
        public int FDInvTypeId { get; set; }
        public int GeneralateGroupLedgerId { get; set; }

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
        public string MaturityDate { get; set; }
        public int ProjectId { get; set; }
        public int SortId { get; set; }
        public int FDLedgerBankAccountId { get; set; }
        public DataTable dtMappingLedgers { get; set; }
        public bool FDLeger { get; set; }
        public int MapLedgerId { get; set; }
        private int LedgerProfileId { get; set; }
        public string FDType { get; set; }
        public int ProjectCategoryId { get; set; }

        public int IsCostCentre { get; set; }
        public int IsTDSApplicable { get; set; }
        public int IsFDInterestLedger { get; set; }
        public int IsFCRAAccount { get; set; }
        public int IsFDPenaltyLedger { get; set; }
        public int IsSBInterestLedger { get; set; }
        public int IsBankCommissionLedger { get; set; }

        public int IsAssetGainLedger { get; set; }
        public int IsAssetLossLedger { get; set; }
        public int IsInKindLedger { get; set; }
        public int IsDepriciationLedger { get; set; }
        public int IsAssetDisposalLedger { get; set; }
        public int tempData { get; set; }

        // On 25/11/2021, to set and define when ledger is closed
        public DateTime LedgerClosedOn { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchLedgerDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAll, connectTo))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerWithNature(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllWithNature, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLedgers(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBranchLedger, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBankAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountFetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFixedDepositDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FixedDepositFetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFixedDepositCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFixedDepositCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgerLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerForLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckBankLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public ResultArgs FetchCashBankFDLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankFDLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDinterestLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankInterestLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteLedgerDetails(int LedgerId, DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.Delete, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 25/10/2021, To update Closed date for ledger after checking its vouchers and balances
        /// #1. Check Vouchers are available for Clsoed Date and for al the Proejcts
        /// #2. Check closing balances for Clsoed Date and for all the Proejcts
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckLedgerClosedDate(Int32 LId, DateTime DateClosed)
        {
            ResultArgs result = new ResultArgs();
            // Validate while closing the Bank Accounts if there is Transaction Exists or not if exists make it false in order to do the Transaction (Chinna)
            if (LId > 0 && DateClosed != DateTime.MinValue)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //#1. Check Vouchers are available for clsoed date
                    result = ledgersystem.CheckTransactionExistsByDateClosed(LId, DateClosed);
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            result.Message = "Transaction is made for this Closed Date (" + DateSet.ToDate(DateClosed.ToShortDateString()) + "), Ledger can not be closed.";
                        }
                        else
                        {
                            //On 28/10/2021, To check Ledger Closing balance only for Bank and FD Ledgers alone
                            //#2. Check Closing Balances are available for all the projects
                            LedgerId = LId;
                            int LgrpId = FetchLedgerGroupById();

                            if (LgrpId == (int)FixedLedgerGroup.BankAccounts || LgrpId == (int)FixedLedgerGroup.FixedDeposit)
                            {
                                using (BalanceSystem balancesystem = new BalanceSystem())
                                {
                                    BalanceProperty ledgerclosingbalance = balancesystem.HasBalance(DateSet.ToDate(DateClosed.ToShortDateString()), 0, 0, LId, BalanceSystem.BalanceType.ClosingBalance);
                                    if (ledgerclosingbalance.Amount > 0)
                                    {
                                        result.Message = "Ledger has closing balance, It can't be closed on " + DateSet.ToDate(DateClosed.ToShortDateString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                result.Success = true;
            }

            return result;
        }

        private ResultArgs CheckTransactionExistsByDateClosed(int CLedgerId, DateTime DateClosed)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionExistsByDateClose, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, CLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs SaveLedger()
        //{
        //    //Begin
        //    //call Save Ledger Details
        //    //Map Project
        //    //End
        //    return resultArgs;
        //}

        public ResultArgs FetchFdLedgersByProject()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FetchFDLedgers))
            {
                dataMember.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveLedgerDetails(DataManager ledgerManager, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.Update, connectTo))
            {
                dataManager.Database = ledgerManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsFDInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSApplicable);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn, IsAssetGainLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn, IsAssetLossLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn, IsInKindLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn, IsDepriciationLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn, IsAssetDisposalLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, BudgetGroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, BudgetSubGroupId);

                dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, FDInvTypeId);

                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsFDPenaltyLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsSBInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionLedger);

                // On 25/11/2021, to set and define when ledger is closed---------------------------------
                if (LedgerClosedOn != null)
                {
                    if (LedgerClosedOn == DateTime.MinValue)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerClosedOn);
                    }
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                }

                //On 05/07/2023 - To update closed by
                dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, (LedgerClosedOn != null && LedgerClosedOn != DateTime.MinValue ? 1 : 0));
                //--------------------------------------------------------------------------------------------
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.UpdateData();
                //  MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
            }
            return resultArgs;

        }

        private ResultArgs MappingLedger(DataManager dataManager)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.dtMappingLedger = dtMappingLedgers;
                mappingSystem.OpeningBalanceDate = BookBeginFrom;
                mappingSystem.IsFDLedger = FDLeger;
                mappingSystem.LedgerId = MapLedgerId;
                mappingSystem.FDTransType = FDType;
                resultArgs = mappingSystem.AccountMappingByLedgerId(dataManager);
            }
            return resultArgs;
        }


        public void FillLedgerProperties(int LedgerId, DataBaseType connectTo)
        {
            resultArgs = FetchLedgerDetailsById(LedgerId, connectTo);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                LedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                LedgerCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                LedgerName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                GroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                GeneralateGroupLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["CON_LEDGER_ID"].ToString());
                LedgerType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName].ToString();
                LedgerSubType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                BankAccountId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn.ColumnName].ToString());
                IsCostCentre = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                IsFDInterestLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                LedgerNotes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.NOTESColumn.ColumnName].ToString();

                LedgerProfileName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.NAMEColumn.ColumnName].ToString();
                LedgerProfileAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.ADDRESSColumn.ColumnName].ToString();
                LedgerProfileEmail = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.EMAILColumn.ColumnName].ToString();
                LedgerProfileContactNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.CONTACT_NUMBERColumn.ColumnName].ToString();
                LedgerProfilePincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PIN_CODEColumn.ColumnName].ToString();
                LedgerProfilePanNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PAN_NUMBERColumn.ColumnName].ToString();
                LedgerProfileCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                LedgerProfileStateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_IDColumn.ColumnName].ToString());
                NatureOfPaymentId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.NATURE_OF_PAYMENT_IDColumn.ColumnName].ToString());
                DeducteeTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.DEDUTEE_TYPE_IDColumn.ColumnName].ToString());
                IsAssetGainLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName].ToString());
                IsAssetLossLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName].ToString());
                IsInKindLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName].ToString());
                IsDepriciationLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName].ToString());
                IsAssetDisposalLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName].ToString());
                BudgetGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName].ToString());
                BudgetSubGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName].ToString());
                FDInvTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());

                IsFDPenaltyLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName].ToString());
                IsSBInterestLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName].ToString());
                IsBankCommissionLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName].ToString());

                LedgerClosedOn = DateTime.MinValue;
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName] != null &&
                    !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString()))
                {
                    LedgerClosedOn = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                }
            }
        }

        private ResultArgs FetchLedgerDetailsById(int LedgerId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectCategoryByLedger(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchProjectCategorybyLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteBankAccountDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.BankAccountDelete))
            {
                dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveBankLedger(bool isFDledger, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager())
            {
                //dataManager.BeginTransaction();
                //resultArgs = SaveBankAccountDetails(dataManager, MapLedgerId);
                //if (resultArgs.Success && resultArgs.RowsAffected > 0)
                //{
                //    BankAccountId = (BankAccountId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BankAccountId;
                //    resultArgs = SaveLedgerDetails(dataManager);
                //    MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                //    if (resultArgs.Success)
                //        if (!isFDledger) { MappingLedger(dataManager); }
                //}
                //dataManager.EndTransaction();
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails(dataManager, connectTo);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                    if (resultArgs.Success)
                        if (!isFDledger) { MappingLedger(dataManager); }
                    resultArgs = SaveBankAccountDetails(dataManager, MapLedgerId);
                    BankAccountId = (BankAccountId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BankAccountId;
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveLedger(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails(dataManager, connectTo);
                MappingLedgerId = LedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : LedgerId;

                if (MappingLedgerId > 0)
                {
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        projectSystem.LedgerId = MappingLedgerId;
                        resultArgs = projectSystem.DeleteMappedProjectCategory();
                        if (resultArgs.Success)
                        {
                            if (lProjectCategoryId != null)
                            {
                                foreach (object projectCategoryId in lProjectCategoryId)
                                {
                                    resultArgs = projectSystem.MapLedger(this.NumberSet.ToInteger(projectCategoryId.ToString()), MappingLedgerId);
                                    if (!resultArgs.Success)
                                        break;
                                }
                            }
                        }
                    }
                }


                if (IsTDSApplicable == 1)
                {
                    MapLedgerId = LedgerId;
                    MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                    LedgerProfileId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
                        {
                            ledgerProfileSystem.LedgerID = LedgerProfileId;
                            ledgerProfileSystem.LedgerGroupID = GroupId;
                            ledgerProfileSystem.CreditorsProfileId = CreditorsProfileId;
                            ledgerProfileSystem.ProfileLedgerName = this.LedgerProfileName;
                            ledgerProfileSystem.ProfileAddress = LedgerProfileAddress;
                            ledgerProfileSystem.ProfileState = this.NumberSet.ToInteger(LedgerProfileStateId.ToString());
                            ledgerProfileSystem.ProfileCountry = this.NumberSet.ToInteger(LedgerProfileCountryId.ToString());
                            ledgerProfileSystem.Email = LedgerProfileEmail;
                            ledgerProfileSystem.PANNo = LedgerProfilePanNo;
                            ledgerProfileSystem.ProfilePinCode = LedgerProfilePincode;
                            ledgerProfileSystem.MobileNumber = LedgerProfileContactNo;
                            ledgerProfileSystem.NatureofPaymentid = NatureOfPaymentId;
                            resultArgs = ledgerProfileSystem.DeleteLedgerProfile(connectTo);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = ledgerProfileSystem.SaveLedgeProfile(dataManager, connectTo);
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs DeleteBankAccount(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteBankAccountDetails();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    LedgerId = FetchLedgerId(BankAccountId, connectTo);
                    resultArgs = DeleteLedgerDetails(LedgerId, connectTo);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }
        //private ResultArgs SaveLedgerProfileInfo(DataManager dataManager)
        //{
        //    using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
        //    {
        //        ledgerProfileSystem.LedgerID = LedgerProfileId;
        //        ledgerProfileSystem.CreditorsProfileId = CreditorsProfileId;
        //        ledgerProfileSystem.ProfileLedgerName = LedgerProfileName;
        //        ledgerProfileSystem.State = LedgerProfileStateId;
        //        ledgerProfileSystem.Country = LedgerProfileCountryId;
        //        ledgerProfileSystem.Email = LedgerProfileEmail;
        //        ledgerProfileSystem.PANNo = LedgerProfilePanNo;
        //        ledgerProfileSystem.PinCode = LedgerProfilePincode;
        //        ledgerProfileSystem.MobileNumber = LedgerProfileContactNo;
        //        resultArgs = ledgerProfileSystem.SaveLedgeProfile(dataManager);
        //    }
        //    return resultArgs;
        //}


        private int FetchLedgerId(int BankAccountId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.LedgerIdFetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchInKindLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByLedgerGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgerCodes(DataBaseType connectTo)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.LedgerBank.FetchLedgerCodes, connectTo))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveBankAccountDetails(DataManager bankAccManager, int SLedgerId)
        {
            using (DataManager dataManager = new DataManager((BankAccountId == 0) ? SQLCommand.LedgerBank.BankAccountAdd : SQLCommand.LedgerBank.BankAccountUpdate))
            {
                dataManager.Database = bankAccManager.Database;
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_CODEColumn, AccountCode);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNumber);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn, AccountHolderName);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, AccountTypeId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_OPENEDColumn, OpenedDate);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, (string.IsNullOrEmpty(ClosedDate)) ? null : ClosedDate);// "2013/10/24"(string.IsNullOrEmpty(ClosedDate)) ? null : ClosedDate
                dataManager.Parameters.Add(this.AppSchema.BankAccount.OPERATED_BYColumn, OperatedBy);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_YEARColumn, PeriodYr);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_MTHColumn, PeriodMth);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_DAYColumn, PeriodDay);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.MATURITY_DATEColumn, (string.IsNullOrEmpty(MaturityDate) ? null : MaturityDate)); //MaturityDate //(string.IsNullOrEmpty(MaturityDate) ? "2013-10-24" : MaturityDate)
                dataManager.Parameters.Add(this.AppSchema.BankAccount.NOTESColumn, BankAccNotes);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, SLedgerId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
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

        public int FetchLedgerNature()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerNature))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int FetchLedgerNatureByLedgerGroup(int GroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerNatureByLedgerGroup, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public string GetLedgerIdsByLedgerGroup(int GroupId)
        {
            string rtn = "0";
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerIdsByLedgerGroup, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success && resultArgs.DataSource.Sclar != null)
            {
                rtn = resultArgs.DataSource.Sclar.ToString;
            }
            return rtn;
        }

        public int FetchLedgerGroupById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerGroupbyLedgerId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchBankAccountById(int ledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankAccountById))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ledgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchFixedDepositByLedgersByProject()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FixedDepositByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, FDLedgerBankAccountId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs SaveFDLedger()
        //{
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.BeginTransaction();
        //        resultArgs = SaveFDMaserLedger(dataManager);
        //        dataManager.EndTransaction();
        //    }
        //    return resultArgs;
        //}

        //private ResultArgs SaveFDMaserLedger(DataManager fdMasterDataManager)
        //{

        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Database = fdMasterDataManager.Database;

        //    }
        //    return resultArgs;
        //}

        //public ResultArgs DeleteProjectLedger()
        //{
        //    using (DataManager dataMager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingDelete))
        //    {
        //        dataMager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //        resultArgs = dataMager.UpdateData();
        //        if (resultArgs.Success)
        //        {
        //            resultArgs = SaveProjectLedger();
        //        }
        //    }
        //    return resultArgs;
        //}

        public ResultArgs SaveProjectLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs LedgerFetchAll(string branchOfficeCode, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.LedgerFetchAll, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, branchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                resultArgs.DataSource.Table.TableName = "Ledger";
            }
            return resultArgs;
        }

        public string GetLegerName(int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchLedgerName, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.Scalar);
            }

            return result.DataSource.Sclar.ToString;
        }

        public ResultArgs GetLegerNameByLedgerIds(string LedgerIds)
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerNameByLedgerIds, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDSColumn.ColumnName, LedgerIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        public Int32 GetLegerId(string ledgername)
        {
            ResultArgs result = new ResultArgs();
            Int32 ledgerid = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerIdByLedgerName, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, ledgername);
                result = dataManager.FetchData(DataSource.Scalar);
            }

            if (result.Success && result.DataSource != null)
            {
                ledgerid = NumberSet.ToInteger(result.DataSource.Sclar.ToString);
            }
            return ledgerid;
        }

        public ResultArgs FetchLedgersByProjectCategory()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByProjectCategory, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCategoryId);
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs FetchDefaultLedgerbyProjectCategoryDetails()
        {
            using (DataManager dataMangaer = new DataManager(SQLCommand.LedgerBank.FetchLedgerDefaultByProjectCategory, DataBaseType.HeadOffice))
            {
                dataMangaer.Parameters.Add(AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCategoryId);
                dataMangaer.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataMangaer.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDefaultLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBranchOfficeDeafultLedger, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs MapDefaultLedgersforAllProjectCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.MapDefaultLedgerMapping, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs LoadBudgetGroupforLoodkup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBudgetGroup, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadBudgetSubGroupforLoodkup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBudgetSubGroup, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadFDInvestmentforLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFDInvestment, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}

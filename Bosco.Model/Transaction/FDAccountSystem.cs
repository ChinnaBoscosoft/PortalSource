using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Collections;
using System;
using Bosco.Model.UIModel;
using AcMEDSync.Model;


namespace Bosco.Model.Transaction
{
    public class FDAccountSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        private string VoucherType = "Contra";
        bool isEditMode = false;
        double LedgerAmt = 0;
        #endregion

        #region Constructor
        public FDAccountSystem()
        {

        }
        public FDAccountSystem(int FDAccountId)
        {
            FillFDAccountDetails(FDAccountId);
        }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int GroupId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsBankInterestLedger { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string AccountDate { get; set; }
        public int AccountTypeId { get; set; }
        public int BankId { get; set; }
        public string OpenedDate { get; set; }
        public string ClosedDate { get; set; }
        public string OperatedBy { get; set; }
        public string LedgerNotes { get; set; }
        public string BankAccNotes { get; set; }
        public int FDVoucherId { get; set; }
        public int ProjectId { get; set; }
        public int SortId { get; set; }
        public string ProId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BranchId { get; set; }
        #endregion

        #region FD Account Properties

        public int FDAccountId { get; set; }
        public string FDAccountNumber { get; set; }
        public string FDAccountHolderName { get; set; }
        public double FdAmount { get; set; }
        public double FDInterestAmount { get; set; }
        public double FDInterestRate { get; set; }
        public double FDPrinicipalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime FDMaturityDate { get; set; }
        public string FDTransType { get; set; }
        public string FDTransMode { get; set; }
        public string FDProjectName { get; set; }
        public string FDLedgerCurBalance { get; set; }
        public int CashBankId { get; set; }
        public string ReceiptNo { get; set; }
        public string FDVoucherNo { get; set; }
        public int TransVoucherMethod { get; set; }
        public string FDOPInvestmentDate { get; set; }
        public DataTable dtFDAcountDetails { get; set; }
        public DataTable dtCashBankLedger { get; set; }
        public DataTable dtFDLedger { get; set; }
        public string CashBankLedgerGroup { get; set; }
        public string CashBankLedgerAmt { get; set; }
        public string FDLedgerAmt { get; set; }
        public int VoucherId { get; set; }
        public int PrevProjectId { get; set; }
        public int PrevLedgerId { get; set; }
        public int IntrestMode { get; set; }
        public int FDStatus { get; set; }

        #endregion

        #region FD Renewal Properties
        public double IntrestAmount { get; set; }
        public double WithdrawAmount { get; set; }
        public int IntrestLedgerId { get; set; }
        public int BankLedgerId { get; set; }
        public int FDIntrestVoucherId { get; set; }
        public DateTime RenewedDate { get; set; }
        public DateTime WithdrawDate { get; set; }
        public string RenewalType { get; set; }
        public double IntrestRate { get; set; }
        public int FDRenewalStatus { get; set; }
        public int FDLedgerId { get; set; }
        public string FDRenewalType { get; set; }
        public double PrinicipalAmount { get; set; }
        public double FDInterstCalAmount { get; set; }
        public double FDCashBankWithdrawAmount { get; set; }
        public int FDRenewalInterestId { get; set; }
        public double PrinicipalInsAmount { get; set; }
        public int FDRenewalId { get; set; }
        public string FDType { get; set; }
        #endregion

        #region Ledger Methods
        public ResultArgs FetchLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FetchFDLedgers))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFDLedgerById(int FDledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFDLedgerById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, FDledgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchProjectByLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchProjectByLedger))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFDRegistersView()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchFDRegistersView, DataBaseType.HeadOffice))
            {
                dataMember.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, DateFrom);
                dataMember.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, DateTo);
                if (ProjectId!=0)
                    dataMember.Parameters.Add(this.AppSchema.FDRegisters.PROJECT_IDColumn, ProjectId);
                dataMember.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchDate, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllProjectId()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchAllProjectId))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SaveFdLedger()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails(dataManager);
                if (resultArgs.Success)
                {
                    if (LedgerId != 0)
                    {
                        LedgerId = LedgerId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : LedgerId;
                        //string[] projectId = ProId.Split(',');
                        //for (int i = 0; i < projectId.Length; i++)
                        //{
                        //    ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                        resultArgs = DeleteProjectLedger(dataManager);
                        // }
                    }
                    else
                    {
                        LedgerId = LedgerId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : LedgerId;
                        string[] projectId = ProId.Split(',');
                        for (int i = 0; i < projectId.Length; i++)
                        {
                            ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                            resultArgs = SaveProjectLedger(dataManager);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveProjectLedger(DataManager projectLedgerManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Database = projectLedgerManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveLedgerDetails(DataManager ledgerDataManager)
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.Update))
            {
                dataManager.Database = ledgerDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode.ToUpper());
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectLedger(DataManager projectLedgerManager)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.FDAccount.DeleteProjectLedger))
            {
                dataManger.Database = projectLedgerManager.Database;
                //  dataManger.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManger.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManger.UpdateData();
                if (resultArgs.Success)
                {
                    string[] projectId = ProId.Split(',');
                    for (int i = 0; i < projectId.Length; i++)
                    {
                        ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                        resultArgs = SaveProjectLedger(dataManger);
                    }
                }
            }
            return resultArgs;
        }

        #endregion

        #region FD Account Details

        private ResultArgs SaveFDAccountDetails(DataManager dataOpBalance)
        {
            using (DataManager dataManager = new DataManager(FDAccountId == 0 ? SQLCommand.FDAccount.Add : SQLCommand.FDAccount.Update))
            {
                dataManager.Database = dataOpBalance.Database;
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, FDAccountNumber);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn, FDAccountHolderName);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INVESTMENT_DATEColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.AMOUNTColumn, FdAmount);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_RATEColumn, FDInterestRate);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_AMOUNTColumn, FDInterestAmount);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MATURED_ONColumn, FDMaturityDate);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDTransType);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_MODEColumn, FDTransMode);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.RECEIPT_NOColumn, ReceiptNo);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.STATUSColumn, 1);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public string FetchCurrentLedgerBalance()
        {
            string LedgerCurBalance = string.Empty;
            //FetchOpBalanceList
            //  using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerBalance))
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceList))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, (int)FixedLedgerGroup.FixedDeposit);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow drLedgerBalance in resultArgs.DataSource.Table.Rows)
                    {
                        FdAmount = drLedgerBalance[this.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != null ? NumberSet.ToDouble(drLedgerBalance[this.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                        FDTransMode = drLedgerBalance[this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName] != null ? drLedgerBalance[this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName].ToString() : string.Empty;
                        LedgerCurBalance = NumberSet.ToCurrency(FdAmount) + " " + FDTransMode;
                    }
                }
            }
            return LedgerCurBalance;
        }

        public ResultArgs FetchFDAccounts()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDTransType);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet LoadFDRenewalDetails()
        {
            DataSet dsFDRenewal = new DataSet();
            try
            {
                string FDAccountId = string.Empty;
                resultArgs = FetchFDAccounts();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);

                    for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                    {
                        FDAccountId += resultArgs.DataSource.Table.Rows[i][AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString() + ",";
                    }

                    FDAccountId = FDAccountId.TrimEnd(',');
                    resultArgs = FetchFDRenewals(FDAccountId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(int));
                        resultArgs.DataSource.Table.TableName = "Renewal History";
                        dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);
                        if (FDType.Equals(FDTypes.RN.ToString()))
                            dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return dsFDRenewal;
        }
        public ResultArgs FetchFDRenewals(string FD_ACCOUNT_ID)
        {

            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDRenewalById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillFDAccountDetails(int FdAccId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FdAccId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    FDAccountId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                    VoucherId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                    PrevProjectId = ProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                    PrevLedgerId = LedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    BankId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.BANK_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.BANK_IDColumn.ColumnName].ToString()) : 0;
                    FDProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString() : string.Empty;
                    FDAccountHolderName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn.ColumnName].ToString() : string.Empty;
                    FDAccountNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString() : string.Empty;
                    FDVoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : string.Empty;
                    FDMaturityDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MATURED_ONColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MATURED_ONColumn.ColumnName].ToString(), false) : DateTime.Now;
                    CreatedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                    FdAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                    FDInterestRate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                    FDInterestAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                    FDTransMode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName].ToString() : string.Empty;
                    FDTransType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString() : string.Empty;
                    ReceiptNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.RECEIPT_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.RECEIPT_NOColumn.ColumnName].ToString() : string.Empty;
                    FDProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName].ToString() : string.Empty;
                    LedgerNotes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName].ToString() : string.Empty;
                    FDLedgerCurBalance = NumberSet.ToNumber(FdAmount) + " " + FDTransMode;
                }
            }
        }
        public ResultArgs FetchLedgerByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchProjectFDAccountId()
        {
            string ProjectId = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        ProjectId += dr[this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString() + ",";
                    }
                    ProjectId = ProjectId.TrimEnd(',');
                }
            }
            return ProjectId;
        }

        public string FetchProjectFDLedgerId()
        {
            string TransType = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                TransType = resultArgs.DataSource.Data != null ? resultArgs.DataSource.Data.ToString() : "";
            }
            return TransType;
        }

        private ResultArgs SaveFDInvestmentDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = VoucherTrans(dataManager);
                if (resultArgs.Success)
                {
                    VoucherId = VoucherId == 0 ? resultArgs.RowUniqueId != null ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0 : VoucherId;
                    resultArgs = SaveFDAccountDetails(dataManager);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveFDOpeningBalance()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveFDAccountDetails(dataManager);
                if (resultArgs.Success)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (isEditMode)
                        {
                            if (ProjectId != PrevProjectId || LedgerId != PrevLedgerId)
                            {
                                LedgerAmt = FetchLedgerBalance(PrevProjectId, PrevLedgerId);
                                balanceSystem.UpdateOpBalance(FDOPInvestmentDate, PrevProjectId, PrevLedgerId, LedgerAmt, FDTransMode, TransactionAction.EditAfterSave);
                            }
                            LedgerAmt = FetchLedgerBalance(ProjectId, LedgerId);
                            balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, LedgerAmt, FDTransMode, TransactionAction.New);
                        }
                        else
                        {
                            LedgerAmt = FetchLedgerBalance(ProjectId, LedgerId);
                            balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, LedgerAmt, FDTransMode, TransactionAction.New);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveFDAccount()
        {
            using (DataManager dataManager = new DataManager())
            {
                isEditMode = (FDAccountId != 0);
                if (FDTransType == FDTypes.IN.ToString())
                {
                    SaveFDInvestmentDetails();
                }
                else if (FDTransType == FDTypes.RN.ToString() || FDTransType == FDTypes.WD.ToString())
                {
                    SaveFdRenewalDetails();
                }
                else
                {
                    SaveFDOpeningBalance();
                }
            }
            return resultArgs;
        }

        private ResultArgs VoucherTrans(DataManager dataManagerVouTrans)
        {
            using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dataManagerVouTrans.Database;
                    if (FDTransType != FDTypes.RN.ToString() && FDTransType != FDTypes.WD.ToString())
                    {
                        // dtCashBankLedger.Rows.Add(LedgerId, FdAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);

                        dtCashBankLedger.Rows.Add(CashBankId, FdAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);

                        TransInfo = dtCashBankLedger.DefaultView;

                        dtFDLedger.Rows.Add(LedgerId, FdAmount, FDLedgerAmt, ledgerSubType.FD.ToString(), "", "", LedgerNotes);

                        CashTransInfo = dtFDLedger.DefaultView;
                        FixedDepositInterestInfo = null;
                    }
                    else if (FDTransType == FDTypes.RN.ToString())
                    {
                        dtCashBankLedger.Rows.Add(LedgerId, FdAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);
                        dtCashBankLedger.Rows.Add(CashBankId, FdAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", "", LedgerNotes);
                        voucherTrans.dtTransInfo = dtCashBankLedger;
                        FixedDepositInterestInfo = null;
                    }
                    else
                    {
                        dtCashBankLedger.Rows.Add(FDLedgerId, FdAmount, CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                        TransInfo = dtCashBankLedger.DefaultView;

                        dtFDLedger.Rows.Add(CashBankId, FdAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", "", LedgerNotes);
                        CashTransInfo = dtFDLedger.DefaultView;

                        DataTable dtReceiptEntry = dtCashBankLedger.Clone();
                        dtReceiptEntry.Rows.Add(LedgerId, FDInterstCalAmount, CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                        dtReceiptEntry.Rows.Add(CashBankId, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", "", LedgerNotes);

                        FixedDepositInterestInfo = dtReceiptEntry;
                    }

                    voucherTrans.VoucherId = VoucherId;
                    voucherTrans.FDInterestVoucherId = FDIntrestVoucherId;
                    voucherTrans.VoucherDate = CreatedOn;
                    voucherTrans.VoucherType = VoucherType;
                    voucherTrans.FDTransType = FDTransType;
                    voucherTrans.TransVoucherMethod = TransVoucherMethod;
                    voucherTrans.VoucherSubType = LedgerTypes.FD.ToString();
                    voucherTrans.VoucherNo = FDVoucherNo;
                    voucherTrans.ProjectId = ProjectId;
                    voucherTrans.InterestType = IntrestMode == 1 ? true : false;
                    voucherTrans.Status = 1;
                    voucherTrans.CreatedOn = DateTime.Now;
                    voucherTrans.ModifiedOn = DateTime.Now;
                    voucherTrans.CreatedBy = NumberSet.ToInteger(LoginUserId.ToString());
                    voucherTrans.ModifiedBy = NumberSet.ToInteger(LoginUserId.ToString());
                    if (FDTransType != FDTypes.RN.ToString())
                    {
                        resultArgs = voucherTrans.SaveVoucherDetails(dataManager);
                        FDRenewalInterestId = voucherTrans.FDVoucherId;
                    }
                    else
                    {
                        resultArgs = voucherTrans.SaveFixedDepositInterest(dataManager);

                        FDVoucherId = voucherTrans.FDVoucherId;
                    }
                }
            }
            return resultArgs;
        }

        private double FetchLedgerBalance(int ProjId, int LedId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
        }

        public ResultArgs RemoveFDAccountDetails()
        {
            string FDVouId = string.Empty;

            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteFdAccountDetails();

                if (FDRenewalType == FDTypes.RN.ToString() || FDRenewalType == FDTypes.WD.ToString())
                {
                    FDVouId = FetchVoucherID();
                }

                if (FDVouId == string.Empty)
                {
                    if (resultArgs.Success)
                    {
                        if (FDTransType == FDTypes.OP.ToString())
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, 0, FDTransMode, TransactionAction.EditAfterSave);
                                FdAmount = FetchLedgerBalance(ProjectId, LedgerId);
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, FdAmount, FDTransMode, TransactionAction.New);
                            }
                        }
                        else
                        {
                            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                            {
                                voucherTransSystem.VoucherId = FDVoucherId;
                                resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                            }
                        }
                    }
                }
                else
                {
                    string[] ArrayOfFDVoucherID = FDVouId.Split(',');
                    if (resultArgs.Success)
                    {
                        if (FDTransType == FDTypes.OP.ToString())
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, 0, FDTransMode, TransactionAction.EditAfterSave);
                                FdAmount = FetchLedgerBalance(ProjectId, LedgerId);
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, FdAmount, FDTransMode, TransactionAction.New);

                                for (int i = 0; i < ArrayOfFDVoucherID.Length; i++)
                                {
                                    using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                                    {
                                        voucherTransSystem.VoucherId = NumberSet.ToInteger(ArrayOfFDVoucherID[i].ToString());
                                        resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                            {
                                voucherTransSystem.VoucherId = FDVoucherId;
                                resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                for (int i = 0; i < ArrayOfFDVoucherID.Length; i++)
                                {
                                    voucherTransSystem.VoucherId = NumberSet.ToInteger(ArrayOfFDVoucherID[i].ToString());
                                    resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                }
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteFdAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeleteFDAcountDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private string FetchVoucherID()
        {
            string FDVoucherId = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        FDVoucherId += dr[this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] != null ? dr[this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString() + "," : string.Empty;

                    }
                    FDVoucherId = FDVoucherId.TrimEnd(',');
                }
            }
            return FDVoucherId;
        }
        #endregion

        #region FD Renewal  Details
        public ResultArgs SaveFdRenewalDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                {
                    resultArgs = VoucherTrans(dataManager);
                    if (resultArgs.Success)
                    {
                        FDVoucherId = resultArgs.RowUniqueId != DBNull.Value && resultArgs.RowUniqueId.ToString() != "0" ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : FDVoucherId;
                        resultArgs = SaveFdDetails(dataManager);
                        if (FDTypes.WD.ToString() == FDTransType)
                        {
                            UpdateFdAccountStatus(dataManager);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveFdDetails(DataManager dataFDRenewal)
        {
            using (DataManager dataManager = new DataManager(FDRenewalId == 0 ? SQLCommand.FDRenewal.Add : SQLCommand.FDRenewal.Update))
            {
                dataManager.Database = dataFDRenewal.Database;
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, CashBankId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDTransType == FDTypes.RN.ToString() ? FDVoucherId : FDVoucherId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDTransType == FDTypes.RN.ToString() ? FDVoucherId : FDRenewalInterestId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.MATURITY_DATEColumn, FDMaturityDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn, IntrestAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn, WithdrawAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_RATEColumn, FDInterestRate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RECEIPT_NOColumn, ReceiptNo);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.PRINICIPAL_AMOUNTColumn, PrinicipalAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_TYPEColumn, FDRenewalType);
                //dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, FDRenewalType != FDRenewalTypes.WDI.ToString() ? 1 : 0);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, 1);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GetLastRenewalDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetLastFDRenewalDate))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public double FetchAccumulatedAmount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchAccumulatedAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        private ResultArgs UpdateFdAccountStatus(DataManager closeFDDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.UpdateFDStatus))
            {
                dataManager.Database = closeFDDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchFDAccountId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchAccountIdByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchRenewalDetailsById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchFDStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using AcMEDSync.Model;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;
using AcMEDSync;
using System.Collections;


namespace Bosco.Model.UIModel.MobileService
{
    public class MobileServiceSystem : SystemBase
    {
        #region Property
        ResultArgs resultArgs = null;
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public int BranchId { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }
        public string ProjectName { get; set; }
        public string LedgerGroup { get; set; }
        public string LedgerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateAsOn { get; set; }
        public string BranchEmailIds { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        #endregion

        #region Methods

        public ResultArgs GetHeadOfficeDBConnection()
        {
            resultArgs = new ResultArgs();
            resultArgs.Success = false;
            string hoConnectionString = string.Empty;
            //Connect to the Concern Head Office Database
            using (Bosco.Model.SettingSystem settingSystem = new Bosco.Model.SettingSystem())
            {
                hoConnectionString = settingSystem.GetHeadOfficeDBConnection(HeadOfficeCode.Trim());
                UserProperty userProperty = new UserProperty();
                userProperty.HeadOfficeDBConnection = hoConnectionString;
                resultArgs.Success = string.IsNullOrEmpty(hoConnectionString) ? false : true;
                if (!resultArgs.Success)
                {
                    resultArgs.Message = MessageCatalog.MobileService.DBCONNECTION;
                }
            }

            return resultArgs;
        }

        public ResultArgs AuthenticateUser()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.User.Authenticate, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, UserName);
                    dataManager.Parameters.Add(this.AppSchema.User.PASSWORDColumn, EncryptValue(Password));
                    dataManager.Parameters.Add(this.AppSchema.User.STATUSColumn, (int)Utility.Status.Active);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (resultArgs.RowsAffected > 0)
                        {
                            DataView dvUser = new DataView(resultArgs.DataSource.Table);
                            DataTable dtUser = dvUser.ToTable("UserDetails", false,
                                    this.AppSchema.User.HEAD_OFFICE_CODEColumn.ColumnName, this.AppSchema.User.USER_NAMEColumn.ColumnName,
                                    this.AppSchema.User.PASSWORDColumn.ColumnName, this.AppSchema.User.FIRSTNAMEColumn.ColumnName,
                                    this.AppSchema.User.USERROLEColumn.ColumnName, this.AppSchema.User.CONTACT_NOColumn.ColumnName,
                                    this.AppSchema.User.EMAIL_IDColumn.ColumnName);
                            if (!string.IsNullOrEmpty(dtUser.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName].ToString()))
                            {
                                dtUser.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName] =
                                    CommonMember.DecryptValue(dtUser.Rows[0][this.AppSchema.User.PASSWORDColumn.ColumnName].ToString());
                                dtUser.AcceptChanges();
                            }
                            resultArgs.DataSource.Data = dtUser;
                        }
                    }
                }

            }
            catch (Exception ex) { resultArgs.Message = ex.Message; }
            return resultArgs;
        }

        public ResultArgs GetLoginDetails()
        {

            DataSet dsUserBranch = new DataSet("UserBranchDetails");
            try
            {
                resultArgs = AuthenticateUser();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtUser = resultArgs.DataSource.Table;
                    //check if admin user
                    if (dtUser.Rows[0]["USERROLE"].Equals(UserRole.Admin.ToString()))
                    {
                        dsUserBranch.Tables.Add(dtUser);

                        // Get Head office Profile
                        resultArgs = GetHeadOfficeProfile();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            dsUserBranch.Tables.Add(resultArgs.DataSource.Table);
                            //Get All Branches of the Head office
                            resultArgs = GetAllBranchesByHeadoffice();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                dsUserBranch.Tables.Add(resultArgs.DataSource.Table);
                                //Get All Branch Projects
                                resultArgs = GetAllBranchProjects();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    dsUserBranch.Tables.Add(resultArgs.DataSource.Table);
                                    //Get Active Financial Year
                                    resultArgs = GetActiveFinancialYear();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        dsUserBranch.Tables.Add(resultArgs.DataSource.Table);
                                        //Get Non Conforming Branches Count (Default Three months (Current Month, Previous Month
                                        DateFrom = GetDateFromNonConformity();
                                        DateTo = GetDateToNonConformity();
                                        resultArgs = GetNonConformityBranches();
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            dsUserBranch.Tables.Add(GenerateNonConformityBranchTable(resultArgs.DataSource.Table.Rows.Count));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.MobileService.InvalidLogin;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {

                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Data = dsUserBranch;
                }
            }
            return resultArgs;
        }

        public ResultArgs GetAllBranchesByHeadoffice()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchBranchDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "BranchDetails";
                }
            }
            return resultArgs;
        }

        public ResultArgs GetActiveFinancialYear()
        {
            DataTable dtFinancialYear = null;
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchActiveTransactionperiod, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataView dvFY = new DataView(resultArgs.DataSource.Table);
                    dtFinancialYear = dvFY.ToTable("FinancialYear", false, AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName, AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName);
                    resultArgs.DataSource.Data = dtFinancialYear;
                }

            }
            return resultArgs;
        }

        //Fetches non Conformity Branches
        public ResultArgs GetNonConformityBranches()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchNonConformityBranches, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                resultArgs.DataSource.Table.TableName = "BranchDefaulters";
            }
            return resultArgs;
        }

        //Fetches Total vouchers available for the particular project by branch
        public ResultArgs GetDataSynStatusProjectWise()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDataSynStatusProjectWise, DataBaseType.HeadOffice))
            {
                resultArgs = GetActiveFinancialYear();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DateFrom = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                    DateTo = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                }
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                BranchId = GetId(Id.BranchId);
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    DataView dvDataStatus = resultArgs.DataSource.Table.DefaultView;
                    dvDataStatus.RowFilter = "PROJECT='" + ProjectName + "'";
                    resultArgs.DataSource.Data = dvDataStatus.ToTable();
                    resultArgs.DataSource.Table.TableName = "BranchDataStatus";
                }

            }
            return resultArgs;
        }

        public ResultArgs GetProjectsByBranch()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsforCombo, DataBaseType.HeadOffice))
            {
                BranchId = GetId(Id.BranchId);
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                DataView dvProject = new DataView(resultArgs.DataSource.Table);
                DataTable dtProjects = dvProject.ToTable("BranchProjectDetails", false, "PROJECT");
                resultArgs.DataSource.Data = dtProjects;
            }
            return resultArgs;
        }

        public ResultArgs GetHeadOfficeProfile()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchByCode, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                resultArgs.DataSource.Table.TableName = "HeadOfficeProfile";
            }
            return resultArgs;
        }

        public ResultArgs GetAllBranchProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchAllBranchProjects, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private DateTime GetDateFromNonConformity()
        {
            DateTime dtFromDate = DateTime.Now.AddMonths(-2);
            return CommonMethod.FirstDayOfMonthFromDateTime(dtFromDate.Date);
        }

        private DateTime GetDateToNonConformity()
        {
            DateTime dtFromDate = DateTime.Now.AddMonths(-2);
            DateTime dtToDate = dtFromDate.AddMonths(2);
            return CommonMethod.LastDayOfMonthFromDateTime(dtToDate.Date);
        }

        private DataTable GenerateNonConformityBranchTable(int count)
        {
            const string colName = "BranchDefaulterCount";
            DataTable dtNonConformity = new DataTable("BranchDefaultersCount");
            dtNonConformity.Columns.Add(colName, typeof(int));
            DataRow drRow = dtNonConformity.NewRow();
            drRow[colName] = count;
            dtNonConformity.Rows.Add(drRow);
            dtNonConformity.AcceptChanges();
            return dtNonConformity;
        }

        public ResultArgs GetCheckBalanceDetails()
        {
            DataSet dsCheckBalance = new DataSet("CheckBalance");
            try
            {
                BranchId = GetId(Id.BranchId);
                ProjectId = GetId(Id.ProjectId);
                if (BranchId > 0 && ProjectId > 0)
                {
                    //Cash,Bank,FD Closing balance
                    resultArgs = GetClosingBalance();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "ClosingBalance";
                        dsCheckBalance.Tables.Add(resultArgs.DataSource.Table);

                        //Cash Detailed Balance
                        resultArgs = GetDetailedBalance(FixedLedgerGroup.Cash);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs.DataSource.Table.TableName = "CashBalanceDetail";
                            dsCheckBalance.Tables.Add(resultArgs.DataSource.Table);

                            //Bank Detailed Balance
                            resultArgs = GetDetailedBalance(FixedLedgerGroup.BankAccounts);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs.DataSource.Table.TableName = "BankBalanceDetail";
                                dsCheckBalance.Tables.Add(resultArgs.DataSource.Table);

                                //FD Detailed Balance
                                resultArgs = GetDetailedBalance(FixedLedgerGroup.FixedDeposit);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs.DataSource.Table.TableName = "FDBalanceDetail";
                                    dsCheckBalance.Tables.Add(resultArgs.DataSource.Table);
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.MobileService.ResultsNotFound;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Data = dsCheckBalance;
                }
            }
            return resultArgs;
        }

        private ResultArgs GetDetailedBalance(FixedLedgerGroup groupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransOPBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId.ToString());
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, (int)groupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, DateAsOn);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId.ToString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs.DataSource.Table.Columns.Add("TEMP_AMOUNT", typeof(string));
                    resultArgs.DataSource.Table.AcceptChanges();
                    resultArgs = FormatCheckBalanceDetailAmount(resultArgs.DataSource.Table);
                }

            }
            return resultArgs;
        }

        public ResultArgs GetAbstractDetail()
        {
            DataSet dsAbstract = new DataSet("Abstract");
            try
            {
                BranchId = GetId(Id.BranchId);
                ProjectId = GetId(Id.ProjectId);
                if (BranchId > 0 && ProjectId > 0)
                {
                    //Cash,Bank,FD Opening Balance
                    resultArgs = GetOpeningBalance();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "OpeningBalance";
                        dsAbstract.Tables.Add(resultArgs.DataSource.Table);

                        //Receipts based on the duration
                        resultArgs = GetReceipts();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            
                            resultArgs.DataSource.Table.TableName = "Receipts";
                            dsAbstract.Tables.Add(resultArgs.DataSource.Table);

                            //Payments based on the duration
                            resultArgs = GetPayments();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs.DataSource.Table.TableName = "Payments";
                                dsAbstract.Tables.Add(resultArgs.DataSource.Table);

                                //Cash,Bank,FD Closing Balance
                                resultArgs = GetClosingBalance();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs.DataSource.Table.TableName = "ClosingBalance";
                                    dsAbstract.Tables.Add(resultArgs.DataSource.Table);
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.MobileService.ResultsNotFound;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Data = dsAbstract;
                }
            }
            return resultArgs;
        }

        private ResultArgs FormatCheckBalanceDetailAmount(DataTable dtSource)
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSource.Rows)
                {
                    dr["TEMP_AMOUNT"] = this.NumberSet.ToNumber(this.NumberSet.ToDouble(dr["AMOUNT"].ToString())).ToString() +" " +dr["TRANSMODE"].ToString();
                }
                dtSource.Columns.Remove("AMOUNT");
                dtSource.Columns["TEMP_AMOUNT"].ColumnName = "AMOUNT";
                dtSource.AcceptChanges();
                resultArgs.DataSource.Data = dtSource;
            }
            return resultArgs;
        }

        private ResultArgs FormatAmount(DataTable dtSource)
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach(DataRow dr in dtSource.Rows)
                {
                    dr["AMOUNT"] = this.NumberSet.ToNumber(this.NumberSet.ToDouble(dr["TEMP_AMOUNT"].ToString())).ToString();
                }
                dtSource.AcceptChanges();
                resultArgs.DataSource.Data = dtSource;
            }
            return resultArgs;
        }

        /// <summary>
        /// Get the opening balance of Cash,Bank,FD based on Branch,Project,Date From
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetOpeningBalance()
        {
            DataTable dtOpeningBalance = new DataTable("Opening Balance");
            dtOpeningBalance.Columns.Add("ACCOUNT", typeof(string));
            dtOpeningBalance.Columns.Add("AMOUNT", typeof(string));
            dtOpeningBalance.Columns.Add("MODE", typeof(string));
            try
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    BalanceProperty bankBalance = balanceSystem.GetBankBalance(BranchId, ProjectId, DateFrom.ToShortDateString(), BalanceSystem.BalanceType.OpeningBalance);
                    BalanceProperty cashBalance = balanceSystem.GetCashBalance(BranchId, ProjectId, DateFrom.ToShortDateString(), BalanceSystem.BalanceType.OpeningBalance);
                    BalanceProperty fdBalance = balanceSystem.GetFDBalance(BranchId, ProjectId, DateFrom.ToShortDateString(), BalanceSystem.BalanceType.OpeningBalance);

                    dtOpeningBalance.Rows.Add("Cash", this.NumberSet.ToNumber(this.NumberSet.ToDouble(cashBalance.Amount.ToString())) + " " + cashBalance.TransMode.ToString(), cashBalance.TransMode.ToString());
                    dtOpeningBalance.Rows.Add("Bank", this.NumberSet.ToNumber(this.NumberSet.ToDouble(bankBalance.Amount.ToString())) + " " + bankBalance.TransMode.ToString(), bankBalance.TransMode.ToString());
                    dtOpeningBalance.Rows.Add("Fixed Deposit", this.NumberSet.ToNumber(this.NumberSet.ToDouble(fdBalance.Amount.ToString())) + " " + fdBalance.TransMode.ToString(), fdBalance.TransMode.ToString());

                    if (dtOpeningBalance != null && dtOpeningBalance.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Data = dtOpeningBalance;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Get all payments based on Branch,Project,Date From and Date To
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetReceipts()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchReceipts, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs = FormatAmount(resultArgs.DataSource.Table);
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Get all receipts based on Branch,Project,Date From and Date To
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetPayments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchPayment, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs = FormatAmount(resultArgs.DataSource.Table);
                }
                
            }
            return resultArgs;
        }

        /// <summary>
        /// Get the closing balance of Cash,Bank,FD based on Branch,Project and Date To
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetClosingBalance()
        {
            DataTable dtClosingBalance = new DataTable("Closing Balance");
            dtClosingBalance.Columns.Add("ACCOUNT", typeof(string));
            dtClosingBalance.Columns.Add("AMOUNT", typeof(string));
            dtClosingBalance.Columns.Add("MODE", typeof(string));
            try
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    BalanceProperty bankBalance = balanceSystem.GetBankBalance(BranchId, ProjectId, DateTo.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);
                    BalanceProperty cashBalance = balanceSystem.GetCashBalance(BranchId, ProjectId, DateTo.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);
                    BalanceProperty fdBalance = balanceSystem.GetFDBalance(BranchId, ProjectId, DateTo.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);

                    dtClosingBalance.Rows.Add("Cash", this.NumberSet.ToNumber(this.NumberSet.ToDouble(cashBalance.Amount.ToString())) + " " + cashBalance.TransMode.ToString(), cashBalance.TransMode.ToString());
                    dtClosingBalance.Rows.Add("Bank", this.NumberSet.ToNumber(this.NumberSet.ToDouble(bankBalance.Amount.ToString())) + " " + bankBalance.TransMode.ToString(), bankBalance.TransMode.ToString());
                    dtClosingBalance.Rows.Add("Fixed Deposit", this.NumberSet.ToNumber(this.NumberSet.ToDouble(fdBalance.Amount.ToString())) + " " + fdBalance.TransMode.ToString(), fdBalance.TransMode.ToString());

                    if (dtClosingBalance != null && dtClosingBalance.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Data = dtClosingBalance;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        public ResultArgs GetLedgerSummary()
        {
            LedgerId = GetId(Id.LedgerId);
            if (LedgerId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchLedgerSummary, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    resultArgs.DataSource.Table.TableName = "LedgerSummary";
                }
            }
            return resultArgs;
        }

        public int GetId(Id queryId)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.SQL;
                    dataManager.DataCommandArgs.ActiveDatabaseType = DataBaseType.HeadOffice;
                    switch (queryId)
                    {
                        case Id.HeadofficeId:
                            {
                                //dataManager.DataCommandArgs.SQLCommandId = SQLCommand.BranchOffice.AddMessage;
                                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                                break;
                            }
                        case Id.BranchId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.BranchOffice.FetchBranchIdByBranchCode;
                                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                                break;
                            }
                        case Id.ProjectId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.Project.FetchProjectIdByProjectName;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_NAMEColumn, ProjectName);
                                break;
                            }
                        case Id.LedgerGroupId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.LedgerGroup.FetchLedgerGroupIdbyLedgerGroup;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                                break;
                            }
                        case Id.LedgerId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.LedgerBank.FetchLedgerIdByLedgerName;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                break;
                            }
                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
                if (!resultArgs.Success)
                {
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger != 0 ? resultArgs.DataSource.Sclar.ToInteger : 0;
        }

        public ResultArgs GetLedgerDetails()
        {
            DataSet dsLedgers = new DataSet("HeadOfficeLedgers");
            try
            {
                resultArgs = GetHeadOfficeLedgerGroup();
                if (resultArgs != null && resultArgs.Success)
                {
                    dsLedgers.Tables.Add(resultArgs.DataSource.Table);
                    resultArgs = GetHeadOfficeLedgers();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dsLedgers.Tables.Add(resultArgs.DataSource.Table);
                    }
                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Data = dsLedgers;
                }

            }
            return resultArgs;
        }

        private ResultArgs GetHeadOfficeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAll, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                DataView dvLedgerGroup = new DataView(resultArgs.DataSource.Table);
                DataTable dtLedgerGroup = dvLedgerGroup.ToTable("Ledgers", false,
                        "Name", "Group");
                resultArgs.DataSource.Data = dtLedgerGroup;

            }
            return resultArgs;
        }
        private ResultArgs GetHeadOfficeLedgerGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchHeadOfficeLedgerGroup, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                DataView dvLedgerGroup = new DataView(resultArgs.DataSource.Table);
                DataTable dtLedgerGroup = dvLedgerGroup.ToTable("LedgerGroup", false,
                        "Ledger Group", "Parent Group");
                resultArgs.DataSource.Data = dtLedgerGroup;
            }
            return resultArgs;
        }

        public ResultArgs SendEmail()
        {
            string colEmail = "Email";
            string colMessage = "Message";
            DataTable dtMailStatus = new DataTable("MailDeliveryStatus");
            dtMailStatus.Columns.Add(colEmail, typeof(string));
            dtMailStatus.Columns.Add(colMessage, typeof(string));

            string Name = "User";

            string Header = "Mail From acme.erp portal";

            string MainContent = Content.Trim();

            string content = Common.GetMailTemplate(Header, MainContent, Name, false);
            string[] MailIds = BranchEmailIds.Split(',');
            if (MailIds.Count() > 0)
            {
                foreach (string mailId in MailIds)
                {
                    resultArgs = Common.SendEmail(mailId.Trim(), "", Subject, content, true);
                    if (!resultArgs.Success)
                    {
                        DataRow drRow = dtMailStatus.NewRow();
                        drRow[colEmail] = mailId.Trim();
                        drRow[colMessage] = string.IsNullOrEmpty(resultArgs.Message) ? MessageCatalog.MobileService.FailedMail : resultArgs.Message;
                        dtMailStatus.Rows.Add(drRow);
                        dtMailStatus.AcceptChanges();
                    }
                }
                if (dtMailStatus != null && dtMailStatus.Rows.Count > 0)
                {
                    resultArgs.DataSource.Data = dtMailStatus;
                    resultArgs.Success = false;
                }
            }

            return resultArgs;
        }
        #endregion
    }
}

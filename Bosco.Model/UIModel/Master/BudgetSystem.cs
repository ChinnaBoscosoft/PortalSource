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

    public class BudgetSystem : SystemBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        public string BranchOfficeCode { get; set; }
        #endregion

        public BudgetSystem()
        {

        }

        public BudgetSystem(int Ledger_ID, int Budget_id)
        {
            FillFundAllotingProperties(Ledger_ID, Budget_id);
            //  GetLedger(Ledger_ID);
        }
        //public BudgetSystem(int BudgetId, string BudgetTypeId, DataBaseType DBMode)
        //{
        //    this.BudgetTypeId = NumberSet.ToInteger(BudgetTypeId);
        //    FillBudetDetails(BudgetId);
        //}
        public BudgetSystem(int BudgetId, DataBaseType DBMode)
        {
            // this.BudgetTypeId = NumberSet.ToInteger(BudgetTypeId);
            if (!IS_SDBINM_CONGREGATION)
            {
                FillBudetDetails(BudgetId);
            }
            else
            {
                SDBFillBudetDetails(BudgetId);
            }
        }

        public int BranchId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectIds { get; set; }
        public int BudgetId { get; set; }
        public string BranchName { get; set; }
        public int Month1BudgetId { get; set; }
        public int Month2BudgetId { get; set; }
        public bool IsTwoMonthBudget { get; set; }
        public int SubLedgerId { get; set; }
        public bool BudgetAction { get; set; }
        public int BudgetActionValue { get; set; }
        public int PreviousBudgetId { get; set; }
        public decimal Percentage { get; set; }
        public string BudgetName { get; set; }
        public string MultipleProjectId { get; set; }
        public string Project { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int BudgetTypeId { get; set; }
        public int BudgetMonth { get; set; }
        public string BudgetTransMode { get; set; }
        public int BudgetLevelId { get; set; }
        public int monthwiseDistribution { get; set; }
        public string Remarks { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
        public string TransMode { get; set; }
        public int Status { get; set; }
        public bool isActive { get; set; }
        public DateTime VoucherDate { get; set; }
        public decimal Month1 { get; set; }
        public decimal Month2 { get; set; }
        public decimal Month3 { get; set; }
        public decimal Month4 { get; set; }
        public decimal Month5 { get; set; }
        public decimal Month6 { get; set; }
        public decimal Month7 { get; set; }
        public decimal Month8 { get; set; }
        public decimal Month9 { get; set; }
        public decimal Month10 { get; set; }
        public decimal Month11 { get; set; }
        public decimal Month12 { get; set; }
        public DataTable dtBudgetLedgers { get; set; }
        public DataTable dtBudgetSubLedgers { get; set; }
        public DataTable dtBudgetStatisticsDetails { get; set; }
        public DataTable dtBudgetCostCentre { get; set; }
        public DataSet dsBudgetCostCentreTables { get; set; }
        public int CostCentreSequenceNo { get; set; }
        public string CostCenterTable { get; set; }
        public DataTable dtBudgetDate { get; set; }
        public string FileName { get; set; }

        /// <summary>
        /// On 28/06/2018, This property is used to skip projects which is closed on or equal to this date
        /// </summary>
        public string ProjectClosedDate { get; set; }

        public DataTable FillBudetDetails(int BudgetId)
        {
            this.BudgetId = BudgetId;
            resultArgs = FetchBudgetDetailedInformation(BudgetId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BudgetName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                BudgetTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());
                BudgetLevelId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_LEVEL_IDColumn.ColumnName].ToString());
                MultipleProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString();
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString());
                DateFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString();
                DateTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString();
                Remarks = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.REMARKSColumn.ColumnName].ToString();
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.IS_ACTIVEColumn.ColumnName].ToString());
                BudgetActionValue = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                BranchName = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_NAME"].ToString();
                Project = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();
            }
            return resultArgs.DataSource.Table;
        }

        private ResultArgs FetchBudgetDetailedInformation(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchById, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public DataTable SDBFillBudetDetails(int BudgetId)
        {
            this.BudgetId = BudgetId;
            resultArgs = SDBFetchBudgetDetailedInformation(BudgetId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BudgetName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                BudgetTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());
                BudgetLevelId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_LEVEL_IDColumn.ColumnName].ToString());
                MultipleProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString();
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString());
                DateFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString();
                DateTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString();
                Remarks = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.REMARKSColumn.ColumnName].ToString();
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.IS_ACTIVEColumn.ColumnName].ToString());
                BudgetActionValue = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_ACTIONColumn.ColumnName].ToString());
                BranchName = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_NAME"].ToString();
                Project = resultArgs.DataSource.Table.Rows[0]["PROJECT"].ToString();
                FileName = resultArgs.DataSource.Table.Rows[0]["FILE_NAME"].ToString();
            }
            return resultArgs.DataSource.Table;
        }

        private ResultArgs SDBFetchBudgetDetailedInformation(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.SDBFetchById, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDetailsByStatistics(int BudgetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchByStatisticId))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchAll))
            {
                if (!ProjectId.Equals(0))
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                if (DateFrom != null && DateFrom != string.Empty)
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchBudgetView()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudget, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAnnualBudgetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetFetch))
            {
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, YearTo);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, new DateTime(DateSet.ToDate(YearFrom, false).Year, 1, 1));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, new DateTime(DateSet.ToDate(YearTo, false).Year, 12, 31));

                if (base.IsHeadOfficeUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerForNewBudget()
        {
            //using (DataManager dataManager = new DataManager(SQLCommand.Budget.AddNewBudgetFetchLedger))
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLoad))
            {
                if (PreviousBudgetId == 0)
                    PreviousBudgetId = BudgetId;  //For Fetching Previous Budget Projected and Actual Amount
                if (BudgetId == 0)
                    BudgetId = PreviousBudgetId;

                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRecentBudgetList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchRecentBudgetList))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckBudgetByDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckBudgetByDate))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveBudgetProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchbyBudgetProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// Others
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="IsAnnualBudget"></param>
        /// <returns></returns>
        private ResultArgs SaveBudgetMasterDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.Update, DataBaseType.HeadOffice))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_ACTIONColumn, BudgetActionValue);
                dataManager.Parameters.Add(this.AppSchema.Budget.IS_ACTIVEColumn, Status);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// INM Details
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <param name="IsAnnualBudget"></param>
        /// <returns></returns>
        private ResultArgs SaveOnlineBudgetMasterDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((BudgetId == 0) ? SQLCommand.Budget.AddAnnual : SQLCommand.Budget.UpdateOnline, DataBaseType.HeadOffice))
            {
                dataManager.Database = dataManagers.Database;

                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId, true);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_NAMEColumn, BudgetName);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_LEVEL_IDColumn, BudgetLevelId);
                dataManager.Parameters.Add(this.AppSchema.Budget.IS_MONTH_WISEColumn, monthwiseDistribution);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);

                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_ACTIONColumn, BudgetActionValue);
                dataManager.Parameters.Add(this.AppSchema.Budget.REMARKSColumn, Remarks);
                dataManager.Parameters.Add(this.AppSchema.Budget.IS_ACTIVEColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.Budget.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.Budget.FILE_NAMEColumn, FileName);
                // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveFund()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveAllotingFundDetails(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveAllotingFundDetails(DataManager dataManagers)
        {
            int ledgerExistID = GetLedger();
            using (DataManager dataManager = new DataManager((ledgerExistID == 0) ? SQLCommand.Budget.AddAllotFund : SQLCommand.Budget.UpdateAllotFund))
            {
                dataManager.Database = dataManagers.Database;
                if (BudgetId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, BudgetId);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH1Column, Month1);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH2Column, Month2);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH3Column, Month3);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH4Column, Month4);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH5Column, Month5);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH6Column, Month6);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH7Column, Month7);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH8Column, Month8);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH9Column, Month9);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH10Column, Month10);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH11Column, Month11);
                    dataManager.Parameters.Add(this.AppSchema.AllotFund.MONTH12Column, Month12);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public ResultArgs GetBindDataRandomSource()
        {
            // using (DataManager dataManager = new DataManager(SQLCommand.Budget.GetRandomMonth))
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLoad))
            {
                if (PreviousBudgetId == 0)
                    PreviousBudgetId = BudgetId;  //For Fetching Previous Budget Projected and Actual Amount
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ImportBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.ImportBudget))
            {
                dataManager.Parameters.Add(AppSchema.Budget.PERCENTAGEColumn, Percentage);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, this.DateSet.ToDateTime(DateTo, Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillFundAllotingProperties(int Ledger_id, int Budget_id)
        {
            resultArgs = FetchBudgetAllotFund(Ledger_id, Budget_id);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Month1 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH1Column.ColumnName].ToString());
                Month2 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH2Column.ColumnName].ToString());
                Month3 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH3Column.ColumnName].ToString());
                Month4 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH4Column.ColumnName].ToString());
                Month5 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH5Column.ColumnName].ToString());
                Month6 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH6Column.ColumnName].ToString());
                Month7 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH7Column.ColumnName].ToString());
                Month8 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH8Column.ColumnName].ToString());
                Month9 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH9Column.ColumnName].ToString());
                Month10 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH10Column.ColumnName].ToString());
                Month11 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH11Column.ColumnName].ToString());
                Month12 = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AllotFund.MONTH12Column.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetAllotFund(int ledger_Id, int Budget_id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchAllotFund))
            {
                dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, ledger_Id);
                dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, Budget_id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.GetLedgerExist))
            {
                dataManager.Parameters.Add(this.AppSchema.AllotFund.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.AllotFund.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs RemoveBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteBudgetCostCenterDetails(dataManager);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteBudgetLedgerDetails(dataManager);
                    if (resultArgs.Success)
                        resultArgs = DeleteBudgetProjectDetails(dataManager);
                    if (resultArgs.Success)
                        resultArgs = DeleteBudgetStatisticsDetails(dataManager);
                    if (resultArgs.Success)
                        resultArgs = DeleteAllotFundDetails(dataManager);
                    if (resultArgs.Success)
                        resultArgs = DeleteBudgetMasterDetails(dataManager);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public int CheckForBudgetEntry()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckForBudgetEntry))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs DeleteAllotFundDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.DeleteAllotFund, DataBaseType.HeadOffice))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetMasterDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.Delete, DataBaseType.HeadOffice))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update the Approved Amount based on the Number (Ledger and sub Ledger)
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs SaveBudgetLedgers(DataManager dataManagers, bool nextmonth = false)
        {
            if (dtBudgetLedgers != null)
            {
                foreach (DataRow drItem in dtBudgetLedgers.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerUpdate, DataBaseType.HeadOffice))
                    {
                        dataManager.Database = dataManagers.Database;
                        if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                        {
                            dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                            dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TRANS_MODEColumn, drItem[this.AppSchema.Budget.BUDGET_TRANS_MODEColumn.ColumnName].ToString());

                            if (nextmonth)
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["M2_APPROVED_AMOUNT"].ToString()));
                            }
                            else
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                            }
                            dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem["HO_NARRATION"].ToString());

                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                    if (!resultArgs.Success) { break; }
                }
                if (resultArgs.Success && dtBudgetSubLedgers != null && dtBudgetSubLedgers.Rows.Count > 0)
                {
                    if (dtBudgetSubLedgers != null && dtBudgetSubLedgers.Rows.Count > 0)
                    {
                        foreach (DataRow drItem in dtBudgetSubLedgers.Rows)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetSubLedgerUpdate, DataBaseType.HeadOffice))
                            {
                                dataManager.Database = dataManagers.Database;
                                if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManager.Parameters.Add(this.AppSchema.Budget.SUB_LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.SUB_LEDGER_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                    if (nextmonth)
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["M2_APPROVED_AMOUNT"].ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                    }
                                    dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem["HO_NARRATION"].ToString());

                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                            if (!resultArgs.Success) { break; }
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Update the Budget Ledgers)
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        private ResultArgs SaveOnlineBudgetLedgers(DataManager dataManagers)
        {
            resultArgs = DeleteBudgetLedgerDetails(dataManagers);
            if (resultArgs.Success)
            {
                resultArgs = DeleteBudgetProjectDetails(dataManagers);
                if (resultArgs.Success)
                {
                    if (dtBudgetLedgers != null)
                    {
                        foreach (DataRow drItem in dtBudgetLedgers.Rows)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetOnlineLedgerUpdate, DataBaseType.HeadOffice))
                            {
                                //dataManager.Database = dataManagers.Database;
                                //if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                //{
                                //    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                //    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                //    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TRANS_MODEColumn, drItem[this.AppSchema.Budget.BUDGET_TRANS_MODEColumn.ColumnName].ToString());
                                //    dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                //    dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem["HO_NARRATION"].ToString());
                                //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                //    resultArgs = dataManager.UpdateData();
                                //}

                                dataManager.Database = dataManagers.Database;
                                if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["PROPOSED_CURRENT_YR"].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, drItem["BUDGET_TRANS_MODE"].ToString());
                                    //dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, drItem[AppSchema.Budget.NARRATIONColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.Budget.HO_NARRATIONColumn, drItem["HO_NARRATION"].ToString());
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                            if (!resultArgs.Success) { break; }
                        }
                        if (resultArgs.Success)
                        {
                            string[] projects = MultipleProjectId.Split(',');
                            foreach (string project in projects)
                            {
                                using (DataManager dataManger = new DataManager(SQLCommand.Budget.AnnualBudgetProjectAdd))
                                {
                                    dataManger.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManger.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, project);
                                    dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManger.UpdateData();
                                }
                                if (!resultArgs.Success) { break; }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        // SaveAnnualBudgetLedger
        //private ResultArgs DeleteBudgetLedgerDetails(DataManager dataManagers)
        //{
        //    using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetLedgerById))
        //    {
        //        datamanager.Database = dataManagers.Database;
        //        datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
        //        resultArgs = datamanager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        private ResultArgs SaveBudgetLedgerDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, TransMode);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetLedgerDetails(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetLedgerById, DataBaseType.HeadOffice))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetProjectDetails(DataManager dataManager)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetProjectById, DataBaseType.HeadOffice))
            {
                datamanager.Database = datamanager.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveBudgetStatisticsDetails(DataManager dataManager)
        {
            if (dtBudgetStatisticsDetails != null && dtBudgetStatisticsDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBudgetStatisticsDetails.Rows)
                {
                    using (DataManager data = new DataManager(SQLCommand.Budget.AddStatisticDetails))
                    {
                        data.Database = dataManager.Database;
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.BUDGET_IDColumn, BudgetId);
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn, dr[AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn.ColumnName].ToString());
                        data.Parameters.Add(this.AppSchema.BudgetStatistics.TOTAL_COUNTColumn, dr[AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName].ToString());
                        resultArgs = data.UpdateData();
                    }
                    if (!resultArgs.Success) { break; }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetStatisticsDetails(DataManager dataManager)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetStatisticsDetails, DataBaseType.HeadOffice))
            {
                datamanager.Database = dataManager.Database;
                datamanager.Parameters.Add(this.AppSchema.BudgetStatistics.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs ChangeStatusToInActive()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.ChangeStatusToInActive))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs ChangeRecentBudgetStatusToActive()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerUpdate))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
            }
            return resultArgs;
        }

        private ResultArgs AllotFund(DataManager dataManagers)
        {
            if (dtBudgetLedgers != null && dtBudgetLedgers.Rows.Count > 0)
            {
                DataTable LedgerGroup = dtBudgetLedgers.AsEnumerable().GroupBy(r => r.Field<UInt32?>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
                foreach (DataRow dr in LedgerGroup.Rows)
                {
                    if (!NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()).Equals(0))
                    {
                        LedgerId = NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                        DataView dvFilter = new DataView(dtBudgetLedgers);
                        dvFilter.RowFilter = String.Format("LEDGER_ID={0}", NumberSet.ToInteger(dr[AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                        foreach (DataRow drFilter in dvFilter.ToTable().Rows)
                        {
                            switch (NumberSet.ToInteger(drFilter["MONTH"].ToString()))
                            {
                                case 1:
                                    Month1 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 2:
                                    Month2 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 3:
                                    Month3 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 4:
                                    Month4 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 5:
                                    Month5 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 6:
                                    Month6 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 7:
                                    Month7 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 8:
                                    Month8 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 9:
                                    Month9 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 10:
                                    Month10 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 11:
                                    Month11 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                                case 12:
                                    Month12 = NumberSet.ToDecimal(drFilter[AppSchema.Budget.AMOUNTColumn.ColumnName].ToString());
                                    break;
                            }
                        }
                        if (dvFilter.ToTable().Rows.Count > 0)
                            resultArgs = SaveAllotingFundDetails(dataManagers);
                    }
                }
            }
            return resultArgs;
        }

        public int CheckStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CheckStatus, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, MultipleProjectId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false));

                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(YearFrom, false));
                // dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(YearTo, false));

                if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return NumberSet.ToInteger(resultArgs.DataSource.Sclar.ToString);
        }

        /// <summary>
        /// chinna 10.01.2019
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                if (BudgetAction)
                {
                    dataManager.BeginTransaction();
                    resultArgs = SaveBudgetMasterDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        resultArgs = SaveBudgetLedgers(dataManager, false);
                    }

                    //For Next Month 
                    if (resultArgs.Success && IsTwoMonthBudget && Month2BudgetId > 0)
                    {
                        BudgetId = Month2BudgetId;
                        resultArgs = SaveBudgetMasterDetails(dataManager);
                        if (resultArgs.Success)
                        {
                            resultArgs = SaveBudgetLedgers(dataManager, true);
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// chinna 11-03-2024
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveINMBudgetDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                if (BudgetAction)
                {
                    dataManager.BeginTransaction();
                    resultArgs = SaveOnlineBudgetMasterDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                        resultArgs = SaveOnlineBudgetLedgers(dataManager);
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }


        public DataTable FetchBudgetsByProjects(string ProjectId)
        {
            ResultArgs resultargs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetByProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, YearTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs.DataSource.Table;
        }

        #region Annual Budget
        public ResultArgs GetAnnualBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetProject))
            {
                //dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);

                //int projectids = this.NumberSet.ToInteger(MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(YearFrom, false));
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(YearTo, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, DateSet.ToDate(DateTo, false));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetCalenderYearBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.CalendarYearBudget))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, DateSet.ToDate(DateFrom, false));
                dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, DateSet.ToDate(DateTo, false));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetAdd()
        {
            DateTime fromdate = DateSet.ToDate(DateFrom, false);
            DateTime todate = DateSet.ToDate(DateTo, false);
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetAddEditDetails))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                if (BudgetTypeId == (int)BudgetType.BudgetMonth)
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddMonths(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddMonths(-1));
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddYears(-1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddYears(-1));
                }
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            //using (DataManager dataManager = new DataManager(SQLCommand.Budget.AnnualBudgetFetchAdd))
            //{
            //    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
            //    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, fromdate.AddYears(-1));
            //    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, todate.AddYears(-1));
            //    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, this.BudgetTypeId);
            //    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DataSource.DataTable);
            //}
            return resultArgs;
        }

        public ResultArgs FetchLastMonthBudget()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchLastBudgetMonth))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, MultipleProjectId.Equals(string.Empty) ? "0" : MultipleProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchBudgetProjectsLookup()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetProjectforLookup))
        //    {
        //        if (this.NumberSet.ToInteger(this.LoginUserId) != (int)UserRights.Admin)
        //        {
        //            dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
        //        }

        //        if (!string.IsNullOrEmpty(ProjectClosedDate))
        //        {
        //            dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
        //        }

        //        dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false));
        //        dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false));
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchBudgetProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchProjectforBudget))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetEdit()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetAddEditDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BRANCH_IDColumn, BranchId);
                //On 22/03/2021
                if (string.IsNullOrEmpty(ProjectIds))
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectIds);
                }

                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddMonths(-1));
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddMonths(-1));
                // 07.03.2023 on Chinna
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddYears(-1));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddYears(-1));
                if (this.HeadOfficeCode == "abeben")
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, "JN");
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, string.Empty);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetDetailsProposals()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetAddEditDetailsProposals, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectIds);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddMonths(-1));
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddMonths(-1));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddYears(-1));
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddYears(-1));

                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, string.Empty);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMysoreBudgetEdit()
        {
            DateTime fromdate = DateSet.ToDate(DateFrom, false);
            DateTime todate = DateSet.ToDate(DateTo, false);

            //Get Previous Budget Id
            DateTime PrevBudgetDateFrom = fromdate.AddMonths(-1);
            //DateTime PrevBudgetDateTo = fromdate.AddMonths(1).AddDays(-1);
            //DateTime PrevBudgetDateTo = todate.AddMonths(-1);
            DateTime PrevBudgetDateTo = PrevBudgetDateFrom.AddMonths(1).AddDays(-1);
            PreviousBudgetId = GetBudgetIdByDateRangeProjectd(PrevBudgetDateFrom, PrevBudgetDateTo, ProjectId.ToString());

            //Month1BudgetId = BudgetId;
            //Month2BudgetId = 0;

            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetMysoreDetails, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Budget.MONTH1_BUDGET_IDColumn, Month1BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.MONTH2_BUDGET_IDColumn, Month2BudgetId);
                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TRANS_MODEColumn, BudgetTransMode);
                //dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateSet.ToDate(DateFrom, false).AddMonths(-1));
                // dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateSet.ToDate(DateTo, false).AddMonths(-1));

                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, PrevBudgetDateFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, PrevBudgetDateTo);
                dataManager.Parameters.Add(AppSchema.Budget.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);
                if (this.HeadOfficeCode == "abeben")
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, "JN");
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, string.Empty);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public Int32 GetBudgetIdByDateRangeProjectd(DateTime budgetfrom, DateTime budgetto, string projectid)
        {
            Int32 rtn = 0;
            ResultArgs resultargs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetIdByDateRangeProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Budget.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, budgetfrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, budgetto);
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                rtn = NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
            }
            return rtn;
        }
        public ResultArgs SaveAnnualBudget()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();

                if (resultArgs != null && resultArgs.Success)
                {
                    BudgetId = BudgetId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BudgetId;
                    resultArgs = SaveAnnualBudgetLedger(dataManager);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveAnnualBudgetLedger(DataManager dataManagers)
        {
            resultArgs = DeleteBudgetLedgerDetails(dataManagers);
            if (resultArgs.Success)
            {
                resultArgs = DeleteBudgetCostCenterDetails(dataManagers);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteBudgetProjectDetails(dataManagers);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteBudgetStatisticsDetails(dataManagers);
                        if (dtBudgetLedgers != null)
                        {
                            foreach (DataRow drItem in dtBudgetLedgers.Rows)
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.Budget.BudgetLedgerUpdate))
                                {
                                    dataManager.Database = dataManagers.Database;
                                    if (drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.PROPOSED_AMOUNTColumn, NumberSet.ToDecimal(drItem["PROPOSED_CURRENT_YR"].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.APPROVED_AMOUNTColumn, NumberSet.ToDecimal(drItem["APPROVED_CURRENT_YR"].ToString()));
                                        dataManager.Parameters.Add(this.AppSchema.Budget.TRANS_MODEColumn, drItem["BUDGET_TRANS_MODE"].ToString());
                                        dataManager.Parameters.Add(this.AppSchema.Budget.NARRATIONColumn, drItem[AppSchema.Budget.NARRATIONColumn.ColumnName].ToString());
                                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dataManager.UpdateData();
                                        if (resultArgs.Success)
                                        {
                                            LedgerId = NumberSet.ToInteger(drItem[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                                            int index = dtBudgetLedgers.Rows.IndexOf(drItem);
                                            CostCenterTable = dtBudgetLedgers.Rows.IndexOf(drItem) + "LDR" + LedgerId;
                                            if (this.HasCostCentre(CostCenterTable))
                                            {
                                                resultArgs = SaveAnnualBudgetCostcentre(dataManager);
                                            }
                                        }
                                    }
                                }
                                if (!resultArgs.Success) { break; }
                            }
                            if (resultArgs.Success)
                            {
                                string[] projects = MultipleProjectId.Split(',');
                                foreach (string project in projects)
                                {
                                    using (DataManager dataManger = new DataManager(SQLCommand.Budget.AnnualBudgetProjectAdd))
                                    {
                                        dataManger.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManger.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, project);
                                        // dataManger.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dataManger.UpdateData();
                                    }
                                    if (!resultArgs.Success) { break; }
                                }
                            }
                            if (resultArgs.Success)
                            {
                                resultArgs = SaveBudgetStatisticsDetails(dataManagers);
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteBudgetDetails(int BudgetId, DataBaseType connecto)
        {
            using (DataManager dataManager = new DataManager())
            {
                this.BudgetId = BudgetId;
                dataManager.BeginTransaction();
                resultArgs = DeleteBudgetLedgerDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetProjectDetails(dataManager);
                if (resultArgs.Success)
                    resultArgs = DeleteBudgetMasterDetails(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveAnnualBudgetCostcentre(DataManager dataManagers)
        {
            if (resultArgs.Success)
            {
                dtBudgetCostCentre = this.GetCostCentreByLedgerID(CostCenterTable).ToTable();
                if (dtBudgetCostCentre != null)
                {
                    foreach (DataRow drItem in dtBudgetCostCentre.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Budget.InsertBudgetCostCentreDetails))
                        {
                            dataManager.Database = dataManagers.Database;
                            if (LedgerId != 0)
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                dataManager.Parameters.Add(this.AppSchema.Budget.LEDGER_IDColumn, LedgerId);
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, NumberSet.ToInteger(drItem[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
                                dataManager.Parameters.Add(this.AppSchema.Budget.AMOUNTColumn, this.NumberSet.ToDecimal(drItem[this.AppSchema.VoucherCostCentre.AMOUNTColumn.ColumnName].ToString()));
                                CostCentreSequenceNo = CostCentreSequenceNo + 1;
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, CostCentreSequenceNo);
                                resultArgs = dataManager.UpdateData();
                            }
                            if (!resultArgs.Success) { break; }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetCostCenterDetails(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetCCdetailsByBudgetId))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GetCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchCostCentreByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public bool isOneTwoMonthBudgetstatus()
        {
            bool rtn = false;
            string BranchCode = FetchBranchOfficeCodeById();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchOneTwoMonthStatus, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchCode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                rtn = resultArgs.DataSource.Sclar.ToInteger == 0 ? false : true;
            }
            return rtn;
        }

        public string FetchBranchOfficeCodeById()
        {
            string BranchOfficeCode = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBranchCodebyBranchId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BRANCH_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    if (BranchId > 0)
                        BranchOfficeCode = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_CODE"].ToString();
            }
            return BranchOfficeCode;
        }


        #endregion
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using DevExpress.XtraSplashScreen;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetViewINM : Bosco.Report.Base.ReportHeaderBase
    {
        public BudgetViewINM()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(this.ReportProperties.BudgetId))
            {
                //ShowBudgetView(); //on 07/07/2020
            }
        }

        #region Show Reports
        public override void ShowReport()
        {
            SetReportTitle();
            ShowBudgetView();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void ShowBudgetView()
        {
            if (LoginUser.IS_CMF_CONGREGATION)
            {

                xrIncomeTitle.WidthF = this.PageWidth - 50;
                xrExpenseTitle.WidthF = this.PageWidth - 50;
            }

            SetTitleWidth(xrIncomeTitle.WidthF);
            this.SetLandscapeBudgetNameWidth = xrIncomeTitle.WidthF;
            this.SetLandscapeHeader = xrIncomeTitle.WidthF;
            this.SetLandscapeFooter = xrIncomeTitle.WidthF;
            this.SetLandscapeFooterDateWidth = xrIncomeTitle.WidthF - 160;
            setHeaderTitleAlignment();
            BindBudgetView();

            //Temp 
            xrCapProposedAmount.Text = "2025-2026";
            xrCapApprovedAmount.Text = "2025-2026";
        }

        private void BindBudgetView()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                setHeaderTitleAlignment();
                SetReportTitle();

                this.HidePageFooter = false;
                string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                    //dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
                }

                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                    DateTime CurrentYrSelectedDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false);
                    DateTime CurrentYrSelectedDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false);
                    Int32 Budget_Type = UtilityMember.NumberSet.ToInteger(dtBudgetInfo.Rows[0]["BUDGET_TYPE_ID"].ToString());
                    Int32 Budget_Action = UtilityMember.NumberSet.ToInteger(dtBudgetInfo.Rows[0]["BUDGET_ACTION"].ToString());
                    //DateTime SelectedDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddYears(-1);
                    //DateTime SelectedDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddYears(-1);

                    DateTime SelectedDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddYears(-1);
                    DateTime SelectedDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddYears(-1);

                    if (this.LoginUser.IS_ABEBEN_DIOCESE)
                    {
                        SelectedDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddMonths(-1);
                        SelectedDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddMonths(-1);
                    }

                    if (LoginUser.IS_CMF_CONGREGATION)
                    {
                        xrExpenseTitle.Font = new Font(xrExpenseTitle.Font.FontFamily, 16);
                        xrIncomeTitle.Font = new Font(xrIncomeTitle.Font.FontFamily, 16);

                        //xrExpenseTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        //xrIncomeTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    }

                    string Projects = dtBudgetInfo.Rows[0]["PROJECT_NAME"].ToString();
                    string ProjectsIds = dtBudgetInfo.Rows[0]["PROJECT"].ToString();  // "116,20"; // test  // dtBudgetInfo.Rows[0]["PROJECT"].ToString();

                    this.HideDateRange = false;
                    this.HideReportSubTitle = true;




                    // this.ReportPeriod = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString()) + " - " + UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString());
                    //Set export file name as budget name
                    //this.DisplayName = "Budgetview - " + ReportProperty.Current.BudgetName;

                    if (LoginUser.IS_CMF_CONGREGATION)
                    {
                        this.ReportProperties.ProjectTitle = Projects;
                        this.HidePageFooter = true;
                        this.HidePageInfo = true;

                        this.ReportTitle = Projects + " - Budget Proposal - " + CurrentYrSelectedDateFrom.Year.ToString();
                        this.ReportSubTitle = Projects;

                        //this.DisplayName += " - " + AppSetting.InstituteName;
                        this.DisplayName = Projects + " - Budget Proposal - " + CurrentYrSelectedDateFrom.Year.ToString(); ;
                        this.ReportProperties.SortByLedger = 1;
                        this.ReportProperties.IncludeAllLedger = 1;
                        this.ReportProperties.ShowPageNumber = 1;
                        this.ReportProperties.ShowProjectsinFooter = 1;
                        //this.ReportProperties.ShowReportDate = 1;
                        this.ReportProperties.ReportDate = this.UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString());
                        this.SetReportDate = this.UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString());
                        // this.ReportProperties.IncludeSignDetails = 1;
                    }
                    else
                    {
                        this.ReportSubTitle = Projects;
                    }

                    //replace special characters which are not valid for file names
                    this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                    this.ReportTitle = !string.IsNullOrEmpty(objReportProperty.BranchOfficeName) ? objReportProperty.BranchOfficeName : string.Empty;  //this.ReportProperties.Current.BudgetName; // ReportProperty.Current.BudgetName;
                    //this.ReportPeriod = String.Format("For the Period: {0} - {1}", CurrentYrSelectedDateFrom.ToShortDateString(), CurrentYrSelectedDateTo.ToShortDateString());
                    this.ReportPeriod = string.Empty;
                    this.ReportBranchName = this.ReportProperties.Current.BudgetName;
                    this.ReportSubTitle = String.Format("Budget Period: {0} - {1}", CurrentYrSelectedDateFrom.ToString("dd/MM/yyyy"), CurrentYrSelectedDateTo.ToString("dd/MM/yyyy"));
                    this.BudgetName = string.Format("Project : {0}", Projects);

                    //this.ReportProperties.DateFrom = SelectedDateFrom.ToShortDateString();
                    //this.ReportProperties.DateTo = SelectedDateFrom.ToShortDateString();


                    //BudgetStatistics budgetstatistics = xrSubBudgetStatistics.ReportSource as BudgetStatistics;
                    //budgetstatistics.HideReportHeader = false;
                    //budgetstatistics.HidePageFooter = false;

                    //budgetstatistics.BindBudgetStatistics(); //on 07/07/2020

                    Int32 PreviousBudgetId = GetPreviousBudgetId();
                    BudgetLedgerINM BudgetIncomeledger = xrSubBudgetIncomeLedgers.ReportSource as BudgetLedgerINM;
                    BudgetIncomeledger.HideReportHeader = false;
                    BudgetIncomeledger.HidePageFooter = false;
                    BudgetIncomeledger.BindBudgetLedgers(TransMode.CR, ProjectsIds, Budget_Type, CurrentYrSelectedDateFrom, CurrentYrSelectedDateTo, SelectedDateFrom, SelectedDateTo, Budget_Action, PreviousBudgetId);

                    BudgetLedgerINM BudgetExpenseledger = xrSubBudgetExpenseLedgers.ReportSource as BudgetLedgerINM;
                    BudgetExpenseledger.HideReportHeader = false;
                    BudgetExpenseledger.HidePageFooter = false;

                    if (this.LoginUser.IS_CMF_CONGREGATION)
                    {
                        BudgetExpenseledger.TotalPrevApprovedBudgetedIncome = BudgetIncomeledger.TotalPrevApprovedBudgetedIncome;
                        BudgetExpenseledger.TotalProposedBudgetedIncome = BudgetIncomeledger.TotalProposedBudgetedIncome;
                        BudgetExpenseledger.TotalApprovedBudgetedIncome = BudgetIncomeledger.TotalApprovedBudgetedIncome;

                        //For 28/01/2022, To assing opening and raw budgeted income
                        BudgetExpenseledger.TotalOpeningBalance = BudgetIncomeledger.TotalOpeningBalance;
                        BudgetExpenseledger.TotalBudgetedRawProposedIncome = BudgetIncomeledger.TotalBudgetedRawProposedIncome;

                        BudgetExpenseledger.TotalBudgetedRawApprovedIncome = BudgetIncomeledger.TotalBudgetedRawApprovedIncome;



                    }
                    BudgetExpenseledger.BindBudgetLedgers(TransMode.DR, ProjectsIds, Budget_Type, CurrentYrSelectedDateFrom, CurrentYrSelectedDateTo, SelectedDateFrom, SelectedDateTo, Budget_Action, PreviousBudgetId);

                    //budgetstatistics.BudgetTypeId = Budget_Type;
                    //budgetstatistics.dtBudgetDateFrom = CurrentYrSelectedDateFrom;
                    //budgetstatistics.dtBudgetDateTo = CurrentYrSelectedDateTo;

                    //budgetstatistics.TotalProposedBudgetedIncome = BudgetIncomeledger.TotalProposedBudgetedIncome;
                    //budgetstatistics.TotalApprovedBudgetedIncome = BudgetIncomeledger.TotalApprovedBudgetedIncome;
                    //budgetstatistics.TotalProposedBudgetedExpense = BudgetExpenseledger.TotalProposedBudgetedExpense;
                    //budgetstatistics.TotalApprovedBudgetedExpense = BudgetExpenseledger.TotalApprovedBudgetedExpense;
                    //budgetstatistics.SetPreviousColumnsWidth = BudgetIncomeledger.GetAmountColumnsWidth - 20;
                    //budgetstatistics.SetCurrentColumnsWidth = BudgetIncomeledger.GetAmountColumnsWidth;
                    //budgetstatistics.SetLedgerNameColumnsWidth = BudgetIncomeledger.GetLedgerNameColumnsWidth;
                    //budgetstatistics.BindBudgetStatistics();



                    double totalProposedBudgetedIncome = BudgetIncomeledger.TotalProposedBudgetedIncome;
                    double totalProposedBudgetedExpense = BudgetExpenseledger.TotalProposedBudgetedExpense;
                    double totalApprovedBudgetedIncome = BudgetIncomeledger.TotalApprovedBudgetedIncome;
                    double totalApprovedBudgetedExpense = BudgetExpenseledger.TotalApprovedBudgetedExpenseOrdinary;

                    // Assign values to the cells
                    xrProposedTotalIncome.Text = totalProposedBudgetedIncome.ToString("C2", new System.Globalization.CultureInfo("en-IN"));
                    xrProposedTotalExpense.Text = totalProposedBudgetedExpense.ToString("C2", new System.Globalization.CultureInfo("en-IN"));
                    xrApprovedTotalIncome.Text = totalApprovedBudgetedIncome.ToString("C2", new System.Globalization.CultureInfo("en-IN"));
                    xrApprovedTotalExpense.Text = totalApprovedBudgetedExpense.ToString("C2", new System.Globalization.CultureInfo("en-IN"));

                    // Calculate differences
                    double proposedDifference = totalProposedBudgetedIncome - totalProposedBudgetedExpense;
                    double approvedDifference = totalApprovedBudgetedIncome - totalApprovedBudgetedExpense;

                    // Set the text for differences
                    xrProposedTotalDifference.Text = proposedDifference.ToString("C2", new System.Globalization.CultureInfo("en-IN"));
                    xrApprovedTotalDifference.Text = approvedDifference.ToString("C2", new System.Globalization.CultureInfo("en-IN"));

                    // Always use this caption for clarity
                    xrTotalDifferenceCaption.Text = "Difference (Deficit / Surplus)";
                    xrCapProposedAmount.Text = UtilityMember.DateSet.ToDate(LoginUser.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(LoginUser.YearTo, false).Year;
                    xrCapApprovedAmount.Text = UtilityMember.DateSet.ToDate(LoginUser.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(LoginUser.YearTo, false).Year;

                }
                else
                {
                    MessageRender.ShowMessage("Invalid Budget");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
        }

        /// <summary>
        ///  This is add by chinna on 02.02.2022 apart from local windows source
        /// </summary>
        /// <returns></returns>
        private Int32 GetPreviousBudgetId()
        {
            Int32 rtn = 0;
            ResultArgs result = new ResultArgs();
            string previousbudgetid = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.PreviousBudgetInfo);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.PreviousBudgetInfo, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BRANCH_IDColumn, this.ReportProperties.BranchOffice);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, previousbudgetid);
            }

            if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
            {
                rtn = UtilityMember.NumberSet.ToInteger(result.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
            }
            return rtn;
        }
        #endregion
    }
}

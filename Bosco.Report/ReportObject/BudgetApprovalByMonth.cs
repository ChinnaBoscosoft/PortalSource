using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;

using DevExpress.XtraSplashScreen;
using System.Globalization;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetApprovalByMonth : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        //using Bosco.Report.View;
        ResultArgs resultArgs = null;
        Int32 PrevBudgetId = 0;
        //DateTime PrevBudgetDateFrom;
        //DateTime PrevBudgetDateTo;
        double TotalPreviousBudgetedAmt = 0;
        double TotalPreviousBudgetDiffBalance = 0;

        double TotalPrevBudgetedAmt = 0;
        double TotalPrevBudgetActual = 0;
        double TotalM1PropsedAmount = 0;
        double TotalM2PropsedAmount = 0;

        double StatementBankBalance = 0;
        double NotMatrilzedAmountInBank = 0;
        double StatementCBBankBalance = 0;

        int RecLedgersSerialNo = 0;
        int RecAlphabetSerialNo = 0;
        private string[] FixedRecAlphabetLedgers = { "Net Amount", "Professional Tax", "LIC" };
        private string[] FixedRecESICMainLedgers = { "Management ESIC", "Employee ESIC" };
        #endregion

        #region Constructor
        public BudgetApprovalByMonth()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            TotalPreviousBudgetedAmt = 0;
            TotalPreviousBudgetDiffBalance = 0;
            BindBudgetExpenseApproval();
        }
        #endregion

        #region Methods
        public void BindBudgetExpenseApproval()
        {
            //ReportProperties.DateFrom = "01-04-2019";
            //ReportProperties.DateTo = "30-04-2019";
            //ReportProperties.ProjectId = "92";
            //ReportProperties.BudgetId = "110";
            //ReportProperties.Budget = "110";
            //ReportProperties.Project = "CHRISTA RAJA CHURCH A/C";

            RecLedgersSerialNo = 0;
            RecAlphabetSerialNo = 0;
            //if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || string.IsNullOrEmpty(this.ReportProperties.Project) ||
            //   (ReportProperties.ReportId == "RPT-171" && this.ReportProperties.Budget.Split(',').Length != 1) || (ReportProperties.ReportId == "RPT-171" && this.ReportProperties.Budget.Split(',').Length != 2))

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || string.IsNullOrEmpty(this.ReportProperties.ProjectId))
            {
                ShowReportFilterDialog();
            }
            else
            {
                FetchBudgetDetails();
                base.ShowReport();
            }
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.BudgetName = objReportProperty.BudgetName;
                //this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                //this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;

                //Assign Previous Budget details
                //PrevBudgetDateFrom = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddMonths(-1);
                //PrevBudgetDateTo = PrevBudgetDateFrom.AddMonths(1).AddDays(-1);
                PrevBudgetId = GetPreviousBudgetId();

                SetReportTitle();
                AssignBudgetDateRangeTitle();
                SetTitleWidth(xrtblHeader.WidthF);

                // commanded - 02.03.2020
                //this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF; 

                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                setHeaderTitleAlignment();

                string budgetmonth = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.FetchMysoreBudget);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.FetchMysoreBudget, DataBaseType.HeadOffice))
                {
                    Int32 month1budgetid = 0;
                    Int32 month2budgetid = 0;
                    string[] monthsbudget = this.ReportProperties.Budget.Split(',');
                    if (monthsbudget.Length == 2)
                    {
                        month1budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                        month2budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString());
                    }
                    else if (monthsbudget.Length == 1)
                    {
                        month1budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                        month2budgetid = 0;
                    }
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.ProjectId);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH1_BUDGET_IDColumn, month1budgetid);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH2_BUDGET_IDColumn, month2budgetid);

                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PREVIOUS_BUDGET_IDColumn, PrevBudgetId);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, this.ReportProperties.PrevDateFrom);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, this.ReportProperties.PrevDateTo);
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);

                    //if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    //{
                    //    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    //}

                    if (ReportProperties.ShowBudgetLedgerActualBalance == "1")
                    {
                        dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetmonth);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtMonthlyBudget = resultArgs.DataSource.Table;
                        AttachRecurringGroup(dtMonthlyBudget);
                        Detail.SortFields.Add(new GroupField("SORT_ID", XRColumnSortOrder.Ascending));
                        this.DataSource = dtMonthlyBudget;
                        this.DataMember = dtMonthlyBudget.TableName;
                    }
                }

                SetReportProperties();
                BindBRSFooterBudgetBalance();

                //grpBSGHeader.GroupFields.Clear();
                //grpBSGHeader.GroupFields.Add(new GroupField("BUDGET_SUB_GROUP_ID"));
                Detail.SortFields.Add(new GroupField("SORT_ID", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("MAIN_LEDGER_NAME"));
                //Detail.SortFields.Add( new GroupField(this.reportSetting1.BUDGET_LEDGER.LEDGER_NAMEColumn.ColumnName));
                Detail.SortFields.Add(new GroupField("SUB_LEDGER_NAME"));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportProperties()
        {
            xrtblHeader1 = AlignHeaderTable(xrtblHeader1);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            //Set Column Caption
            xrcellHeaderPrevBudgetedAmt1.Text = "Budgeted for " + UtilityMember.DateSet.ToDate(ReportProperties.PrevDateFrom, false).ToString("MMM yyyy");
            xrcellHeaderM1Proposal1.Text = "Proposed for " + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToString("MMM yyyy");
            xrcellHeaderM2Proposal1.Text = "Proposed for " + UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).ToString("MMM yyyy");

            xrcellHeaderPrevBudgetedAmt1.Text = "Budgeted for " + ReportProperties.BudgetPrevDateCaption;
            xrcellHeaderM1Proposal1.Text = "Proposed for " + ReportProperties.BudgetM1PropsedDateCaption;
            xrcellHeaderM2Proposal1.Text = "Proposed for " + ReportProperties.BudgetM2PropsedDateCaption;

            string[] monthsbudget = this.ReportProperties.Budget.Split(',');

            //if (ReportProperties.ReportId.Equals("RPT-171"))
            if ((monthsbudget.Length <= 1))
            {
                xrtblHeader1.SuspendLayout();
                if (xrHeaderRow1.Cells.Contains(xrcellHeaderM2Proposal1))
                    xrHeaderRow1.Cells.Remove(xrHeaderRow1.Cells[xrcellHeaderM2Proposal1.Name]);
                xrtblHeader1.PerformLayout();

                xrtblBudget.SuspendLayout();
                if (xrDataRow.Cells.Contains(xrcellM2Proposal))
                    xrDataRow.Cells.Remove(xrDataRow.Cells[xrcellM2Proposal.Name]);
                xrtblBudget.PerformLayout();

                xrtblGrandTotal.SuspendLayout();
                if (xrGrandTotalRow.Cells.Contains(xrCellSumM2PropsedAmt))
                    xrGrandTotalRow.Cells.Remove(xrGrandTotalRow.Cells[xrCellSumM2PropsedAmt.Name]);
                xrtblGrandTotal.PerformLayout();
            }
        }

        private Int32 GetPreviousBudgetId()
        {
            Int32 rtn = 0;
            ResultArgs resultargs = new ResultArgs();
            //string test = this.ReportProperties.PrevDateFrom + "-" + this.ReportProperties.PrevDateTo + "-" + this.ReportProperties.ProjectId + "-" + this.ReportProperties.BranchOffice;
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetIdByDateRangeProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.PrevDateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.PrevDateTo);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.ProjectId);
                dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                //if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                //}
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                rtn = UtilityMember.NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
            }
            return rtn;
        }

        private bool IsNotBudgetedAmount()
        {
            bool Rtn = false;
            double m1proposedamount = 0;
            double m2proposedamount = 0;
            if (GetCurrentColumnValue("M1_PROPOSED_AMOUNT") != null)
            {
                m1proposedamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("M1_PROPOSED_AMOUNT").ToString());
            }

            if (GetCurrentColumnValue("M2_PROPOSED_AMOUNT") != null)
            {
                m2proposedamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("M2_PROPOSED_AMOUNT").ToString());
            }
            Rtn = (m1proposedamount == 0 && m2proposedamount == 0);
            return Rtn;
        }

        private string getAlphabetSerialNo()
        {
            string rtn = string.Empty;

            try
            {
                int pos = (RecAlphabetSerialNo == 1 || RecAlphabetSerialNo == 2) ? RecAlphabetSerialNo + 2 : RecAlphabetSerialNo;
                string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                rtn = alphabetArray.GetValue(pos).ToString();
                RecAlphabetSerialNo++;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn.ToLower();
        }

        private void BindBRSFooterBudgetBalance()
        {
            ResultArgs resultargs = new ResultArgs();
            DataTable dtBRSList = new DataTable();

            double UnRealizedAmt = 0;
            double UnClearedAmt = 0;

            //# Get BRS details still previous budget date to
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRSByMaterialized, DataBaseType.HeadOffice))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.ProjectId);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.PrevDateTo);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);  //this.ReportProperties.BranchOffice);
                }
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                dtBRSList = resultargs.DataSource.Table;

                UnRealizedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnRealised)", "").ToString());
                UnClearedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnCleared)", "").ToString());
            }

            //# GEt CB bank balance for previous budget date to, closing balance
            StatementCBBankBalance = this.GetBalance(this.ReportProperties.ProjectId, this.ReportProperties.PrevDateTo, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance, AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance);

            //# Bank Balance
            StatementBankBalance = 0;
            StatementBankBalance = StatementCBBankBalance - UnRealizedAmt;
            StatementBankBalance += UnClearedAmt;

            NotMatrilzedAmountInBank = 0;
            NotMatrilzedAmountInBank = UnRealizedAmt + UnClearedAmt;

            //xrfooterOP.Text = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt);
            //xrfooterBankBalance.Text = this.UtilityMember.NumberSet.ToNumber(StatementBankBalance);
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(Cashbookbankbalance);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Cashbookbankbalance - Statementbankbalance)).ToString();
            //xrfooterBRSAmt.Text = this.UtilityMember.NumberSet.ToNumber(NotMatrilzedAmountInBank);

            if (this.DataSource != null)
            {
                DataTable dtBind = this.DataSource as DataTable;
                TotalPrevBudgetedAmt = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                TotalPrevBudgetActual = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + ")", string.Empty).ToString());
                TotalM1PropsedAmount = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                TotalM2PropsedAmount = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
            }

            //xrfooterOP.Text = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt);
            //xrfooterTotalBudget.Text = this.UtilityMember.NumberSet.ToNumber(TotalM1PropsedAmount + TotalM2PropsedAmount);
            //xrfooterPreviousBudgetBalance.Text = this.UtilityMember.NumberSet.ToNumber(TotalPrevBudgetedAmt - TotalPrevBudgetActual);
            //xrfooterTotalBalance.Text = this.UtilityMember.NumberSet.ToNumber((TotalM1PropsedAmount + TotalM2PropsedAmount) - (TotalPrevBudgetedAmt - TotalPrevBudgetActual));

            //for temp
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(totalPrevBudgetedAmt - totalPrevBudgetActual);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Statementbankbalance - (totalPrevBudgetedAmt - totalPrevBudgetActual))).ToString();
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(Cashbookclosingbankbalance);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(StatementBankBalance - Cashbookclosingbankbalance)).ToString();


            //if (ReportProperties.ReportId.Equals("RPT-171"))
            if (ReportProperties.IsTwoMonthBudget)
            {
                //  xrfooterTotalBudgetCaption.Text = "Total Budget of  " + UtilityMember.DateSet.ToDate(objReportProperty.Current.DateFrom, "MMM") + " & " + UtilityMember.DateSet.ToDate(objReportProperty.Current.DateTo, "MMM yyyy");
                if (!(string.IsNullOrEmpty(ReportProperties.BudgetM1PropsedDateCaption) && string.IsNullOrEmpty(ReportProperties.BudgetM1PropsedDateCaption)))
                {
                    //xrfooterTotalBudgetCaption.Text = "Total Budget of  " + ReportProperties.BudgetM1PropsedDateCaption.Substring(0, 3) + " & " + ReportProperties.BudgetM2PropsedDateCaption.Substring(0, 3) + " " + ReportProperties.BudgetM2PropsedDateCaption.Substring(3, 0);
                    xrfooterTotalBudgetCaption.Text = "Total Budget of  " + ReportProperties.BudgetM1PropsedDateCaption.Substring(0, 3) + " & " + ReportProperties.BudgetM2PropsedDateCaption.Substring(0, 8);
                }
            }
            else
            {
                xrfooterTotalBudgetCaption.Text = "Total Budget of " + ReportProperties.BudgetM1PropsedDateCaption;
            }
            //xrfooterPreviousBudgetBalanceCaption.Text = "Balance from Previous Month Budget " + UtilityMember.DateSet.ToDate(PrevBudgetDateFrom.ToShortDateString(), "MMM yyyy");
            xrfooterPreviousBudgetBalanceCaption.Text = "Balance from Previous Month Budget " + ReportProperties.BudgetPrevDateCaption;  //+ UtilityMember.DateSet.ToDate(ReportProperties.PrevDateFrom, "MMM yyyy");

            //xrTblBRSbalance.SuspendLayout();
            //xrfooterOP.WidthF = xrfooterBankBalance.WidthF = xrfooterCBbalance.WidthF = xrfooterDifference.WidthF = xrfooterBRS.WidthF = xrfooterBRSAmt.WidthF = xrfooterTotalBudget.WidthF = xrfooterPreviousBudgetBalance.WidthF = xrfooterTotalBalance.WidthF = (xrcellHeaderPrevBudgetedAmt.WidthF + xrcellHeaderPrevBudgetedActual.WidthF);
            //xrTblBRSbalance.PerformLayout();

            xrTblBRSbalance.SuspendLayout();

            xrfooterOPCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterBankBalanceCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterCBbalanceCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterDifferenceCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterBRSCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterEmpty1Caption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterTotalBudgetCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterPreviousBudgetBalanceCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrfooterTotalBalanceCaption.WidthF = xrcellHeaderSNo1.WidthF + xrcellParticular.WidthF;
            xrTblBRSbalance.PerformLayout();

            xrTblBRSbalance.SuspendLayout();
            xrfooterOP.WidthF = xrcellHeaderPrevBudgetedAmt.WidthF;// (xrcellHeaderPrevBudgetedAmt.WidthF + xrcellHeaderPrevBudgetedActual.WidthF);
            xrTblBRSbalance.PerformLayout();

        }

        /// <summary>
        /// Attach Rec Ledgers Group
        /// </summary>
        /// <param name="dtBindData"></param>
        private void AttachRecurringGroup(DataTable dtBindData)
        {
            DataRow dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["BUDGET_GROUP"] = "Recurring Expenses";
            dr["MAIN_LEDGER_NAME"] = "  Salary";
            dr["LEDGER_NAME"] = "  Salary";
            dr["SORT_ID"] = "0";
            dtBindData.Rows.Add(dr);

            dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["BUDGET_GROUP"] = "Recurring Expenses";
            dr["MAIN_LEDGER_NAME"] = "  b. P.F :";
            dr["LEDGER_NAME"] = "  b. P.F :";
            dr["SORT_ID"] = "2";
            dtBindData.Rows.Add(dr);

            dr = dtBindData.NewRow();
            dr["BUDGET_GROUP_ID"] = 1;
            dr["BUDGET_GROUP"] = "Recurring Expenses";
            dr["MAIN_LEDGER_NAME"] = "  c. ESIC :";
            dr["LEDGER_NAME"] = "  c. ESIC :";
            dr["SORT_ID"] = "5";
            dtBindData.Rows.Add(dr);
        }
        #endregion

        #region Events

        private void xrcellPrevBudgetedBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = "0.0";

            if (GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_PROPOSED_AMOUNTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName) != null)
            {
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();

                double prevbudgetedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName).ToString());
                double prevbudgeteactual = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName).ToString());
                double m1Propsedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                double m2Propsedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName).ToString());

                double diff = prevbudgetedamt - prevbudgeteactual;
                TotalPreviousBudgetedAmt += prevbudgetedamt;
                TotalPreviousBudgetDiffBalance += diff;

                if (ledgername.Trim().ToUpper() == "SALARY" || ledgername.Trim().ToUpper() == "B. P.F :" || ledgername.Trim().ToUpper() == "C. ESIC :")
                {
                    cell.Text = string.Empty;
                }
                else
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(diff);
                }
            }

            //if (IsNotBudgetedAmount())
            //{
            //    cell.Text = string.Empty;
            //}
        }

        private void xrcellM2Proposal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrcellM1Proposal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrcellPrevBudgetedActual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrfooterOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt); ;
            e.Handled = true;
        }

        private void xrfooterBankBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(StatementBankBalance); ;
            e.Handled = true;
        }

        private void xrfooterBRSAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(NotMatrilzedAmountInBank); ;
            e.Handled = true;
        }

        private void xrfooterCBbalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(StatementCBBankBalance); ;
            e.Handled = true;
        }

        private void xrfooterDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(Math.Abs(StatementBankBalance - StatementCBBankBalance)).ToString();
            e.Handled = true;
        }

        private void xrfooterPreviousBudgetBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPrevBudgetedAmt - TotalPrevBudgetActual);
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetDiffBalance);
            e.Handled = true;
        }

        private void xrfooterTotalBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber((TotalM1PropsedAmount + TotalM2PropsedAmount) - (TotalPrevBudgetedAmt - TotalPrevBudgetActual));
            e.Handled = true;
        }

        private void xrCellSumPreviousBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetDiffBalance);
            e.Handled = true;
        }

        private void xrfooterTotalBudget_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalM1PropsedAmount + TotalM2PropsedAmount);
            e.Handled = true;
        }

        private void xrcellBudgetSubGrp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string BudgetSubGrpName = string.Empty;
            if (cell != null)
            {
                if (GetCurrentColumnValue("BUDGET_SUB_GROUP_ID") != null)
                {
                    int budgetsupgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                    if (budgetsupgrpid == 1)
                    {
                        BudgetSubGrpName = "Salary";
                    }
                    else if (budgetsupgrpid == 2)
                    {
                        BudgetSubGrpName = " b. PF : ";
                    }
                    else if (budgetsupgrpid == 3)
                    {
                        BudgetSubGrpName = " c. ESIC : ";
                    }
                }

                cell.Text = BudgetSubGrpName;
            }
        }

        private void xrcellSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                Int32 subledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("SUB_LEDGER_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();
                bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;
                bool ESICMainLedgersExists = Array.IndexOf(FixedRecESICMainLedgers, ledgername) >= 0;
                if ((budgetgrpid == 1 && budgetsubgrpid > 2 && !AlphaLedgersExists && !ESICMainLedgersExists) || (ledgername.Trim().ToUpper() == "SALARY"))//Skip Salary/PF group
                {
                    RecLedgersSerialNo++;
                    cell.Text = RecLedgersSerialNo.ToString();
                }
                else if (budgetgrpid == 2 && subledgerid == 0)
                {
                    RecLedgersSerialNo++;
                    cell.Text = RecLedgersSerialNo.ToString();
                }
                else
                {
                    cell.Text = string.Empty;
                }
            }
        }

        private void xrcellParticular_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();

                bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;

                //if (AlphaLedgersExists)
                //{
                //    cell.Text = getAlphabetSerialNo() + ". " + ledgername.Trim();
                //}
                cell.Font = new Font(cell.Font, FontStyle.Regular);
                if (ledgername.Trim().ToUpper() == "SALARY" || ledgername.Trim().ToUpper() == "B. P.F :" || ledgername.Trim().ToUpper() == "C. ESIC :")
                {
                    cell.Text = ledgername.Trim();
                    cell.Font = new Font(cell.Font, FontStyle.Bold);
                }
                else if (budgetgrpid == 2 && budgetsubgrpid == 1)
                {
                    cell.Text = "&nbsp;&nbsp;" + ledgername;
                }
                else
                {
                    if (AlphaLedgersExists)
                    {
                        cell.Text = getAlphabetSerialNo() + ". " + ledgername;
                    }
                }
            }
        }

        private void grpBSGHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();
                e.Cancel = (budgetgrpid == 2);
            }
        }

        private void xrcellBudgetSubSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();

                if (budgetgrpid == 1 && budgetsubgrpid == 1) // for Rec, Salary group
                {
                    cell.Text = "1";
                    RecLedgersSerialNo++;
                }
                else
                {
                    cell.Text = "";
                }
            }
        }

        private void grpBGFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                Int32 budgetgroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                xrPageBreak.Visible = true;
                if (budgetgroupid == 2)
                {
                    xrPageBreak.Visible = false;
                }
            }
        }

        private void xrcellPrevBudgetedAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }
        #endregion

    }
}

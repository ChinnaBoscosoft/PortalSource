using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using System.Web;
using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateActivityIE : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        public DataTable dtSource { get; set; }
        double TotalNetProfitIncome = 0;
        double groupDebitSum = 0;
        double groupCreditSum = 0;
        double TotalNetProfitExpense = 0;

        double A2ExpenseSubTotalIncome, A2ExpenseSubTotalExpense = 0;
        double A2ExpenseSubTotalIncome1, A2ExpenseSubTotalExpense1 = 0;
        double A2ExpenseSubTotalIncome2, A2ExpenseSubTotalExpense2 = 0;
        double E_TotalAmount = 0;

        DataTable dtGSTLedgerSplitation = null;

        //For Time begin------------
        Int32 CGST = 2244; //"Central Goods & Service Tax (CGST)";
        Int32 SGST = 2245; //"State Goods & Service Tax (SGST)";
        Int32 IGST = 2246; //Integrated Good & Service Tax (IGST);   

        string CGST_Name = "Central Goods & Service Tax (CGST)";
        string SGST_Name = "State Goods & Service Tax (SGST)";
        string IGST_Name = "Integrated Good & Service Tax (IGST)";
        //--------------------------

        #endregion

        #region Constructor
        public GeneralateActivityIE()
        {
            this.SetLandscapeHeader = 723.25f;
            this.SetLandscapeFooter = 723.25f;
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 14/01/2020, for FDCCSI, to keep Management: NET Total
        /// </summary>
        public double NetTotalManagementActivity
        {
            get
            {
                double rtn = 0;
                if (HttpContext.Current.Session["NetTotalManagementActivity"] != null)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(HttpContext.Current.Session["NetTotalManagementActivity"].ToString());
                }
                return rtn;
            }
            set
            {
                HttpContext.Current.Session["NetTotalManagementActivity"] = value;
            }
        }


        #region ShowReports
        public override void ShowReport()
        {
            groupDebitSum = 0;
            groupCreditSum = 0;
            TotalNetProfitIncome = 0;
            TotalNetProfitExpense = 0;
            E_TotalAmount = 0;
            NetTotalManagementActivity = 0;

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindSource();
            }


            base.ShowReport();
        }


        private void xrTableExpenseLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ExpenseLedgerAmt = this.ReportProperties.NumberSet.ToDouble(xrTableExpenseLedgerName.Text);
            if (ExpenseLedgerAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableExpenseLedgerName.Text = "";
            }

            // For A2ExpenseSubTotal -------------------------------------------------
            bool isA2ExpenseSubTotal = (xrTableCongGrpSub.Text.ToLower().ToUpper() == "A2 EXPENDITURE");
            if (isA2ExpenseSubTotal)
            {
                if (xrTableCode.Text.Trim() == "2.1" || xrTableCode.Text.Trim() == "2.2" || xrTableCode.Text.Trim() == "2.3" ||
                    xrTableCode.Text.Trim() == "2.4" || xrTableCode.Text.Trim() == "2.5" || xrTableCode.Text.Trim() == "2.6")
                {
                    A2ExpenseSubTotalExpense += ExpenseLedgerAmt;
                }
                else if (xrTableCode.Text.Trim() == "2.7" || xrTableCode.Text.Trim() == "2.8" || xrTableCode.Text.Trim() == "2.9" ||
                    xrTableCode.Text.Trim() == "2.10" || xrTableCode.Text.Trim() == "2.11" || xrTableCode.Text.Trim() == "2.12" ||
                    xrTableCode.Text.Trim() == "2.13" || xrTableCode.Text.Trim() == "2.14" || xrTableCode.Text.Trim() == "2.15" ||
                    xrTableCode.Text.Trim() == "2.16" || xrTableCode.Text.Trim() == "2.17" || xrTableCode.Text.Trim() == "2.18" ||
                    xrTableCode.Text.Trim() == "2.19" || xrTableCode.Text.Trim() == "2.20" ||
                    xrTableCode.Text.Trim() == "2.20W" || xrTableCode.Text.Trim() == "2.20X" || xrTableCode.Text.Trim() == "2.20Y" ||
                    xrTableCode.Text.Trim() == "2.21")
                {
                    A2ExpenseSubTotalExpense1 += ExpenseLedgerAmt;
                }
                else if (xrTableCode.Text.Trim() == "2.22" || xrTableCode.Text.Trim() == "2.23" || xrTableCode.Text.Trim() == "2.24" ||
                    xrTableCode.Text.Trim() == "2.25" || xrTableCode.Text.Trim() == "2.26")
                {
                    A2ExpenseSubTotalExpense2 += ExpenseLedgerAmt;
                }
                else
                {
                    A2ExpenseSubTotalExpense = A2ExpenseSubTotalExpense1 = A2ExpenseSubTotalExpense2 = 0;
                }
            }
            //--------------------------------------------------------------------------

        }

        private void xrTableIncomeLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double IncomeLedgerAmt = this.ReportProperties.NumberSet.ToDouble(xrTableIncomeLedgerName.Text);
            if (IncomeLedgerAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableIncomeLedgerName.Text = "";
            }

            // For A2ExpenseSubTotal -------------------------------------------------
            bool isA2ExpenseSubTotal = (xrTableCongGrpSub.Text.ToLower().ToUpper() == "A2 EXPENDITURE");
            if (isA2ExpenseSubTotal)
            {
                if (xrTableCode.Text.Trim() == "2.1" || xrTableCode.Text.Trim() == "2.2" || xrTableCode.Text.Trim() == "2.3" ||
                    xrTableCode.Text.Trim() == "2.4" || xrTableCode.Text.Trim() == "2.5" || xrTableCode.Text.Trim() == "2.6")
                {
                    A2ExpenseSubTotalIncome += IncomeLedgerAmt;
                }
                else if (xrTableCode.Text.Trim() == "2.7" || xrTableCode.Text.Trim() == "2.8" || xrTableCode.Text.Trim() == "2.9" ||
                    xrTableCode.Text.Trim() == "2.10" || xrTableCode.Text.Trim() == "2.11" || xrTableCode.Text.Trim() == "2.12" ||
                    xrTableCode.Text.Trim() == "2.13" || xrTableCode.Text.Trim() == "2.14" || xrTableCode.Text.Trim() == "2.15" ||
                    xrTableCode.Text.Trim() == "2.16" || xrTableCode.Text.Trim() == "2.17" || xrTableCode.Text.Trim() == "2.18" ||
                    xrTableCode.Text.Trim() == "2.19" || xrTableCode.Text.Trim() == "2.20" ||
                    xrTableCode.Text.Trim() == "2.20W" || xrTableCode.Text.Trim() == "2.20X" || xrTableCode.Text.Trim() == "2.20Y" ||
                    xrTableCode.Text.Trim() == "2.21")
                {
                    A2ExpenseSubTotalIncome1 += IncomeLedgerAmt;
                }
                else if (xrTableCode.Text.Trim() == "2.22" || xrTableCode.Text.Trim() == "2.23" || xrTableCode.Text.Trim() == "2.24" ||
                    xrTableCode.Text.Trim() == "2.25" || xrTableCode.Text.Trim() == "2.26")
                {
                    A2ExpenseSubTotalIncome2 += IncomeLedgerAmt;
                }
                else
                {
                    A2ExpenseSubTotalIncome = A2ExpenseSubTotalIncome1 = A2ExpenseSubTotalIncome2 = 0;
                }
            }
            //--------------------------------------------------------------------------
        }

        private void xrTableFooterCongGroMain_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("MASTER_NAME") == null) ? string.Empty : "Total " + GetCurrentColumnValue("MASTER_NAME").ToString();
            xrTableFooterCongGroMain.Text = Narration;

            if (xrTableFooterCongGroMain.Text.Equals("Total A1 Income - Institutional Activity"))
            {
                xrTableFooterCongGroMain.Text = "A1  Total Income Institutional Activity";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total A2 Expenditure - Institutional activity"))
            {
                xrTableFooterCongGroMain.Text = "A2  Total Expenses Institutional Activity";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total B1 Income - Fixed Assets"))
            {
                xrTableFooterCongGroMain.Text = "B1  Total Income from Fixed Assets";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total B2 Expenditure - Fixed Assets"))
            {
                xrTableFooterCongGroMain.Text = "B2  Total Expenditure Incurred in respect of Fixed Assets";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total C1 Income - Financial Management"))
            {
                xrTableFooterCongGroMain.Text = "C1  Total Financial Management Income";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total C2 Expenditure - Financial Management"))
            {
                xrTableFooterCongGroMain.Text = "C2  Total Financial Management Expenditure";
            }

            if (xrTableFooterCongGroMain.Text.Equals("Total D1 Income - Extraordinary"))
            {
                xrTableFooterCongGroMain.Text = "D1  Total Extraordinary Income";
            }
            if (xrTableFooterCongGroMain.Text.Equals("Total D2 Expenditure - Extraordinary"))
            {
                xrTableFooterCongGroMain.Text = "D2  Total Extraordinary Expenditure";
            }
        }

        private void xrTableFooterCongGrpSub_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("MASTER_PARENT") == null) ? string.Empty : GetCurrentColumnValue("MASTER_PARENT").ToString();

            xrTableFooterCongGrpSub.Text = Narration;

            if (xrTableFooterCongGrpSub.Text.Equals("A Management of institutional activity (Community)"))
            {
                xrTableFooterCongGrpSub.Text = "Profit/Loss from Institutional Activity (A1-A2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("B From Fixed Assets and Property"))
            {
                xrTableFooterCongGrpSub.Text = "Profit/Loss from the Management of Fixed Assets (B1-B2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("C Financial Management"))
            {
                xrTableFooterCongGrpSub.Text = "Profit/Loss from Financial Management Activities (C1-C2)";
            }
            if (xrTableFooterCongGrpSub.Text.Equals("D Extraordinary Activities"))
            {
                xrTableFooterCongGrpSub.Text = "Profit/Loss from Extraordinary Activities (D1-D2) ";
            }
        }

        #endregion

        #region Methods
        public void BindSource(bool fromMasterReport = false)
        {
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            //this.SetLandscapeFooterDateWidth = 712.25f;

            if (fromMasterReport)
            {
                this.HideReportHeader = this.HidePageHeader = this.HidePageFooter = false;
                this.HideHeaderFully = true;
            }

            if (resultArgs.Success && resultArgs.DataSource != null)
            {
                string conLedgerCode = "E";
                DataTable dtDetails = resultArgs.DataSource.Table;
                dtDetails.DefaultView.RowFilter = "CON_LEDGER_CODE='" + conLedgerCode + "'";
                DataTable dtRows = dtDetails.DefaultView.ToTable();
                E_TotalAmount = UtilityMember.NumberSet.ToDouble(dtRows.Rows[0]["NXTPAYMENT"].ToString());
                xrtblExpense.Text = this.UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtRows.Rows[0]["NXTPAYMENT"].ToString()));
                dtDetails.DefaultView.RowFilter = "";

                dtSource = dtDetails; // resultArgs.DataSource.Table;
                if (dtSource != null)
                {
                    //dtSource.DefaultView.RowFilter = "CON_LEDGER_CODE IN ('A', 'A1','A2','B','B1','B2','C','C1','C2','D','D1','D2','E')"; //Filter concern code for Management Activity;
                    dtSource.DefaultView.RowFilter = "CON_LEDGER_CODE IN ('A', 'A1','A2','B','B1','B2','C','C1','C2','D','D1','D2')"; //Filter concern code for Management Activity;
                    DataTable dtManagementActivity = dtSource.DefaultView.ToTable();

                    dtManagementActivity.TableName = "CongiregationProfitandLoss";
                    this.DataSource = dtManagementActivity;
                    this.DataMember = dtManagementActivity.TableName;
                    dtSource.DefaultView.RowFilter = "";
                }
            }

            xrcellBudgetYearCaption2.Text = "Actual Results " + this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString();
            xrcellBudgetYearCaption3.Text = "Budget " + (this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year + 1).ToString();

            Detail.SortFields.Add(new GroupField("CODE_LENGTH"));//CODE_LENGTH
            Detail.SortFields.Add(new GroupField(reportSetting1.CongiregationProfitandLoss.LEDGER_CODEColumn.ColumnName));

            //For General Annaul reports, hide/remove budget columns
            xrlblYear.Text = (this.ReportProperties.Current.ReportId == "RPT-170" ? "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString() : string.Empty);
            xrlblYear.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            xrTblSignFooter.Visible = (this.ReportProperties.Current.ReportId == "RPT-175");
            xrLblTitle.Text = "Name of the  House : " + ReportProperties.BranchOfficeName;
            lblBudgetTitle.Visible = (this.ReportProperties.Current.ReportId == "RPT-175");
            if (this.ReportProperties.Current.ReportId == "RPT-170")
            {
                xrLblTitle.Text = "Management Activities";
                xrRowBudgetYearCaption.Visible = false;
                xrcellExpenditureCaption.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right;
                xrTableExpenseCongGrpMain.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                xrTableExpenseCongGrpSub.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                //Main Group--------------------------------------------------------------------------------------------------

                //Header
                xrTblHeaderCongGrpMain.SuspendLayout();
                if (xrMainGrpRow.Cells.Contains(xrTableIncomeCongGrpMainBudget1))
                    xrMainGrpRow.Cells.Remove(xrMainGrpRow.Cells[xrTableIncomeCongGrpMainBudget1.Name]);

                if (xrMainGrpRow.Cells.Contains(xrTableIncomeCongGrpMainBudget2))
                    xrMainGrpRow.Cells.Remove(xrMainGrpRow.Cells[xrTableIncomeCongGrpMainBudget2.Name]);

                if (xrMainGrpRow1.Cells.Contains(xrTableIncomeCongGrpMainBudget3))
                    xrMainGrpRow1.Cells.Remove(xrMainGrpRow1.Cells[xrTableIncomeCongGrpMainBudget3.Name]);

                if (xrMainGrpRow1.Cells.Contains(xrTableIncomeCongGrpMainBudget4))
                    xrMainGrpRow1.Cells.Remove(xrMainGrpRow1.Cells[xrTableIncomeCongGrpMainBudget4.Name]);
                xrTblHeaderCongGrpMain.PerformLayout();
                //Footer
                xrTblFooterCongGrpMain.SuspendLayout();
                if (xrRowFooterMainGrp.Cells.Contains(xrTableFooterMainGrpBudget1))
                    xrRowFooterMainGrp.Cells.Remove(xrRowFooterMainGrp.Cells[xrTableFooterMainGrpBudget1.Name]);
                if (xrRowFooterMainGrp.Cells.Contains(xrTableFooterMainGrpBudget2))
                    xrRowFooterMainGrp.Cells.Remove(xrRowFooterMainGrp.Cells[xrTableFooterMainGrpBudget2.Name]);
                xrTblFooterCongGrpMain.PerformLayout();
                //--------------------------------------------------------------------------------------------------------------

                //Sub Group ----------------------------------------------------------------------------------------------------
                //Header 
                xrTblHeaderSubGrp.SuspendLayout();
                if (xrRowSubGrp.Cells.Contains(xrTableIncomeCongGrpSubBudget1))
                    xrRowSubGrp.Cells.Remove(xrRowSubGrp.Cells[xrTableIncomeCongGrpSubBudget1.Name]);
                if (xrRowSubGrp.Cells.Contains(xrTableIncomeCongGrpSubBudget2))
                    xrRowSubGrp.Cells.Remove(xrRowSubGrp.Cells[xrTableIncomeCongGrpSubBudget2.Name]);
                xrTblHeaderSubGrp.PerformLayout();
                //Footer
                xrTblFooterCongGrpSub.SuspendLayout();
                if (xrRowFooterSubGrp.Cells.Contains(xrTableFooterSubGrpBudget1))
                    xrRowFooterSubGrp.Cells.Remove(xrRowFooterSubGrp.Cells[xrTableFooterSubGrpBudget1.Name]);
                if (xrRowFooterSubGrp.Cells.Contains(xrTableFooterSubGrpBudget2))
                    xrRowFooterSubGrp.Cells.Remove(xrRowFooterSubGrp.Cells[xrTableFooterSubGrpBudget2.Name]);
                xrTblFooterCongGrpSub.PerformLayout();
                //--------------------------------------------------------------------------------------------------------------

                //Ledger Name
                xrTblHeaderLedger.SuspendLayout();
                if (xrRowLedgerName.Cells.Contains(xrTableIncomeLedgerNameBudget1))
                    xrRowLedgerName.Cells.Remove(xrRowLedgerName.Cells[xrTableIncomeLedgerNameBudget1.Name]);

                if (xrRowLedgerName.Cells.Contains(xrTableIncomeLedgerNameBudget2))
                    xrRowLedgerName.Cells.Remove(xrRowLedgerName.Cells[xrTableIncomeLedgerNameBudget2.Name]);

                if (xrRowA2ExpenseSubTotal.Cells.Contains(xrTableIncomeLedgerNameBudget3))
                    xrRowA2ExpenseSubTotal.Cells.Remove(xrRowA2ExpenseSubTotal.Cells[xrTableIncomeLedgerNameBudget3.Name]);

                if (xrRowA2ExpenseSubTotal.Cells.Contains(xrTableIncomeLedgerNameBudget4))
                    xrRowA2ExpenseSubTotal.Cells.Remove(xrRowA2ExpenseSubTotal.Cells[xrTableIncomeLedgerNameBudget4.Name]);
                xrTblHeaderLedger.PerformLayout();

                //Report Footer
                xrTblReportFooter.SuspendLayout();
                if (xrRowReportFooter.Cells.Contains(xrcellRptFooterBudget1))
                    xrRowReportFooter.Cells.Remove(xrRowReportFooter.Cells[xrcellRptFooterBudget1.Name]);
                if (xrRowReportFooter.Cells.Contains(xrcellRptFooterBudget2))
                    xrRowReportFooter.Cells.Remove(xrRowReportFooter.Cells[xrcellRptFooterBudget2.Name]);
                xrTblReportFooter.PerformLayout();

                xrTblReportFooter1.SuspendLayout();
                if (xrRowReportFooter1.Cells.Contains(xrcellRptFooter1Budget1))
                    xrRowReportFooter1.Cells.Remove(xrRowReportFooter1.Cells[xrcellRptFooter1Budget1.Name]);
                if (xrRowReportFooter1.Cells.Contains(xrcellRptFooter1Budget2))
                    xrRowReportFooter1.Cells.Remove(xrRowReportFooter1.Cells[xrcellRptFooter1Budget2.Name]);
                xrTblReportFooter1.PerformLayout();

            }
        }

        private ResultArgs GetReportSource()
        {
            //# 31/01/2020, For (Management Activities, Movement of FA and Capital), Get amount only for Community Projects
            string CommunityProjectIds = GetCommunityProjectIds();
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpense);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpense, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, CommunityProjectIds);
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }


            //As on 21/06/2021, To have GST Ledger values will be splited as Income part and Expense Part
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {

                string sqlGSTSplitation = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateActivityGSTLedgerIncomeExpense);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateActivityGSTLedgerIncomeExpense, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, CommunityProjectIds);
                    if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }

                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs result = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlGSTSplitation);
                    if (result.Success)
                    {
                        DataTable dtGSTLedgers = result.DataSource.Table;

                        //For Income Part
                        double amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(INCOME)", "LEDGER_ID=" + CGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A1", "1.10", "Central Goods & Service Tax (CGST)",
                                            "A1 Income - Institutional Activity", "A Management of institutional activity (Community)", amt, 0, amt);

                        amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(INCOME)", "LEDGER_ID=" + SGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A1", "1.10", "State Goods & Service Tax (SGST)",
                                            "A1 Income - Institutional Activity", "A Management of institutional activity (Community)", amt, 0, amt);

                        amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(INCOME)", "LEDGER_ID=" + IGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A1", "1.10", "Integrated Good & Service Tax (IGST)",
                                            "A1 Income - Institutional Activity", "A Management of institutional activity (Community)", amt, 0, amt);

                        //For Expense Part
                        amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(EXPENSE)", "LEDGER_ID=" + CGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A2", "2.20", "Central Goods & Service Tax (CGST)",
                                            "A2 Expenditure - Institutional activity", "A Management of institutional activity (Community)", 0, amt, amt);

                        amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(EXPENSE)", "LEDGER_ID=" + SGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A2", "2.20", "State Goods & Service Tax (SGST)",
                                            "A2 Expenditure - Institutional activity", "A Management of institutional activity (Community)", 0, amt, amt);

                        amt = this.UtilityMember.NumberSet.ToDouble(dtGSTLedgers.Compute("SUM(EXPENSE)", "LEDGER_ID=" + IGST).ToString());
                        AddGSTLedgerAmounts(resultArgs.DataSource.Table, "A2", "2.20", "Integrated Good & Service Tax (IGST)",
                                            "A2 Expenditure - Institutional activity", "A Management of institutional activity (Community)", 0, amt, amt);

                        resultArgs.DataSource.Table.Columns.Add("CODE_LENGTH", typeof(Int32), "LEN(" + reportSetting1.CongiregationProfitandLoss.LEDGER_CODEColumn.ColumnName + ")");
                    }
                }
            }

            return resultArgs;
        }

        private void AddGSTLedgerAmounts(DataTable dtSource, string ConLedgerCode, string LedgerCode, string ConLedgerName,
                            string Master, string MasterParent, double receipt, double payment, double amount)
        {
            DataRow dr = dtSource.NewRow();
            dr[reportSetting1.CongiregationProfitandLoss.CON_LEDGER_CODEColumn.ColumnName] = ConLedgerCode;
            dr[reportSetting1.CongiregationProfitandLoss.LEDGER_CODEColumn.ColumnName] = LedgerCode;
            dr[reportSetting1.CongiregationProfitandLoss.CON_LEDGER_NAMEColumn.ColumnName] = ConLedgerName;
            dr[reportSetting1.CongiregationProfitandLoss.MASTER_NAMEColumn.ColumnName] = Master;
            dr[reportSetting1.CongiregationProfitandLoss.MASTER_PARENTColumn.ColumnName] = MasterParent;
            dr[reportSetting1.CongiregationProfitandLoss.NXTRECEIPTColumn.ColumnName] = receipt;
            dr[reportSetting1.CongiregationProfitandLoss.NXTPAYMENTColumn.ColumnName] = payment;
            dr["AMOUNT_ACTUAL"] = amount;
            dtSource.Rows.Add(dr);
        }

        /// <summary>
        /// To get Community ProjectsId
        /// </summary>
        /// <returns></returns>
        private string GetCommunityProjectIds()
        {
            string Rtn = string.Empty;
            Int32 CommunityProjectCategoryId = 0;

            if (HttpContext.Current.Session["ProjectSource"] != null)
            {
                //# Get Project Category Community Id 
                using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                {
                    ResultArgs resultArgs = projectCategorySystem.ProjectCatogoryDetailsByName("Community", DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProjectCategory = resultArgs.DataSource.Table;
                        CommunityProjectCategoryId = UtilityMember.NumberSet.ToInteger(dtProjectCategory.Rows[0][projectCategorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName].ToString());
                    }
                }

                //# Get Community Projects
                DataTable dCommunityProjects = ((DataTable)HttpContext.Current.Session["ProjectSource"]).DefaultView.ToTable();
                if (dCommunityProjects != null)
                {
                    dCommunityProjects.DefaultView.RowFilter = "PROJECT_CATEGORY_ID = " + CommunityProjectCategoryId;
                    dCommunityProjects = dCommunityProjects.DefaultView.ToTable();
                    foreach (DataRow dr in dCommunityProjects.Rows)
                    {
                        Rtn += dr["PROJECT_ID"].ToString() + ",";
                    }
                }
            }
            Rtn = Rtn.TrimEnd(',').Trim();

            if (String.IsNullOrEmpty(Rtn))
            {
                Rtn = "0";
            }
            return Rtn;
        }
        #endregion

        private void xrTableFooterExpenseCongGroSub_SummaryRowChanged(object sender, EventArgs e)
        {
            groupDebitSum += (GetCurrentColumnValue("NXTRECEIPT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("NXTRECEIPT").ToString());
            groupCreditSum += (GetCurrentColumnValue("NXTPAYMENT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("NXTPAYMENT").ToString());
        }
        private void xrTableFooterExpenseCongGroSub_SummaryReset(object sender, EventArgs e)
        {
            groupDebitSum = groupCreditSum = 0;
        }
        private void xrTableFooterExpenseCongGroSub_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double Balance = 0;

            Balance = groupDebitSum - groupCreditSum;
            TotalNetProfitExpense += Balance;

            e.Result = Balance;
            e.Handled = true;
        }
        private void xrTableCell3_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalNetProfit = 0;
            TotalNetProfit = TotalNetProfitExpense - E_TotalAmount;
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalNetProfit);
            e.Handled = true;
            this.NetTotalManagementActivity = TotalNetProfit;
        }

        private void xrTableCongGrpSub_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string MasterName = (GetCurrentColumnValue("MASTER_NAME") == null) ? string.Empty : GetCurrentColumnValue("MASTER_NAME").ToString();
            xrTableCongGrpSub.Text = MasterName;

            if (xrTableCongGrpSub.Text.Equals("A1 Income - Institutional Activity"))
            {
                xrTableCongGrpSub.Text = "A1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("A2 Expenditure - Institutional activity"))
            {
                xrTableCongGrpSub.Text = "A2 Expenditure";
            }

            if (xrTableCongGrpSub.Text.Equals("B1 Income - Fixed Assets"))
            {
                xrTableCongGrpSub.Text = "B1 Income";
            }
            if (xrTableCongGrpSub.Text.Equals("B2 Expenditure - Fixed Assets"))
            {
                xrTableCongGrpSub.Text = "B2 Expenditure";
            }

            if (xrTableCongGrpSub.Text.Equals("C1 Income - Financial Management"))
            {
                xrTableCongGrpSub.Text = "C1 Income from Financial Management";
            }
            if (xrTableCongGrpSub.Text.Equals("C2 Expenditure - Financial Management"))
            {
                xrTableCongGrpSub.Text = "C2 Financial Management Expenditure";
            }

            if (xrTableCongGrpSub.Text.Equals("D1 Income - Extraordinary"))
            {
                xrTableCongGrpSub.Text = "D1 Extraordinary Income";
            }
            if (xrTableCongGrpSub.Text.Equals("D2 Expenditure - Extraordinary"))
            {
                xrTableCongGrpSub.Text = "D2 Extraordinary Expenditure";
            }
        }

        private void xrRowA2ExpenseSubTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool isA2ExpenseSubTotal = (xrTableCongGrpSub.Text.ToLower().ToUpper() == "A2 EXPENDITURE" &&
                                        (xrTableCode.Text.Trim() == "2.6" || xrTableCode.Text.Trim() == "2.21" || xrTableCode.Text.Trim() == "2.26"));
            e.Cancel = !isA2ExpenseSubTotal;

            if (isA2ExpenseSubTotal)
            {
                if (xrTableCode.Text.Trim() == "2.6")
                {
                    xrCellA2ExpenseSubTotalTitle.Text = "Total expenses for remunerations and fees";
                }
                else if (xrTableCode.Text.Trim() == "2.21")
                {
                    xrCellA2ExpenseSubTotalTitle.Text = "Total General and administrative expenses";
                }
                else if (xrTableCode.Text.Trim() == "2.26")
                {
                    xrCellA2ExpenseSubTotalTitle.Text = "Total charitable donations";
                }
                else
                {
                    xrCellA2ExpenseSubTotalTitle.Text = string.Empty;
                }
            }
        }

        private void xrA2ExpenseSubIncomeTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool isA2ExpenseSubTotal = (xrTableCongGrpSub.Text.ToLower().ToUpper() == "A2 EXPENDITURE" &&
                                        (xrTableCode.Text.Trim() == "2.6" || xrTableCode.Text.Trim() == "2.21" || xrTableCode.Text.Trim() == "2.26"));
            e.Cancel = !isA2ExpenseSubTotal;
            XRTableCell cell = sender as XRTableCell;
            if (isA2ExpenseSubTotal)
            {
                if (xrTableCode.Text.Trim() == "2.6" && A2ExpenseSubTotalIncome > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalIncome);
                }
                else if (xrTableCode.Text.Trim() == "2.21" && A2ExpenseSubTotalIncome1 > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalIncome1);
                }
                else if (xrTableCode.Text.Trim() == "2.26" && A2ExpenseSubTotalIncome2 > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalIncome2);
                }
                else
                {
                    cell.Text = string.Empty;
                }
            }

        }

        private void xrA2ExpenseSubExpenseTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool isA2ExpenseSubTotal = (xrTableCongGrpSub.Text.ToLower().ToUpper() == "A2 EXPENDITURE" &&
                                       (xrTableCode.Text.Trim() == "2.6" || xrTableCode.Text.Trim() == "2.21" || xrTableCode.Text.Trim() == "2.26"));
            e.Cancel = !isA2ExpenseSubTotal;
            XRTableCell cell = sender as XRTableCell;
            if (isA2ExpenseSubTotal)
            {
                if (xrTableCode.Text.Trim() == "2.6" && A2ExpenseSubTotalExpense > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalExpense);
                }
                else if (xrTableCode.Text.Trim() == "2.21" && A2ExpenseSubTotalExpense1 > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalExpense1);
                }
                else if (xrTableCode.Text.Trim() == "2.26" && A2ExpenseSubTotalExpense2 > 0)
                {
                    cell.Text = UtilityMember.NumberSet.ToNumber(A2ExpenseSubTotalExpense2);
                }
                else
                {
                    cell.Text = string.Empty;
                }
            }
        }

        private void xrTableCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.LEDGER_NAMEColumn.ColumnName) != "")
            {
                XRTableCell cell = sender as XRTableCell;
                string gstledgername = GetCurrentColumnValue(reportSetting1.CongiregationProfitandLoss.CON_LEDGER_NAMEColumn.ColumnName).ToString();

                if (gstledgername.ToUpper() == CGST_Name.ToUpper())
                {
                    cell.Text = cell.Text + "W";
                }
                else if (gstledgername.ToUpper() == SGST_Name.ToUpper())
                {
                    cell.Text = cell.Text + "X";
                }
                else if (gstledgername.ToUpper() == IGST_Name.ToUpper())
                {
                    cell.Text = cell.Text + "Y";
                }
            }
        }


    }
}

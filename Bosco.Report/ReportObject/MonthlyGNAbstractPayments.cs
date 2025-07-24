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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MonthlyGNAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor

        public MonthlyGNAbstractPayments()
        {
            InitializeComponent();

            //this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName, new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName },
            //    DrillDownType.GROUP_SUMMARY_PAYMENTS, false, "", true);

            //this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName },
            //        DrillDownType.LEDGER_SUMMARY, false, "", true);
        }

        #endregion

        #region Variables
        public double TotalReceiptAmout { get; set; }
        public double TotalPaymentAmout { get; set; }
        private double fdDifference = 0;
        public int Flag = 0;

        #endregion

        #region ShowReport


        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                //BindPaymentSource();
                base.ShowReport();
            }
        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindPaymentSource(DataTable dtPayments)
        {
            this.ReportTitle = objReportProperty.ReportTitle;
            SetReportTitle();
            this.SetReportDate = this.ReportProperties.ReportDate;
            xrCellFDDifferenceCaption.Text = "FD Difference";
            TotalPaymentAmout = 0;
                        
            xrCellTotalPayments.Text = "Total Payments";
            Titleclosngbalance.Text = "Closing Balance";

            if (this.ReportProperties.ShowDetailedBalance == 1)
            {
                ReportProperties.ShowByLedgerGroup = 0;  //ReportProperties.ShowByLedgerGroup = 1;
                ReportProperties.ShowByLedger = 1;
            }

            if (dtPayments != null)
            {
                TotalPaymentAmout = UtilityMember.NumberSet.ToDouble(dtPayments.Compute("SUM(AMOUNT_PERIOD)", string.Empty).ToString());
                
                xrSubClosingBalance.Visible = false;
                if (!string.IsNullOrEmpty(this.ReportProperties.Project))
                {
                    xrSubClosingBalance.Visible = true;
                    AccountBalance xrSubBalance = xrSubClosingBalance.ReportSource as AccountBalance;
                    SetReportSetting(xrSubBalance);
                    xrSubBalance.BindBalance(false, false);
                    TotalPaymentAmout += xrSubBalance.PeriodBalanceAmount;
                    fdDifference = (TotalReceiptAmout - TotalPaymentAmout);
                    prBalancePeriodAmount.Value = xrSubBalance.PeriodBalanceAmount + fdDifference;

                    xrCellFDDifference.Text = UtilityMember.NumberSet.ToNumber(fdDifference);

                    xrSubBalance.NameHeaderColumWidth = xrSubBalance.NameColumnWidth = xrTableCell11.WidthF;
                    xrSubBalance.AmountHeaderColumWidth = xrSubBalance.AmountColumnWidth = xrTableCell12.WidthF;
                }

                dtPayments.TableName = "MonthlyAbstract";
                this.DataSource = dtPayments;
                this.DataMember = dtPayments.TableName;
            }
            
            SetReportBorder();
            SortByLedgerorGroup();
        }

        private void SetReportSetting(AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            SetReportBorder();

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
           
            //Include / Exclude Code
            if (tcCapCode.Tag != null && tcCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCode.Tag.ToString());
            }
            else
            {
                tcCapCode.Tag = tcCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            //tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            //tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            //tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = true;
            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpParentGroup.GroupFields[0].FieldName = "";
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";

            if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
            {
                grpLedger.Visible = true;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName;

                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;

                }
                else
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
            }

            if (grpLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
            }

            //Group Row Style
            if (grpLedger.Visible == false)
            {
                styleGroupRow.BackColor = Color.White;
                styleGroupRow.Borders = styleRow.Borders;
                xrTableLedgerGroup.StyleName = styleGroupRow.Name;
            }
            else
            {
                xrTableLedgerGroup.StyleName = styleGroupRowBase.Name;
            }
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            //Set Subreport Properties
            //accountBalance.CodeColumnWidth = tcCapCode.WidthF + tcCapParticulars.WidthF;
            //accountBalance.AmountHeaderColumWidth = tcCapAmountPeriod.WidthF;
            //accountBalance.AmountProgressiveHeaderColumnWidth = tcCapAmountPeriod.WidthF;
            //accountBalance.AmountColumnWidth = tcCapAmountPeriod.WidthF;
            //accountBalance.AmountProgressiveColumnWidth = 90;
            this.setHeaderTitleAlignment();
        }

        private void SetReportBorder()
        {

            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrTotalPayment = AlignTotalTable(xrTotalPayment);
            xrGrandTotal = AlignGrandTotalTable(xrGrandTotal);
            xrParentGroup = AlignGroupTable(xrParentGroup);
        }

        private XRTable AlignGroupTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                            tcell.Borders = BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }

            return table;

        }

        public override XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  //  GROUP_CODE
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";  //  GROUP_CODE
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    grpLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_NAME
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
        }

        #endregion

        private void tcAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double PeriodAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountPeriod.Text);
            //if (PeriodAmt != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    tcAmountPeriod.Text = "";
            //}
        }

        private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double ProgressAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
            //if (ProgressAmt != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    tcAmountProgress.Text = "";
            //}
        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null)
            //{
            //    string ParentGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName) != null ?
            //        GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
            //    string LedgerGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null ?
            //        GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

            //    if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void xrTblFDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (fdDifference == 0);
        }
    }
}

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
    public partial class MonthlyAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor

        public MonthlyAbstractPayments()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName, new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName },
                DrillDownType.GROUP_SUMMARY_PAYMENTS, false, "", true);

            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName },
                    DrillDownType.LEDGER_SUMMARY, false, "", true);
        }

        #endregion

        #region Variables
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
                BindPaymentSource();
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

        public void BindPaymentSource()
        {
            this.ReportTitle = objReportProperty.ReportTitle;
            SetReportTitle();
            this.SetReportDate = this.ReportProperties.ReportDate;
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtPayments= resultArgs.DataSource.Table;

                if (dtPayments != null)
                {
                    dtPayments.TableName = "MonthlyAbstract";
                    this.DataSource = dtPayments;
                    this.DataMember = dtPayments.TableName;
                }
            }

            AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
            SetReportSetting(accountBalance);
            accountBalance.BindBalance(false, true);

            prBalancePeriodAmount.Value = accountBalance.PeriodBalanceAmount;
            prBalanceProgressiveAmount.Value = accountBalance.ProgressiveBalanceAmount;
            //xrTableCell6.WidthF = tcGrpGroupName.WidthF;
            SetReportBorder();
            SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Report.MonthlyAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());

                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void SetReportSetting(AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            SetReportBorder();

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);

            //Attach / Detach all ledgers
            //dvPayment.RowFilter = "";
            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvPayment.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //}

            //if (dvPayment.Count == 0)
            //{
            //    DataRowView drvPayment = dvPayment.AddNew();
            //    drvPayment.BeginEdit();
            //    drvPayment[reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName] = 0;
            //    drvPayment[reportSetting1.MonthlyAbstract.AMOUNT_PROGRESSIVEColumn.ColumnName] = 0;
            //    drvPayment[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;
            //    drvPayment.EndEdit();
            //}

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
            tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
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
            xrSubBalance.LeftF = xrTableHeader.LeftF;
            accountBalance.LeftPosition = (xrTableHeader.LeftF - 5);
            accountBalance.CodeColumnWidth = tcCapCode.WidthF;
            accountBalance.NameColumnWidth = tcLedgerName.WidthF;
            accountBalance.AmountColumnWidth = tcCapAmountPeriod.WidthF + 1;
            accountBalance.AmountProgressiveColumnWidth = tcCapAmountProgress.WidthF - 1;

            //accountBalance.GroupNameWidth =  tcLedgerName.WidthF ;// 2.29f;
            accountBalance.GroupAmountWidth = tcCapAmountPeriod.WidthF + 1.50f;
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
            double PeriodAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountPeriod.Text);
            if (PeriodAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountPeriod.Text = "";
            }
        }

        private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ProgressAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
            if (ProgressAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountProgress.Text = "";
            }
        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

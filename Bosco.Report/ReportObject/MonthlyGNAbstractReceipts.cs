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
    public partial class MonthlyGNAbstractReceipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        public int Flag = 0;
        #endregion


        public double TotalReceiptAmount { get; set; }

        #region Constructor

        public MonthlyGNAbstractReceipts()
        {
            InitializeComponent();

            //  ReportProperties.IncludeNarration = 1;
            //this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName,
            //        new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false, "", true);
            //this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName,
            //    new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY, false, "", true);
        }
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
            }
            else
            {
                //BindReceiptSource();
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

        public void BindReceiptSource(DataTable dtReceipts)
        {
            SettingProperty settingProperty = new SettingProperty();

            // this.HideReportHeader = true;
            SetReportTitle();
            //ResultArgs resultArgs = GetReportSource();
            if (this.ReportProperties.ShowDetailedBalance == 1)
            {
                ReportProperties.ShowByLedgerGroup = 0;
                //ReportProperties.ShowByLedgerGroup = 1;
                ReportProperties.ShowByLedger = 1;
            }
            //if (resultArgs.Success)
            //{
            //DataTable dtReceipts= resultArgs.DataSource.Table;
            if (dtReceipts != null)
            {
                TotalReceiptAmount = this.UtilityMember.NumberSet.ToDouble(dtReceipts.Compute("SUM(AMOUNT_PERIOD)", "").ToString());
                
                dtReceipts.TableName = "MonthlyAbstract";
                this.DataSource = dtReceipts;
                this.DataMember = dtReceipts.TableName;
            }
            //}

            xrSubOpeningBalance.Visible = false;
            if (!string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                xrSubOpeningBalance.Visible = true;
                AccountBalance accountBalance = xrSubOpeningBalance.ReportSource as AccountBalance;
                //SetReportSetting(dvReceipt, accountBalance);
                SetReportSetting(accountBalance);
                accountBalance.BindBalance(true, false);

                prBalancePeriodAmount.Value = accountBalance.PeriodBalanceAmount;
                TotalReceiptAmount += accountBalance.PeriodBalanceAmount;
                //prBalanceProgressiveAmount.Value = accountBalance.ProgressiveBalanceAmount;

                //accountBalance.NameHeaderColumWidth = accountBalance.NameColumnWidth = xrTableCell7.WidthF + xrTableCell11.WidthF;
                accountBalance.NameHeaderColumWidth = accountBalance.NameColumnWidth = xrTableCell11.WidthF;
                accountBalance.AmountHeaderColumWidth = accountBalance.AmountColumnWidth = xrTableCell12.WidthF;
                //accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = xrTableCell13.WidthF;
            }

            SetReportBorder();
            SortByLedgerorGroup();

        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void SetReportSetting(AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);

            //Attach / Detach all ledgers
            //dvReceipt.RowFilter = "";
            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //}

            //if (dvReceipt.Count == 0)
            //{
            //    DataRowView drvReceipt = dvReceipt.AddNew();
            //    drvReceipt.BeginEdit();
            //    drvReceipt[reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName] = 0;
            //    drvReceipt[reportSetting1.MonthlyAbstract.AMOUNT_PROGRESSIVEColumn.ColumnName] = 0;
            //    drvReceipt[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;
            //    drvReceipt.EndEdit();
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
            //tcGrpParentCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            //tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = true;
            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1); // 
            grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
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
            accountBalance.CodeColumnWidth = tcCapCode.WidthF + tcCapParticulars.WidthF;
            accountBalance.AmountColumnWidth = tcCapAmountPeriod.WidthF;
            accountBalance.AmountHeaderColumWidth = tcCapAmountPeriod.WidthF;
            accountBalance.AmountProgressiveColumnWidth = accountBalance.AmountProgressiveHeaderColumnWidth = tcCapAmountProgress.WidthF;
            this.setHeaderTitleAlignment();
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
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
                grpParentGroup.SortingSummary.Enabled = true;
                grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";
                grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";
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
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";
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
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
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

        #region Events

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

        //private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    double ProgressAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
        //    if (ProgressAmt != 0)
        //    {
        //        e.Cancel = false;
        //    }
        //    else
        //    {
        //        tcAmountProgress.Text = "";
        //    }
        //}

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
        #endregion

        
    }
}

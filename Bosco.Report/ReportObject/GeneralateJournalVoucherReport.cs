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
    public partial class GeneralateJournalVoucherReport : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        public int Flag = 0;
        public double TotalReceiptsAmt { get; set; }
        public double TotalPaymentAmt { get; set; }
        public double TotalAmount { get; set; }
        #endregion

        #region Constructor

        public GeneralateJournalVoucherReport()
        {
            InitializeComponent();

        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Society == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                //BindJournalSource();
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

        public void BindJournalSource(DataTable dtJournalVouchers)
        {
            SettingProperty settingProperty = new SettingProperty();
            SetReportTitle();
            //ResultArgs resultArgs = GetJournalSource();

            if (this.ReportProperties.ShowDetailedBalance == 1)
            {
                ReportProperties.ShowByLedgerGroup = 1;
                ReportProperties.ShowByLedger = 1;
            }

            if (dtJournalVouchers != null)
            {
                TotalReceiptsAmt = this.UtilityMember.NumberSet.ToDouble(dtJournalVouchers.Compute("SUM(CREDIT_AMOUNT)", string.Empty).ToString());
                TotalPaymentAmt = this.UtilityMember.NumberSet.ToDouble(dtJournalVouchers.Compute("SUM(DEBIT_AMOUNT)", string.Empty).ToString());
                TotalAmount = this.UtilityMember.NumberSet.ToDouble(dtJournalVouchers.Compute("SUM(AMOUNT)", string.Empty).ToString());

                dtJournalVouchers.TableName = "CongiregationJournal";
                this.DataSource = dtJournalVouchers;
                this.DataMember = dtJournalVouchers.TableName;
            }

            grpLedgerGroup.Visible = true;
            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1); // 
            Detail.Visible = (ReportProperties.ShowByLedger == 1);

            SetReportBorder();
            SortByLedgerorGroup();

        }

        private ResultArgs GetJournalSource()
        {
            ResultArgs resultArgs = null;
            string sqlJournalTransaction = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateJournalLedgerVouchers);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateJournalLedgerVouchers, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlJournalTransaction);
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
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
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
        }
        #endregion

        private void xrtblTotalReceipts_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(TotalReceiptsAmt);
            e.Handled = true;
        }

        private void xrtblTotalPayments_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(TotalPaymentAmt);
            e.Handled = true;
        }

        private void xrtblTotalAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(TotalAmount);
            e.Handled = true;
        }

        private void grpHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (TotalAmount == 0);
        }

        private void grpParentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (TotalAmount == 0);
        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (TotalAmount == 0);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (TotalAmount == 0);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = (TotalAmount == 0);
        }

        #region Events

        #endregion

    }
}

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
    public partial class CostCentreMontlyAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {

        #region Constructor

        public CostCentreMontlyAbstractPayments()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 722.25f;
            this.SetLandscapeFooter = 722.25f;
            this.AttachDrillDownToRecord(xrtblCCLedgerGroup, xrcellCCLedgerGroup,
                 new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
                 this.ReportParameters.COST_CENTRE_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName,
                new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName,
                this.ReportParameters.COST_CENTRE_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false, "", true);
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                    || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
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
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = objReportProperty.CostCentreName;
            this.HideCostCenter = (ReportProperties.ShowByCostCentre == 0);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            ResultArgs resultArgs = GetReportSource();
            DataView dvReceipt = resultArgs.DataSource.TableView;

            if (dvReceipt != null)
            {
                dvReceipt.Table.TableName = "MonthlyAbstract";
                this.DataSource = dvReceipt;
                this.DataMember = dvReceipt.Table.TableName;
            }
            SetReportSetting(dvReceipt);
            SetReportBorder();
            SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.MonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CostCentre.MonthlyAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void SetReportSetting(DataView dvReceipt)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);

            //Attach / Detach all ledgers
            dvReceipt.RowFilter = "";
            if (ReportProperties.IncludeAllLedger == 0)
            {
                dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            }
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
            tcCapCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //this line by amal
            tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            xrcellccGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
            //Include / Exclude Ledger group or Ledger            
            grpCostCName.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCCLedgerGroup.Visible = grpParentGroup.Visible = ReportProperties.ShowByLedgerGroup == 1;
            grpLedger.Visible = ReportProperties.ShowByLedger == 1;

            if (ReportProperties.ShowByCostCentreCategory == 1)
            {
                if (ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCentreCategory.Visible = true;
                }
                else
                {
                    grpCostCentreCategory.Visible = false;
                }
            }
            else
            {
                grpCostCentreCategory.Visible = false;
            }

            if (grpCostCName.Visible != true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            if (grpCCLedgerGroup.Visible == false && grpLedger.Visible == false)
            {
                if (ReportProperties.ShowByCostCentreCategory != 1)
                    grpLedger.Visible = true;
                else
                    grpLedger.Visible = false;
            }

            grpParentGroup.GroupFields[0].FieldName = "";
            grpCCLedgerGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";
            grpCostCName.GroupFields[0].FieldName = "";
            grpCostCentreCategory.GroupFields[0].FieldName = "";

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

            if (grpCCLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpCCLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpCCLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
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

            if (grpCostCName.Visible)
            {
                if (ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCName.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.COST_CENTRE_NAMEColumn.ColumnName;
                }
            }

            if (grpCostCentreCategory.Visible)
            {
                if (ReportProperties.ShowByCostCentreCategory == 1)
                {
                    grpCostCentreCategory.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                }
            }
            //Include / Exclude Report Lines

            //Sub Group Row Style
            if (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else if (ReportProperties.ShowVerticalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            }

            //Group Row Style
            if (grpLedger.Visible == false)
            {
                styleGroupRow.BackColor = Color.White;
                styleGroupRow.Borders = styleRow.Borders;
                xrtblCCLedgerGroup.StyleName = styleGroupRow.Name;
            }
            else
            {
                xrtblCCLedgerGroup.StyleName = styleGroupRowBase.Name;
            }
        }

        private void SetReportBorder()
        {
            xrParentGroup = AlignGroupTable(xrParentGroup);
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrtblCCLedgerGroup = AlignGroupTable(xrtblCCLedgerGroup);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
            xrtblCostCentreCategroyName = AlignCCCategoryTable(xrtblCostCentreCategroyName);
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
                        if (count == 1)
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

        private XRTable AlignCCCategoryTable(XRTable table)
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
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
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
        private XRTable AlignCostCentreTable(XRTable table)
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
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
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

        #endregion

        #region Events

        private void tcAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(tcAmountPeriod.Text);
            if (CashReceipts != 0)
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
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountProgress.Text = "";
            }
        }

        #endregion

        private void SortByLedgerorGroup()
        {

            //grpCostCentreCategory.SortingSummary.Enabled = true;
            //grpCostCentreCategory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //grpCostCentreCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //grpCostCentreCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //if (grpCCCatogory.Visible)
            //{
            //grpCCCatogory.SortingSummary.Enabled = true;
            //grpCCCatogory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //grpCCCatogory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //grpCCCatogory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCName.Visible)
            //{
            //grpCostCName.SortingSummary.Enabled = true;
            //grpCostCName.SortingSummary.FieldName = "COST_CENTRE_NAME";
            //grpCostCName.SortingSummary.Function = SortingSummaryFunction.Avg;
            //grpCostCName.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //if (grpCCLedgerGroup.Visible)
            //{

            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    grpParentGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
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

            if (grpCCLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpCCLedgerGroup.SortingSummary.Enabled = true;
                    grpCCLedgerGroup.SortingSummary.FieldName = "SORT_ORDER"; // GROUP_CODE
                    grpCCLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpCCLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpCCLedgerGroup.SortingSummary.Enabled = true;
                    grpCCLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpCCLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpCCLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
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
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";  // LEDGER_CODE
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
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";  // LEDGER_CODE
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";  // LEDGER_CODE
                    }
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //if (grpCostCentreCategory.Visible)
            //{

            //}
        }

        private void grpParentGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void grpCCLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

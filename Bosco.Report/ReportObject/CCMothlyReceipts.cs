using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CCMothlyReceipts : Bosco.Report.Base.ReportHeaderBase
    {

        #region Properties

        public bool ShowLedger
        {
            set { xrtblLedger.Visible = value; }
        }
        #endregion

        #region Constructor

        public CCMothlyReceipts()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 722.25f;
            this.SetLandscapeFooter = 722.25f;
            this.AttachDrillDownToRecord(xrtblLedgerGroup, tcgrpLedgerGroup,
                 new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
                 this.ReportParameters.COST_CENTRE_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false);
            this.AttachDrillDownToRecord(xrtblLedger, tcgrpcosLedgerName,
                new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName,
                this.ReportParameters.COST_CENTRE_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_RECEIPTS, false);
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindReceiptSource();
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

        public void BindReceiptSource()
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = objReportProperty.CostCentreName;
            // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideCostCenter = (ReportProperties.ShowByCostCentre == 0);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ResultArgs resultArgs = GetReportSource();

            grpCCBreakup.Visible = (objReportProperty.BreakByCostCentre == 1) ? true : false;
            DataView dvReceipt = resultArgs.DataSource.TableView;


            if (dvReceipt != null)
            {
                dvReceipt.Table.TableName = "MonthlyAbstract";
                this.DataSource = dvReceipt;
                this.DataMember = dvReceipt.Table.TableName;
            }
            SetReportBorder();
            SetReportSetting(dvReceipt);
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
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());

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
            float actualCodeWidth = tcCapcoscode.WidthF;
            bool isCapCodeVisible = true;

            tcCapCosAmountPeriod.Text = this.SetCurrencyFormat(tcCapCosAmountPeriod.Text);
            tcCapCosProgressive.Text = this.SetCurrencyFormat(tcCapCosProgressive.Text);

            //Attach / Detach all ledgers 
            // commended by chinna
            //dvReceipt.RowFilter = "";
            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //}

            //Include / Exclude Code
            if (tcCapcoscode.Tag != null && tcCapcoscode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapcoscode.Tag.ToString());
            }
            else
            {
                tcCapcoscode.Tag = tcCapcoscode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapcoscode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
            tcParentGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcgrpCosCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcgrpCosLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerCosGroup.Visible = grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpCosLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpCostCentreName.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpCosLedger.Visible = (ReportProperties.ShowByLedger == 1);

            if (ReportProperties.ShowByCostCentreCategory == 1)
            {
                //grpCosLedger.Visible = true;
                if (ReportProperties.ShowByCostCentre == 1)
                {
                    xrtblCCCName.Visible = true;
                }
                else
                {
                    xrtblCCCName.Visible = true;
                }
            }

            grpCostCentreCategoryName.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpLedgerCosGroup.GroupFields[0].FieldName = "";
            grpCosLedger.GroupFields[0].FieldName = "";
            grpCostCentreName.GroupFields[0].FieldName = "";


            if (grpLedgerCosGroup.Visible == false && grpCosLedger.Visible == false)
            {
                if (ReportProperties.ShowByCostCentreCategory != 1)
                    grpCosLedger.Visible = true;
                else
                    grpCosLedger.Visible = false;
            }

            if (grpCostCentreCategoryName.Visible)
            {
                if (ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCentreCategoryName.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                }
            }

            if (grpCostCentreName.Visible)
            {
                if (ReportProperties.ShowByCostCentre == 1)
                {
                    grpCostCentreName.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.COST_CENTRE_NAMEColumn.ColumnName;
                }
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

            if (grpLedgerCosGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerCosGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;

                }
                else
                {
                    grpLedgerCosGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
            }



            if (grpCosLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpCosLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpCosLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrParentGroup = AlignGroupTable(xrParentGroup);
            xrtblLedgerGroup = AlignGroupTable(xrtblLedgerGroup);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblCostCentreName = AlignCostCentreTable(xrtblCostCentreName);
            xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            xrtblCCTotal = AlignGroupTable(xrtblCCTotal);
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

        private void SortByLedgerorGroup()
        {

            grpCostCentreCategoryName.SortingSummary.Enabled = true;
            grpCostCentreCategoryName.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            grpCostCentreCategoryName.SortingSummary.Function = SortingSummaryFunction.Avg;
            grpCostCentreCategoryName.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //if (grpCostCategoryName.Visible)
            //{
            grpCostCentreCategoryName.SortingSummary.Enabled = true;
            grpCostCentreCategoryName.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            grpCostCentreCategoryName.SortingSummary.Function = SortingSummaryFunction.Avg;
            grpCostCentreCategoryName.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCentreName.Visible)
            //{
            grpCostCentreName.SortingSummary.Enabled = true;
            grpCostCentreName.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            grpCostCentreName.SortingSummary.Function = SortingSummaryFunction.Avg;
            grpCostCentreName.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

            //if (grpLedgerCosGroup.Visible)
            //{
            if (grpParentGroup.Visible)
            {
                grpParentGroup.SortingSummary.Enabled = true;
                grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (this.ReportProperties.ShowByLedgerGroup == 0)
            {
                grpLedgerCosGroup.SortingSummary.Enabled = true;
                grpLedgerCosGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                grpLedgerCosGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpLedgerCosGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            else
            {
                grpLedgerCosGroup.SortingSummary.Enabled = true;
                grpLedgerCosGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                grpLedgerCosGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpLedgerCosGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            //}
            //if (grpCosLedger.Visible)
            //{

            if (this.ReportProperties.SortByLedger == 0)
            {
                grpCosLedger.SortingSummary.Enabled = true;
                if (this.ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpCosLedger.SortingSummary.FieldName = "SORT_ORDER";
                    grpCosLedger.SortingSummary.FieldName = "LEDGER_CODE";
                }
                else
                {
                    grpCosLedger.SortingSummary.FieldName = "LEDGER_CODE";
                }
                grpCosLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpCosLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            else
            {
                grpCosLedger.SortingSummary.Enabled = true;
                if (this.ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpCosLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                    grpCosLedger.SortingSummary.FieldName = "LEDGER_NAME";
                }
                else
                {
                    grpCosLedger.SortingSummary.FieldName = "LEDGER_NAME";
                }
                grpCosLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpCosLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            //}

            //if (grpCostCentreCategoryName.Visible)
            //{

            //}
        }

        private void grpLedgerCosGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

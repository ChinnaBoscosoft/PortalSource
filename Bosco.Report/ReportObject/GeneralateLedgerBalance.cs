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
    public partial class GeneralateLedgerBalance: Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        public int Flag = 0;
        private bool RecordsExists = false;
        #endregion

        #region Constructor

        public GeneralateLedgerBalance()
        {
            InitializeComponent();
            //  ReportProperties.IncludeNarration = 1;
            this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName,
                    new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_RECEIPTS, false, "", true);
            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName,
                new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY, false, "", true);
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
            SettingProperty settingProperty = new SettingProperty();
            
            // this.HideReportHeader = true;
            SetReportTitle();
            RecordsExists = false;
            grpParentGroup.Visible = grpLedgerGroup.Visible = grpLedger.Visible = RecordsExists;
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtReceipts= resultArgs.DataSource.Table;
                if (dtReceipts != null)
                {
                    /*if (!string.IsNullOrEmpty(this.ReportProperties.LedgerGroup) && this.ReportProperties.LedgerGroup != "0")
                    {
                        dtReceipts.DefaultView.RowFilter = ReportParameters.GROUP_IDColumn.ColumnName + " IN (" + this.ReportProperties.LedgerGroup + ")";
                        dtReceipts = dtReceipts.DefaultView.ToTable();
                    }*/
                    RecordsExists = (dtReceipts.Rows.Count>0);;
                    dtReceipts.TableName = "MonthlyAbstract";
                    this.DataSource = dtReceipts;
                    this.DataMember = dtReceipts.TableName;
                }
            }
            SetReportSetting();
            SetReportBorder();
            SortByLedgerorGroup();
            grpConLedgerHeader.Visible = RecordsExists;
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateLedgerBalance);

            if ((!string.IsNullOrEmpty(this.ReportProperties.CongregationLedger) && this.ReportProperties.CongregationLedger != "0") &&
               (string.IsNullOrEmpty(this.ReportProperties.LedgerGroup) || this.ReportProperties.LedgerGroup == "0"))
            {
                sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateLedgerBalanceByConLedger);
            }
            else if ((!string.IsNullOrEmpty(this.ReportProperties.LedgerGroup) && this.ReportProperties.LedgerGroup != "0") &&
               (string.IsNullOrEmpty(this.ReportProperties.CongregationLedger) || this.ReportProperties.CongregationLedger == "0"))
            {
                sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateLedgerBalanceByLedgerGroup);
            }
            
            
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateLedgerBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                
                //Should always padding
                int LedgerPaddingRequired = 1; // (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = 1; //(ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

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

                if (!string.IsNullOrEmpty(this.ReportProperties.CongregationLedger) && this.ReportProperties.CongregationLedger != "0")
                {
                    dataManager.Parameters.Add(this.reportSetting1.CongiregationProfitandLoss.CON_LEDGER_IDColumn.ColumnName, this.ReportProperties.CongregationLedger);
                }
                
                if (!string.IsNullOrEmpty(this.ReportProperties.LedgerGroup) && this.ReportProperties.LedgerGroup != "0")
                {
                    dataManager.Parameters.Add("LEDGER_GROUP_ID", this.ReportProperties.LedgerGroup);
                }
                
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }
               
        private void SetReportSetting()
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;
            ReportProperties.ShowGroupCode = 1;
            ReportProperties.ShowLedgerCode = 1;

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(tcCapAmountPeriod.Text);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(tcCapAmountProgress.Text);
                        
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
            tcGrpParentCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            ReportProperties.ShowByLedgerGroup = 1;
            grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1 && RecordsExists); // 
            grpLedgerGroup.Visible = grpLedger.Visible = (ReportProperties.ShowByLedger == 1 && RecordsExists);
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";
                        
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
            this.setHeaderTitleAlignment();
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblParentGroup = AlignGroupTable(xrtblParentGroup);
            xrTblConLedger = AlignGroupTable(xrTblConLedger, true);
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

        public void SortByLedgerorGroup()
        {

            if (grpConLedgerHeader.Visible)
            {
                grpConLedgerHeader.SortingSummary.Enabled = true;
                grpConLedgerHeader.SortingSummary.FieldName = "CON_LEDGER_CODE";
                grpConLedgerHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpConLedgerHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (grpParentGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpParentGroup.SortingSummary.FieldName = "PARENT_GROUP_CODE";
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpParentGroup.SortingSummary.Enabled = true;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpParentGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpParentGroup.SortingSummary.FieldName = "PARENT_GROUP_CODE";
                    grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // GROUP_CODE
                    grpLedgerGroup.SortingSummary.FieldName = "GROUP_CODE";
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //On 03/04/2020, to keep ledger group second leavel proper order 
                    //grpReceiptGroup.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_GROUP
                    grpLedgerGroup.SortingSummary.FieldName = "GROUP_CODE";
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            if (grpLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger== 0)
                {
                    grpLedger.SortingSummary.Enabled = true;
                    grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedger.SortingSummary.Enabled = true;
                    grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }

            //On 10/05/2019, To remove ledger group (it is already grouped in sql itself and to have proper ledger code sorting)
            //Detail.SortFields.Clear();
            //if (this.ReportProperties.SortByLedger == 0)
            //{
            //    Detail.SortFields.Add(new GroupField("LEDGER_CODE", XRColumnSortOrder.Ascending));
            //    //Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            //}
            //else
            //{
            //    //Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            //    Detail.SortFields.Add(new GroupField("LEDGER_CODE", XRColumnSortOrder.Ascending));
            //    //Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            //}

            //if (grpReceiptLedger.Visible)
            //{
            //    if (this.ReportProperties.SortByLedger == 0)
            //    {
            //        grpReceiptLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
            //            grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        else
            //        {
            //            grpReceiptLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //        }
            //        grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //    else
            //    {
            //        grpReceiptLedger.SortingSummary.Enabled = true;
            //        if (this.ReportProperties.ShowByLedgerGroup == 1)
            //        {
            //            grpReceiptLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
            //            grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        else
            //        {
            //            grpReceiptLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //        }
            //        grpReceiptLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //        grpReceiptLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //    }
            //}

            //if (grpcostCenterCategory.Visible)
            //{
            //    grpcostCenterCategory.SortingSummary.Enabled = true;
            //    grpcostCenterCategory.SortingSummary.FieldName = "COST_CENTRE_CATEGORY_NAME";
            //    grpcostCenterCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCenterCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (grpCostCentreNameReceipts.Visible)
            //{
            //    grpcostCenterCategory.SortingSummary.Enabled = true;
            //    grpcostCenterCategory.SortingSummary.FieldName = "COST_CENTRE_NAME";
            //    grpcostCenterCategory.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpcostCenterCategory.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }
        #endregion

        #region Events

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

        #endregion

       
       
    }
}

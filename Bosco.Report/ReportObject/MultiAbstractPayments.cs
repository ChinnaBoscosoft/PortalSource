using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using System.Linq;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        public MultiAbstractPayments()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiAbstractPayment.WidthF);
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindMultiAbstractPaymentSource();
                base.ShowReport();
            }
            // base.ShowReport();
        }

        #endregion

        public void BindMultiAbstractPaymentSource()
        {
            //  this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            // this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;

            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "MultiAbstract";
                xrPGMultiAbstractPayment.DataSource = dvPayment;
                xrPGMultiAbstractPayment.DataMember = dvPayment.Table.TableName;

            }

            AccountBalanceMulti accountBalanceMulti = xrSubBalanceMulti.ReportSource as AccountBalanceMulti;
            SetReportSetting(dvPayment, accountBalanceMulti);
            accountBalanceMulti.BindBalance(false);
        }

        private void BindGrandTotal(DataTable dtGrantTotal)
        {
            AccountBalanceMulti accountBalanceMulti = xrSubBalanceMulti.ReportSource as AccountBalanceMulti;
            DataTable dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            int rowIdx = 0;
            double amount = 0;

            foreach (DataRow drGrantTotalBal in dtGrantTotalBalance.Rows)
            {
                rowIdx = dtGrantTotalBalance.Rows.IndexOf(drGrantTotalBal);

                if (rowIdx < dtGrantTotal.Rows.Count)
                {
                    DataRow drGrantTotal = dtGrantTotal.Rows[rowIdx];
                    amount = this.UtilityMember.NumberSet.ToDouble(drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());
                    amount += this.UtilityMember.NumberSet.ToDouble(drGrantTotalBal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());

                    drGrantTotal.BeginEdit();
                    drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = amount;
                    drGrantTotal.EndEdit();
                }
            }

            dtGrantTotal.AcceptChanges();
            dtGrantTotal.TableName = "MultiAbstract";
            xrPGGrandTotal.DataSource = dtGrantTotal;
            xrPGGrandTotal.DataMember = dtGrantTotal.TableName;
        }

        //private DataTable RemoveEmptyRow(DataTable dt)
        //{
        //    string LedgerCode = string.Empty;
        //    DataTable dtPayment = dt;
        //    DataView dvReport = new DataView(dtPayment);
        //    dvReport.Sort = "LEDGER_NAME  DESC";
        //    DataTable dtSorted = dvReport.ToTable();

        //    DataTable dtLedgerAmount = dtSorted.AsEnumerable().GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();
        //    DataTable dtFilter = new DataTable();
        //    foreach (DataRow dr in dtLedgerAmount.Rows)
        //    {
        //        dvReport.RowFilter = String.Format("{0}='{1}'", reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName, dr[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName].ToString());
        //        dtFilter = dvReport.ToTable();

        //        var IsEmptyRow = (from report in dtFilter.AsEnumerable()
        //                          where ((report.Field<Decimal?>(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName) > 0))
        //                          select report);

        //        if (IsEmptyRow.Count() <= 0)
        //        {
        //            LedgerCode += dr[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName].ToString() + ',';
        //        }
        //    }
        //    dvReport.RowFilter = String.Format("{0} NOT IN({1})", reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName, LedgerCode.TrimEnd(','));
        //    if (dvReport != null)
        //        dtPayment = dvReport.ToTable();
        //    return dtPayment;
        //}

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractPayments = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstract);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Report.MultiAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.DR.ToString());
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMultiAbstractPayments);
            }

            return resultArgs;
        }

        private void SortByLedgerorGroup()
        {

            //if (this.ReportProperties.SortByGroup == 0)
            //{
            //    DetailReport1.SortingSummary.Enabled = true;
            //    grpLedgerGroup.SortingSummary.FieldName = "GROUP_CODE";
            //    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    grpLedgerGroup.SortingSummary.Enabled = true;
            //    grpLedgerGroup.SortingSummary.FieldName = "LEDGER_GROUP";
            //    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}

            //if (this.ReportProperties.SortByLedger == 0)
            //{
            //    grpLedger.SortingSummary.Enabled = true;
            //    grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
            //    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    grpLedger.SortingSummary.Enabled = true;
            //    grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
            //    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
            //    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            //}
        }

        private void xrPGMultiAbstractPayment_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldMONTHNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_YEARColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGMultiAbstractPayment_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }

        private void xrPGMultiAbstractPayment_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                    || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    //e.Field.RowValueLineCount = 2;
                    //DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                    //textBrick.Size = new SizeF(textBrick.Size.Width, 300);
                }
                else if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    // e.Appearance.ForeColor = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;
                }
            }
        }

        private void xrPGGrandTotal_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldGRANTTOTALPARTICULARS.Name)
                {
                    e.Appearance.BackColor = xrPGGrandTotal.Styles.GrandTotalCellStyle.BackColor;
                }
                else if (e.Field.Name == fieldGRANTTOTALMONTH.Name)
                {
                    if (xrPGGrandTotal.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;

                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        //e.Field.Options.ShowValues = false;
                    }
                }
            }
        }

        private void xrPGMultiAbstractPayment_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //  e.Appearance.ForeColor = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;


            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
            }
        }

        private void xrPGMultiAbstractPayment_AfterPrint(object sender, EventArgs e)
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.MONTHColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName, typeof(double));
            object oTotVal = null;
            double totVal = 0;
            int row = xrPGMultiAbstractPayment.RowCount - 1;

            for (int col = 0; col < xrPGMultiAbstractPayment.ColumnCount; col++)
            {
                oTotVal = xrPGMultiAbstractPayment.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.MONTHColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            BindGrandTotal(dtGrantTotal);
        }

        private void SetReportSetting(DataView dvPayment, AccountBalanceMulti accountBalanceMulti)
        {
            fieldGROUPCODE.Width = 35;
            fieldLEDGERGROUP.Width = 90;
            fieldLEDGERCODE.Width = 35;
            fieldLEDGERNAME.Width = 130;
            fieldMONTHNAME.Width = 70;

            try { fieldGROUPCODE.Visible = true; }
            catch { }
            try { fieldLEDGERGROUP.Visible = true; }
            catch { }
            try { fieldLEDGERCODE.Visible = true; }
            catch { }
            try { fieldLEDGERNAME.Visible = true; }
            catch { }

            fieldGROUPCODE.AreaIndex = 0;
            fieldLEDGERGROUP.AreaIndex = 1;
            fieldLEDGERCODE.AreaIndex = 2;
            fieldLEDGERNAME.AreaIndex = 3;

            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            string rowFilterItem = "";
            string ledgerCodeDefault = "";
            string ledgerCode = "";
            string lastLedgerCode = "";

            //Attach / Detach all ledgers
            dvPayment.RowFilter = "";

            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    DataView dvFilter = dvPayment;

            //    //if (this.ReportProperties.SortByGroup == 0 && this.ReportProperties.SortByLedger == 0)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.GROUP_CODEColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByGroup == 1 && this.ReportProperties.SortByLedger == 1)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByLedger == 1 && this.ReportProperties.SortByGroup == 0)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + "," + reportSetting1.MultiAbstract.GROUP_CODEColumn.ColumnName;
            //    //}
            //    //else if (this.ReportProperties.SortByLedger == 0 && this.ReportProperties.SortByGroup == 1)
            //    //{
            //    //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName + "," + reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName;
            //    //}
            //    dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //    try
            //    {
            //        if (dvFilter.ToTable() != null && dvPayment.ToTable() != null)
            //        {
            //            dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
            //            //Applying Group by LEDGER_NAME
            //            DataTable dtMappedLedger = dvFilter.ToTable().AsEnumerable().
            //                GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();

            //            DataTable dtSource = dvPayment.ToTable();

            //            //Joining Two table on LEDGER_NAME wiht Amount >0 and get all those records
            //            var LedgerName = (from s in dtMappedLedger.AsEnumerable()
            //                              join m in dtSource.AsEnumerable() on s.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
            //                              equals m.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
            //                              where m.Field<Decimal?>(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName) > 0
            //                              select m);
            //            if (LedgerName.Count() > 0)
            //            {
            //                //Applying Group by LEDGER_NAME
            //                DataTable dtLedgerList = LedgerName.CopyToDataTable().AsEnumerable().GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();
            //                rowFilterItem = this.GetCommaSeparatedValue(dtLedgerList, reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName);
            //                dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + "  IN (" + rowFilterItem + ")";
            //            }
            //            else
            //            {
            //                DataRow dr = dtSource.AsEnumerable().FirstOrDefault();
            //                dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + " IN ('" + dr[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] + "')";
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageRender.ShowMessage(ex.Message);
            //    }
            //}


            //Include / Exclude Code
            try { fieldGROUPCODE.Visible = (isGroupCodeVisible); }
            catch { }
            try { fieldLEDGERGROUP.Visible = isGroupVisible; }
            catch { }
            try { fieldLEDGERCODE.Visible = (isLedgerCodeVisible); }
            catch { }
            try { fieldLEDGERNAME.Visible = isLedgerVisible; }
            catch { }

            //Grant Total Grid
            int rowWidth = 0;
            xrPGGrandTotal.OptionsView.ShowRowHeaders = false;
            xrPGGrandTotal.LeftF = xrPGMultiAbstractPayment.LeftF;
            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALMONTH.Width = fieldMONTHNAME.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }

            //Set Subreport Properties
            xrSubBalanceMulti.LeftF = xrPGMultiAbstractPayment.LeftF;
            accountBalanceMulti.LeftPosition = (xrPGMultiAbstractPayment.LeftF - 5);
            accountBalanceMulti.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMulti.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
            accountBalanceMulti.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMulti.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMulti.AmountColumnWidth = fieldMONTHNAME.Width;
            accountBalanceMulti.ShowColumnHeader = false;
        }

        private void xrPGGrandTotal_PrintHeader(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs e)
        {

        }
    }
}

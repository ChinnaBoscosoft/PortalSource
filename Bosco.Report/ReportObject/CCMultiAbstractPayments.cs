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
    public partial class CCMultiAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        public CCMultiAbstractPayments()
        {
            InitializeComponent();
            this.SetTitleWidth(xrCosPGMultiAbstractPayment.WidthF);
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                this.SetLandscapeHeader = 1115.25f;
                this.SetLandscapeFooter = 1115.25f;
                this.SetLandscapeFooterDateWidth = 970.00f;
                //this.SetLandscapeCostCentreWidth = 1110.25f;
                BindMultiAbstractPaymentSource();
                base.ShowReport();
            }
        }

        #endregion

        public void BindMultiAbstractPaymentSource()
        {
            //this.ReportTitle = ReportProperty.Current.ReportTitle;
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = objReportProperty.CostCentreName;
            // this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;

            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "MultiAbstract";
                xrCosPGMultiAbstractPayment.DataSource = dvPayment;
                xrCosPGMultiAbstractPayment.DataMember = dvPayment.Table.TableName;
            }

            SetReportSetting(dvPayment);
        }


        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractPayments = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.MultiAbstract);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CostCentre.MultiAbstract, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMultiAbstractPayments);
            }

            return resultArgs;
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
                if (e.Field.Name == fieldCosLEDGERCODE.Name || e.Field.Name == fieldCosLEDGERNAME.Name
                    || e.Field.Name == fieldCosGROUPCODE.Name || e.Field.Name == fieldCosLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    //e.Field.RowValueLineCount = 2;
                    //DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                    //textBrick.Size = new SizeF(textBrick.Size.Width, 300);
                }
                else if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrCosPGMultiAbstractPayment.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrCosPGMultiAbstractPayment.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrCosPGMultiAbstractPayment.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    // e.Appearance.ForeColor = xrCosPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrCosPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;
                }
            }
        }

        private void xrPGMultiAbstractPayment_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                // e.Appearance.ForeColor = xrCosPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrCosPGMultiAbstractPayment.Styles.HeaderGroupLineStyle.Font;
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
            int row = xrCosPGMultiAbstractPayment.RowCount - 1;

            for (int col = 0; col < xrCosPGMultiAbstractPayment.ColumnCount; col++)
            {
                oTotVal = xrCosPGMultiAbstractPayment.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.MONTHColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            //  BindGrandTotal(dtGrantTotal);
        }

        private void SetReportSetting(DataView dvPayment)
        {
            fieldCosGROUPCODE.Width = 35;
            fieldCosLEDGERGROUP.Width = 90;
            fieldCosLEDGERCODE.Width = 35;
            fieldCosLEDGERNAME.Width = 130;
            fieldMONTHNAME.Width = 70;

            try { fieldCosGROUPCODE.Visible = true; }
            catch { }
            try { fieldCosLEDGERGROUP.Visible = true; }
            catch { }
            try { fieldCosLEDGERCODE.Visible = true; }
            catch { }
            try { fieldCosLEDGERNAME.Visible = true; }
            catch { }

            fieldCosGROUPCODE.AreaIndex = 0;
            fieldCosLEDGERGROUP.AreaIndex = 1;
            fieldCosLEDGERCODE.AreaIndex = 2;
            fieldCosLEDGERNAME.AreaIndex = 3;

            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);

            string rowFilterItem = "";
            string ledgerCodeDefault = "";
            string ledgerCode = "";
            string lastLedgerCode = "";

            //Attach / Detach all ledgers
            dvPayment.RowFilter = "";

            if (ReportProperties.IncludeAllLedger == 0)
            {
                //dvPayment.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
                DataView dvFilter = dvPayment;
                dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
                try
                {
                    if (dvFilter.ToTable() != null && dvPayment.ToTable() != null)
                    {
                        dvFilter.Sort = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName;
                        //Applying Group by LEDGER_NAME
                        DataTable dtMappedLedger = dvFilter.ToTable().AsEnumerable().
                            GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();

                        DataTable dtSource = dvPayment.ToTable();

                        //Joining Two table on LEDGER_NAME wiht Amount >0 and get all those records
                        var LedgerName = (from s in dtMappedLedger.AsEnumerable()
                                          join m in dtSource.AsEnumerable() on s.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
                                          equals m.Field<string>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)
                                          where m.Field<Decimal?>(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName) > 0
                                          select m);
                        if (LedgerName.Count() > 0)
                        {
                            //Applying Group by LEDGER_NAME
                            DataTable dtLedgerList = LedgerName.CopyToDataTable().AsEnumerable().GroupBy(r => r.Field<String>(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName)).Select(g => g.First()).CopyToDataTable();
                            rowFilterItem = GetCommaSeparatedValue(dtLedgerList, reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName);
                            dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + "  IN (" + rowFilterItem + ")";
                        }
                        else
                        {
                            DataRow dr = dtSource.AsEnumerable().FirstOrDefault();
                            dvPayment.RowFilter = reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName + " IN ('" + dr[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] + "')";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageRender.ShowMessage(ex.Message);
                }
            }

            //Include / Exclude Code
            try { fieldCosGROUPCODE.Visible = (isGroupCodeVisible); }
            catch { }
            try { fieldCosLEDGERGROUP.Visible = isGroupVisible; }
            catch { }
            try { fieldCosLEDGERCODE.Visible = (isLedgerCodeVisible); }
            catch { }
            try { fieldCosLEDGERNAME.Visible = isLedgerVisible; }
            catch { }

            //Grant Total Grid
            int rowWidth = 0;
            if (fieldCosGROUPCODE.Visible) { rowWidth = fieldCosGROUPCODE.Width; }
            if (fieldCosLEDGERGROUP.Visible) { rowWidth += fieldCosLEDGERGROUP.Width; }
            if (fieldCosLEDGERCODE.Visible) { rowWidth += fieldCosLEDGERCODE.Width; }
            if (fieldCosLEDGERNAME.Visible) { rowWidth += fieldCosLEDGERNAME.Width; }

            //Grid Lines
            if (isHorizontalLine)
            {
                xrCosPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrCosPGMultiAbstractPayment.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrCosPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrCosPGMultiAbstractPayment.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }


        }
    }
}

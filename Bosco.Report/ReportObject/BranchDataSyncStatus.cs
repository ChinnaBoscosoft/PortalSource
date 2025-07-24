using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BranchDataSyncStatus : Bosco.Report.Base.ReportHeaderBase
    {
        public BranchDataSyncStatus()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1045.25f;
            this.SetLandscapeFooter = 1045.25f;
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
                || String.IsNullOrEmpty(this.ReportProperties.BranchOffice))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindBranchDataStatusSoure();
            }
            base.ShowReport();
        }

        #endregion

        public void BindBranchDataStatusSoure()
        {
            DataTable dtDataStatus = new DataTable();
            setHeaderTitleAlignment();
            this.SetLandscapeFooterDateWidth = 880.25f;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            DataView dvStatus = resultArgs.DataSource.TableView;
            if (resultArgs.Success && dvStatus != null)
            {
                dtDataStatus.TableName = "BranchDataStatus";
                dtDataStatus = dvStatus.ToTable();
                xrPGDataStatus.DataSource = dtDataStatus;
                xrPGDataStatus.DataMember = dtDataStatus.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlBranchDataStatus = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.FetchBranchDatastatus);

            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.FetchBranchDatastatus, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlBranchDataStatus);
            }

            return resultArgs;
        }

        private void xrPGDataStatus_AfterPrint(object sender, EventArgs e)
        {

        }

        private void xrPGDataStatus_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldMONTHNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting2.BranchDataStatus.MONTH_NAMEColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting2.BranchDataStatus.MONTH_NAMEColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGDataStatus_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }

        private void xrPGDataStatus_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                e.Appearance.ForeColor = xrPGDataStatus.Styles.HeaderGroupLineStyle.ForeColor;
                e.Appearance.Font = xrPGDataStatus.Styles.HeaderGroupLineStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
            }
        }

        private void xrPGDataStatus_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                if (e.Field.Name == fieldMONTHNAME.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGDataStatus.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGDataStatus.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGDataStatus.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    e.Appearance.ForeColor = xrPGDataStatus.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGDataStatus.Styles.HeaderGroupLineStyle.Font;
                }
            }
        }

    }
}

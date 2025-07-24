using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class MonthWiseLedgerReport : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor

        public MonthWiseLedgerReport()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGBranchLedgerComaparative.WidthF);
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            ShowMonthWiseLedgerComparativeReport();
            base.ShowReport();
        }

        #endregion

        #region Declaration

        #endregion

        #region Methods

        public void ShowMonthWiseLedgerComparativeReport()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                //this.SetLandscapeHeader = 1000.25f;
                //this.SetLandscapeFooter = 1000.25f;
                //this.SetLandscapeFooterDateWidth = 835.25f;
                this.SetLandscapeHeader = 1280.25f;
                this.SetLandscapeFooter = 1280.25f;
                this.SetLandscapeFooterDateWidth = 835.25f;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                lblLedger.Text = ReportProperties.LedgerName;
                lblLedgerGroup.Text = ReportProperties.LedgerGroupName;
                setHeaderTitleAlignment();
                SetReportTitle();

                this.ReportSubTitle = string.Empty;
                this.ReportBranchName = string.Empty;
                this.CosCenterName = string.Empty;

                ResultArgs resultArgs = GetReportSource();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "MultiAbstract";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.MonthWiseLedgerComparative);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.MonthWiseLedgerComparative, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperties.Ledger) && ReportProperties.Ledger != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractPayments);
            }
            return resultArgs;
        }

        #endregion

        #region Events

        private void xrPGBranchLedgerComaparative_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total";
            }
        }

        #endregion

        private void xrPGBranchLedgerComaparative_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == xrPivotGridField2.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MONTH_NAME").ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MONTH_NAME").ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }
    }
}

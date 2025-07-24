using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class BranchComparativeReport : ReportHeaderBase
    {       
        public BranchComparativeReport()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGBranchComaparative.WidthF);
        }
        public override void ShowReport()
        {
           ShowComparativeReport();
            base.ShowReport();
        }
        public void ShowComparativeReport()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
               
                ShowReportFilterDialog();
            }
            else
            {
                this.ReportAmountLakh = true; 
                this.SetLandscapeHeader = 1000.25f;
                this.SetLandscapeFooter = 1000.25f;
                this.SetLandscapeFooterDateWidth = 835.25f;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                setHeaderTitleAlignment();
                SetReportTitle();

                this.ReportBranchName = string.Empty;
                this.ReportSubTitle = string.Empty;
                
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
            string sqlMultiAbstractPayments = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BranchWiseIncomeExpense);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BranchWiseIncomeExpense, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_FROM_TRANSFER_IDColumn, this.LoginUser.InterAccountFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.INTER_AC_TO_TRANSFER_IDColumn, this.LoginUser.InterAccountToLedgerIds);
                
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractPayments);
            }

            return resultArgs;
        }

        private void xrPGBranchComaparative_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == xrMonthName.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    DateTime dt1 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    DateTime dt2 = DateTime.Parse(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.MONTH_NAMEColumn.ColumnName).ToString());
                    e.Result = Comparer.Default.Compare(dt1, dt2);
                    e.Handled = true;
                }
            }
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class HOOfficeLedgersTransGreater10000 : Bosco.Report.Base.ReportHeaderBase
    {
        #region variable
        double AmountValue = 0;
        #endregion
        #region Constructor

        public HOOfficeLedgersTransGreater10000()
        {
            InitializeComponent();
        }

        #endregion

        #region Show Reports

        public override void ShowReport()
        {
            SetReportTitle();
            FetchBranchwithProjectwiseLedgerTransactions();
            // this.HideDateRange = false;
            base.ShowReport();
        }

        #endregion

        #region Methods

        public void FetchBranchwithProjectwiseLedgerTransactions()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                //this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                //this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                //setHeaderTitleAlignment();
                //SetReportTitle();

                ResultArgs resultArgs = GetReportSource();
                DataView dvLedgers = resultArgs.DataSource.TableView;

                //  this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                //this.ReportSubTitle = string.Empty;
                //this.ReportBranchName = string.Empty;
                // this.CosCenterName = string.Empty;

                if (dvLedgers != null)
                {
                    dvLedgers.Table.TableName = "BranchReports";
                    this.DataSource = dvLedgers;
                    this.DataMember = dvLedgers.Table.TableName;
                }
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlBranchProjectwiseLedgers = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.FetchBranchProjectLedger);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.FetchBranchProjectLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);

                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);

                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                if (this.ReportProperties.ShowAllCash == 0)
                    dataManager.Parameters.Add(this.ReportParameters.AMOUNTColumn, 0);
                else
                    dataManager.Parameters.Add(this.ReportParameters.AMOUNTColumn, 10000);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlBranchProjectwiseLedgers);

                if (resultArgs.DataSource.TableView.Table != null && resultArgs.DataSource.TableView.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.TableView.Table.AsEnumerable().OrderBy(en => en.Field<UInt32>("BRANCH_ID")).CopyToDataTable();
                    resultArgs.DataSource.TableView.Table.AcceptChanges();
                }
            }
            return resultArgs;
        }

        #endregion

        private void xrTableCell2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = AmountValue;
            e.Handled = true;
        }

        private void xrTableCell2_SummaryReset(object sender, EventArgs e)
        {
            AmountValue = 0;
        }

        private void xrTableCell2_SummaryRowChanged(object sender, EventArgs e)
        {
            AmountValue += (GetCurrentColumnValue(this.BranchProjectParameters.AMOUNTColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.BranchProjectParameters.AMOUNTColumn.ColumnName).ToString());
        }
    }
}

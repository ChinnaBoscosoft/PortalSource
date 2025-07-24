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
    public partial class BranchExportVoucher : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public BranchExportVoucher()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            this.SetLandscapeHeader = 1066.25f;
            this.SetLandscapeFooter = 1068.25f;
            this.SetLandscapeFooterDateWidth = 900.00f;

            SetReportTitle();
            FetchHeadOfficeLedgers();
            this.HideDateRange = false;
            base.ShowReport();
        }
        #endregion
        #region Methods

        public void FetchHeadOfficeLedgers()
        {
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataView dvBranchExportVoucher = resultArgs.DataSource.TableView;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportSubTitle = string.Empty;
                this.ReportBranchName = string.Empty;
                this.CosCenterName = string.Empty;

                if (dvBranchExportVoucher != null)
                {
                    if (dvBranchExportVoucher.Count == 0)
                    {
                        xrTableValue.Visible = false;
                    }
                    dvBranchExportVoucher.RowFilter = this.reportSetting1.BranchExportVoucher.BRANCH_OFFICE_CODEColumn.ColumnName + " IN (" + this.ReportProperties.BranchOfficeCode + ")";
                    dvBranchExportVoucher.Table.TableName = "BranchExportVoucher";
                    this.DataSource = dvBranchExportVoucher;
                    this.DataMember = dvBranchExportVoucher.Table.TableName;
                }
            }
            else
            {
                resultArgs.ShowMessage(resultArgs.Message);
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = new ResultArgs();
            if (!string.IsNullOrEmpty(this.LoginUser.LoginUserHeadOfficeCode))
            {
                string sqlbranchexportvoucher = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.BranchExportVoucher);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.BranchExportVoucher, DataBaseType.Portal))
                {
                    //dataManager.Parameters.Add(this.reportSetting1.BranchExportVoucher.BRANCH_OFFICE_CODEColumn, this.ReportProperties.BranchOfficeCode);
                    dataManager.Parameters.Add(this.reportSetting1.BranchExportVoucher.HEAD_OFFICE_CODEColumn, this.LoginUser.LoginUserHeadOfficeCode);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlbranchexportvoucher);
                }
            }
            return resultArgs;
        }
        #endregion
    }
}

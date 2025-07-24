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
    public partial class HeadOfficeBranchOffice : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public HeadOfficeBranchOffice()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            SetReportTitle();
            FetchHeadofficeProject();
            this.HideDateRange = false;
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void FetchHeadofficeProject()
        {
            ResultArgs resultArgs = GetReportSource();
            DataView dvLedgers = resultArgs.DataSource.TableView;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.ReportSubTitle = string.Empty;
            this.ReportBranchName = string.Empty;
            this.CosCenterName = string.Empty;

            if (dvLedgers != null)
            {
                dvLedgers.Table.TableName = "BranchReports";
                this.DataSource = dvLedgers;
                this.DataMember = dvLedgers.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.FetchHeadOfficeProject);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.FetchHeadOfficeProject, DataBaseType.HeadOffice))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }
            return resultArgs;
        }
        #endregion
    }
}

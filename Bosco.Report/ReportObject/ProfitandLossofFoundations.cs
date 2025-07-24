using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ProfitandLossofFoundations : Bosco.Report.Base.ReportHeaderBase
    {
        public ProfitandLossofFoundations()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1065.25f;
            this.SetLandscapeFooter = 1065.25f;
            xrTableHeader.WidthF = 1065.25f;
            xrTableDetail.WidthF = 1065.25f;
            xrtableFooter.WidthF = 1065.25f;
            this.SetLandscapeFooterDateWidth = 900.00f;
        }

        #region ShowReports
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
                BindSource();
            }
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindSource()
        {

            this.ReportTitle = objReportProperty.ReportTitle;
            SetReportTitle();
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success && resultArgs.DataSource != null)
            {
                DataView dvLedgers = resultArgs.DataSource.Table.AsDataView();
                if (dvLedgers != null)
                {
                    dvLedgers.Table.TableName = "ProfitandLossbyHouse";
                    this.DataSource = dvLedgers;
                    this.DataMember = dvLedgers.Table.TableName;
                }
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.ProfitandLossbyFoundationWise);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.ProfitandLossbyFoundationWise, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceipts);
            }
            return resultArgs;
        }
        #endregion
    }
}

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

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreSummary : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public CostCentreSummary()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindReport();
        }
        #endregion

        #region Methods
        public void BindReport()
        {
            if ((string.IsNullOrEmpty(this.ReportProperties.DateFrom) ||
                string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0"))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                setHeaderTitleAlignment();
                SetReportTitle();
                this.SetLandscapeHeader = 668.25f;
                this.SetLandscapeFooter = 668.25f;
                xrtblHeaderCaption.WidthF = xrtblCCCName.WidthF = xrtblDetails.WidthF = xrtblGrandTotal.WidthF = xrTable1.WidthF = 668.25f;
                this.CosCenterName = objReportProperty.CostCentreName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                grpCostCategoryName.Visible = grpFooterBalance.Visible = this.ReportProperties.ShowByCostCentreCategory == 1;
                ResultArgs resultArgs = BindCCBalanceSource();
                DataView dtview = resultArgs.DataSource.TableView;
                if (dtview != null)
                {
                    dtview.Table.TableName = "Ledger";
                    this.DataSource = dtview;
                    this.DataMember = dtview.Table.TableName;
                }
                base.ShowReport();
            }
        }

        public ResultArgs BindCCBalanceSource()
        {
            ResultArgs resultArgs = null;
            string Query = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreSummary);
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                if (!string.IsNullOrEmpty(objReportProperty.BranchOffice) && objReportProperty.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
            }
            return resultArgs;
        }
        #endregion
    }
}

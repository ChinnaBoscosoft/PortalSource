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
    public partial class InstitutionDetails : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public InstitutionDetails()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1050.25f;
            this.SetLandscapeFooter = 1050.25f;
        }
        #endregion

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
            xrTableHeader.WidthF = xrTableDetails.WidthF = xrTable1.WidthF = 1040.25f;
            this.SetReportDate = this.ReportProperties.ReportDate;
            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;
            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "Generalate";
                this.DataSource = dvPayment;
                this.DataMember = dvPayment.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            DateTime dateAsOn = this.UtilityMember.DateSet.ToDate(objReportProperty.DateFrom.ToString(), false);
            dateAsOn = dateAsOn.AddDays(-1);
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetMasterSQL(SQL.ReportSQLCommand.Masters.DetailsOfAllInstitution);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Masters.DetailsOfAllInstitution, DataBaseType.HeadOffice))
            {
                string DivisionId="1,2";
                dataManager.Parameters.Add(this.ReportParameters.DIVISION_IDColumn, DivisionId);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.SUBSIDY_LEDGER_IDColumn,this.ReportProperties.LedgerGroup);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_LEDGER_IDColumn,this.ReportProperties.Ledger);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }
            return resultArgs;
        }
        #endregion
    }
}

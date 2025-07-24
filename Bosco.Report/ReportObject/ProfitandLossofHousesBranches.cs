using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class ProfitandLossofHousesBranches : Bosco.Report.Base.ReportHeaderBase
    {
        double FinalResult = 0;

        #region Constructor

        public ProfitandLossofHousesBranches()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReports

        public override void ShowReport()
        {
            FinalResult = 0;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo))
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
            DataView dvProfitandLoss = resultArgs.DataSource.TableView;
            if (dvProfitandLoss != null)
            {
                dvProfitandLoss.Table.TableName = "ProfitandLossbyHouse";
                this.DataSource = dvProfitandLoss;
                this.DataMember = dvProfitandLoss.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            //this.ReportProperties.Project = this.GetSocietyProjectIds();
            //--------------------------------------------------------

            ResultArgs resultArgs = null;
            string sqlProfitandLoss = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.ProfitandLossbyBranchHousewise);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.ProfitandLossbyBranchHousewise, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                //dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                //dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataView, sqlProfitandLoss);
            }
            return resultArgs;
        }

        #endregion

        private void xrResult_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue("RECEIPT") != null && GetCurrentColumnValue("PAYMENT") != null)
            {
                Double incomeAmt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("RECEIPT").ToString());
                Double expenseAmt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("PAYMENT").ToString());
                double result = incomeAmt - expenseAmt;
                FinalResult += result;
                cell.Text = UtilityMember.NumberSet.ToNumber(result);
            }
        }

        private void xrSumResult_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(FinalResult);
        }
    }
}

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
    public partial class GeneralateProfitandLoss : Bosco.Report.Base.ReportHeaderBase
    {
        //private string ContributionFromProvince = "Contribution from Province";
        //private string ContributionToProvince = "Contribution to Province";
        //private int ContributionFrom = 0;
        //private int ContributionTo = 0;

        public GeneralateProfitandLoss()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1060.25f;
            this.SetLandscapeFooter = 1060.25f;
            xrTableHeader.WidthF = 1060.25f;
            this.SetLandscapeFooterDateWidth = 900.00f;
        }

        private bool isGBVerification
        {
            get
            {
                return (this.ReportProperties.ReportId == "RPT-078");
            }
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
            DataTable dtPLIncomeDetails = new DataTable(); ;
            DataTable dtPLExpenseDetails = new DataTable(); ;

            Double totalLoss = 0;
            Double totalProfit = 0;

            DateTime date = Convert.ToDateTime(ReportProperties.DateFrom);
            date = Convert.ToDateTime(ReportProperties.DateTo);
            tcExpenditureYearTo.Text = "Amount";
            tcIncomeYearTo.Text = "Amount";

            this.ReportTitle = objReportProperty.ReportTitle;
            SetReportTitle();
            this.SetReportDate = this.ReportProperties.ReportDate;

            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtGenerlatePL = resultArgs.DataSource.Table;

                //For PL Detail by Con Ledger Details -----------------------------------------------
                if (isGBVerification)
                {
                    if (this.ReportProperties.ShowDetailedBalance == 1)
                    {
                        resultArgs = GetReportSourcePLDetailHOLedger();
                    }
                    else
                    {
                        resultArgs = GetReportSourcePLDetailConLedger();
                    }

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtGeneralatePLRptDetails = resultArgs.DataSource.Table;
                        //dtGeneralatePLRptDetails.Columns.Add("FINAL", dtGeneralatePLRptDetails.Columns["AMOUNT"].DataType, "AMOUNT * -1");
                        dtPLExpenseDetails = dtGeneralatePLRptDetails.DefaultView.ToTable();
                        dtPLIncomeDetails = dtGeneralatePLRptDetails.DefaultView.ToTable();

                        //For Expense Data
                        dtPLExpenseDetails.Columns.Add("FINAL", dtGeneralatePLRptDetails.Columns["AMOUNT"].DataType, "AMOUNT * -1");
                        //dtGeneralatePLRptDetails.DefaultView.RowFilter = "AMOUNT<0";
                        //dtPLExpenseDetails = dtGeneralatePLRptDetails.DefaultView.ToTable();
                        //dtPLExpenseDetails.Columns.Add("FINAL", dtGeneralatePLRptDetails.Columns["AMOUNT"].DataType, "AMOUNT * -1");

                        //For Income Data
                        dtPLIncomeDetails.Columns["AMOUNT"].ColumnName = "FINAL";
                        //dtGeneralatePLRptDetails.DefaultView.RowFilter = string.Empty;
                        //dtGeneralatePLRptDetails.DefaultView.RowFilter = "AMOUNT>0";
                        //dtPLIncomeDetails = dtGeneralatePLRptDetails.DefaultView.ToTable();
                        //dtPLIncomeDetails.Columns["AMOUNT"].ColumnName = "FINAL";

                        if (this.ReportProperties.ShowDetailedBalance == 1)
                        {
                            dtPLExpenseDetails.Columns["LEDGER_NAME"].ColumnName = "SOCIETYNAME";
                            dtPLIncomeDetails.Columns["LEDGER_NAME"].ColumnName = "SOCIETYNAME";
                        }
                    }
                }

                //Loss Data
                //dtGenerlatePL.DefaultView.RowFilter = "AMOUNT<0";
                dtGenerlatePL.DefaultView.RowFilter = "MASTER_LEDGER_CODE LIKE '%C%'";
                DataTable dtGeneralateLoss = dtGenerlatePL.DefaultView.ToTable();
                dtGeneralateLoss.Columns["AMOUNT"].ColumnName = "ACTUAL_AMOUNT";
                //Change negative value to positive
                dtGeneralateLoss.Columns.Add("AMOUNT", dtGeneralateLoss.Columns["ACTUAL_AMOUNT"].DataType, "ACTUAL_AMOUNT * -1");
                dtGeneralateLoss.DefaultView.RowFilter = "AMOUNT<>0";
                dtGeneralateLoss = dtGeneralateLoss.DefaultView.ToTable();
                if (dtGeneralateLoss.Rows.Count > 0)
                {
                    totalLoss = UtilityMember.NumberSet.ToDouble(dtGeneralateLoss.Compute("SUM(AMOUNT)", string.Empty).ToString());
                }

                //Profit Data
                dtGenerlatePL.DefaultView.RowFilter = string.Empty;
                //dtGenerlatePL.DefaultView.RowFilter = "AMOUNT>0";
                dtGenerlatePL.DefaultView.RowFilter = "MASTER_LEDGER_CODE LIKE '%D%'";
                DataTable dtGeneralateProfit = dtGenerlatePL.DefaultView.ToTable();
                dtGeneralateProfit.DefaultView.RowFilter = "AMOUNT<>0";
                dtGeneralateProfit = dtGeneralateProfit.DefaultView.ToTable();

                if (dtGeneralateLoss.Rows.Count > 0)
                {
                    totalProfit = UtilityMember.NumberSet.ToDouble(dtGeneralateProfit.Compute("SUM(AMOUNT)", string.Empty).ToString());
                }

                GeneralateLoss loss = xrsrLoss.ReportSource as GeneralateLoss;
                loss.BindGeneralateLoss(dtGeneralateLoss, dtPLExpenseDetails);  //loss.FetchLoss();
                xrExpencenxtSum.Text = UtilityMember.NumberSet.ToNumber(totalLoss);

                GeneralateProfit profit = xrsrincome.ReportSource as GeneralateProfit;
                profit.BindGeneralateProfit(dtGeneralateProfit, dtPLIncomeDetails); //profit.FetchProfit();
                xrIncomenxtSum.Text = UtilityMember.NumberSet.ToNumber(totalProfit).ToString();

                xrtcNetresultnext.Text = UtilityMember.NumberSet.ToNumber(totalProfit - totalLoss);
            }

            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = new ResultArgs();
            //ContributionFrom = GetContriFromProvinceId();
            //ContributionTo = GetContriToProvinceId();

            //Get Projects for selected projects ---------------------
           // this.ReportProperties.Project = this.GetSocietyProjectIds(); 30/05/2024
            this.ReportProperties.Project = this.GetProjectIds(this.ReportProperties.Society, this.ReportProperties.BranchOffice);
            //--------------------------------------------------------

            string sqlGenerlatePL = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLoss);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLoss, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);
                if (!(string.IsNullOrEmpty(ReportProperties.BranchOffice)) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_IDColumn, this.ReportProperties.BranchOffice);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlGenerlatePL);
            }
            return resultArgs;
        }

        private ResultArgs GetReportSourcePLDetailConLedger()
        {
            ResultArgs resultArgs = new ResultArgs();
            //ContributionFrom = GetContriFromProvinceId();
            //ContributionTo = GetContriToProvinceId();

            string sqlGenerlatePL = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLossDetailByConLedger);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLossDetailByConLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlGenerlatePL);
            }
            return resultArgs;
        }

        private ResultArgs GetReportSourcePLDetailHOLedger()
        {
            ResultArgs resultArgs = new ResultArgs();
            //ContributionFrom = GetContriFromProvinceId();
            //ContributionTo = GetContriToProvinceId();

            string sqlGenerlatePL = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLossDetailByHOLedger);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateProfitandLossDetailByHOLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);

                if (!(string.IsNullOrEmpty(ReportProperties.Society)) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);

                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_FROM_PROVINCE_IDColumn, this.LoginUser.ProvinceFromLedgerIds);
                dataManager.Parameters.Add(this.ReportParameters.CONTRIBUTION_TO_PROVINCE_IDColumn, this.LoginUser.ProvinceToLedgerIds);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlGenerlatePL);
            }
            return resultArgs;
        }

        /*
        private int GetContriFromProvinceId()
        {
            ResultArgs resultArgs = null;
            string sqlLedgerIds = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GetLedgerContributionFromProvince);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GetLedgerContributionFromProvince, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_NAMEColumn, ContributionFromProvince);
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.Scalar, sqlLedgerIds);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        private int GetContriToProvinceId()
        {
            ResultArgs resultArgs = null;
            string sqlLedgerIds = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GetLegerContributionToProvince);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GetLegerContributionToProvince, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_NAMEColumn, ContributionToProvince);
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.Scalar, sqlLedgerIds);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
         */
        #endregion
    }
}

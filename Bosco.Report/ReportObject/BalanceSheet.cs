using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BalanceSheet : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        double CapDebit = 0;
        double CapCredit = 0;
        double OpCapDebit = 0;
        double OpCapCredit = 0;
        double TotalAssetAmt = 0;
        double TotalLiabilitiesAmt = 0;
        double DifferenceAmt = 0;
        double assetTotal = 0;
        double LiabilityTotal = 0;

        double ExcessDebitAmount;
        double ExcessCreditAmount;
        double DiffOpeningAmount = 0;   // chinna 22.02.2019

        #endregion

        #region Constructor
        public BalanceSheet()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            ExcessDebitAmount = 0;
            ExcessCreditAmount = 0;
            DiffOpeningAmount = 0;

            BindBalanceSheet();

            xrDifferenceOPLiability.Text = string.Empty;
            xrDifferenceOPAsset.Text = string.Empty;

            if (DiffOpeningAmount >= 0)
                xrDifferenceOPLiability.Text = this.UtilityMember.NumberSet.ToNumber(DiffOpeningAmount).ToString();
            else
                xrDifferenceOPAsset.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(DiffOpeningAmount)).ToString();

            base.ShowReport();
        }

        #endregion

        #region Events
        private void xrtblAssetTotalAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(assetTotal) + (DiffOpeningAmount < 0 ? Math.Abs(DiffOpeningAmount) : 0) + ExcessDebitAmount;
            e.Handled = true;
        }

        private void xrtblLiaAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Abs(LiabilityTotal) + (DiffOpeningAmount >= 0 ? Math.Abs(DiffOpeningAmount) : 0) + ExcessCreditAmount;
            e.Handled = true;
        }

        private void xrDiffLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (DiffOpeningAmount >= 0)
            {
                e.Result = "Difference in Opening Balance";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (DiffOpeningAmount >= 0)
            //{
            //    e.Result = "Difference in Opening Balance";
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrAssetDiff_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (DiffOpeningAmount < 0)
            {
                e.Result = "Difference in Opening Balance";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (DiffOpeningAmount < 0)
            //{
            //    e.Result = "Difference in Opening Balance";
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrExcessLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = "Excess of Income Over Expenditure";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (ExcessCreditAmount > 0)
            //{
            //    e.Result = "Excess of Income Over Expenditure";
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrExcessAssets_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = "Excess of Expenditure Over Income";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (ExcessDebitAmount > 0)
            //{
            //    e.Result = "Excess of Expenditure Over Income";
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrExcessLiabilitiesAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (ExcessCreditAmount > 0)
            //{
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
            //    e.Handled = true;

            //    double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
            //    if (ZeroValue == 0.00)
            //    {
            //        e.Result = "";
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrExcessAssetsAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            //if (ExcessDebitAmount > 0)
            //{
            //    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
            //    e.Handled = true;

            //    double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
            //    if (ZeroValue == 0.00)
            //    {
            //        e.Result = "";
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }
        #endregion

        #region Methods
        public void BindBalanceSheet()
        {
            try
            {
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;

                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;
                if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
                else
                {
                    SetReportTitle();
                    this.ReportPeriod = String.Format("As on: {0}", this.ReportProperties.DateAsOn);
                    setHeaderTitleAlignment();
                    BindSubReportSource();

                    GetBalanceSheetExcessAmount();
                    AssignDifferenceInOpeningBalance();
                    base.ShowReport();
                    xrtblDifference = AlignContentTable(xrtblDifference);
                    xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
                    xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        public ResultArgs GetBalanceSheet()
        {
            string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);

                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 1;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 1;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, 1);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, 1);

                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        public void AssignDifferenceInOpeningBalance()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalanceSheetOpeningAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetOpeningAmt);
                using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, BalanceSheetOpeningAmt);
                    DiffOpeningAmount = 0;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DiffOpeningAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
                    }
                }
            }
        }

        public void BindSubReportSource()
        {
            ResultArgs resultArgs = GetBalanceSheetSource();
            if (resultArgs.Success)
            {
                xrSubLiabilities.Visible = xrsubAssets.Visible = true;
                DataTable dtBalanceRpt = resultArgs.DataSource.Table;

                dtBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL < 0";
                DataTable dtLiabilities = dtBalanceRpt.DefaultView.ToTable();
                dtLiabilities.Columns.Add("AMOUNT", dtBalanceRpt.Columns["AMOUNT_ACTUAL"].DataType, "AMOUNT_ACTUAL * -1"); //Change negative value to possitive value
                BalanceSheetLiabilities liabilities = xrSubLiabilities.ReportSource as BalanceSheetLiabilities;
                liabilities.BindBalanceSheetLiability(dtLiabilities);
                LiabilityTotal = liabilities.TotalLiabilities;
                this.AttachDrillDownToSubReport(liabilities);
                liabilities.HideBalanceSheetLiabilityHeader();

                dtBalanceRpt.DefaultView.RowFilter = string.Empty;
                dtBalanceRpt.DefaultView.RowFilter = "AMOUNT_ACTUAL > 0";
                DataTable dtAsset = dtBalanceRpt.DefaultView.ToTable();
                dtAsset.Columns["AMOUNT_ACTUAL"].ColumnName = "AMOUNT";
                BalanceSheetAssets Asset = xrsubAssets.ReportSource as BalanceSheetAssets;
                Asset.BindBalanceSheetAsset(dtAsset);
                assetTotal = Asset.TotalAssets;
                this.AttachDrillDownToSubReport(Asset);
                Asset.HideBalanceSheetAssetCapHeader();
            }
            else
            {
                xrSubLiabilities.Visible = xrsubAssets.Visible = false;
            }
        }

        public ResultArgs GetBalanceSheetSource()
        {
            string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);

                int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, LedgerPaddingRequired);

                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
            }
            return resultArgs;
        }

        private void GetBalanceSheetExcessAmount()
        {
            if (LiabilityTotal != 0 || assetTotal != 0)
            {
                string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheetExcessDifference);
                using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateAsOn);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheet);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResource = resultArgs.DataSource.Table;
                        double IESUM = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["IESUM"].ToString());
                        ExcessCreditAmount = ExcessDebitAmount = 0;
                        if (IESUM < 0)
                        {
                            ExcessDebitAmount = Math.Abs(IESUM);
                        }
                        else
                        {
                            ExcessCreditAmount = IESUM;
                        }
                    }
                }
            }
        }
        #endregion
    }
}

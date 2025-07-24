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
    public partial class BalanceSheetAsset : Bosco.Report.Base.ReportHeaderBase
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
        double ExcessAmt = 0;
        #endregion

        #region Constructor
        public BalanceSheetAsset()
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
            BindBalanceSheetAsset();
            HideBalanceSheetAssetCapHeader();
            //base.ShowReport();
        }
        #endregion

        #region Events

        private void xrAssetAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double AssetAmt = this.ReportProperties.NumberSet.ToDouble(xrAssetAmt.Text);
            if (AssetAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrAssetAmt.Text = "";
            }
        }

        private void xrtblAssetTotalAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapDebit < CapCredit)
            {
                ExcessAmt = CapCredit - CapDebit;
                e.Result = CapDebit + ExcessAmt;
                e.Handled = true;
            }
            else
            {
                e.Result = CapDebit;
                e.Handled = true;
            }
        }

        private void xrtblLiaAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapCredit < CapDebit)
            {
                ExcessAmt = CapDebit - CapCredit;
                e.Result = CapCredit + ExcessAmt;
                e.Handled = true;
            }
            else
            {
                e.Result = CapCredit;
                e.Handled = true;
            }
        }

        private void xrDiffLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapDebit != CapCredit)
            {
                if (CapCredit < CapDebit)
                {
                    e.Result = "Difference in Balance";
                    e.Handled = true;
                }
                else
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
        }

        private void xrAssetDiff_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapDebit != CapCredit)
            {
                if (CapDebit < CapCredit)
                {
                    e.Result = "Difference in Balance";
                    e.Handled = true;
                }
                else
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
        }

        private void xrExcessLiaAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapDebit != CapCredit)
            {
                if (CapCredit < CapDebit)
                {
                    ExcessAmt = CapDebit - CapCredit;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessAmt);
                    e.Handled = true;
                }
                else
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
        }

        private void xrExcessAsssetAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapDebit != CapCredit)
            {
                if (CapDebit < CapCredit)
                {
                    ExcessAmt = CapCredit - CapDebit;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessAmt);
                    e.Handled = true;
                }
                else
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
        }

        private void xrOPAssetAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OPAssetAmt = this.ReportProperties.NumberSet.ToDouble(xrOPAssetAmt.Text);
            if (OPAssetAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOPAssetAmt.Text = "";
            }
        }

        private void xrOpTotaltblLiabilities_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (OpCapCredit < OpCapDebit)
            {
                ExcessAmt = OpCapDebit - OpCapCredit;
                e.Result = this.UtilityMember.NumberSet.ToNumber(OpCapCredit + ExcessAmt);
                e.Handled = true;
            }
            else
            {
                e.Result = OpCapCredit;
                e.Handled = true;
            }
        }

        private void xrOpTotalAssetAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (OpCapDebit < OpCapCredit)
            {
                ExcessAmt = OpCapCredit - OpCapDebit;
                e.Result = OpCapDebit + ExcessAmt;
                e.Handled = true;
            }
            else
            {
                e.Result = OpCapDebit;
                e.Handled = true;
            }
        }

        private void XrOpLiaDiffAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (OpCapDebit != OpCapCredit)
            {
                if (OpCapCredit < OpCapDebit)
                {
                    ExcessAmt = OpCapDebit - OpCapCredit;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessAmt);
                    e.Handled = true;
                }
                else
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
        }

        private void xrOpDiffAssetAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (OpCapDebit != OpCapCredit)
            {
                if (OpCapDebit < OpCapCredit)
                {
                    ExcessAmt = OpCapCredit - OpCapDebit;
                    e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessAmt);
                    e.Handled = true;
                }
                else
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
        }

        #endregion

        #region Methods
        public void HideBalanceSheetAssetCapHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
            this.HidePageHeader = false;
        }

        public void BindBalanceSheetAsset()
        {
            try
            {
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;

                // BalanceSheet Groups and Capital Fund 
                DateTime dtDateFrom = Convert.ToDateTime(YearFrom);
                string YearFromReducing = dtDateFrom.AddDays(-1).ToShortDateString();
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
                    this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
                    setHeaderTitleAlignment();
                    ResultArgs resultArgs = GetBalanceSheetSource();
                    DataView dtValue = resultArgs.DataSource.TableView;
                    DataTable dtOpValue = dtValue.ToTable();
                    if (dtOpValue != null && dtOpValue.Rows.Count > 0)
                    {
                        CapCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(OP_CREDIT)", "").ToString());
                        CapDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(OP_DEBIT)", "").ToString());
                        OpCapCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(POP_CREDIT)", "").ToString());
                        OpCapDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(POP_DEBIT)", "").ToString());
                    }
                    if (dtValue != null)
                    {
                        if (CapDebit > 0)
                        {
                            dtValue.Table.TableName = "BalanceSheet";
                            this.DataSource = dtValue;
                            this.DataMember = dtValue.Table.TableName;
                        }
                        else
                        {
                            dtValue.Table.TableName = "BalanceSheet";
                            this.DataSource = dtValue;
                            this.DataMember = dtValue.Table.TableName;
                        }
                        dtValue.RowFilter = "";
                        dtValue.RowFilter = "FLAG='ASSET'";
                        Detail.Visible = (this.ReportProperties.IncludeDetailed == 1);
                    }
                    base.ShowReport();
                    //    xrtblDetail = AlignContentTable(xrtblDetail);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        public ResultArgs GetBalanceSheetSource()
        {
            string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, YearFrom);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BalanceSheet);
            }
            return resultArgs;
        }
        #endregion
    }
}

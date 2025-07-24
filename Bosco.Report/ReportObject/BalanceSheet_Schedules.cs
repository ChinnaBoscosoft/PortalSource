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
    public partial class BalanceSheet_Schedules : Bosco.Report.Base.ReportHeaderBase
    {
        double RecAmt = 0;
        double PayAmt = 0;
        double RecPayCapOpAmt = 0;
        double CapTransAmt = 0;
        double OpeningTotal = 0;
        double IncExpenceTotal = 0;
        double Total = 0;
        double Prev_Total = 0;
        string dtClosingDateRangeYear = string.Empty;
        int CurrentFinancialYear = 0;
        private string EXCESSOFINCOME = "Excess Of Expenditure Over Income";
        private string EXCESSOFEXPENDITURE = "Excess of Income Over Expenditure";
        ResultArgs resultArgs = new ResultArgs();
        SettingProperty settingProperty = new SettingProperty();
        Receipts ReceiptsLedger = new Receipts();
        Payments PaymentsLedger = new Payments();
        public BalanceSheet_Schedules()
        {
            InitializeComponent();
        }
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
            BindBalanceSheet();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindBalanceSheet()
        {
            try
            {
                if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn) || this.ReportProperties.Project == "0")
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    DateTime dtYearClosing = Convert.ToDateTime(YearFrom);
                    dtClosingDateRangeYear = dtYearClosing.AddDays(-1).ToShortDateString();
                    CurrentFinancialYear = IsFinancialYear();
                    ResultArgs result = GetBalanceSheetSchedules();
                    DataTable dtGetAmount = result.DataSource.Table;
                    RecAmt = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["RECEIPTAMT"].ToString());
                    PayAmt = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["PAYMENTAMT"].ToString());
                    RecPayCapOpAmt = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["RPC_OP_TOTAL"].ToString());
                    CapTransAmt = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["CAP_TRANS_TOTAL"].ToString());
                    OpeningTotal = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["OP_TOTAL"].ToString());
                    IncExpenceTotal = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["IE_TOTAL"].ToString());
                    Total = this.UtilityMember.NumberSet.ToDouble(dtGetAmount.Rows[0]["TOTAL"].ToString());
                    Prev_Total = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["PREV_TOTAL"].ToString());
                    xrCellOpDate.Text = this.UtilityMember.DateSet.ToDate(YearFrom, DateFormatInfo.DateFormatYMD);
                    DataTable dtOpValue = result.DataSource.Table;
                    if (dtOpValue != null && dtOpValue.Rows.Count > 0)
                    {
                        if (CurrentFinancialYear > 0)
                        {
                            double amtOp = (RecPayCapOpAmt == 0 ? 0 : RecPayCapOpAmt);
                            xrCellOpAmt.Text = this.UtilityMember.NumberSet.ToNumber(amtOp);
                        }
                        else
                        {
                            xrCellOpAmt.Text = this.UtilityMember.NumberSet.ToNumber(Prev_Total);
                        }
                    }
                }
               //    xrTableCell1.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                //   xrHeaderCapitalFund.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        /// <summary>
        /// To Get Capital Fund
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetBalanceSheetSchedules()
        {
            string BalanceSheetSchedules = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSchedules);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.BalanceSchedules,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, dtClosingDateRangeYear);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                if (CurrentFinancialYear > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, dtClosingDateRangeYear);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, "");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BalanceSheetSchedules);
            }
            return resultArgs;
        }

        /// <summary>
        /// To get Financial Year
        /// </summary>
        /// <returns></returns>
        public int IsFinancialYear()
        {
            string FinancialYear = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, FinancialYear);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        private void xrcellCaptionExcessOfIncomeExpence_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (PayAmt < RecAmt)
            {
                e.Result = EXCESSOFEXPENDITURE;
                e.Handled = true;
            }
            else if (RecAmt < PayAmt)
            {
                e.Result = EXCESSOFINCOME;
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellExcessAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (IncExpenceTotal != 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(IncExpenceTotal);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

        }

        private void xrCellCalOPandCapitalFund_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (OpeningTotal != 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(OpeningTotal);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

        }

        private void xrCellCaptionAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapTransAmt != 0)
            {
                e.Result = "Addition";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }
        private void xrCellTotalClosingBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (Total != 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(Total);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrCellAdd_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (PayAmt != RecAmt)
            {
                if (PayAmt < RecAmt)
                {
                    e.Result = "Add :";
                    e.Handled = true;
                }
                else if (RecAmt < PayAmt)
                {
                    e.Result = "Less :";
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

        public void HideBalanceSheetCapHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        private void xrCellCapitalAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CapTransAmt != 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(CapTransAmt);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrCellOpAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (CurrentFinancialYear > 0)
            {
                if (RecPayCapOpAmt != 0)
                {
                    e.Result = this.UtilityMember.NumberSet.ToNumber(RecPayCapOpAmt);
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
                e.Result = this.UtilityMember.NumberSet.ToNumber(Prev_Total);
                e.Handled = true;
            }
        }
    }
}

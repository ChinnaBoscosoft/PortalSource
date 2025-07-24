using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetStatistics : Bosco.Report.Base.ReportHeaderBase
    {
        public BudgetStatistics()
        {
            InitializeComponent();

            //On 19th AB diocese, hide satistical details
            // if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE)
            if (LoginUser.IS_ABEBEN_DIOCESE || LoginUser.IS_DIOMYS_DIOCESE || LoginUser.IS_SDBINM_CONGREGATION)
            {
                PageHeader.Visible = Detail.Visible = grpBudgetHeader.Visible = xrTblSumFooter.Visible = false;
            }
        }

        public float SetLedgerNameColumnsWidth
        {
            set
            {
                xrCapStatisticsType.WidthF = xrStatisticsType.WidthF = xrTotalCaption.WidthF =
                 xrSummaryCaption.WidthF = xrSummaryCaption1.WidthF = xrTotalIncome.WidthF = xrTotalExpense.WidthF = xrTotalDifferenceCaption.WidthF = value;
            }
        }

        public float SetPreviousColumnsWidth
        {
            set
            {
                xrCapPreviousYear.WidthF = xrPreviousYear.WidthF = xrPreviousSum.WidthF = xrCapProposed.WidthF =
                    xrCapProposedAmount.WidthF = xrProposedTotalIncome.WidthF = xrProposedTotalExpense.WidthF = xrProposedTotalDifference.WidthF = value;
            }
        }

        public float SetCurrentColumnsWidth
        {
            set
            {
                xrCapCurrentYear.WidthF = xrCurrentYear.WidthF = xrCurrentSum.WidthF = xrCapApproved.WidthF =
                    xrCapApprovedAmount.WidthF = xrApprovedTotalIncome.WidthF = xrApprovedTotalExpense.WidthF = xrApprovedTotalDifference.WidthF = value;
            }
        }

        public Int32 BudgetTypeId
        {
            get;
            set;
        }

        public DateTime dtBudgetDateFrom
        {
            get;
            set;
        }

        public DateTime dtBudgetDateTo
        {
            get;
            set;
        }


        double totalProposedBudgetedIncome = 0;
        public double TotalProposedBudgetedIncome
        {
            set
            {
                totalProposedBudgetedIncome = value;
            }
        }

        double totalProposedBudgetedExpense = 0;
        public double TotalProposedBudgetedExpense
        {
            set
            {
                totalProposedBudgetedExpense = value;
            }
        }

        double totalApprovedBudgetedIncome = 0;
        public double TotalApprovedBudgetedIncome
        {
            set
            {
                totalApprovedBudgetedIncome = value;
            }
        }

        double totalApprovedBudgetedExpense = 0;
        public double TotalApprovedBudgetedExpense
        {
            set
            {
                totalApprovedBudgetedExpense = value;
            }
        }


        public void BindBudgetStatistics()
        {
            ResultArgs resultArgs = new ResultArgs();
            xrCapPreviousYear.Text = UtilityMember.DateSet.ToDate(this.LoginUser.YearFrom, false).AddYears(-1).Year + "-" + UtilityMember.DateSet.ToDate(this.LoginUser.YearTo, false).AddYears(-1).Year;
            xrCapCurrentYear.Text = UtilityMember.DateSet.ToDate(LoginUser.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(LoginUser.YearTo, false).Year;

            if (BudgetTypeId == (int)BudgetType.BudgetMonth)
            {
                xrCapProposedAmount.Text = dtBudgetDateFrom.ToString("MMMM yyyy");
                xrCapApprovedAmount.Text = dtBudgetDateFrom.ToString("MMMM yyyy");
            }
            else
            {
                xrCapProposedAmount.Text = UtilityMember.DateSet.ToDate(LoginUser.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(LoginUser.YearTo, false).Year;
                xrCapApprovedAmount.Text = UtilityMember.DateSet.ToDate(LoginUser.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(LoginUser.YearTo, false).Year;
            }

            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                DateTime PreviousFromDate = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddYears(-1);
                DateTime PreviousToDate = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddYears(-1);
                string Project = dtBudgetInfo.Rows[0]["PROJECT"].ToString();

                string budgetstatisticsSql = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetStatistics);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, PreviousFromDate);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, PreviousToDate);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECTColumn, Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetstatisticsSql);
                }



                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBudgetStatistics = resultArgs.DataSource.Table;
                    dtBudgetStatistics.TableName = "BUDGET_STATISTICS";
                    dtBudgetStatistics.DefaultView.Sort = "STATISTICS_TYPE";
                    SetReportBorder();

                    parBudgetProposedIncome.Visible = parBudgetProposedExpense.Visible = false;
                    parBudgetApprovedIncome.Visible = parBudgetApprovedExpense.Visible = prBudgetProposedDifference.Visible = prBudgetApprovedDifference.Visible = false;

                    parBudgetProposedIncome.Value = totalProposedBudgetedIncome;
                    parBudgetProposedExpense.Value = totalProposedBudgetedExpense;

                    parBudgetApprovedIncome.Value = totalApprovedBudgetedIncome;
                    parBudgetApprovedExpense.Value = totalApprovedBudgetedExpense;

                    prBudgetProposedDifference.Value = (totalProposedBudgetedIncome - totalProposedBudgetedExpense);
                    prBudgetApprovedDifference.Value = (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense);
                    if (LoginUser.IS_CMF_CONGREGATION)
                    {
                        parBudgetProposedExpense.Value = totalProposedBudgetedExpense + (totalProposedBudgetedIncome - totalProposedBudgetedExpense);
                        parBudgetApprovedExpense.Value = totalApprovedBudgetedExpense + (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense);

                        prBudgetProposedDifference.Value = 0;
                        prBudgetApprovedDifference.Value = 0;
                    }

                    //xrProposedTotalIncome.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedIncome);
                    //xrProposedTotalExpense.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedExpense);
                    //xrApprovedTotalIncome.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedIncome);
                    //xrApprovedTotalExpense.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedExpense);
                    //xrProposedTotalDifference.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedIncome - totalProposedBudgetedExpense);
                    //xrApprovedTotalDifference.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedIncome - totalApprovedBudgetedExpense);

                    xrTotalDifferenceCaption.Text = "Difference (Deficit / Surplus)"; //string.Empty;

                    //if ((totalProposedBudgetedIncome - totalProposedBudgetedExpense) > 0 && (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) > 0
                    //    || (totalProposedBudgetedIncome - totalProposedBudgetedExpense) == 0 || (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) == 0)
                    if ((totalProposedBudgetedIncome - totalProposedBudgetedExpense) > 0 && (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) > 0
                            || (totalProposedBudgetedIncome - totalProposedBudgetedExpense) == 0 && (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) == 0)
                    {
                        xrTotalDifferenceCaption.Text = "Surplus";
                    }
                    else if ((totalProposedBudgetedIncome - totalProposedBudgetedExpense) < 0 && (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) < 0)
                    {
                        xrTotalDifferenceCaption.Text = "Deficit";
                    }
                    else if ((totalProposedBudgetedIncome - totalProposedBudgetedExpense) < 0 || (totalApprovedBudgetedIncome - totalApprovedBudgetedExpense) < 0)
                    {
                        xrTotalDifferenceCaption.Text = "Difference (Deficit / Surplus)";
                    }
                    this.DataSource = dtBudgetStatistics;
                    this.DataMember = dtBudgetStatistics.TableName;
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
        }

        private void SetReportBorder()
        {
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblSumFooter = AlignContentTable(xrTblSumFooter);
            xrtblTotalBudget = AlignContentTable(xrtblTotalBudget);
            xrtblTotalCaption = AlignHeaderTable(xrtblTotalCaption);
        }

    }
}

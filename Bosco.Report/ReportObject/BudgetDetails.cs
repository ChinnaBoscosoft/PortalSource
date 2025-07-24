using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report;
using DevExpress.XtraSplashScreen;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetDetails : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BudgetDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetVariance();
        }
        #endregion

        #region Methods
        public void FetchBudgetVariance()
        {
            //if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                FetchBudgetDetails();
                base.ShowReport();
            }
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.BudgetName = objReportProperty.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                SetReportTitle();
                AssignBudgetDateRangeTitle();
                SetTitleWidth(xrtblHeader.WidthF);
                //this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                setHeaderTitleAlignment();

                //Hide budget group for ABE ( ABE,DIOmys)
                grpBudgetGroup.Visible = false;
                xrtblHeader.SuspendLayout();
                if (xrHeaderRow.Cells.Contains(xrcellHeaderBudgetSubGroup))
                    xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrcellHeaderBudgetSubGroup.Name]);
                xrtblHeader.PerformLayout();

                // commanded by chinna on portal (20.3.2019) 
                //xrtblBudget.SuspendLayout();
                //if (xrDataRow.Cells.Contains(xrcellBudgetSubGroup))
                // xrDataRow.Cells.Remove(xrDataRow.Cells[xrcellBudgetSubGroup.Name]);
                //xrtblBudget.PerformLayout();
                //xrcellTotal.WidthF = xrcellHeaderParticular.WidthF + xrcellHeaderCode.WidthF;
                //xrcellSumProposal.WidthF = xrcellHeaderSumProposal.WidthF;
                //xrcellSumApproval.WidthF = xrcellHeaderSumApproval.WidthF;

                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetDetails);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.BudgetDetails, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                    //  dataManager.Parameters.Add(this.ReportParameters.FDREGISTERSTATUSColumn, this.ReportProperties.FDRegisterStatus);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);

                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
                    Detail.SortFields.Add(new GroupField("BUDGET_SUB_GROUP"));

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        this.DataSource = resultArgs.DataSource.Table;
                        this.DataMember = resultArgs.DataSource.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
        }

        private void xrBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text.Trim();
            label.Text = budgetgroup.ToString().ToUpper();
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();
        }

        private void grpBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                string budgetnature = drvcrrentrow["BUDGET_NATURE"].ToString().Trim();
                string budgetgroup = drvcrrentrow["BUDGET_GROUP"].ToString().Trim();
                if (budgetnature.ToUpper() == "INCOME" && string.IsNullOrEmpty(budgetgroup))
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }
        #endregion
    }
}

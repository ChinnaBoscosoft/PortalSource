using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;


namespace Bosco.Report.ReportObject
{

    public partial class BudgetVariance : ReportHeaderBase
    {
        #region Variable
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BudgetVariance()
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
            //this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
            this.SetLandscapeHeader = xrtblHeader.WidthF;
            this.SetLandscapeFooter = xrtblHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
            setHeaderTitleAlignment();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            {
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                ShowReportFilterDialog();
            }
            else
            {
                FetchBudgetVarianceDetails();
                base.ShowReport();
            }
        }

        private void FetchBudgetVarianceDetails()
        {
            try
            {
                setHeaderTitleAlignment();
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                this.BudgetName = objReportProperty.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                //Hide budget group for ABE
                // chinna 20.03.2019 at Portal commanded
                // if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE)
                // {
                // grpBudgetGroup.Visible = false;
                //xrtblHeader.SuspendLayout();
                //if (xrHeaderRow.Cells.Contains(xrcellHeaderBudgetSubGroup))
                //   xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrcellHeaderBudgetSubGroup.Name]);
                //xrtblHeader.PerformLayout();
                //xrtblDetails.SuspendLayout();
                // if (xrDetailRow.Cells.Contains(xrcellBudgetSubGroup))
                // xrDetailRow.Cells.Remove(xrDetailRow.Cells[xrcellBudgetSubGroup.Name]);
                //xrtblDetails.PerformLayout();
                // xrCellTotal.WidthF = xrCellHeaderLedgerName.WidthF + xrCellHeaderLedgerCode.WidthF;
                //xrCellSumApproved.WidthF = xrCellHeaderApproved.WidthF;
                // xrCellSumActual.WidthF = xrCellHeaderActual.WidthF;
                // xrlblVariance.WidthF = xrcellTotalSpace.WidthF;
                // xrcellHeaderAmount.WidthF = xrCellInAmt.WidthF;
                // xrcellHeaderInpercentage.WidthF = xrCellInPercentage.WidthF;
                // }

                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetVarianceReport);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BudgetVariance.BudgetVarianceReport, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting5.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    //if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                    // {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                    // }

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
                    SetReportBorder();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        this.DataSource = resultArgs.DataSource.Table;
                        this.DataMember = resultArgs.DataSource.Table.TableName;
                    }
                }
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
            xrtblDetails = AlignContentTable(xrtblDetails);
        }

        private void xrBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text;
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

        private void xrCellInPercentage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                string budgetnature = drvcrrentrow["BUDGET_NATURE"].ToString().Trim();
                string budgetgroup = drvcrrentrow["BUDGET_GROUP"].ToString().Trim();
                Double approvedamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["APPROVED_AMOUNT"].ToString());
                Double actualamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["ACTUAL_AMOUNT"].ToString());
                Double varianceamountamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["BUDGET_VARIANCE"].ToString());

                XRTableCell xrpercentagecell = ((XRTableCell)sender);
                xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Regular);
                xrpercentagecell.ForeColor = Color.Black;
                if (budgetnature.ToUpper() == "INCOME")
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Green;
                    }
                }
                else
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Red;
                    }
                }

                if (varianceamountamount == 0)
                {

                }
            }
        }

        private void xrCellInAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                Double varianceamountamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["BUDGET_VARIANCE"].ToString());
                if (varianceamountamount == 0)
                {
                    XRTableCell xrvariaincecell = sender as XRTableCell;
                }
            }
        }

        #endregion

    }
}

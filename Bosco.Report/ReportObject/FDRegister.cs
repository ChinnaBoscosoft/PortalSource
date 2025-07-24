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
    public partial class FDRegister : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FDRegister()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblFdRegister, xrTableCell14,
   new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchFDRegister();
        }
        #endregion

        #region Methods
        public void FetchFDRegister()
        {

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                FetchFDRegisterDetails();
                base.ShowReport();
            }
        }

        public void FetchFDRegisterDetails()
        {
            try
            {
                //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.SetLandscapeHeader = 1060.25f;
                this.SetLandscapeFooter = 1060.25f;
                this.SetLandscapeFooterDateWidth = 900.00f;
                setHeaderTitleAlignment();
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrAccumulatedInterest.Text = this.SetCurrencyFormat(xrAccumulatedInterest.Text);
                xrIntReceived.Text = this.SetCurrencyFormat(xrIntReceived.Text);
                xrPrincipalAmount.Text = this.SetCurrencyFormat(xrPrincipalAmount.Text);
                xrTotalAmount.Text = this.SetCurrencyFormat(xrTotalAmount.Text);
                xrWithdrawAmount.Text = this.SetCurrencyFormat(xrWithdrawAmount.Text);
                xrClosingBalance.Text = this.SetCurrencyFormat(xrClosingBalance.Text);
                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDRegisterDetails);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.FetchFDRegisterDetails, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDRegister);

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        string RegisterStatus = objReportProperty.FDRegisterStatus.Equals((int)YesNo.No) ? "All" : objReportProperty.FDRegisterStatus.Equals((int)YesNo.Yes) ? "Active" : "Closed";
                        if (!RegisterStatus.Equals("All"))
                            dvCashFlow.RowFilter = "CLOSING_STATUS='" + RegisterStatus + "'";

                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow.ToTable();
                        this.DataMember = dvCashFlow.Table.TableName;
                        dvCashFlow.RowFilter = "";
                    }
                    else
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;
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
            //***To align header table dynamically---changed by sugan******************************************************************************************
            xrtblHeaderTable.SuspendLayout();
            xrtblFdRegister.SuspendLayout();
            xrtblGrandTotal.SuspendLayout();
            //remove headers
            if (xrTableRow1.Cells.Contains(xrTableCell8))
                xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrTableCell8.Name]);

            if (xrTableRow1.Cells.Contains(xrTableCell7))
                xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrTableCell7.Name]);

            //values of columns
            if (xrTableRow2.Cells.Contains(xrTableCell9))
                xrTableRow2.Cells.Remove(xrTableRow2.Cells[xrTableCell9.Name]);

            if (xrTableRow2.Cells.Contains(xrTableCell20))
                xrTableRow2.Cells.Remove(xrTableRow2.Cells[xrTableCell20.Name]);

            //footer
            if (xrTableRow3.Cells.Contains(xrTableCell38))
                xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrTableCell38.Name]);

            if (xrTableRow3.Cells.Contains(xrTableCell39))
                xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrTableCell39.Name]);

            xrtblHeaderTable.PerformLayout();
            xrtblFdRegister.PerformLayout();
            xrtblGrandTotal.PerformLayout();
            //*********************************************************************************************
            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
        }
        #endregion

    }
}

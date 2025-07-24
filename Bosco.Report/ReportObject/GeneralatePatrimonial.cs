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
    public partial class GeneralatePatrimonial: Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        int RecordNumber = 0;
        #endregion

        #region Constructor
        public GeneralatePatrimonial()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReports
        public override void ShowReport()
        {
            RecordNumber = 0;
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
        public void BindSource(bool fromMasterReport= false)
        {
            RecordNumber = 0;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            this.SetLandscapeHeader = xrtblGrpHeader.WidthF;
            this.SetLandscapeFooter= xrtblGrpHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblGrpHeader.WidthF;

            if (fromMasterReport)
            {
                this.HideReportHeader = this.HidePageHeader = this.HidePageFooter = false;
                this.HideHeaderFully = true;
                xrlblYear.Text = "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            }

            if (resultArgs.Success && resultArgs.DataSource != null)
            {
                DataTable dtCBBalances = resultArgs.DataSource.Table;
                if (dtCBBalances != null)
                {
                    dtCBBalances.TableName = this.DataMember;
                    CheckCashBankFDLedgers(dtCBBalances);
                    this.DataSource = dtCBBalances;
                    this.DataMember = dtCBBalances.TableName;
                }
            }

            FillReportProperties();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralatePatrimonial);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralatePatrimonial, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }
            return resultArgs;
        }

        /// <summary>
        /// IF there is no cash/bank/fd ledgers, add empty row for those ledgers
        /// </summary>
        /// <param name="dtCBBalances"></param>
        private void CheckCashBankFDLedgers(DataTable dtCBBalances)
        {
            if (dtCBBalances != null)
            {
                dtCBBalances.DefaultView.RowFilter=string.Empty;
                //For Cash in hand
                dtCBBalances.DefaultView.RowFilter = this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash;
                if (dtCBBalances.DefaultView.Count == 0)
                {
                    DataRow dr = dtCBBalances.NewRow();
                    dr[reportSetting1.AccountBalance.SORT_IDColumn.ColumnName] = 0;
                    dr[this.ReportParameters.GROUP_IDColumn.ColumnName] = (int)FixedLedgerGroup.Cash;
                    dr[this.ReportParameters.LEDGER_IDColumn.ColumnName] = 0;
                    dr[this.ReportParameters.BANKColumn.ColumnName] = string.Empty;
                    dr[this.ReportParameters.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                    dr[reportSetting1.AccountBalance.AMOUNT_OPColumn.ColumnName] = 0;
                    dr[reportSetting1.AccountBalance.AMOUNT_CLColumn.ColumnName] = 0;
                    dtCBBalances.Rows.Add(dr);
                }

                //For Bank Accounts
                dtCBBalances.DefaultView.RowFilter = this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts;
                if (dtCBBalances.DefaultView.Count == 0)
                {
                    DataRow dr = dtCBBalances.NewRow();
                    dr[reportSetting1.AccountBalance.SORT_IDColumn.ColumnName] = 1;
                    dr[this.ReportParameters.GROUP_IDColumn.ColumnName] = (int)FixedLedgerGroup.BankAccounts;
                    dr[this.ReportParameters.LEDGER_IDColumn.ColumnName] = 0;
                    dr[this.ReportParameters.BANKColumn.ColumnName] = string.Empty;
                    dr[this.ReportParameters.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                    dr[reportSetting1.AccountBalance.AMOUNT_OPColumn.ColumnName] = 0;
                    dr[reportSetting1.AccountBalance.AMOUNT_CLColumn.ColumnName] = 0;
                    dtCBBalances.Rows.Add(dr);
                }

                //For FD Accounts
                dtCBBalances.DefaultView.RowFilter = this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit;
                if (dtCBBalances.DefaultView.Count == 0)
                {
                    DataRow dr = dtCBBalances.NewRow();
                    dr[reportSetting1.AccountBalance.SORT_IDColumn.ColumnName] =2;
                    dr[this.ReportParameters.GROUP_IDColumn.ColumnName] = (int)FixedLedgerGroup.FixedDeposit;
                    dr[this.ReportParameters.LEDGER_IDColumn.ColumnName] = 0;
                    dr[this.ReportParameters.BANKColumn.ColumnName] = string.Empty;
                    dr[this.ReportParameters.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                    dr[reportSetting1.AccountBalance.AMOUNT_OPColumn.ColumnName] = 0;
                    dr[reportSetting1.AccountBalance.AMOUNT_CLColumn.ColumnName] = 0;
                    dtCBBalances.Rows.Add(dr);
                }
                dtCBBalances.DefaultView.RowFilter = string.Empty;
            }
        }
        private void FillReportProperties()
        {
            xrAsOnFrom.Text = "Situation on " + ReportProperties.DateFrom.ToString();
            xrAsOnTo.Text = "Situation on " + ReportProperties.DateTo.ToString();
        }

        private string GetSerialCode(Int32 grpid)
        {
            string rtn = string.Empty;
            if (grpid == (int)FixedLedgerGroup.Cash)
            {
                rtn = "N";
            }
            else if (grpid == (int)FixedLedgerGroup.BankAccounts)
            {
                rtn = "O";
            }
            else if (grpid == (int)FixedLedgerGroup.FixedDeposit)
            {
                rtn = "P";
            }
            return rtn;
        }
        #endregion

        #region Events
        private void xrCellGrpSCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = GetSerialCode(grpid);
                RecordNumber = 0;
            }
        }

        private void xrCellCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RecordNumber++;
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (ledgerid > 0)
                {
                    cell.Text = GetSerialCode(grpid) + RecordNumber.ToString();
                }
                else
                {
                    cell.Text = string.Empty;
                }
            }
        }

        private void xrCellLGGrpFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = "Total " + GetSerialCode(grpid);
            }
        }

        private void xrcellLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (grpid == (int)FixedLedgerGroup.Cash)
                {
                    cell.Text = "Cash-in-hand ";
                }
                else if (grpid == (int)FixedLedgerGroup.BankAccounts)
                {
                    cell.Text = "Interest bearing Deposits at : ";
                }
                else if (grpid == (int)FixedLedgerGroup.FixedDeposit)
                {
                    cell.Text = "Bonds and other types of investment : ";
                }
            }
        }

        private void GrpLGHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                xrgrpCBRow1.Visible = (grpid == (int)FixedLedgerGroup.BankAccounts || grpid == (int)FixedLedgerGroup.FixedDeposit);

                if (grpid == (int)FixedLedgerGroup.BankAccounts)
                {
                    xrcellGrpBankCaption.Text = "Bank";
                    xrcellGrpAccountCaption.Text = "N. Bank A/c";
                }
                else if (grpid == (int)FixedLedgerGroup.FixedDeposit)
                {
                    xrcellGrpBankCaption.Text = "Type of investment";
                    xrcellGrpAccountCaption.Text = "Bank or Financing Institution";
                }
            }
        }

        private void xrCellGrandAmountOP_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                if (dtReport != null)
                {
                    XRTableCell cell = sender as XRTableCell;
                    object obsum = dtReport.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNT_OPColumn.ColumnName + ")", string.Empty).ToString();
                    cell.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(obsum.ToString()));
                }
            }
        }

        private void xrCellGrandAmountCL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                if (dtReport != null)
                {
                    XRTableCell cell = sender as XRTableCell;
                    object obsum = dtReport.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNT_CLColumn.ColumnName + ")", string.Empty).ToString();
                    cell.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(obsum.ToString()));
                }
            }
        }

        private void tcAmountOP_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                if (ledgerid == 0)
                {
                    XRTableCell cell = sender as XRTableCell;
                    cell.Text = string.Empty;
                }
            }
        }

        private void tcAmountCL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName) != null)
            {
                Int32 grpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                if (ledgerid == 0)
                {
                    XRTableCell cell = sender as XRTableCell;
                    cell.Text = string.Empty;
                }
            }
        }
        #endregion

        

        

    }
}

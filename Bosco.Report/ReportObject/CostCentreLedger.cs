using System;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreLedger : ReportHeaderBase
    {
        #region Constructor
        public CostCentreLedger()
        {
            InitializeComponent();

            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrTable3.WidthF = xrCosLedgerGroup.WidthF = xrlblCosLedgerGroup.WidthF = 670.25f;
            xrtblCCName.WidthF = xrTable7.WidthF = xrTable2.WidthF = xrGrouptotal.WidthF = xrtblGrandTotal.WidthF = 670.25f;
        }
        #endregion

        #region Variables

        int count = 0;
        double CosLedgerDebit = 0;
        double CosLedgerCredit = 0;
        double CosLedgerDebitSum = 0;

        int CosMonthlyGroupNumber = 0;
        double CosMonthlyOpeningBalance = 0;
        double CosMonthlyClosingBalance = 0;

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindReport();
            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                setHeaderTitleAlignment();
                SetReportTitle();
                this.CosCenterName = objReportProperty.CostCentreName;
                string[] Led = this.ReportProperties.Ledger.Split(',');
                this.ReportTitle = this.ReportProperties.ReportTitle;

                prOPBalance.Value = "0.00";
                grpHeaderLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1) ? true : false;
                grpCostCentreName.Visible = (this.ReportProperties.ShowByCostCentre == 1) ? true : false;
                grpCostcentreCategoryName.Visible = (this.ReportProperties.ShowByCostCentreCategory == 1) ? true : false;

                prOPBalance.Visible = false;
                ResultArgs resultArgs = BindLedgerSource();
                DataView dtview = resultArgs.DataSource.TableView;
                if (dtview != null)
                {
                    dtview.Table.TableName = "Ledger";
                    this.DataSource = dtview;
                    this.DataMember = dtview.Table.TableName;
                }
            }
        }

        public ResultArgs BindLedgerSource()
        {
            ResultArgs resultArgs = null;
            string Test = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterLedger);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CostCentre.CostCenterLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Test);
            }
            return resultArgs;
        }

        public XRTable AlignDetailTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;
            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Left;
                            else
                                if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }
                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }

                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;

                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.None;
                                else if (cellcount == 1)
                                    cell.Borders = BorderSide.Left | BorderSide.Bottom;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Left;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                }
                cellcount = 0;
            }
            return table;
        }
        #endregion

        #region Events

        private void xrtblLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            CosLedgerDebit += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            CosLedgerCredit += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
            if ((CosLedgerDebit - CosLedgerCredit) < 0)
            {
                // xrTableCell4.Text =
                xrtblCosClosingBalance.Text = xrCosGroupLedgerDebit.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs((CosLedgerDebit - CosLedgerCredit))).ToString() + " Cr";
            }
            else
            {
                // xrTableCell4.Text =
                xrtblCosClosingBalance.Text = xrCosGroupLedgerDebit.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs((CosLedgerDebit - CosLedgerCredit))).ToString() + " Dr";
            }

            CosLedgerDebitSum = CosLedgerDebit;
        }

        private void xrtblLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            string[] Led = this.ReportProperties.Ledger.Split(',');
            if (Led.Length != 1)
            {
                CosLedgerDebit = CosLedgerCredit = 0.0;
            }
            e.Result = CosLedgerDebitSum;
            e.Handled = true;
        }

        private void xrtblLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            string[] Led = this.ReportProperties.Ledger.Split(',');
            if (Led.Length != 1)
            {
                CosLedgerDebitSum = 0;
                CosLedgerDebit = CosLedgerCredit = 0;
            }
        }

        private void xttblOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            if (CosMonthlyGroupNumber == 0)
            {
                grpHeaderVoucherMth.Visible = false;
                e.Result = "0.00";
                CosMonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblCosClosingBalance.Text);
                CosMonthlyGroupNumber++;
                e.Handled = true;
            }
            else
            {
                if (this.ReportProperties.IncludeLedgerGroupTotal == 1) { grpHeaderVoucherMth.Visible = true; }
                e.Result = CosMonthlyClosingBalance;
                CosMonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblCosClosingBalance.Text);
                e.Handled = true;

            }
        }

        private void xrCosDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosDebit.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosDebit.Text = "";
            }
        }

        private void xrCosCrdit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosCrdit.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosCrdit.Text = "";
            }
        }

        private void xrtblCosClosingBalance_SummaryReset(object sender, EventArgs e)
        {
            CosLedgerDebit = CosLedgerCredit = 0;
        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrTable2 = AlignTable(xrTable2, Narration, string.Empty, count);
        }

        #endregion
    }
}

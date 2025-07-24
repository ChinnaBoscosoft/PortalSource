using System;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class BankFlow : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        double DailyReceipts = 0;
        double DailyPayments = 0;
        double DailyClBalance = 0;
        double PreviousCLBalance = 0;
        int GroupNumber = 0;
        bool IsFirstVal = true;
        #endregion

        #region Constructor
        public BankFlow()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrtblBindBankFlow.WidthF = xrOpeningBalance.WidthF = xrtblCbalance.WidthF = xrtblGrandTotal.WidthF= 670.25f;
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
           // BankFlowReport();
            DailyReceipts = 0;
            DailyPayments = 0;
            DailyClBalance = 0;
            PreviousCLBalance = 0;
            GroupNumber = 0;
            BankFlowReport();
            base.ShowReport();
        }
        private void BankFlowReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty
                || this.ReportProperties.Project != "0")
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrCapInFlow.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.INFLOW);
                xrCapOutFlow.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.OUTFLOW);
                xrCapBalance.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.BALANCE);

                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    //To show Opening Balance
                    AcMEDSync.Model.BalanceProperty bankop = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                                           BalanceSystem.BalanceType.OpeningBalance);
                    AcMEDSync.Model.BalanceProperty fdopbal = balanceSystem.GetFDBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                                           BalanceSystem.BalanceType.OpeningBalance);
                    prOPBalance.Value = bankop.Amount + fdopbal.Amount;

                    AcMEDSync.Model.BalanceProperty bankcl = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                                           BalanceSystem.BalanceType.ClosingBalance);
                    prOPBalance.Visible = false;
                    DataTable dtCashBankBook = GetReportSource();
                    if (dtCashBankBook != null && dtCashBankBook.Rows.Count > 0)
                    {
                        this.DataSource = dtCashBankBook;
                        this.DataMember = dtCashBankBook.TableName;
                    }
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            //xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrtblBindBankFlow = SetBorders(xrtblBindBankFlow, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrTable2 = SetBorders(xrTable2, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrTable3 = SetBorders(xrTable3, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);

            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrOpeningBalance = AlignOpeningBalanceTable(xrOpeningBalance);
            xrtblBindBankFlow = AlignContentTable(xrtblBindBankFlow);
            xrtblCbalance = AlignClosingBalance(xrtblCbalance);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);

        }

        private DataTable GetReportSource()
        {
            try
            {
                string BankFlowQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankFlow);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.BankFlow,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankFlowQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }
        #endregion

        private void xrtblClosingBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            if (!IsFirstVal)
            {
                GroupNumber++;
                DailyReceipts = (GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName).ToString());
                DailyPayments = (GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName).ToString());
                if (GroupNumber == 1)
                    DailyClBalance = PreviousCLBalance = (this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString()) + DailyReceipts) - DailyPayments;
                else
                    DailyClBalance = PreviousCLBalance = (PreviousCLBalance + DailyReceipts) - DailyPayments;
                xrCashAmount.Text = xrCashBalance.Text = this.UtilityMember.NumberSet.ToNumber(DailyClBalance);
            }
            else
            {
                IsFirstVal = false;
            }
        }

        private void xrOPBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void xrOutflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankOutFlow = this.ReportProperties.NumberSet.ToDouble(xrOutflow.Text);
            if (BankOutFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOutflow.Text = "";
            }
        }

        private void xrInflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankInFlow = this.ReportProperties.NumberSet.ToDouble(xrInflow.Text);
            if (BankInFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrInflow.Text = "";
            }
        }

        private void xrDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrDate.NavigateUrl = PagePath.ReportPath + "?rid=" + this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.LEDGER_BANK) +
          "&hdva=true&DrillDownType=" + DrillDownType.LEDGER_BANK + "&FNAME=" + this.reportSetting1.CashBankFlow.DATEColumn.ColumnName +
          "&FVALUE=" + GetCurrentColumnValue(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName);
            xrDate.Target = "_self";
        }
    }
}

using System;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMEDSync.Model;


namespace Bosco.Report.ReportObject
{
    public partial class CashFlow : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double DailyReceipts = 0;
        double DailyPayments = 0;
        double DailyClBalance = 0;
        double PreviousCLBalance = 0;
        int GroupNumber = 0;
        bool IsFirstValue = true;
        #endregion

        #region Constructor
        public CashFlow()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrtblBindData.WidthF = xrtblOpeningBalance.WidthF = xrtblCbalance.WidthF = xrtblGrandTotal.WidthF = 670.25f;
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindCashFlow();
            DailyReceipts = 0;
            DailyPayments = 0;
            DailyClBalance = 0;
            PreviousCLBalance = 0;
            GroupNumber = 0;
            base.ShowReport();

        }
        #endregion

        #region Method
        private void BindCashFlow()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.HideCostCenter = false;
                this.CosCenterName = null;
                xrCapInFlow.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.INFLOW);
                xrCapOutFlow.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.OUTFLOW);
                xrCapBalance.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.BALANCE);
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    //To show Opening Balance
                    AcMEDSync.Model.BalanceProperty cashop = balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                                            BalanceSystem.BalanceType.OpeningBalance);
                    prOPBalance.Value = cashop.Amount;
                }

                prOPBalance.Visible = false;
                resultArgs = GetReportSource();
                DataView dvCashFlow = resultArgs.DataSource.TableView;
                if (dvCashFlow != null && dvCashFlow.Count != 0)
                {
                    dvCashFlow.Table.TableName = "CashBankFlow";
                    this.DataSource = dvCashFlow;
                    this.DataMember = dvCashFlow.Table.TableName;
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            //xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrtblBindData = SetBorders(xrtblBindData, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrtblGrandTotal = SetBorders(xrtblGrandTotal, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrTable2 = SetBorders(xrTable2, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);

            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindData = AlignContentTable(xrtblBindData);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblOpeningBalance = AlignContentTable(xrtblOpeningBalance);
            xrtblCbalance = AlignContentTable(xrtblCbalance);

        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string CashFlow = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashFlow);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.CashFlow,DataBaseType.HeadOffice))
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashFlow);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrtblClosingBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            if (!IsFirstValue)
            {
                GroupNumber++;

                DailyReceipts = (GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName).ToString());
                DailyPayments = (GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName).ToString());
                if (GroupNumber == 1)
                    DailyClBalance = PreviousCLBalance = (this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString()) + DailyReceipts) - DailyPayments;
                else
                    DailyClBalance = PreviousCLBalance = (PreviousCLBalance + DailyReceipts) - DailyPayments;
                xrCashBalanceAmt.Text = xrCashBalance.Text = this.UtilityMember.NumberSet.ToNumber(DailyClBalance);
            }
            else
            {
                IsFirstValue = false;
            }
        }

        private void xrOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void xrCashInflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashInflow = this.ReportProperties.NumberSet.ToDouble(xrCashInflow.Text);
            if (CashInflow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCashInflow.Text = "";
            }
        }

        private void xrCashOutFlow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashOutFlow = this.ReportProperties.NumberSet.ToDouble(xrCashOutFlow.Text);
            if (CashOutFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCashOutFlow.Text = "";
            }
        }

        private void xrCashDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrCashDate.NavigateUrl = PagePath.ReportPath + "?rid=" + this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.LEDGER_CASH) +
          "&hdva=true&DrillDownType=" + DrillDownType.LEDGER_CASH + "&FNAME=" + this.reportSetting1.CashBankFlow.DATEColumn.ColumnName +
          "&FVALUE=" + GetCurrentColumnValue(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName);
            xrCashDate.Target = "_self";
        }

    }
}

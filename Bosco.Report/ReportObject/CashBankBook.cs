using System;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankBook : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int DailyGroupNumber = 0;
        double DailyGrpCashOpbalance = 0;
        double DailyGrpCashClbalance = 0;
        double DailyGrpBankOpbalance = 0;
        double DailyGrpBankClbalance = 0;

        double DailyCashReceipts = 0;
        double DailyBankPayments = 0;
        double DailyBankReceipts = 0;
        double DailyCashPayments = 0;

        int count = 0;
        #endregion

        #region Constructor
        public CashBankBook()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 1070.25f;
            xrtblHeaderCaption.WidthF = xrTblOpeningBalance.WidthF = xrTblDailyOpeningBalance.WidthF = xrTblDailyClosingBalance.WidthF = 1070.25f;
            xrtblBindSource.WidthF = xrTblTotal.WidthF = xrTblClosingBalance.WidthF = xrtblGrandTotal.WidthF = 1070.25f;
            this.SetLandscapeFooter = 1070.25f;
            //this.AttachDrillDownToRecord(xrtblBindSource, xrReceipt,
            //    new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            //this.AttachDrillDownToRecord(xrtblBindSource, xrPayments,
            //    new ArrayList { this.ReportParameters.PAY_VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_PAYMENT_SUB_TYPE");
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            CashAndBankBook();
            DailyGrpCashOpbalance = 0;
            DailyGrpCashClbalance = 0;
            DailyGrpBankOpbalance = 0;
            DailyGrpBankClbalance = 0;
            DailyGroupNumber = 0;
            base.ShowReport();
        }

        private void CashAndBankBook()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                setHeaderTitleAlignment();               
                this.SetLandscapeFooterDateWidth = 900.00f;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrCapRecCash.Text = xrCapPayCash.Text = this.SetCurrencyFormat(xrCapRecCash.Text);
                xrCapRecBank.Text = xrCapPayBank.Text = this.SetCurrencyFormat(xrCapRecBank.Text);

                //xrReceiptAmount.Text = xrPaymentCash.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                //xrPaymentAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);

                //To Show OP & Cl Balance
                grpHeaderVoucherDate.Visible = (this.ReportProperties.ShowDailyBalance == 1);
                grpFooterVoucherDate.Visible = grpHeaderVoucherDate.Visible;

                grpHeaderOPBalance.Visible = (this.ReportProperties.ShowDailyBalance == 0);
                grpHeaderCLBalance.Visible = ReportFooter.Visible = grpHeaderOPBalance.Visible;

                prOPCashBalance.Visible = prCLCashBalance.Visible = prOPBankBalance.Visible = prCLBankBalance.Visible = false;
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {

                    //AcMEDSync.Model.BalanceProperty cashop = balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                    //                     BalanceSystem.BalanceType.OpeningBalance);
                    //prOPCashBalance.Value = cashop.Amount;

                    //AcMEDSync.Model.BalanceProperty cashcl=balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateTo,
                    //                     BalanceSystem.BalanceType.ClosingBalance);
                    //prCLCashBalance.Value = cashcl.Amount;

                    //AcMEDSync.Model.BalanceProperty bankop = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateFrom,
                    //                    BalanceSystem.BalanceType.OpeningBalance);
                    //prOPBankBalance.Value = bankop.Amount;

                    //AcMEDSync.Model.BalanceProperty bankcl = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.DateTo,
                    //                     BalanceSystem.BalanceType.ClosingBalance);
                    //prCLBankBalance.Value = bankcl.Amount;

                    prOPCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                    BalanceSystem.BalanceType.OpeningBalance);

                    prCLCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                         BalanceSystem.BalanceType.ClosingBalance);

                    prOPBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                        BalanceSystem.BalanceType.OpeningBalance);

                    prCLBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                         BalanceSystem.BalanceType.ClosingBalance);
                }
                xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = true;
                if (objReportProperty.ShowDetailedBalance == 1)
                {
                    LoadDetailedBalance();
                }
                else
                {
                    xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = grpHeaderDetailedOPBalance.Visible = grpDetailedCLBalance.Visible = false;
                }

                resultArgs = GetReportSource();
                this.DataSource = null;
                DataView dvCashBankBook = resultArgs.DataSource.TableView;
                if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                {
                    dvCashBankBook.Table.TableName = "CashBankBook";
                    this.DataSource = dvCashBankBook;
                    this.DataMember = dvCashBankBook.Table.TableName;
                }


                if (this.ReportProperties.IncludeNarration == 1)
                {
                    string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
                    string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
                }
            }
            SetReportSettings();
        }
        private void LoadDetailedBalance()
        {
            grpHeaderCLBalance.Visible = ReportFooter.Visible = true;
            grpHeaderOPBalance.Visible = xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = grpHeaderDetailedOPBalance.Visible = grpDetailedCLBalance.Visible = true;
            AccountBalance accountBalance = xrSubOpeningBalance.ReportSource as AccountBalance;
            accountBalance.BindBalance(true, false);

            accountBalance.NameColumnWidth = accountBalance.NameHeaderColumWidth = xrCapReceipt.WidthF;
            accountBalance.GroupAmountWidth = accountBalance.AmountColumnWidth = xrCapRecCash.WidthF + xrCapRecBank.WidthF;
            accountBalance.GroupNameWidth = xrCapReceipt.WidthF;
       //     accountBalance.GroupNameWidth = accountBalance.AmountColumnWidth = xrCapReceipt.WidthF + xrReceiptAmount.WidthF;
            accountBalance.NameColumnWidth = xrCapReceipt.WidthF;
            accountBalance.AmountColumnWidth = xrCapRecCash.WidthF + xrCapRecBank.WidthF;
            accountBalance.AmountProgressiveColumnWidth = accountBalance.AmountProgressiveHeaderColumnWidth = 0;
            Double ReceiptOPAmt = accountBalance.PeriodBalanceAmount;

            AccountBalance accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalance;
            accountClosingBalance.BindBalance(false, false);
            Double PaymentClAmt = accountClosingBalance.PeriodBalanceAmount;

            accountClosingBalance.GroupNameWidth = xrCapPayment.WidthF;
            accountClosingBalance.GroupAmountWidth = xrCapPayCash.WidthF + xrCapPayBank.WidthF;
            accountClosingBalance.NameColumnWidth = accountBalance.GroupNameWidth = xrCapPayment.WidthF;
            accountClosingBalance.AmountColumnWidth = accountBalance.GroupAmountWidth = xrCapPayCash.WidthF + xrCapPayBank.WidthF;
            accountClosingBalance.AmountProgressiveColumnWidth = accountClosingBalance.AmountProgressiveHeaderColumnWidth = 0;
            xrtblBindSource.Borders = xrCapPayBank.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashBankBook);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.CashBankBook, DataBaseType.HeadOffice))
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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportSettings()
        {
            count = 0;
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            if (ReportProperties.ShowDailyBalance == 1 || ReportProperties.ShowDetailedBalance == 1)
                xrTblOpeningBalance = SetBorders(xrTblOpeningBalance, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            else
                xrTblOpeningBalance = SetBalanceTableBorders(xrTblOpeningBalance);
            xrTblDailyOpeningBalance = SetBalanceTableBorders(xrTblDailyOpeningBalance);
            xrTblDailyClosingBalance = SetBalanceTableBorders(xrTblDailyClosingBalance);
            xrtblBindSource = SetBorders(xrtblBindSource, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTblTotal = SetHeadingTableBorder(xrTblTotal, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);

            xrTblClosingBalance = SetBorders(xrTblClosingBalance, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrtblGrandTotal = SetGrandTotalTableBorders(xrtblGrandTotal);

        }
        #endregion

        #region Events
        private void xrtblCashDailyOPBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            if (DailyGroupNumber == 2)
            {
                DailyGrpCashOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPCashBalance.Value.ToString());
                DailyGrpBankOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString());
            }
            else
            {
                DailyGrpCashOpbalance = DailyGrpCashClbalance;
                DailyGrpBankOpbalance = DailyGrpBankClbalance;
            }
            e.Result = DailyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyOPBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrtblCashDailyOPBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGrpCashOpbalance = DailyGrpBankOpbalance = 0;
        }

        private void xrtblDailyRecCashBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGroupNumber++;
            DailyCashReceipts = DailyCashPayments = DailyBankReceipts = DailyBankPayments = 0;
        }

        private void xrtblDailyRecCashBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            DailyCashReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.CASHColumn.ColumnName).ToString());
            DailyCashPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_CASHColumn.ColumnName).ToString());
            DailyBankReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.BANKColumn.ColumnName).ToString());
            DailyBankPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_BANKColumn.ColumnName).ToString());
        }

        private void xrtblDailyRecCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            DailyGrpCashClbalance = (DailyGrpCashOpbalance + DailyCashReceipts) - DailyCashPayments;
            DailyGrpBankClbalance = (DailyGrpBankOpbalance + DailyBankReceipts) - DailyBankPayments;
            e.Result = DailyCashReceipts + DailyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyRecBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyBankReceipts + DailyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyPayCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyCashPayments + DailyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrtblDailyPayBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyBankPayments + DailyGrpBankClbalance;
            e.Handled = true;
        }

        private void xrtblDailyCLCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrtblDailyCLBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpBankClbalance;
            e.Handled = true;
        }

        private void xrPayment_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentCash = this.ReportProperties.NumberSet.ToDouble(xrRec_Cash.Text);
            if (paymentCash != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrRec_Cash.Text = "";
            }
        }

        private void xrPay_Cash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentBank = this.ReportProperties.NumberSet.ToDouble(xrRec_Bank.Text);
            if (paymentBank != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrRec_Bank.Text = "";
            }
        }

        private void xrPaymentCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentCash = this.ReportProperties.NumberSet.ToDouble(xrPaymentCash.Text);
            if (paymentCash != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPaymentCash.Text = "";
            }
        }

        private void xrPaymentBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentBank = this.ReportProperties.NumberSet.ToDouble(xrPaymentBank.Text);
            if (paymentBank != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPaymentBank.Text = "";
            }

        }
        #endregion

        private void xtNarrRec_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrNarrPay_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            xrtblBindSource = AlignTable(xrtblBindSource, Narration, Narration_Pay, count);
        }

        private void xrReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrReceipt.Text.Equals("Opening Balance"))
            {
                if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
                {
                    xrReceipt.NavigateUrl = PagePath.VoucherViewPath + "?VoucherId=" + GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString()
                        + "&BranchId=" + GetCurrentColumnValue(reportSetting1.Ledger.BRANCH_IDColumn.ColumnName) + "";
                    xrReceipt.Target = "_search";
                }
            }
        }

        private void xrPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrPayments.Text.Equals("Opening Balance"))
            {
                if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
                {
                    xrPayments.NavigateUrl = PagePath.VoucherViewPath + "?VoucherId=" + GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString()
                        + "&BranchId=" + GetCurrentColumnValue(reportSetting1.Ledger.BRANCH_IDColumn.ColumnName) + "";
                    xrPayments.Target = "_search";
                }
            }
        }
    }
}

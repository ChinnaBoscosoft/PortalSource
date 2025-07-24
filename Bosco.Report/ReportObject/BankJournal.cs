using System;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections.Generic;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class BankJournal : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        int DailyGroupNumber = 0;
        double DailyGrpOpbalance = 0;
        double DailyGrpClbalance = 0;
        double DailyReceipts = 0;
        double DailyPayments = 0;

        string LedgerDate = string.Empty;
        string datefrom = string.Empty;
        string dateto = string.Empty;

        int count = 0;
        #endregion

        #region Constructor
        public BankJournal()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrtblSource.WidthF = xrTable1.WidthF = xrTable2.WidthF = xrTable3.WidthF = xrTable7.WidthF = 670.25f;
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                    LedgerDate = dicDDProperties[this.reportSetting1.CashBankFlow.DATEColumn.ColumnName].ToString();
                    this.ReportProperties.BankAccount = "0";
                    datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                datefrom = this.ReportProperties.DateFrom;
                dateto = this.ReportProperties.DateTo;
            }

            DailyGroupNumber = 0;
            DailyGrpOpbalance = 0;
            DailyGrpClbalance = 0;

            DailyReceipts = 0;
            DailyPayments = 0;
            BankJournalReport();
            base.ShowReport();
        }

        private void BankJournalReport()
        {

            if (string.IsNullOrEmpty(datefrom) ||
                string.IsNullOrEmpty(dateto) ||
                this.ReportProperties.Project == "0")
            {
                if (string.IsNullOrEmpty(this.ReportProperties.BankAccount))
                {
                    if (LedgerDate == string.Empty)
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
            }
            else
            {
                xrCapReceipts.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.RECEIPT);
                xrCapPayments.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PAYMENTS);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;

                this.HideCostCenter = ReportProperties.Count == 1 ? true : false;
                this.CosCenterName = ReportProperties.Count == 1 ? ReportProperties.BankAccountName : " ";

                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();

                //show daiy balance
                grpheaderVoucherDate.Visible = (this.ReportProperties.ShowDailyBalance == 1);
                grpfooterVoucherDate.Visible = grpheaderVoucherDate.Visible;

                ReportFooter.Visible = (this.ReportProperties.ShowDailyBalance == 0);
                xrtblCLBalance.Visible = grpheaderVoucherBalance.Visible = ReportFooter.Visible;

                SetReportProperty();
                prOPBalance.Visible = prCLBalance.Visible = false;
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    AcMEDSync.Model.BalanceProperty bankop = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, this.ReportProperties.Ledger, datefrom,
                                         BalanceSystem.BalanceType.OpeningBalance);

                    AcMEDSync.Model.BalanceProperty FDOpeBal = balanceSystem.GetFDBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, datefrom,
                    BalanceSystem.BalanceType.OpeningBalance);

                    prOPBalance.Value = bankop.TransMode == TransactionMode.CR.ToString() ? -bankop.Amount : bankop.Amount; // +FDOpeBal.Amount;

                    AcMEDSync.Model.BalanceProperty bankcl = balanceSystem.GetBankBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, datefrom,
                                         BalanceSystem.BalanceType.ClosingBalance);
                    AcMEDSync.Model.BalanceProperty FDClBal = balanceSystem.GetFDBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, datefrom,
                    BalanceSystem.BalanceType.ClosingBalance);
                    prCLBalance.Value = bankcl.TransMode == TransactionMode.CR.ToString() ? -bankcl.Amount : bankcl.Amount; // +FDClBal.Amount;

                }
                DataTable dtCashBankBook = GetReportSource();
                if (dtCashBankBook != null)
                {
                    this.DataSource = dtCashBankBook;
                    this.DataMember = dtCashBankBook.TableName;
                }

                if (this.ReportProperties.IncludeNarration == 1)
                {
                }
                else
                {

                }
            }
        }

        private DataTable GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankJournal);
                using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.Parameters.Add(this.ReportParameters.COUNTColumn, this.ReportProperties.Count);

                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankBookQueryPath);
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

        private void xrtblDailyOpBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGrpOpbalance = 0;
        }

        private void xrtblDailyOpBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            if (DailyGroupNumber == 0)
            {
                DailyGrpOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
                DailyGroupNumber++;
            }
            else
            {
                DailyGrpOpbalance = DailyGrpClbalance;
                DailyGrpClbalance = 0;
            }
            e.Result = DailyGrpOpbalance;
            e.Handled = true;
        }

        private void xtrtblDailyRecTotal_SummaryReset(object sender, EventArgs e)
        {

            DailyReceipts = DailyPayments = 0;
        }

        private void xtrtblDailyRecTotal_SummaryRowChanged(object sender, EventArgs e)
        {
            DailyReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.RECEIPTColumn.ColumnName).ToString());
            DailyPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAYMENTColumn.ColumnName).ToString());
        }

        private void xtrtblDailyRecTotal_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyReceipts + DailyGrpOpbalance;
            e.Handled = true;
        }

        private void xrDailyClosingBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            DailyGrpClbalance = (DailyGrpOpbalance + DailyReceipts) - DailyPayments;
            //if (this.ReportProperties.ShowDailyBalance == 1) { prCLBalance.Value = DailyGrpClbalance; }
            prCLBalance.Value = DailyGrpClbalance;
            e.Result = DailyGrpClbalance;
            e.Handled = true;
        }

        private void xtrtblDailyPayTotal_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyPayments + DailyGrpClbalance;
            e.Handled = true;
        }

        private void xttblOpBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void SetReportProperty()
        {
            float actualCodeWidth = xrCapLedgerCode.WidthF;
            bool isCapCodeVisible = true;

            //Include / Exclude Code
            if (xrCapLedgerCode.Tag != null && xrCapLedgerCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapLedgerCode.Tag.ToString());
            }
            else
            {
                xrCapLedgerCode.Tag = xrCapLedgerCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell24.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell29.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell50.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell33.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell35.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell39.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell41.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);

            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable7 = SetBorders(xrTable7, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable2 = SetBorders(xrTable2, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable3 = SetBorders(xrTable3, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
        }

        private void xrReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }



        private void xrtblSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblSource = AlignTable(xrtblSource, Narration, string.Empty, count);
        }

        private void xrLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrLedger.Text.Equals("Opening Balance"))
            {
                if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
                {
                    xrLedger.NavigateUrl = PagePath.VoucherViewPath + "?VoucherId=" + GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString()
                       + "&BranchId=" + GetCurrentColumnValue(reportSetting1.Ledger.BRANCH_IDColumn.ColumnName) + "";
                    xrLedger.Target = "_search";
                }
            }
        }

    }
}

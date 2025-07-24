using System;
using Bosco.Report.Base;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using AcMEDSync.Model;


namespace Bosco.Report.ReportObject
{
    public partial class CashJournal : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;

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
        public CashJournal()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrBindSource.WidthF = xrTable1.WidthF = xrTable2.WidthF = xrTable3.WidthF = xrTable4.WidthF = 670.25f;
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
                    datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                datefrom = this.ReportProperties.DateFrom;
                dateto = this.ReportProperties.DateTo;
            }

            CashJournalReport();

            DailyGroupNumber = 0;
            DailyGrpOpbalance = 0;
            DailyGrpClbalance = 0;

            DailyReceipts = 0;
            DailyPayments = 0;

            base.ShowReport();
        }

        private void CashJournalReport()
        {
            if (string.IsNullOrEmpty(datefrom) || string.IsNullOrEmpty(dateto) || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrCapPayments.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PAYMENTS);
                xrCapReceipt.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.RECEIPT);

                //show daiy balance
                grpheaderVoucherDate.Visible = (this.ReportProperties.ShowDailyBalance == 1);
                grpfooterVoucherDate.Visible = grpheaderVoucherDate.Visible;

                grpOPBalance.Visible = (this.ReportProperties.ShowDailyBalance == 0);
                xrTableRow8.Visible = ReportFooter.Visible = grpOPBalance.Visible;

                prOPBalance.Visible = prCLBalance.Visible = false;

                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    AcMEDSync.Model.BalanceProperty cashop = balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, datefrom,
                        BalanceSystem.BalanceType.OpeningBalance);
                    prOPBalance.Value = cashop.TransMode == TransMode.CR.ToString() ? -cashop.Amount : cashop.Amount;

                    AcMEDSync.Model.BalanceProperty cashcl = balanceSystem.GetCashBalance(this.ReportProperties.BranchOffice, this.ReportProperties.Project, dateto,
                                         BalanceSystem.BalanceType.ClosingBalance);
                    prCLBalance.Value = cashcl.TransMode == TransMode.CR.ToString() ? -cashcl.Amount : cashcl.Amount;
                }
                //Attach CostCenter
                SetReportProperty();

                DataTable dtCashBankBook = GetReportSource();
                if (dtCashBankBook != null)
                {
                    this.DataSource = dtCashBankBook;
                    this.DataMember = dtCashBankBook.TableName;
                }
            }
        }

        private DataTable GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashJournal);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.CashJournal,DataBaseType.HeadOffice))
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
            //DailyGroupNumber++;
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
            ////if (this.ReportProperties.ShowDailyBalance == 1) { prCLBalance.Value = DailyGrpClbalance; }
            ////e.Result = DailyGrpClbalance;
            prCLBalance.Value = DailyGrpClbalance;
            e.Result = DailyGrpClbalance;
            e.Handled = true;
        }

        private void xtrtblDailyPayTotal_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyPayments + DailyGrpClbalance;
            e.Handled = true;
        }

        private void xrtblOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void SetReportProperty()
        {
            //    float actualCCWidth = xrtblCostCentreCaption.WidthF;
            //    float actualLedgerWidth = xrCapLedger.WidthF;
            //     float actualCodeWidth = xrCapCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            //Include / Exclude CostCentre
            //if (xrtblCostCentreCaption.Tag != null && xrtblCostCentreCaption.Tag.ToString() != "")
            //{
            //    actualCCWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrtblCostCentreCaption.Tag.ToString());
            //}
            //else
            //{
            //    xrtblCostCentreCaption.Tag = xrtblCostCentreCaption.WidthF;
            //}

            //if (xrCapLedger.Tag != null && xrCapLedger.Tag.ToString() != "")
            //{
            //    actualLedgerWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapLedger.Tag.ToString());
            //}
            //else
            //{
            //    xrCapLedger.Tag = xrCapLedger.WidthF;
            //}

            //if (ReportProperties.IncludeCostCentre == 1)
            //{
            //    xrtblCostCentreCaption.Visible = xrtblCostCentreCaption.Visible = true;
            //    xrtblCostCentreName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            //| DevExpress.XtraPrinting.BorderSide.Bottom))); ;
            //    xrtblCostCentreCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
            //| DevExpress.XtraPrinting.BorderSide.Bottom))); ;
            //}
            //else
            //{
            //    xrtblCostCentreCaption.Visible = xrtblCostCentreCaption.Visible = false;
            //    xrtblCostCentreName.Borders = xrtblCostCentreCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //}
            //xrtblCostCentreName.WidthF = xrtblCostCentreCaption.WidthF = ((xrtblCostCentreCaption.Visible) ? actualCCWidth : (float)0.0);
            //xrCapLedger.WidthF = xrLedgerName.WidthF = ((xrtblCostCentreCaption.Visible) ? actualLedgerWidth : actualLedgerWidth + actualCCWidth);


            //if (xrCapCode.Tag != null && xrCapCode.Tag.ToString() != "")
            //{
            //    actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapCode.Tag.ToString());
            //}
            //else
            //{
            //    xrCapCode.Tag = xrCapCode.WidthF;
            //}

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            //xrCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrtblaLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell12.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell15.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell30.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell19.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell20.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell26.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            //xrTableCell27.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);

            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable4 = SetBorders(xrTable4, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable2 = SetBorders(xrTable2, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            //xrBindSource = SetBorders(xrBindSource, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable3 = SetBorders(xrTable3, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
        }

        private void xrReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double receiptAmt = this.ReportProperties.NumberSet.ToDouble(xrReceipt.Text);
            if (receiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrReceipt.Text = "";
            }
        }

        private void xrPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentAmt = this.ReportProperties.NumberSet.ToDouble(xrPayments.Text);
            if (paymentAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPayments.Text = "";
            }
        }
               
        private void xrBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // SetTableborder(0);
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            AlignTable(xrBindSource, string.Empty, Narration,count);
        }

        private void CashJournal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrLedgerName.Text.Equals("Opening Balance"))
            {
                if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
                {
                    xrLedgerName.NavigateUrl = PagePath.VoucherViewPath + "?VoucherId=" + GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString()
                        + "&BranchId=" + GetCurrentColumnValue(reportSetting1.Ledger.BRANCH_IDColumn.ColumnName) + "";
                    xrLedgerName.Target = "_search";
                }
            }
        }
    }
}

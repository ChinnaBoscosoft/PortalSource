using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;


namespace Bosco.Report.ReportObject
{
    public partial class Ledger : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Ledger()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrTable4.WidthF = xrTable3.WidthF = xrTable7.WidthF = xrTable2.WidthF = xrGrouptotal.WidthF = xrTable1.WidthF = 670.25f;
        }
        #endregion

        #region Variables
        double LedgerDebit = 0;
        double LedgerCredit = 0;
        double LedgerDebitSum = 0;
        int MonthlyGroupNumber = 0;
        double GroupDetbitSum = 0;
        double GroupCreditSum = 0;
        double MonthlyOpeningBalance = 0;
        double MonthlyClosingBalance = 0;
        double MonthlyPriousClBalance = 0;

        public double GrandCreditAmount { get; set; }
        public double GrandDebitAmount { get; set; }
        string OPBalanceDate = string.Empty;

        string VoucherType = "RC','PY";
        Int32 LedgerId = 0;
        string CostCenter = string.Empty;
        string RptAsOnDate = string.Empty;

        int count = 0;
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            //MonthlyGroupNumber = 0;
            LedgerDebit = 0;
            LedgerCredit = 0;
            MonthlyOpeningBalance = 0;
            MonthlyClosingBalance = 0;
            GroupDetbitSum = 0;
            GroupCreditSum = 0;
            xrtblClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = "";

            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                switch (ddtypeLinkType)
                {
                    case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                        VoucherType = "RC";
                        break;
                    case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                        VoucherType = "PY";
                        break;
                }

                if (dicDDProperties.ContainsKey("LEDGER_ID"))
                    LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["LEDGER_ID"].ToString());

                if (dicDDProperties.ContainsKey("PARTICULARS_ID"))
                    LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["PARTICULARS_ID"].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                    CostCenter = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();

                if (dicDDProperties.ContainsKey(this.ReportParameters.DATE_AS_ONColumn.ColumnName))
                    RptAsOnDate = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();
            }

            BindReport();
            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindReport()
        {
            if ((string.IsNullOrEmpty(this.ReportProperties.DateFrom) ||
                string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.Ledger == "0") && (LedgerId == 0))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (IsDrillDownMode)
                    this.ReportTitle = GetLedgerName(LedgerId);
                else
                    this.ReportTitle = objReportProperty.ReportTitle;

                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 605.25f;
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                grpLedgerGroup.Visible = false;
                if (this.ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpLedgerGroup.Visible = true;
                }

                xrCapDebit.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.DEBIT);
                xrCapCredit.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CREDIT);
                xrCapclosingBalance.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CLOSINGBALANCE);
                prOPBalance.Value = "0.00";


                grpHeaderVoucherMth.Visible = (this.ReportProperties.IncludeLedgerGroupTotal == 1);
                grpLedgerFooter.Visible = grpHeaderVoucherMth.Visible;
                grpFoooterDate.Visible = (this.ReportProperties.IncludeLedgerGroupTotal == 0);
                prOPBalance.Visible = false;
                ResultArgs resultArgs = BindLedgerSource();
                if (resultArgs != null && resultArgs.DataSource.TableView.ToTable().Rows.Count != 0)
                {
                    GrandCreditAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.TableView.ToTable().Compute("SUM(CREDIT)", "").ToString());
                    GrandDebitAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.TableView.ToTable().Compute("SUM(DEBIT)", "").ToString());
                    if (GrandDebitAmount >= GrandCreditAmount)
                    {
                        xrGrandCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandCreditAmount + (GrandDebitAmount - GrandCreditAmount));
                        xrGrandDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandDebitAmount);
                    }
                    else
                    {
                        xrGrandDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandDebitAmount + (GrandCreditAmount - GrandDebitAmount));
                        xrGrandCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandCreditAmount);
                    }
                }
                DataView dtview = resultArgs.DataSource.TableView;
                if (dtview != null)
                {
                    Detail.SortFields.Add(new GroupField("DATE"));
                    dtview.Table.TableName = "Ledger";
                    this.DataSource = dtview;
                    this.DataMember = dtview.Table.TableName;

                }
                if (this.ReportProperties.IncludeNarration == 1)
                {

                }
                else
                {

                }
            }
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable3 = SetBorders(xrTable3, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable7 = SetBorders(xrTable7, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrGrouptotal = SetBorders(xrGrouptotal, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);

        }
        public ResultArgs BindLedgerSource()
        {
            ResultArgs resultArgs = null;
            string Test = this.GetReportSQL(SQL.ReportSQLCommand.Report.Ledger);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Report.Ledger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!string.IsNullOrEmpty(this.ReportProperties.BranchOffice) && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                if (RptAsOnDate != string.Empty)
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, RptAsOnDate);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                }
                if (!string.IsNullOrEmpty(CostCenter))
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCenter);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, (LedgerId == 0) ? this.ReportProperties.Ledger : LedgerId.ToString());
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherType);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Test);
            }
            return resultArgs;
        }

        #endregion

        private void xrtblLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            GroupDetbitSum += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            GroupCreditSum += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
        }

        private void xrtblLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text);
            e.Handled = true;
        }

        private void xrtblLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            GroupDetbitSum = GroupCreditSum = 0;
        }

        private void xttblOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (MonthlyGroupNumber == 0)
            {
                e.Result = "0.00";
                MonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text);
                MonthlyGroupNumber++;
                e.Handled = true;
            }
            else
            {
                if (this.ReportProperties.IncludeLedgerGroupTotal == 1) { grpHeaderVoucherMth.Visible = true; }
                e.Result = MonthlyClosingBalance;// LedgerDebit =
                MonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text);
                e.Handled = true;

            }
        }

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrDebit.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrDebit.Text = "";
            }
        }

        private void xrCrdit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double creditAmt = this.ReportProperties.NumberSet.ToDouble(xrCrdit.Text);
            if (creditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCrdit.Text = "";
            }
        }

        private void xrtblLedgerCreditBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = LedgerCredit;
            e.Handled = true;
        }

        private void xrLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            LedgerDebit += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            LedgerCredit += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
            if ((LedgerDebit - LedgerCredit) < 0)
            {
                string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs((LedgerDebit - LedgerCredit))).ToString();
                xrtblClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = amt + " Cr";
            }
            else
            {
                xrtblClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = this.UtilityMember.NumberSet.ToNumber(LedgerDebit - LedgerCredit).ToString() + " Dr";
            }

            OPBalanceDate = (GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName).ToString();
            if (!string.IsNullOrEmpty(OPBalanceDate))
            {
                string dtTemp = this.UtilityMember.DateSet.ToDate(OPBalanceDate, false).Year + "-" +
                        this.UtilityMember.DateSet.ToDate(OPBalanceDate, false).Month + "-" + 1;
                xrOPBalanceCaption.Text = "Opening Balance As On  " + this.UtilityMember.DateSet.ToDate(dtTemp, "dd-MM-yyyy");
            }
            else
            {
                xrOPBalanceCaption.Text = "Opening Balance";
            }
        }

        private void xrlblLedgerGroup_SummaryReset(object sender, EventArgs e)
        {
            LedgerDebit = LedgerCredit = 0;
            MonthlyClosingBalance = 0;
        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrTable2 = AlignTable(xrTable2, Narration, string.Empty, count);
        }

        private void xrTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrTransMode.Text.Equals("Opening Balance"))
            {
                if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
                {
                    xrTransMode.NavigateUrl = PagePath.VoucherViewPath + "?VoucherId=" + GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString()
                        + "&BranchId=" + GetCurrentColumnValue(reportSetting1.Ledger.BRANCH_IDColumn.ColumnName) + "";
                    xrTransMode.Target = "_search";
                }
            }
        }
    }
}

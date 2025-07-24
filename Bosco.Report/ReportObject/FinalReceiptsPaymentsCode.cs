using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FinalReceiptsPaymentsCode : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public FinalReceiptsPaymentsCode()
        {
            InitializeComponent();
        }

        #endregion

        #region Decelartion
        double ReceiptsAmt = 0;
        double PaymentsAmt = 0;
        double ReceiptOPAmt = 0;
        double PaymentClAmt = 0;
        float ReceiptCodeWidth;
        float ReceiptNameWidth;
        float ReceiptAmountWidth;
        float PaymentCodeWidth;
        float PaymentNameWidth;
        float PaymentAmountWidth;
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            ReceiptCodeWidth = xrHeadReceiptCode.WidthF;
            if (xrHeadReceiptCode.Tag != null && xrHeadReceiptCode.Tag.ToString() != "")
            {
                ReceiptCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadReceiptCode.Tag.ToString());
            }
            else
            {
                xrHeadReceiptCode.Tag = xrHeadReceiptCode.WidthF;
            }
            ReceiptNameWidth = xrHeadLedgerName.WidthF;
            if (xrHeadLedgerName.Tag != null && xrHeadLedgerName.Tag.ToString() != "")
            {
                ReceiptNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadLedgerName.Tag.ToString());
            }
            else
            {
                xrHeadLedgerName.Tag = xrHeadLedgerName.WidthF;
            }
            ReceiptAmountWidth = xrHeadReceiptAmount.WidthF;
            if (xrHeadReceiptAmount.Tag != null && xrHeadReceiptAmount.Tag.ToString() != "")
            {
                ReceiptAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadReceiptAmount.Tag.ToString());
            }
            else
            {
                xrHeadReceiptAmount.Tag = xrHeadReceiptAmount.WidthF;
            }
            PaymentCodeWidth = xrHeadPaymentCode.WidthF;
            if (xrHeadPaymentCode.Tag != null && xrHeadPaymentCode.Tag.ToString() != "")
            {
                PaymentCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadPaymentCode.Tag.ToString());
            }
            else
            {
                xrHeadPaymentCode.Tag = xrHeadReceiptCode.WidthF;
            }
            PaymentNameWidth = xrHeadPaymentLedgerName.WidthF;
            if (xrHeadPaymentLedgerName.Tag != null && xrHeadPaymentLedgerName.Tag.ToString() != "")
            {
                PaymentNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadPaymentLedgerName.Tag.ToString());
            }
            else
            {
                xrHeadPaymentLedgerName.Tag = xrHeadPaymentLedgerName.WidthF;
            }
            PaymentAmountWidth = xrHeadPaymentAmount.WidthF;
            if (xrHeadPaymentAmount.Tag != null && xrHeadPaymentAmount.Tag.ToString() != "")
            {
                PaymentAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadPaymentAmount.Tag.ToString());
            }
            else
            {
                xrHeadPaymentAmount.Tag = xrHeadPaymentAmount.WidthF;
            }
            BindReceiptPaymentsSource();


        }
        #endregion

        #region Methods

        public void BindReceiptPaymentsSource()
        {
            this.ReportProperties.ShowGroupCode = (this.LoginUser.IS_CMFCHE_CONGREGATION || this.LoginUser.IS_SAP_CONGREGATION) ? 1 : 0;
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = xrtableHeaderCaption.WidthF;
            xrHeadPaymentAmount.Text = this.SetCurrencyFormat(xrHeadPaymentAmount.Text);
            xrHeadReceiptAmount.Text = this.SetCurrencyFormat(xrHeadReceiptAmount.Text);
            //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            // this.ReportTitle = this.ReportProperties.ReportTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            //  this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ReceiptsCode receiptLedgers = xrSubReceipts.ReportSource as ReceiptsCode;
            receiptLedgers.HideReceiptReportHeader();
            PaymentsCode paymentLedgers = xrSubPayments.ReportSource as PaymentsCode;
            paymentLedgers.HidePaymentReportHeader();

            this.AttachDrillDownToSubReport(receiptLedgers);
            this.AttachDrillDownToSubReport(paymentLedgers);

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                                || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                AccountBalanceCode accountBalance = xrSubOpeningBalance.ReportSource as AccountBalanceCode;
                accountBalance.BindBalance(true, true);

                accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                accountBalance.AmountProgressiveColumnWidth = 0;
                accountBalance.AmountProgressVisible = false;
                accountBalance.GroupProgressVisible = false;
                ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
                receiptLedgers.BindReceiptSource(TransType.RC);
                ReceiptsAmt = receiptLedgers.ReceiptAmount;
                receiptLedgers.PaymentCostCentreNameVisible = false;
                receiptLedgers.CostCentreCategoryNameWidth = xrHeadReceiptCode.WidthF + xrHeadLedgerName.WidthF + xrHeadReceiptAmount.WidthF;
                receiptLedgers.PaymentCostCentreNameVisible = false;

                if (ReportProperties.ShowLedgerCode == 1)
                {
                    accountBalance.CodeColumnWidth = ReceiptCodeWidth;
                    // accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;// ReceiptNameWidth + 1;
                    accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;// ReceiptNameWidth + 1;
                    receiptLedgers.CodeColumnWidth = ReceiptCodeWidth;
                    //  receiptLedgers.NameColumnWidth = ReceiptNameWidth-3;
                    receiptLedgers.NameColumnWidth = ReceiptNameWidth - 1;
                    receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                    accountBalance.AmountColumnWidth = ReceiptAmountWidth;

                    accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                }
                else
                {
                    accountBalance.CodeColumnWidth = 0;
                    accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;
                    receiptLedgers.CodeColumnWidth = 0;
                    receiptLedgers.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                    receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                    accountBalance.AmountColumnWidth = ReceiptAmountWidth - 2;

                    accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                }
                if (ReportProperties.ShowGroupCode == 1)    // Need to Include the Parent Group also to set the width
                {
                    accountBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                    accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth;    // ReceiptNameWidth + ReceiptCodeWidth;
                    accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;
                    receiptLedgers.GroupCodeColumnWidth = receiptLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth;
                    receiptLedgers.GroupNameColumnWidth = receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth - 1;
                    receiptLedgers.GroupAmountColumnWidth = receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;
                    accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;

                    accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                }
                else
                {
                    accountBalance.CodeHeaderColumWidth = 0F;
                    accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth + 2;
                    // chinna 30.03.2023
                    receiptLedgers.GroupCodeColumnWidth = receiptLedgers.ParentGroupCodeColumnWidth = 0.0F;
                    receiptLedgers.GroupNameColumnWidth = receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;
                    receiptLedgers.GroupAmountColumnWidth = receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth + 1; // aldrin
                    accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth + 1;// aldrin
                    if (ReportProperties.ShowByLedgerGroup == 1)
                    {
                        receiptLedgers.GroupNameColumnWidth = receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 5;
                        receiptLedgers.GroupAmountColumnWidth = receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;
                        //receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 5;
                        //receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;
                    }

                    accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                }

                paymentLedgers.BindPaymentSource(TransType.PY);
                PaymentsAmt = paymentLedgers.PaymentAmout;
                AccountBalanceCode accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalanceCode;
                accountClosingBalance.BindBalance(false, true);
                PaymentClAmt = accountClosingBalance.PeriodBalanceAmount;
                paymentLedgers.CostCentreNameVisible = false;

                paymentLedgers.CategoryNameWidth = xrHeadPaymentCode.WidthF + xrHeadPaymentAmount.WidthF + xrHeadPaymentLedgerName.WidthF;

                accountClosingBalance.AmountProgressiveColumnWidth = 0;
                accountClosingBalance.AmountProgressiveHeaderColumnWidth = 0;
                accountClosingBalance.AmountProgressVisible = false;
                accountClosingBalance.GroupProgressVisible = false;
                if (ReportProperties.ShowLedgerCode == 1)
                {//
                    if (ReportProperties.ShowByLedgerGroup == 1)
                    {
                        accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                        accountClosingBalance.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 2;// PaymentNameWidth;
                        paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                        paymentLedgers.NameColumnWidth = PaymentNameWidth + 0.5F;
                        paymentLedgers.AmountColumnWidth = PaymentAmountWidth - 1;
                        accountClosingBalance.AmountColumnWidth = PaymentAmountWidth - 2;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else
                    {
                        accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                        accountClosingBalance.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 2;// PaymentNameWidth;
                        paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                        paymentLedgers.NameColumnWidth = PaymentNameWidth + 1;
                        paymentLedgers.AmountColumnWidth = PaymentAmountWidth - 4; // aldrin
                        accountClosingBalance.AmountColumnWidth = PaymentAmountWidth - 2;// aldrin

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }

                }
                else
                {
                    accountClosingBalance.CodeColumnWidth = 0;
                    accountClosingBalance.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 2;
                    paymentLedgers.CodeColumnWidth = 0;
                    paymentLedgers.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 3;
                    paymentLedgers.AmountColumnWidth = PaymentAmountWidth;
                    accountClosingBalance.AmountColumnWidth = PaymentAmountWidth - 2;

                    accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;

                }
                if (ReportProperties.ShowGroupCode == 1)
                {//
                    if (ReportProperties.ShowByLedgerGroup == 1)
                    {
                        accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                        accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth + -2; // PaymentNameWidth - 2;
                        accountClosingBalance.NameColumnWidth = PaymentNameWidth + ReceiptCodeWidth + -2;
                        paymentLedgers.GroupCodeColumnWidth = paymentLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth - 2;
                        paymentLedgers.GroupNameColumnWidth = paymentLedgers.ParentGroupColumWidth = PaymentNameWidth + 1;
                        paymentLedgers.GroupAmountColumnWidth = paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth - 1;//  +0.5F; aldrin
                        accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth - 3;// aldrin;;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else
                    {
                        accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                        accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth + -2; // PaymentNameWidth - 2;
                        paymentLedgers.GroupCodeColumnWidth = paymentLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth - 2;
                        paymentLedgers.GroupNameColumnWidth = paymentLedgers.ParentGroupColumWidth = PaymentNameWidth;
                        paymentLedgers.GroupAmountColumnWidth = paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                        accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth + 1;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }

                }
                else
                {
                    if (ReportProperties.ShowByLedgerGroup == 1)
                    {
                        accountClosingBalance.CodeHeaderColumWidth = 0;
                        accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 2;
                        // chinna 30.03.2023
                        paymentLedgers.GroupCodeColumnWidth = paymentLedgers.ParentGroupCodeColumnWidth = 0.0F;
                        paymentLedgers.GroupNameColumnWidth = paymentLedgers.ParentGroupColumWidth = ReceiptCodeWidth + PaymentNameWidth - 4; // 
                        paymentLedgers.GroupAmountColumnWidth = paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth - 2;
                        if (ReportProperties.ShowDetailedBalance == 1)
                        {
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth + 2;
                        }
                        else
                        {
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth - 2;
                        }

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else if (ReportProperties.ShowByLedger == 1)
                    {
                        accountClosingBalance.CodeHeaderColumWidth = 0;
                        accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 2;
                        // chinna 31.03.2023
                        paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth; // paymentLedgers.ParentGroupCodeColumnWidth = 0;
                        paymentLedgers.GroupNameColumnWidth = paymentLedgers.ParentGroupColumWidth = PaymentNameWidth; //ReceiptCodeWidth +
                        paymentLedgers.GroupAmountColumnWidth = paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth - 2;
                        accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else
                    {
                        accountClosingBalance.CodeHeaderColumWidth = 0;
                        accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth;
                        // chinna 30.03.2023
                        paymentLedgers.GroupCodeColumnWidth = paymentLedgers.ParentGroupCodeColumnWidth = 0;
                        paymentLedgers.GroupNameColumnWidth = paymentLedgers.ParentGroupColumWidth = PaymentNameWidth + ReceiptCodeWidth;
                        paymentLedgers.GroupAmountColumnWidth = paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                        accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    }
                }

                //xrPaymentAmt.WidthF = 100;

                SetReportSetting();

                base.ShowReport();
            }
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrHeadReceiptCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrHeadReceiptCode.Tag != null && xrHeadReceiptCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeadReceiptCode.Tag.ToString());
            }
            else
            {
                xrHeadReceiptCode.Tag = xrHeadReceiptCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrHeadReceiptCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrHeadPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            //  this.ReportPeriod = this.ReportProperties.ReportDate;

            xrtableHeaderCaption = AlignHeaderTable(xrtableHeaderCaption);
            xrTblOpeningBalance = AlignOpeningBalanceTable(xrTblOpeningBalance);
            xtTblClosingBalance = AlignClosingBalance(xtTblClosingBalance);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrCrossBandLine1.ForeColor = xrCrossBandLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }

        public override XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignOpeningBalanceTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignClosingBalance(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        #endregion

        private void xrReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptOPAmt + ReceiptsAmt;
            e.Handled = true;
        }

        private void xrPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentClAmt + PaymentsAmt;
            e.Handled = true;
        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void SetTotalTableBorders()
        {
            foreach (XRTableRow trow in xrtblGrandTotal.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }

        }
    }
}

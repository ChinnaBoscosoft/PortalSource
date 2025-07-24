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
    public partial class CCReceiptsPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public CCReceiptsPayments()
        {
            InitializeComponent();
        }

        #endregion

        #region Decelartion
        double ReceiptsAmt = 0;
        double PaymentsAmt = 0;
        Receipts receiptLedgers;
        Payments paymentLedgers;
        //  double ReceiptOPAmt = 0;
        //   double PaymentClAmt = 0;
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            BindReceiptPaymentsSource();

        }
        #endregion

        #region Methods

        public void BindReceiptPaymentsSource()
        {
            //  this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            xrCapPaymentAmount.Text = this.SetCurrencyFormat(xrCapPaymentAmount.Text);
            xrCapReceiptAmount.Text = this.SetCurrencyFormat(xrCapReceiptAmount.Text);
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            this.CosCenterName = objReportProperty.CostCentreName;
            this.ReportTitle = this.ReportProperties.ReportTitle;
            receiptLedgers = xrCosSubReceipts.ReportSource as Receipts;
            this.AttachDrillDownToSubReport(receiptLedgers);
            receiptLedgers.HideReceiptReportHeader();
            paymentLedgers = xrCosSubPayments.ReportSource as Payments;
            this.AttachDrillDownToSubReport(paymentLedgers);
            paymentLedgers.HidePaymentReportHeader();
            this.AttachDrillDownToSubReport(receiptLedgers);
            this.AttachDrillDownToSubReport(paymentLedgers);

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                receiptLedgers.BindReceiptSource(TransType.CRC);
                ReceiptsAmt = receiptLedgers.ReceiptAmount;

                paymentLedgers.BindPaymentSource(TransType.CPY);
                PaymentsAmt = paymentLedgers.PaymentAmout;
                float ReceiptCodeWidth = xrCapReceiptCode.WidthF;
                if (xrCapReceiptCode.Tag != null && xrCapReceiptCode.Tag.ToString() != "")
                {
                    ReceiptCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptCode.Tag.ToString());
                }
                else
                {
                    xrCapReceiptCode.Tag = xrCapReceiptCode.WidthF;
                }

                float ReceiptNameWidth = xrReceiptLedgerName.WidthF;
                if (xrReceiptLedgerName.Tag != null && xrReceiptLedgerName.Tag.ToString() != "")
                {
                    ReceiptNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrReceiptLedgerName.Tag.ToString());
                }
                else
                {
                    xrReceiptLedgerName.Tag = xrReceiptLedgerName.WidthF;
                }

                float ReceiptAmountWidth = xrCapReceiptAmount.WidthF;
                if (xrCapReceiptAmount.Tag != null && xrCapReceiptAmount.Tag.ToString() != "")
                {
                    ReceiptAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptAmount.Tag.ToString());
                }
                else
                {
                    xrCapReceiptAmount.Tag = xrCapReceiptAmount.WidthF;
                }

                if (ReportProperties.ShowLedgerCode == 1)
                {
                    receiptLedgers.CodeColumnWidth = ReceiptCodeWidth;
                    receiptLedgers.NameColumnWidth = ReceiptNameWidth;
                    receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;

                    receiptLedgers.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth + ReceiptAmountWidth;
                    receiptLedgers.CostCentreCategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;

                    paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                    paymentLedgers.NameColumnWidth = ReceiptNameWidth + 2;
                    paymentLedgers.AmountColumnWidth = ReceiptAmountWidth - 5;

                    paymentLedgers.CategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;
                    paymentLedgers.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth + ReceiptAmountWidth - 5;
                    xrGrandTotalPayment.WidthF = ReceiptCodeWidth;
                }
                else
                {
                    receiptLedgers.CodeColumnWidth = 0;
                    receiptLedgers.NameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                    receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;

                    receiptLedgers.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth + ReceiptAmountWidth;
                    receiptLedgers.CostCentreCategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;

                    paymentLedgers.CodeColumnWidth = 0;
                    paymentLedgers.NameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                    paymentLedgers.AmountColumnWidth = ReceiptAmountWidth - 5;

                    paymentLedgers.CategoryNameWidth = ReceiptCodeWidth + ReceiptNameWidth;
                    paymentLedgers.CostCentreWidth = ReceiptCodeWidth + ReceiptNameWidth + ReceiptAmountWidth - 5;
                }
                if (ReportProperties.ShowGroupCode == 1)
                {
                    receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                    receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                    receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;

                    paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                    paymentLedgers.GroupNameColumnWidth = ReceiptNameWidth + 2;
                    paymentLedgers.GroupAmountColumnWidth = ReceiptAmountWidth - 5;
                }
                else
                {
                    receiptLedgers.GroupCodeColumnWidth = 0;
                    receiptLedgers.GroupNameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                    receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;

                    paymentLedgers.GroupCodeColumnWidth = 0;
                    paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + ReceiptNameWidth - 2;
                    paymentLedgers.GroupAmountColumnWidth = ReceiptAmountWidth - 5;
                }
                SetReportSetting();
            }
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrCapReceiptCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapReceiptCode.Tag != null && xrCapReceiptCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptCode.Tag.ToString());
            }
            else
            {
                xrCapReceiptCode.Tag = xrCapReceiptCode.WidthF;
            }
            if (receiptLedgers.HideCostCentreReceipts != true && paymentLedgers.HideCostCentrePayments != true)
            {
                this.CosCenterName = objReportProperty.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapReceiptCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            setHeaderTableBorder();
            xrtblGrandTotal = SetGrandTotalTableBorders(xrtblGrandTotal);
            xrCrossBandLine1.ForeColor = xrCrossBandLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }
        private void setHeaderTableBorder()
        {
            foreach (XRTableRow trow in xrtblHeaderCaption.Rows)
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
        }

        #endregion

        #region Events

        private void xrCosReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptsAmt;
            e.Handled = true;
        }

        private void xrCosPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentsAmt;
            e.Handled = true;
        }

        #endregion


    }
}

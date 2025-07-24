namespace Bosco.Report.ReportObject
{
    partial class FinalReceiptsPayments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinalReceiptsPayments));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapReceiptCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReceiptAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpOpeningBalance = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblOpeningBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrOpeningBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubOpeningBalance = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubReceipts = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubPayments = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xtTblClosingBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrColClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotalReceipts = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotalPayment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymentAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubClosingBalance = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.clPaymentAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.clfReceiptAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrCrossBandLine1 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine2 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtableHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrHeadReceiptCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeadLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeadReceiptAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeadPaymentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeadPaymentLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeadPaymentAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblOpeningBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTblClosingBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtableHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubPayments,
            this.xrSubReceipts});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapReceiptCode,
            this.xrReceiptLedgerName,
            this.xrCapReceiptAmount,
            this.xrCapPaymentCode,
            this.xrCapPaymentLedgerName,
            this.xrCapPaymentAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapReceiptCode
            // 
            this.xrCapReceiptCode.Name = "xrCapReceiptCode";
            resources.ApplyResources(this.xrCapReceiptCode, "xrCapReceiptCode");
            // 
            // xrReceiptLedgerName
            // 
            this.xrReceiptLedgerName.Name = "xrReceiptLedgerName";
            resources.ApplyResources(this.xrReceiptLedgerName, "xrReceiptLedgerName");
            // 
            // xrCapReceiptAmount
            // 
            this.xrCapReceiptAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapReceiptAmount.Name = "xrCapReceiptAmount";
            this.xrCapReceiptAmount.StylePriority.UseBorders = false;
            this.xrCapReceiptAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapReceiptAmount, "xrCapReceiptAmount");
            // 
            // xrCapPaymentCode
            // 
            this.xrCapPaymentCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapPaymentCode.Name = "xrCapPaymentCode";
            this.xrCapPaymentCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapPaymentCode, "xrCapPaymentCode");
            // 
            // xrCapPaymentLedgerName
            // 
            this.xrCapPaymentLedgerName.Name = "xrCapPaymentLedgerName";
            resources.ApplyResources(this.xrCapPaymentLedgerName, "xrCapPaymentLedgerName");
            // 
            // xrCapPaymentAmount
            // 
            this.xrCapPaymentAmount.Name = "xrCapPaymentAmount";
            this.xrCapPaymentAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapPaymentAmount, "xrCapPaymentAmount");
            // 
            // grpOpeningBalance
            // 
            this.grpOpeningBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblOpeningBalance,
            this.xrSubOpeningBalance});
            resources.ApplyResources(this.grpOpeningBalance, "grpOpeningBalance");
            this.grpOpeningBalance.Name = "grpOpeningBalance";
            // 
            // xrTblOpeningBalance
            // 
            resources.ApplyResources(this.xrTblOpeningBalance, "xrTblOpeningBalance");
            this.xrTblOpeningBalance.Name = "xrTblOpeningBalance";
            this.xrTblOpeningBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblOpeningBalance.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable1_BeforePrint);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrOpeningBalance});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrOpeningBalance
            // 
            resources.ApplyResources(this.xrOpeningBalance, "xrOpeningBalance");
            this.xrOpeningBalance.Name = "xrOpeningBalance";
            this.xrOpeningBalance.StylePriority.UseFont = false;
            this.xrOpeningBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrSubOpeningBalance
            // 
            resources.ApplyResources(this.xrSubOpeningBalance, "xrSubOpeningBalance");
            this.xrSubOpeningBalance.Name = "xrSubOpeningBalance";
            this.xrSubOpeningBalance.ReportSource = new Bosco.Report.ReportObject.AccountBalance();
            // 
            // xrSubReceipts
            // 
            resources.ApplyResources(this.xrSubReceipts, "xrSubReceipts");
            this.xrSubReceipts.Name = "xrSubReceipts";
            this.xrSubReceipts.ReportSource = new Bosco.Report.ReportObject.Receipts();
            // 
            // xrSubPayments
            // 
            resources.ApplyResources(this.xrSubPayments, "xrSubPayments");
            this.xrSubPayments.Name = "xrSubPayments";
            this.xrSubPayments.ReportSource = new Bosco.Report.ReportObject.Payments();
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtTblClosingBalance,
            this.xrtblGrandTotal,
            this.xrSubClosingBalance});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xtTblClosingBalance
            // 
            resources.ApplyResources(this.xtTblClosingBalance, "xtTblClosingBalance");
            this.xtTblClosingBalance.Name = "xtTblClosingBalance";
            this.xtTblClosingBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrColClosingBalance});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrColClosingBalance
            // 
            resources.ApplyResources(this.xrColClosingBalance, "xrColClosingBalance");
            this.xrColClosingBalance.Name = "xrColClosingBalance";
            this.xrColClosingBalance.StylePriority.UseFont = false;
            this.xrColClosingBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrtblGrandTotal
            // 
            resources.ApplyResources(this.xrtblGrandTotal, "xrtblGrandTotal");
            this.xrtblGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotalReceipts,
            this.xrReceiptAmt,
            this.xrGrandTotalPayment,
            this.xrPaymentAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrGrandTotalReceipts
            // 
            this.xrGrandTotalReceipts.Name = "xrGrandTotalReceipts";
            this.xrGrandTotalReceipts.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalReceipts, "xrGrandTotalReceipts");
            // 
            // xrReceiptAmt
            // 
            this.xrReceiptAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.clfReceiptAmt")});
            this.xrReceiptAmt.Name = "xrReceiptAmt";
            this.xrReceiptAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrReceiptAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrReceiptAmt, "xrReceiptAmt");
            this.xrReceiptAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrReceiptAmt_SummaryGetResult);
            // 
            // xrGrandTotalPayment
            // 
            this.xrGrandTotalPayment.Name = "xrGrandTotalPayment";
            this.xrGrandTotalPayment.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalPayment, "xrGrandTotalPayment");
            // 
            // xrPaymentAmt
            // 
            this.xrPaymentAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.clPaymentAmt")});
            this.xrPaymentAmt.Name = "xrPaymentAmt";
            this.xrPaymentAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrPaymentAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrPaymentAmt, "xrPaymentAmt");
            this.xrPaymentAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrPaymentAmt_SummaryGetResult);
            // 
            // xrSubClosingBalance
            // 
            resources.ApplyResources(this.xrSubClosingBalance, "xrSubClosingBalance");
            this.xrSubClosingBalance.Name = "xrSubClosingBalance";
            this.xrSubClosingBalance.ReportSource = new Bosco.Report.ReportObject.AccountBalance();
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clPaymentAmt
            // 
            this.clPaymentAmt.DataMember = "Payments";
            this.clPaymentAmt.Name = "clPaymentAmt";
            // 
            // clfReceiptAmt
            // 
            this.clfReceiptAmt.DataMember = "Receipts";
            this.clfReceiptAmt.Name = "clfReceiptAmt";
            // 
            // xrCrossBandLine1
            // 
            this.xrCrossBandLine1.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine1, "xrCrossBandLine1");
            this.xrCrossBandLine1.Name = "xrCrossBandLine1";
            this.xrCrossBandLine1.StartBand = this.grpOpeningBalance;
            this.xrCrossBandLine1.WidthF = 1.000061F;
            // 
            // xrCrossBandLine2
            // 
            this.xrCrossBandLine2.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine2, "xrCrossBandLine2");
            this.xrCrossBandLine2.Name = "xrCrossBandLine2";
            this.xrCrossBandLine2.StartBand = this.Detail;
            this.xrCrossBandLine2.WidthF = 1F;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtableHeaderCaption});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrtableHeaderCaption
            // 
            resources.ApplyResources(this.xrtableHeaderCaption, "xrtableHeaderCaption");
            this.xrtableHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtableHeaderCaption.Name = "xrtableHeaderCaption";
            this.xrtableHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtableHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrtableHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtableHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtableHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtableHeaderCaption.StylePriority.UseBorders = false;
            this.xrtableHeaderCaption.StylePriority.UseFont = false;
            this.xrtableHeaderCaption.StylePriority.UsePadding = false;
            this.xrtableHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrHeadReceiptCode,
            this.xrHeadLedgerName,
            this.xrHeadReceiptAmount,
            this.xrHeadPaymentCode,
            this.xrHeadPaymentLedgerName,
            this.xrHeadPaymentAmount});
            this.xrTableRow5.Name = "xrTableRow5";
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // xrHeadReceiptCode
            // 
            this.xrHeadReceiptCode.Name = "xrHeadReceiptCode";
            resources.ApplyResources(this.xrHeadReceiptCode, "xrHeadReceiptCode");
            // 
            // xrHeadLedgerName
            // 
            this.xrHeadLedgerName.Name = "xrHeadLedgerName";
            resources.ApplyResources(this.xrHeadLedgerName, "xrHeadLedgerName");
            // 
            // xrHeadReceiptAmount
            // 
            this.xrHeadReceiptAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrHeadReceiptAmount.Name = "xrHeadReceiptAmount";
            this.xrHeadReceiptAmount.StylePriority.UseBorders = false;
            this.xrHeadReceiptAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrHeadReceiptAmount, "xrHeadReceiptAmount");
            // 
            // xrHeadPaymentCode
            // 
            this.xrHeadPaymentCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrHeadPaymentCode.Name = "xrHeadPaymentCode";
            this.xrHeadPaymentCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrHeadPaymentCode, "xrHeadPaymentCode");
            // 
            // xrHeadPaymentLedgerName
            // 
            this.xrHeadPaymentLedgerName.Name = "xrHeadPaymentLedgerName";
            resources.ApplyResources(this.xrHeadPaymentLedgerName, "xrHeadPaymentLedgerName");
            // 
            // xrHeadPaymentAmount
            // 
            this.xrHeadPaymentAmount.Name = "xrHeadPaymentAmount";
            this.xrHeadPaymentAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrHeadPaymentAmount, "xrHeadPaymentAmount");
            // 
            // FinalReceiptsPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpOpeningBalance,
            this.ReportFooter,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.clPaymentAmt,
            this.clfReceiptAmt});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandLine2,
            this.xrCrossBandLine1});
            this.DataMember = "FinalReceiptsPayments";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpOpeningBalance, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblOpeningBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTblClosingBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtableHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport xrSubPayments;
        private DevExpress.XtraReports.UI.XRSubreport xrSubReceipts;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpOpeningBalance;
        private DevExpress.XtraReports.UI.XRSubreport xrSubOpeningBalance;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalReceipts;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalPayment;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentAmt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubClosingBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField clPaymentAmt;
        private DevExpress.XtraReports.UI.CalculatedField clfReceiptAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptAmount;
        private DevExpress.XtraReports.UI.XRTable xrTblOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrOpeningBalance;
        private DevExpress.XtraReports.UI.XRTable xtTblClosingBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrColClosingBalance;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine1;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtableHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadReceiptCode;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadReceiptAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadPaymentCode;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadPaymentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrHeadPaymentAmount;
    }
}

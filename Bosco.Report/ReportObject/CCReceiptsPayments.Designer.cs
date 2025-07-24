namespace Bosco.Report.ReportObject
{
    partial class CCReceiptsPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCReceiptsPayments));
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
            this.xrCosSubReceipts = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrCosSubPayments = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotalReceipts = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCosReceiptAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotalPayment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCosPaymentAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.clPaymentAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.clfReceiptAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrCrossBandLine1 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine2 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosSubPayments,
            this.xrCosSubReceipts});
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
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
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
            // xrCosSubReceipts
            // 
            resources.ApplyResources(this.xrCosSubReceipts, "xrCosSubReceipts");
            this.xrCosSubReceipts.Name = "xrCosSubReceipts";
            this.xrCosSubReceipts.ReportSource = new Bosco.Report.ReportObject.Receipts();
            // 
            // xrCosSubPayments
            // 
            resources.ApplyResources(this.xrCosSubPayments, "xrCosSubPayments");
            this.xrCosSubPayments.Name = "xrCosSubPayments";
            this.xrCosSubPayments.ReportSource = new Bosco.Report.ReportObject.Payments();
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGrandTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
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
            this.xrCosReceiptAmt,
            this.xrGrandTotalPayment,
            this.xrCosPaymentAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrGrandTotalReceipts
            // 
            this.xrGrandTotalReceipts.Name = "xrGrandTotalReceipts";
            resources.ApplyResources(this.xrGrandTotalReceipts, "xrGrandTotalReceipts");
            // 
            // xrCosReceiptAmt
            // 
            this.xrCosReceiptAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.clfReceiptAmt")});
            this.xrCosReceiptAmt.Name = "xrCosReceiptAmt";
            this.xrCosReceiptAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCosReceiptAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrCosReceiptAmt, "xrCosReceiptAmt");
            this.xrCosReceiptAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrCosReceiptAmt_SummaryGetResult);
            // 
            // xrGrandTotalPayment
            // 
            this.xrGrandTotalPayment.Name = "xrGrandTotalPayment";
            resources.ApplyResources(this.xrGrandTotalPayment, "xrGrandTotalPayment");
            // 
            // xrCosPaymentAmt
            // 
            this.xrCosPaymentAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.clPaymentAmt")});
            this.xrCosPaymentAmt.Name = "xrCosPaymentAmt";
            this.xrCosPaymentAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCosPaymentAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrCosPaymentAmt, "xrCosPaymentAmt");
            this.xrCosPaymentAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrCosPaymentAmt_SummaryGetResult);
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
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrCrossBandLine1
            // 
            this.xrCrossBandLine1.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine1, "xrCrossBandLine1");
            this.xrCrossBandLine1.Name = "xrCrossBandLine1";
            this.xrCrossBandLine1.StartBand = this.PageHeader;
            this.xrCrossBandLine1.WidthF = 1.000023F;
            // 
            // xrCrossBandLine2
            // 
            this.xrCrossBandLine2.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine2, "xrCrossBandLine2");
            this.xrCrossBandLine2.Name = "xrCrossBandLine2";
            this.xrCrossBandLine2.StartBand = this.PageHeader;
            this.xrCrossBandLine2.WidthF = 1F;
            // 
            // CCReceiptsPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.clPaymentAmt,
            this.clfReceiptAmt});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandLine2,
            this.xrCrossBandLine1});
            this.DataMember = "FinalReceiptsPayments";
            this.DataSource = this.reportSetting1;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport xrCosSubPayments;
        private DevExpress.XtraReports.UI.XRSubreport xrCosSubReceipts;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentAmount;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalReceipts;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalPayment;
        private DevExpress.XtraReports.UI.XRTableCell xrCosPaymentAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCosReceiptAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField clPaymentAmt;
        private DevExpress.XtraReports.UI.CalculatedField clfReceiptAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptAmount;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine1;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine2;
    }
}

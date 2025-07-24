namespace Bosco.Report.ReportObject
{
    partial class ReceiptsPaymentsWithPreviosYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptsPaymentsWithPreviosYear));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrReceiptPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceipts = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptsAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayments = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapReceiptPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReciptsCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReceipts = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReceiptAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPayPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPayCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPayments = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPayAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.StylePriority.UseBorderColor = false;
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StyleName = "styleRow";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrReceiptPrevious,
            this.xrReceiptCode,
            this.xrReceipts,
            this.xrReceiptsAmount,
            this.xrPayPrevious,
            this.xrPayCode,
            this.xrPayments,
            this.xrPayAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrReceiptPrevious
            // 
            this.xrReceiptPrevious.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.RECEIPT_PREVIOUS")});
            this.xrReceiptPrevious.Name = "xrReceiptPrevious";
            xrSummary1.IgnoreNullValues = true;
            this.xrReceiptPrevious.Summary = xrSummary1;
            resources.ApplyResources(this.xrReceiptPrevious, "xrReceiptPrevious");
            // 
            // xrReceiptCode
            // 
            this.xrReceiptCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.RECEIPT_CODE")});
            this.xrReceiptCode.Name = "xrReceiptCode";
            xrSummary2.IgnoreNullValues = true;
            this.xrReceiptCode.Summary = xrSummary2;
            resources.ApplyResources(this.xrReceiptCode, "xrReceiptCode");
            // 
            // xrReceipts
            // 
            this.xrReceipts.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.PAYMENTS")});
            this.xrReceipts.Name = "xrReceipts";
            xrSummary3.IgnoreNullValues = true;
            this.xrReceipts.Summary = xrSummary3;
            resources.ApplyResources(this.xrReceipts, "xrReceipts");
            // 
            // xrReceiptsAmount
            // 
            this.xrReceiptsAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.RECEIPTS_AMOUNT", "{0:n}")});
            this.xrReceiptsAmount.Name = "xrReceiptsAmount";
            this.xrReceiptsAmount.StylePriority.UseTextAlignment = false;
            xrSummary4.IgnoreNullValues = true;
            this.xrReceiptsAmount.Summary = xrSummary4;
            resources.ApplyResources(this.xrReceiptsAmount, "xrReceiptsAmount");
            // 
            // xrPayPrevious
            // 
            this.xrPayPrevious.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.PAY_PREVIOUS")});
            this.xrPayPrevious.Name = "xrPayPrevious";
            xrSummary5.IgnoreNullValues = true;
            this.xrPayPrevious.Summary = xrSummary5;
            resources.ApplyResources(this.xrPayPrevious, "xrPayPrevious");
            // 
            // xrPayCode
            // 
            this.xrPayCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.PAY_CODE")});
            this.xrPayCode.Name = "xrPayCode";
            xrSummary6.IgnoreNullValues = true;
            this.xrPayCode.Summary = xrSummary6;
            resources.ApplyResources(this.xrPayCode, "xrPayCode");
            // 
            // xrPayments
            // 
            this.xrPayments.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.PAYMENTS")});
            this.xrPayments.Name = "xrPayments";
            xrSummary7.IgnoreNullValues = true;
            this.xrPayments.Summary = xrSummary7;
            resources.ApplyResources(this.xrPayments, "xrPayments");
            // 
            // xrPayAmount
            // 
            this.xrPayAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReceiptsPaymentsWithPreviousYear.PAY_AMOUNT", "{0:n}")});
            this.xrPayAmount.Name = "xrPayAmount";
            this.xrPayAmount.StylePriority.UseTextAlignment = false;
            xrSummary8.IgnoreNullValues = true;
            this.xrPayAmount.Summary = xrSummary8;
            resources.ApplyResources(this.xrPayAmount, "xrPayAmount");
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTable2
            // 
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.StyleName = "styleColumnHeader";
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapReceiptPrevious,
            this.xrCapReciptsCode,
            this.xrCapReceipts,
            this.xrCapReceiptAmount,
            this.xrCapPayPrevious,
            this.xrCapPayCode,
            this.xrCapPayments,
            this.xrCapPayAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrCapReceiptPrevious
            // 
            this.xrCapReceiptPrevious.Name = "xrCapReceiptPrevious";
            resources.ApplyResources(this.xrCapReceiptPrevious, "xrCapReceiptPrevious");
            // 
            // xrCapReciptsCode
            // 
            this.xrCapReciptsCode.Name = "xrCapReciptsCode";
            resources.ApplyResources(this.xrCapReciptsCode, "xrCapReciptsCode");
            // 
            // xrCapReceipts
            // 
            this.xrCapReceipts.Name = "xrCapReceipts";
            resources.ApplyResources(this.xrCapReceipts, "xrCapReceipts");
            // 
            // xrCapReceiptAmount
            // 
            this.xrCapReceiptAmount.Name = "xrCapReceiptAmount";
            this.xrCapReceiptAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapReceiptAmount, "xrCapReceiptAmount");
            // 
            // xrCapPayPrevious
            // 
            this.xrCapPayPrevious.Name = "xrCapPayPrevious";
            resources.ApplyResources(this.xrCapPayPrevious, "xrCapPayPrevious");
            // 
            // xrCapPayCode
            // 
            this.xrCapPayCode.Name = "xrCapPayCode";
            resources.ApplyResources(this.xrCapPayCode, "xrCapPayCode");
            // 
            // xrCapPayments
            // 
            this.xrCapPayments.Name = "xrCapPayments";
            resources.ApplyResources(this.xrCapPayments, "xrCapPayments");
            // 
            // xrCapPayAmount
            // 
            this.xrCapPayAmount.Name = "xrCapPayAmount";
            this.xrCapPayAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapPayAmount, "xrCapPayAmount");
            // 
            // ReceiptsPaymentsWithPreviosYear
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptCode;
        private DevExpress.XtraReports.UI.XRTableCell xrReceipts;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptsAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrPayPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrPayCode;
        private DevExpress.XtraReports.UI.XRTableCell xrPayments;
        private DevExpress.XtraReports.UI.XRTableCell xrPayAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReciptsCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceipts;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPayPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPayCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPayments;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPayAmount;
    }
}

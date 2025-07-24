namespace Bosco.Report.ReportObject
{
    partial class BankChequeUnrealized
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
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankChequeUnrealized));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblUnrealized = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblUnrealized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblUnrealized});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblTotal
            // 
            resources.ApplyResources(this.xrtblTotal, "xrtblTotal");
            this.xrtblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblTotal.Name = "xrtblTotal";
            this.xrtblTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblTotal.StyleName = "styleTotalRow";
            this.xrtblTotal.StylePriority.UseBackColor = false;
            this.xrtblTotal.StylePriority.UseBorderColor = false;
            this.xrtblTotal.StylePriority.UseBorders = false;
            this.xrtblTotal.StylePriority.UseFont = false;
            this.xrtblTotal.StylePriority.UsePadding = false;
            this.xrtblTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTotalAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            // 
            // xrTotalAmount
            // 
            this.xrTotalAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTotalAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            resources.ApplyResources(this.xrTotalAmount, "xrTotalAmount");
            this.xrTotalAmount.Name = "xrTotalAmount";
            this.xrTotalAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTotalAmount.StylePriority.UseBorders = false;
            this.xrTotalAmount.StylePriority.UseFont = false;
            this.xrTotalAmount.StylePriority.UsePadding = false;
            this.xrTotalAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalAmount.Summary = xrSummary2;
            // 
            // xrtblUnrealized
            // 
            resources.ApplyResources(this.xrtblUnrealized, "xrtblUnrealized");
            this.xrtblUnrealized.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblUnrealized.Name = "xrtblUnrealized";
            this.xrtblUnrealized.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblUnrealized.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblUnrealized.StyleName = "styleRow";
            this.xrtblUnrealized.StylePriority.UseBorderColor = false;
            this.xrtblUnrealized.StylePriority.UseBorders = false;
            this.xrtblUnrealized.StylePriority.UseFont = false;
            this.xrtblUnrealized.StylePriority.UsePadding = false;
            this.xrtblUnrealized.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrChequeNo,
            this.xrCode,
            this.xrParticulars,
            this.xrAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrDate
            // 
            this.xrDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.VOUCHER_DATE", "{0:d}")});
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrDate, "xrDate");
            // 
            // xrChequeNo
            // 
            this.xrChequeNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChequeNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.CHEQUE_NO")});
            this.xrChequeNo.Name = "xrChequeNo";
            this.xrChequeNo.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrChequeNo, "xrChequeNo");
            // 
            // xrCode
            // 
            this.xrCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_CODE")});
            this.xrCode.Name = "xrCode";
            this.xrCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCode, "xrCode");
            // 
            // xrParticulars
            // 
            this.xrParticulars.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrParticulars.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_NAME")});
            this.xrParticulars.Name = "xrParticulars";
            this.xrParticulars.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrParticulars, "xrParticulars");
            // 
            // xrAmount
            // 
            this.xrAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT", "{0:n}")});
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrAmount.StylePriority.UseBorders = false;
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrAmount.Summary = xrSummary1;
            resources.ApplyResources(this.xrAmount, "xrAmount");
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PageHeader1
            // 
            this.PageHeader1.Name = "PageHeader1";
            // 
            // ReportFooter1
            // 
            this.ReportFooter1.Name = "ReportFooter1";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
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
            this.xrCapDate,
            this.xrCapChequeNo,
            this.xrCapCode,
            this.xrCapParticulars,
            this.xrCapAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            this.xrCapDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StylePriority.UseBorderColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            // 
            // xrCapChequeNo
            // 
            resources.ApplyResources(this.xrCapChequeNo, "xrCapChequeNo");
            this.xrCapChequeNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapChequeNo.Name = "xrCapChequeNo";
            this.xrCapChequeNo.StylePriority.UseBorderColor = false;
            this.xrCapChequeNo.StylePriority.UseBorders = false;
            // 
            // xrCapCode
            // 
            resources.ApplyResources(this.xrCapCode, "xrCapCode");
            this.xrCapCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapCode.Name = "xrCapCode";
            this.xrCapCode.StylePriority.UseBorderColor = false;
            this.xrCapCode.StylePriority.UseBorders = false;
            // 
            // xrCapParticulars
            // 
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            this.xrCapParticulars.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.StylePriority.UseBorderColor = false;
            this.xrCapParticulars.StylePriority.UseBorders = false;
            // 
            // xrCapAmount
            // 
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            this.xrCapAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapAmount.StylePriority.UseBorderColor = false;
            this.xrCapAmount.StylePriority.UseBorders = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            // 
            // BankChequeUnrealized
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblUnrealized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblUnrealized;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
    }
}

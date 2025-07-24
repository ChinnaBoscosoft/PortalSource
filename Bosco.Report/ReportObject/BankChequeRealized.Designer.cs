namespace Bosco.Report.ReportObject
{
    partial class BankChequeRealized
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankChequeRealized));
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblAmount = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrtblBankRealized = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBankRealized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblBankRealized});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblAmount});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblAmount
            // 
            resources.ApplyResources(this.xrtblAmount, "xrtblAmount");
            this.xrtblAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblAmount.Name = "xrtblAmount";
            this.xrtblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblAmount.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblAmount.StyleName = "styleTotalRow";
            this.xrtblAmount.StylePriority.UseBackColor = false;
            this.xrtblAmount.StylePriority.UseBorderColor = false;
            this.xrtblAmount.StylePriority.UseBorders = false;
            this.xrtblAmount.StylePriority.UsePadding = false;
            this.xrtblAmount.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTotalAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrTableCell9, "xrTableCell9");
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            // 
            // xrTotalAmt
            // 
            this.xrTotalAmt.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTotalAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            resources.ApplyResources(this.xrTotalAmt, "xrTotalAmt");
            this.xrTotalAmt.Name = "xrTotalAmt";
            this.xrTotalAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTotalAmt.StylePriority.UseBorders = false;
            this.xrTotalAmt.StylePriority.UseFont = false;
            this.xrTotalAmt.StylePriority.UsePadding = false;
            this.xrTotalAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalAmt.Summary = xrSummary1;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrtblBankRealized
            // 
            resources.ApplyResources(this.xrtblBankRealized, "xrtblBankRealized");
            this.xrtblBankRealized.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblBankRealized.Name = "xrtblBankRealized";
            this.xrtblBankRealized.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblBankRealized.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblBankRealized.StyleName = "styleRow";
            this.xrtblBankRealized.StylePriority.UseBorderColor = false;
            this.xrtblBankRealized.StylePriority.UseBorders = false;
            this.xrtblBankRealized.StylePriority.UseFont = false;
            this.xrtblBankRealized.StylePriority.UsePadding = false;
            this.xrtblBankRealized.StylePriority.UseTextAlignment = false;
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
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrAmount.StylePriority.UseBorders = false;
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrAmount, "xrAmount");
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
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
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
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
            this.xrCapDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UsePadding = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapChequeNo
            // 
            this.xrCapChequeNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapChequeNo.Name = "xrCapChequeNo";
            this.xrCapChequeNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapChequeNo.StylePriority.UseBorders = false;
            this.xrCapChequeNo.StylePriority.UsePadding = false;
            this.xrCapChequeNo.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapChequeNo, "xrCapChequeNo");
            // 
            // xrCapCode
            // 
            this.xrCapCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapCode.Name = "xrCapCode";
            this.xrCapCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapCode.StylePriority.UseBorders = false;
            this.xrCapCode.StylePriority.UsePadding = false;
            this.xrCapCode.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapCode, "xrCapCode");
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapParticulars.StylePriority.UseBorders = false;
            this.xrCapParticulars.StylePriority.UsePadding = false;
            this.xrCapParticulars.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrCapAmount.StylePriority.UseBorders = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            // 
            // BankChequeRealized
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.ReportFooter,
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBankRealized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblAmount;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrtblBankRealized;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
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

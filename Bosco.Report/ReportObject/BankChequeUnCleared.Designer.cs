namespace Bosco.Report.ReportObject
{
    partial class BankChequeUnCleared
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankChequeUnCleared));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StyleName = "styleRow";
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrChequeNo,
            this.xrCode,
            this.xrParticulars,
            this.xrAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
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
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrAmount.Summary = xrSummary1;
            resources.ApplyResources(this.xrAmount, "xrAmount");
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable3
            // 
            resources.ApplyResources(this.xrTable3, "xrTable3");
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.StyleName = "styleTotalRow";
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseBorderColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotal,
            this.xrTotalAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrTotal
            // 
            this.xrTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrTotal.StylePriority.UseBorders = false;
            this.xrTotal.StylePriority.UsePadding = false;
            this.xrTotal.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTotal, "xrTotal");
            // 
            // xrTotalAmt
            // 
            this.xrTotalAmt.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTotalAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            this.xrTotalAmt.Name = "xrTotalAmt";
            this.xrTotalAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrTotalAmt.StylePriority.UseBorders = false;
            this.xrTotalAmt.StylePriority.UsePadding = false;
            this.xrTotalAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrTotalAmt, "xrTotalAmt");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.xrTableRow2});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapChequeNo,
            this.xrCapCode,
            this.xrCapParticulars,
            this.xrCapAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrCapDate
            // 
            this.xrCapDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapChequeNo
            // 
            this.xrCapChequeNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapChequeNo.Name = "xrCapChequeNo";
            this.xrCapChequeNo.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapChequeNo, "xrCapChequeNo");
            // 
            // xrCapCode
            // 
            this.xrCapCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapCode.Name = "xrCapCode";
            this.xrCapCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapCode, "xrCapCode");
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapAmount.StylePriority.UseBorders = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            // 
            // BankChequeUnCleared
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
    }
}

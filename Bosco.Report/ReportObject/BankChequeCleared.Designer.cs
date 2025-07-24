namespace Bosco.Report.ReportObject
{
    partial class BankChequeCleared
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankChequeCleared));
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblCleared = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCheque = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCleared)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCleared});
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
            this.xrtblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblTotal.StyleName = "styleTotalRow";
            this.xrtblTotal.StylePriority.UseBackColor = false;
            this.xrtblTotal.StylePriority.UseBorderColor = false;
            this.xrtblTotal.StylePriority.UseBorders = false;
            this.xrtblTotal.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrCapTotal,
            this.xrTotalAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            // 
            // xrTableCell2
            // 
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            // 
            // xrTableCell3
            // 
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            // 
            // xrCapTotal
            // 
            resources.ApplyResources(this.xrCapTotal, "xrCapTotal");
            this.xrCapTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapTotal.Name = "xrCapTotal";
            this.xrCapTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapTotal.StylePriority.UseBorderColor = false;
            this.xrCapTotal.StylePriority.UseBorders = false;
            this.xrCapTotal.StylePriority.UseFont = false;
            this.xrCapTotal.StylePriority.UsePadding = false;
            this.xrCapTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTotalAmt
            // 
            resources.ApplyResources(this.xrTotalAmt, "xrTotalAmt");
            this.xrTotalAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTotalAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            this.xrTotalAmt.Name = "xrTotalAmt";
            this.xrTotalAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrTotalAmt.StylePriority.UseBorderColor = false;
            this.xrTotalAmt.StylePriority.UseBorders = false;
            this.xrTotalAmt.StylePriority.UseFont = false;
            this.xrTotalAmt.StylePriority.UsePadding = false;
            this.xrTotalAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalAmt.Summary = xrSummary1;
            // 
            // xrtblCleared
            // 
            resources.ApplyResources(this.xrtblCleared, "xrtblCleared");
            this.xrtblCleared.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCleared.Name = "xrtblCleared";
            this.xrtblCleared.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCleared.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblCleared.StyleName = "styleRow";
            this.xrtblCleared.StylePriority.UseBorderColor = false;
            this.xrtblCleared.StylePriority.UseBorders = false;
            this.xrtblCleared.StylePriority.UseFont = false;
            this.xrtblCleared.StylePriority.UsePadding = false;
            this.xrtblCleared.StylePriority.UseTextAlignment = false;
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
            this.xrDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.VOUCHER_DATE", "{0:d}")});
            resources.ApplyResources(this.xrDate, "xrDate");
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            // 
            // xrChequeNo
            // 
            this.xrChequeNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChequeNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.CHEQUE_NO")});
            resources.ApplyResources(this.xrChequeNo, "xrChequeNo");
            this.xrChequeNo.Name = "xrChequeNo";
            this.xrChequeNo.StylePriority.UseBorders = false;
            this.xrChequeNo.StylePriority.UseFont = false;
            // 
            // xrCode
            // 
            this.xrCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_CODE")});
            resources.ApplyResources(this.xrCode, "xrCode");
            this.xrCode.Name = "xrCode";
            this.xrCode.StylePriority.UseBorders = false;
            this.xrCode.StylePriority.UseFont = false;
            // 
            // xrParticulars
            // 
            this.xrParticulars.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrParticulars.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_NAME")});
            resources.ApplyResources(this.xrParticulars, "xrParticulars");
            this.xrParticulars.Name = "xrParticulars";
            this.xrParticulars.StylePriority.UseBorders = false;
            this.xrParticulars.StylePriority.UseFont = false;
            // 
            // xrAmount
            // 
            this.xrAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrAmount, "xrAmount");
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrAmount.StylePriority.UseBorders = false;
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
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
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCaption});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrtblCaption
            // 
            resources.ApplyResources(this.xrtblCaption, "xrtblCaption");
            this.xrtblCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCaption.Name = "xrtblCaption";
            this.xrtblCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblCaption.StyleName = "styleColumnHeader";
            this.xrtblCaption.StylePriority.UseBackColor = false;
            this.xrtblCaption.StylePriority.UseBorderColor = false;
            this.xrtblCaption.StylePriority.UseBorders = false;
            this.xrtblCaption.StylePriority.UseFont = false;
            this.xrtblCaption.StylePriority.UsePadding = false;
            this.xrtblCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapCheque,
            this.xrCapCode,
            this.xrCapParticulars,
            this.xrCapAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            this.xrCapDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapDate.StylePriority.UseBackColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UsePadding = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapCheque
            // 
            this.xrCapCheque.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCheque.Name = "xrCapCheque";
            this.xrCapCheque.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCheque.StylePriority.UseBorders = false;
            this.xrCapCheque.StylePriority.UsePadding = false;
            this.xrCapCheque.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapCheque, "xrCapCheque");
            // 
            // xrCapCode
            // 
            this.xrCapCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCode.Name = "xrCapCode";
            this.xrCapCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCode.StylePriority.UseBorders = false;
            this.xrCapCode.StylePriority.UsePadding = false;
            this.xrCapCode.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapCode, "xrCapCode");
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapParticulars.StylePriority.UsePadding = false;
            this.xrCapParticulars.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapAmount.StylePriority.UseBorders = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            // 
            // BankChequeCleared
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
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCleared)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmt;
        private DevExpress.XtraReports.UI.XRTable xrtblCleared;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCheque;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
    }
}

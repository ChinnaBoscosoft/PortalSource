namespace Bosco.Report.ReportObject
{
    partial class DrillDownReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrillDownReport));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblDrillDown = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xtGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlblCapClosingBalance = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtblCapDebitCredit = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDrillDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCapDebitCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblDrillDown});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // xrtblDrillDown
            // 
            resources.ApplyResources(this.xrtblDrillDown, "xrtblDrillDown");
            this.xrtblDrillDown.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrtblDrillDown.Name = "xrtblDrillDown";
            this.xrtblDrillDown.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblDrillDown.StyleName = "styleRow";
            this.xrtblDrillDown.StylePriority.UseBorderColor = false;
            this.xrtblDrillDown.StylePriority.UseBorders = false;
            this.xrtblDrillDown.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrParticulars,
            this.xrDebit,
            this.xrCredit});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrParticulars
            // 
            this.xrParticulars.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrParticulars.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DrillDownReport.PARTICULARS")});
            resources.ApplyResources(this.xrParticulars, "xrParticulars");
            this.xrParticulars.Name = "xrParticulars";
            this.xrParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrParticulars.StylePriority.UseBorders = false;
            this.xrParticulars.StylePriority.UseFont = false;
            this.xrParticulars.StylePriority.UsePadding = false;
            this.xrParticulars.StylePriority.UseTextAlignment = false;
            xrSummary1.IgnoreNullValues = true;
            this.xrParticulars.Summary = xrSummary1;
            this.xrParticulars.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrParticulars_BeforePrint);
            // 
            // xrDebit
            // 
            this.xrDebit.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DrillDownReport.DEBIT", "{0:n}")});
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrDebit.StylePriority.UseBorders = false;
            this.xrDebit.StylePriority.UseFont = false;
            this.xrDebit.StylePriority.UsePadding = false;
            this.xrDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrCredit
            // 
            this.xrCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DrillDownReport.CREDIT", "{0:n}")});
            resources.ApplyResources(this.xrCredit, "xrCredit");
            this.xrCredit.Name = "xrCredit";
            this.xrCredit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCredit.StylePriority.UseBorders = false;
            this.xrCredit.StylePriority.UseFont = false;
            this.xrCredit.StylePriority.UsePadding = false;
            this.xrCredit.StylePriority.UseTextAlignment = false;
            xrSummary2.IgnoreNullValues = true;
            this.xrCredit.Summary = xrSummary2;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtGrandTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xtGrandTotal
            // 
            resources.ApplyResources(this.xtGrandTotal, "xtGrandTotal");
            this.xtGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xtGrandTotal.Name = "xtGrandTotal";
            this.xtGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xtGrandTotal.StylePriority.UseBackColor = false;
            this.xtGrandTotal.StylePriority.UseBorderColor = false;
            this.xtGrandTotal.StylePriority.UseBorders = false;
            this.xtGrandTotal.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrGrandTotal
            // 
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UsePadding = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DrillDownReport.DEBIT")});
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell2.Summary = xrSummary3;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DrillDownReport.CREDIT")});
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell3.Summary = xrSummary4;
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
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleGroupRow";
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
            this.xrCapParticulars,
            this.xrCapClosingBalance});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrCapParticulars.StylePriority.UseBorders = false;
            this.xrCapParticulars.StylePriority.UseFont = false;
            this.xrCapParticulars.StylePriority.UsePadding = false;
            this.xrCapParticulars.StylePriority.UseTextAlignment = false;
            // 
            // xrCapClosingBalance
            // 
            this.xrCapClosingBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapClosingBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblCapClosingBalance,
            this.xrtblCapDebitCredit});
            this.xrCapClosingBalance.Name = "xrCapClosingBalance";
            this.xrCapClosingBalance.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapClosingBalance, "xrCapClosingBalance");
            // 
            // xrlblCapClosingBalance
            // 
            this.xrlblCapClosingBalance.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrlblCapClosingBalance, "xrlblCapClosingBalance");
            this.xrlblCapClosingBalance.Name = "xrlblCapClosingBalance";
            this.xrlblCapClosingBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrlblCapClosingBalance.StylePriority.UseBorders = false;
            this.xrlblCapClosingBalance.StylePriority.UseFont = false;
            this.xrlblCapClosingBalance.StylePriority.UsePadding = false;
            this.xrlblCapClosingBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrtblCapDebitCredit
            // 
            this.xrtblCapDebitCredit.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrtblCapDebitCredit, "xrtblCapDebitCredit");
            this.xrtblCapDebitCredit.Name = "xrtblCapDebitCredit";
            this.xrtblCapDebitCredit.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblCapDebitCredit.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDebit,
            this.xrCapCredit});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrCapDebit
            // 
            this.xrCapDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrCapDebit, "xrCapDebit");
            this.xrCapDebit.Name = "xrCapDebit";
            this.xrCapDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrCapDebit.StylePriority.UseBorders = false;
            this.xrCapDebit.StylePriority.UseFont = false;
            this.xrCapDebit.StylePriority.UsePadding = false;
            this.xrCapDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrCapCredit
            // 
            this.xrCapCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrCapCredit, "xrCapCredit");
            this.xrCapCredit.Name = "xrCapCredit";
            this.xrCapCredit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCredit.StylePriority.UseBorders = false;
            this.xrCapCredit.StylePriority.UseFont = false;
            this.xrCapCredit.StylePriority.UsePadding = false;
            this.xrCapCredit.StylePriority.UseTextAlignment = false;
            // 
            // DrillDownReport
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
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDrillDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCapDebitCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblDrillDown;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCredit;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xtGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapClosingBalance;
        private DevExpress.XtraReports.UI.XRLabel xrlblCapClosingBalance;
        private DevExpress.XtraReports.UI.XRTable xrtblCapDebitCredit;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
    }
}

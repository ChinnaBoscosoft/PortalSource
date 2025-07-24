namespace Bosco.Report.ReportObject
{
    partial class MonthlyGNAbstractReceiptsPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyGNAbstractReceiptsPayments));
            this.xrSubreportMontlyPayments = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubreportMonthlyReceipts = new DevExpress.XtraReports.UI.XRSubreport();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.detailSubPayments = new DevExpress.XtraReports.UI.DetailBand();
            this.detailPayments = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.styleTitleBar = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubreportJV = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // xrSubreportMontlyPayments
            // 
            resources.ApplyResources(this.xrSubreportMontlyPayments, "xrSubreportMontlyPayments");
            this.xrSubreportMontlyPayments.Name = "xrSubreportMontlyPayments";
            this.xrSubreportMontlyPayments.ReportSource = new Bosco.Report.ReportObject.MonthlyGNAbstractPayments();
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel2.StyleName = "styleTitleRow";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel1.BorderWidth = 0F;
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel1.StyleName = "styleTitleRow";
            // 
            // xrSubreportMonthlyReceipts
            // 
            resources.ApplyResources(this.xrSubreportMonthlyReceipts, "xrSubreportMonthlyReceipts");
            this.xrSubreportMonthlyReceipts.Name = "xrSubreportMonthlyReceipts";
            this.xrSubreportMonthlyReceipts.ReportSource = new Bosco.Report.ReportObject.MonthlyGNAbstractReceipts();
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // detailSubPayments
            // 
            this.detailSubPayments.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportMonthlyReceipts,
            this.xrLabel1});
            resources.ApplyResources(this.detailSubPayments, "detailSubPayments");
            this.detailSubPayments.Name = "detailSubPayments";
            // 
            // detailPayments
            // 
            this.detailPayments.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailSubPayments,
            this.DetailReport});
            this.detailPayments.Level = 0;
            this.detailPayments.Name = "detailPayments";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrSubreportMontlyPayments});
            resources.ApplyResources(this.Detail1, "Detail1");
            this.Detail1.Name = "Detail1";
            // 
            // styleTitleBar
            // 
            this.styleTitleBar.BackColor = System.Drawing.Color.Honeydew;
            this.styleTitleBar.BorderColor = System.Drawing.Color.DimGray;
            this.styleTitleBar.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.styleTitleBar.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.styleTitleBar.BorderWidth = 1F;
            this.styleTitleBar.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTitleBar.Name = "styleTitleBar";
            this.styleTitleBar.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleTitleBar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportJV});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrSubreportJV
            // 
            this.xrSubreportJV.CanShrink = true;
            resources.ApplyResources(this.xrSubreportJV, "xrSubreportJV");
            this.xrSubreportJV.Name = "xrSubreportJV";
            this.xrSubreportJV.ReportSource = new Bosco.Report.ReportObject.GeneralateJournalVoucherReport();
            this.xrSubreportJV.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreportJV_BeforePrint);
            // 
            // MonthlyGNAbstractReceiptsPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.detailPayments,
            this.ReportFooter});
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleTitleBar});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.detailPayments, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport xrSubreportMonthlyReceipts;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportMontlyPayments;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.DetailBand detailSubPayments;
        private DevExpress.XtraReports.UI.DetailReportBand detailPayments;
        private DevExpress.XtraReports.UI.XRControlStyle styleTitleBar;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportJV;
    }
}

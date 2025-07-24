namespace Bosco.Report.ReportObject
{
    partial class GeneralateBalanceSheetDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralateBalanceSheetDetail));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblLEAmount = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCurrentYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrlblline = new DevExpress.XtraReports.UI.XRLabel();
            this.grpFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCurrentYear = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLEAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLEAmount});
            this.Detail.Expanded = true;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrtblLEAmount
            // 
            resources.ApplyResources(this.xrtblLEAmount, "xrtblLEAmount");
            this.xrtblLEAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblLEAmount.Name = "xrtblLEAmount";
            this.xrtblLEAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblLEAmount.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblLEAmount.StyleName = "styleRow";
            this.xrtblLEAmount.StylePriority.UseBackColor = false;
            this.xrtblLEAmount.StylePriority.UseBorderColor = false;
            this.xrtblLEAmount.StylePriority.UseBorders = false;
            this.xrtblLEAmount.StylePriority.UseFont = false;
            this.xrtblLEAmount.StylePriority.UsePadding = false;
            this.xrtblLEAmount.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTitle,
            this.xrCurrentYear});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTitle
            // 
            resources.ApplyResources(this.xrTitle, "xrTitle");
            this.xrTitle.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ProfitandLossbyHouse.SOCIETYNAME")});
            this.xrTitle.Name = "xrTitle";
            this.xrTitle.StylePriority.UseBackColor = false;
            this.xrTitle.StylePriority.UseFont = false;
            // 
            // xrCurrentYear
            // 
            resources.ApplyResources(this.xrCurrentYear, "xrCurrentYear");
            this.xrCurrentYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ProfitandLossbyHouse.FINAL", "{0:n}")});
            this.xrCurrentYear.Name = "xrCurrentYear";
            this.xrCurrentYear.StylePriority.UseBackColor = false;
            this.xrCurrentYear.StylePriority.UseFont = false;
            this.xrCurrentYear.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrCurrentYear.Summary = xrSummary1;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblline});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrlblline
            // 
            resources.ApplyResources(this.xrlblline, "xrlblline");
            this.xrlblline.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrlblline.CanShrink = true;
            this.xrlblline.Name = "xrlblline";
            this.xrlblline.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrlblline.StylePriority.UseBackColor = false;
            this.xrlblline.StylePriority.UseBorderColor = false;
            this.xrlblline.StylePriority.UseBorderDashStyle = false;
            this.xrlblline.StylePriority.UseBorders = false;
            this.xrlblline.StylePriority.UseFont = false;
            this.xrlblline.StylePriority.UseForeColor = false;
            this.xrlblline.StylePriority.UsePadding = false;
            this.xrlblline.StylePriority.UseTextAlignment = false;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.grpFooter, "grpFooter");
            this.grpFooter.Name = "grpFooter";
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
            this.xrCapTitle,
            this.xrCapCurrentYear});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapTitle
            // 
            resources.ApplyResources(this.xrCapTitle, "xrCapTitle");
            this.xrCapTitle.Name = "xrCapTitle";
            this.xrCapTitle.StylePriority.UseFont = false;
            this.xrCapTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrCapCurrentYear
            // 
            this.xrCapCurrentYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ProfitandLossbyHouse.FINAL")});
            resources.ApplyResources(this.xrCapCurrentYear, "xrCapCurrentYear");
            this.xrCapCurrentYear.Name = "xrCapCurrentYear";
            this.xrCapCurrentYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCurrentYear.StylePriority.UseFont = false;
            this.xrCapCurrentYear.StylePriority.UsePadding = false;
            this.xrCapCurrentYear.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCapCurrentYear.Summary = xrSummary2;
            // 
            // GeneralateBalanceSheetDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.grpFooter});
            this.DataMember = "ProfitandLossbyHouse";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLEAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblLEAmount;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrCurrentYear;
        private DevExpress.XtraReports.UI.XRTableCell xrTitle;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrlblline;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapTitle;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCurrentYear;
    }
}

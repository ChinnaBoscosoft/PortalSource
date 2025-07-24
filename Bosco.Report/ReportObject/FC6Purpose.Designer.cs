namespace Bosco.Report.ReportObject
{
    partial class FC6Purpose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FC6Purpose));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            resources.ApplyResources(this.TopMargin, "TopMargin");
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            // 
            // xrTable16
            // 
            resources.ApplyResources(this.xrTable16, "xrTable16");
            this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable16.StylePriority.UseBorderColor = false;
            this.xrTable16.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell79,
            this.xrTableCell82,
            this.xrTableCell81,
            this.xrTableCell80});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell76.Summary = xrSummary1;
            resources.ApplyResources(this.xrTableCell76, "xrTableCell76");
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.DONOR_TYPE")});
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseTextAlignment = false;
            xrSummary2.IgnoreNullValues = true;
            this.xrTableCell77.Summary = xrSummary2;
            resources.ApplyResources(this.xrTableCell77, "xrTableCell77");
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.DONOR_ADDRESS")});
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell79, "xrTableCell79");
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.PURPOSE")});
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell82, "xrTableCell82");
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.DOF_RECEIPTS", "{0:dd/MM/yyyy}")});
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell81, "xrTableCell81");
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.AMOUNT")});
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell80, "xrTableCell80");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTotal});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            // 
            // xrTotal
            // 
            this.xrTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FC6DONORLIST.AMOUNT")});
            resources.ApplyResources(this.xrTotal, "xrTotal");
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.StylePriority.UseFont = false;
            this.xrTotal.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTotal.Summary = xrSummary3;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            resources.ApplyResources(this.GroupFooter1, "GroupFooter1");
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // FC6Purpose
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.Detail,
            this.GroupFooter1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTable16;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell76;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell77;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell79;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell82;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell81;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell80;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTotal;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}

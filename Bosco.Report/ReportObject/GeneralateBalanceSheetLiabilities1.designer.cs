namespace Bosco.Report.ReportObject
{
    partial class GeneralateBalanceSheetLiabilities1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralateBalanceSheetLiabilities1));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrTblLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtcLiabilityLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblLiabilityLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLiabilityGrpGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTransCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.grpLedgerGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblLiabilityParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLiabilityParentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblParentGroupAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = true;
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // xrTblLiabilityLedgerName
            // 
            resources.ApplyResources(this.xrTblLiabilityLedgerName, "xrTblLiabilityLedgerName");
            this.xrTblLiabilityLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblLiabilityLedgerName.Name = "xrTblLiabilityLedgerName";
            this.xrTblLiabilityLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblLiabilityLedgerName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblLiabilityLedgerName.StyleName = "styleRow";
            this.xrTblLiabilityLedgerName.StylePriority.UseBorderColor = false;
            this.xrTblLiabilityLedgerName.StylePriority.UseBorders = false;
            this.xrTblLiabilityLedgerName.StylePriority.UsePadding = false;
            this.xrTblLiabilityLedgerName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLiabilityLedgerName,
            this.xrtcLiabilityLedgerAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // tcLiabilityLedgerName
            // 
            this.tcLiabilityLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_NAME")});
            this.tcLiabilityLedgerName.Name = "tcLiabilityLedgerName";
            this.tcLiabilityLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.tcLiabilityLedgerName.StylePriority.UsePadding = false;
            this.tcLiabilityLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.tcLiabilityLedgerName, "tcLiabilityLedgerName");
            // 
            // xrtcLiabilityLedgerAmt
            // 
            this.xrtcLiabilityLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT", "{0:n}")});
            this.xrtcLiabilityLedgerAmt.Name = "xrtcLiabilityLedgerAmt";
            resources.ApplyResources(this.xrtcLiabilityLedgerAmt, "xrtcLiabilityLedgerAmt");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // calLiaTotalAmt
            // 
            this.calLiaTotalAmt.DataMember = "BalanceSheet";
            this.calLiaTotalAmt.Name = "calLiaTotalAmt";
            // 
            // calAssetAmt
            // 
            this.calAssetAmt.DataMember = "BalanceSheet";
            this.calAssetAmt.Name = "calAssetAmt";
            // 
            // calOpLiability
            // 
            this.calOpLiability.DataMember = "BalanceSheet";
            this.calOpLiability.Name = "calOpLiability";
            // 
            // calOpAsset
            // 
            this.calOpAsset.DataMember = "BalanceSheet";
            this.calOpAsset.Name = "calOpAsset";
            // 
            // grpLedger
            // 
            this.grpLedger.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityLedgerName});
            this.grpLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedger, "grpLedger");
            this.grpLedger.Name = "grpLedger";
            // 
            // xrTblLiabilityLedgerGroup
            // 
            this.xrTblLiabilityLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblLiabilityLedgerGroup, "xrTblLiabilityLedgerGroup");
            this.xrTblLiabilityLedgerGroup.Name = "xrTblLiabilityLedgerGroup";
            this.xrTblLiabilityLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTblLiabilityLedgerGroup.StyleName = "styleGroupRow";
            this.xrTblLiabilityLedgerGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLiabilityGrpGroupName,
            this.xrtblTransCredit});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // tcLiabilityGrpGroupName
            // 
            resources.ApplyResources(this.tcLiabilityGrpGroupName, "tcLiabilityGrpGroupName");
            this.tcLiabilityGrpGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_GROUP")});
            this.tcLiabilityGrpGroupName.Name = "tcLiabilityGrpGroupName";
            this.tcLiabilityGrpGroupName.StylePriority.UseBorderColor = false;
            // 
            // xrtblTransCredit
            // 
            this.xrtblTransCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrtblTransCredit.Name = "xrtblTransCredit";
            this.xrtblTransCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblTransCredit.Summary = xrSummary1;
            resources.ApplyResources(this.xrtblTransCredit, "xrtblTransCredit");
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
            this.Detail1.Expanded = false;
            this.Detail1.Name = "Detail1";
            // 
            // grpLedgerGroup
            // 
            this.grpLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityLedgerGroup});
            this.grpLedgerGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedgerGroup, "grpLedgerGroup");
            this.grpLedgerGroup.Level = 1;
            this.grpLedgerGroup.Name = "grpLedgerGroup";
            this.grpLedgerGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpLedgerGroup_BeforePrint);
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblLiabilityParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PARENT_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            // 
            // xrTblLiabilityParentGroup
            // 
            this.xrTblLiabilityParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblLiabilityParentGroup, "xrTblLiabilityParentGroup");
            this.xrTblLiabilityParentGroup.Name = "xrTblLiabilityParentGroup";
            this.xrTblLiabilityParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblLiabilityParentGroup.StyleName = "styleGroupRow";
            this.xrTblLiabilityParentGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLiabilityParentGroupName,
            this.xrtblParentGroupAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // tcLiabilityParentGroupName
            // 
            resources.ApplyResources(this.tcLiabilityParentGroupName, "tcLiabilityParentGroupName");
            this.tcLiabilityParentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.PARENT_GROUP")});
            this.tcLiabilityParentGroupName.Name = "tcLiabilityParentGroupName";
            this.tcLiabilityParentGroupName.StylePriority.UseBorderColor = false;
            // 
            // xrtblParentGroupAmount
            // 
            this.xrtblParentGroupAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrtblParentGroupAmount.Name = "xrtblParentGroupAmount";
            this.xrtblParentGroupAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblParentGroupAmount.Summary = xrSummary2;
            resources.ApplyResources(this.xrtblParentGroupAmount, "xrtblParentGroupAmount");
            this.xrtblParentGroupAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtblParentGroupAmount_BeforePrint);
            // 
            // GeneralateBalanceSheetLiabilities1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpLedger,
            this.DetailReport,
            this.grpLedgerGroup,
            this.grpParentGroup});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "BalanceSheet";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpLedgerGroup, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.grpLedger, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityLedgerName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrtcLiabilityLedgerAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calLiaTotalAmt;
        private DevExpress.XtraReports.UI.CalculatedField calAssetAmt;
        private DevExpress.XtraReports.UI.CalculatedField calOpLiability;
        private DevExpress.XtraReports.UI.CalculatedField calOpAsset;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedger;
        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityGrpGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTransCredit;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedgerGroup;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.XRTable xrTblLiabilityParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcLiabilityParentGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrtblParentGroupAmount;
    }
}

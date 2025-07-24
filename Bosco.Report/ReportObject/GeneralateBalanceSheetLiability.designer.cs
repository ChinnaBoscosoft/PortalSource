namespace Bosco.Report.ReportObject
{
    partial class GeneralateBalanceSheetLiability
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
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralateBalanceSheetLiability));
            this.xrTblLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLiabilityLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLiabilityOpening = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtcLiabilityLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.grpMaster = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblAssetLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcNewLiabilityGrpGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrOpeningTotalAssetAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrsubGBConLedgerDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrsubGBConLedgerDetails,
            this.xrTblLiabilityLedgerName});
            this.Detail.Expanded = true;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
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
            this.xrTblLiabilityLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblLiabilityLedgerName_BeforePrint);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLiabilityLedgerName,
            this.xrLiabilityOpening,
            this.xrtcLiabilityLedgerAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // tcLiabilityLedgerName
            // 
            this.tcLiabilityLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcLiabilityLedgerName.BorderWidth = 1F;
            this.tcLiabilityLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.CON_LEDGER_NAME")});
            resources.ApplyResources(this.tcLiabilityLedgerName, "tcLiabilityLedgerName");
            this.tcLiabilityLedgerName.Name = "tcLiabilityLedgerName";
            this.tcLiabilityLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.tcLiabilityLedgerName.StylePriority.UseBorders = false;
            this.tcLiabilityLedgerName.StylePriority.UseBorderWidth = false;
            this.tcLiabilityLedgerName.StylePriority.UseFont = false;
            this.tcLiabilityLedgerName.StylePriority.UsePadding = false;
            this.tcLiabilityLedgerName.StylePriority.UseTextAlignment = false;
            this.tcLiabilityLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.tcLiabilityLedgerName_BeforePrint);
            // 
            // xrLiabilityOpening
            // 
            this.xrLiabilityOpening.BorderWidth = 1F;
            this.xrLiabilityOpening.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.OP_AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrLiabilityOpening, "xrLiabilityOpening");
            this.xrLiabilityOpening.Name = "xrLiabilityOpening";
            this.xrLiabilityOpening.StylePriority.UseBorderWidth = false;
            this.xrLiabilityOpening.StylePriority.UseFont = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrLiabilityOpening.Summary = xrSummary1;
            // 
            // xrtcLiabilityLedgerAmt
            // 
            this.xrtcLiabilityLedgerAmt.BorderWidth = 1F;
            this.xrtcLiabilityLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrtcLiabilityLedgerAmt, "xrtcLiabilityLedgerAmt");
            this.xrtcLiabilityLedgerAmt.Name = "xrtcLiabilityLedgerAmt";
            this.xrtcLiabilityLedgerAmt.StylePriority.UseBorderWidth = false;
            this.xrtcLiabilityLedgerAmt.StylePriority.UseFont = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            this.xrtcLiabilityLedgerAmt.Summary = xrSummary2;
            this.xrtcLiabilityLedgerAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtcLiabilityLedgerAmt_BeforePrint);
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
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Expanded = false;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            resources.ApplyResources(this.DetailReport, "DetailReport");
            // 
            // Detail1
            // 
            resources.ApplyResources(this.Detail1, "Detail1");
            this.Detail1.Name = "Detail1";
            // 
            // grpMaster
            // 
            this.grpMaster.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblAssetLedgerGroup});
            this.grpMaster.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MASTER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpMaster, "grpMaster");
            this.grpMaster.Name = "grpMaster";
            // 
            // xrTblAssetLedgerGroup
            // 
            this.xrTblAssetLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblAssetLedgerGroup, "xrTblAssetLedgerGroup");
            this.xrTblAssetLedgerGroup.Name = "xrTblAssetLedgerGroup";
            this.xrTblAssetLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblAssetLedgerGroup.StyleName = "styleGroupRow";
            this.xrTblAssetLedgerGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcNewLiabilityGrpGroupName,
            this.xrOpeningTotalAssetAmount,
            this.xrtblTransDebit});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // tcNewLiabilityGrpGroupName
            // 
            this.tcNewLiabilityGrpGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcNewLiabilityGrpGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.MASTER_NAME")});
            resources.ApplyResources(this.tcNewLiabilityGrpGroupName, "tcNewLiabilityGrpGroupName");
            this.tcNewLiabilityGrpGroupName.Name = "tcNewLiabilityGrpGroupName";
            this.tcNewLiabilityGrpGroupName.StylePriority.UseBorders = false;
            this.tcNewLiabilityGrpGroupName.StylePriority.UseFont = false;
            this.tcNewLiabilityGrpGroupName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.tcNewLiabilityGrpGroupName_BeforePrint);
            // 
            // xrOpeningTotalAssetAmount
            // 
            this.xrOpeningTotalAssetAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.OP_AMOUNT")});
            resources.ApplyResources(this.xrOpeningTotalAssetAmount, "xrOpeningTotalAssetAmount");
            this.xrOpeningTotalAssetAmount.Name = "xrOpeningTotalAssetAmount";
            this.xrOpeningTotalAssetAmount.StylePriority.UseFont = false;
            this.xrOpeningTotalAssetAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrOpeningTotalAssetAmount.Summary = xrSummary3;
            // 
            // xrtblTransDebit
            // 
            this.xrtblTransDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            resources.ApplyResources(this.xrtblTransDebit, "xrtblTransDebit");
            this.xrtblTransDebit.Name = "xrtblTransDebit";
            this.xrtblTransDebit.StylePriority.UseFont = false;
            this.xrtblTransDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblTransDebit.Summary = xrSummary4;
            // 
            // xrsubGBConLedgerDetails
            // 
            this.xrsubGBConLedgerDetails.CanShrink = true;
            resources.ApplyResources(this.xrsubGBConLedgerDetails, "xrsubGBConLedgerDetails");
            this.xrsubGBConLedgerDetails.Name = "xrsubGBConLedgerDetails";
            this.xrsubGBConLedgerDetails.ReportSource = new Bosco.Report.ReportObject.GeneralateBalanceSheetDetail();
            this.xrsubGBConLedgerDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrsubGBConLedgerDetails_BeforePrint);
            // 
            // GeneralateBalanceSheetLiability
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.DetailReport,
            this.grpMaster});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "CongiregationProfitandLoss";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpMaster, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLiabilityLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).EndInit();
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
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpMaster;
        private DevExpress.XtraReports.UI.XRTableCell xrLiabilityOpening;
        private DevExpress.XtraReports.UI.XRSubreport xrsubGBConLedgerDetails;
        private DevExpress.XtraReports.UI.XRTable xrTblAssetLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcNewLiabilityGrpGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrOpeningTotalAssetAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTransDebit;
    }
}

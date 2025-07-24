namespace Bosco.Report.ReportObject
{
    partial class BalanceSheetAsset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanceSheetAsset));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrOPAssetAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerAsset = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAssetAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpLedgerGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblOPDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblDetail});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // xrtblDetail
            // 
            resources.ApplyResources(this.xrtblDetail, "xrtblDetail");
            this.xrtblDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblDetail.Name = "xrtblDetail";
            this.xrtblDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblDetail.StyleName = "styleRow";
            this.xrtblDetail.StylePriority.UseBorderColor = false;
            this.xrtblDetail.StylePriority.UseBorders = false;
            this.xrtblDetail.StylePriority.UsePadding = false;
            this.xrtblDetail.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrOPAssetAmt,
            this.xrLedgerAsset,
            this.xrAssetAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrOPAssetAmt
            // 
            this.xrOPAssetAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.POP_DEBIT", "{0:n}")});
            this.xrOPAssetAmt.Name = "xrOPAssetAmt";
            resources.ApplyResources(this.xrOPAssetAmt, "xrOPAssetAmt");
            this.xrOPAssetAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrOPAssetAmt_BeforePrint);
            // 
            // xrLedgerAsset
            // 
            this.xrLedgerAsset.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.SUB_GROUP")});
            this.xrLedgerAsset.Name = "xrLedgerAsset";
            this.xrLedgerAsset.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrLedgerAsset.StylePriority.UsePadding = false;
            this.xrLedgerAsset.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrLedgerAsset, "xrLedgerAsset");
            // 
            // xrAssetAmt
            // 
            this.xrAssetAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT", "{0:n}")});
            this.xrAssetAmt.Name = "xrAssetAmt";
            resources.ApplyResources(this.xrAssetAmt, "xrAssetAmt");
            this.xrAssetAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAssetAmt_BeforePrint);
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
            // grpLedgerGroup
            // 
            this.grpLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableLedgerGroup});
            this.grpLedgerGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedgerGroup, "grpLedgerGroup");
            this.grpLedgerGroup.Name = "grpLedgerGroup";
            // 
            // xrTableLedgerGroup
            // 
            this.xrTableLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableLedgerGroup, "xrTableLedgerGroup");
            this.xrTableLedgerGroup.Name = "xrTableLedgerGroup";
            this.xrTableLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTableLedgerGroup.StyleName = "styleGroupRow";
            this.xrTableLedgerGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblOPDebit,
            this.xrTableCell2,
            this.xrtblTransDebit});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // xrtblOPDebit
            // 
            this.xrtblOPDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.POP_DEBIT")});
            this.xrtblOPDebit.Name = "xrtblOPDebit";
            this.xrtblOPDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblOPDebit.Summary = xrSummary1;
            resources.ApplyResources(this.xrtblOPDebit, "xrtblOPDebit");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_GROUP")});
            this.xrTableCell2.Name = "xrTableCell2";
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrtblTransDebit
            // 
            this.xrtblTransDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrtblTransDebit.Name = "xrtblTransDebit";
            this.xrtblTransDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblTransDebit.Summary = xrSummary2;
            resources.ApplyResources(this.xrtblTransDebit, "xrtblTransDebit");
            // 
            // BalanceSheetAsset
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpLedgerGroup});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "BalanceSheet";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpLedgerGroup, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblDetail;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrOPAssetAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerAsset;
        private DevExpress.XtraReports.UI.XRTableCell xrAssetAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calLiaTotalAmt;
        private DevExpress.XtraReports.UI.CalculatedField calAssetAmt;
        private DevExpress.XtraReports.UI.CalculatedField calOpLiability;
        private DevExpress.XtraReports.UI.CalculatedField calOpAsset;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedgerGroup;
        private DevExpress.XtraReports.UI.XRTable xrTableLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTransDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrtblOPDebit;
    }
}

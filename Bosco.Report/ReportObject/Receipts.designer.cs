namespace Bosco.Report.ReportObject
{
    partial class Receipts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receipts));
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary2 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary3 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary4 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.grpReceiptLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableReceipt = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpReceiptGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblReceiptGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpCostCentreNameReceipts = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPaymentCostCentreName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblCellCostcentreName = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpcostCenterCategory = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCCCAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpCCBreakup = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrCCBreakup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrParentGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentgroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrPaymentCostCentreName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCostCentreCategoryName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // grpReceiptLedger
            // 
            this.grpReceiptLedger.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableReceipt});
            this.grpReceiptLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpReceiptLedger, "grpReceiptLedger");
            this.grpReceiptLedger.Name = "grpReceiptLedger";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "LEDGER_CODE";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpReceiptLedger.SortingSummary = xrGroupSortingSummary1;
            // 
            // xrTableReceipt
            // 
            resources.ApplyResources(this.xrTableReceipt, "xrTableReceipt");
            this.xrTableReceipt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableReceipt.Name = "xrTableReceipt";
            this.xrTableReceipt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableReceipt.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTableReceipt.StyleName = "styleRow";
            this.xrTableReceipt.StylePriority.UseBorderColor = false;
            this.xrTableReceipt.StylePriority.UseBorders = false;
            this.xrTableReceipt.StylePriority.UseFont = false;
            this.xrTableReceipt.StylePriority.UsePadding = false;
            this.xrTableReceipt.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerCode,
            this.xrLedgerName,
            this.xrLedgerAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrLedgerCode
            // 
            this.xrLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_CODE")});
            this.xrLedgerCode.Name = "xrLedgerCode";
            this.xrLedgerCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerCode.StylePriority.UseBorders = false;
            this.xrLedgerCode.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrLedgerCode, "xrLedgerCode");
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_NAME")});
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3,3, 3, 3, 100F);
            this.xrLedgerName.StylePriority.UseBorders = false;
            this.xrLedgerName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            this.xrLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLedgerName_BeforePrint);
            // 
            // xrLedgerAmt
            // 
            this.xrLedgerAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT", "{0:n}")});
            this.xrLedgerAmt.Name = "xrLedgerAmt";
            this.xrLedgerAmt.StylePriority.UseBorders = false;
            this.xrLedgerAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrLedgerAmt, "xrLedgerAmt");
            this.xrLedgerAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLedgerAmt_BeforePrint);
            // 
            // grpReceiptGroup
            // 
            this.grpReceiptGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblReceiptGroup});
            this.grpReceiptGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpReceiptGroup, "grpReceiptGroup");
            this.grpReceiptGroup.Level = 1;
            this.grpReceiptGroup.Name = "grpReceiptGroup";
            xrGroupSortingSummary2.Enabled = true;
            xrGroupSortingSummary2.FieldName = "GROUP_CODE";
            xrGroupSortingSummary2.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpReceiptGroup.SortingSummary = xrGroupSortingSummary2;
            this.grpReceiptGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpReceiptGroup_BeforePrint);
            // 
            // xrtblReceiptGroup
            // 
            resources.ApplyResources(this.xrtblReceiptGroup, "xrtblReceiptGroup");
            this.xrtblReceiptGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblReceiptGroup.Name = "xrtblReceiptGroup";
            this.xrtblReceiptGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblReceiptGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblReceiptGroup.StyleName = "styleGroupRow";
            this.xrtblReceiptGroup.StylePriority.UseBackColor = false;
            this.xrtblReceiptGroup.StylePriority.UseBorderColor = false;
            this.xrtblReceiptGroup.StylePriority.UseBorders = false;
            this.xrtblReceiptGroup.StylePriority.UseFont = false;
            this.xrtblReceiptGroup.StylePriority.UsePadding = false;
            this.xrtblReceiptGroup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGroupCode,
            this.xrGroupName,
            this.xrGroupAmt});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrGroupCode
            // 
            this.xrGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.GROUP_CODE")});
            this.xrGroupCode.Name = "xrGroupCode";
            this.xrGroupCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroupCode.StylePriority.UseBorders = false;
            this.xrGroupCode.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrGroupCode, "xrGroupCode");
            // 
            // xrGroupName
            // 
            this.xrGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_GROUP")});
            this.xrGroupName.Name = "xrGroupName";
            this.xrGroupName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroupName.StylePriority.UseBorders = false;
            this.xrGroupName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrGroupName, "xrGroupName");
            this.xrGroupName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrGroupName_BeforePrint);
            // 
            // xrGroupAmt
            // 
            this.xrGroupAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            this.xrGroupAmt.Name = "xrGroupAmt";
            this.xrGroupAmt.StylePriority.UseBorders = false;
            this.xrGroupAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGroupAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrGroupAmt, "xrGroupAmt");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpCostCentreNameReceipts
            // 
            this.grpCostCentreNameReceipts.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPaymentCostCentreName});
            this.grpCostCentreNameReceipts.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpCostCentreNameReceipts, "grpCostCentreNameReceipts");
            this.grpCostCentreNameReceipts.Level = 3;
            this.grpCostCentreNameReceipts.Name = "grpCostCentreNameReceipts";
            xrGroupSortingSummary3.Enabled = true;
            xrGroupSortingSummary3.FieldName = "COST_CENTRE_NAME";
            xrGroupSortingSummary3.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpCostCentreNameReceipts.SortingSummary = xrGroupSortingSummary3;
            // 
            // xrPaymentCostCentreName
            // 
            resources.ApplyResources(this.xrPaymentCostCentreName, "xrPaymentCostCentreName");
            this.xrPaymentCostCentreName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrPaymentCostCentreName.Name = "xrPaymentCostCentreName";
            this.xrPaymentCostCentreName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrPaymentCostCentreName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrPaymentCostCentreName.StylePriority.UseBackColor = false;
            this.xrPaymentCostCentreName.StylePriority.UseBorderColor = false;
            this.xrPaymentCostCentreName.StylePriority.UseBorders = false;
            this.xrPaymentCostCentreName.StylePriority.UseFont = false;
            this.xrPaymentCostCentreName.StylePriority.UseForeColor = false;
            this.xrPaymentCostCentreName.StylePriority.UsePadding = false;
            this.xrPaymentCostCentreName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblCellCostcentreName});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrtblCellCostcentreName
            // 
            this.xrtblCellCostcentreName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.COST_CENTRE_NAME")});
            resources.ApplyResources(this.xrtblCellCostcentreName, "xrtblCellCostcentreName");
            this.xrtblCellCostcentreName.Name = "xrtblCellCostcentreName";
            this.xrtblCellCostcentreName.StylePriority.UseFont = false;
            // 
            // grpcostCenterCategory
            // 
            this.grpcostCenterCategory.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblCostCentreCategoryName});
            this.grpcostCenterCategory.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_CATEGORY_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpcostCenterCategory, "grpcostCenterCategory");
            this.grpcostCenterCategory.Level = 4;
            this.grpcostCenterCategory.Name = "grpcostCenterCategory";
            xrGroupSortingSummary4.Enabled = true;
            xrGroupSortingSummary4.FieldName = "COST_CENTRE_CATEGORY_NAME";
            xrGroupSortingSummary4.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpcostCenterCategory.SortingSummary = xrGroupSortingSummary4;
            // 
            // xrTblCostCentreCategoryName
            // 
            resources.ApplyResources(this.xrTblCostCentreCategoryName, "xrTblCostCentreCategoryName");
            this.xrTblCostCentreCategoryName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTblCostCentreCategoryName.Name = "xrTblCostCentreCategoryName";
            this.xrTblCostCentreCategoryName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTblCostCentreCategoryName.StylePriority.UseBackColor = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseBorderColor = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseBorders = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseFont = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCostCentreCategoryName,
            this.xrcellCCCAmount});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrCostCentreCategoryName
            // 
            this.xrCostCentreCategoryName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.COST_CENTRE_CATEGORY_NAME")});
            this.xrCostCentreCategoryName.Name = "xrCostCentreCategoryName";
            this.xrCostCentreCategoryName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCostCentreCategoryName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrCostCentreCategoryName, "xrCostCentreCategoryName");
            // 
            // xrcellCCCAmount
            // 
            this.xrcellCCCAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            this.xrcellCCCAmount.Name = "xrcellCCCAmount";
            this.xrcellCCCAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellCCCAmount.Summary = xrSummary2;
            resources.ApplyResources(this.xrcellCCCAmount, "xrcellCCCAmount");
            // 
            // grpCCBreakup
            // 
            this.grpCCBreakup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrCCBreakup});
            resources.ApplyResources(this.grpCCBreakup, "grpCCBreakup");
            this.grpCCBreakup.Name = "grpCCBreakup";
            // 
            // xrPageBreak1
            // 
            resources.ApplyResources(this.xrPageBreak1, "xrPageBreak1");
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // xrCCBreakup
            // 
            resources.ApplyResources(this.xrCCBreakup, "xrCCBreakup");
            this.xrCCBreakup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCCBreakup.Name = "xrCCBreakup";
            this.xrCCBreakup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCCBreakup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrCCBreakup.StyleName = "styleGroupRow";
            this.xrCCBreakup.StylePriority.UseBackColor = false;
            this.xrCCBreakup.StylePriority.UseBorderColor = false;
            this.xrCCBreakup.StylePriority.UseBorders = false;
            this.xrCCBreakup.StylePriority.UseFont = false;
            this.xrCCBreakup.StylePriority.UsePadding = false;
            this.xrCCBreakup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow5.Name = "xrTableRow5";
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell4.Summary = xrSummary3;
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            // 
            // xrParentGroup
            // 
            resources.ApplyResources(this.xrParentGroup, "xrParentGroup");
            this.xrParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroup.Name = "xrParentGroup";
            this.xrParentGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrParentGroup.StyleName = "styleGroupRow";
            this.xrParentGroup.StylePriority.UseBackColor = false;
            this.xrParentGroup.StylePriority.UseBorderColor = false;
            this.xrParentGroup.StylePriority.UseBorders = false;
            this.xrParentGroup.StylePriority.UseFont = false;
            this.xrParentGroup.StylePriority.UsePadding = false;
            this.xrParentGroup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrParentGroupCode,
            this.xrParentgroupName,
            this.xrParentGroupAmount});
            this.xrTableRow6.Name = "xrTableRow6";
            resources.ApplyResources(this.xrTableRow6, "xrTableRow6");
            // 
            // xrParentGroupCode
            // 
            this.xrParentGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.PARENT_GROUP_CODE")});
            this.xrParentGroupCode.Name = "xrParentGroupCode";
            this.xrParentGroupCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrParentGroupCode.StylePriority.UseBorders = false;
            this.xrParentGroupCode.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrParentGroupCode, "xrParentGroupCode");
            // 
            // xrParentgroupName
            // 
            this.xrParentgroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentgroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.PARENT_GROUP")});
            this.xrParentgroupName.Name = "xrParentgroupName";
            this.xrParentgroupName.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrParentgroupName, "xrParentgroupName");
            this.xrParentgroupName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrParentgroupName_BeforePrint);
            // 
            // xrParentGroupAmount
            // 
            this.xrParentGroupAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroupAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            this.xrParentGroupAmount.Name = "xrParentGroupAmount";
            this.xrParentGroupAmount.StylePriority.UseBorders = false;
            this.xrParentGroupAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrParentGroupAmount.Summary = xrSummary4;
            resources.ApplyResources(this.xrParentGroupAmount, "xrParentGroupAmount");
            // 
            // Receipts
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpReceiptLedger,
            this.grpReceiptGroup,
            this.grpCostCentreNameReceipts,
            this.grpcostCenterCategory,
            this.grpCCBreakup,
            this.grpParentGroup});
            this.DataMember = "Receipts";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpCCBreakup, 0);
            this.Controls.SetChildIndex(this.grpcostCenterCategory, 0);
            this.Controls.SetChildIndex(this.grpCostCentreNameReceipts, 0);
            this.Controls.SetChildIndex(this.grpReceiptGroup, 0);
            this.Controls.SetChildIndex(this.grpReceiptLedger, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrPaymentCostCentreName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCostCentreCategoryName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpReceiptLedger;
        private DevExpress.XtraReports.UI.XRTable xrTableReceipt;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerAmt;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpReceiptGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblReceiptGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCostCentreNameReceipts;
        private DevExpress.XtraReports.UI.XRTable xrPaymentCostCentreName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrtblCellCostcentreName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpcostCenterCategory;
        private DevExpress.XtraReports.UI.XRTable xrTblCostCentreCategoryName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrCostCentreCategoryName;
        private DevExpress.XtraReports.UI.GroupFooterBand grpCCBreakup;
        private DevExpress.XtraReports.UI.XRTable xrCCBreakup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCCAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.XRTable xrParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell xrParentgroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupAmount;
    }
}

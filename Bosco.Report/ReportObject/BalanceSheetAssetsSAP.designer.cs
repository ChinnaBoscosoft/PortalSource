namespace Bosco.Report.ReportObject
{
    partial class BalanceSheetAssetsSAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanceSheetAssetsSAP));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary2 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary3 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            this.xrTblAssetLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblAssetLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetGrpGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpLedgerGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblAssetParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcAssetParentGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetParentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblParentGroupAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubBalance = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = true;
            // 
            // xrTblAssetLedgerName
            // 
            this.xrTblAssetLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblAssetLedgerName, "xrTblAssetLedgerName");
            this.xrTblAssetLedgerName.Name = "xrTblAssetLedgerName";
            this.xrTblAssetLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblAssetLedgerName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblAssetLedgerName.StyleName = "styleRow";
            this.xrTblAssetLedgerName.StylePriority.UseBorderColor = false;
            this.xrTblAssetLedgerName.StylePriority.UseBorders = false;
            this.xrTblAssetLedgerName.StylePriority.UsePadding = false;
            this.xrTblAssetLedgerName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLedgerCode,
            this.tcAssetLedgerName,
            this.tcAssetLedgerAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // tcLedgerCode
            // 
            this.tcLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_CODE")});
            this.tcLedgerCode.Name = "tcLedgerCode";
            resources.ApplyResources(this.tcLedgerCode, "tcLedgerCode");
            // 
            // tcAssetLedgerName
            // 
            this.tcAssetLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_NAME")});
            this.tcAssetLedgerName.Name = "tcAssetLedgerName";
            this.tcAssetLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tcAssetLedgerName.StylePriority.UsePadding = false;
            this.tcAssetLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.tcAssetLedgerName, "tcAssetLedgerName");
            // 
            // tcAssetLedgerAmt
            // 
            this.tcAssetLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.tcAssetLedgerAmt.Name = "tcAssetLedgerAmt";
            this.tcAssetLedgerAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.tcAssetLedgerAmt.Summary = xrSummary1;
            resources.ApplyResources(this.tcAssetLedgerAmt, "tcAssetLedgerAmt");
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
            this.xrTblAssetLedgerName});
            this.grpLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedger, "grpLedger");
            this.grpLedger.Name = "grpLedger";
            xrGroupSortingSummary1.FieldName = "LEDGER_NAME";
            this.grpLedger.SortingSummary = xrGroupSortingSummary1;
            // 
            // xrTblAssetLedgerGroup
            // 
            this.xrTblAssetLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblAssetLedgerGroup, "xrTblAssetLedgerGroup");
            this.xrTblAssetLedgerGroup.Name = "xrTblAssetLedgerGroup";
            this.xrTblAssetLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTblAssetLedgerGroup.StyleName = "styleGroupRow";
            this.xrTblAssetLedgerGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.tcAssetGroupCode,
            this.tcAssetGrpGroupName,
            this.xrtblTransDebit});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            // 
            // tcAssetGroupCode
            // 
            this.tcAssetGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAssetGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.GROUP_CODE")});
            this.tcAssetGroupCode.Name = "tcAssetGroupCode";
            this.tcAssetGroupCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.tcAssetGroupCode, "tcAssetGroupCode");
            // 
            // tcAssetGrpGroupName
            // 
            this.tcAssetGrpGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.LEDGER_GROUP")});
            this.tcAssetGrpGroupName.Name = "tcAssetGrpGroupName";
            resources.ApplyResources(this.tcAssetGrpGroupName, "tcAssetGrpGroupName");
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
            // grpLedgerGroup
            // 
            this.grpLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblAssetLedgerGroup});
            this.grpLedgerGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpLedgerGroup, "grpLedgerGroup");
            this.grpLedgerGroup.Level = 1;
            this.grpLedgerGroup.Name = "grpLedgerGroup";
            xrGroupSortingSummary2.FieldName = "LEDGER_GROUP";
            xrGroupSortingSummary2.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpLedgerGroup.SortingSummary = xrGroupSortingSummary2;
            this.grpLedgerGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpLedgerGroup_BeforePrint);
            // 
            // xrTblAssetParentGroup
            // 
            this.xrTblAssetParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTblAssetParentGroup, "xrTblAssetParentGroup");
            this.xrTblAssetParentGroup.Name = "xrTblAssetParentGroup";
            this.xrTblAssetParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblAssetParentGroup.StyleName = "styleGroupRow";
            this.xrTblAssetParentGroup.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcAssetParentGroupCode,
            this.tcAssetParentGroupName,
            this.xrtblParentGroupAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // tcAssetParentGroupCode
            // 
            this.tcAssetParentGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.PARENT_GROUP_CODE")});
            this.tcAssetParentGroupCode.Name = "tcAssetParentGroupCode";
            resources.ApplyResources(this.tcAssetParentGroupCode, "tcAssetParentGroupCode");
            // 
            // tcAssetParentGroupName
            // 
            this.tcAssetParentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.PARENT_GROUP")});
            this.tcAssetParentGroupName.Name = "tcAssetParentGroupName";
            resources.ApplyResources(this.tcAssetParentGroupName, "tcAssetParentGroupName");
            // 
            // xrtblParentGroupAmount
            // 
            this.xrtblParentGroupAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BalanceSheet.AMOUNT")});
            this.xrtblParentGroupAmount.Name = "xrtblParentGroupAmount";
            this.xrtblParentGroupAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblParentGroupAmount.Summary = xrSummary3;
            resources.ApplyResources(this.xrtblParentGroupAmount, "xrtblParentGroupAmount");
            this.xrtblParentGroupAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtblParentGroupAmount_BeforePrint);
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblAssetParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PARENT_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            xrGroupSortingSummary3.FieldName = "PARENT_GROUP";
            xrGroupSortingSummary3.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpParentGroup.SortingSummary = xrGroupSortingSummary3;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBalance});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrSubBalance
            // 
            resources.ApplyResources(this.xrSubBalance, "xrSubBalance");
            this.xrSubBalance.Name = "xrSubBalance";
            this.xrSubBalance.ReportSource = new Bosco.Report.ReportObject.AccountBalance();
            // 
            // BalanceSheetAssetsSAP
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpLedger,
            this.grpLedgerGroup,
            this.grpParentGroup,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "BalanceSheet";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpLedgerGroup, 0);
            this.Controls.SetChildIndex(this.grpLedger, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTblAssetLedgerName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetLedgerAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calLiaTotalAmt;
        private DevExpress.XtraReports.UI.CalculatedField calAssetAmt;
        private DevExpress.XtraReports.UI.CalculatedField calOpLiability;
        private DevExpress.XtraReports.UI.CalculatedField calOpAsset;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedger;
        private DevExpress.XtraReports.UI.XRTable xrTblAssetLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTransDebit;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpLedgerGroup;
        private DevExpress.XtraReports.UI.XRTable xrTblAssetParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetParentGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrtblParentGroupAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBalance;
        private DevExpress.XtraReports.UI.XRTableCell tcLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetParentGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetGrpGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}

namespace Bosco.Report.ReportObject
{
    partial class GeneralateBalanceSheetAsset
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
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralateBalanceSheetAsset));
            this.xrTblAssetLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcAssetLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrOpeningAssetAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calLiaTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calAssetAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpLiability = new DevExpress.XtraReports.UI.CalculatedField();
            this.calOpAsset = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAssetGrpGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrOpeningTotalAssetAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTblAssetLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.grpMaster = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrsubGBConLedgerDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrsubGBConLedgerDetails,
            this.xrTblAssetLedgerName});
            this.Detail.Expanded = true;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
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
            this.xrTblAssetLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblAssetLedgerName_BeforePrint);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcAssetLedgerName,
            this.xrOpeningAssetAmount,
            this.tcAssetLedgerAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // tcAssetLedgerName
            // 
            this.tcAssetLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAssetLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.CON_LEDGER_NAME")});
            resources.ApplyResources(this.tcAssetLedgerName, "tcAssetLedgerName");
            this.tcAssetLedgerName.Name = "tcAssetLedgerName";
            this.tcAssetLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.tcAssetLedgerName.StylePriority.UseBorders = false;
            this.tcAssetLedgerName.StylePriority.UseFont = false;
            this.tcAssetLedgerName.StylePriority.UsePadding = false;
            this.tcAssetLedgerName.StylePriority.UseTextAlignment = false;
            this.tcAssetLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.tcAssetLedgerName_BeforePrint);
            // 
            // xrOpeningAssetAmount
            // 
            this.xrOpeningAssetAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrOpeningAssetAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.OP_AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrOpeningAssetAmount, "xrOpeningAssetAmount");
            this.xrOpeningAssetAmount.Name = "xrOpeningAssetAmount";
            this.xrOpeningAssetAmount.StylePriority.UseBorders = false;
            this.xrOpeningAssetAmount.StylePriority.UseFont = false;
            this.xrOpeningAssetAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrOpeningAssetAmount.Summary = xrSummary1;
            this.xrOpeningAssetAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrOpeningAssetAmount_BeforePrint);
            // 
            // tcAssetLedgerAmt
            // 
            this.tcAssetLedgerAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAssetLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT", "{0:n}")});
            resources.ApplyResources(this.tcAssetLedgerAmt, "tcAssetLedgerAmt");
            this.tcAssetLedgerAmt.Name = "tcAssetLedgerAmt";
            this.tcAssetLedgerAmt.StylePriority.UseBorders = false;
            this.tcAssetLedgerAmt.StylePriority.UseFont = false;
            this.tcAssetLedgerAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            this.tcAssetLedgerAmt.Summary = xrSummary2;
            this.tcAssetLedgerAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.tcAssetLedgerAmt_BeforePrint);
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
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblTransDebit
            // 
            this.xrtblTransDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            resources.ApplyResources(this.xrtblTransDebit, "xrtblTransDebit");
            this.xrtblTransDebit.Name = "xrtblTransDebit";
            this.xrtblTransDebit.StylePriority.UseFont = false;
            this.xrtblTransDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblTransDebit.Summary = xrSummary3;
            this.xrtblTransDebit.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrtblTransDebit_SummaryGetResult);
            // 
            // tcAssetGrpGroupName
            // 
            this.tcAssetGrpGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAssetGrpGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.MASTER_NAME")});
            resources.ApplyResources(this.tcAssetGrpGroupName, "tcAssetGrpGroupName");
            this.tcAssetGrpGroupName.Name = "tcAssetGrpGroupName";
            this.tcAssetGrpGroupName.StylePriority.UseBorders = false;
            this.tcAssetGrpGroupName.StylePriority.UseFont = false;
            this.tcAssetGrpGroupName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.tcAssetGrpGroupName_BeforePrint);
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcAssetGrpGroupName,
            this.xrOpeningTotalAssetAmount,
            this.xrtblTransDebit});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // xrOpeningTotalAssetAmount
            // 
            this.xrOpeningTotalAssetAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.OP_AMOUNT")});
            resources.ApplyResources(this.xrOpeningTotalAssetAmount, "xrOpeningTotalAssetAmount");
            this.xrOpeningTotalAssetAmount.Name = "xrOpeningTotalAssetAmount";
            this.xrOpeningTotalAssetAmount.StylePriority.UseFont = false;
            this.xrOpeningTotalAssetAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrOpeningTotalAssetAmount.Summary = xrSummary4;
            this.xrOpeningTotalAssetAmount.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrOpeningTotalAssetAmount_SummaryGetResult);
            this.xrOpeningTotalAssetAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrOpeningTotalAssetAmount_BeforePrint);
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
            // grpMaster
            // 
            this.grpMaster.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblAssetLedgerGroup});
            this.grpMaster.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MASTER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpMaster, "grpMaster");
            this.grpMaster.Name = "grpMaster";
            xrGroupSortingSummary1.FieldName = "LEDGER_GROUP";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpMaster.SortingSummary = xrGroupSortingSummary1;
            this.grpMaster.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpLedgerGroup_BeforePrint);
            // 
            // xrsubGBConLedgerDetails
            // 
            this.xrsubGBConLedgerDetails.CanShrink = true;
            resources.ApplyResources(this.xrsubGBConLedgerDetails, "xrsubGBConLedgerDetails");
            this.xrsubGBConLedgerDetails.Name = "xrsubGBConLedgerDetails";
            this.xrsubGBConLedgerDetails.ReportSource = new Bosco.Report.ReportObject.GeneralateBalanceSheetDetail();
            this.xrsubGBConLedgerDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrsubGBConLedgerDetails_BeforePrint_1);
            // 
            // GeneralateBalanceSheetAsset
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpMaster,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calLiaTotalAmt,
            this.calAssetAmt,
            this.calOpLiability,
            this.calOpAsset});
            this.DataMember = "CongiregationProfitandLoss";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpMaster, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblAssetLedgerGroup)).EndInit();
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
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTransDebit;
        private DevExpress.XtraReports.UI.XRTableCell tcAssetGrpGroupName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTable xrTblAssetLedgerGroup;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpMaster;
        private DevExpress.XtraReports.UI.XRTableCell xrOpeningAssetAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrOpeningTotalAssetAmount;
        private DevExpress.XtraReports.UI.XRSubreport xrsubGBConLedgerDetails;
    }
}

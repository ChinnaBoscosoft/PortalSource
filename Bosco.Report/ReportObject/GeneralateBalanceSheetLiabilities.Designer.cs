namespace Bosco.Report.ReportObject
{
    partial class GeneralateBalanceSheetLiabilities
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrGrpSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.grpCongregatoionParent = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.grpCongregatoionGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrConLedgerSum = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.Visible = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 19.00001F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrGrpSum
            // 
            this.xrGrpSum.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrGrpSum.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGrpSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            this.xrGrpSum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrGrpSum.Name = "xrGrpSum";
            this.xrGrpSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrGrpSum.StylePriority.UseBorderColor = false;
            this.xrGrpSum.StylePriority.UseBorders = false;
            this.xrGrpSum.StylePriority.UseFont = false;
            this.xrGrpSum.StylePriority.UsePadding = false;
            this.xrGrpSum.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGrpSum.Summary = xrSummary1;
            this.xrGrpSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrGrpSum.Weight = 1.0732911538680934D;
            
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.MASTER_NAME")});
            this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.9022844678462203D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrGrpSum});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(530.9717F, 25F);
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // grpCongregatoionParent
            // 
            this.grpCongregatoionParent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.grpCongregatoionParent.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MASTER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpCongregatoionParent.HeightF = 25F;
            this.grpCongregatoionParent.Level = 1;
            this.grpCongregatoionParent.Name = "grpCongregatoionParent";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // grpCongregatoionGroup
            // 
            this.grpCongregatoionGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.grpCongregatoionGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CON_LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpCongregatoionGroup.HeightF = 25F;
            this.grpCongregatoionGroup.Name = "grpCongregatoionGroup";
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(530.9717F, 25F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerName,
            this.xrConLedgerSum});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.CON_LEDGER_NAME")});
            this.xrLedgerName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerName.StylePriority.UseBorderColor = false;
            this.xrLedgerName.StylePriority.UseBorders = false;
            this.xrLedgerName.StylePriority.UseFont = false;
            this.xrLedgerName.StylePriority.UsePadding = false;
            this.xrLedgerName.StylePriority.UseTextAlignment = false;
            this.xrLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLedgerName.Weight = 1.9022842969222824D;
            // 
            // xrConLedgerSum
            // 
            this.xrConLedgerSum.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrConLedgerSum.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrConLedgerSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            this.xrConLedgerSum.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrConLedgerSum.Name = "xrConLedgerSum";
            this.xrConLedgerSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrConLedgerSum.StylePriority.UseBorderColor = false;
            this.xrConLedgerSum.StylePriority.UseBorders = false;
            this.xrConLedgerSum.StylePriority.UseFont = false;
            this.xrConLedgerSum.StylePriority.UsePadding = false;
            this.xrConLedgerSum.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrConLedgerSum.Summary = xrSummary2;
            this.xrConLedgerSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrConLedgerSum.Weight = 1.0732911537709298D;
            
            // 
            // GeneralateLoss
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.grpCongregatoionParent,
            this.grpCongregatoionGroup});
            this.DataMember = "CongiregationProfitandLoss";
            this.DataSource = this.reportSetting1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(100, 190, 19, 100);
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrGrpSum;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCongregatoionParent;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCongregatoionGroup;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrConLedgerSum;
    }
}

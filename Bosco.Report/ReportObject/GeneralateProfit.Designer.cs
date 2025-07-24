namespace Bosco.Report.ReportObject
{
    partial class GeneralateProfit
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrConLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpCongregatoionParent = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrConParentGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrpSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpCongregatoion = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrsubPLConLedgerDetails = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpHeaderProjectCategoryGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.Visible = false;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(530F, 25F);
            this.xrTable2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable2_BeforePrint);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrConLedgerName,
            this.xrLedgerSum});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrConLedgerName
            // 
            this.xrConLedgerName.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrConLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrConLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.CON_LEDGER_NAME")});
            this.xrConLedgerName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrConLedgerName.Name = "xrConLedgerName";
            this.xrConLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 3, 3, 3, 100F);
            this.xrConLedgerName.StylePriority.UseBorderColor = false;
            this.xrConLedgerName.StylePriority.UseBorders = false;
            this.xrConLedgerName.StylePriority.UseFont = false;
            this.xrConLedgerName.StylePriority.UsePadding = false;
            this.xrConLedgerName.StylePriority.UseTextAlignment = false;
            this.xrConLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrConLedgerName.Weight = 1.9084741209911758D;
            this.xrConLedgerName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrConLedgerName_BeforePrint);
            // 
            // xrLedgerSum
            // 
            this.xrLedgerSum.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrLedgerSum.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            this.xrLedgerSum.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLedgerSum.Name = "xrLedgerSum";
            this.xrLedgerSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrLedgerSum.StylePriority.UseBorderColor = false;
            this.xrLedgerSum.StylePriority.UseBorders = false;
            this.xrLedgerSum.StylePriority.UseFont = false;
            this.xrLedgerSum.StylePriority.UsePadding = false;
            this.xrLedgerSum.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLedgerSum.Summary = xrSummary1;
            this.xrLedgerSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrLedgerSum.Weight = 1.0665002089864453D;
            this.xrLedgerSum.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLedgerSum_BeforePrint);
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "[CON_MAIN_PARENT_ID]==[mailledgerid] && [CON_LEDGER_ID]==[mailledgerid]";
            this.formattingRule1.DataMember = "CongiregationProfitandLoss";
            // 
            // 
            // 
            this.formattingRule1.Formatting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formattingRule1.Name = "formattingRule1";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 30F;
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
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(530F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrConParentGroup,
            this.xrGrpSum});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrConParentGroup
            // 
            this.xrConParentGroup.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrConParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrConParentGroup.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.MASTER_NAME")});
            this.xrConParentGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrConParentGroup.Name = "xrConParentGroup";
            this.xrConParentGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrConParentGroup.StylePriority.UseBorderColor = false;
            this.xrConParentGroup.StylePriority.UseBorders = false;
            this.xrConParentGroup.StylePriority.UseFont = false;
            this.xrConParentGroup.StylePriority.UsePadding = false;
            this.xrConParentGroup.StylePriority.UseTextAlignment = false;
            this.xrConParentGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrConParentGroup.Weight = 1.9413622892212661D;
            this.xrConParentGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrConParentGroup_BeforePrint);
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
            this.xrGrpSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrGrpSum.StylePriority.UseBorderColor = false;
            this.xrGrpSum.StylePriority.UseBorders = false;
            this.xrGrpSum.StylePriority.UseFont = false;
            this.xrGrpSum.StylePriority.UsePadding = false;
            this.xrGrpSum.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGrpSum.Summary = xrSummary2;
            this.xrGrpSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrGrpSum.Weight = 1.0848788591515393D;
            // 
            // grpCongregatoion
            // 
            this.grpCongregatoion.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrsubPLConLedgerDetails,
            this.xrTable2});
            this.grpCongregatoion.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CON_LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpCongregatoion.HeightF = 47.99999F;
            this.grpCongregatoion.Name = "grpCongregatoion";
            this.grpCongregatoion.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpCongregatoion_BeforePrint);
            // 
            // xrsubPLConLedgerDetails
            // 
            this.xrsubPLConLedgerDetails.CanShrink = true;
            this.xrsubPLConLedgerDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99999F);
            this.xrsubPLConLedgerDetails.Name = "xrsubPLConLedgerDetails";
            this.xrsubPLConLedgerDetails.ReportSource = new Bosco.Report.ReportObject.GeneralateBalanceSheetDetail();
            this.xrsubPLConLedgerDetails.SizeF = new System.Drawing.SizeF(532F, 23F);
            this.xrsubPLConLedgerDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrsubPLConLedgerDetails_BeforePrint);
            // 
            // grpHeaderProjectCategoryGroup
            // 
            this.grpHeaderProjectCategoryGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.grpHeaderProjectCategoryGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PROJECT_CATOGORY_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderProjectCategoryGroup.HeightF = 25F;
            this.grpHeaderProjectCategoryGroup.Level = 2;
            this.grpHeaderProjectCategoryGroup.Name = "grpHeaderProjectCategoryGroup";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(530F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.PROJECT_CATOGORY_GROUP")});
            this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 1.945128295389184D;
            this.xrTableCell2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell2_BeforePrint);
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CongiregationProfitandLoss.AMOUNT")});
            this.xrTableCell3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell3.Summary = xrSummary3;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 1.086983461896033D;
            // 
            // GeneralateProfit
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.grpCongregatoionParent,
            this.grpCongregatoion,
            this.grpHeaderProjectCategoryGroup});
            this.DataMember = "CongiregationProfitandLoss";
            this.DataSource = this.reportSetting1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(89, 219, 30, 100);
            this.ReportPrintOptions.DetailCountAtDesignTime = 1;
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrConLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerSum;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCongregatoionParent;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrConParentGroup;
        private DevExpress.XtraReports.UI.XRTableCell xrGrpSum;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCongregatoion;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderProjectCategoryGroup;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRSubreport xrsubPLConLedgerDetails;
    }
}

namespace Bosco.Report.ReportObject
{
    partial class AccountBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountBalance));
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.grpBalanceGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcGroupAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcGroupAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpBalanceLedger = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            this.styleOddRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrStyleOddRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // grpBalanceGroup
            // 
            this.grpBalanceGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableLedgerGroup});
            this.grpBalanceGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpBalanceGroup, "grpBalanceGroup");
            this.grpBalanceGroup.Level = 1;
            this.grpBalanceGroup.Name = "grpBalanceGroup";
            xrGroupSortingSummary1.FieldName = "LEDGER_CODE";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpBalanceGroup.SortingSummary = xrGroupSortingSummary1;
            // 
            // xrTableLedgerGroup
            // 
            resources.ApplyResources(this.xrTableLedgerGroup, "xrTableLedgerGroup");
            this.xrTableLedgerGroup.Name = "xrTableLedgerGroup";
            this.xrTableLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTableLedgerGroup.StyleName = "styleGroupRow";
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcGroupName,
            this.tcGroupAmountPeriod,
            this.tcGroupAmountProgress});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // tcGroupName
            // 
            this.tcGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.LEDGER_GROUP")});
            this.tcGroupName.Name = "tcGroupName";
            this.tcGroupName.StylePriority.UseBorders = false;
            resources.ApplyResources(this.tcGroupName, "tcGroupName");
            // 
            // tcGroupAmountPeriod
            // 
            this.tcGroupAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcGroupAmountPeriod.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT")});
            this.tcGroupAmountPeriod.Name = "tcGroupAmountPeriod";
            this.tcGroupAmountPeriod.StylePriority.UseBorders = false;
            this.tcGroupAmountPeriod.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcGroupAmountPeriod.Summary = xrSummary1;
            resources.ApplyResources(this.tcGroupAmountPeriod, "tcGroupAmountPeriod");
            // 
            // tcGroupAmountProgress
            // 
            this.tcGroupAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcGroupAmountProgress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT_PROGRESS")});
            this.tcGroupAmountProgress.Name = "tcGroupAmountProgress";
            this.tcGroupAmountProgress.StylePriority.UseBorders = false;
            this.tcGroupAmountProgress.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcGroupAmountProgress.Summary = xrSummary2;
            resources.ApplyResources(this.tcGroupAmountProgress, "tcGroupAmountProgress");
            // 
            // grpBalanceLedger
            // 
            this.grpBalanceLedger.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLedger});
            this.grpBalanceLedger.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpBalanceLedger, "grpBalanceLedger");
            this.grpBalanceLedger.Name = "grpBalanceLedger";
            // 
            // xrtblLedger
            // 
            resources.ApplyResources(this.xrtblLedger, "xrtblLedger");
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrtblLedger.StyleName = "styleRow";
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLedgerName,
            this.tcAmountPeriod,
            this.tcAmountProgress});
            this.xrTableRow8.Name = "xrTableRow8";
            resources.ApplyResources(this.xrTableRow8, "xrTableRow8");
            // 
            // tcLedgerName
            // 
            this.tcLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.LEDGER_NAME")});
            this.tcLedgerName.Name = "tcLedgerName";
            this.tcLedgerName.StylePriority.UseBorders = false;
            this.tcLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.tcLedgerName, "tcLedgerName");
            // 
            // tcAmountPeriod
            // 
            this.tcAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAmountPeriod.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT")});
            this.tcAmountPeriod.Name = "tcAmountPeriod";
            this.tcAmountPeriod.StylePriority.UseBorders = false;
            this.tcAmountPeriod.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcAmountPeriod.Summary = xrSummary3;
            resources.ApplyResources(this.tcAmountPeriod, "tcAmountPeriod");
            // 
            // tcAmountProgress
            // 
            this.tcAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAmountProgress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AccountBalance.AMOUNT_PROGRESS")});
            this.tcAmountProgress.Name = "tcAmountProgress";
            this.tcAmountProgress.StylePriority.UseBorders = false;
            this.tcAmountProgress.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tcAmountProgress.Summary = xrSummary4;
            resources.ApplyResources(this.tcAmountProgress, "tcAmountProgress");
            // 
            // styleOddRow
            // 
            this.styleOddRow.BackColor = System.Drawing.Color.DarkOrange;
            this.styleOddRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleOddRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleOddRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleOddRow.BorderWidth = 1F;
            this.styleOddRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleOddRow.ForeColor = System.Drawing.Color.SpringGreen;
            this.styleOddRow.Name = "styleOddRow";
            this.styleOddRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.IndianRed;
            this.styleEvenRow.BorderColor = System.Drawing.Color.Silver;
            this.styleEvenRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleEvenRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleEvenRow.BorderWidth = 1F;
            this.styleEvenRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEvenRow.Name = "styleEvenRow";
            this.styleEvenRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // xrStyleOddRow
            // 
            this.xrStyleOddRow.BackColor = System.Drawing.Color.DarkOrange;
            this.xrStyleOddRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrStyleOddRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrStyleOddRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrStyleOddRow.BorderWidth = 1F;
            this.xrStyleOddRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrStyleOddRow.ForeColor = System.Drawing.Color.SpringGreen;
            this.xrStyleOddRow.Name = "xrStyleOddRow";
            this.xrStyleOddRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1F;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleGroupRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleRow
            // 
            this.styleRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRow.BorderWidth = 1F;
            this.styleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRow.Name = "styleRow";
            this.styleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeader
            // 
            this.styleColumnHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeader.BorderWidth = 1F;
            this.styleColumnHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeader.Name = "styleColumnHeader";
            this.styleColumnHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleColumnHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1F;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.ForeColor = System.Drawing.Color.Black;
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AccountBalance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.grpBalanceGroup,
            this.grpBalanceLedger});
            this.DataMember = "AccountBalance";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleOddRow,
            this.styleEvenRow,
            this.xrStyleOddRow,
            this.styleGroupRow,
            this.styleRow,
            this.styleColumnHeader,
            this.styleTotalRow});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBalanceGroup;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBalanceLedger;
        private DevExpress.XtraReports.UI.XRControlStyle styleOddRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleEvenRow;
        private DevExpress.XtraReports.UI.XRControlStyle xrStyleOddRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeader;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRow;
        private DevExpress.XtraReports.UI.XRTable xrTableLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupName;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcGroupAmountProgress;
        private DevExpress.XtraReports.UI.XRTable xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell tcLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountProgress;
        private ReportSetting reportSetting1;
    }
}

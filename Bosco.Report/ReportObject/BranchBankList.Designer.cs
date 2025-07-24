namespace Bosco.Report.ReportObject
{
    partial class BranchBankList
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
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcCapAmountPeriod = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcCapAmountProgress = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLedger});
            this.Detail.HeightF = 25F;
            this.Detail.Visible = true;
            // 
            // xrtblLedger
            // 
            this.xrtblLedger.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblLedger.LocationFloat = new DevExpress.Utils.PointFloat(2.000817F, 0F);
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrtblLedger.SizeF = new System.Drawing.SizeF(723F, 25F);
            this.xrtblLedger.StyleName = "styleRow";
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcLedgerName,
            this.tcAmountPeriod,
            this.tcAmountProgress});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // tcLedgerName
            // 
            this.tcLedgerName.BackColor = System.Drawing.Color.Transparent;
            this.tcLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MonthWiseLedger.BRANCH_OFFICE_NAME"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "MonthlyAbstract.LEDGER_NAME")});
            this.tcLedgerName.Name = "tcLedgerName";
            this.tcLedgerName.StylePriority.UseBackColor = false;
            this.tcLedgerName.StylePriority.UseBorders = false;
            this.tcLedgerName.StylePriority.UseTextAlignment = false;
            this.tcLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcLedgerName.Weight = 2.3357034006843245D;
            // 
            // tcAmountPeriod
            // 
            this.tcAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAmountPeriod.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MonthWiseLedger.PROJECT")});
            this.tcAmountPeriod.Name = "tcAmountPeriod";
            this.tcAmountPeriod.StylePriority.UseBorders = false;
            this.tcAmountPeriod.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            this.tcAmountPeriod.Summary = xrSummary1;
            this.tcAmountPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcAmountPeriod.Weight = 2.3426460993773119D;
            // 
            // tcAmountProgress
            // 
            this.tcAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcAmountProgress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MonthWiseLedger.BANK_ACCOUNT")});
            this.tcAmountProgress.Name = "tcAmountProgress";
            this.tcAmountProgress.StylePriority.UseBorders = false;
            this.tcAmountProgress.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            this.tcAmountProgress.Summary = xrSummary2;
            this.tcAmountProgress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcAmountProgress.Weight = 2.551650461654341D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableHeader});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTableHeader
            // 
            this.xrTableHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableHeader.Name = "xrTableHeader";
            this.xrTableHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTableHeader.SizeF = new System.Drawing.SizeF(722.9998F, 25F);
            this.xrTableHeader.StyleName = "styleColumnHeader";
            this.xrTableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcCapParticulars,
            this.tcCapAmountPeriod,
            this.tcCapAmountProgress});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // tcCapParticulars
            // 
            this.tcCapParticulars.BorderColor = System.Drawing.Color.DarkGray;
            this.tcCapParticulars.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcCapParticulars.Name = "tcCapParticulars";
            this.tcCapParticulars.StylePriority.UseBackColor = false;
            this.tcCapParticulars.StylePriority.UseBorderColor = false;
            this.tcCapParticulars.StylePriority.UseBorders = false;
            this.tcCapParticulars.Text = "Branch";
            this.tcCapParticulars.Weight = 2.3357029378856562D;
            // 
            // tcCapAmountPeriod
            // 
            this.tcCapAmountPeriod.BorderColor = System.Drawing.Color.DarkGray;
            this.tcCapAmountPeriod.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcCapAmountPeriod.Name = "tcCapAmountPeriod";
            this.tcCapAmountPeriod.StylePriority.UseBorderColor = false;
            this.tcCapAmountPeriod.StylePriority.UseBorders = false;
            this.tcCapAmountPeriod.StylePriority.UseTextAlignment = false;
            this.tcCapAmountPeriod.Text = "Project";
            this.tcCapAmountPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcCapAmountPeriod.Weight = 2.3426465940408532D;
            // 
            // tcCapAmountProgress
            // 
            this.tcCapAmountProgress.BorderColor = System.Drawing.Color.DarkGray;
            this.tcCapAmountProgress.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcCapAmountProgress.Name = "tcCapAmountProgress";
            this.tcCapAmountProgress.StylePriority.UseBorderColor = false;
            this.tcCapAmountProgress.StylePriority.UseBorders = false;
            this.tcCapAmountProgress.StylePriority.UseTextAlignment = false;
            this.tcCapAmountProgress.Text = "Bank Account";
            this.tcCapAmountProgress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcCapAmountProgress.Weight = 2.5516480300256887D;
            // 
            // BranchBankList
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "MonthWiseLedger";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell tcLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcAmountProgress;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTableHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell tcCapAmountPeriod;
        private DevExpress.XtraReports.UI.XRTableCell tcCapAmountProgress;
    }
}

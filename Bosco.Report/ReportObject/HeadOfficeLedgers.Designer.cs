namespace Bosco.Report.ReportObject
{
    partial class HeadOfficeLedgers
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
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.Expanded = true;
            this.Detail.HeightF = 25F;
            this.Detail.Visible = true;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(724.9998F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCode,
            this.xrLedger,
            this.xrGroup,
            this.xrTableCell2});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCode
            // 
            this.xrCode.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.LEDGER_CODE")});
            this.xrCode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrCode.Name = "xrCode";
            this.xrCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCode.StylePriority.UseBorderColor = false;
            this.xrCode.StylePriority.UseBorders = false;
            this.xrCode.StylePriority.UseFont = false;
            this.xrCode.StylePriority.UsePadding = false;
            this.xrCode.StylePriority.UseTextAlignment = false;
            this.xrCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCode.Weight = 0.3582762261608195D;
            // 
            // xrLedger
            // 
            this.xrLedger.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.LEDGER_NAME")});
            this.xrLedger.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLedger.Name = "xrLedger";
            this.xrLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedger.StylePriority.UseBorderColor = false;
            this.xrLedger.StylePriority.UseBorders = false;
            this.xrLedger.StylePriority.UseFont = false;
            this.xrLedger.StylePriority.UsePadding = false;
            this.xrLedger.StylePriority.UseTextAlignment = false;
            this.xrLedger.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLedger.Weight = 1.1029321338561091D;
            // 
            // xrGroup
            // 
            this.xrGroup.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGroup.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.LEDGER_GROUP")});
            this.xrGroup.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrGroup.Name = "xrGroup";
            this.xrGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroup.StylePriority.UseBorderColor = false;
            this.xrGroup.StylePriority.UseBorders = false;
            this.xrGroup.StylePriority.UseFont = false;
            this.xrGroup.StylePriority.UsePadding = false;
            this.xrGroup.StylePriority.UseTextAlignment = false;
            this.xrGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrGroup.Weight = 0.89008710518767808D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.NATURE")});
            this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.Weight = 0.64870453479539358D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(725.0005F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerCode,
            this.xrLedgerName,
            this.xrLedgerGroup,
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrLedgerCode
            // 
            this.xrLedgerCode.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLedgerCode.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrLedgerCode.Name = "xrLedgerCode";
            this.xrLedgerCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerCode.StylePriority.UseBackColor = false;
            this.xrLedgerCode.StylePriority.UseBorderColor = false;
            this.xrLedgerCode.StylePriority.UseBorders = false;
            this.xrLedgerCode.StylePriority.UseFont = false;
            this.xrLedgerCode.StylePriority.UsePadding = false;
            this.xrLedgerCode.Text = "Code";
            this.xrLedgerCode.Weight = 0.35827587333252064D;
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLedgerName.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerName.StylePriority.UseBackColor = false;
            this.xrLedgerName.StylePriority.UseBorderColor = false;
            this.xrLedgerName.StylePriority.UseBorders = false;
            this.xrLedgerName.StylePriority.UseFont = false;
            this.xrLedgerName.StylePriority.UsePadding = false;
            this.xrLedgerName.Text = "Ledger Name";
            this.xrLedgerName.Weight = 1.1029313439982866D;
            // 
            // xrLedgerGroup
            // 
            this.xrLedgerGroup.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLedgerGroup.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLedgerGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrLedgerGroup.Name = "xrLedgerGroup";
            this.xrLedgerGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerGroup.StylePriority.UseBackColor = false;
            this.xrLedgerGroup.StylePriority.UseBorderColor = false;
            this.xrLedgerGroup.StylePriority.UseBorders = false;
            this.xrLedgerGroup.StylePriority.UseFont = false;
            this.xrLedgerGroup.StylePriority.UsePadding = false;
            this.xrLedgerGroup.StylePriority.UseTextAlignment = false;
            this.xrLedgerGroup.Text = "Group";
            this.xrLedgerGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLedgerGroup.Weight = 0.89008604940998826D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.Text = "Nature";
            this.xrTableCell1.Weight = 0.64870673325920447D;
            // 
            // HeadOfficeLedgers
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "TrialBalance";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrGroup;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}

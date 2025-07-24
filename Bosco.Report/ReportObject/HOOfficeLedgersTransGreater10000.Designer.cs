namespace Bosco.Report.ReportObject
{
    partial class HOOfficeLedgersTransGreater10000
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
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GHBranch = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrVouceDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNarrations = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrBranchOffice = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooterGrand = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GFTotal = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblGroupFooterTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
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
            this.xrTableCell3,
            this.xrLedger,
            this.xrtblAmount,
            this.xrTableCell5});
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.VOUCHER_DATE", "{0:d}")});
            this.xrCode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrCode.Name = "xrCode";
            this.xrCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCode.StylePriority.UseBorderColor = false;
            this.xrCode.StylePriority.UseBorders = false;
            this.xrCode.StylePriority.UseFont = false;
            this.xrCode.StylePriority.UsePadding = false;
            this.xrCode.StylePriority.UseTextAlignment = false;
            this.xrCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCode.Weight = 0.3553450541348297D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.VOUCHER_NO")});
            this.xrTableCell3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.21780314717549865D;
            // 
            // xrLedger
            // 
            this.xrLedger.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.LEDGER_NAME")});
            this.xrLedger.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLedger.Name = "xrLedger";
            this.xrLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedger.StylePriority.UseBorderColor = false;
            this.xrLedger.StylePriority.UseBorders = false;
            this.xrLedger.StylePriority.UseFont = false;
            this.xrLedger.StylePriority.UsePadding = false;
            this.xrLedger.StylePriority.UseTextAlignment = false;
            this.xrLedger.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLedger.Weight = 1.2436650542998358D;
            // 
            // xrtblAmount
            // 
            this.xrtblAmount.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.AMOUNT", "{0:n}")});
            this.xrtblAmount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblAmount.Name = "xrtblAmount";
            this.xrtblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblAmount.StylePriority.UseBorderColor = false;
            this.xrtblAmount.StylePriority.UseBorders = false;
            this.xrtblAmount.StylePriority.UseFont = false;
            this.xrtblAmount.StylePriority.UsePadding = false;
            this.xrtblAmount.StylePriority.UseTextAlignment = false;
            this.xrtblAmount.Text = "z";
            this.xrtblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrtblAmount.Weight = 0.37715275785949903D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.NARRATION")});
            this.xrTableCell5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 0.80603398653033742D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GHBranch
            // 
            this.GHBranch.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6,
            this.xrTable1});
            this.GHBranch.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PROJECT_ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GHBranch.HeightF = 50F;
            this.GHBranch.Name = "GHBranch";
            // 
            // xrTable6
            // 
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable6.SizeF = new System.Drawing.SizeF(725.0005F, 25F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.PROJECT")});
            this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.Weight = 3D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 25F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(725.0005F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrVouceDate,
            this.xrtblVoucherNo,
            this.xrLedgerName,
            this.xrAmount,
            this.xrNarrations});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrVouceDate
            // 
            this.xrVouceDate.BackColor = System.Drawing.Color.Gainsboro;
            this.xrVouceDate.BorderColor = System.Drawing.Color.DarkGray;
            this.xrVouceDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrVouceDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrVouceDate.Name = "xrVouceDate";
            this.xrVouceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrVouceDate.StylePriority.UseBackColor = false;
            this.xrVouceDate.StylePriority.UseBorderColor = false;
            this.xrVouceDate.StylePriority.UseBorders = false;
            this.xrVouceDate.StylePriority.UseFont = false;
            this.xrVouceDate.StylePriority.UsePadding = false;
            this.xrVouceDate.Text = "Date";
            this.xrVouceDate.Weight = 0.35534415680956566D;
            // 
            // xrtblVoucherNo
            // 
            this.xrtblVoucherNo.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblVoucherNo.BorderColor = System.Drawing.Color.DarkGray;
            this.xrtblVoucherNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblVoucherNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblVoucherNo.Name = "xrtblVoucherNo";
            this.xrtblVoucherNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblVoucherNo.StylePriority.UseBackColor = false;
            this.xrtblVoucherNo.StylePriority.UseBorderColor = false;
            this.xrtblVoucherNo.StylePriority.UseBorders = false;
            this.xrtblVoucherNo.StylePriority.UseFont = false;
            this.xrtblVoucherNo.StylePriority.UsePadding = false;
            this.xrtblVoucherNo.StylePriority.UseTextAlignment = false;
            this.xrtblVoucherNo.Text = "V.No";
            this.xrtblVoucherNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtblVoucherNo.Weight = 0.21780291390900203D;
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
            this.xrLedgerName.StylePriority.UseTextAlignment = false;
            this.xrLedgerName.Text = "Ledger Name";
            this.xrLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLedgerName.Weight = 1.2436640920231061D;
            // 
            // xrAmount
            // 
            this.xrAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.xrAmount.BorderColor = System.Drawing.Color.DarkGray;
            this.xrAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrAmount.StylePriority.UseBackColor = false;
            this.xrAmount.StylePriority.UseBorderColor = false;
            this.xrAmount.StylePriority.UseBorders = false;
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            this.xrAmount.Text = "Amount";
            this.xrAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAmount.Weight = 0.37715217182302541D;
            // 
            // xrNarrations
            // 
            this.xrNarrations.BackColor = System.Drawing.Color.Gainsboro;
            this.xrNarrations.BorderColor = System.Drawing.Color.DarkGray;
            this.xrNarrations.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrNarrations.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrNarrations.Name = "xrNarrations";
            this.xrNarrations.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrNarrations.StylePriority.UseBackColor = false;
            this.xrNarrations.StylePriority.UseBorderColor = false;
            this.xrNarrations.StylePriority.UseBorders = false;
            this.xrNarrations.StylePriority.UseFont = false;
            this.xrNarrations.StylePriority.UsePadding = false;
            this.xrNarrations.StylePriority.UseTextAlignment = false;
            this.xrNarrations.Text = "Narration";
            this.xrNarrations.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrNarrations.Weight = 0.80603666543530073D;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(725.0005F, 25F);
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrBranchOffice});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrBranchOffice
            // 
            this.xrBranchOffice.BackColor = System.Drawing.Color.White;
            this.xrBranchOffice.BorderColor = System.Drawing.Color.DarkGray;
            this.xrBranchOffice.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrBranchOffice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.BRANCH_OFFICE_NAME")});
            this.xrBranchOffice.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.xrBranchOffice.Name = "xrBranchOffice";
            this.xrBranchOffice.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrBranchOffice.StylePriority.UseBackColor = false;
            this.xrBranchOffice.StylePriority.UseBorderColor = false;
            this.xrBranchOffice.StylePriority.UseBorders = false;
            this.xrBranchOffice.StylePriority.UseFont = false;
            this.xrBranchOffice.StylePriority.UsePadding = false;
            this.xrBranchOffice.Weight = 3D;
            // 
            // ReportFooterGrand
            // 
            this.ReportFooterGrand.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.ReportFooterGrand.HeightF = 25F;
            this.ReportFooterGrand.Name = "ReportFooterGrand";
            // 
            // xrTable5
            // 
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0.9996335F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable5.SizeF = new System.Drawing.SizeF(724.001F, 25F);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblGrandTotal,
            this.xrTableCell2,
            this.xrTableCell7});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrtblGrandTotal
            // 
            this.xrtblGrandTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblGrandTotal.BorderColor = System.Drawing.Color.DarkGray;
            this.xrtblGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblGrandTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            this.xrtblGrandTotal.Text = "Grand Total";
            this.xrtblGrandTotal.Weight = 1.8202072432367369D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.AMOUNT")});
            this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell2.StylePriority.UseBackColor = false;
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell2.Summary = xrSummary1;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 0.37871908953292538D;
            this.xrTableCell2.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrTableCell2_SummaryGetResult);
            this.xrTableCell2.SummaryReset += new System.EventHandler(this.xrTableCell2_SummaryReset);
            this.xrTableCell2.SummaryRowChanged += new System.EventHandler(this.xrTableCell2_SummaryRowChanged);
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell7.StylePriority.UseBackColor = false;
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.8093864477055237D;
            // 
            // GFTotal
            // 
            this.GFTotal.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GFTotal.HeightF = 30.20833F;
            this.GFTotal.Name = "GFTotal";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(725.0005F, 25F);
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblGroupFooterTotal,
            this.xrTableCell4,
            this.xrTableCell6});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrtblGroupFooterTotal
            // 
            this.xrtblGroupFooterTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblGroupFooterTotal.BorderColor = System.Drawing.Color.DarkGray;
            this.xrtblGroupFooterTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblGroupFooterTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblGroupFooterTotal.Name = "xrtblGroupFooterTotal";
            this.xrtblGroupFooterTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGroupFooterTotal.StylePriority.UseBackColor = false;
            this.xrtblGroupFooterTotal.StylePriority.UseBorderColor = false;
            this.xrtblGroupFooterTotal.StylePriority.UseBorders = false;
            this.xrtblGroupFooterTotal.StylePriority.UseFont = false;
            this.xrtblGroupFooterTotal.StylePriority.UsePadding = false;
            this.xrtblGroupFooterTotal.Text = "Total";
            this.xrtblGroupFooterTotal.Weight = 1.8168113521609963D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchReports.AMOUNT")});
            this.xrTableCell4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell4.StylePriority.UseBackColor = false;
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell4.Summary = xrSummary2;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 0.37715217182302563D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell6.StylePriority.UseBackColor = false;
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.806036476015978D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("BRANCH_ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // HOOfficeLedgersTransGreater10000
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GHBranch,
            this.ReportFooterGrand,
            this.GFTotal,
            this.GroupHeader1});
            this.DataMember = "BranchReports";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.GFTotal, 0);
            this.Controls.SetChildIndex(this.ReportFooterGrand, 0);
            this.Controls.SetChildIndex(this.GHBranch, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedger;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GHBranch;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrVouceDate;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrBranchOffice;
        private DevExpress.XtraReports.UI.XRTableCell xrtblAmount;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooterGrand;
        private DevExpress.XtraReports.UI.GroupFooterBand GFTotal;
        private DevExpress.XtraReports.UI.XRTable xrTable4;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrtblGroupFooterTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTableCell xrtblVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrNarrations;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    }
}

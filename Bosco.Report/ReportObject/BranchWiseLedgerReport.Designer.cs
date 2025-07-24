namespace Bosco.Report.ReportObject
{
    partial class BranchWiseLedgerReport
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
            this.GrpHeaderReceipts = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.xrpgfReceiptMonth = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpgfReceiptBranch = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpgfReceiptLedger = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpfReceiptAmount = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblBranchDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellBranchInfo = new DevExpress.XtraReports.UI.XRTableCell();
            this.GrpHeaderPayments = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.xrpgfPaymentMonth = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpgfPaymentBranch = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpgfPaymentLedger = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrpfPaymentAmount = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblBranchDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // GrpHeaderReceipts
            // 
            this.GrpHeaderReceipts.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrPivotGrid1});
            this.GrpHeaderReceipts.HeightF = 93.12503F;
            this.GrpHeaderReceipts.Level = 1;
            this.GrpHeaderReceipts.Name = "GrpHeaderReceipts";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.999705F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(83.33334F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Receipt";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrPivotGrid1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.TotalCell.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.TotalCell.Trimming = System.Drawing.StringTrimming.Word;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.xrpgfReceiptMonth,
            this.xrpgfReceiptBranch,
            this.xrpgfReceiptLedger,
            this.xrpfReceiptAmount});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(1.001016F, 25F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1085.999F, 68.12503F);
            this.xrPivotGrid1.CustomFieldValueCells += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs>(this.xrPivotGrid1_CustomFieldValueCells);
            this.xrPivotGrid1.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPivotGrid1_CustomFieldSort);
            this.xrPivotGrid1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPivotGrid1_BeforePrint);
            // 
            // xrpgfReceiptMonth
            // 
            this.xrpgfReceiptMonth.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptMonth.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfReceiptMonth.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptMonth.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfReceiptMonth.Appearance.FieldHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrpgfReceiptMonth.Appearance.FieldHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.xrpgfReceiptMonth.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptMonth.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfReceiptMonth.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptMonth.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptMonth.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptMonth.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrpgfReceiptMonth.AreaIndex = 0;
            this.xrpgfReceiptMonth.Caption = "Month Name";
            this.xrpgfReceiptMonth.FieldName = "MONTH_NAME";
            this.xrpgfReceiptMonth.Name = "xrpgfReceiptMonth";
            this.xrpgfReceiptMonth.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.xrpgfReceiptMonth.UnboundFieldName = "xrpgfMonth";
            this.xrpgfReceiptMonth.Visible = false;
            this.xrpgfReceiptMonth.Width = 70;
            // 
            // xrpgfReceiptBranch
            // 
            this.xrpgfReceiptBranch.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptBranch.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfReceiptBranch.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptBranch.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfReceiptBranch.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrpgfReceiptBranch.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptBranch.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfReceiptBranch.Appearance.FieldHeader.WordWrap = true;
            this.xrpgfReceiptBranch.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 6.5F);
            this.xrpgfReceiptBranch.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptBranch.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptBranch.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptBranch.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrpgfReceiptBranch.AreaIndex = 0;
            this.xrpgfReceiptBranch.Caption = "Branch Code";
            this.xrpgfReceiptBranch.FieldName = "BRANCH_OFFICE_NAME";
            this.xrpgfReceiptBranch.Name = "xrpgfReceiptBranch";
            this.xrpgfReceiptBranch.Width = 80;
            // 
            // xrpgfReceiptLedger
            // 
            this.xrpgfReceiptLedger.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrpgfReceiptLedger.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfReceiptLedger.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfReceiptLedger.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfReceiptLedger.Appearance.TotalCell.Trimming = System.Drawing.StringTrimming.Word;
            this.xrpgfReceiptLedger.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrpgfReceiptLedger.AreaIndex = 0;
            this.xrpgfReceiptLedger.Caption = "Ledger Name";
            this.xrpgfReceiptLedger.FieldName = "LEDGER_NAME";
            this.xrpgfReceiptLedger.Name = "xrpgfReceiptLedger";
            this.xrpgfReceiptLedger.Width = 150;
            // 
            // xrpfReceiptAmount
            // 
            this.xrpfReceiptAmount.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrpfReceiptAmount.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpfReceiptAmount.Appearance.FieldHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrpfReceiptAmount.Appearance.FieldHeader.BorderColor = System.Drawing.Color.DimGray;
            this.xrpfReceiptAmount.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfReceiptAmount.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfReceiptAmount.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrpfReceiptAmount.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfReceiptAmount.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpfReceiptAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrpfReceiptAmount.AreaIndex = 0;
            this.xrpfReceiptAmount.CellFormat.FormatString = "{0:n}";
            this.xrpfReceiptAmount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrpfReceiptAmount.FieldName = "AMOUNT";
            this.xrpfReceiptAmount.Name = "xrpfReceiptAmount";
            this.xrpfReceiptAmount.ValueFormat.FormatString = "{0:n}";
            this.xrpfReceiptAmount.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrpfReceiptAmount.Width = 50;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblBranchDetails});
            this.GroupHeader2.HeightF = 20F;
            this.GroupHeader2.Level = 2;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrTblBranchDetails
            // 
            this.xrTblBranchDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTblBranchDetails.Name = "xrTblBranchDetails";
            this.xrTblBranchDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblBranchDetails.SizeF = new System.Drawing.SizeF(1087F, 20F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellBranchInfo});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.8D;
            // 
            // xrcellBranchInfo
            // 
            this.xrcellBranchInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcellBranchInfo.Multiline = true;
            this.xrcellBranchInfo.Name = "xrcellBranchInfo";
            this.xrcellBranchInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellBranchInfo.StylePriority.UseFont = false;
            this.xrcellBranchInfo.StylePriority.UsePadding = false;
            this.xrcellBranchInfo.StylePriority.UseTextAlignment = false;
            this.xrcellBranchInfo.Text = "xrcellBranchInfo";
            this.xrcellBranchInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrcellBranchInfo.Weight = 3D;
            // 
            // GrpHeaderPayments
            // 
            this.GrpHeaderPayments.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrPivotGrid2});
            this.GrpHeaderPayments.HeightF = 93.95836F;
            this.GrpHeaderPayments.Name = "GrpHeaderPayments";
            this.GrpHeaderPayments.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            this.GrpHeaderPayments.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GrpHeaderPayments_BeforePrint);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.4998207F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(95.45856F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Payment";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid2.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrPivotGrid2.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid2.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid2.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid2.Appearance.TotalCell.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGrid2.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.TotalCell.Trimming = System.Drawing.StringTrimming.Word;
            this.xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.xrpgfPaymentMonth,
            this.xrpgfPaymentBranch,
            this.xrpgfPaymentLedger,
            this.xrpfPaymentAmount});
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.83333F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(1088F, 68.12503F);
            this.xrPivotGrid2.CustomFieldValueCells += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs>(this.xrPivotGrid2_CustomFieldValueCells);
            this.xrPivotGrid2.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPivotGrid2_CustomFieldSort);
            // 
            // xrpgfPaymentMonth
            // 
            this.xrpgfPaymentMonth.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentMonth.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfPaymentMonth.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentMonth.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfPaymentMonth.Appearance.FieldHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrpgfPaymentMonth.Appearance.FieldHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.xrpgfPaymentMonth.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentMonth.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfPaymentMonth.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentMonth.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentMonth.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentMonth.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrpgfPaymentMonth.AreaIndex = 0;
            this.xrpgfPaymentMonth.Caption = "Month Name";
            this.xrpgfPaymentMonth.FieldName = "MONTH_NAME";
            this.xrpgfPaymentMonth.Name = "xrpgfPaymentMonth";
            this.xrpgfPaymentMonth.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.xrpgfPaymentMonth.UnboundFieldName = "xrpgfMonth";
            this.xrpgfPaymentMonth.Visible = false;
            this.xrpgfPaymentMonth.Width = 70;
            // 
            // xrpgfPaymentBranch
            // 
            this.xrpgfPaymentBranch.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentBranch.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrpgfPaymentBranch.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentBranch.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfPaymentBranch.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrpgfPaymentBranch.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentBranch.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfPaymentBranch.Appearance.FieldHeader.WordWrap = true;
            this.xrpgfPaymentBranch.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 6.5F);
            this.xrpgfPaymentBranch.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentBranch.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentBranch.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentBranch.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrpgfPaymentBranch.AreaIndex = 0;
            this.xrpgfPaymentBranch.Caption = "Branch Code";
            this.xrpgfPaymentBranch.FieldName = "BRANCH_OFFICE_NAME";
            this.xrpgfPaymentBranch.Name = "xrpgfPaymentBranch";
            this.xrpgfPaymentBranch.Width = 80;
            // 
            // xrpgfPaymentLedger
            // 
            this.xrpgfPaymentLedger.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrpgfPaymentLedger.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpgfPaymentLedger.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrpgfPaymentLedger.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpgfPaymentLedger.Appearance.TotalCell.Trimming = System.Drawing.StringTrimming.Word;
            this.xrpgfPaymentLedger.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrpgfPaymentLedger.AreaIndex = 0;
            this.xrpgfPaymentLedger.Caption = "Ledger Name";
            this.xrpgfPaymentLedger.FieldName = "LEDGER_NAME";
            this.xrpgfPaymentLedger.Name = "xrpgfPaymentLedger";
            this.xrpgfPaymentLedger.Width = 150;
            // 
            // xrpfPaymentAmount
            // 
            this.xrpfPaymentAmount.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrpfPaymentAmount.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpfPaymentAmount.Appearance.FieldHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrpfPaymentAmount.Appearance.FieldHeader.BorderColor = System.Drawing.Color.DimGray;
            this.xrpfPaymentAmount.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfPaymentAmount.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfPaymentAmount.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.xrpfPaymentAmount.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrpfPaymentAmount.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrpfPaymentAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrpfPaymentAmount.AreaIndex = 0;
            this.xrpfPaymentAmount.CellFormat.FormatString = "{0:n}";
            this.xrpfPaymentAmount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrpfPaymentAmount.FieldName = "AMOUNT";
            this.xrpfPaymentAmount.Name = "xrpfPaymentAmount";
            this.xrpfPaymentAmount.ValueFormat.FormatString = "{0:n}";
            this.xrpfPaymentAmount.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrpfPaymentAmount.Width = 50;
            // 
            // BranchWiseLedgerReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GrpHeaderReceipts,
            this.GroupHeader2,
            this.GrpHeaderPayments});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(18, 53, 61, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GrpHeaderPayments, 0);
            this.Controls.SetChildIndex(this.GroupHeader2, 0);
            this.Controls.SetChildIndex(this.GrpHeaderReceipts, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblBranchDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GrpHeaderReceipts;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfReceiptMonth;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfReceiptBranch;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfReceiptLedger;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpfReceiptAmount;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRTable xrTblBranchDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrcellBranchInfo;
        private DevExpress.XtraReports.UI.GroupHeaderBand GrpHeaderPayments;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfPaymentMonth;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfPaymentBranch;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpgfPaymentLedger;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrpfPaymentAmount;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
    }
}

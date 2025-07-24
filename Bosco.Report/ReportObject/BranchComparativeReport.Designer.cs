namespace Bosco.Report.ReportObject
{
    partial class BranchComparativeReport
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
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrPGBranchComaparative = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldBRANCHOFFICENAME1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBRANCHOFFICENAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldRECEIPT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldPAYMENT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.pivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrMonthName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = true;
            this.Detail.HeightF = 32.29167F;
            this.Detail.Visible = true;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrPGBranchComaparative
            // 
            this.xrPGBranchComaparative.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.Transparent;
            this.xrPGBranchComaparative.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPGBranchComaparative.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPGBranchComaparative.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.xrPGBranchComaparative.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.FieldValueTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPGBranchComaparative.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPGBranchComaparative.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBranchComaparative.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBranchComaparative.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldBRANCHOFFICENAME1,
            this.fieldBRANCHOFFICENAME,
            this.fieldRECEIPT,
            this.fieldPAYMENT,
            this.pivotGridField1,
            this.xrMonthName});
            this.xrPGBranchComaparative.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrPGBranchComaparative.Name = "xrPGBranchComaparative";
            this.xrPGBranchComaparative.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGBranchComaparative.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGBranchComaparative.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.xrPGBranchComaparative.OptionsView.ShowColumnHeaders = false;
            this.xrPGBranchComaparative.OptionsView.ShowColumnTotals = false;
            this.xrPGBranchComaparative.OptionsView.ShowDataHeaders = false;
            this.xrPGBranchComaparative.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPGBranchComaparative.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPGBranchComaparative.SizeF = new System.Drawing.SizeF(1059.807F, 77.76321F);
            this.xrPGBranchComaparative.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGBranchComaparative_CustomFieldSort);
            // 
            // fieldBRANCHOFFICENAME1
            // 
            this.fieldBRANCHOFFICENAME1.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.fieldBRANCHOFFICENAME1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldBRANCHOFFICENAME1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME1.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldBRANCHOFFICENAME1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldBRANCHOFFICENAME1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME1.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBRANCHOFFICENAME1.AreaIndex = 0;
            this.fieldBRANCHOFFICENAME1.Caption = "Branch";
            this.fieldBRANCHOFFICENAME1.FieldName = "BRANCH_OFFICE_NAME";
            this.fieldBRANCHOFFICENAME1.Name = "fieldBRANCHOFFICENAME1";
            this.fieldBRANCHOFFICENAME1.Width = 130;
            // 
            // fieldBRANCHOFFICENAME
            // 
            this.fieldBRANCHOFFICENAME.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldBRANCHOFFICENAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldBRANCHOFFICENAME.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBRANCHOFFICENAME.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldBRANCHOFFICENAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBRANCHOFFICENAME.AreaIndex = 0;
            this.fieldBRANCHOFFICENAME.Caption = "Branch Name";
            this.fieldBRANCHOFFICENAME.FieldName = "BRANCH_OFFICE_NAME";
            this.fieldBRANCHOFFICENAME.Name = "fieldBRANCHOFFICENAME";
            this.fieldBRANCHOFFICENAME.Visible = false;
            this.fieldBRANCHOFFICENAME.Width = 120;
            // 
            // fieldRECEIPT
            // 
            this.fieldRECEIPT.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.fieldRECEIPT.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldRECEIPT.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldRECEIPT.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.fieldRECEIPT.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldRECEIPT.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldRECEIPT.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldRECEIPT.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldRECEIPT.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldRECEIPT.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldRECEIPT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldRECEIPT.AreaIndex = 0;
            this.fieldRECEIPT.Caption = "Income";
            this.fieldRECEIPT.CellFormat.FormatString = "{0:n}";
            this.fieldRECEIPT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldRECEIPT.FieldName = "RECEIPT";
            this.fieldRECEIPT.Name = "fieldRECEIPT";
            this.fieldRECEIPT.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.fieldRECEIPT.Width = 60;
            // 
            // fieldPAYMENT
            // 
            this.fieldPAYMENT.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.fieldPAYMENT.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldPAYMENT.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldPAYMENT.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.fieldPAYMENT.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldPAYMENT.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldPAYMENT.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldPAYMENT.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldPAYMENT.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldPAYMENT.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldPAYMENT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldPAYMENT.AreaIndex = 1;
            this.fieldPAYMENT.Caption = "Expense";
            this.fieldPAYMENT.CellFormat.FormatString = "{0:n}";
            this.fieldPAYMENT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPAYMENT.FieldName = "PAYMENT";
            this.fieldPAYMENT.Name = "fieldPAYMENT";
            this.fieldPAYMENT.ValueFormat.FormatString = "{0:n}";
            this.fieldPAYMENT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPAYMENT.Width = 60;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.pivotGridField1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.pivotGridField1.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.pivotGridField1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.pivotGridField1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.TotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField1.AreaIndex = 2;
            this.pivotGridField1.Caption = "Variance";
            this.pivotGridField1.CellFormat.FormatString = "{0:n}";
            this.pivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField1.FieldName = "AMOUNT";
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.ValueFormat.FormatString = "{0:n}";
            this.pivotGridField1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField1.Width = 70;
            // 
            // xrMonthName
            // 
            this.xrMonthName.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrMonthName.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrMonthName.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrMonthName.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrMonthName.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrMonthName.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrMonthName.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrMonthName.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrMonthName.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrMonthName.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrMonthName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrMonthName.AreaIndex = 0;
            this.xrMonthName.Caption = "Month Name";
            this.xrMonthName.FieldName = "MONTH_NAME";
            this.xrMonthName.Name = "xrMonthName";
            this.xrMonthName.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.xrMonthName.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.xrMonthName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.xrMonthName.UnboundFieldName = "pivotGridField2";
            this.xrMonthName.Width = 80;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGBranchComaparative});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 77.76321F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // BranchComparativeReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGBranchComaparative;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBRANCHOFFICENAME1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBRANCHOFFICENAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldRECEIPT;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldPAYMENT;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrMonthName;
    }
}

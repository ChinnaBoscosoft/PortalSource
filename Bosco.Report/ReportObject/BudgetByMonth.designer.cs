namespace Bosco.Report.ReportObject
{
    partial class BudgetByMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetByMonth));
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrPGBudgetByMonth = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.grpOpeningBalance = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPGMonthOpeningBalance = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldCBLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCBLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCBMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCBAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGBudgetByMonth});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrPGBudgetByMonth
            // 
            this.xrPGBudgetByMonth.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPGBudgetByMonth.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPGBudgetByMonth.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGBudgetByMonth.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPGBudgetByMonth.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGBudgetByMonth.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGBudgetByMonth.CellStyleName = "styleRowSmall";
            this.xrPGBudgetByMonth.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGBudgetByMonth.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldLEDGERCODE,
            this.fieldLEDGERNAME,
            this.fieldMONTHNAME,
            this.fieldAMOUNT});
            this.xrPGBudgetByMonth.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGBudgetByMonth.FieldValueStyleName = "styleRowSmall";
            this.xrPGBudgetByMonth.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGBudgetByMonth.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGBudgetByMonth.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrPGBudgetByMonth, "xrPGBudgetByMonth");
            this.xrPGBudgetByMonth.Name = "xrPGBudgetByMonth";
            this.xrPGBudgetByMonth.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGBudgetByMonth.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGBudgetByMonth.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPGBudgetByMonth.OptionsView.ShowColumnGrandTotals = false;
            this.xrPGBudgetByMonth.OptionsView.ShowColumnHeaders = false;
            this.xrPGBudgetByMonth.OptionsView.ShowColumnTotals = false;
            this.xrPGBudgetByMonth.OptionsView.ShowDataHeaders = false;
            this.xrPGBudgetByMonth.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPGBudgetByMonth.OptionsView.ShowRowTotals = false;
            this.xrPGBudgetByMonth.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGBudgetByMonth_CustomFieldSort);
            this.xrPGBudgetByMonth.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGBudgetByMonth_PrintFieldValue);
            this.xrPGBudgetByMonth.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGBudgetByMonth_PrintCell);
            // 
            // fieldLEDGERCODE
            // 
            this.fieldLEDGERCODE.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERCODE.AreaIndex = 0;
            resources.ApplyResources(this.fieldLEDGERCODE, "fieldLEDGERCODE");
            this.fieldLEDGERCODE.Name = "fieldLEDGERCODE";
            // 
            // fieldLEDGERNAME
            // 
            this.fieldLEDGERNAME.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERNAME.AreaIndex = 1;
            resources.ApplyResources(this.fieldLEDGERNAME, "fieldLEDGERNAME");
            this.fieldLEDGERNAME.Name = "fieldLEDGERNAME";
            // 
            // fieldMONTHNAME
            // 
            this.fieldMONTHNAME.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldMONTHNAME.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldMONTHNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldMONTHNAME.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldMONTHNAME.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldMONTHNAME.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldMONTHNAME.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldMONTHNAME.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldMONTHNAME.AreaIndex = 0;
            resources.ApplyResources(this.fieldMONTHNAME, "fieldMONTHNAME");
            this.fieldMONTHNAME.Name = "fieldMONTHNAME";
            this.fieldMONTHNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldAMOUNT
            // 
            this.fieldAMOUNT.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAMOUNT.AreaIndex = 0;
            this.fieldAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            resources.ApplyResources(this.fieldAMOUNT, "fieldAMOUNT");
            this.fieldAMOUNT.Name = "fieldAMOUNT";
            this.fieldAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // styleRowSmall
            // 
            this.styleRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRowSmall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRowSmall.Name = "styleRowSmall";
            this.styleRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleColumnHeaderSmall
            // 
            this.styleColumnHeaderSmall.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeaderSmall.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeaderSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeaderSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeaderSmall.Name = "styleColumnHeaderSmall";
            this.styleColumnHeaderSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRowSmall
            // 
            this.styleTotalRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleTotalRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRowSmall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRowSmall.Name = "styleTotalRowSmall";
            this.styleTotalRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // styleGroupRowSmall
            // 
            this.styleGroupRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRowSmall.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRowSmall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRowSmall.ForeColor = System.Drawing.Color.IndianRed;
            this.styleGroupRowSmall.Name = "styleGroupRowSmall";
            this.styleGroupRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle3
            // 
            this.xrControlStyle3.Name = "xrControlStyle3";
            this.xrControlStyle3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // grpOpeningBalance
            // 
            this.grpOpeningBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGMonthOpeningBalance});
            resources.ApplyResources(this.grpOpeningBalance, "grpOpeningBalance");
            this.grpOpeningBalance.Name = "grpOpeningBalance";
            // 
            // xrPGMonthOpeningBalance
            // 
            this.xrPGMonthOpeningBalance.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPGMonthOpeningBalance.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPGMonthOpeningBalance.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGMonthOpeningBalance.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPGMonthOpeningBalance.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMonthOpeningBalance.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGMonthOpeningBalance.CellStyleName = "styleRowSmall";
            this.xrPGMonthOpeningBalance.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGMonthOpeningBalance.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldCBLEDGERCODE,
            this.fieldCBLEDGERNAME,
            this.fieldCBMONTHNAME,
            this.fieldCBAMOUNT});
            this.xrPGMonthOpeningBalance.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGMonthOpeningBalance.FieldValueStyleName = "styleRowSmall";
            this.xrPGMonthOpeningBalance.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGMonthOpeningBalance.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGMonthOpeningBalance.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrPGMonthOpeningBalance, "xrPGMonthOpeningBalance");
            this.xrPGMonthOpeningBalance.Name = "xrPGMonthOpeningBalance";
            this.xrPGMonthOpeningBalance.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGMonthOpeningBalance.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGMonthOpeningBalance.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowColumnGrandTotals = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowColumnHeaders = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowColumnTotals = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowDataHeaders = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPGMonthOpeningBalance.OptionsView.ShowRowHeaders = false;
            this.xrPGMonthOpeningBalance.OptionsView.ShowRowTotals = false;
            this.xrPGMonthOpeningBalance.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGMonthOpeningBalance_CustomFieldSort);
            this.xrPGMonthOpeningBalance.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGMonthOpeningBalance_PrintFieldValue);
            this.xrPGMonthOpeningBalance.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGMonthOpeningBalance_PrintCell);
            // 
            // fieldCBLEDGERCODE
            // 
            this.fieldCBLEDGERCODE.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCBLEDGERCODE.AreaIndex = 0;
            resources.ApplyResources(this.fieldCBLEDGERCODE, "fieldCBLEDGERCODE");
            this.fieldCBLEDGERCODE.Name = "fieldCBLEDGERCODE";
            // 
            // fieldCBLEDGERNAME
            // 
            this.fieldCBLEDGERNAME.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCBLEDGERNAME.AreaIndex = 1;
            resources.ApplyResources(this.fieldCBLEDGERNAME, "fieldCBLEDGERNAME");
            this.fieldCBLEDGERNAME.Name = "fieldCBLEDGERNAME";
            // 
            // fieldCBMONTHNAME
            // 
            this.fieldCBMONTHNAME.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldCBMONTHNAME.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBMONTHNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldCBMONTHNAME.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBMONTHNAME.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBMONTHNAME.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.fieldCBMONTHNAME.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBMONTHNAME.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldCBMONTHNAME.AreaIndex = 0;
            resources.ApplyResources(this.fieldCBMONTHNAME, "fieldCBMONTHNAME");
            this.fieldCBMONTHNAME.Name = "fieldCBMONTHNAME";
            this.fieldCBMONTHNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldCBAMOUNT
            // 
            this.fieldCBAMOUNT.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldCBAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldCBAMOUNT.AreaIndex = 0;
            this.fieldCBAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldCBAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            resources.ApplyResources(this.fieldCBAMOUNT, "fieldCBAMOUNT");
            this.fieldCBAMOUNT.Name = "fieldCBAMOUNT";
            this.fieldCBAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldCBAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // BudgetByMonth
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpOpeningBalance});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleRowSmall,
            this.styleColumnHeaderSmall,
            this.styleTotalRowSmall,
            this.xrControlStyle1,
            this.xrControlStyle2,
            this.styleGroupRowSmall,
            this.xrControlStyle3});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpOpeningBalance, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGBudgetByMonth;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAMOUNT;
        private DevExpress.XtraReports.UI.XRControlStyle styleRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeaderSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle3;
        private DevExpress.XtraReports.UI.GroupFooterBand grpOpeningBalance;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGMonthOpeningBalance;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCBLEDGERCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCBLEDGERNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCBMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCBAMOUNT;
    }
}

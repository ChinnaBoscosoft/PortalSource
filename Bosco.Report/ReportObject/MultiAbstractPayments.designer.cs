namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiAbstractPayments));
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrPGMultiAbstractPayment = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldGROUPCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERGROUP = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrLabelClBal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubBalanceMulti = new DevExpress.XtraReports.UI.XRSubreport();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPGGrandTotal = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldGRANTTOTALPARTICULARS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldGRANTTOTALMONTH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldGRANTTOTALAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGMultiAbstractPayment});
            this.Detail.Expanded = false;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("GROUP_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("LEDGER_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrPGMultiAbstractPayment
            // 
            this.xrPGMultiAbstractPayment.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.Appearance.Cell.WordWrap = true;
            this.xrPGMultiAbstractPayment.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPGMultiAbstractPayment.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGMultiAbstractPayment.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.Appearance.FieldValue.WordWrap = true;
            this.xrPGMultiAbstractPayment.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGMultiAbstractPayment.CellStyleName = "styleRowSmall";
            this.xrPGMultiAbstractPayment.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGMultiAbstractPayment.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldGROUPCODE,
            this.fieldLEDGERGROUP,
            this.fieldLEDGERCODE,
            this.fieldLEDGERNAME,
            this.fieldMONTHNAME,
            this.fieldAMOUNT});
            this.xrPGMultiAbstractPayment.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGMultiAbstractPayment.FieldValueStyleName = "styleRowSmall";
            this.xrPGMultiAbstractPayment.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGMultiAbstractPayment.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGMultiAbstractPayment.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrPGMultiAbstractPayment, "xrPGMultiAbstractPayment");
            this.xrPGMultiAbstractPayment.Name = "xrPGMultiAbstractPayment";
            this.xrPGMultiAbstractPayment.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGMultiAbstractPayment.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGMultiAbstractPayment.OptionsView.ShowColumnHeaders = false;
            this.xrPGMultiAbstractPayment.OptionsView.ShowDataHeaders = false;
            this.xrPGMultiAbstractPayment.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPGMultiAbstractPayment.OptionsView.ShowRowTotals = false;
            this.xrPGMultiAbstractPayment.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGMultiAbstractPayment_CustomFieldSort);
            this.xrPGMultiAbstractPayment.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPGMultiAbstractPayment_FieldValueDisplayText);
            this.xrPGMultiAbstractPayment.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGMultiAbstractPayment_PrintFieldValue);
            this.xrPGMultiAbstractPayment.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGMultiAbstractPayment_PrintCell);
            this.xrPGMultiAbstractPayment.AfterPrint += new System.EventHandler(this.xrPGMultiAbstractPayment_AfterPrint);
            // 
            // fieldGROUPCODE
            // 
            this.fieldGROUPCODE.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldGROUPCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldGROUPCODE.AreaIndex = 0;
            resources.ApplyResources(this.fieldGROUPCODE, "fieldGROUPCODE");
            this.fieldGROUPCODE.Name = "fieldGROUPCODE";
            // 
            // fieldLEDGERGROUP
            // 
            this.fieldLEDGERGROUP.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldLEDGERGROUP.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERGROUP.AreaIndex = 1;
            resources.ApplyResources(this.fieldLEDGERGROUP, "fieldLEDGERGROUP");
            this.fieldLEDGERGROUP.Name = "fieldLEDGERGROUP";
            // 
            // fieldLEDGERCODE
            // 
            this.fieldLEDGERCODE.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERCODE.AreaIndex = 2;
            resources.ApplyResources(this.fieldLEDGERCODE, "fieldLEDGERCODE");
            this.fieldLEDGERCODE.Name = "fieldLEDGERCODE";
            // 
            // fieldLEDGERNAME
            // 
            this.fieldLEDGERNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERNAME.AreaIndex = 3;
            resources.ApplyResources(this.fieldLEDGERNAME, "fieldLEDGERNAME");
            this.fieldLEDGERNAME.Name = "fieldLEDGERNAME";
            // 
            // fieldMONTHNAME
            // 
            this.fieldMONTHNAME.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldMONTHNAME.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldMONTHNAME.AreaIndex = 0;
            resources.ApplyResources(this.fieldMONTHNAME, "fieldMONTHNAME");
            this.fieldMONTHNAME.Name = "fieldMONTHNAME";
            this.fieldMONTHNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldAMOUNT
            // 
            this.fieldAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAMOUNT.AreaIndex = 0;
            this.fieldAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            resources.ApplyResources(this.fieldAMOUNT, "fieldAMOUNT");
            this.fieldAMOUNT.Name = "fieldAMOUNT";
            this.fieldAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // xrLabelClBal
            // 
            resources.ApplyResources(this.xrLabelClBal, "xrLabelClBal");
            this.xrLabelClBal.Name = "xrLabelClBal";
            this.xrLabelClBal.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabelClBal.StyleName = "styleTotalRowSmall";
            // 
            // xrSubBalanceMulti
            // 
            resources.ApplyResources(this.xrSubBalanceMulti, "xrSubBalanceMulti");
            this.xrSubBalanceMulti.Name = "xrSubBalanceMulti";
            this.xrSubBalanceMulti.ReportSource = new Bosco.Report.ReportObject.AccountBalanceMulti();
            // 
            // styleInstitute
            // 
            this.styleInstitute.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleInstitute.Name = "styleInstitute";
            this.styleInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportTitle
            // 
            this.styleReportTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportTitle.Name = "styleReportTitle";
            this.styleReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportSubTitle
            // 
            this.styleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportSubTitle.Name = "styleReportSubTitle";
            this.styleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleDateInfo
            // 
            this.styleDateInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleDateInfo.Name = "styleDateInfo";
            this.styleDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.styleDateInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // stylePageInfo
            // 
            this.stylePageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stylePageInfo.Name = "stylePageInfo";
            this.stylePageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.stylePageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.White;
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
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.Linen;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1F;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
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
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1F;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRowSmall
            // 
            this.styleGroupRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRowSmall.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRowSmall.ForeColor = System.Drawing.Color.IndianRed;
            this.styleGroupRowSmall.Name = "styleGroupRowSmall";
            // 
            // styleRowSmall
            // 
            this.styleRowSmall.BackColor = System.Drawing.Color.Empty;
            this.styleRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRowSmall.BorderWidth = 1F;
            this.styleRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRowSmall.Name = "styleRowSmall";
            this.styleRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRowSmall.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeaderSmall
            // 
            this.styleColumnHeaderSmall.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeaderSmall.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeaderSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeaderSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeaderSmall.BorderWidth = 1F;
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeaderSmall.Name = "styleColumnHeaderSmall";
            this.styleColumnHeaderSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRowSmall
            // 
            this.styleTotalRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleTotalRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRowSmall.Name = "styleTotalRowSmall";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBalanceMulti,
            this.xrLabelClBal});
            resources.ApplyResources(this.Detail1, "Detail1");
            this.Detail1.KeepTogetherWithDetailReports = true;
            this.Detail1.Name = "Detail1";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport1});
            this.DetailReport.Expanded = false;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
            this.DetailReport1.Level = 0;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGGrandTotal});
            resources.ApplyResources(this.Detail2, "Detail2");
            this.Detail2.Name = "Detail2";
            // 
            // xrPGGrandTotal
            // 
            this.xrPGGrandTotal.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPGGrandTotal.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGGrandTotal.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.xrPGGrandTotal.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPGGrandTotal.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrPGGrandTotal.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGGrandTotal.CellStyleName = "styleTotalRowSmall";
            this.xrPGGrandTotal.DataMember = "MultiAbstract";
            this.xrPGGrandTotal.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGGrandTotal.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldGRANTTOTALPARTICULARS,
            this.fieldGRANTTOTALMONTH,
            this.fieldGRANTTOTALAMOUNT});
            this.xrPGGrandTotal.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGGrandTotal.FieldValueStyleName = "styleTotalRowSmall";
            this.xrPGGrandTotal.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGGrandTotal.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGGrandTotal.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrPGGrandTotal, "xrPGGrandTotal");
            this.xrPGGrandTotal.Name = "xrPGGrandTotal";
            this.xrPGGrandTotal.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGGrandTotal.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGGrandTotal.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPGGrandTotal.OptionsView.ShowColumnGrandTotals = false;
            this.xrPGGrandTotal.OptionsView.ShowColumnHeaders = false;
            this.xrPGGrandTotal.OptionsView.ShowDataHeaders = false;
            this.xrPGGrandTotal.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPGGrandTotal.OptionsView.ShowRowGrandTotals = false;
            this.xrPGGrandTotal.PrintHeader += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs>(this.xrPGGrandTotal_PrintHeader);
            this.xrPGGrandTotal.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGGrandTotal_PrintFieldValue);
            // 
            // fieldGRANTTOTALPARTICULARS
            // 
            this.fieldGRANTTOTALPARTICULARS.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.fieldGRANTTOTALPARTICULARS.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.fieldGRANTTOTALPARTICULARS.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 6.5F, System.Drawing.FontStyle.Bold);
            this.fieldGRANTTOTALPARTICULARS.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.fieldGRANTTOTALPARTICULARS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldGRANTTOTALPARTICULARS.AreaIndex = 0;
            resources.ApplyResources(this.fieldGRANTTOTALPARTICULARS, "fieldGRANTTOTALPARTICULARS");
            this.fieldGRANTTOTALPARTICULARS.Name = "fieldGRANTTOTALPARTICULARS";
            // 
            // fieldGRANTTOTALMONTH
            // 
            this.fieldGRANTTOTALMONTH.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldGRANTTOTALMONTH.AreaIndex = 0;
            resources.ApplyResources(this.fieldGRANTTOTALMONTH, "fieldGRANTTOTALMONTH");
            this.fieldGRANTTOTALMONTH.Name = "fieldGRANTTOTALMONTH";
            // 
            // fieldGRANTTOTALAMOUNT
            // 
            this.fieldGRANTTOTALAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldGRANTTOTALAMOUNT.AreaIndex = 0;
            this.fieldGRANTTOTALAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldGRANTTOTALAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            resources.ApplyResources(this.fieldGRANTTOTALAMOUNT, "fieldGRANTTOTALAMOUNT");
            this.fieldGRANTTOTALAMOUNT.Name = "fieldGRANTTOTALAMOUNT";
            this.fieldGRANTTOTALAMOUNT.Options.HideEmptyVariationItems = true;
            this.fieldGRANTTOTALAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldGRANTTOTALAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // MultiAbstractPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.DetailReport});
            this.DataMember = "MultiAbstract";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleEvenRow,
            this.styleGroupRow,
            this.styleColumnHeader,
            this.styleTotalRow,
            this.styleGroupRowSmall,
            this.styleRowSmall,
            this.styleColumnHeaderSmall,
            this.styleTotalRowSmall});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGMultiAbstractPayment;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAMOUNT;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBalanceMulti;
        private DevExpress.XtraReports.UI.XRLabel xrLabelClBal;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGROUPCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERGROUP;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleInstitute;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportSubTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleDateInfo;
        private DevExpress.XtraReports.UI.XRControlStyle stylePageInfo;
        private DevExpress.XtraReports.UI.XRControlStyle styleEvenRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeader;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeaderSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRowSmall;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGGrandTotal;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALPARTICULARS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALMONTH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALAMOUNT;
    }
}

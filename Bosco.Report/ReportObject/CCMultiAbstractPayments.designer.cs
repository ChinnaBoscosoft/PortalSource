namespace Bosco.Report.ReportObject
{
    partial class CCMultiAbstractPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCMultiAbstractPayments));
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrCosPGMultiAbstractPayment = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldCosGROUPCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCosLEDGERGROUP = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCosLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCosLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
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
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosPGMultiAbstractPayment});
            resources.ApplyResources(this.Detail, "Detail");
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
            // xrCosPGMultiAbstractPayment
            // 
            this.xrCosPGMultiAbstractPayment.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.Appearance.Cell.WordWrap = true;
            this.xrCosPGMultiAbstractPayment.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldValue.WordWrap = true;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractPayment.CellStyleName = "styleRowSmall";
            this.xrCosPGMultiAbstractPayment.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrCosPGMultiAbstractPayment.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldCosGROUPCODE,
            this.fieldCosLEDGERGROUP,
            this.fieldCosLEDGERCODE,
            this.fieldCosLEDGERNAME,
            this.fieldMONTHNAME,
            this.fieldAMOUNT});
            this.xrCosPGMultiAbstractPayment.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractPayment.FieldValueStyleName = "styleRowSmall";
            this.xrCosPGMultiAbstractPayment.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractPayment.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractPayment.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrCosPGMultiAbstractPayment, "xrCosPGMultiAbstractPayment");
            this.xrCosPGMultiAbstractPayment.Name = "xrCosPGMultiAbstractPayment";
            this.xrCosPGMultiAbstractPayment.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrCosPGMultiAbstractPayment.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrCosPGMultiAbstractPayment.OptionsView.ShowColumnHeaders = false;
            this.xrCosPGMultiAbstractPayment.OptionsView.ShowDataHeaders = false;
            this.xrCosPGMultiAbstractPayment.OptionsView.ShowRowTotals = false;
            this.xrCosPGMultiAbstractPayment.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGMultiAbstractPayment_CustomFieldSort);
            this.xrCosPGMultiAbstractPayment.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPGMultiAbstractPayment_FieldValueDisplayText);
            this.xrCosPGMultiAbstractPayment.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGMultiAbstractPayment_PrintFieldValue);
            this.xrCosPGMultiAbstractPayment.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGMultiAbstractPayment_PrintCell);
            this.xrCosPGMultiAbstractPayment.AfterPrint += new System.EventHandler(this.xrPGMultiAbstractPayment_AfterPrint);
            // 
            // fieldCosGROUPCODE
            // 
            this.fieldCosGROUPCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCosGROUPCODE.AreaIndex = 0;
            resources.ApplyResources(this.fieldCosGROUPCODE, "fieldCosGROUPCODE");
            this.fieldCosGROUPCODE.Name = "fieldCosGROUPCODE";
            // 
            // fieldCosLEDGERGROUP
            // 
            this.fieldCosLEDGERGROUP.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCosLEDGERGROUP.AreaIndex = 1;
            resources.ApplyResources(this.fieldCosLEDGERGROUP, "fieldCosLEDGERGROUP");
            this.fieldCosLEDGERGROUP.Name = "fieldCosLEDGERGROUP";
            // 
            // fieldCosLEDGERCODE
            // 
            this.fieldCosLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCosLEDGERCODE.AreaIndex = 2;
            resources.ApplyResources(this.fieldCosLEDGERCODE, "fieldCosLEDGERCODE");
            this.fieldCosLEDGERCODE.Name = "fieldCosLEDGERCODE";
            // 
            // fieldCosLEDGERNAME
            // 
            this.fieldCosLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCosLEDGERNAME.AreaIndex = 3;
            resources.ApplyResources(this.fieldCosLEDGERNAME, "fieldCosLEDGERNAME");
            this.fieldCosLEDGERNAME.Name = "fieldCosLEDGERNAME";
            // 
            // fieldMONTHNAME
            // 
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
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // CCMultiAbstractPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
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
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRPivotGrid xrCosPGMultiAbstractPayment;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCosLEDGERCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCosLEDGERNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAMOUNT;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCosGROUPCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCosLEDGERGROUP;
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
    }
}

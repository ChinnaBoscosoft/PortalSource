namespace Bosco.Report.ReportObject
{
    partial class BranchDataSyncStatus
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
            this.xrPGDataStatus = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.reportSetting2 = new Bosco.Report.ReportSetting();
            this.fieldBRANCHCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBRANCHOFFICENAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldRESULT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldPROJECT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGDataStatus});
            this.Detail.HeightF = 52.08333F;
            this.Detail.Visible = true;
            // 
            // xrPGDataStatus
            // 
            this.xrPGDataStatus.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGDataStatus.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGDataStatus.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPGDataStatus.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGDataStatus.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGDataStatus.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrPGDataStatus.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGDataStatus.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrPGDataStatus.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPGDataStatus.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.Gainsboro;
            this.xrPGDataStatus.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.xrPGDataStatus.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Bold);
            this.xrPGDataStatus.CellStyleName = "styleRowSmall";
            this.xrPGDataStatus.DataMember = "BranchDataStatus";
            this.xrPGDataStatus.DataSource = this.reportSetting2;
            this.xrPGDataStatus.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGDataStatus.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldBRANCHCODE,
            this.fieldBRANCHOFFICENAME,
            this.fieldMONTHNAME,
            this.fieldRESULT,
            this.fieldPROJECT});
            this.xrPGDataStatus.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGDataStatus.FieldValueStyleName = "styleRowSmall";
            this.xrPGDataStatus.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGDataStatus.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGDataStatus.HeaderGroupLineStyleName = "styleGroupRowSmall";
            this.xrPGDataStatus.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPGDataStatus.Name = "xrPGDataStatus";
            this.xrPGDataStatus.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGDataStatus.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGDataStatus.OptionsView.ShowColumnHeaders = false;
            this.xrPGDataStatus.OptionsView.ShowDataHeaders = false;
            this.xrPGDataStatus.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPGDataStatus.SizeF = new System.Drawing.SizeF(1110F, 49.37499F);
            this.xrPGDataStatus.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGDataStatus_CustomFieldSort);
            this.xrPGDataStatus.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPGDataStatus_FieldValueDisplayText);
            this.xrPGDataStatus.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGDataStatus_PrintFieldValue);
            this.xrPGDataStatus.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGDataStatus_PrintCell);
            this.xrPGDataStatus.AfterPrint += new System.EventHandler(this.xrPGDataStatus_AfterPrint);
            // 
            // reportSetting2
            // 
            this.reportSetting2.DataSetName = "ReportSetting";
            this.reportSetting2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldBRANCHCODE
            // 
            this.fieldBRANCHCODE.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 3F);
            this.fieldBRANCHCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBRANCHCODE.AreaIndex = 0;
            this.fieldBRANCHCODE.Caption = "Code";
            this.fieldBRANCHCODE.FieldName = "BRANCH_OFFICE_CODE";
            this.fieldBRANCHCODE.Name = "fieldBRANCHCODE";
            this.fieldBRANCHCODE.Visible = false;
            // 
            // fieldBRANCHOFFICENAME
            // 
            this.fieldBRANCHOFFICENAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBRANCHOFFICENAME.AreaIndex = 0;
            this.fieldBRANCHOFFICENAME.Caption = "Branch Office Name";
            this.fieldBRANCHOFFICENAME.FieldName = "BRANCH_OFFICE_NAME";
            this.fieldBRANCHOFFICENAME.Name = "fieldBRANCHOFFICENAME";
            this.fieldBRANCHOFFICENAME.Width = 180;
            // 
            // fieldMONTHNAME
            // 
            this.fieldMONTHNAME.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldMONTHNAME.AreaIndex = 0;
            this.fieldMONTHNAME.FieldName = "MONTH_NAME";
            this.fieldMONTHNAME.Name = "fieldMONTHNAME";
            this.fieldMONTHNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.fieldMONTHNAME.Width = 50;
            // 
            // fieldRESULT
            // 
            this.fieldRESULT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldRESULT.AreaIndex = 0;
            this.fieldRESULT.FieldName = "RESULT";
            this.fieldRESULT.Name = "fieldRESULT";
            this.fieldRESULT.Width = 40;
            // 
            // fieldPROJECT
            // 
            this.fieldPROJECT.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldPROJECT.AreaIndex = 1;
            this.fieldPROJECT.Caption = "Project";
            this.fieldPROJECT.FieldName = "PROJECT";
            this.fieldPROJECT.Name = "fieldPROJECT";
            this.fieldPROJECT.Width = 150;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // styleRowSmall
            // 
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
            this.styleTotalRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleTotalRowSmall.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // styleGroupRowSmall
            // 
            this.styleGroupRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRowSmall.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRowSmall.Font = new System.Drawing.Font("Tahoma", 6.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRowSmall.ForeColor = System.Drawing.Color.IndianRed;
            this.styleGroupRowSmall.Name = "styleGroupRowSmall";
            // 
            // BranchDataSyncStatus
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
            this.DataMember = "BranchDataStatus";
            this.DataSource = this.reportSetting2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(49, 0, 61, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleRowSmall,
            this.styleColumnHeaderSmall,
            this.styleTotalRowSmall,
            this.styleGroupRowSmall});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRPivotGrid xrPGDataStatus;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBRANCHCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBRANCHOFFICENAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldRESULT;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldPROJECT;
        private ReportSetting reportSetting1;
        private ReportSetting reportSetting2;
        private DevExpress.XtraReports.UI.XRControlStyle styleRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeaderSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowSmall;
    }
}

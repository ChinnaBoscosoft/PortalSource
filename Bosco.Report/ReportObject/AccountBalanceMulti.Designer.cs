namespace Bosco.Report.ReportObject
{
    partial class AccountBalanceMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountBalanceMulti));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPGAccountBalanceMulti = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldLEDGERGROUP = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
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
            this.xrPGAccountBalanceMulti});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrPGAccountBalanceMulti
            // 
            this.xrPGAccountBalanceMulti.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPGAccountBalanceMulti.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPGAccountBalanceMulti.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 6.3F, System.Drawing.FontStyle.Bold);
            this.xrPGAccountBalanceMulti.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.Appearance.TotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPGAccountBalanceMulti.CellStyleName = "styleRowSmall";
            this.xrPGAccountBalanceMulti.DataMember = "AccountBalance";
            this.xrPGAccountBalanceMulti.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrPGAccountBalanceMulti.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldLEDGERGROUP,
            this.fieldLEDGERNAME,
            this.fieldMONTHNAME,
            this.fieldAMOUNT,
            this.fieldLEDGERCODE});
            this.xrPGAccountBalanceMulti.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrPGAccountBalanceMulti.FieldValueStyleName = "styleRowSmall";
            this.xrPGAccountBalanceMulti.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrPGAccountBalanceMulti.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrPGAccountBalanceMulti.HeaderGroupLineStyleName = "styleGroupRowSmall";
            resources.ApplyResources(this.xrPGAccountBalanceMulti, "xrPGAccountBalanceMulti");
            this.xrPGAccountBalanceMulti.Name = "xrPGAccountBalanceMulti";
            this.xrPGAccountBalanceMulti.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGAccountBalanceMulti.OptionsView.ShowColumnHeaders = false;
            this.xrPGAccountBalanceMulti.OptionsView.ShowDataHeaders = false;
            this.xrPGAccountBalanceMulti.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGAccountBalanceMulti_CustomFieldSort);
            this.xrPGAccountBalanceMulti.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPGAccountBalanceMulti_FieldValueDisplayText);
            this.xrPGAccountBalanceMulti.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGAccountBalanceMulti_PrintFieldValue);
            this.xrPGAccountBalanceMulti.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGAccountBalanceMulti_PrintCell);
            // 
            // fieldLEDGERGROUP
            // 
            this.fieldLEDGERGROUP.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERGROUP.AreaIndex = 0;
            resources.ApplyResources(this.fieldLEDGERGROUP, "fieldLEDGERGROUP");
            this.fieldLEDGERGROUP.Name = "fieldLEDGERGROUP";
            // 
            // fieldLEDGERNAME
            // 
            this.fieldLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERNAME.AreaIndex = 1;
            resources.ApplyResources(this.fieldLEDGERNAME, "fieldLEDGERNAME");
            this.fieldLEDGERNAME.Name = "fieldLEDGERNAME";
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
            // fieldLEDGERCODE
            // 
            this.fieldLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERCODE.AreaIndex = 1;
            resources.ApplyResources(this.fieldLEDGERCODE, "fieldLEDGERCODE");
            this.fieldLEDGERCODE.Name = "fieldLEDGERCODE";
            this.fieldLEDGERCODE.Visible = false;
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
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.styleTotalRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // AccountBalanceMulti
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataMember = "AccountBalance";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleGroupRowSmall,
            this.styleRowSmall,
            this.styleColumnHeaderSmall,
            this.styleTotalRowSmall});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGAccountBalanceMulti;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAMOUNT;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERGROUP;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERNAME;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeaderSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRowSmall;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERCODE;
    }
}

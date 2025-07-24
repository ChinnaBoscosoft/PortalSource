namespace Bosco.Report.ReportObject
{
    partial class BudgetView
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
            this.grpBudgetExpenseLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrExpenseTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubBudgetExpenseLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.GrpBudgetIncomeLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrIncomeTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubBudgetIncomeLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpBudgetStatistics = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubBudgetStatistics = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = true;
            this.Detail.HeightF = 30.29162F;
            // 
            // grpBudgetExpenseLedgers
            // 
            this.grpBudgetExpenseLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrExpenseTitle,
            this.xrSubBudgetExpenseLedgers});
            this.grpBudgetExpenseLedgers.HeightF = 75.41669F;
            this.grpBudgetExpenseLedgers.Name = "grpBudgetExpenseLedgers";
            // 
            // xrExpenseTitle
            // 
            this.xrExpenseTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrExpenseTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 1.999982F);
            this.xrExpenseTitle.Name = "xrExpenseTitle";
            this.xrExpenseTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrExpenseTitle.SizeF = new System.Drawing.SizeF(776F, 23F);
            this.xrExpenseTitle.StylePriority.UseFont = false;
            this.xrExpenseTitle.StylePriority.UseTextAlignment = false;
            this.xrExpenseTitle.Text = "EXPENDITURE";
            this.xrExpenseTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrSubBudgetExpenseLedgers
            // 
            this.xrSubBudgetExpenseLedgers.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 25F);
            this.xrSubBudgetExpenseLedgers.Name = "xrSubBudgetExpenseLedgers";
            this.xrSubBudgetExpenseLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedger();
            this.xrSubBudgetExpenseLedgers.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // GrpBudgetIncomeLedgers
            // 
            this.GrpBudgetIncomeLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrIncomeTitle,
            this.xrSubBudgetIncomeLedgers});
            this.GrpBudgetIncomeLedgers.HeightF = 75.41669F;
            this.GrpBudgetIncomeLedgers.Level = 1;
            this.GrpBudgetIncomeLedgers.Name = "GrpBudgetIncomeLedgers";
            // 
            // xrIncomeTitle
            // 
            this.xrIncomeTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrIncomeTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrIncomeTitle.Name = "xrIncomeTitle";
            this.xrIncomeTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrIncomeTitle.SizeF = new System.Drawing.SizeF(775.9999F, 23F);
            this.xrIncomeTitle.StylePriority.UseFont = false;
            this.xrIncomeTitle.StylePriority.UseTextAlignment = false;
            this.xrIncomeTitle.Text = "INCOME";
            this.xrIncomeTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrSubBudgetIncomeLedgers
            // 
            this.xrSubBudgetIncomeLedgers.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 25.99999F);
            this.xrSubBudgetIncomeLedgers.Name = "xrSubBudgetIncomeLedgers";
            this.xrSubBudgetIncomeLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedger();
            this.xrSubBudgetIncomeLedgers.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // grpBudgetStatistics
            // 
            this.grpBudgetStatistics.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBudgetStatistics});
            this.grpBudgetStatistics.HeightF = 48.33336F;
            this.grpBudgetStatistics.Level = 2;
            this.grpBudgetStatistics.Name = "grpBudgetStatistics";
            // 
            // xrSubBudgetStatistics
            // 
            this.xrSubBudgetStatistics.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrSubBudgetStatistics.Name = "xrSubBudgetStatistics";
            this.xrSubBudgetStatistics.SizeF = new System.Drawing.SizeF(775.9999F, 22.99997F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // BudgetView
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpBudgetExpenseLedgers,
            this.GrpBudgetIncomeLedgers,
            this.grpBudgetStatistics});
            this.DataMember = "BUDGET_STATISTICS";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(40, 3, 61, 0);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpBudgetStatistics, 0);
            this.Controls.SetChildIndex(this.GrpBudgetIncomeLedgers, 0);
            this.Controls.SetChildIndex(this.grpBudgetExpenseLedgers, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetExpenseLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand GrpBudgetIncomeLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetStatistics;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetExpenseLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetIncomeLedgers;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetStatistics;
        private DevExpress.XtraReports.UI.XRLabel xrExpenseTitle;
        private DevExpress.XtraReports.UI.XRLabel xrIncomeTitle;
    }
}

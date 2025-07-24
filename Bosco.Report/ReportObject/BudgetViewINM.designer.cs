namespace Bosco.Report.ReportObject
{
    partial class BudgetViewINM
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
            this.xrtblTotalBudget = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotalIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrProposedTotalIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrApprovedTotalIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotalExpense = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrProposedTotalExpense = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrApprovedTotalExpense = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotalDifferenceCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrProposedTotalDifference = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrApprovedTotalDifference = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTotalCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSummaryCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapProposed = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapApproved = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSummaryCaption1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapProposedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapApprovedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotalBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotalCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
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
            this.xrSubBudgetExpenseLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedgerINM();
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
            this.xrSubBudgetIncomeLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedgerINM();
            this.xrSubBudgetIncomeLedgers.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // grpBudgetStatistics
            // 
            this.grpBudgetStatistics.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblTotalBudget,
            this.xrtblTotalCaption});
            this.grpBudgetStatistics.HeightF = 135.1042F;
            this.grpBudgetStatistics.Level = 2;
            this.grpBudgetStatistics.Name = "grpBudgetStatistics";
            // 
            // xrtblTotalBudget
            // 
            this.xrtblTotalBudget.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblTotalBudget.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblTotalBudget.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblTotalBudget.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 55.0521F);
            this.xrtblTotalBudget.Name = "xrtblTotalBudget";
            this.xrtblTotalBudget.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblTotalBudget.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrtblTotalBudget.SizeF = new System.Drawing.SizeF(470F, 75F);
            this.xrtblTotalBudget.StyleName = "styleRow";
            this.xrtblTotalBudget.StylePriority.UseBorderColor = false;
            this.xrtblTotalBudget.StylePriority.UseBorders = false;
            this.xrtblTotalBudget.StylePriority.UseFont = false;
            this.xrtblTotalBudget.StylePriority.UsePadding = false;
            this.xrtblTotalBudget.StylePriority.UseTextAlignment = false;
            this.xrtblTotalBudget.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotalIncome,
            this.xrProposedTotalIncome,
            this.xrApprovedTotalIncome});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTotalIncome
            // 
            this.xrTotalIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTotalIncome.Name = "xrTotalIncome";
            this.xrTotalIncome.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrTotalIncome.StylePriority.UseBorders = false;
            this.xrTotalIncome.StylePriority.UsePadding = false;
            this.xrTotalIncome.Text = "Budgeted Income";
            this.xrTotalIncome.Weight = 0.848114165102783D;
            // 
            // xrProposedTotalIncome
            // 
            this.xrProposedTotalIncome.Name = "xrProposedTotalIncome";
            this.xrProposedTotalIncome.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrProposedTotalIncome.StylePriority.UsePadding = false;
            this.xrProposedTotalIncome.StylePriority.UseTextAlignment = false;
            this.xrProposedTotalIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrProposedTotalIncome.Weight = 0.48188297995944046D;
            // 
            // xrApprovedTotalIncome
            // 
            this.xrApprovedTotalIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrApprovedTotalIncome.Name = "xrApprovedTotalIncome";
            this.xrApprovedTotalIncome.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrApprovedTotalIncome.StylePriority.UseBorders = false;
            this.xrApprovedTotalIncome.StylePriority.UsePadding = false;
            this.xrApprovedTotalIncome.StylePriority.UseTextAlignment = false;
            this.xrApprovedTotalIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrApprovedTotalIncome.Weight = 0.48188307021914023D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotalExpense,
            this.xrProposedTotalExpense,
            this.xrApprovedTotalExpense});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTotalExpense
            // 
            this.xrTotalExpense.Name = "xrTotalExpense";
            this.xrTotalExpense.Text = "Budgeted Expenditure";
            this.xrTotalExpense.Weight = 0.84811410627917028D;
            // 
            // xrProposedTotalExpense
            // 
            this.xrProposedTotalExpense.Name = "xrProposedTotalExpense";
            this.xrProposedTotalExpense.StylePriority.UseTextAlignment = false;
            this.xrProposedTotalExpense.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrProposedTotalExpense.Weight = 0.48188303878305327D;
            // 
            // xrApprovedTotalExpense
            // 
            this.xrApprovedTotalExpense.Name = "xrApprovedTotalExpense";
            this.xrApprovedTotalExpense.StylePriority.UseTextAlignment = false;
            this.xrApprovedTotalExpense.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrApprovedTotalExpense.Weight = 0.48188307021914023D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotalDifferenceCaption,
            this.xrProposedTotalDifference,
            this.xrApprovedTotalDifference});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTotalDifferenceCaption
            // 
            this.xrTotalDifferenceCaption.Name = "xrTotalDifferenceCaption";
            this.xrTotalDifferenceCaption.StylePriority.UseTextAlignment = false;
            this.xrTotalDifferenceCaption.Text = "Difference(Deficit / Surplus)";
            this.xrTotalDifferenceCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTotalDifferenceCaption.Weight = 0.84811410627917638D;
            // 
            // xrProposedTotalDifference
            // 
            this.xrProposedTotalDifference.Name = "xrProposedTotalDifference";
            this.xrProposedTotalDifference.StylePriority.UseTextAlignment = false;
            this.xrProposedTotalDifference.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrProposedTotalDifference.Weight = 0.48188303878305327D;
            // 
            // xrApprovedTotalDifference
            // 
            this.xrApprovedTotalDifference.Name = "xrApprovedTotalDifference";
            this.xrApprovedTotalDifference.StylePriority.UseTextAlignment = false;
            this.xrApprovedTotalDifference.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrApprovedTotalDifference.Weight = 0.48188307021913446D;
            // 
            // xrtblTotalCaption
            // 
            this.xrtblTotalCaption.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblTotalCaption.BorderColor = System.Drawing.Color.DarkGray;
            this.xrtblTotalCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblTotalCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblTotalCaption.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 5.052101F);
            this.xrtblTotalCaption.Name = "xrtblTotalCaption";
            this.xrtblTotalCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrtblTotalCaption.SizeF = new System.Drawing.SizeF(470F, 50F);
            this.xrtblTotalCaption.StyleName = "styleColumnHeader";
            this.xrtblTotalCaption.StylePriority.UseBackColor = false;
            this.xrtblTotalCaption.StylePriority.UseBorderColor = false;
            this.xrtblTotalCaption.StylePriority.UseBorders = false;
            this.xrtblTotalCaption.StylePriority.UseFont = false;
            this.xrtblTotalCaption.StylePriority.UseTextAlignment = false;
            this.xrtblTotalCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrSummaryCaption,
            this.xrCapProposed,
            this.xrCapApproved});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrSummaryCaption
            // 
            this.xrSummaryCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSummaryCaption.Name = "xrSummaryCaption";
            this.xrSummaryCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrSummaryCaption.StylePriority.UseBorders = false;
            this.xrSummaryCaption.StylePriority.UsePadding = false;
            this.xrSummaryCaption.StylePriority.UseTextAlignment = false;
            this.xrSummaryCaption.Text = "Budget Summary";
            this.xrSummaryCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrSummaryCaption.Weight = 1.0289539456810681D;
            // 
            // xrCapProposed
            // 
            this.xrCapProposed.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapProposed.Name = "xrCapProposed";
            this.xrCapProposed.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrCapProposed.StylePriority.UseBorders = false;
            this.xrCapProposed.StylePriority.UsePadding = false;
            this.xrCapProposed.StylePriority.UseTextAlignment = false;
            this.xrCapProposed.Text = "Proposed";
            this.xrCapProposed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapProposed.Weight = 0.58463294030953772D;
            // 
            // xrCapApproved
            // 
            this.xrCapApproved.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapApproved.Name = "xrCapApproved";
            this.xrCapApproved.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrCapApproved.StylePriority.UseBorders = false;
            this.xrCapApproved.StylePriority.UsePadding = false;
            this.xrCapApproved.StylePriority.UseTextAlignment = false;
            this.xrCapApproved.Text = "Approved";
            this.xrCapApproved.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapApproved.Weight = 0.5846329753199232D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrSummaryCaption1,
            this.xrCapProposedAmount,
            this.xrCapApprovedAmount});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrSummaryCaption1
            // 
            this.xrSummaryCaption1.Name = "xrSummaryCaption1";
            this.xrSummaryCaption1.Weight = 1.0289539456810681D;
            // 
            // xrCapProposedAmount
            // 
            this.xrCapProposedAmount.Name = "xrCapProposedAmount";
            this.xrCapProposedAmount.StylePriority.UseTextAlignment = false;
            this.xrCapProposedAmount.Text = "2025-2026";
            this.xrCapProposedAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapProposedAmount.Weight = 0.58463294030953772D;
            // 
            // xrCapApprovedAmount
            // 
            this.xrCapApprovedAmount.Name = "xrCapApprovedAmount";
            this.xrCapApprovedAmount.StylePriority.UseTextAlignment = false;
            this.xrCapApprovedAmount.Text = "2025-2026";
            this.xrCapApprovedAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapApprovedAmount.Weight = 0.5846329753199232D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // BudgetViewINM
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
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotalBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotalCaption)).EndInit();
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
        private DevExpress.XtraReports.UI.XRLabel xrExpenseTitle;
        private DevExpress.XtraReports.UI.XRLabel xrIncomeTitle;
        private DevExpress.XtraReports.UI.XRTable xrtblTotalBudget;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalIncome;
        private DevExpress.XtraReports.UI.XRTableCell xrProposedTotalIncome;
        private DevExpress.XtraReports.UI.XRTableCell xrApprovedTotalIncome;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalExpense;
        private DevExpress.XtraReports.UI.XRTableCell xrProposedTotalExpense;
        private DevExpress.XtraReports.UI.XRTableCell xrApprovedTotalExpense;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalDifferenceCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrProposedTotalDifference;
        private DevExpress.XtraReports.UI.XRTableCell xrApprovedTotalDifference;
        private DevExpress.XtraReports.UI.XRTable xrtblTotalCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrSummaryCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrCapProposed;
        private DevExpress.XtraReports.UI.XRTableCell xrCapApproved;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrSummaryCaption1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapProposedAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrCapApprovedAmount;
    }
}

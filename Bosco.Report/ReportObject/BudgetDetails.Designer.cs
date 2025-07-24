namespace Bosco.Report.ReportObject
{
    partial class BudgetDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetDetails));
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrHeaderRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellHeaderCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellHeaderParticular = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellHeaderBudgetSubGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellHeaderSumProposal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellHeaderSumApproval = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblBudget = new DevExpress.XtraReports.UI.XRTable();
            this.xrDataRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellParticular = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellProjectedAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpBudgetGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrBudgetGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpBudgetNature = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrBudgetNature = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblTotalPayments = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellSumProposal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellSumApproval = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.grpBudgetdetails = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblBudget});
            this.Detail.Expanded = true;
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeader});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeader
            // 
            resources.ApplyResources(this.xrtblHeader, "xrtblHeader");
            this.xrtblHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeader.Name = "xrtblHeader";
            this.xrtblHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrHeaderRow});
            this.xrtblHeader.StylePriority.UseBackColor = false;
            this.xrtblHeader.StylePriority.UseBorderColor = false;
            this.xrtblHeader.StylePriority.UseBorders = false;
            this.xrtblHeader.StylePriority.UseFont = false;
            this.xrtblHeader.StylePriority.UsePadding = false;
            this.xrtblHeader.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderRow
            // 
            this.xrHeaderRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellHeaderCode,
            this.xrcellHeaderParticular,
            this.xrcellHeaderBudgetSubGroup,
            this.xrcellHeaderSumProposal,
            this.xrcellHeaderSumApproval});
            this.xrHeaderRow.Name = "xrHeaderRow";
            resources.ApplyResources(this.xrHeaderRow, "xrHeaderRow");
            // 
            // xrcellHeaderCode
            // 
            resources.ApplyResources(this.xrcellHeaderCode, "xrcellHeaderCode");
            this.xrcellHeaderCode.Name = "xrcellHeaderCode";
            this.xrcellHeaderCode.StylePriority.UseFont = false;
            this.xrcellHeaderCode.StylePriority.UseTextAlignment = false;
            // 
            // xrcellHeaderParticular
            // 
            resources.ApplyResources(this.xrcellHeaderParticular, "xrcellHeaderParticular");
            this.xrcellHeaderParticular.Name = "xrcellHeaderParticular";
            this.xrcellHeaderParticular.StylePriority.UseFont = false;
            this.xrcellHeaderParticular.StylePriority.UseTextAlignment = false;
            // 
            // xrcellHeaderBudgetSubGroup
            // 
            resources.ApplyResources(this.xrcellHeaderBudgetSubGroup, "xrcellHeaderBudgetSubGroup");
            this.xrcellHeaderBudgetSubGroup.Name = "xrcellHeaderBudgetSubGroup";
            this.xrcellHeaderBudgetSubGroup.StylePriority.UseFont = false;
            // 
            // xrcellHeaderSumProposal
            // 
            resources.ApplyResources(this.xrcellHeaderSumProposal, "xrcellHeaderSumProposal");
            this.xrcellHeaderSumProposal.Name = "xrcellHeaderSumProposal";
            this.xrcellHeaderSumProposal.StylePriority.UseFont = false;
            this.xrcellHeaderSumProposal.StylePriority.UseTextAlignment = false;
            // 
            // xrcellHeaderSumApproval
            // 
            this.xrcellHeaderSumApproval.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrcellHeaderSumApproval, "xrcellHeaderSumApproval");
            this.xrcellHeaderSumApproval.Name = "xrcellHeaderSumApproval";
            this.xrcellHeaderSumApproval.StylePriority.UseBorders = false;
            this.xrcellHeaderSumApproval.StylePriority.UseFont = false;
            this.xrcellHeaderSumApproval.StylePriority.UseTextAlignment = false;
            // 
            // xrtblBudget
            // 
            resources.ApplyResources(this.xrtblBudget, "xrtblBudget");
            this.xrtblBudget.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblBudget.Name = "xrtblBudget";
            this.xrtblBudget.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblBudget.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDataRow});
            this.xrtblBudget.StyleName = "styleRow";
            this.xrtblBudget.StylePriority.UseBackColor = false;
            this.xrtblBudget.StylePriority.UseBorderColor = false;
            this.xrtblBudget.StylePriority.UseBorders = false;
            this.xrtblBudget.StylePriority.UseFont = false;
            this.xrtblBudget.StylePriority.UsePadding = false;
            this.xrtblBudget.StylePriority.UseTextAlignment = false;
            // 
            // xrDataRow
            // 
            this.xrDataRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrcellParticular,
            this.xrTableCell5,
            this.xrCellProjectedAmt});
            this.xrDataRow.Name = "xrDataRow";
            resources.ApplyResources(this.xrDataRow, "xrDataRow");
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.LEDGER_CODE")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrTableCell7, "xrTableCell7");
            // 
            // xrcellParticular
            // 
            this.xrcellParticular.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.LEDGER_NAME")});
            this.xrcellParticular.Name = "xrcellParticular";
            this.xrcellParticular.StylePriority.UseFont = false;
            resources.ApplyResources(this.xrcellParticular, "xrcellParticular");
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.PROPOSED_AMOUNT", "{0:n}")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            // 
            // xrCellProjectedAmt
            // 
            this.xrCellProjectedAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.APPROVED_AMOUNT", "{0:n}")});
            this.xrCellProjectedAmt.Name = "xrCellProjectedAmt";
            this.xrCellProjectedAmt.StylePriority.UseFont = false;
            this.xrCellProjectedAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellProjectedAmt, "xrCellProjectedAmt");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpBudgetGroup
            // 
            this.grpBudgetGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.grpBudgetGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("BUDGET_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
            resources.ApplyResources(this.grpBudgetGroup, "grpBudgetGroup");
            this.grpBudgetGroup.Name = "grpBudgetGroup";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "LEDGER_CODE";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpBudgetGroup.SortingSummary = xrGroupSortingSummary1;
            this.grpBudgetGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpBudgetGroup_BeforePrint);
            // 
            // xrTable3
            // 
            resources.ApplyResources(this.xrTable3, "xrTable3");
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable3.StylePriority.UseBorderColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrBudgetGroup});
            this.xrTableRow6.Name = "xrTableRow6";
            resources.ApplyResources(this.xrTableRow6, "xrTableRow6");
            // 
            // xrBudgetGroup
            // 
            this.xrBudgetGroup.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.BUDGET_GROUP")});
            resources.ApplyResources(this.xrBudgetGroup, "xrBudgetGroup");
            this.xrBudgetGroup.Name = "xrBudgetGroup";
            this.xrBudgetGroup.StylePriority.UseFont = false;
            this.xrBudgetGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrBudgetGroup_BeforePrint);
            // 
            // grpBudgetNature
            // 
            this.grpBudgetNature.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.grpBudgetNature.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("BUDGET_NATURE", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
            resources.ApplyResources(this.grpBudgetNature, "grpBudgetNature");
            this.grpBudgetNature.Level = 1;
            this.grpBudgetNature.Name = "grpBudgetNature";
            // 
            // xrTable4
            // 
            resources.ApplyResources(this.xrTable4, "xrTable4");
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable4.StylePriority.UseBorderColor = false;
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrBudgetNature});
            this.xrTableRow7.Name = "xrTableRow7";
            resources.ApplyResources(this.xrTableRow7, "xrTableRow7");
            // 
            // xrBudgetNature
            // 
            this.xrBudgetNature.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.BUDGET_NATURE")});
            resources.ApplyResources(this.xrBudgetNature, "xrBudgetNature");
            this.xrBudgetNature.Name = "xrBudgetNature";
            this.xrBudgetNature.StylePriority.UseFont = false;
            this.xrBudgetNature.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrBudgetNature_BeforePrint);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGrandTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblGrandTotal
            // 
            resources.ApplyResources(this.xrtblGrandTotal, "xrtblGrandTotal");
            this.xrtblGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrtblTotalPayments});
            this.xrTableRow5.Name = "xrTableRow5";
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // xrTableCell3
            // 
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.PROPOSED_AMOUNT")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell6.Summary = xrSummary1;
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            // 
            // xrtblTotalPayments
            // 
            resources.ApplyResources(this.xrtblTotalPayments, "xrtblTotalPayments");
            this.xrtblTotalPayments.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.GROUP_CODE")});
            this.xrtblTotalPayments.Name = "xrtblTotalPayments";
            this.xrtblTotalPayments.StylePriority.UseBorderColor = false;
            this.xrtblTotalPayments.StylePriority.UseFont = false;
            this.xrtblTotalPayments.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrtblTotalPayments.Summary = xrSummary2;
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellTotal,
            this.xrcellSumProposal,
            this.xrcellSumApproval});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrcellTotal
            // 
            resources.ApplyResources(this.xrcellTotal, "xrcellTotal");
            this.xrcellTotal.Name = "xrcellTotal";
            this.xrcellTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrcellTotal.StylePriority.UseBorderColor = false;
            this.xrcellTotal.StylePriority.UseFont = false;
            this.xrcellTotal.StylePriority.UsePadding = false;
            this.xrcellTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrcellSumProposal
            // 
            this.xrcellSumProposal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.PROPOSED_AMOUNT")});
            this.xrcellSumProposal.Name = "xrcellSumProposal";
            this.xrcellSumProposal.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellSumProposal.Summary = xrSummary3;
            resources.ApplyResources(this.xrcellSumProposal, "xrcellSumProposal");
            // 
            // xrcellSumApproval
            // 
            resources.ApplyResources(this.xrcellSumApproval, "xrcellSumApproval");
            this.xrcellSumApproval.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.GROUP_CODE")});
            this.xrcellSumApproval.Name = "xrcellSumApproval";
            this.xrcellSumApproval.StylePriority.UseBorderColor = false;
            this.xrcellSumApproval.StylePriority.UseFont = false;
            this.xrcellSumApproval.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellSumApproval.Summary = xrSummary4;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            resources.ApplyResources(this.GroupFooter1, "GroupFooter1");
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // grpBudgetdetails
            // 
            this.grpBudgetdetails.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.grpBudgetdetails, "grpBudgetdetails");
            this.grpBudgetdetails.Level = 2;
            this.grpBudgetdetails.Name = "grpBudgetdetails";
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell2
            // 
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell8
            // 
            resources.ApplyResources(this.xrTableCell8, "xrTableCell8");
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell9, "xrTableCell9");
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            // 
            // BudgetDetails
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpBudgetGroup,
            this.grpBudgetNature,
            this.ReportFooter,
            this.GroupFooter1,
            this.grpBudgetdetails});
            this.DataMember = "BUDGETVARIANCE";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpBudgetdetails, 0);
            this.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpBudgetNature, 0);
            this.Controls.SetChildIndex(this.grpBudgetGroup, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrHeaderRow;
        private DevExpress.XtraReports.UI.XRTableCell xrcellHeaderCode;
        private DevExpress.XtraReports.UI.XRTableCell xrcellHeaderParticular;
        private DevExpress.XtraReports.UI.XRTableCell xrcellHeaderSumApproval;
        private DevExpress.XtraReports.UI.XRTable xrtblBudget;
        private DevExpress.XtraReports.UI.XRTableRow xrDataRow;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrcellParticular;
        private DevExpress.XtraReports.UI.XRTableCell xrCellProjectedAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrcellHeaderSumProposal;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetGroup;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrBudgetGroup;
        private DevExpress.XtraReports.UI.XRTableCell xrcellHeaderBudgetSubGroup;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetNature;
        private DevExpress.XtraReports.UI.XRTable xrTable4;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrBudgetNature;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrtblTotalPayments;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrcellTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrcellSumProposal;
        private DevExpress.XtraReports.UI.XRTableCell xrcellSumApproval;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetdetails;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
    }
}

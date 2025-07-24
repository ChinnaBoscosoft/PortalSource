namespace Bosco.Report.ReportObject
{
    partial class BankFlow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankFlow));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.prOPBalance = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrtblBindBankFlow = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrInflow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrOutflow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashInFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashOutFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpftrClBalance = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrtblCbalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.calCLBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandInFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandOutFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpHeaderOPBalance = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrOpeningBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrOPBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapInFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapOutFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBalance = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBindBankFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCbalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrOpeningBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblBindBankFlow});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // prOPBalance
            // 
            resources.ApplyResources(this.prOPBalance, "prOPBalance");
            this.prOPBalance.Name = "prOPBalance";
            this.prOPBalance.Type = typeof(int);
            this.prOPBalance.ValueInfo = "0";
            // 
            // xrtblBindBankFlow
            // 
            resources.ApplyResources(this.xrtblBindBankFlow, "xrtblBindBankFlow");
            this.xrtblBindBankFlow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblBindBankFlow.Name = "xrtblBindBankFlow";
            this.xrtblBindBankFlow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblBindBankFlow.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblBindBankFlow.StyleName = "styleRow";
            this.xrtblBindBankFlow.StylePriority.UseBorderColor = false;
            this.xrtblBindBankFlow.StylePriority.UseBorders = false;
            this.xrtblBindBankFlow.StylePriority.UseFont = false;
            this.xrtblBindBankFlow.StylePriority.UsePadding = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrInflow,
            this.xrOutflow,
            this.xrCashBalance});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrDate
            // 
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.DATE", "{0:d}")});
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrDate, "xrDate");
            this.xrDate.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDate_BeforePrint);
            // 
            // xrInflow
            // 
            this.xrInflow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.IN_FLOW", "{0:n}")});
            this.xrInflow.Name = "xrInflow";
            this.xrInflow.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrInflow.Summary = xrSummary1;
            resources.ApplyResources(this.xrInflow, "xrInflow");
            this.xrInflow.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrInflow_BeforePrint);
            // 
            // xrOutflow
            // 
            this.xrOutflow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.OUT_FLOW", "{0:n}")});
            this.xrOutflow.Name = "xrOutflow";
            this.xrOutflow.StylePriority.UseTextAlignment = false;
            xrSummary2.IgnoreNullValues = true;
            this.xrOutflow.Summary = xrSummary2;
            resources.ApplyResources(this.xrOutflow, "xrOutflow");
            this.xrOutflow.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrOutflow_BeforePrint);
            // 
            // xrCashBalance
            // 
            this.xrCashBalance.Name = "xrCashBalance";
            this.xrCashBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            this.xrCashBalance.Summary = xrSummary3;
            resources.ApplyResources(this.xrCashBalance, "xrCashBalance");
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
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblGrandTotal.StyleName = "styleTotalRow";
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrCashInFlow,
            this.xrCashOutFlow,
            this.xrCashAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            // 
            // xrCashInFlow
            // 
            this.xrCashInFlow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.calGrandInFlow", "{0:n}")});
            this.xrCashInFlow.Name = "xrCashInFlow";
            this.xrCashInFlow.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.IgnoreNullValues = true;
            this.xrCashInFlow.Summary = xrSummary4;
            resources.ApplyResources(this.xrCashInFlow, "xrCashInFlow");
            // 
            // xrCashOutFlow
            // 
            this.xrCashOutFlow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.calGrandOutFlow", "{0:n}")});
            this.xrCashOutFlow.Name = "xrCashOutFlow";
            this.xrCashOutFlow.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary5, "xrSummary5");
            xrSummary5.IgnoreNullValues = true;
            this.xrCashOutFlow.Summary = xrSummary5;
            resources.ApplyResources(this.xrCashOutFlow, "xrCashOutFlow");
            // 
            // xrCashAmount
            // 
            this.xrCashAmount.Name = "xrCashAmount";
            this.xrCashAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary6, "xrSummary6");
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary6.IgnoreNullValues = true;
            this.xrCashAmount.Summary = xrSummary6;
            resources.ApplyResources(this.xrCashAmount, "xrCashAmount");
            // 
            // grpftrClBalance
            // 
            this.grpftrClBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCbalance});
            resources.ApplyResources(this.grpftrClBalance, "grpftrClBalance");
            this.grpftrClBalance.Name = "grpftrClBalance";
            // 
            // xrtblCbalance
            // 
            resources.ApplyResources(this.xrtblCbalance, "xrtblCbalance");
            this.xrtblCbalance.Name = "xrtblCbalance";
            this.xrtblCbalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrtblCbalance.StyleName = "styleGroupRow";
            this.xrtblCbalance.StylePriority.UseFont = false;
            this.xrtblCbalance.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblClosingBalance});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrtblClosingBalance
            // 
            resources.ApplyResources(this.xrtblClosingBalance, "xrtblClosingBalance");
            this.xrtblClosingBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblClosingBalance.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.calClBalance", "{0:n}")});
            this.xrtblClosingBalance.Name = "xrtblClosingBalance";
            this.xrtblClosingBalance.StylePriority.UseBorderColor = false;
            this.xrtblClosingBalance.StylePriority.UseBorders = false;
            this.xrtblClosingBalance.StylePriority.UseFont = false;
            this.xrtblClosingBalance.StylePriority.UseTextAlignment = false;
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblClosingBalance.Summary = xrSummary7;
            this.xrtblClosingBalance.SummaryRowChanged += new System.EventHandler(this.xrtblClosingBalance_SummaryRowChanged);
            // 
            // xrBalance
            // 
            this.xrBalance.Name = "xrBalance";
            this.xrBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary8, "xrSummary8");
            xrSummary8.IgnoreNullValues = true;
            this.xrBalance.Summary = xrSummary8;
            resources.ApplyResources(this.xrBalance, "xrBalance");
            // 
            // calCLBalance
            // 
            this.calCLBalance.DataMember = "CashBankFlow";
            this.calCLBalance.Name = "calCLBalance";
            // 
            // calGrandInFlow
            // 
            this.calGrandInFlow.DataMember = "CashBankFlow";
            this.calGrandInFlow.Expression = "Iif(IsNullOrEmpty([].Sum([IN_FLOW])), 0, [].Sum([IN_FLOW]))  + [Parameters.prOPBa" +
    "lance]";
            this.calGrandInFlow.Name = "calGrandInFlow";
            // 
            // calGrandOutFlow
            // 
            this.calGrandOutFlow.DataMember = "CashBankFlow";
            this.calGrandOutFlow.Expression = "Iif(IsNullOrEmpty([].Sum([OUT_FLOW])), 0, [].Sum([OUT_FLOW])) ";
            this.calGrandOutFlow.Name = "calGrandOutFlow";
            // 
            // grpHeaderOPBalance
            // 
            this.grpHeaderOPBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrOpeningBalance});
            resources.ApplyResources(this.grpHeaderOPBalance, "grpHeaderOPBalance");
            this.grpHeaderOPBalance.Name = "grpHeaderOPBalance";
            // 
            // xrOpeningBalance
            // 
            resources.ApplyResources(this.xrOpeningBalance, "xrOpeningBalance");
            this.xrOpeningBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrOpeningBalance.Name = "xrOpeningBalance";
            this.xrOpeningBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrOpeningBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrOpeningBalance.StyleName = "styleGroupRow";
            this.xrOpeningBalance.StylePriority.UseBackColor = false;
            this.xrOpeningBalance.StylePriority.UseBorderColor = false;
            this.xrOpeningBalance.StylePriority.UseBorders = false;
            this.xrOpeningBalance.StylePriority.UseFont = false;
            this.xrOpeningBalance.StylePriority.UsePadding = false;
            this.xrOpeningBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrOPBalance,
            this.xrTableCell14});
            this.xrTableRow7.Name = "xrTableRow7";
            resources.ApplyResources(this.xrTableRow7, "xrTableRow7");
            // 
            // xrTableCell12
            // 
            resources.ApplyResources(this.xrTableCell12, "xrTableCell12");
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBackColor = false;
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            // 
            // xrOPBalance
            // 
            resources.ApplyResources(this.xrOPBalance, "xrOPBalance");
            this.xrOPBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrOPBalance.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.prOPBalance, "Text", "{0:n}")});
            this.xrOPBalance.Name = "xrOPBalance";
            this.xrOPBalance.StylePriority.UseBackColor = false;
            this.xrOPBalance.StylePriority.UseBorderColor = false;
            this.xrOPBalance.StylePriority.UseBorders = false;
            this.xrOPBalance.StylePriority.UseFont = false;
            this.xrOPBalance.StylePriority.UseTextAlignment = false;
            this.xrOPBalance.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrOPBalance_SummaryGetResult);
            // 
            // xrTableCell14
            // 
            resources.ApplyResources(this.xrTableCell14, "xrTableCell14");
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBackColor = false;
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrCapInFlow,
            this.xrCapOutFlow,
            this.xrCapBalance});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell4
            // 
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell4.StylePriority.UseBackColor = false;
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            // 
            // xrCapInFlow
            // 
            resources.ApplyResources(this.xrCapInFlow, "xrCapInFlow");
            this.xrCapInFlow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapInFlow.Name = "xrCapInFlow";
            this.xrCapInFlow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapInFlow.StylePriority.UseBackColor = false;
            this.xrCapInFlow.StylePriority.UseBorderColor = false;
            this.xrCapInFlow.StylePriority.UseBorders = false;
            this.xrCapInFlow.StylePriority.UseFont = false;
            this.xrCapInFlow.StylePriority.UsePadding = false;
            this.xrCapInFlow.StylePriority.UseTextAlignment = false;
            // 
            // xrCapOutFlow
            // 
            resources.ApplyResources(this.xrCapOutFlow, "xrCapOutFlow");
            this.xrCapOutFlow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapOutFlow.Name = "xrCapOutFlow";
            this.xrCapOutFlow.StylePriority.UseBackColor = false;
            this.xrCapOutFlow.StylePriority.UseBorderColor = false;
            this.xrCapOutFlow.StylePriority.UseBorders = false;
            this.xrCapOutFlow.StylePriority.UseFont = false;
            this.xrCapOutFlow.StylePriority.UseTextAlignment = false;
            // 
            // xrCapBalance
            // 
            resources.ApplyResources(this.xrCapBalance, "xrCapBalance");
            this.xrCapBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapBalance.Name = "xrCapBalance";
            this.xrCapBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapBalance.StylePriority.UseBackColor = false;
            this.xrCapBalance.StylePriority.UseBorderColor = false;
            this.xrCapBalance.StylePriority.UseBorders = false;
            this.xrCapBalance.StylePriority.UseFont = false;
            this.xrCapBalance.StylePriority.UsePadding = false;
            this.xrCapBalance.StylePriority.UseTextAlignment = false;
            // 
            // BankFlow
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.grpftrClBalance,
            this.grpHeaderOPBalance,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calCLBalance,
            this.calGrandInFlow,
            this.calGrandOutFlow});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.prOPBalance});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.grpHeaderOPBalance, 0);
            this.Controls.SetChildIndex(this.grpftrClBalance, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBindBankFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCbalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrOpeningBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrtblBindBankFlow;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrInflow;
        private DevExpress.XtraReports.UI.XRTableCell xrOutflow;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBalance;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrCashInFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCashOutFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCashAmount;
        private DevExpress.XtraReports.UI.GroupFooterBand grpftrClBalance;
        private DevExpress.XtraReports.UI.XRTable xrtblCbalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrtblClosingBalance;
        private DevExpress.XtraReports.Parameters.Parameter prOPBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrBalance;
        private DevExpress.XtraReports.UI.CalculatedField calCLBalance;
        private DevExpress.XtraReports.UI.CalculatedField calGrandInFlow;
        private DevExpress.XtraReports.UI.CalculatedField calGrandOutFlow;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderOPBalance;
        private DevExpress.XtraReports.UI.XRTable xrOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrOPBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrCapInFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapOutFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBalance;
    }
}

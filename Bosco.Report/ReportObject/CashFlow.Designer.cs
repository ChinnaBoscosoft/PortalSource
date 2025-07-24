namespace Bosco.Report.ReportObject
{
    partial class CashFlow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashFlow));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblBindData = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCashDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashInflow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashOutFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.prOPBalance = new DevExpress.XtraReports.Parameters.Parameter();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashOutAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBalanceAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.calGrandInFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandOutFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.calClBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpftrClBalance = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrtblCbalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpHeaderOPBalance = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblOpeningBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrOPBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapInFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapOutFlow = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBalance = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBindData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCbalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblOpeningBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblBindData});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.Detail.StylePriority.UsePadding = false;
            // 
            // xrtblBindData
            // 
            resources.ApplyResources(this.xrtblBindData, "xrtblBindData");
            this.xrtblBindData.Name = "xrtblBindData";
            this.xrtblBindData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblBindData.StyleName = "styleRow";
            this.xrtblBindData.StylePriority.UseFont = false;
            this.xrtblBindData.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCashDate,
            this.xrCashInflow,
            this.xrCashOutFlow,
            this.xrCashBalance});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCashDate
            // 
            resources.ApplyResources(this.xrCashDate, "xrCashDate");
            this.xrCashDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.DATE", "{0:d}")});
            this.xrCashDate.Name = "xrCashDate";
            this.xrCashDate.StyleName = "styleDateInfo";
            this.xrCashDate.StylePriority.UseBorderColor = false;
            this.xrCashDate.StylePriority.UseBorders = false;
            this.xrCashDate.StylePriority.UseFont = false;
            this.xrCashDate.StylePriority.UseTextAlignment = false;
            this.xrCashDate.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCashDate_BeforePrint);
            // 
            // xrCashInflow
            // 
            resources.ApplyResources(this.xrCashInflow, "xrCashInflow");
            this.xrCashInflow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashInflow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.IN_FLOW", "{0:n}")});
            this.xrCashInflow.Name = "xrCashInflow";
            this.xrCashInflow.StylePriority.UseBorderColor = false;
            this.xrCashInflow.StylePriority.UseBorders = false;
            this.xrCashInflow.StylePriority.UseFont = false;
            this.xrCashInflow.StylePriority.UseTextAlignment = false;
            this.xrCashInflow.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCashInflow_BeforePrint);
            // 
            // xrCashOutFlow
            // 
            resources.ApplyResources(this.xrCashOutFlow, "xrCashOutFlow");
            this.xrCashOutFlow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashOutFlow.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.OUT_FLOW", "{0:n}")});
            this.xrCashOutFlow.Name = "xrCashOutFlow";
            this.xrCashOutFlow.StylePriority.UseBorderColor = false;
            this.xrCashOutFlow.StylePriority.UseBorders = false;
            this.xrCashOutFlow.StylePriority.UseFont = false;
            this.xrCashOutFlow.StylePriority.UseTextAlignment = false;
            this.xrCashOutFlow.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCashOutFlow_BeforePrint);
            // 
            // xrCashBalance
            // 
            resources.ApplyResources(this.xrCashBalance, "xrCashBalance");
            this.xrCashBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashBalance.Name = "xrCashBalance";
            this.xrCashBalance.StylePriority.UseBorderColor = false;
            this.xrCashBalance.StylePriority.UseBorders = false;
            this.xrCashBalance.StylePriority.UseFont = false;
            this.xrCashBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrCashBalance.Summary = xrSummary1;
            // 
            // prOPBalance
            // 
            resources.ApplyResources(this.prOPBalance, "prOPBalance");
            this.prOPBalance.Name = "prOPBalance";
            this.prOPBalance.Type = typeof(int);
            this.prOPBalance.ValueInfo = "0";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrCashAmt,
            this.xrCashOutAmt,
            this.xrCashBalanceAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrGrandTotal
            // 
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            this.xrGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGrandTotal.StylePriority.UseBackColor = false;
            this.xrGrandTotal.StylePriority.UseBorderColor = false;
            this.xrGrandTotal.StylePriority.UseBorders = false;
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UsePadding = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrCashAmt
            // 
            resources.ApplyResources(this.xrCashAmt, "xrCashAmt");
            this.xrCashAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.calGrandInFlow", "{0:n}")});
            this.xrCashAmt.Name = "xrCashAmt";
            this.xrCashAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCashAmt.StylePriority.UseBackColor = false;
            this.xrCashAmt.StylePriority.UseBorderColor = false;
            this.xrCashAmt.StylePriority.UseBorders = false;
            this.xrCashAmt.StylePriority.UseFont = false;
            this.xrCashAmt.StylePriority.UsePadding = false;
            this.xrCashAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            this.xrCashAmt.Summary = xrSummary2;
            // 
            // xrCashOutAmt
            // 
            resources.ApplyResources(this.xrCashOutAmt, "xrCashOutAmt");
            this.xrCashOutAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashOutAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankFlow.calGrandOutFlow", "{0:n}")});
            this.xrCashOutAmt.Name = "xrCashOutAmt";
            this.xrCashOutAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCashOutAmt.StylePriority.UseBackColor = false;
            this.xrCashOutAmt.StylePriority.UseBorderColor = false;
            this.xrCashOutAmt.StylePriority.UseBorders = false;
            this.xrCashOutAmt.StylePriority.UseFont = false;
            this.xrCashOutAmt.StylePriority.UsePadding = false;
            this.xrCashOutAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            this.xrCashOutAmt.Summary = xrSummary3;
            // 
            // xrCashBalanceAmt
            // 
            resources.ApplyResources(this.xrCashBalanceAmt, "xrCashBalanceAmt");
            this.xrCashBalanceAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashBalanceAmt.Name = "xrCashBalanceAmt";
            this.xrCashBalanceAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCashBalanceAmt.StylePriority.UseBackColor = false;
            this.xrCashBalanceAmt.StylePriority.UseBorderColor = false;
            this.xrCashBalanceAmt.StylePriority.UseBorders = false;
            this.xrCashBalanceAmt.StylePriority.UseFont = false;
            this.xrCashBalanceAmt.StylePriority.UsePadding = false;
            this.xrCashBalanceAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.IgnoreNullValues = true;
            this.xrCashBalanceAmt.Summary = xrSummary4;
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
            // calClBalance
            // 
            this.calClBalance.DataMember = "CashBankFlow";
            this.calClBalance.Name = "calClBalance";
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
            this.xrtblClosingBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblClosingBalance.StylePriority.UseBorderColor = false;
            this.xrtblClosingBalance.StylePriority.UseBorders = false;
            this.xrtblClosingBalance.StylePriority.UseFont = false;
            this.xrtblClosingBalance.StylePriority.UsePadding = false;
            this.xrtblClosingBalance.StylePriority.UseTextAlignment = false;
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblClosingBalance.Summary = xrSummary5;
            this.xrtblClosingBalance.SummaryRowChanged += new System.EventHandler(this.xrtblClosingBalance_SummaryRowChanged);
            // 
            // grpHeaderOPBalance
            // 
            this.grpHeaderOPBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblOpeningBalance});
            resources.ApplyResources(this.grpHeaderOPBalance, "grpHeaderOPBalance");
            this.grpHeaderOPBalance.Name = "grpHeaderOPBalance";
            // 
            // xrtblOpeningBalance
            // 
            resources.ApplyResources(this.xrtblOpeningBalance, "xrtblOpeningBalance");
            this.xrtblOpeningBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblOpeningBalance.Name = "xrtblOpeningBalance";
            this.xrtblOpeningBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblOpeningBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrtblOpeningBalance.StyleName = "styleGroupRow";
            this.xrtblOpeningBalance.StylePriority.UseBackColor = false;
            this.xrtblOpeningBalance.StylePriority.UseBorderColor = false;
            this.xrtblOpeningBalance.StylePriority.UseBorders = false;
            this.xrtblOpeningBalance.StylePriority.UseFont = false;
            this.xrtblOpeningBalance.StylePriority.UsePadding = false;
            this.xrtblOpeningBalance.StylePriority.UseTextAlignment = false;
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
            this.xrTableRow2});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrCapInFlow,
            this.xrCapOutFlow,
            this.xrCapBalance});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
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
            this.xrCapOutFlow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapOutFlow.StylePriority.UseBackColor = false;
            this.xrCapOutFlow.StylePriority.UseBorderColor = false;
            this.xrCapOutFlow.StylePriority.UseBorders = false;
            this.xrCapOutFlow.StylePriority.UseFont = false;
            this.xrCapOutFlow.StylePriority.UsePadding = false;
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
            // CashFlow
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.grpftrClBalance,
            this.grpHeaderOPBalance,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calGrandInFlow,
            this.calGrandOutFlow,
            this.calClBalance});
            this.DataMember = "CashBankFlow";
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
            ((System.ComponentModel.ISupportInitialize)(this.xrtblBindData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCbalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblOpeningBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblBindData;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCashDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCashInflow;
        private DevExpress.XtraReports.UI.XRTableCell xrCashOutFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBalance;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrCashAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCashOutAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBalanceAmt;
        private DevExpress.XtraReports.UI.CalculatedField calGrandInFlow;
        private DevExpress.XtraReports.UI.CalculatedField calGrandOutFlow;
        private DevExpress.XtraReports.UI.CalculatedField calClBalance;
        private DevExpress.XtraReports.Parameters.Parameter prOPBalance;
        private DevExpress.XtraReports.UI.GroupFooterBand grpftrClBalance;
        private DevExpress.XtraReports.UI.XRTable xrtblCbalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrtblClosingBalance;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderOPBalance;
        private DevExpress.XtraReports.UI.XRTable xrtblOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrOPBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrCapInFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapOutFlow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBalance;
    }
}

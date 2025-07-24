namespace Bosco.Report.ReportObject
{
    partial class CashBankTransactions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashBankTransactions));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblCashBankTrans = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrVoucherDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCostCentre = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCostCentre = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCashBankTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCashBankTrans});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // xrtblCashBankTrans
            // 
            resources.ApplyResources(this.xrtblCashBankTrans, "xrtblCashBankTrans");
            this.xrtblCashBankTrans.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCashBankTrans.Name = "xrtblCashBankTrans";
            this.xrtblCashBankTrans.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCashBankTrans.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblCashBankTrans.StyleName = "styleRow";
            this.xrtblCashBankTrans.StylePriority.UseBackColor = false;
            this.xrtblCashBankTrans.StylePriority.UseBorderColor = false;
            this.xrtblCashBankTrans.StylePriority.UseBorders = false;
            this.xrtblCashBankTrans.StylePriority.UseFont = false;
            this.xrtblCashBankTrans.StylePriority.UsePadding = false;
            this.xrtblCashBankTrans.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrVoucherDate,
            this.xrLedger,
            this.xrCostCentre,
            this.xrVoucherNo,
            this.xrNarration,
            this.xrCashBank,
            this.xrCredit,
            this.xrDebit});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrVoucherDate
            // 
            this.xrVoucherDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.VOUCHER_DATE", "{0:d}")});
            this.xrVoucherDate.Name = "xrVoucherDate";
            this.xrVoucherDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrVoucherDate, "xrVoucherDate");
            // 
            // xrLedger
            // 
            this.xrLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.LEDGER_NAME")});
            this.xrLedger.Name = "xrLedger";
            resources.ApplyResources(this.xrLedger, "xrLedger");
            // 
            // xrCostCentre
            // 
            this.xrCostCentre.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.COSTCENTRE")});
            this.xrCostCentre.Name = "xrCostCentre";
            resources.ApplyResources(this.xrCostCentre, "xrCostCentre");
            // 
            // xrVoucherNo
            // 
            this.xrVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.VOUCHER_NO")});
            this.xrVoucherNo.Name = "xrVoucherNo";
            resources.ApplyResources(this.xrVoucherNo, "xrVoucherNo");
            // 
            // xrNarration
            // 
            this.xrNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.NARRATION")});
            this.xrNarration.Name = "xrNarration";
            resources.ApplyResources(this.xrNarration, "xrNarration");
            // 
            // xrCashBank
            // 
            this.xrCashBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.TRANS_MODE")});
            this.xrCashBank.Name = "xrCashBank";
            resources.ApplyResources(this.xrCashBank, "xrCashBank");
            // 
            // xrCredit
            // 
            this.xrCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.CREDIT", "{0:n}")});
            this.xrCredit.Name = "xrCredit";
            this.xrCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCredit, "xrCredit");
            this.xrCredit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCredit_BeforePrint);
            // 
            // xrDebit
            // 
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.DEBIT", "{0:n1}")});
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrDebit.Summary = xrSummary1;
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDebit_BeforePrint);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
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
            this.xrCapDate,
            this.xrCapLedger,
            this.xrCapCostCentre,
            this.xrCapVoucherNo,
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrCapCredit,
            this.xrCapDebit});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapLedger
            // 
            this.xrCapLedger.Name = "xrCapLedger";
            resources.ApplyResources(this.xrCapLedger, "xrCapLedger");
            // 
            // xrCapCostCentre
            // 
            this.xrCapCostCentre.Name = "xrCapCostCentre";
            resources.ApplyResources(this.xrCapCostCentre, "xrCapCostCentre");
            // 
            // xrCapVoucherNo
            // 
            this.xrCapVoucherNo.Name = "xrCapVoucherNo";
            resources.ApplyResources(this.xrCapVoucherNo, "xrCapVoucherNo");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            // 
            // xrCapCredit
            // 
            this.xrCapCredit.Multiline = true;
            this.xrCapCredit.Name = "xrCapCredit";
            this.xrCapCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapCredit, "xrCapCredit");
            // 
            // xrCapDebit
            // 
            this.xrCapDebit.Name = "xrCapDebit";
            this.xrCapDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDebit, "xrCapDebit");
            // 
            // CashBankTransactions
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCashBankTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblCashBankTrans;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrCostCentre;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBank;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrCredit;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherDate;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCostCentre;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
    }
}

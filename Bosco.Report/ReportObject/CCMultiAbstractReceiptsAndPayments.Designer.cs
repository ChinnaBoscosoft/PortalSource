namespace Bosco.Report.ReportObject
{
    partial class CCMultiAbstractReceiptsAndPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCMultiAbstractReceiptsAndPayments));
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlReceipt = new DevExpress.XtraReports.UI.DetailBand();
            this.xrCosSubMultiAbstractReceipt = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.dtlMulti = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlPayment = new DevExpress.XtraReports.UI.DetailBand();
            this.xrCosSubMultiAbstractPayment = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.dtlReceipt,
            this.dtlMulti});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // dtlReceipt
            // 
            this.dtlReceipt.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosSubMultiAbstractReceipt,
            this.xrLabel1});
            resources.ApplyResources(this.dtlReceipt, "dtlReceipt");
            this.dtlReceipt.Name = "dtlReceipt";
            // 
            // xrCosSubMultiAbstractReceipt
            // 
            resources.ApplyResources(this.xrCosSubMultiAbstractReceipt, "xrCosSubMultiAbstractReceipt");
            this.xrCosSubMultiAbstractReceipt.Name = "xrCosSubMultiAbstractReceipt";
            this.xrCosSubMultiAbstractReceipt.ReportSource = new Bosco.Report.ReportObject.CCMultiAbstractReceipts();
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StyleName = "styleTitleRow";
            // 
            // dtlMulti
            // 
            this.dtlMulti.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.dtlPayment});
            this.dtlMulti.Level = 0;
            this.dtlMulti.Name = "dtlMulti";
            // 
            // dtlPayment
            // 
            this.dtlPayment.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosSubMultiAbstractPayment,
            this.xrLabel2});
            resources.ApplyResources(this.dtlPayment, "dtlPayment");
            this.dtlPayment.Name = "dtlPayment";
            // 
            // xrCosSubMultiAbstractPayment
            // 
            resources.ApplyResources(this.xrCosSubMultiAbstractPayment, "xrCosSubMultiAbstractPayment");
            this.xrCosSubMultiAbstractPayment.Name = "xrCosSubMultiAbstractPayment";
            this.xrCosSubMultiAbstractPayment.ReportSource = new Bosco.Report.ReportObject.CCMultiAbstractPayments();
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.StyleName = "styleTitleRow";
            // 
            // CCMultiAbstractReceiptsAndPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.DetailReport});
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand dtlReceipt;
        private DevExpress.XtraReports.UI.XRSubreport xrCosSubMultiAbstractReceipt;
        private DevExpress.XtraReports.UI.XRSubreport xrCosSubMultiAbstractPayment;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.DetailReportBand dtlMulti;
        private DevExpress.XtraReports.UI.DetailBand dtlPayment;
    }
}

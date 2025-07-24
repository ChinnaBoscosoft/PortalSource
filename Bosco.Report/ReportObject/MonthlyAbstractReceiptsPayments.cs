using System;
using Bosco.Report.Base;


namespace Bosco.Report.ReportObject
{
    public partial class MonthlyAbstractReceiptsPayments :ReportHeaderBase
    {
        public MonthlyAbstractReceiptsPayments()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
        }
        public override void ShowReport()
        {
            LoadMonthlyAbstractReport();
            base.ShowReport();
        }

        private void LoadMonthlyAbstractReport()
        {
            SetReportTitle();
            this.SetLandscapeFooterDateWidth = 505.25f;
            setHeaderTitleAlignment();
            MonthlyAbstractReceipts montlyAbstractReceipts = xrSubreportMonthlyReceipts.ReportSource as MonthlyAbstractReceipts;
            MonthlyAbstractPayments montlyAbstractPayments = xrSubreportMontlyPayments.ReportSource as MonthlyAbstractPayments;
            this.AttachDrillDownToSubReport(montlyAbstractReceipts);
            this.AttachDrillDownToSubReport(montlyAbstractPayments);
            montlyAbstractReceipts.HideReportHeaderFooter();
            montlyAbstractPayments.HideReportHeaderFooter();
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                montlyAbstractReceipts.BindReceiptSource();
                montlyAbstractPayments.BindPaymentSource();
            }
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
        }
    }
}

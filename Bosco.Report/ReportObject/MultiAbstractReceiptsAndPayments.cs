using System;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsAndPayments : ReportHeaderBase
    {
        public MultiAbstractReceiptsAndPayments()
        {
            InitializeComponent();
        //    this.SetTitleWidth(xrSubMultiAbstractReceipt.WidthF);
            this.SetLandscapeHeader = 1045.25f;
            this.SetLandscapeFooter = 1045.25f;
        }

        public override void ShowReport()
        {
            LoadMultiAbstractReport();
            base.ShowReport();
        }

        private void LoadMultiAbstractReport()
        {
            SetReportTitle();
            this.SetLandscapeFooterDateWidth = 880.25f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            MultiAbstractReceipts multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractReceipts;
            MultiAbstractPayments multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractPayments;
            multiAbstractReceipts.HideReportHeaderFooter();
            multiAbstractPayments.HideReportHeaderFooter();
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                ShowReportFilterDialog();
            }
            else
            {
                multiAbstractReceipts.BindMultiAbstractReceiptSource();
                multiAbstractPayments.BindMultiAbstractPaymentSource();
            }
        }
    }
}

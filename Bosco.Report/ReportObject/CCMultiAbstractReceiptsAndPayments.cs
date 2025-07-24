using System;

using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class CCMultiAbstractReceiptsAndPayments :ReportHeaderBase
    {

        public CCMultiAbstractReceiptsAndPayments()
        {
            InitializeComponent();
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
            setHeaderTitleAlignment();
            this.SetLandscapeFooterDateWidth = 883.25f;
            SetReportTitle();
            this.CosCenterName = objReportProperty.CostCentreName;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            CCMultiAbstractReceipts multiAbstractReceipts = xrCosSubMultiAbstractReceipt.ReportSource as CCMultiAbstractReceipts;
            CCMultiAbstractPayments multiAbstractPayments = xrCosSubMultiAbstractPayment.ReportSource as CCMultiAbstractPayments;

            multiAbstractReceipts.HideReportHeaderFooter();
            multiAbstractPayments.HideReportHeaderFooter();
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0")
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

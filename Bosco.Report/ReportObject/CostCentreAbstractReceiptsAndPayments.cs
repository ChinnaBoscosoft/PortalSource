using System;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreAbstractReceiptsAndPayments :ReportHeaderBase
    {
        public CostCentreAbstractReceiptsAndPayments()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 723.25f;
            this.SetLandscapeFooter = 723.25f;
        }

        public override void ShowReport()
        {
            LoadMonthlyAbstractReport();
            base.ShowReport();
        }

        private void LoadMonthlyAbstractReport()
        {
            setHeaderTitleAlignment();
            this.SetLandscapeFooterDateWidth = 505.25f;
            SetReportTitle();
            this.CosCenterName = objReportProperty.CostCentreName;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            CCMothlyReceipts montlyAbstractReceipts = xrSubMonthlyReceipts.ReportSource as CCMothlyReceipts;
            CostCentreMontlyAbstractPayments montlyAbstractPayments = xrSubreport1.ReportSource as CostCentreMontlyAbstractPayments;
            this.AttachDrillDownToSubReport(montlyAbstractReceipts);
            this.AttachDrillDownToSubReport(montlyAbstractPayments);
            montlyAbstractReceipts.HideReportHeaderFooter();
            montlyAbstractPayments.HideReportHeaderFooter();
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                            || this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                montlyAbstractReceipts.BindReceiptSource();
                montlyAbstractPayments.BindPaymentSource();
            }
        }

    }
}

using System;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;

namespace Bosco.Report.ReportObject
{
    public enum IEHouseDetailAc
    {
        InterAcTransfer = 0,
        ContributionFromProvince = 1,
        ContributionToProvince = 2
    }
    

    public partial class ProfitAndLossofHousesInterAcDetail : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        private string Caption_InterACTransfer = "Inter Account Transfer";
        private string Caption_ContributionFromProvince = "Contribution from Province";
        private string Caption_ContributionToProvince = "Contribution to Province";
        private int SocietyId = 0;
        private IEHouseDetailAc detailAccount = IEHouseDetailAc.InterAcTransfer;
        #endregion

        #region Properties
        public float TitleColumnWidth
        {
            set
            {
                xrCapTitle.WidthF = xrTitle.WidthF = value;
            }
        }

        public float DateColumnWidth
        {
            set
            {
                xrCapDate.WidthF = xrVoucherDate.WidthF = value;
                xrSubTotal.WidthF = xrCapTitle.WidthF + xrCapDate.WidthF;
                xrGrandTotal.WidthF = xrCapTitle.WidthF + xrCapDate.WidthF;
                xrCellProjectName.WidthF = xrCapTitle.WidthF + xrCapDate.WidthF;
            }
        }

        public float ReceiptsColumnWidth
        {
            set
            {
                xrCapReceipts.WidthF = xrProjectCreditSum.WidthF = xrCredit.WidthF = xrCreditSum.WidthF = xrCreditSumTotal.WidthF = value;
            }
        }

        public float PaymentsColumnWidth
        {
            set
            {
                xrCapPayments.WidthF = xrProjectDebitSum.WidthF = xrDebit.WidthF = xrDebitSum.WidthF = xrDebitSumTotal.WidthF = value;
            }
        }

        public float LedgerTitleWidth
        {
            set
            {
                xrlblLedgerName.WidthF  = value;
            }
        }

        #endregion

        #region Constructor

        public ProfitAndLossofHousesInterAcDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            //BindPAndLHousesInterAcDetail();
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void HidePAndLHousesInterAcDetailHeaders()
        {
            this.HidePageHeader = this.HidePageFooter = this.HidePageInfo = false;
            this.HidePageHeader = false;
            this.HideReportHeader = false;
        }

        public void BindPAndLHousesInterAcDetail(int societyId, DataTable dtPLHouseDetails , IEHouseDetailAc ieHouseDetailAc )
        {
            detailAccount = ieHouseDetailAc;
            SocietyId = societyId;
            this.SetLandscapeHeader = 1030.25f;
            this.SetLandscapeFooter = 1030.25f;
            this.SetLandscapeFooterDateWidth = 860.00f;
            
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (dtPLHouseDetails != null)
                {
                    Detail.Visible = grpProjectFooter.Visible = (this.ReportProperties.ShowDetailedBalance == 1);
                    xrCapDate.Text = (this.ReportProperties.ShowDetailedBalance == 1? "Date" : string.Empty);
                    grpProjectHeader.HeightF = 25;
                    
                    if (dtPLHouseDetails != null)
                    {
                        xrlblLedgerName.Text = "Ledger Name : ";
                        if (detailAccount == IEHouseDetailAc.InterAcTransfer)
                        {
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.INTER_CRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName;
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.INTER_DRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName;
                            xrlblLedgerName.Text += Caption_InterACTransfer;
                        }
                        else if (detailAccount == IEHouseDetailAc.ContributionFromProvince )
                        {
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_CRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName;
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_FROM_DRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName;
                            xrlblLedgerName.Text += Caption_ContributionFromProvince;
                        }
                        else if (detailAccount == IEHouseDetailAc.ContributionToProvince)
                        {
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_CRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName;
                            dtPLHouseDetails.Columns[reportSetting1.ProfitandLossbyHouse.CONTRIBUTION_TO_DRColumn.ColumnName].ColumnName = reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName;
                            xrlblLedgerName.Text += Caption_ContributionToProvince;
                        }

                        dtPLHouseDetails.DefaultView.RowFilter = "(" + reportSetting1.ProfitandLossbyHouse.RECEIPTColumn.ColumnName + " >0 OR "+
                                                                    reportSetting1.ProfitandLossbyHouse.PAYMENTColumn.ColumnName + " >0 "+
                                                                 ")";
                        dtPLHouseDetails.TableName = this.DataMember;
                        this.DataSource = dtPLHouseDetails;
                        this.DataMember = dtPLHouseDetails.TableName;
                        grpLedgerHeader.Visible = grpProjectHeader.Visible = Detail.Visible = grpProjectFooter.Visible = ReportFooter.Visible = (dtPLHouseDetails.DefaultView.Count > 0);
                    }
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private void MakeHighlightColor(XRTableCell xrcell)
        {
            if (GetCurrentColumnValue("CUSTOMERID") != null)
            {
                if (this.ReportProperties.ShowInterAccountDetails == 1)
                {
                    xrcell.Font = new Font(xrcell.Font, FontStyle.Bold);
                    xrcell.BackColor = Color.LightYellow;
                }
                else if (this.ReportProperties.ShowProvinceFromToContributionDetails == 1)
                {
                    if (detailAccount == IEHouseDetailAc.ContributionFromProvince)
                        xrcell.BackColor = Color.LightGreen;
                    else
                        xrcell.BackColor = Color.LightBlue;
                    xrcell.Font = new Font(xrcell.Font, FontStyle.Bold);
                }
            }
        }

        #endregion

        #region Events
        private void xrCreditSumTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("CUSTOMERID") != null)
            {
                MakeHighlightColor((sender as XRTableCell));
            }
        }

        private void xrDebitSumTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("CUSTOMERID") != null)
            {
                MakeHighlightColor((sender as XRTableCell));
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(this.ReportProperties.ShowDetailedBalance == 1);
        }

        private void grpProjectFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(this.ReportProperties.ShowDetailedBalance == 1);

        }
        #endregion
    }
}

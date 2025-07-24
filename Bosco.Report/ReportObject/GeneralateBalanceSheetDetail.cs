using System;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;

namespace Bosco.Report.ReportObject
{
    
    public partial class GeneralateBalanceSheetDetail: Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;  
        #endregion

        #region Properties
        private DataTable gbConLedgerRptSourceDetails = new DataTable();
        private DataTable GBConLedgerRptSourceDetails
        {
            get
            {
                return gbConLedgerRptSourceDetails;
            }
            set
            {
                gbConLedgerRptSourceDetails = value;
            }
        }

        public float TitleColumnWidth
        {
            set
            {
                xrCapTitle.WidthF = value-20;
                xrTitle.WidthF = value-20;
                //xrFooterTitle.WidthF = value-10;
                //xrLblBottomBorder.WidthF = xrTitle.WidthF + xrCapCurrentYear.WidthF + xrCurrentYear.WidthF;
                //xrlblline.WidthF = value - 10; ;
            }
        }
        
        public float AmountColumnWidth
        {
            set
            {
                xrCapCurrentYear.WidthF = xrCurrentYear.WidthF = value;
                //xrLblBottomBorder.WidthF = xrTitle.WidthF + xrCapCurrentYear.WidthF + xrCurrentYear.WidthF;
            }
        }

        public float FooterBorderWidth
        {
            set
            {
                xrlblline.LeftF = 0;
                xrlblline.WidthF = value;
            }
        }
        #endregion

        #region Constructor

        public GeneralateBalanceSheetDetail()
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

        public void HideGBConLedgerDetailHeaders()
        {
            this.HidePageHeader = this.HidePageFooter = this.HidePageInfo = false;
            this.HidePageHeader = false;
            this.HideReportHeader = false;
        }

        public void BindGBConLedgerDetail(DataTable dtConLedgerDetails)
        {
            this.SetLandscapeHeader = 1030.25f;
            this.SetLandscapeFooter = 1030.25f;
            this.SetLandscapeFooterDateWidth = 860.00f;
            grpFooter.Visible = Detail.Visible = false;
            if (dtConLedgerDetails != null)
            {
                dtConLedgerDetails.DefaultView.RowFilter = "FINAL <> 0";
                dtConLedgerDetails.DefaultView.Sort = "SOCIETYNAME";
                dtConLedgerDetails = dtConLedgerDetails.DefaultView.ToTable();
                GBConLedgerRptSourceDetails = dtConLedgerDetails;
                dtConLedgerDetails.TableName = this.DataMember;
                this.DataSource = dtConLedgerDetails;
                this.DataMember = dtConLedgerDetails.TableName;
                grpFooter.Visible = Detail.Visible = (dtConLedgerDetails.Rows.Count > 0);
                xrlblline.Visible = (dtConLedgerDetails.Rows.Count > 0);
            }
        }
        #endregion

        #region Events
        private void xrCreditSumTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrDebitSumTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = !(this.ReportProperties.ShowDetailedBalance == 1);
        }

        private void grpProjectFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = !(this.ReportProperties.ShowDetailedBalance == 1);

        }
        #endregion
    }
}

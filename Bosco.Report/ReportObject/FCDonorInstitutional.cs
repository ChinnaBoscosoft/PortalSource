using System.Data;

using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class FCDonorInstitutional : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FCDonorInstitutional()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            tblFCDonorHeader.WidthF = xrtblDetails.WidthF = xrTable1.WidthF = tblGrandTotal.WidthF = 670;
            xrtblFDGroup.WidthF = 672;
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFCDonor();
            base.ShowReport();
        }
        #endregion

        #region Method
        private void BindFCDonor()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();
                //this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                this.ReportSubTitle = this.ReportProperties.ProjectTitle;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrtblAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                DataTable dtFCDonor = GetReportSource();
                if (ReportProperties.ShowDonorAddress != 0)
                {
                    xrDonorAddress.Visible = true;
                    xrDonorName.WidthF = (float)132.21;
                }
                else
                {
                    xrDonorName.WidthF = xrDonorAddress.WidthF + xrDonorName.WidthF;
                }

                if (dtFCDonor != null)
                {
                    this.DataSource = dtFCDonor;
                    this.DataMember = dtFCDonor.TableName;
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }
        private DataTable GetReportSource()
        {
            string FcDonor = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCDonorInstitutional);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.FCDonorInstitutional,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }
                if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FcDonor);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            tblFCDonorHeader = SetHeadingTableBorder(tblFCDonorHeader, this.ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblFDGroup = SetBorders(xrtblFDGroup, this.ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblDetails = SetBorders(xrtblDetails, this.ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            tblGrandTotal = SetBorders(tblGrandTotal, this.ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);

        }
        #endregion

        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrFDAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrFDAmount.Text = "";
            }
        }
    }
}

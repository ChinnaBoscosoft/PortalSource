using System.Data;

using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class FCDonorIndividual : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public FCDonorIndividual()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrHeader.WidthF = xrtblFDIndividualGroup.WidthF = xrtblTotal.WidthF = xrtblGrandTotal.WidthF = 670;
            xrGroupIndividual.WidthF = 672;
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFCDonorIndividual();
            base.ShowReport();
        }
        #endregion

        #region Method
        private void BindFCDonorIndividual()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();
                this.ReportSubTitle = this.ReportProperties.ProjectTitle;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrCapAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                if (ReportProperties.ShowDonorAddress != 0)
                {
                    xrDonorAddress.Visible = true;
                    xrDonorName.WidthF = (float)135.2;
                }
                else
                {
                    xrDonorName.WidthF = xrDonorName.WidthF + xrDonorAddress.WidthF;
                }
                DataTable dtFCDonor = GetReportSource();
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
            string FcDonorIndividual = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCDonorIndividual);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.FCDonorIndividual,DataBaseType.HeadOffice))
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
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FcDonorIndividual);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            xrHeader = SetHeadingTableBorder(xrHeader, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrGroupIndividual = SetBorders(xrGroupIndividual, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblFDIndividualGroup = SetBorders(xrtblFDIndividualGroup, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblTotal = SetBorders(xrtblTotal, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblGrandTotal = SetBorders(xrtblGrandTotal, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            
        }
        #endregion

        private void xrtblDetailsAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrtblDetailsAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblDetailsAmount.Text = "";
            }


        }
    }
}

using System;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class FCCountry : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        double DonorAmt = 0;
        double DonorTotalAmt = 0;
        #endregion

        #region Constructor
        public FCCountry()
        {
            InitializeComponent();
            this.SetLandscapeHeader = 670.25f;
            this.SetLandscapeFooter = 670.25f;
            xrtblHeaderCaption.WidthF = xrTable1.WidthF = xrtblGrandTotal.WidthF = 670;
        }

        #endregion

        #region Show Report
        public override void ShowReport()
        {
            FCCountryReport();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void FCCountryReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                setHeaderTitleAlignment();
                this.SetLandscapeFooterDateWidth = 505.25f;
                SetReportTitle();
                //this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                this.ReportSubTitle = this.ReportProperties.ProjectTitle;
                xrCapAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);

                DataTable dtFCCountry = GetReportSource();
                if (dtFCCountry != null)
                {
                    dtFCCountry.TableName = "FCCountry";
                    this.DataSource = dtFCCountry;
                    this.DataMember = dtFCCountry.TableName;
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
            try
            {
                string FCCountry = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCCountry);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.ForeginContribution.FCCountry,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    if (!string.IsNullOrEmpty(ReportProperties.BranchOffice) && ReportProperties.BranchOffice != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    }
                    if (!string.IsNullOrEmpty(ReportProperties.Society) && ReportProperties.Society != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FCCountry);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrTable1 = SetBorders(xrTable1, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrtblGrandTotal = SetBorders(xrtblGrandTotal, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
        }
        #endregion

        #region Events
        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrAmount.Text = "";
            }
        }
        #endregion
    }
}

using System;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BankChequeRealized : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BankChequeRealized()
        {
            InitializeComponent();
            /*this.AttachDrillDownToRecord(xrtblBankRealized, xrParticulars,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");*/
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindBankChequeRealized();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindBankChequeRealized()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                && !string.IsNullOrEmpty(this.ReportProperties.Project)
                && this.ReportProperties.Ledger != "0")
            {
              //  SplashScreenManager.ShowForm(typeof(frmReportWait));
                SetReportTitle();
                setHeaderTitleAlignment();
                xrCapAmount.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.AMOUNT);
                //  this.ReportTitle = objReportProperty.ReportTitle;
                //  this.ReportSubTitle = objReportProperty.ProjectTitle;
                this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                resultArgs = GetReportSource();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "ChequeUncleared";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
                else
                {
                    this.DataSource = null;
                }
             //   SplashScreenManager.CloseForm();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportSetup();
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string bankCleared = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeRealiszed);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.BankReport.ChequeRealiszed,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.LedgalEntityId);
                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, "0");
                    }
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, bankCleared);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapCode.Tag != null && xrCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapCode.Tag.ToString());
            }
            else
            {
                xrCapCode.Tag = xrCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
        }

        public void SetReportBorder()
        {
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblBankRealized = SetBorders(xrtblBankRealized, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
            xrtblAmount = SetBorders(xrtblAmount, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }
        #endregion

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
    }
}

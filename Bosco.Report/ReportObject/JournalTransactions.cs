using System;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class JournalTransactions : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public JournalTransactions()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindJournalTransactions();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void BindJournalTransactions()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
               // SplashScreenManager.ShowForm(typeof(frmReportWait));
                //  this.ReportTitle = objReportProperty.ReportTitle;
                //  this.ReportSubTitle = objReportProperty.ProjectTitle;
                setHeaderTitleAlignment();
                SetReportTitle();
                // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                xrCapCredit.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.CREDIT);
                xrCapDebit.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.DEBIT);
                resultArgs = GetReportSource();
                DataView dvCashBank = resultArgs.DataSource.TableView;
                if (dvCashBank != null)
                {
                    dvCashBank.Table.TableName = "CashBankTransactions";
                    this.DataSource = dvCashBank;
                    this.DataMember = dvCashBank.Table.TableName;
                }
                //SplashScreenManager.CloseForm();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            xrtblHeaderCaption = SetHeadingTableBorder(xrtblHeaderCaption, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            xrtblCashBankTrans = SetBorders(xrtblCashBankTrans, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankTransaction = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.JournalTransactions);
                using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CashBankVoucher.JournalTransactions,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankTransaction);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrCredit.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCredit.Text = "";
            }
        }

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrDebit.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrDebit.Text = "";
            }
        }

    }
}

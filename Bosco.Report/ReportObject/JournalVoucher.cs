using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class JournalVoucher : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Debitamt = 0;
        #endregion

        #region Constructor
        public JournalVoucher()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            this.HidePageFooter = this.HideReportHeader = false;
            BindJournal();
            base.ShowReport();
        }
        public override void VoucherPrint()
        {
            this.HidePageFooter = this.HideReportHeader = false;
            BindJournal();

            this.Print();

        }
        #endregion

        #region Method
        public void BindJournal()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                //  this.ReportTitle = objReportProperty.ReportTitle;
                setHeaderTitleAlignment();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                // this.ReportSubTitle = objReportProperty.ProjectTitle;
                //this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                //this.HideDateRange = false;
                if (!string.IsNullOrEmpty(objReportProperty.ProjectTitle))
                {
                    string[] projectName = objReportProperty.ProjectTitle.Split('-');
                    xrlblJournalinsName.Text = projectName[0];
                }

                xrlblInsName.Text = this.GetInstituteName();
                xrlblInsAddress.Text = objReportProperty.LegalAddress;
                // xrlblJournalinsName.Text = settings.InstituteName;
                resultArgs = GetJournal();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankJournal";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            else
            {
                xrlblJournalinsName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
        }
        public ResultArgs GetJournal()
        {
            //string JournalVoucher = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.JournalVoucher);
            //using (DataManager dataManager = new DataManager())
            //{
            //    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
            //    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, JournalVoucher);
            //}
            //return resultArgs;
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchJournalByVoucher);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.CashBankVoucher.FetchJournalByVoucher,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                this.ReportProperties.PrintCashBankVoucherId = string.Empty;
            }
            return resultArgs;
        }
        #endregion

        private void xrtblRuppee_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) != null)
                Debitamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
        }

        private void xrtblRuppee_SummaryReset(object sender, EventArgs e)
        {
            Debitamt = 0;
        }

        private void xrtblRuppee_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Debitamt.ToString());
            e.Handled = true;
        }

        private void xrDebitAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double DebitAmount = this.ReportProperties.NumberSet.ToDouble(xrDebitAmount.Text);
            xrDebitAmount.Text = objReportProperty.NumberSet.ToCurrency(DebitAmount);
            if (DebitAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrDebitAmount.Text = "";
            }
        }

        private void xrCreditAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CreditAmount = this.ReportProperties.NumberSet.ToDouble(xrCreditAmount.Text);
            xrCreditAmount.Text = objReportProperty.NumberSet.ToCurrency(CreditAmount);
            if (CreditAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCreditAmount.Text = "";
            }
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double sumdebit = objReportProperty.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = objReportProperty.NumberSet.ToCurrency(sumdebit);
        }

        private void xrTableCell18_SummaryRowChanged(object sender, EventArgs e)
        {
            //double sumdebit = objReportProperty.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = objReportProperty.NumberSet.ToCurrency(sumdebit);
        }

        private void xrTableCell18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double sumdebit = objReportProperty.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = objReportProperty.NumberSet.ToCurrency(sumdebit);
        }
    }
}
